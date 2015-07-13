using System;
using Newtonsoft.Json;

namespace iReach.Portable
{
	public class DietModel
	{
		[JsonProperty("foodID")]
		public int FoodId { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }
	 
	}
}

