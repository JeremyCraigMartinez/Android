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
				HttpResponseMessage response = client.GetAsync("user/" + username).Result;
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
				string str2 = Encoding.ASCII.GetString (stream.ToArray());
				//HttpContent content = new StreamContent (stream);
				//HttpContent content = new ByteArrayContent(stream.ToArray());
				HttpContent content = new StringContent(Encoding.ASCII.GetString(stream.ToArray()));
				HttpContent c2 = new StringContent(@"{email:xirdie@a.com,pass:123456}");
				//HttpResponseMessage response = client.PostAsync ("user/create", content).Result;
				HttpResponseMessage response = client.PostAsync ("user/create", c2).Result;
				if (response.IsSuccessStatusCode) {
				
				}
			} catch (Exception e) {
				string err = e.ToString ();
			}
		}
	}
}

