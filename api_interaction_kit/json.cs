using System;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

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
		public string height;
		[DataMember]
		public string weight; 
		[DataMember]
		public string sex;
		[DataMember]
		public string group;
	}

	[DataContract]
	public class login_information
	{
		[DataMember]
		public string email;
		[DataMember]
		public string pass;
	}

	public static class json_functions
	{
		static public void serializer (Object o, Type T, ref MemoryStream S)
		{
			System.Runtime.Serialization.Json.DataContractJsonSerializer s = new System.Runtime.Serialization.Json.DataContractJsonSerializer (T);
			s.WriteObject (S, o);
		}
		static public Object deserializer (Stream data, Type T)
		{
			System.Runtime.Serialization.Json.DataContractJsonSerializer s = new System.Runtime.Serialization.Json.DataContractJsonSerializer (T);
			return(s.ReadObject (data));
		}
	}
}

