using System;


using Android.Views;
using Android.Widget;
using Android.OS;
using Android.App;

using Android.Support.V7.App;
using Android.Support.V4.Widget;


// Toolbar explicitly defined
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace WearableSensorUI
{
    [Activity(Label = "Wearables", Icon = "@drawable/icon", Theme = "@style/DefaultAppTheme")]
    public class MainActivity : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;

        // Drawer Items Lists views and operation
        private ListView mLeftDrawerList;
        private ListView mRightDrawerList;
        private ArrayAdapter mLeftAdapter;
        private ArrayAdapter mRightAdapter;

        // List of the data sets for left and right drawers
        private string[] mLeftData;
        private string[] mRightData;

        string mTitle;
        string mDrawerTitle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var profileName = Intent.GetStringExtra("profile");

            // Set Title;
            mTitle = mDrawerTitle = this.Title;

            // Initialize the drawer
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Find  references to the left and right drawer Views
            mLeftDrawerList = FindViewById<ListView>(Resource.Id.left_drawer);
            mRightDrawerList = FindViewById<ListView>(Resource.Id.right_drawer);

            mLeftDrawerList.Tag = 0;
            mRightDrawerList.Tag = 1;

            SetSupportActionBar(mToolbar);		

            // Load the resources into a string[] 
            mLeftData = Resources.GetStringArray(Resource.Array.left_drawer_items);
            mRightData = Resources.GetStringArray(Resource.Array.right_drawer_items);

            // Load the Left Drawer List Items from String Resource
            mLeftAdapter = new ArrayAdapter(this, Resource.Layout.drawer_list_item, mLeftData);
            mLeftDrawerList.Adapter = mLeftAdapter;

            mRightAdapter = new ArrayAdapter<string>(this, Resource.Layout.drawer_list_item, mRightData);
            mRightDrawerList.Adapter = mRightAdapter;


            // Set the On Item Click listener for the List
            mRightDrawerList.ItemClick += RightDrawerList_ItemClicked;
            mLeftDrawerList.ItemClick +=  (sender, args) => LeftDrawer_ListItemClicked(args.Position);


            // Set the drawer shadow
            this.mDrawerLayout.SetDrawerShadow(Resource.Drawable.drawer_shadow, (int)GravityFlags.Start);


            // This is what toggles our drawer
            mDrawerToggle = new MyActionBarDrawerToggle(
                this, 	                                        // Host
                mDrawerLayout,									// DrawerLayout

                // As per Android Design these are for accessibility reasons and should be used
                Resource.String.drawer_open,					// Message for opened drawer
                Resource.String.drawer_close					// Message for closed drawer
            );


            this.mDrawerToggle.DrawerClosed += (o, args) =>
            {
                this.SupportActionBar.Title = this.mTitle;
                this.InvalidateOptionsMenu();
                
            };

            this.mDrawerToggle.DrawerOpened += (o, args) =>
            {
                this.SupportActionBar.Title = this.mDrawerTitle;
                this.InvalidateOptionsMenu();
            };
       
            // Initialize Drawer Behaviors
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();


            // First Time the Activity Starts
            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null) {
                LeftDrawer_ListItemClicked (0);
            }



           
        }

        void LeftDrawer_ListItemClicked(int position)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = new HomeFragment();

                    break;
                case 1:
                    fragment = new ProfileFragment();

                    break;
                case 2:
                    fragment = new GoalsFragment();
                    break;
                case 3:
                    fragment = new HistoryFragment();
                    break;
                default:
                    fragment = new ExerciseFragment();
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.main_content_frame, fragment)
                .Commit();


            // Finalize Drawer Events (I FInally Understand This)
            this.mLeftDrawerList.SetItemChecked(position, true);
            mToolbar.Title = this.Title = mLeftData[position];

            mDrawerLayout.CloseDrawer(mLeftDrawerList);

        }


        void RightDrawerList_ItemClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            
        }

        // Calls when item is clicked and handles menu overlaps
        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.CloseDrawer(mRightDrawerList);
                    mDrawerToggle.OnOptionsItemSelected(item);
                    return true;
                case Resource.Id.action_overflow:
                    if (mDrawerLayout.IsDrawerOpen(mRightDrawerList))
                        mDrawerLayout.CloseDrawer(mRightDrawerList);
                    else
                    {
                        mDrawerLayout.CloseDrawer(mLeftDrawerList);
                        mDrawerLayout.OpenDrawer(mRightDrawerList);
                    }

                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }

        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            // Check if left drawer is open then close it to open right drawer
            var open = this.mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left);
            for (int i = 0; i < menu.Size(); i++)
            {
                menu.GetItem(i).SetVisible(!open);
            }
                

            return base.OnPrepareOptionsMenu(menu);
        }
        // PostCreation
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        // Inflate the options menu
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        

        // Save instance of the activity
        protected override void OnSaveInstanceState(Bundle outState)
        {
            // Check if the left Drawer open GravityFlags.Left is an
            // Integer representation of The State of DrawerLayou
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }
            else
            {
                outState.PutString("DrawerState", "Closed");
            }
            base.OnSaveInstanceState(outState);
        }


      

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }

    }
}


