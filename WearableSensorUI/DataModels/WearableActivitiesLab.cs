//
//  WearableActivitiesLab.cs
//
//  Author:
//       Faustino Lukolo <faustino.lukolo@wsu.edu>
//
//  Copyright (c) 2015 GPL
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Android.App;
using Android.Content;
using System.Collections.Generic;
using Java.Util;
using Android.Provider;

namespace WearableSensorUI
{
    public class WearableActivitiesLab
    {
        private List<WearableActivity> mUserActivities;

        private static WearableActivitiesLab sMyActivity;
        private Context mAppContext;

        public WearableActivitiesLab(Context appContext)
        {
            mAppContext = appContext;         
            mUserActivities = new List<WearableActivity>();

            var rnd = new System.Random();
            // Generate 10 activities
            for (int i = 0; i < 20; i++)
            {
                int pace = rnd.Next(0, 10);
                int heart = rnd.Next(60, 160);
                int duration = rnd.Next(5, 60);
                int cal = rnd.Next(50, 500);
                string ActivityName = getName(rnd.Next(0, 4));
               
                WearableActivity act = new WearableActivity();
                act.ActivityType = ActivityName;
                act.Duration = duration;
                act.AvgPace = pace;
                act.Calorie = cal;       

                mUserActivities.Add(act);
            }

        }

        public string getName(int activity)
        {
            switch (activity)
            {
                case 0:
                    return "Sitting";
                case 1:
                    return "Walking";
                case 2:
                    return "Standing";
                case 3:
                    return "running";
                case 4:
                    return "cooking";
                default:
                    break;
            }

            return "Unknown";
        }

        // Incase app needs to do something
        public static WearableActivitiesLab get(Context c)
        {
            if (sMyActivity == null)
                sMyActivity = new WearableActivitiesLab(c.ApplicationContext);

            return sMyActivity;
                    
        }

        public List<WearableActivity> getActivities()
        {
            return mUserActivities;
        }

        public WearableActivity getActivity(UUID id)
        {
            foreach (WearableActivity a in mUserActivities)
            {
                if (a.getId() == id)
                    return a;
            }

            return null;
        }
    }
}

