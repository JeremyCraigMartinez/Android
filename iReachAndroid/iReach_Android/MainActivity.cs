using System;
using Android;
using Android.Widget;
using Android.App;
using Android.Net;
using Android.Content;
using Android.Views;
using Android.OS;
using Android.Hardware;
using api_interaction_kit;
using Mono.Data.Sqlite;

namespace iReach_Android
{
	[Activity (Label = "iReach_Android", MainLauncher = true, Icon = "@drawable/ireach_logo", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public partial class MainActivity : Activity, ISensorEventListener
	{
		private enum State {Initialize, Log_In, Create_User_Page,
			Landing_Page, Account_Page, Settings_Page, Food_Page,
			User_Activity_Page, Exit}

		private State state;
		//private Network_State network_state;

		private api interaction_kit;

		private SensorManager sensor_manager;
		private wifi_watcher network_monitor;
		private battery_watcher battery_monitor;

		private bool initialized;

		private string m_email;
		private string m_pass;
		private string sex_gender;

		private string[] doctor_array;
		private string[] group_array;

		private sensor_data sd;
		private DateTime time;
		private int future_cut_off_time;

		private static readonly object _synclock = new object();

		//private ConnectivityManager connectivity_manager;

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
			interaction_kit.initialize();
			interaction_kit.server_update += Interaction_kit_server_update;
			interaction_kit.announcment += Interaction_kit_announcment;
			interaction_kit.api_get_doctor_list();
			interaction_kit.api_get_group_list();

			network_monitor = new wifi_watcher();
			network_monitor.Connection_Status_Changed += Network_monitor_Connection_Status_Changed;
			RegisterReceiver(network_monitor, new IntentFilter(ConnectivityManager.ConnectivityAction));

			battery_monitor = new battery_watcher();
			battery_monitor.Power_Status_Changed += Battery_monitor_Power_Status_Changed;
			RegisterReceiver(battery_monitor, new IntentFilter(Intent.ActionBatteryChanged));

			sensor_manager = (SensorManager)GetSystemService (Context.SensorService);
			sd = new sensor_data ();
			time = DateTime.Now;
			future_cut_off_time = 0;

			turn_on_sensors ();

			SetContentView (Resource.Layout.Main);
		}

		void Battery_monitor_Power_Status_Changed (Power_State power_state)
		{
			interaction_kit.power_state = power_state;
		}

		void Network_monitor_Connection_Status_Changed (Network_State network_state)
		{
			interaction_kit.network_state = network_state;
		}

		void Interaction_kit_announcment (Announcement_Type input)
		{
			if (input == Announcement_Type.Running)
				initialized = true;
			else if (input == Announcement_Type.SMError)
			{
				notify_user("Error In Interaction Kit State Machine");
				initialized = false;
				change_state(ref state, State.Log_In);
			}
		}


		public override void OnBackPressed()
		{
			if(state == State.Landing_Page || state == State.Create_User_Page)
			{
				interaction_kit.logout();
				change_state(ref state, State.Log_In);
			}
			else if(state == State.Log_In)
				base.OnBackPressed();
			else
				change_state(ref state, State.Landing_Page);
		}

		private void notify_user(string input)
		{
			RunOnUiThread(() => {
				Toast.MakeText(this, input, ToastLength.Short).Show();
			});
		}
	}
}