using System;
using Newtonsoft.Json;
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
			public List<string> x;
			public List<string> y;
			public List<string> z;
			public a() { x = new List<string>(); y = new List<string>(); z = new List<string>(); }
		}
		public class g
		{
			public List<string> x;
			public List<string> y;
			public List<string> z;
			public g() { x = new List<string>(); y = new List<string>(); z = new List<string>(); }
		}
		public class m
		{
			public List<string> x;
			public List<string> y;
			public List<string> z;
			public m() { x = new List<string>(); y = new List<string>(); z = new List<string>(); }
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
		[DataMember]
		public _a accel;
		[DataMember]
		public _g gyro;
		[DataMember]
		public _m mag;
		public raw_data(_a a, _g g, _m m) { accel = a; gyro = g; mag = m; }
	}
	public class _a
	{
		public string[] x;
		public string[] y;
		public string[] z;
	}
	public class _g
	{
		public string[] x;
		public string[] y;
		public string[] z;
	}
	public class _m
	{
		public string[] x;
		public string[] y;
		public string[] z;
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

	public static class json_functions
	{
		static public string serializer (Object o)
		{
			return JsonConvert.SerializeObject (o);
		}

		static public Object deserializer (string data, Type T)
		{
			var temp = JsonConvert.DeserializeObject (data, T); 
			return temp;
		}
	}
}

