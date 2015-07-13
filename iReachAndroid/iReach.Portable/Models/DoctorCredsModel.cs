using System;
using Newtonsoft.Json;

namespace iReach.Portable
{
	public class DoctorCredsModel
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("pass")]
		public string Password { get; set; }
	}
}

