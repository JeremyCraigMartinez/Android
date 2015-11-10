using System;
using System.Threading.Tasks;
using Android.Hardware;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using api_interaction_kit;

namespace api_interaction_kit_testing_suite
{
	[Activity (Label = "api_interaction_kit_testing_suite", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ISensorEventListener
	{
		api interaction_kit;
		TextView output;

		DateTime _time;
		sensor_data _data;
		bool send_raw_data;
		private static readonly object _synclock = new object ();
		int future_cut_off_time;

		SensorManager sensor_manager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			output = FindViewById<TextView> (Resource.Id.et_output);
			output.Text = "";
			initialize ();
			Task t = new Task (() => {
				run ();
			});
			t.Start ();
		}

		private void run ()
		{
			interaction_kit.login ("test@test.com", "test");
			interaction_kit.api_request_user_data ();
			test_raw_data ();
			post ("Activating Full Sensor Data Gathering");
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Game); // ~50Hz
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.Gyroscope), SensorDelay.Game);
			sensor_manager.RegisterListener(this, sensor_manager.GetDefaultSensor(SensorType.MagneticField), SensorDelay.Game);
			send_raw_data = true;
		}

		private void initialize ()
		{
			interaction_kit = new api ();
			interaction_kit.initialize ();
			interaction_kit.server_update += Interaction_kit_server_update;

			_data = new sensor_data ();
			send_raw_data = false;
			sensor_manager = (SensorManager)GetSystemService (Context.SensorService);
		}

		private void post (string input)
		{
			RunOnUiThread (() => {
				output.Text += input + "\n";
			});
		}

		private void clear_output() { RunOnUiThread (() => { output.Text = ""; }); }
		private void set_output (bool success)
		{
			if(success)
			RunOnUiThread (() => {
					output.SetBackgroundColor(Android.Graphics.Color.Green);
			});
			else
				RunOnUiThread (() => {
					output.SetBackgroundColor(Android.Graphics.Color.Crimson);
				});
		}

		void Interaction_kit_server_update (object o, Response_Type r)
		{
			if (r == Response_Type.login_result) {
				if ((bool)o)
					post ("Login Successful");
				else
					post ("Login Failed");
			} else if (r == Response_Type.user_info) {
				user_information user = (user_information)o;
				if (user.first_name != "test") {
					post ("First Name Incorrect");
					return;
				}
				if (user.last_name != "test") {
					post ("Last Name Incorrect");
					return;
				}
				if (user.age != 5) {
					post ("Age Incorrect");
					return;
				}
				if (user.weight != 5) {
					post ("Weight Incorrect");
					return;
				}
				if (user.height != 5) {
					post ("Height Incorrect");
					return;
				}
				post ("User Information Correct");
			} else if (r == Response_Type.raw_data) {
				if ((bool)o) {
					set_output (true);
					post ("Posting Raw Sensor Data Was Successful");
				} else {
					set_output (false);
					post ("Posting Raw Sensor Data Failed");
				}
			}


		}

		private void test_raw_data ()
		{
			float[] _x = { 3.5f, 0.225f, 0.5f };
			float[] _y = { 3.5f, 0.225f, 0.5f };
			float[] _z = { 3.5f, 0.225f, 0.5f };
			raw_data temp = new raw_data (new _a () { x = _x, y = _y, z = _z },
				                new _g () { x = _x, y = _y, z = _z },
				                new _m (){ x = _x, y = _y, z = _z });
			temp.created = DateTime.Now.ToString ("HH:mm-MM-dd-yyyy");
			interaction_kit.api_upload_raw_data (temp);
			post ("sending sudo raw data");
			interaction_kit.force_pushing = true;
		}

		public void OnAccuracyChanged (Sensor sensor, SensorStatus accuracy)
		{

		}

		public void OnSensorChanged (SensorEvent e)
		{
			if (send_raw_data) {
				lock (_synclock) {
					if (future_cut_off_time == DateTime.Now.Second) {
						//Task.Factory.StartNew (() => upload_raw_data (sd));
						Task t = new Task (() => upload_raw_data (_data, _time));
						t.Start ();
						_data = new sensor_data ();
						_time = DateTime.Now;
						future_cut_off_time = (DateTime.Now.Second + 2) % 60;
					}
					if (e.Sensor.Type == SensorType.Accelerometer) {
						_data.accel.x.Add (e.Values [0]);
						_data.accel.y.Add (e.Values [1]);
						_data.accel.z.Add (e.Values [2]);
					}
					if (e.Sensor.Type == SensorType.Gyroscope) {
						_data.gyro.x.Add (e.Values [0]);
						_data.gyro.y.Add (e.Values [1]);
						_data.gyro.z.Add (e.Values [2]);
					}
					if (e.Sensor.Type == SensorType.MagneticField) {
						_data.mag.x.Add (e.Values [0]);
						_data.mag.y.Add (e.Values [1]);
						_data.mag.z.Add (e.Values [2]);
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
					if(test_json(data))
						interaction_kit.api_upload_raw_data (data);
				}
			} 
			catch (Exception ex) 
			{
				string _e_ = ex.ToString();
			}
		}

		protected bool test_json(raw_data data)
		{
			clear_output ();
			post ("Testing Json");
			data.email = "test@test.com";
			string json = json_functions.serializer (data);
			if (!json.Contains ("\"created\":")) { post ("Failed to find creation date"); return false; }
			if (!json.Contains ("\"email\":")) { post ("Failed to find email field"); return false; }
			if (!json.Contains ("\"data\":")) { post ("Failed to find sensor data"); return false; }
			post (json);
			return true;
		}
	}
}


