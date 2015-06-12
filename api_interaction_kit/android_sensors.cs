using System;
using Android;
using Android.Hardware;
using Android.OS;

namespace api_interaction_kit
{
	public class android_sensors : Java.Lang.Object, ISensorEventListener, IDisposable 
	{
		SensorManager sensor_manager;

		public android_sensors ()
		{
			//sensor_manager = (SensorManager)
		}

		public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
		{
			
		}

		public void OnSensorChanged(SensorEvent e)
		{

		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle)
		}
	}
}

