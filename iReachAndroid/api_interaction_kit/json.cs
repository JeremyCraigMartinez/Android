using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace api_interaction_kit
{
	[DataContract]
	public class user_information
	{
		[DataMember]
		public string id;
		[DataMember]
		public string email;
		[DataMember]
		public string pass;
		[DataMember]
		public string first_name;
		[DataMember]
		public string last_name;
		[DataMember]
		public int age;
		[DataMember]
		public int height;
		[DataMember]
		public int weight;
		[DataMember]
		public string sex;
		[DataMember]
		public string[] group;
		[DataMember]
		public string doctor;
	}

	[DataContract]
	public class create_user_information
	{
		[DataMember]
		public string email;
		[DataMember]
		public string pass;
		[DataMember]
		public string[] group;
		[DataMember]
		public string first_name;
		[DataMember]
		public string last_name;
		[DataMember]
		public int age;
		[DataMember]
		public int height;
		[DataMember]
		public int weight;
		[DataMember]
		public string sex;
		[DataMember]
		public string doctor;
	}

	[DataContract]
	public class food_item
	{
		[DataMember]
		public string created;
		[DataMember]
		public string foodID;
		[DataMember]
		public int quantity;
	}
		
	public class sensor_data
	{
		public class a
		{
			public List<float> x;
			public List<float> y;
			public List<float> z;
			public a() { x = new List<float>(); y = new List<float>(); z = new List<float>(); }
		}
		public class g
		{
			public List<float> x;
			public List<float> y;
			public List<float> z;
			public g() { x = new List<float>(); y = new List<float>(); z = new List<float>(); }
		}
		public class m
		{
			public List<float> x;
			public List<float> y;
			public List<float> z;
			public m() { x = new List<float>(); y = new List<float>(); z = new List<float>(); }
		}
		public a accel;
		public g gyro;
		public m mag;
		public sensor_data() { accel = new a(); gyro = new g(); mag = new m(); }
	}

	[DataContract]
	public class raw_data
	{
		[DataMember]
		public string created;
		[DataMember]
		public string email;
		[DataContract]
		public class Data
		{
			[DataMember]
			public _a accelerometer;
			[DataMember]
			public _m magnetometer;
			[DataMember]
			public _g gyroscope;
			public Data(_a a, _g g, _m m) {accelerometer = a; gyroscope = g; magnetometer = m;}
		}
		[DataMember]
		public Data data;
		public raw_data(_a a, _g g, _m m) { data = new Data (a, g, m); }
	}
	public class _a
	{
		public float[] x;
		public float[] y;
		public float[] z;
	}
	public class _g
	{
		public float[] x;
		public float[] y;
		public float[] z;
	}
	public class _m
	{
		public float[] x;
		public float[] y;
		public float[] z;
	}

	[DataContract]
	public class login_information
	{
		[DataMember]
		public string email;
		[DataMember]
		public string password;


		public string EmailAddress {
			get{ return email; }
		}
	}

	[DataContract]
	public class group
	{
		[DataMember]
		public string _id;
	}
	[DataContract]
	public class Groups
	{
		[DataMember]
		public string[] groups;
	}
	[DataContract]
	public class Doctors
	{
		[DataMember]
		public string[] doctors;
	}
	[DataContract]
	public class Processed_Data
	{
		[DataMember]
		public string _id;
		[DataMember]
		public string email;
		[DataMember]
		public string created;
		[DataMember]
		public string activity;
		[DataMember]
		public int duration;
		[DataMember]
		public float calories_burned;
	}

	public static class json_functions
	{
		static public string serializer (Object o)
		{
			return JsonConvert.SerializeObject (o);
		}

		static public Object deserializer (string data, Type T)
		{
			return JsonConvert.DeserializeObject (data, T); 
		}

		static public Object[] deserialize_array (string data, Type T)
		{
			JArray arr = JArray.Parse (data);
			if (arr != null && arr.Count > 1) {
				List<dynamic> l = new List<dynamic> ();
				foreach (JToken token in arr.Children()) {
					l.Add (JsonConvert.DeserializeObject (token.ToString (), T));
				}
				return l.ToArray ();
			}
			return null;
		}

	}
}

