
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using iReachAndroid.Activities;
using iReachAndroid.Helpers;

namespace iReachAndroid.Fragments
{
	public class HomeFragment : Fragment
	{
		
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.fragment_home, container, false);
		
			var viewPager = view.FindViewById<ViewPager> (Resource.Id.viewpager);
			if (viewPager != null)
				SetupViewPager (viewPager);


			var tabLayout = view.FindViewById<TabLayout> (Resource.Id.tabs);
			tabLayout.SetupWithViewPager (viewPager);


			return view;		
		}

		void SetupViewPager (Android.Support.V4.View.ViewPager vPager)
		{
			var adapter = new Adapter (FragmentManager);
			adapter.AddFragment (new DetectedActivityFragment (), "Recent");
			adapter.AddFragment (new HistoryFragment (), "History");
			adapter.AddFragment (new GoalsFragment (), "Goals");

			vPager.Adapter = adapter;
		}



	}

}

