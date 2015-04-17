using System;
using System.Runtime.Serialization;
using System.IO;

namespace api_interaction_kit
{
	[DataContract]
	public class json : Object
	{
		
	}
		
	public class information : json
	{
		[DataMember]
		protected string first_name;
		[DataMember]
		protected string last_name;
		[DataMember]
		protected string age;
		[DataMember]
		protected string height; //Inches
		[DataMember]
		protected string weight; //pounds
		[DataMember]
		protected string sex;
	}

	public static class json_functions
	{
		static public System.Runtime.Serialization.Json.DataContractJsonSerializer serializer (Type T)
		{
			return (new System.Runtime.Serialization.Json.DataContractJsonSerializer (T));
		}
		static public Object deserializer (System.Runtime.Serialization.Json.DataContractJsonSerializer s)
		{
			
		}
	}
}

