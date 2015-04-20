using Android.App;
using Android.Widget;
using Android.OS;
using api_interaction_kit;

namespace backend_testing
{
	[Activity (Label = "backend_testing", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			api a = new api ();
			a.announcment += A_announcment;
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);


			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
			};
		}

		void A_announcment (string input)
		{
			
		}
	}
}


