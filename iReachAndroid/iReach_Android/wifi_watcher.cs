
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

namespace iReach_Android
{
	[Activity (Label = "wifi_watcher")]			
	public class wifi_watcher : BroadcastReceiver
	{
		public override void OnReceive (Context context, Intent intent)
		{
			Intent i = new Intent (context, typeof(MainActivity));  
			  
		}
	}
}

