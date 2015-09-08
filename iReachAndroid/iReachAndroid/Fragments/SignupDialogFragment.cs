
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using iReachAndroid.Activities;

namespace iReachAndroid
{
	public class SignupDialogFragment : Android.Support.V4.App.DialogFragment
	{

		private EditText mFirstName;
		private EditText mLastName;
		private EditText mDocEmail;

		private EditText mEmail;
		private EditText mPassword;

		private Spinner groupSpinner;
		private Spinner genderSpinner;
		private string GroupId;
		private string mGender;




		private EditText mHeight;
		private EditText mAge;
		private EditText mWeight;


		private Button mCancelButton;
		private Button mSubmitButton;

		// API Access
		private static api_interaction_kit.api api;

		public override void OnCreate (Bundle savedInstanceState)
		{
			api = (api_interaction_kit.api)iReachApp.GetInstance ();	
			api.server_update += Api_server_update;
			base.OnCreate (savedInstanceState);
			// Create your fragment here
		}

		void Api_server_update (object o, api_interaction_kit.Response_Type r)
		{
			if (r == api_interaction_kit.Response_Type.user_created) {
				if ((bool)o == true) {
					//do something

					// Here switch to Home Fragment
					var intent = new Intent(Activity, typeof(MainActivity));
					intent.PutExtra ("ProfileType", mEmail.Text );

					StartActivity (intent);
				}
			}
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			var view = inflater.Inflate (Resource.Layout.signup_dialog_fragment, container, false);
			SetupViews (view);

			mCancelButton.Click += (sender, e) => Dismiss ();
			mSubmitButton.Click += MSubmitButton_Click;
			return view;


		}

		public void SetupViews(View view)
		{
			mFirstName = view.FindViewById<EditText> (Resource.Id.txtFirstName);
			mLastName = view.FindViewById<EditText> (Resource.Id.txtLastName);

			mEmail = view.FindViewById<EditText> (Resource.Id.txtEmail);
			mPassword = view.FindViewById<EditText> (Resource.Id.txtPassword);

			mDocEmail = view.FindViewById<EditText> (Resource.Id.txtDocEmail);

			mCancelButton = view.FindViewById<Button> (Resource.Id.btnCancel);
			mSubmitButton = view.FindViewById<Button> (Resource.Id.btnSubmit);

			groupSpinner = view.FindViewById<Spinner> (Resource.Id.groupSpinner);
			genderSpinner = view.FindViewById<Spinner> (Resource.Id.genderSpinner);

			mAge = view.FindViewById<EditText> (Resource.Id.txtAge);
			mHeight = view.FindViewById<EditText> (Resource.Id.txtHeight);
			mWeight = view.FindViewById<EditText> (Resource.Id.txtWeight);

			groupSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (GroupSpinner_ItemSelected);
			var group_adapter = ArrayAdapter.CreateFromResource (
				Activity, Resource.Array.groups_array, Android.Resource.Layout.SimpleSpinnerItem
			);
			group_adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			groupSpinner.Adapter = group_adapter;

			genderSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (GenderSpinner_ItemSelected);
			var gender_adapter = ArrayAdapter.CreateFromResource (
				Activity, Resource.Array.gender_array, Android.Resource.Layout.SimpleSpinnerItem
			);
			gender_adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			genderSpinner.Adapter = group_adapter;


		}

		private void GroupSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner grpSpinner = (Spinner)sender;

			GroupId = grpSpinner.GetItemAtPosition (e.Position).ToString();
		}

		private void GenderSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner genderSpinner = (Spinner)sender;

			mGender = genderSpinner.GetItemAtPosition (e.Position).ToString ();
		}

		void MSubmitButton_Click (object sender, EventArgs e)
		{ 
			//api.api_create_new_user (mEmail.Text, mPassword.Text);   old call
		}
	}
}

