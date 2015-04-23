//
//  HomeFragment.cs
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
using Android.OS;

using Android.Content;
using Android.App;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Widget;
using Android.Views;

namespace WearableSensorUI
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        private List<WearableActivity> ActivitiesDetected;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            ActivitiesDetected = new List<WearableActivity>();

           // Generate 20 Activities
            for (int i = 0; i < 15; i++)
            {
                var DetectedActivity = new WearableActivity();
                DetectedActivity.Generate();


                ActivitiesDetected.Add(DetectedActivity);

            }
        }        


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_home_page, container, false);

            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);


            //Create our layout manager
            mLayoutManager = new LinearLayoutManager(view.Context);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerAdapter(ActivitiesDetected, mRecyclerView, view.Context);
            mRecyclerView.SetAdapter(mAdapter);

            return view;
        }

    }
}

