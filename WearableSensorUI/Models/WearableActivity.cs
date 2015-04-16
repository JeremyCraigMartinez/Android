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
     
        private UUID mId;

        public UUID getId()
        {
            return mId;
            mDate = new Date();
        }

        private Date mDate;

        public WearableActivity()
        {
            this.mId = UUID.RandomUUID();
        }
     
        // Properties
        public string ActivityType
        {
            get;
            set;
        }

        public int Duration
        {
            get;
            set;
        }

        public double AvgPace
        {
            get;
            set;
        }

        public int Calorie
        {
            get;
            set;
        }
    }
}

