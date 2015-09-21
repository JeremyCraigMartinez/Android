using Android;
using Android.Net;
using Android.Content;
using Android.OS;

namespace api_interaction_kit
{
	public static class hardware_inspector
	{
//		public static bool wifi(Context c)
//		{
//			using (ConnectivityManager m = (ConnectivityManager)c.GetSystemService(Context.ConnectivityService)) 
//			{
//				if (m.GetNetworkInfo (ConnectivityType.Wifi).GetState () == NetworkInfo.State.Connected)
//					return true;
//			}
//			return false;
//		}
//
//		public static bool plugged_in(Context c)
//		{
//			return false;
//		}
		public static bool connected_to_wifi()
		{
			var connectivity_manager = (ConnectivityManager)Context.GetSystemService(Android.App.Activity.ConnectivityService);
			var mobile_state = connectivity_manager.GetNetworkInfo(ConnectivityType.Mobile).GetState();
			return(mobile_state == NetworkInfo.State.Connected);
		}
	}
	public class Battery : BroadcastReceiver
	{
		#region Variables

		public bool charging { get; private set; }
		public int charge 	{ 	
								get	
								{ 
									int level = battery_status.GetIntExtra(BatteryManager.ExtraLevel, -1);
									int scale = battery_status.GetIntExtra(BatteryManager.ExtraScale, -1);
									return((int)((level / (float)scale) * 100)); 
								} 
							}

		private Intent battery_status;

		#endregion

		public Battery(Context c)
		{
			IntentFilter ifilter = new IntentFilter (Intent.ActionBatteryChanged);
			battery_status = c.RegisterReceiver (null, ifilter);
		}

		public override void OnReceive (Context context, Intent intent)
		{
			int status = intent.GetIntExtra( BatteryManager.ExtraStatus, -1);
			charging = (status == (int)BatteryStatus.Charging ||
				status == (int)BatteryStatus.Full);
		}
	}
}

