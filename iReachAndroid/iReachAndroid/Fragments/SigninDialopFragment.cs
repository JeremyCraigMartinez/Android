
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using iReachAndroid.Activities;


namespace iReachAndroid.Fragments
{
	public class SigninDialopFragment : DialogFragment
	{
		// Create an instance of the Api

		private EditText mTextEmail;
		private EditText mTextPassword;
		private Button mSubmitButton;
		private Button mCancelButton;


		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			// Create your fragment here

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			var ignored = base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.signin_dialog_fragment, container, false);

			initViews (view);

			mSubmitButton.Click += SubmitButton_Click;
			mCancelButton.Click += CancelButton_Click;

			return view;

		}

	
		void CancelButton_Click (object sender, EventArgs e)
		{
			Dismiss ();
		}

		void SubmitButton_Click (object sender, EventArgs e)
		{
			var email = mTextEmail.Text;
			var password = mTextPassword.Text;		
		}

		void initViews (View v)
		{
			mTextEmail = v.FindViewById<EditText> (Resource.Id.txtEmailSignin);
			mTextPassword = v.FindViewById<EditText> (Resource.Id.txtPasswordSignin);
			mCancelButton = v.FindViewById<Button> (Resource.Id.btnCancelSignin);
			mSubmitButton = v.FindViewById<Button> (Resource.Id.btnSubmitSignin);
		}


	}
}

