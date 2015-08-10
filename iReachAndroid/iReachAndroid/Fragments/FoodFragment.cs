
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
using iReachAndroid.Fragments;

namespace iReachAndroid
{
	public class FoodFragment : Fragment
	{
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
		 
			var view = inflater.Inflate (Resource.Layout.fragment_food_log, null);
			var viewPager = view.FindViewById<ViewPager> (Resource.Id.food_viewpager);

			if (viewPager != null)
				SetupViewPager (viewPager);

			var tabLayout = view.FindViewById<TabLayout> (Resource.Id.food_tabs);
			tabLayout.SetupWithViewPager (viewPager);

			return view;
		}

		void SetupViewPager (Android.Support.V4.View.ViewPager vPager)
		{
			var adapter = new Adapter (FragmentManager);
			adapter.AddFragment (new ScanFoodFragment(), "Scan Barcode");
			adapter.AddFragment (new BrowseFoodFragment (), "Browse Foodtype");
			adapter.AddFragment (new MyFoodDiaryFragment (), "My Food Journal");

			vPager.Adapter = adapter;
		}

		 
	}
}

