﻿using Android.Runtime;
using Android.App;
using Android.Net;
using Android.Content;
using Android.OS;

namespace api_interaction_kit
{
	public class hardware_inspector
	{
		public bool wifi;

		public hardware_inspector()
		{
			wifi = false;
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

