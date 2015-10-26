using System;
using Android.Widget;
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

		void Create_cancel_Click (object sender, EventArgs e) { change_state (ref state, State.Initialize); }

		void Create_user_btn_Click (object sender, EventArgs e)	{ change_state (ref state, State.Create_User_Page); }
	}
}

