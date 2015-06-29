using System;

namespace iReach.Core.Helpers
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


	public class Constants
	{
		static int server_port = 5024;
		static string server_address = "104.236.169.12";
	}
}

