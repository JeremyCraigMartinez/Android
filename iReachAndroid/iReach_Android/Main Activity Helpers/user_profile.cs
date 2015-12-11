using System;
using Android;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.App;
using Android.Graphics;
using api_interaction_kit;

namespace iReach_Android
{
	public partial class MainActivity
	{
		void Create_submit_btn_Click (object sender, EventArgs e)
		{
			RunOnUiThread (() => {
				create_user_information temp = new create_user_information();

				var email = FindViewById<TextView> (Resource.Id.create_email);
				var age = FindViewById<TextView> (Resource.Id.create_age);
				var doctor = FindViewById<Spinner> (Resource.Id.create_doctor_spinner);
				var group = FindViewById<Spinner> (Resource.Id.create_group_spinner);
				var gender = FindViewById<RadioGroup> (Resource.Id.create_gender_radio);
				var height = FindViewById<TextView> (Resource.Id.create_height);
				var weight = FindViewById<TextView> (Resource.Id.create_weight);
				var first_name = FindViewById<TextView> (Resource.Id.create_firstn);
				var last_name = FindViewById<TextView> (Resource.Id.create_lastn);

				var pass = FindViewById<TextView> (Resource.Id.create_password);

				int i = gender.IndexOfChild (FindViewById (gender.CheckedRadioButtonId));
				if(i == 0) sex_gender = "Male"; else sex_gender = "Female";

				temp.sex = sex_gender;
				temp.email = email.Text;
				temp.age = Convert.ToInt32(age.Text);
				temp.pass = pass.Text;
				temp.height = Convert.ToInt32(height.Text);
				temp.weight = Convert.ToInt32(weight.Text);
				temp.first_name = first_name.Text;
				temp.last_name = last_name.Text;
				temp.doctor = doctor.SelectedItem.ToString();
				temp.group = new string[]{group.SelectedItem.ToString()};

				m_email = temp.email;
				m_pass =  temp.pass;

				interaction_kit._create_user(temp);

			});
		}
			

		void Create_user_btn_Click (object sender, EventArgs e)	{ change_state (ref state, State.Create_User_Page); }

		void Update_user_Click (object sender, EventArgs e)	
		{
			user_information user_update = new user_information();
			var email = FindViewById<TextView> (Resource.Id.email);
			var age = FindViewById<TextView> (Resource.Id.age);
			var group = FindViewById<TextView> (Resource.Id.group);
			var gender = FindViewById<TextView> (Resource.Id.gender);
			var height = FindViewById<TextView> (Resource.Id.height);
			var weight = FindViewById<TextView> (Resource.Id.weight);
			var first_name = FindViewById<TextView> (Resource.Id.first_name);
			var last_name = FindViewById<TextView> (Resource.Id.last_name);
			var doctor = FindViewById<TextView> (Resource.Id.doctor);

			user_update.email = email.Text;
			user_update.age = Convert.ToInt32(age.Text);
			//user_update.group = group.Text;
			user_update.sex = gender.Text;
			user_update.height = Convert.ToInt32(height.Text);
			user_update.weight = Convert.ToInt32(weight.Text);
			user_update.first_name = first_name.Text;
			user_update.last_name = last_name.Text;
			user_update.doctor = doctor.Text;

			interaction_kit.api_update_user_info(user_update);

		}

	}
}

