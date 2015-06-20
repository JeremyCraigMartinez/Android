
using System;
using Android.Views;

namespace iReachAndroid.Helpers
{

	public class ClickListener : Java.Lang.Object, View.IOnClickListener
	{
		public ClickListener (Action<View> handler)
		{
			Handler = handler;			
		}

		public Action<View> Handler { get; set; }

		public void OnClick (View v)
		{
			var h = Handler;
			if (h != null)
				h (v);
		}
	}
}
