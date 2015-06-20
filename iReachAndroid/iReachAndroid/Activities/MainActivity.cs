
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using iReachAndroid.Fragments;
using SupportFragment = Android.Support.V4.App.Fragment;
using api_interaction_kit;

namespace iReachAndroid.Activities
{
	[Activity (Label = "MainActivity")]			
	public class MainActivity : BaseActivity
	{
		private DrawerLayout mDrawer;
		private NavigationView mNvView;
		private ActionBarDrawerToggle drawerToggle;
		private TextView HeaderUserName;
		private string USER_EMAIL;
		private api iReachApi;


		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			USER_EMAIL = Intent.GetStringExtra ("Email") ?? "Logged in as Guest";
			// get the res for drawerlayout
			mDrawer = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			drawerToggle = SetupDrawerToggle (Resource.String.drawer_open, Resource.String.drawer_close);
			mDrawer.SetDrawerListener (drawerToggle);
			HeaderUserName = FindViewById<TextView> (Resource.Id.header_userName);

			HeaderUserName.Text = USER_EMAIL;


			mNvView = FindViewById<NavigationView> (Resource.Id.main_drawer);
			if (mNvView != null) {
				SetupDrawerContent (mNvView);
			}

			// When app first launches after login / skip or app we need the Home View to display
			if (savedInstanceState == null) {
				var navMenu = mNvView.Menu;
				var firstItem = navMenu.FindItem (Resource.Id.nav_home_fragment);
				SelectDrawerItem (firstItem);

			}

		}

		private ActionBarDrawerToggle SetupDrawerToggle (int openResDesc, int closeResDesc)
		{
			return new ActionBarDrawerToggle (this, mDrawer, Toolbar, openResDesc, closeResDesc);
		}

		void SetupDrawerContent (NavigationView navigationView)
		{

			// Subscribe to the event using annoymous method to get MenuItem
			navigationView.NavigationItemSelected += (sender, e) => {
				SelectDrawerItem (e.MenuItem);
				mDrawer.CloseDrawers ();
			};
		}

		public void SelectDrawerItem (IMenuItem menuItem)
		{

			Fragment fragment = null;
			SupportFragment supportFrag = null;

			switch (menuItem.ItemId) {
			case Resource.Id.nav_home_fragment:
				supportFrag = new HomeFragment ();
				break;
			case Resource.Id.nav_activity_fragment:
				supportFrag = new DetectedActivityFragment ();
				break;
			case Resource.Id.nav_food_fragment:
				supportFrag = new FoodFragment ();
				break;
			default:
				break;
			}
			// Handle Non Support Fragment
			if (fragment != null) {
				var fragtrans = FragmentManager.BeginTransaction ();
				fragtrans.Replace (Resource.Id.main_content_frame, fragment).Commit ();
				menuItem.SetChecked (true);
				mDrawer.CloseDrawers ();
			}

			var fragTransaction = SupportFragmentManager.BeginTransaction ();
			fragTransaction.Replace (Resource.Id.main_content_frame, supportFrag).Commit ();

			this.Title = menuItem.TitleFormatted.ToString ();
			menuItem.SetChecked (true);
			mDrawer.CloseDrawers ();


		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (drawerToggle.OnOptionsItemSelected (item))
				return true;

			return base.OnOptionsItemSelected (item);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			base.OnCreateOptionsMenu (menu);


			MenuInflater.Inflate (Resource.Menu.app_menu, menu);
			return true;
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			drawerToggle.SyncState ();
		}

		public override void OnConfigurationChanged (Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);

			drawerToggle.OnConfigurationChanged (newConfig);
		}


		#region implemented abstract members of BaseActivity

		protected override int LayoutResource {
			get {
				return Resource.Layout.activity_main;
			}
		}

		#endregion
	}



}

