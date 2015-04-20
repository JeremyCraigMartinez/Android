using System;
using System.Runtime.Serialization;
using System.IO;

namespace api_interaction_kit
{
	[DataContract]
	public class user_information
	{
		[DataMember]
		public string _id;
		[DataMember]
		public string first_name;
		[DataMember]
		public string last_name;
		[DataMember]
		public string age;
		[DataMember]
		public string height; //Inches
		[DataMember]
		public string weight; //pounds
		[DataMember]
		public string sex;
		[DataMember]
		public string group;
	}

	public static class json_functions
	{
		static public System.Runtime.Serialization.Json.DataContractJsonSerializer serializer (Type T)
		{
			return (new System.Runtime.Serialization.Json.DataContractJsonSerializer (T));
		}
		static public Object deserializer (Stream data, Type T)
		{
			System.Runtime.Serialization.Json.DataContractJsonSerializer s = new System.Runtime.Serialization.Json.DataContractJsonSerializer (T);
			var temp = s.ReadObject (data);
			return temp;
		}
	}
}

