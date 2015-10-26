using System;
using Android.Widget;

namespace iReach_Android
{
	public partial class MainActivity
	{
		void Hertz_spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;
		}
		void Settings_back_btn_Click (object sender, EventArgs e)
		{
			change_state (ref state, State.Landing_Page);
		}

		void Force_push_btn_Click (object sender, EventArgs e)
		{
			Button force_push_btn = FindViewById<Button> (Resource.Id.force_push_button);
			if (!interaction_kit.force_pushing) 
			{ 
				force_push_btn.SetBackgroundColor(Android.Graphics.Color.Turquoise);
				interaction_kit.force_pushing = true; 
			} 
			else 
			{ 
				force_push_btn.SetBackgroundColor(Android.Graphics.Color.DarkRed);
				interaction_kit.force_pushing = false;
			}
		}
	}
}

