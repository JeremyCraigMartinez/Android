using System;
using Android.Hardware;
using api_interaction_kit;

namespace iReach_Android
{
	public partial class MainActivity
	{
		public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
		{

		}

		public void OnSensorChanged(SensorEvent e)
		{
			if (initialized) {
				lock (_synclock) {
					if(time.Second == DateTime.Now.Second)
					{
						if (e.Sensor.Type == SensorType.Accelerometer) {
							sd.accel.x.Add (e.Values [0].ToString());
							sd.accel.y.Add (e.Values [1].ToString());
							sd.accel.z.Add (e.Values [2].ToString());
						}
						if (e.Sensor.Type == SensorType.Gyroscope) {
							sd.gyro.x.Add (e.Values [0].ToString());
							sd.gyro.y.Add (e.Values [1].ToString());
							sd.gyro.z.Add (e.Values [2].ToString());
						}
						if (e.Sensor.Type == SensorType.MagneticField) {
							sd.mag.x.Add (e.Values [0].ToString());
							sd.mag.y.Add (e.Values [1].ToString());
							sd.mag.z.Add (e.Values [2].ToString());
						}
					}
					else
					{
						try {
							raw_data data = new raw_data (new _a () {x = sd.accel.x.ToArray (),
								y = sd.accel.y.ToArray (),
								z = sd.accel.z.ToArray ()
							},
								new _g () {x = sd.gyro.x.ToArray (),
									y = sd.gyro.y.ToArray (),
									z = sd.gyro.z.ToArray ()
								},
								new _m () {x = sd.mag.x.ToArray (),
									y = sd.mag.y.ToArray (),
									z = sd.mag.z.ToArray ()
								}
							);

							data.created = time.ToString ("HH:mm-MM-dd-yyyy");
							sd = new sensor_data ();
							time = DateTime.Now;
							interaction_kit.api_upload_raw_data (data);
						} catch (Exception ex) {
							string _e_ = ex.ToString();
						}
					}
				}
			}
		}

		protected override void OnResume()
		{
			base.OnResume();
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Fastest); // ~50Hz
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.Gyroscope), SensorDelay.Fastest);
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.MagneticField), SensorDelay.Fastest);
		}

		protected override void OnPause()
		{
			base.OnPause();
			sensor_manager.UnregisterListener(this);
		}
	}
}

