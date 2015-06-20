
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.App;

namespace iReachAndroid.Activities
{
	[Activity (Label = "BaseActivity")]			
	public abstract class BaseActivity : AppCompatActivity
	{

		public Toolbar Toolbar {
			get;
			set;
		}

		protected abstract int LayoutResource {
			get;
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (LayoutResource);
			// Create your application here
			SetupToolbar (Toolbar);

		}

		public void SetupToolbar (Toolbar toolbar)
		{
			Toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);

			if (Toolbar != null) {

				SetSupportActionBar (Toolbar);
				SupportActionBar.SetDisplayHomeAsUpEnabled (true);
				SupportActionBar.SetHomeButtonEnabled (true);
			}

		}

		protected int ActionBarIcon {
			set { Toolbar.SetNavigationIcon (value); }
		}
	}
}

