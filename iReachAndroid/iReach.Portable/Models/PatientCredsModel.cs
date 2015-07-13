using System;
using Newtonsoft.Json;

namespace iReach.Portable
{
	public class PatientCredsModel
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("pass")]
		public string Password { get; set; }
	}
}

