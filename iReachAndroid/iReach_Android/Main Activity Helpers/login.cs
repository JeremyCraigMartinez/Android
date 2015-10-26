using System;
using Android.Widget;

namespace iReach_Android
{		
	public partial class MainActivity
	{
		void Login_button_Click (object sender, EventArgs e)
		{
			var email = FindViewById<TextView> (Resource.Id.email_text_field);
			var pass = FindViewById<TextView> (Resource.Id.password_text_field);
			interaction_kit.login (email.Text, pass.Text);
		}
	}
}

