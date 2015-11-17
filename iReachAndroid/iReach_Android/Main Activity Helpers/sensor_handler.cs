using System;
using System.Threading.Tasks;
using Android.Hardware;
using api_interaction_kit;

namespace iReach_Android
{
	public partial class MainActivity
	{
		public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
		{

		}

		public void OnSensorChanged (SensorEvent e)
		{
			if (initialized) {
				lock (_synclock) {
					if (future_cut_off_time == DateTime.Now.Second) {
						Task t = new Task (() => upload_raw_data (sd, time));
						t.Start ();
						sd = new sensor_data ();
						time = DateTime.Now;
						future_cut_off_time = (DateTime.Now.Second + 2) % 60;
					}
					if (e.Sensor.Type == SensorType.Accelerometer) {
						sd.accel.x.Add (e.Values [0]);
						sd.accel.y.Add (e.Values [1]);
						sd.accel.z.Add (e.Values [2]);
					}
					if (e.Sensor.Type == SensorType.Gyroscope) {
						sd.gyro.x.Add (e.Values [0]);
						sd.gyro.y.Add (e.Values [1]);
						sd.gyro.z.Add (e.Values [2]);
					}
					if (e.Sensor.Type == SensorType.MagneticField) {
						sd.mag.x.Add (e.Values [0]);
						sd.mag.y.Add (e.Values [1]);
						sd.mag.z.Add (e.Values [2]);
					}
				}
			}
		}

		protected void upload_raw_data(sensor_data input, DateTime time)
		{
			try 
			{
				raw_data data = new raw_data (new _a () {
					x = input.accel.x.ToArray (),
					y = input.accel.y.ToArray (),
					z = input.accel.z.ToArray ()
				},
					new _g () {
						x = input.gyro.x.ToArray (),
						y = input.gyro.y.ToArray (),
						z = input.gyro.z.ToArray ()
					},
					new _m () {
						x = input.mag.x.ToArray (),
						y = input.mag.y.ToArray (),
						z = input.mag.z.ToArray ()
					}
				);

				data.created = time.ToString ("HH:mm-MM-dd-yyyy");
				if((data.data.accelerometer.x.Length > 0 && data.data.accelerometer.y.Length > 0 && data.data.accelerometer.z.Length > 0) ||
					(data.data.gyroscope.x.Length > 0 && data.data.gyroscope.y.Length > 0 && data.data.gyroscope.z.Length > 0) ||
					(data.data.magnetometer.x.Length > 0 && data.data.magnetometer.y.Length > 0 && data.data.magnetometer.z.Length > 0))
				{
					interaction_kit.api_upload_raw_data (data);
				}
			} 
			catch (Exception ex) 
			{
				string _e_ = ex.ToString();
			}
		}

		protected override void OnResume()
		{
			base.OnResume();
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Game); // ~100Hz
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.Gyroscope), SensorDelay.Game);
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.MagneticField), SensorDelay.Game);
		}

		protected override void OnPause()
		{
			base.OnPause();
			sensor_manager.UnregisterListener(this);
		}
	}
}

