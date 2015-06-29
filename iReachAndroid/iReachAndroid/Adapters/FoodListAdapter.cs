using System;
using System.Collections.Generic;
using iReach.Core;
using Android.Widget;
using Android.App;
using Android;

namespace iReachAndroid
{
	public class FoodListAdapter : BaseAdapter<Food>
	{
		protected Activity mContext = null;
		protected IList<Food> mFoods = new List<Food>();

		public FoodListAdapter (Activity context, IList<Food> foods)
		{
			this.mContext = context;
			this.mFoods = foods;		
			
		}

		public override Food this[int position]
		{
			get { return mFoods [position]; }
		}

		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			// Get object located at position
			var item = mFoods [position];

			// Reuse convert view if not null otherwise inflate it
			var view = (convertView ??
			           mContext.LayoutInflater.Inflate
						(
							Android.Resource.Layout.SimpleListItem1,
				           parent,
				           false)
			           ) as CheckedTextView;

			view.SetText (item.ComName == "" ? "<new food>" : item.ComName, TextView.BufferType.Normal);


			return view;
		}

		public override int Count {
			get {
				return mFoods.Count;
			}
		}

		#endregion
	}
}

