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
			create_user_information _user;

			public create_user_event (create_user_information user, api Reference)
			{
				_user = user;
				reference = Reference;
			}

			public override void execute ()
			{
				reference.server_response_helper(reference.create_user (_user), Response_Type.user_created);
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
			raw_data data;

			public raw_data_event(raw_data _data, api Reference) { data = _data; reference = Reference; }
			public override void execute()
			{
				bool t = reference.post_raw_data (data);
				reference.server_response_helper (t, Response_Type.raw_data);
			}
		}
		public class request_doctor_event : event_object
		{
			public request_doctor_event (api Reference) {reference = Reference;}

			public override void execute ()
			{
				Doctors d = (Doctors)reference.request_doctor_list ();
				reference.server_response_helper (d, Response_Type.doctor_list);
			}
		}
		public class request_group_event : event_object
		{
			public request_group_event (api Reference) {reference = Reference;}

			public override void execute ()
			{
				Groups g = (Groups)reference.request_group_list ();
				reference.server_response_helper (g, Response_Type.group_list);
			}
		}
	}
}