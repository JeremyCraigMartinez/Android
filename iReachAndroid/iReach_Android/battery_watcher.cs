using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using api_interaction_kit;

namespace iReach_Android
{
	[BroadcastReceiver]
	public class battery_watcher : BroadcastReceiver
	{
		#region Variables

		public Power_State state { get; private set; }

		public float charge 	{ get; private set; } // percentage of the battery

		public delegate void POWER_CHANGE(Power_State power_state);
		public event POWER_CHANGE Power_Status_Changed;

		#endregion

		public battery_watcher() 
		{ 
			charge = 0f;
			state = Power_State.UNKOWN;
		}

		public override void OnReceive (Context context, Intent intent)
		{

			int level = intent.GetIntExtra(BatteryManager.ExtraLevel, -1);
			int scale = intent.GetIntExtra(BatteryManager.ExtraScale, -1);
			charge = ((level / (float)scale) * 100f); 

			int status = intent.GetIntExtra(BatteryManager.ExtraStatus, -1);
			bool charging = (status == (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full);

			int plugged_in_status = intent.GetIntExtra(BatteryManager.ExtraPlugged, -1);
			bool plugged_in = (plugged_in_status == (int)BatteryPlugged.Usb || plugged_in_status == (int)BatteryPlugged.Ac || plugged_in_status == (int)BatteryPlugged.Wireless);

			if(charging || plugged_in){
				if(state != Power_State.CHARGING){
					state = Power_State.CHARGING;
					Power_Status_Changed(state);
				}
			}
			else{
				if(state != Power_State.DEPLETING){
					state = Power_State.DEPLETING;
					Power_Status_Changed(state);
				}
			}
			
		}
	}
}

