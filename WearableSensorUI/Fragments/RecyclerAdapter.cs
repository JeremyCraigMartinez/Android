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
using Android.Animation;
using System;

namespace WearableSensorUI
{
    public class RecyclerAdapter : RecyclerView.Adapter
	{
        private List<WearableActivity> Activities;
        private RecyclerView mRecyclerView;
        private Context mContext;
        private int mCurrentPosition = -1;

        public RecyclerAdapter(List<WearableActivity> activitiesList, RecyclerView recyclerView, Context context)
        {
            Activities = activitiesList;
            mRecyclerView = mRecyclerView;
            mContext = context;
         
        }


        public class MyView : RecyclerView.ViewHolder
        {
            public View mMainView { get; set; }

            public TextView Name { get; set; }
            public TextView Duration { get; set; }
            public TextView TotalDistance { get; set; }
            public TextView TotalSteps { get; set; }
            public TextView CaloriesBurned { get; set; }
            public TextView  AvgPac { get; set; }
            public TextView AvgSpeed { get; set; }


            public MyView (View view) : base (view){

                mMainView = view;

            }

        }

        public class MapView : RecyclerView.ViewHolder
        {

            public View mMapView { get; set;}
            public MapView (View view) : base(view)
            {
                mMapView = view;                
                
            }
        }


        public override int GetItemViewType(int position)
        {

            if((position % 2 ) == 0)
                return Resource.Layout.rowActivity;
            else
                return Resource.Layout.rowSummary;


        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            if (viewType == Resource.Layout.rowActivity)
            {
                //First card view
                View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rowActivity, parent, false);

                TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
                TextView txtDuration = row.FindViewById<TextView>(Resource.Id.txtTimeDuration);
                TextView txtDistance = row.FindViewById<TextView>(Resource.Id.txtDistance);
                TextView txtStepCount = row.FindViewById<TextView>(Resource.Id.txtStepscount);
                TextView txtCalories = row.FindViewById<TextView>(Resource.Id.txtCalories);
                TextView txtPace = row.FindViewById<TextView>(Resource.Id.txtPace);
                TextView txtSpeed = row.FindViewById<TextView>(Resource.Id.txtSpeed);

                MyView view = new MyView(row) { Name = txtName, Duration = txtDuration, TotalDistance = txtDistance, TotalSteps = txtStepCount, CaloriesBurned = txtCalories, AvgPac = txtPace, AvgSpeed = txtSpeed};
                return view;
            }

            else
            {
                //Second card view
                View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rowSummary, parent, false);
                MapView view = new MapView(row);
                return view;
            }

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is MyView)
            {
                //First view
                MyView myHolder = holder as MyView;
                myHolder.mMainView.Click += mMainView_Click;
                myHolder.Name.Text += Activities[position].Name;
                myHolder.Duration.Text += Activities[position].Duration;
                myHolder.TotalDistance.Text += Activities[position].TotalDistance;
                myHolder.TotalSteps.Text += Activities[position].TotalSteps;
                myHolder.CaloriesBurned.Text += Activities[position].CaloriesBurned;
                myHolder.AvgPac.Text += Activities[position].AvgPac;
                myHolder.AvgSpeed.Text += Activities[position].AvgSpeed;

                if (position > mCurrentPosition)
                {
                    int currentAnim = Resource.Animation.slide_left_to_right;
                    SetAnimation(myHolder.mMainView, currentAnim);
                    mCurrentPosition = position;
                }
            }

            else
            {
                //Second View
                MapView myHolder = holder as MapView;
                if (position > mCurrentPosition)
                {
                    int currentAnim = Resource.Animation.slide_right_to_left;
                    SetAnimation(myHolder.mMapView, currentAnim);
                    mCurrentPosition = position;
                }
            }

        }

        void SetAnimation(View mMainView, int currentAnim)
        {
            Animator animator = AnimatorInflater.LoadAnimator(mMainView.Context, Resource.Animation.flip);
            animator.SetTarget(mMainView);
            animator.Start();
        }

        void mMainView_Click(object sender, System.EventArgs e)
        {
//            int position = mRecyclerView.GetChildPosition((View)sender);
//            Console.WriteLine(Activities[position].Name);

            Toast.MakeText(this.mContext, "Item Click Not Yet Implemented", ToastLength.Short).Show();
        }


        #region implemented abstract members of Adapter
        public override int ItemCount
        {
            get
            {
                return Activities.Count;
            }
        }
        #endregion
	}

}

