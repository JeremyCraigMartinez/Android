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
		void Food_temp_Click (object sender, EventArgs e)
		{
			RadioGroup r = FindViewById<RadioGroup> (Resource.Id.food_choices);
			//RadioButton b = FindViewById<RadioButton> (r.CheckedRadioButtonId);
			int i = r.IndexOfChild (FindViewById (r.CheckedRadioButtonId));
			string food = "";
			switch (i) {
			case 1: food = "08659";
				break;
			case 2: food = "32032";
				break;
			case 3: food = "01132";
				break;
			case 4: food = "18390";
				break;
			case 7: food = "21089";
				break;
			case 8: food = "21155";
				break;
			case 9: food = "21378";
				break;
			case 12: food = "22910";
				break;
			case 13: food = "22972";
				break;
			case 14: food = "22978";
				break;
			case 15: food = "11584";
				break;
			}

			interaction_kit.api_food_upload (food, 1f);
		}
	}
}