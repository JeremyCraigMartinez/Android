using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using iReachAndroid.Fragments;

namespace iReachAndroid.Activities
{
	[Activity (Label = "iReachAndroid", MainLauncher = true, Icon = "@drawable/icon")]
	public class AppStart : BaseActivity
	{
		private Button mLoginButton;
		private Button mSignupButton;
		private Button mSkipButton;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			mLoginButton = FindViewById<Button> (Resource.Id.btnLogininWithEmail);
			mSignupButton = FindViewById<Button> (Resource.Id.btnSignupWithEmail);
			mSkipButton = FindViewById<Button> (Resource.Id.btnSkipToApp);
	 
			mLoginButton.Click += LoginButton_Click;
			mSignupButton.Click += SignupButton_Click;
			mSkipButton.Click += SkipButton_Click;

		}

		void SkipButton_Click (object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(MainActivity));
			intent.PutExtra ("ProfileType", "Guest");

			StartActivity (intent);
		}

		void SignupButton_Click (object sender, EventArgs e)
		{
			var dialog = new SignupDialogFragment ();
			dialog.Show (SupportFragmentManager, "signup");

		}

		void LoginButton_Click (object sender, EventArgs e)
		{
			var dialog = new SigninDialopFragment ();
			dialog.Show (SupportFragmentManager, "login");
			
		}

		#region implemented abstract members of BaseActivity

		protected override int LayoutResource {
			get {
				return Resource.Layout.app_start;
			}
		}

		#endregion
	}
}


