using System;
using Android.Content;
using Android.OS;
using Android.Views;
using IReach.BL;
using Android.Widget;
using iReachAndroid.Fragments;

namespace iReachAndroid.Fragments
{
	
	public class FoodDetailFragment : Android.Support.V4.App.Fragment
	{
		protected FoodItem foodItem = new FoodItem();
		protected Button cancelDeleteButton = null;

		protected EditText nameTextEdit = null;
		protected Button saveButton = null;
		protected SeekBar _seekbar; 
		protected TextView AmountTextEdit = null;

		protected Android.Support.V4.App.FragmentManager fragManager;

		private static api_interaction_kit.api mApi;
		public FoodDetailFragment()
		{

			mApi = iReachApp.GetInstance();	
			mApi.server_update += MApi_server_update;

		}	

		void MApi_server_update (object o, api_interaction_kit.Response_Type r)
		{
			BrowseFoodFragment browseFrag = new BrowseFoodFragment ();
			if (r == api_interaction_kit.Response_Type.food_sent) {
			
				if ((bool)o == true) {

					var fragTrans = fragManager.BeginTransaction ();
					fragTrans.Replace (Resource.Id.main_content_frame, browseFrag, "BrowseFood");
					fragTrans.Commit ();
				}
			}
		}

		 

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.FoodDetails, container, false);
			 
			_seekbar = view.FindViewById<SeekBar>(Resource.Id.seekbar);
			cancelDeleteButton = view.FindViewById<Button>(Resource.Id.btnCancelDelete);
			nameTextEdit = view.FindViewById<EditText>(Resource.Id.txtFoodName);
			saveButton = view.FindViewById<Button>(Resource.Id.btnSave);
			AmountTextEdit = view.FindViewById<TextView>(Resource.Id.AmountLabel);

			int foodID = Activity.Intent.GetIntExtra ("FoodId", 0);
			 
			if(foodID > 0) {
				foodItem = IReach.BL.Managers.ItemManager.GetFood(foodID);
			}
			_seekbar.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
				if (e.FromUser)
				{
					AmountTextEdit.Text = string.Format("{0}", e.Progress);
				}
			};
 
			// set the cancel delete based on whether or not it's an existing task
			cancelDeleteButton.Text = (foodItem.ID == 0 ? "Cancel" : "Delete");

			// name
			nameTextEdit.Text = foodItem.Name;
			// notes
			AmountTextEdit.Text = foodItem.Amount;
			 
		
			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };

			return view;
		}
	 

		protected void Save()
		{



			foodItem.Name = nameTextEdit.Text;
			foodItem.Amount = AmountTextEdit.Text;
		 
			var serving = Convert.ToInt32(foodItem.Amount);				
			IReach.BL.Managers.ItemManager.SaveFood(foodItem);
//
//			BrowseFoodFragment foodListFrag = new BrowseFoodFragment();
//			var fragtrans = FragmentManager.BeginTransaction ();
//			fragtrans.Replace (Resource.Id.main_content_frame, foodListFrag).Commit ();

			mApi.api_food_upload (foodItem.Name, serving);

		}

		protected void CancelDelete()
		{
			if(foodItem.ID != 0) {
				IReach.BL.Managers.ItemManager.DeleteFood(foodItem.ID);
			}

			Activity.Finish ();		
		

			BrowseFoodFragment foodListFrag = new BrowseFoodFragment();
			var fragtrans = FragmentManager.BeginTransaction ();
			fragtrans.Replace (Resource.Id.main_content_frame, foodListFrag).Commit ();
		}
	}
}

