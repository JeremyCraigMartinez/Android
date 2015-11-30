using System;

namespace iReach_Android
{
	public partial class MainActivity
	{
		void User_info_btn_Click (object sender, EventArgs e)
		{
			interaction_kit.api_request_user_data ();
			change_state (ref state, State.Account_Page);
		}

		void Settings_btn_Click (object sender, EventArgs e)
		{
			change_state (ref state, State.Settings_Page);
		}

		void Food_btn_Click (object sender, EventArgs e)
		{
			change_state(ref state, State.Food_Page);
		}
		void User_activity_btn_Click (object sender, EventArgs e)
		{
			change_state (ref state, State.User_Activity_Page);
		}
	}
}

