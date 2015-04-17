using System;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading.Tasks;
using Android.Util;
using System.IO;

namespace api_interaction_kit
{
	public enum States
	{
		Initializing,
		Running,
		Offline,
		Stopping

	}

	public class api
	{
		#region Variables

		// API IP and Port
		private const int server_port = 5024;
		private const string server_ip = "104.236.169.12";

		//Announces what's going on
		public delegate void Announcment(string input);

		public event Announcment announcment;

		States state;

		TcpClient client;

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
		/// Makes sure the connection is still alive
		/// </summary>
		/// <returns><c>true</c>, if connection is alive, <c>false</c> otherwise.</returns>
		private bool is_alive()
		{ 
			return (client.Connected);
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
					client.Close();
					break;
			}
		}

		/// <summary>
		/// Initializes the connection.
		/// </summary>
		private void initialize()
		{
			try {
				client = new TcpClient(server_ip, server_port);
			} catch {
				announcment("Error: C001");
			} //C001 = can't open a connection to the api
		}

		/// <summary>
		/// API's best friend; they could talk forever
		/// </summary>
		private void start ()
		{
			while (state == States.Running) {
//				try {
//					byte[] b = new byte[client.ReceiveBufferSize];
//					SslStream stream = new SslStream(client.GetStream());
//					//System.Diagnostics.Debug.Write("Fetching Data");
//					byte[] buffer = new byte[2048];
//					if (stream.CanWrite)
//						stream.Write(buffer, 0, buffer.Length);
//					if (stream.CanRead)
//					{
//						stream.Read (b, 0, (int)client.ReceiveBufferSize);
//						Log.Info("Server Response", byte_to_str(b));
//					}
//					stream.Flush ();
//				} catch { 
//					announcment ("Error: C001");
//					Log.Error ("Server Connection Error", "C001");
//					if (!check_connection ())
//						state_change (ref state, States.Offline);
//					else
//						continue;
//				}
				request_user_data("");
			}
		}

		private void request_user_data(string username)
		{
			try {
				byte[] b = Encoding.ASCII.GetBytes(@"/user");//new byte[client.ReceiveBufferSize];
				NetworkStream stream = client.GetStream();
				//System.Diagnostics.Debug.Write("Fetching Data");
				byte[] buffer = new byte[2048];
				(using StreamReader sr = new StreamReader(stream))
				{

				}

			} catch { 
				announcment ("Error: C001");
				Log.Error ("Server Connection Error", "C001");
				if (!check_connection ())
					state_change (ref state, States.Offline);
			}
		}

		private string byte_to_str(byte[] arr)
		{
			string str = "";
			foreach (byte b in arr) 
			{
				if (Convert.ToChar (b) != '\0')
					str += Convert.ToChar (b);
			}
			return str;
		}
		/// <summary>
		/// Reattempts to connect to the API 3 times
		/// </summary>
		/// <returns><c>true</c>, if connection was checked, <c>false</c> otherwise.</returns>
		private bool check_connection()
		{
			for (int i = 0; i < 3; ++i) {
				announcment(String.Format("Trying to Reconnect {0}/3", i + 1));
				try {
					client = new TcpClient(server_ip, server_port);
					if (is_alive())
						return true;
				} catch {
					continue;
				}
			}
			return false;
		}
	}
}