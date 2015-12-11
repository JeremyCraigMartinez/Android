using System;
using System.Collections.Generic;
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
				break;
			case State.Create_User_Page:
				Button create_submit_btn = FindViewById<Button> (Resource.Id.create_submit_btn);
				create_submit_btn.Click -= Create_submit_btn_Click;
				break;
			case State.Landing_Page:
				Button user_info_btn = FindViewById<Button> (Resource.Id.user_profile_button);
				user_info_btn.Click -= User_info_btn_Click;
				Button settings_btn = FindViewById<Button> (Resource.Id.settings_button);
				settings_btn.Click -= Settings_btn_Click;
				Button food_btn = FindViewById<Button> (Resource.Id.food_button);
				food_btn.Click -= Food_btn_Click;
				Button user_activity_btn = FindViewById<Button> (Resource.Id.b_user_activity);
				user_activity_btn.Click -= User_activity_btn_Click;
				break;
			case State.Account_Page:
				break;
			case State.Settings_Page:
				Button force_push_btn = FindViewById<Button> (Resource.Id.force_push_button);
				force_push_btn.Click -= Force_push_btn_Click;
				break;
			case State.Food_Page:
//				Button food_submit_btn = FindViewById<Button> (Resource.Id.save_food_btn);
//				food_submit_btn.Click -= Food_submit_btn_Click;
				Button food_temp = FindViewById<Button> (Resource.Id.btn_save_food_temp);
				food_temp.Click -= Food_temp_Click;
				break;
			case State.User_Activity_Page:
				break;
			case State.Log_In:
				Button login_button = FindViewById<Button> (Resource.Id.login_button);
				login_button.Click -= Login_button_Click;
				Button create_user_btn = FindViewById<Button> (Resource.Id.Create_User_Button);
				create_user_btn.Click -= Create_user_btn_Click;
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
				Button create_submit_btn = FindViewById<Button> (Resource.Id.create_submit_btn);
				create_submit_btn.Click += Create_submit_btn_Click;
				if (doctor_array.Length > 0) {
					var doctor = FindViewById<Spinner> (Resource.Id.create_doctor_spinner);	
					ArrayAdapter<string> a = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, doctor_array);
					doctor.Adapter = a;
				}
				if (group_array.Length > 0) {
					var group = FindViewById<Spinner> (Resource.Id.create_group_spinner);	
					ArrayAdapter<string> a = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, group_array);
					group.Adapter = a;
				}
				break;
			case State.Landing_Page:
				SetContentView (Resource.Layout.landing_page);
//				Button kit_kat = FindViewById<Button> (Resource.Id.food_button);
//				kit_kat.Click += Kit_kat_Click;
				Button user_info_btn = FindViewById<Button> (Resource.Id.user_profile_button);
				user_info_btn.Click += User_info_btn_Click;
				Button settings_btn = FindViewById<Button> (Resource.Id.settings_button);
				settings_btn.Click += Settings_btn_Click;
				Button food_btn = FindViewById<Button> (Resource.Id.food_button);
				food_btn.Click += Food_btn_Click;
				Button user_activity_btn = FindViewById<Button> (Resource.Id.b_user_activity);
				user_activity_btn.Click += User_activity_btn_Click;
				break;
			case State.Account_Page:
				SetContentView (Resource.Layout.user_page);
				Button update_user = FindViewById<Button> (Resource.Id.update_user_btn);
				update_user.Click += Update_user_Click;
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
				if (interaction_kit.force_pushing) {
					force_push_btn.SetBackgroundColor (Android.Graphics.Color.Turquoise);
				} else {
					force_push_btn.SetBackgroundColor (Android.Graphics.Color.DarkRed);
				}
				check_battery ();
				break;
			case State.Food_Page:
				SetContentView (Resource.Layout.food_page_temp);
