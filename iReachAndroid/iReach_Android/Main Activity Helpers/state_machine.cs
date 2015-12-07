using System;
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
				Button food_submit_btn = FindViewById<Button> (Resource.Id.save_food_btn);
				food_submit_btn.Click -= Food_submit_btn_Click;
				break;
			case State.User_Activity_Page:
				break;
			case State.Log_In:
				Button login_btn = FindViewById<Button> (Resource.Id.login_button);
				login_btn.Click -= Login_button_Click;
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
				Button food_btn = FindViewById<Button> (Resource.Id.food_button);
				food_btn.Click += Food_btn_Click;
				Button user_activity_btn = FindViewById<Button> (Resource.Id.b_user_activity);
				user_activity_btn.Click += User_activity_btn_Click;
				break;
			case State.Account_Page:
				SetContentView (Resource.Layout.user_page);
				Button update_user = FindViewById<Button>(Resource.Id.update_user_btn);
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
				SetContentView(Resource.Layout.food_page);
				Button food_submit_btn = FindViewById<Button> (Resource.Id.save_food_btn);
				food_submit_btn.Click += Food_submit_btn_Click;
				break;
			case State.User_Activity_Page:
				SetContentView (Resource.Layout.user_activity_layout);
				interaction_kit.api_request_processed_data();
				break;
			case State.Log_In:
				SetContentView(Resource.Layout.Main);
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
			if (r == Response_Type.processed_data_collection) {
				
				TextView tv = FindViewById<TextView> (Resource.Id.tv_processed_data);
				RunOnUiThread (() => {tv.Text = "";});
				foreach (var obj in o as Array) {
					Processed_Data pd = obj as Processed_Data;
					//"HH:mm-MM-dd-yyyy"
					string[] temp = pd.created.Split (new string[]{"-"}, StringSplitOptions.RemoveEmptyEntries);
					int day = Convert.ToInt32 (temp [2]);

					int today = DateTime.Now.Day;
					int yesterday = today - 1;
					//DateTime check = new DateTime (Convert.ToInt32 (temp [3]), Convert.ToInt32 (temp [1]), Convert.ToInt32 (temp [2]));
					if (day != today && day != yesterday)
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
}

