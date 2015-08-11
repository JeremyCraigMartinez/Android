using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using IReach.BL;


namespace iReachAndroid.Fragments
{
	public class BrowseFoodFragment : Android.Support.V4.App.Fragment
	{
 
		protected Adapters.FoodListAdapter fooditemListAdapter;
		protected IList<FoodItem> foodItems;

		private Button AddFoodButton;
		private ListView FoodListView;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.fragment_browse_food, null);
			 
		 
			AddFoodButton = view.FindViewById<Button> (Resource.Id.btnAddFood);
			FoodListView = view.FindViewById<ListView> (Resource.Id.lstFood);

			if (AddFoodButton != null) {
				AddFoodButton.Click += (sender, e) => { 
					
					FoodDetailFragment foodDetail = new FoodDetailFragment ();					 
					var fragtrans = FragmentManager.BeginTransaction ();
					fragtrans.Replace (Resource.Id.main_content_frame, foodDetail).Commit ();
				};
			}

			// wire up task click handler
			if (FoodListView != null) { 

				FoodListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					 
					FoodDetailFragment foodDetail = new FoodDetailFragment ();
					var fragtrans = FragmentManager.BeginTransaction ();

					Bundle data = new Bundle ();
					data.PutInt("FoodId", foodItems[e.Position].ID);

					foodDetail.Arguments = data;
					fragtrans.Replace (Resource.Id.main_content_frame, foodDetail).Commit ();
				};
			}

			return view;
		}
	}
}

