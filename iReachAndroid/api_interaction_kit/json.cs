using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.IO;
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

	[DataContract]
	public class raw_data
	{
		[DataMember]
		public string created;
		[DataMember]
		public string email;
		[DataMember]
		public string data;
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

