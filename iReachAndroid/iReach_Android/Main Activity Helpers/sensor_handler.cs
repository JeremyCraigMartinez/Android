using System;
using System.Threading.Tasks;
using Android.Hardware;
using Android.Widget;
using api_interaction_kit;

namespace iReach_Android
{
	public partial class MainActivity
	{
		private bool sensors_on = false;
		private float old_charge = 0;

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
						check_battery ();
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
		}

		protected override void OnPause()
		{
			base.OnPause();
		}

		protected void turn_on_sensors() 
		{
			if (!sensors_on) {
				sensor_manager.RegisterListener (this, sensor_manager.GetDefaultSensor (SensorType.Accelerometer), SensorDelay.Game); // ~50Hz
				sensor_manager.RegisterListener (this, sensor_manager.GetDefaultSensor (SensorType.Gyroscope), SensorDelay.Game);
				sensor_manager.RegisterListener (this, sensor_manager.GetDefaultSensor (SensorType.MagneticField), SensorDelay.Game);
				sensors_on = true;
			}
		}
		protected void turn_off_sensors() { sensor_manager.UnregisterListener(this); sensors_on = false; }

		protected void check_battery()
		{
			float rate_of_decay = old_charge - battery_monitor.charge;
			if(rate_of_decay > 6f) turn_off_sensors(); else turn_on_sensors();

			old_charge = battery_monitor.charge > 0f ? battery_monitor.charge : 0f;

			if (state == State.Settings_Page) {
				TextView text_view = FindViewById<TextView> (Resource.Id.tb_battery_decay);
				RunOnUiThread (() => {
					text_view.Text = System.String.Format("Battery Decay Rate: {0}", 
						(rate_of_decay / 100f));
				});
			}
		}
	}
}

