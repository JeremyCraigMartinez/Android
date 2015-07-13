using System;
using System.Threading.Tasks;
using iReach.Portable.Models;
using Newtonsoft.Json;

namespace iReach.Portable.Models
{
	public class DoctorModel
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("email")]
		public string Password { get; set; }
	}
}

