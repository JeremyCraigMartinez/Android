using System;
using Android.Widget;

namespace iReach_Android
{		
	public partial class MainActivity
	{
		void Food_submit_btn_Click (object sender, EventArgs e)
		{
			MultiAutoCompleteTextView food = FindViewById<MultiAutoCompleteTextView> (Resource.Id.auto_complete_food);
			EditText portion = FindViewById<EditText> (Resource.Id.serving_size_et);

			interaction_kit.api_food_upload("Pull from db?", (float)Convert.ToDouble(portion.Text));
		}
	}
}