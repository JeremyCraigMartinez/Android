using System;

namespace api_interaction_kit
{
	public partial class api
	{
		public abstract class event_object
		{
			protected Object data;

			protected event_object ()
			{
				data = new Object ();
			}

			protected event_object (Object d)
			{
				data = d;
			}

			protected api reference;

			public abstract void execute ();
		}

		public class request_user_event : event_object
		{

			public request_user_event (api Reference)
			{
				reference = Reference;
			}

			public override void execute ()
			{
				user_information u = (user_information)reference.request_user_data ();
				reference.server_response_helper (u, Response_Type.user_info);
			}
		}

		public class create_user_event : event_object
		{
			user_information _user;

			public create_user_event (user_information user, api Reference)
			{
				_user = user;
				reference = Reference;
			}

			public override void execute ()
			{
				reference.create_user (_user);
			}
		}

		public class request_accel_event : event_object
		{
			public request_accel_event ()
			{
				
			}

			public override void execute ()
			{
			
			}
		}

		public class request_create_group_event : event_object
		{
			public request_create_group_event (string group, api Reference)
			{
				data = group;
				reference = Reference;
			}

			public override void execute ()
			{
				reference.create_group (data.ToString ());
			}
		}

		public class post_food_item : event_object
		{
			string id;
			int serving_size;
			public post_food_item ( string _id, int _serving_size, api Reference)
			{
				id = _id;
				serving_size = _serving_size;
				reference = Reference;
			}

			public override void execute ()
			{
				bool t = reference.food (id, serving_size);
				reference.server_response_helper (t, Response_Type.food_sent);
			}
		}
		public class raw_data_event: event_object
		{
			string time_stamp;
			string data;

			public raw_data_event(string _time_stamp, string _data, api Reference) { time_stamp = _time_stamp; data = _data; reference = Reference; }
			public override void execute()
			{
				bool t = reference.post_raw_data (time_stamp, data);
				reference.server_response_helper (t, Response_Type.raw_data);
			}
		}
	}
}