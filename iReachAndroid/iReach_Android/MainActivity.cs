using System;
using Android.App;
using Android.Net;
using Android.Content;
using Android.Views;
using Android.OS;
using Android.Hardware;
using api_interaction_kit;

namespace iReach_Android
{
	[Activity (Label = "iReach_Android", MainLauncher = true, Icon = "@drawable/ireach_logo")]
	public partial class MainActivity : Activity, ISensorEventListener
	{
		private enum State {Initialize, Log_In, Create_User_Page, Landing_Page, Account_Page, Settings_Page, Exit}
		private State state;

		private api interaction_kit;

		private SensorManager sensor_manager;

		private bool initialized;

		private string m_email;
		private string m_pass;
		private string sex_gender;
		private sensor_data sd;
		private DateTime time;

		private static readonly object _synclock = new object();

		public ConnectivityManager connectivity_manager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature(WindowFeatures.NoTitle);
			initialized = false;
			change_state (ref state, State.Initialize);
		}

		void initialize()
		{
			interaction_kit = new api ();
			connectivity_manager = (ConnectivityManager)GetSystemService (ConnectivityService);
			interaction_kit.initialize();
			interaction_kit.server_update += Interaction_kit_server_update;
			interaction_kit.announcment += Interaction_kit_announcment;

			sensor_manager = (SensorManager)GetSystemService (Context.SensorService);
			sd = new sensor_data ();
			time = DateTime.Now;

			SetContentView (Resource.Layout.Main);
		}

		void Interaction_kit_announcment (Announcement_Type input)
		{
			if (input == Announcement_Type.Running)
				initialized = true;
		}

	}
}