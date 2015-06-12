using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using Android.Util;
using Newtonsoft.Json;

namespace api_interaction_kit
{
	public partial class api
	{
		public Object request_user_data()
		{
			return _get ("patients/" + userName, typeof(user_information));
		}
		public void create_user(string Email, string Pass)
		{
			post ("patients", json_functions.serializer (new user_information (){ email = Email }));
		}
		public void create_group(string name)
		{
			post("groups",json_functions.serializer(new group(){_id = name}));
		}
		public bool post(string location, string content)
		{
			HttpResponseMessage response = client.PostAsync (location,
				new StringContent(content, Encoding.UTF8, "application/json")).Result;
			if (response.IsSuccessStatusCode)
				return true;
			return false;
		}
		public Object _get(string location, Type T)
		{
			HttpResponseMessage response = client.GetAsync (location).Result;
			if (response.IsSuccessStatusCode) 
			{
				var data = response.Content.ReadAsStreamAsync().Result;
				return json_functions.deserializer(data, T);
			}
			announcment (Announcement_Type.Error);
			return null;
		}
	}
}

