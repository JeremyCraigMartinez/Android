using System;
using Android;
using Android.App;
using Android.Net;
using Android.Content;
using Android.Views;
using Android.OS;
using Android.Hardware;
using api_interaction_kit;

using api_interaction_kit;

namespace iReach_Android
{
	[BroadcastReceiver()]			
	public class wifi_watcher : BroadcastReceiver
	{
		public Network_State state { get; private set; }

		public delegate void CONNECTION_STATUS_CHANGED(Network_State network_state);
		public event CONNECTION_STATUS_CHANGED Connection_Status_Changed;

		private ConnectivityManager connectivity_manager;

		public wifi_watcher() { state = Network_State.UNKOWN; }

		public override void OnReceive (Context context, Intent intent)
		{
//			if(Connection_Status_Changed != null)
//				Connection_Status_Changed(this, EventArgs.Empty);
			connectivity_manager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);

			var network_info = connectivity_manager.ActiveNetworkInfo;

			if(network_info.IsConnectedOrConnecting){
				if(network_info.Type == ConnectivityType.Wifi && state != Network_State.WIFI){
					state = Network_State.WIFI;
					Connection_Status_Changed(state);
				}
				else if(state != Network_State.DATA){
					state = Network_State.DATA;
					Connection_Status_Changed(state);
				}
			}
			else{
				if(state != Network_State.OFFLINE)
				{
					state = Network_State.OFFLINE;
					Connection_Status_Changed(state);
				}
			}

		}
	}
}

