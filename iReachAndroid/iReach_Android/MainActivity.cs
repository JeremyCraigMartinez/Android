using System;
using Android.App;
using Android.Net;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using api_interaction_kit;

namespace iReach_Android
{
	[Activity (Label = "iReach_Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private enum State {Initialize, Log_In, Create_User_Page, Landing_Page, Account_Page, Exit}
		private State state;
		private api_interaction_kit.api interaction_kit;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature(WindowFeatures.NoTitle);
			change_state (ref state, State.Initialize);
		}

		private void change_state(ref State state, State new_state)
		{
			switch (state) {
			case State.Initialize:
				break;
			case State.Landing_Page:
				break;
			case State.Account_Page:
				break;
			}
			state = new_state;
			switch (state) {
			case State.Initialize:
				initialize ();
				Button login_button = FindViewById<Button> (Resource.Id.login_button);
				login_button.Click += Login_button_Click;
				break;
			case State.Landing_Page:
				SetContentView (Resource.Layout.landing_page);
				Button kit_kat = FindViewById<Button> (Resource.Id.food_button);
				kit_kat.Click += Kit_kat_Click;
				Button user_info_btn = FindViewById<Button> (Resource.Id.user_profile_button);
				user_info_btn.Click += User_info_btn_Click;
				break;
			case State.Account_Page:
				SetContentView (Resource.Layout.user_page);
				break;
			}
		}

		void User_info_btn_Click (object sender, EventArgs e)
		{
			interaction_kit.api_request_user_data ();
			SetContentView (Resource.Layout.user_page);
		}

		void Kit_kat_Click (object sender, EventArgs e)
		{
			interaction_kit.api_food_upload ("19250", 1);
		}

		void initialize()
		{
			interaction_kit = new api ();
			var c = (ConnectivityManager)GetSystemService (ConnectivityService);
			interaction_kit.initialize(ref c);
			interaction_kit.server_update += Interaction_kit_server_update;

			SetContentView (Resource.Layout.Main);
		}

		void Login_button_Click (object sender, EventArgs e)
		{
			var email = FindViewById<TextView> (Resource.Id.email_text_field);
			var pass = FindViewById<TextView> (Resource.Id.password_text_field);
			interaction_kit.login (email.Text, pass.Text);
		}

		void Interaction_kit_server_update (object o, Response_Type r)
		{
			if (r == Response_Type.login_result) {
				if ((bool)o)
					change_state (ref state, State.Landing_Page);
			}
			if (r == Response_Type.user_info) {
				if ((user_information)o != null) {
					RunOnUiThread (() => {
						user_information temp = (user_information)o;

						var email = FindViewById<TextView> (Resource.Id.email);
						var age = FindViewById<TextView> (Resource.Id.age);
						var group = FindViewById<TextView> (Resource.Id.group);
						var gender = FindViewById<TextView> (Resource.Id.gender);
						var height = FindViewById<TextView> (Resource.Id.height);
						var weight = FindViewById<TextView> (Resource.Id.weight);
						var first_name = FindViewById<TextView> (Resource.Id.first_name);
						var last_name = FindViewById<TextView> (Resource.Id.last_name);
						var doctor = FindViewById<TextView> (Resource.Id.doctor);

						email.Text = temp.email;
						age.Text = temp.age.ToString ();
						group.Text = temp.group [0];
						gender.Text = temp.sex;
						height.Text = temp.height.ToString ();
						weight.Text = temp.weight.ToString ();
						first_name.Text = temp.first_name;
						last_name.Text = temp.last_name;
						doctor.Text = temp.doctor;
					});
				}
			}
		}
	}
}


