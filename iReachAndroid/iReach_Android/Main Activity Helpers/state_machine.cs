﻿using System;
using Android.Widget;
using api_interaction_kit;

namespace iReach_Android
{
	public partial class MainActivity
	{

		private void change_state(ref State state, State new_state)
		{
			switch (state) {
			case State.Initialize:
				if (initialized) {
					Button login_button = FindViewById<Button> (Resource.Id.login_button);
					login_button.Click -= Login_button_Click;
					Button create_user_btn = FindViewById<Button> (Resource.Id.Create_User_Button);
					create_user_btn.Click -= Create_user_btn_Click;
				}
				break;
			case State.Create_User_Page:
				Button create_cancel = FindViewById<Button> (Resource.Id.create_cancel_btn);
				Button create_submit_btn = FindViewById<Button> (Resource.Id.create_submit_btn);
				create_cancel.Click -= Create_cancel_Click;
				create_submit_btn.Click -= Create_submit_btn_Click;
				break;
			case State.Landing_Page:
				Button user_info_btn = FindViewById<Button> (Resource.Id.user_profile_button);
				user_info_btn.Click -= User_info_btn_Click;
				Button settings_btn = FindViewById<Button> (Resource.Id.settings_button);
				settings_btn.Click -= Settings_btn_Click;
				break;
			case State.Account_Page:
				break;
			case State.Settings_Page:
				Button force_push_btn = FindViewById<Button> (Resource.Id.force_push_button);
				force_push_btn.Click -= Force_push_btn_Click;
				Button settings_back_btn = FindViewById<Button> (Resource.Id.settings_back_button);
				settings_back_btn.Click -= Settings_back_btn_Click;	
				break;
			}

			state = new_state;

			switch (state) {
			case State.Initialize:
				initialize ();
				Button login_button = FindViewById<Button> (Resource.Id.login_button);
				login_button.Click += Login_button_Click;
				Button create_user_btn = FindViewById<Button> (Resource.Id.Create_User_Button);
				create_user_btn.Click += Create_user_btn_Click;
				break;
			case State.Create_User_Page:
				SetContentView (Resource.Layout.create_user);
				Button create_cancel = FindViewById<Button> (Resource.Id.create_cancel_btn);
				Button create_submit_btn = FindViewById<Button> (Resource.Id.create_submit_btn);
				create_cancel.Click += Create_cancel_Click;
				create_submit_btn.Click += Create_submit_btn_Click;
				interaction_kit.api_get_doctor_list ();
				interaction_kit.api_get_group_list ();
				break;
			case State.Landing_Page:
				SetContentView (Resource.Layout.landing_page);
//				Button kit_kat = FindViewById<Button> (Resource.Id.food_button);
//				kit_kat.Click += Kit_kat_Click;
				Button user_info_btn = FindViewById<Button> (Resource.Id.user_profile_button);
				user_info_btn.Click += User_info_btn_Click;
				Button settings_btn = FindViewById<Button> (Resource.Id.settings_button);
				settings_btn.Click += Settings_btn_Click;
				break;
			case State.Account_Page:
				SetContentView (Resource.Layout.user_page);
				break;
			case State.Settings_Page:
				SetContentView (Resource.Layout.settings_layout);
//				Spinner hertz_spinner = FindViewById<Spinner> (Resource.Id.sensor_spinner);
//				var adapter = ArrayAdapter.CreateFromResource (this, Resource.Array.sensor_settings, Resource.Layout.settings_layout);
//				hertz_spinner.ItemSelected += Hertz_spinner_ItemSelected;
//				adapter.SetDropDownViewResource (Resource.Layout.settings_layout);
//				hertz_spinner.Adapter = adapter;
				Button force_push_btn = FindViewById<Button> (Resource.Id.force_push_button);
				force_push_btn.Click += Force_push_btn_Click;
				Button settings_back_btn = FindViewById<Button> (Resource.Id.settings_back_button);
				settings_back_btn.Click += Settings_back_btn_Click;
				if (interaction_kit.force_pushing) { force_push_btn.SetBackgroundColor(Android.Graphics.Color.Turquoise); } 
				else { force_push_btn.SetBackgroundColor(Android.Graphics.Color.DarkRed); }
				break;
			}
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
						string grps = "";

						var email = FindViewById<TextView> (Resource.Id.email);
						var age = FindViewById<TextView> (Resource.Id.age);
						var group = FindViewById<TextView> (Resource.Id.group);
						var gender = FindViewById<TextView> (Resource.Id.gender);
						var height = FindViewById<TextView> (Resource.Id.height);
						var weight = FindViewById<TextView> (Resource.Id.weight);
						var first_name = FindViewById<TextView> (Resource.Id.first_name);
						var last_name = FindViewById<TextView> (Resource.Id.last_name);
						var doctor = FindViewById<TextView> (Resource.Id.doctor);

						foreach (string _temp in temp.group)
							grps += (_temp + " - ");

						email.Text = temp.email;
						age.Text = temp.age.ToString ();
						group.Text = grps;
						gender.Text = temp.sex;
						height.Text = temp.height.ToString ();
						weight.Text = temp.weight.ToString ();
						first_name.Text = temp.first_name;
						last_name.Text = temp.last_name;
						doctor.Text = temp.doctor;

						Button back = FindViewById<Button> (Resource.Id.user_back_btn);
						back.Click += Back_Click;

					});
				}
			}
			if (r == Response_Type.doctor_list) {
				//				if ((Doctors)o != null) {
				//					RunOnUiThread (() => {
				//						Doctors temp = (Doctors)o;
				//						var doctor = FindViewById<Spinner> (Resource.Id.create_doctor_spinner);
				//						ArrayAdapter a = new ArrayAdapter(this, Resource.Layout.create_user, temp.doctors);
				//						doctor.Adapter = a;
				//					});
				//				}
			}
			if (r == Response_Type.group_list) {
				//				if ((Groups)o != null) {
				//					RunOnUiThread (() => {
				//						var group = FindViewById<Spinner> (Resource.Id.create_group_spinner);
				//					});
				//				}
			}
			if (r == Response_Type.user_created) {
				//				if ((bool)o)
				//					interaction_kit.login (m_email, m_pass);
				//				else
				change_state (ref state, State.Initialize);
			}
		}
			
		void Back_Click (object sender, EventArgs e)
		{
			change_state (ref state, State.Landing_Page);
		}
	}
}