//				Button food_submit_btn = FindViewById<Button> (Resource.Id.save_food_btn);
//				food_submit_btn.Click += Food_submit_btn_Click;
				Button food_temp = FindViewById<Button> (Resource.Id.btn_save_food_temp);
				food_temp.Click += Food_temp_Click;
				break;
			case State.User_Activity_Page:
				SetContentView (Resource.Layout.user_activity_layout);
				interaction_kit.api_request_processed_data ();
				break;
			case State.Log_In:
				SetContentView (Resource.Layout.Main);
				Button login_btn = FindViewById<Button> (Resource.Id.login_button);
				login_btn.Click += Login_button_Click;
				Button create_u_btn = FindViewById<Button> (Resource.Id.Create_User_Button);
				create_u_btn.Click += Create_user_btn_Click;
				break;
			}
		}

		void Interaction_kit_server_update (object o, Response_Type r)
		{
			if (r == Response_Type.login_result) {
				if ((bool)o)
					change_state (ref state, State.Landing_Page);
				else
					notify_user("Failed to Log In");
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

						if(temp.group != null)
							foreach (string _temp in temp.group)
								grps += (_temp + " - ");

						email.Text = temp.email ?? "";
						age.Text = temp.age.ToString () ?? "";
						group.Text = grps ?? "";
						gender.Text = temp.sex ?? "";
						height.Text = temp.height.ToString () ?? "";
						weight.Text = temp.weight.ToString () ?? "";
						first_name.Text = temp.first_name ?? "";
						last_name.Text = temp.last_name ?? "";
						doctor.Text = temp.doctor ?? "";
					});
					notify_user("Successfully Pulled User Data");
				}
				else
					notify_user("Failed to Pull User Information");
			}
			if (r == Response_Type.doctor_list) {
				if (o != null) {
					List<string> doctors = new List<string>();
					foreach (var obj in o as Array) {
						doctors.Add(obj.ToString());
					}
					doctor_array = doctors.ToArray();
				}

			}
			if (r == Response_Type.group_list) {
				if (o != null) {
					List<string> groups = new List<string>();
					foreach (var obj in o as Array) {
						group temp = obj as group;
						groups.Add(temp._id);
					}
					group_array = groups.ToArray();
				}
			}
			if (r == Response_Type.user_created) {
				if ((bool)o)
					change_state (ref state, State.Log_In);
				else
					notify_user("Failed to Create New User");

			}
			if (r == Response_Type.food_sent) {
				if ((bool)o)
					notify_user("Food Saved");
				else
					notify_user("Failed to Save Food");
			}
			if (r == Response_Type.raw_data) {
				if ((bool)o)
					notify_user("Sensor Data Sent Successfully");
				else
					notify_user("Failed to Send Sensor Data");
			}
			else if (r == Response_Type.user_info_updated) {
				if ((bool)o)
					notify_user("User Profile Updated Successfully");
				else
					notify_user("Failed to Update User Profile");
			}
			if (r == Response_Type.processed_data_collection) {
				
				TextView tv = FindViewById<TextView> (Resource.Id.tv_processed_data);
				if (o != null) {
					RunOnUiThread (() => {
						tv.Text = "";
					});
					foreach (var obj in o as Array) {
						Processed_Data pd = obj as Processed_Data;
						//"HH:mm-MM-dd-yyyy"
						string[] temp = pd.created.Split (new string[]{ "-" }, StringSplitOptions.RemoveEmptyEntries);
						int day = Convert.ToInt32 (temp [2]);
					
						int today = DateTime.Now.Day;
						int yesterday = today - 1;
						//DateTime check = new DateTime (Convert.ToInt32 (temp [3]), Convert.ToInt32 (temp [1]), Convert.ToInt32 (temp [2]));
						if (!check_processed_data (pd))// || (day != today && day != yesterday))
							break;
						RunOnUiThread (() => {
							tv.Text +=
								"Timestamp: " + pd.created + Environment.NewLine +
							"Activity: " + pd.activity + Environment.NewLine +
							"Calories Burned: " + pd.calories_burned.ToString () +
							Environment.NewLine + Environment.NewLine;
						});
					}
				}
			}
		}
		private bool check_processed_data(Processed_Data data)
		{
			if (data.activity == null) return false;
			if (data.calories_burned == null) return false;
			if (data.created == null) return false;
			if (data.duration == null) return false;
			if (data.email == null) return false;
			if (data._id == null) return false;

			if (data.activity == "") return false;
			if (data.calories_burned == 0f) return false;
			if (data.created == "") return false;
			if (data.duration == 0) return false;
			if (data.email == "") return false;
			if (data._id == "") return false;

			return true;
		}


	}
}

