using System;
using iReach.Portable.Models;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;

namespace iReach.Portable.Models
{
	public class PatientPhoto
	{
		[JsonProperty("photo_link")]
		public string PhotoLink { get; set; }

		[JsonProperty("highres_link")]
		public string HighResLink { get; set; }

		[JsonProperty("thumb_link")]
		public string ThumbLink { get; set; }

		[JsonProperty("photo_id")]
		public int PhotoId { get; set; }
	}

}

