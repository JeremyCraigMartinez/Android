using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Threading.Tasks;
using Android.Util;

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
		user_created,
		user_info
	}

	public partial class api
	{
		#region Variables

		// API IP and Port
		private const int server_port = 5024;
		private const string server_ip = "104.236.169.12";

		//Announces what's going on
		public delegate void Announcment(string input);
		public event Announcment announcment;

		public delegate void Server_Update (Object o, Response_Type r);
		public event Server_Update server_update;

		States state;

		HttpClient client;

		List<event_object> events;

		bool run_lock;

		#endregion

		public api()
		{
			announcment += listener;
			state_change(ref state, States.Initializing);
		}

		/// <summary>
		/// Listens for special announcements
		/// </summary>
		/// <param name="input">Input.</param>
		private void listener(string input)
		{
			if (input.CompareTo("Initialization Complete") == 0)
				state_change(ref state, States.Running);
			else if (input.Contains("Error"))
				state_change(ref state, States.Stopping);
		}
		/// <summary>
		/// Changes the state
		/// </summary>
		/// <param name="old_state">Old state.</param>
		/// <param name="new_state">New state.</param>
		private void state_change(ref States old_state, States new_state)
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
					initialize();
					announcment("Initialization Complete");
					break;
				case States.Running:
					Task t = new Task(start);
					t.Start();
					break;
				case States.Stopping:
					break;
			}
		}
		/// <summary>
		/// Initializes the connection.
		/// </summary>
		private void initialize()
		{
			run_lock = false;
			try {
				client = new HttpClient();
				client.BaseAddress = new Uri("http://" + server_ip + ":" + server_port + "/" );
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			} catch {
				announcment("Error: C001");
			} //C001 = can't open a connection to the api
		}

		/// <summary>
		/// API's best friend; they could talk forever
		/// </summary>
		private void start ()
		{
			events = new List<event_object> ();
			bool clear = false;
			while (state == States.Running) 
			{
				if (!run_lock) {
					foreach (event_object e in events) {
						e.execute ();
						clear = true;
					}
				}
				if (events.Count != 0 && clear) {
					events.Clear ();
					clear = false;
				}
			}
		}
		public void api_update_user_data(string username)
		{
			run_lock = true;
			events.Add(new request_user_event(username, this));
			run_lock = false;
		}
		public void api_create_new_user(string username, string password)
		{
			run_lock = true;
			events.Add (new create_user_event (username, password, this));
			run_lock = false;
		}
		public void server_response_helper(Object o, Response_Type r)
		{
			server_update (o, r);
		}
	}
}