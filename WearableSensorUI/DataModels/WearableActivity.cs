//
//  EmptyClass.cs
//
//  Author:
//       Faustino Lukolo <faustino.lukolo@wsu.edu>
//
//  Copyright (c) 2015 GPL
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Java.Util;
using Android.Locations;
using System.Collections.Generic;
using Android.Views;
using Android.Util;

namespace WearableSensorUI
{
    public class WearableActivity
    {
     
        public string Name { get; set; }
        public string Duration { get; set; }
        public string TotalSteps { get; set; }
        public string CaloriesBurned { get; set; }
        public string AvgPac { get; set; }
        public string AvgSpeed { get; set; }
        public string TotalDistance { get; set; }

        System.Random rnd;
      
        public WearableActivity()
        {
            rnd = new System.Random();    
        }       

        public void Generate()
        {
             

            Duration = rnd.Next(20, 120).ToString();
            TotalSteps = rnd.Next(10, 10000).ToString();
            CaloriesBurned = rnd.Next(10, Convert.ToInt32(TotalSteps) * Convert.ToInt32(Duration)).ToString();
           
            TotalDistance = rnd.Next(Convert.ToInt32(TotalSteps) / Convert.ToInt32(Duration), 6000).ToString();
            AvgPac = rnd.Next(0, 40).ToString();
            AvgSpeed = rnd.Next(3, 15).ToString();

            Name = getActivityName(AvgPac);


        }

        public string getActivityName(string avgpace)
        {
            int pace = Convert.ToInt32(AvgPac);

            if (pace <= 3)
                return "Walking";
            else if( pace > 3 && pace <= 5)
                return "Jogging";
            else if (pace > 5 && pace <= 10)
                return "Running";
            else if (pace >10 && pace <= 15)
                return "Biking";
            else if (pace > 15 && pace <= 60)
                return "Driving";
            else 
                return "Unknown Activity";
        }

      

    }
}

