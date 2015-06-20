
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;


namespace iReachAndroid.Helpers
{
	public class Adapter: FragmentPagerAdapter
	{
		List<Fragment> fragments = new List<Fragment> ();
		List<string> fragmentTitles = new List<string> ();

		public Adapter (FragmentManager fm) : base (fm)
		{

		}

		public void AddFragment (Fragment fragment, String title)
		{
			fragments.Add (fragment);
			fragmentTitles.Add (title);
		}

		public override Fragment GetItem (int position)
		{
			return fragments [position];
		}

		public override int Count {
			get { return fragments.Count; }
		}

		public override Java.Lang.ICharSequence GetPageTitleFormatted (int position)
		{
			return new Java.Lang.String (fragmentTitles [position]);
		}
	}
}

