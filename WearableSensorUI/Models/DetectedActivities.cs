﻿//
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
    public class DetectedActivities
    {
        public int ImageResId { get; set; }
        public string Name {get; set; }
        public string Calories {get; set; }
        public string Time {get; set; }
        public bool favorite {get; set; }

    }



}
