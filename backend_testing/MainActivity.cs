using Android.App;
using Android.Widget;
using Android.OS;
using api_interaction_kit;

namespace backend_testing
{
	[Activity (Label = "backend_testing", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		api a;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			a = new api ();
			a.server_update += A_server_update;
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);


			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate 
			{
				a.api_create_group("avery");
			};
		}

		void A_server_update (object o, Response_Type r)
		{
			if (r == api_interaction_kit.Response_Type.user_info) 
			{
				user_information i = o as user_information;
			}
		}
	}
}


