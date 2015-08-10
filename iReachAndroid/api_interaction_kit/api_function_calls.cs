﻿using System;
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
		private Object request_user_data()
		{
			return _get ("patients/" + userName, typeof(user_information));
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
			return post("diets", json_functions.serializer(new food_item() {food_id = _id, serving_size = _serving_size, time_stamp = DateTime.Now}));
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

