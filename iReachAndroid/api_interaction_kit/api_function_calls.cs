using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using Android.Util;
using Newtonsoft.Json;
using System.Globalization;

namespace api_interaction_kit
{
	public partial class api
	{
		private Object request_user_data()
		{
			return _get ("patients", typeof(user_information));
		}

		private void create_user(string Email, string Pass)
		{
			post ("patients", json_functions.serializer (new user_information (){ email = Email }));
		}

		private void create_group(string name)
		{
			post("groups",json_functions.serializer(new group(){_id = name}));
		}

		private bool authenticate(string _email, string _password)
		{
			return post("auth", json_functions.serializer(new login_information() {email = _email, password = _password}));
		}

		private bool food(int _id, int _serving_size)
		{
			string now = Convert.ToString (DateTime.Now);
			string pattern = "HH:mm-MM-dd-yyyy";
			return post("diet", json_functions.serializer(new food_item() {foodID = Convert.ToString(_id), quantity = _serving_size,
				created = DateTime.ParseExact(now, pattern , CultureInfo.InvariantCulture).ToString()}));
		}

		private bool post(string location, string content)
		{
			HttpResponseMessage response = client.PostAsync (location,
				new StringContent(content, Encoding.UTF8, "application/json")).Result;
			if (response.IsSuccessStatusCode)
				return true;
			return false;
		}

		private Object _get(string location, Type T)
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

