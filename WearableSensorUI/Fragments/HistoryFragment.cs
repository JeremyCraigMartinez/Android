//
//  HistoryFragment.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace WearableSensorUI
{
    public class HistoryFragment :ListFragment
    {

        private List<WearableActivity> mDetectedActivities;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.SetTitle(Resource.String.activity_title);
           
         
        }

        //        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //        {
        //            // Use this to return your custom view for this Fragment
        //            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
        //
        //            base.OnCreateView(inflater, container, savedInstanceState);
        //
        //            var view = inflater.Inflate(Resource.Layout.fragment_history_page, container, false);
        //            return view;
        //        }
    }
}

