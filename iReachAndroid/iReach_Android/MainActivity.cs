using System;
using System.Text;
using Android.App;
using Android.Net;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using api_interaction_kit;

namespace iReach_Android
{
	[Activity (Label = "iReach_Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ISensorEventListener
	{
		private enum State {Initialize, Log_In, Create_User_Page, Landing_Page, Account_Page, Exit}
		private State state;

		private api_interaction_kit.api interaction_kit;

		private SensorManager sensor_manager;

		private bool initialized;

		private string m_email;
		private string m_pass;
		private string sex_gender;

		private static readonly object _synclock = new object();

		public ConnectivityManager connectivity_manager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature(WindowFeatures.NoTitle);
			change_state (ref state, State.Initialize);
			initialized = false;
		}

		public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
		{
			
		}

		public void OnSensorChanged(SensorEvent e)
		{
			if (initialized) {
				lock (_synclock) {
					
					string text = "";
					text += e.Values [0] + ", ";
					text += e.Values [1] + ", ";
					text += e.Values [2];
					//for (int i = 0; i < e.Values.Count; i++)
								//	interaction_kit.api_upload_raw_data (DateTime.Now.ToString("HH:mm-MM-dd-yyyy"), e.Values [i].ToString ());
					interaction_kit.api_upload_raw_data (DateTime.Now.ToString("HH:mm-MM-dd-yyyy"), text);
				}
			}
		}

		protected override void OnResume()
		{
			base.OnResume();
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Ui);
		}

		protected override void OnPause()
		{
			base.OnPause();
			sensor_manager.UnregisterListener(this);
		}

		private void change_state(ref State state, State new_state)
		{
			switch (state) {
			case State.Initialize:
				break;
			case State.Create_User_Page:
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

		void Create_submit_btn_Click (object sender, EventArgs e)
		{
			RunOnUiThread (() => {
				create_user_information temp = new create_user_information();

				var email = FindViewById<TextView> (Resource.Id.create_email);
				var age = FindViewById<TextView> (Resource.Id.create_age);
				//var group = FindViewById<Spinner> (Resource.Id.create_group_spinner);
				//var gender = FindViewById<RadioGroup> (Resource.Id.create_gender_radio);
				var male = FindViewById<RadioButton> (Resource.Id.radio_male_btn);
				var female = FindViewById<RadioButton> (Resource.Id.female_radio_btn);
				var height = FindViewById<TextView> (Resource.Id.create_height);
				var weight = FindViewById<TextView> (Resource.Id.create_weight);
				var first_name = FindViewById<TextView> (Resource.Id.create_firstn);
				var last_name = FindViewById<TextView> (Resource.Id.create_lastn);
				//var doctor = FindViewById<Spinner> (Resource.Id.create_doctor_spinner);
				var pass = FindViewById<TextView> (Resource.Id.create_password);

				sex_gender = "male";

				if(male.Selected)
					sex_gender = "male";
				if(female.Selected)
					sex_gender = "female";

				temp.sex = sex_gender;
				temp.email = email.Text;
				temp.age = Convert.ToInt32(age.Text);
				temp.pass = pass.Text;
				temp.height = Convert.ToInt32(height.Text);
				temp.weight = Convert.ToInt32(weight.Text);
				temp.first_name = first_name.Text;
				temp.last_name = last_name.Text;
				temp.doctor = "house@md.com";
				string[] s = {"type ii diabetes"};
				temp.group = s;

				m_email = temp.email;
				m_pass =  temp.pass;

				interaction_kit._create_user(temp);

			});
		}

		void Create_cancel_Click (object sender, EventArgs e)
		{
			change_state (ref state, State.Initialize);
		}

		void Create_user_btn_Click (object sender, EventArgs e)
		{
			change_state (ref state, State.Create_User_Page);
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
			connectivity_manager = (ConnectivityManager)GetSystemService (ConnectivityService);
			interaction_kit.initialize(ref connectivity_manager);
			interaction_kit.server_update += Interaction_kit_server_update;
			interaction_kit.announcment += Interaction_kit_announcment;

			sensor_manager = (SensorManager)GetSystemService (Context.SensorService);


			SetContentView (Resource.Layout.Main);
		}

		void Interaction_kit_announcment (Announcement_Type input)
		{
			if (input == Announcement_Type.Running)
				initialized = true;
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

						foreach(string _temp in temp.group)
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