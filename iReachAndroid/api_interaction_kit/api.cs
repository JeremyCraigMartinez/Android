using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android;
using Android.App;

namespace api_interaction_kit
{
	public enum States
	{
		Initializing,
		Running,
		Offline,
		Stopping
	}

	public enum Response_Type
	{
		food_sent,
		login_result,
		user_created,
		user_info
	}

	public enum Announcement_Type
	{
		Initialization_Complete,
		Error
	}

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

		List<event_object> events;

		bool run_lock;

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
			client.BaseAddress = new Uri ("https://" + server_ip + ":" + server_port + "/");
			client.DefaultRequestHeaders.Accept.Clear ();
			client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

			if (authenticate (username, pass)) {
				userName = username;
				password = pass;
				server_update (true, Response_Type.login_result);
				state_change (ref state, States.Initializing);
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
			if (input == Announcement_Type.Initialization_Complete)
				state_change (ref state, States.Running);
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
				break;
			case States.Running:
				break;
			case States.Stopping:
				break;
			}
			old_state = new_state;
			switch (old_state) {
			case States.Initializing:
				initialize ();
				announcment (Announcement_Type.Initialization_Complete);
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

		/// <summary>
		/// Initializes the connection.
		/// </summary>
		private void initialize ()
		{
			run_lock = false;
			connect ();
		}

		private void connect ()
		{

			var basic_auth_header = Encoding.ASCII.GetBytes (userName + ":" + password);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Basic",
				Convert.ToBase64String (basic_auth_header));

		}

		/// <summary>
		/// API's best friend; they could talk forever
		/// </summary>
		private void start ()
		{
			events = new List<event_object> ();
			bool clear = false;
			while (state == States.Running) {
				try {
					if (!run_lock && events.Count != 0) {
						foreach (event_object e in events) {
							e.execute ();
							clear = true;
						}
					} else if (events.Count != 0 && clear) {
						events.Clear ();
						clear = false;
					}
				} catch (Exception ex) {
					string e = ex.ToString ();
				}
			}
		}

		public void api_update_user_data (string username)
		{
			run_lock = true;
			events.Add (new request_user_event (username, this));
			run_lock = false;
		}

		public void api_create_new_user (string username, string password)
		{
			run_lock = true;
			events.Add (new create_user_event (username, password, this));
			run_lock = false;
		}

		public void server_response_helper (Object o, Response_Type r)
		{
			server_update (o, r);
		}

		public void api_create_group (string name)
		{
			run_lock = true;
			events.Add (new request_create_group_event (name, this));
			run_lock = false;
		}

		public void api_food_upload (string id, int serving_size)
		{
			run_lock = true;
			events.Add (new post_food_item(id, serving_size, this));
			run_lock = false;
		}

		private void exit ()
		{
			client.Dispose ();
			events.Clear ();
		}
	}
}