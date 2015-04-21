using System;
using System.Net.Http;
using Android.Util;

namespace api_interaction_kit
{
	public partial class api
	{
		public user_information request_user_data(string username)
		{
			try
			{
				HttpResponseMessage response = client.GetAsync("user/" + username).Result;
				if(response.IsSuccessStatusCode)
				{
					var data = response.Content.ReadAsStreamAsync().Result;
					return(user_information)json_functions.deserializer(data, typeof(user_information));
				}
			} 
			catch (Exception e) {
				string err = e.ToString ();
				announcment ("Error: C001");
				Log.Error ("Server Connection Error", "C001");
				return null;
			}
			return null;
		}
		public void create_user(string Email, string Pass)
		{
			login_information user = new login_information(){ email = Email, pass = Pass };

//			HttpResponseMessage response = client.PostAsync ("/user/create/", user).Result;
//			if (response.IsSuccessStatusCode) 
//			{
//
//			}
		}
	}
}

