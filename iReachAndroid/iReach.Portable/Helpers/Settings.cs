using System;
using Cirrious.CrossCore;
using MvvmCross.Plugins.Settings;


namespace iReach.Portable.Helpers
{
	public static class Settings
	{
		private static IMvxSettings _settings;
		private static IMvxSettings UserSettings 
		{
			get { return _settings ?? (_settings = Mvx.GetSingleton<IMvxSettings> ());}
		}

		private const string ShowAllEventsKey = "show_all_events";
		private const bool ShowAllEventsDefault = false;

		private const string UserIdKey = "user_id";
		private static string UserIdDefault = string.Empty;

		private const string UserNameKey = "user_name";
		private static string UserNameDefault = string.Empty;


		// For Future Authentications and Security we can save the Api Key here

		// User Id
		public static string Email
		{
			get 
			{ 
				return UserSettings.GetValue(UserIdKey, UserIdDefault);
			}

			set 
			{
				if (UserSettings.AddOrUpdateValue (UserIdKey, value))
					UserSettings.Save ();
			}
		}



	}
}

