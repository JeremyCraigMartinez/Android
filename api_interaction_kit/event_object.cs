﻿using System;

namespace api_interaction_kit
{
	public abstract class event_object
	{
		protected Object data;

		protected event_object () { data = new Object (); }
		protected event_object (Object d) { data = d; }

		protected api reference;

		public abstract void execute ();
	}

	public class request_user_event : event_object
	{

		public request_user_event(string Username, api Reference) { data = Username; reference = Reference;}
		public override void execute ()
		{
			reference.server_response_helper(reference.request_user_data((string)data), Response_Type.user_info);
		}
	}

	public class create_user_event : event_object
	{
		string user = "";
		string p = "";
		public create_user_event(string Username, string Pass, api Reference) { user = Username; p = Pass; reference = Reference; }
		public override void execute()
		{
			reference.create_user (user, p);
		}
	}
	public class request_accel_event : event_object
	{
		public request_accel_event() { }
		public override void execute()
		{
			
		}
	}
	public class request_create_group_event : event_object
	{
		public request_create_group_event(string group, api Reference) { data = group; reference = Reference;}
		public override void execute ()
		{
			reference.create_group (data.ToString ());
		}
	}
}