using System;
using Newtonsoft.Json;

namespace iReach.Portable.Models
{
	public class Patient
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("pass")]
		public string Password { get; set; }

		[JsonProperty("first_name")]
		public string First_Name { get; set; }

		[JsonProperty("last_name")]
		public string Last_Name { get; set; }

		[JsonProperty("group")]
		public string Group { get; set; }

		[JsonProperty("doctor")]
		public string Doctor { get; set; }

		[JsonProperty("age")]
		public int Age { get; set; }

		[JsonProperty("height")]
		public float Height { get; set; }

		[JsonProperty("weight")]
		public float Weight { get; set; }

		[JsonProperty("sex")]
		public string Sex { get; set; }


		public Patient ()
		{
			
		}
	}
}

