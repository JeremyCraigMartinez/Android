using System;
using System.IO;
using System.Text;
using System.Net.Http;
using Android.Util;

namespace api_interaction_kit
{
	public partial class api
	{
		public Object request_user_data(string username)
		{
			try
			{
				HttpResponseMessage response = client.GetAsync("patients/" + username).Result;
				if(response.IsSuccessStatusCode)
				{
					var data = response.Content.ReadAsStreamAsync().Result;
					return json_functions.deserializer(data, typeof(user_information));
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
			try {
				login_information user = new login_information (){ email = Email, pass = Pass };
				MemoryStream stream = new MemoryStream ();
				json_functions.serializer (user, user.GetType (), ref stream);
				HttpContent content = new StreamContent (stream);
				HttpResponseMessage response = client.PostAsync ("user/create", content).Result;
				if (response.IsSuccessStatusCode) {
				
				}
			} catch (Exception e) {
				string err = e.ToString ();
			}
		}
	}
}

