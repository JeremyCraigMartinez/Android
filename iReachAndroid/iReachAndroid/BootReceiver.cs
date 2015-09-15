using Android;
using Android.Content;

//Reference: http://javatechig.com/android/how-to-start-an-application-at-device-bootup-in-android

namespace iReachAndroid.Activities
{
	public class BootReceiver : BroadcastReceiver
	{
		public override void OnReceive (Context context, Intent intent)
		{
			Intent i = new Intent (context, typeof(MainActivity));  
			i.AddFlags (ActivityFlags.NewTask);
			context.StartActivity (i);  
		}
	}
}