using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Android;
using Android.App;
using Android.Net;

namespace api_interaction_kit
{
	public enum States { Initializing, LogIn, Running, Offline, Stopping }

	public enum Response_Type { food_sent, login_result, user_created, user_info, raw_data }

	public enum Announcement_Type { Initialization_Complete, Log_In_Complete, Error }

	public partial class api
	{
		#region Variables

		// API IP and Porta
		private const int server_port = 5025;
		private const string server_ip = "104.236.169.12";

		private string userName;
		private string password;

		 
		//Announces what's going on
		public delegate void Announcment (Announcement_Type input);

		public event Announcment announcment;

		public delegate void Server_Update (Object o, Response_Type r);

		public event Server_Update server_update;

		States state;

		HttpClient client;

		ConcurrentQueue<event_object> event_queue;
		ConcurrentQueue<event_object> long_term_storage; //ice cold raw data

		hardware_inspector inspector;

		#endregion

		public api ()
		{
			announcment += listener;
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};
		}

		public void login (string username, string pass)
		{
			client = new HttpClient ();
			client.BaseAddress = new System.Uri ("https://" + server_ip + ":" + server_port + "/");
			client.DefaultRequestHeaders.Accept.Clear ();
			client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

			if (authenticate (username, pass)) {
				userName = username;
				password = pass;
				state_change (ref state, States.LogIn);
			} 
			else
				server_update (false, Response_Type.login_result);
		}

		/// <summary>
		/// Listens for special announcements
		/// </summary>
		/// <param name="input">Input.</param>
		private void listener (Announcement_Type input)
		{
			if (input == Announcement_Type.Log_In_Complete) {
				server_update (true, Response_Type.login_result);
				state_change (ref state, States.Running);
			}
			else if (input == Announcement_Type.Error)
				state_change (ref state, States.Stopping);
		}

		/// <summary>
		/// Changes the state
		/// </summary>
		/// <param name="old_state">Old state.</param>
		/// <param name="new_state">New state.</param>
		private void state_change (ref States old_state, States new_state)
		{
			switch (old_state) {
			case States.Initializing:
				announcment (Announcement_Type.Initialization_Complete);
				break;
			case States.LogIn:
				break;
			case States.Running:
				break;
			case States.Stopping:
				break;
			}
			old_state = new_state;
			switch (old_state) {
			case States.Initializing:
				break;
			case States.LogIn:
				connect ();
				break;
			case States.Running:
				Task t = new Task (start);
				t.Start ();
				break;
			case States.Stopping:
				exit ();
				break;
			}
		}

		public void initialize (ref ConnectivityManager m)
		{
			inspector = new hardware_inspector(ref m);
		}

		private void connect ()
		{

			var basic_auth_header = Encoding.ASCII.GetBytes (userName + ":" + password);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Basic",
				Convert.ToBase64String (basic_auth_header));
			announcment (Announcement_Type.Log_In_Complete);

		}

		/// <summary>
		/// API's best friend; they could talk forever
		/// </summary>
		private void start ()
		{
			event_queue = new ConcurrentQueue<event_object> ();
			long_term_storage = new ConcurrentQueue<event_object> ();
			while (state == States.Running) {

				if (!event_queue.IsEmpty) 
				{
					event_object e;
					if (event_queue.TryDequeue (out e))
						e.execute ();
				}

				if (!long_term_storage.IsEmpty &&
					inspector.connected_to_wifi()) 
				{
					event_object e;
					if (long_term_storage.TryDequeue (out e))
						e.execute ();
				}
			}
		}

		public void api_request_user_data ()
		{
			event_queue.Enqueue(new request_user_event (this));
		}

		public void api_create_new_user (user_information user)
		{
			//events.Add (new create_user_event (user, this));
			event_queue.Enqueue(new create_user_event (user, this));
		}

		public void server_response_helper (Object o, Response_Type r)
		{
			server_update (o, r);
		}

		public void api_create_group (string name)
		{
			//events.Add (new request_create_group_event (name, this));
			event_queue.Enqueue(new request_create_group_event (name, this));
		}

		public void api_food_upload (string id, int serving_size)
		{
			//events.Add (new post_food_item(id, serving_size, this));
			event_queue.Enqueue(new post_food_item(id, serving_size, this));
		}

		public void api_upload_raw_data(string time_stamp, string data)
		{
			//events.Add (new raw_data_event(time_stamp, data, this));
			event_queue.Enqueue(new raw_data_event(time_stamp, data, this));
		}
			

		private void exit ()
		{
			client.Dispose ();
		}
	}
}