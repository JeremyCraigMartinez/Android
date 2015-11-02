using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using api_interaction_kit;

namespace api_interaction_kit_testing_suite
{
	[Activity (Label = "api_interaction_kit_testing_suite", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		api interaction_kit;
		TextView output;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			output = FindViewById<TextView> (Resource.Id.et_output);
			output.Text = "";
			initialize ();
			Task t = new Task (() => {
				run ();
			});
			t.Start ();
		}

		private void run()
		{
			interaction_kit.login ("test@test.com", "test");
			interaction_kit.api_request_user_data ();
			test_raw_data();
		}

		private void initialize()
		{
			interaction_kit = new api ();
			interaction_kit.initialize ();
			interaction_kit.server_update += Interaction_kit_server_update;
		}

		private void post(string input) { RunOnUiThread(()=> { output.Text += input + "\n";}); }

		void Interaction_kit_server_update (object o, Response_Type r)
		{
			if (r == Response_Type.login_result) {
				if ((bool)o)
					post("Login Successful");
				else
					post("Login Failed");
			}
			else if (r == Response_Type.user_info) {
				user_information user = (user_information)o;
				if (user.first_name != "test") {post ("First Name Incorrect"); return;}
				if (user.last_name != "test") {post ("Last Name Incorrect"); return;}
				if (user.age != 5) {post ("Age Incorrect"); return;}
				if (user.weight != 5) {post ("Weight Incorrect"); return;}
				if (user.height != 5) {post ("Height Incorrect"); return;}
				post ("User Information Correct");
			}
			else if(r == Response_Type.raw_data)
			{
				if ((bool)o)
					post("Posting Raw Sensor Data Was Successful");
				else
					post("Posting Raw Sensor Data Failed");
			}


		}

		private void test_raw_data()
		{
			float[] _x = {3.5f, 0.225f, 0.5f};
			float[] _y = {3.5f, 0.225f, 0.5f};
			float[] _z = {3.5f, 0.225f, 0.5f};
			raw_data temp = new raw_data (new _a() {x=_x, y=_y, z=_z},
				new _g() {x=_x, y=_y, z=_z},
				new _m(){x=_x, y=_y, z=_z});
			temp.created = DateTime.Now.ToString("HH:mm-MM-dd-yyyy");
			interaction_kit.api_upload_raw_data (temp);
			post("sending sudo raw data");
			interaction_kit.force_pushing = true;
		}
	}
}


