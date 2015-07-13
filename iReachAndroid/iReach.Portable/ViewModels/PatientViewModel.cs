using System;
using iReach.Portable.Models;
using Cirrious.MvvmCross.ViewModels;
using System.Threading.Tasks;
using Cirrious.CrossCore;

namespace iReach.Portable.ViewModels
{
	public class PatientViewModel : MvxViewModel
	{

		public static string DefaultIcon = @"http://refractored.com/default.png";
		public Patient User { get; set; }
		public bool isAuthenticated = false;


		public bool CanDelete
		{
			get { return NewUserId != 0; }
		}

		public int NewUserId { get; set; }

		public PatientPhoto Photo{ get; set; }
		private readonly string eventId, eventName, groupId, group;
		private long eventDate;
		public PatientViewModel(Patient patient, PatientPhoto photo, string eventId, string eName, string gId, string gName, long eDate)
		{
			this.User = patient;
			this.eventId = eventId;
			this.eventName = eName;
			this.groupId = gId;
			this.group = gName;
			this.eventDate = eDate;
			this.Photo = photo ?? new PatientPhoto
			{
				HighResLink = DefaultIcon,
				PhotoId = 0,
				ThumbLink = DefaultIcon,
				PhotoLink = DefaultIcon
			};

			if (string.IsNullOrWhiteSpace(this.Photo.HighResLink))
				this.Photo.HighResLink = DefaultIcon;


			if (string.IsNullOrWhiteSpace(this.Photo.ThumbLink))
				this.Photo.ThumbLink = DefaultIcon;

			if (string.IsNullOrWhiteSpace(this.Photo.PhotoLink))
				this.Photo.PhotoLink = DefaultIcon;
		}

		public string Name { get { return this.User.First_Name; } }
		private IMvxCommand checkInCommand;
		public IMvxCommand CheckInCommand
		{
//			get { return checkInCommand ?? (checkInCommand = new MvxCommand(async () => ExecuteCheckInCommand())); }
//			
			get; set;
		}

		private async Task ExecuteCheckInCommand()
		{
			
		}
		
	}
}

