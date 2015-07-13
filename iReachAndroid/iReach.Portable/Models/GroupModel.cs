using System;
using Newtonsoft.Json;

namespace iReach.Portable
{
	public class GroupModel
	{
		[JsonProperty("_id")]
		public string GroupId { get; set; }
	}
}

