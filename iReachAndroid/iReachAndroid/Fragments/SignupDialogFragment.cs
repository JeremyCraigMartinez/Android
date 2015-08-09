
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

namespace iReachAndroid
{
	public class SignupDialogFragment : Android.Support.V4.App.DialogFragment
	{

		private EditText mName;
		private EditText mEmail;
		private EditText mPassword;

		private Button mCancelButton;
		private Button mSubmitButton;

		private api_interaction_kit.api api;

		public override void OnCreate (Bundle savedInstanceState)
		{
			api = (api_interaction_kit.api)Android.App.Application.Context;
			api.server_update += Api_server_update;
			base.OnCreate (savedInstanceState);
			// Create your fragment here
		}

		void Api_server_update (object o, api_interaction_kit.Response_Type r)
		{
			if (r == api_interaction_kit.Response_Type.user_created) {
				if ((bool)o == true) {
					//do something
				}
			}
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			var view = inflater.Inflate (Resource.Layout.signup_dialog_fragment, container, false);
			mName = view.FindViewById<EditText> (Resource.Id.txtName);
			mEmail = view.FindViewById<EditText> (Resource.Id.txtEmail);
			mPassword = view.FindViewById<EditText> (Resource.Id.txtPassword);

			mCancelButton = view.FindViewById<Button> (Resource.Id.btnCancel);
			mSubmitButton = view.FindViewById<Button> (Resource.Id.btnSubmit);


			mCancelButton.Click += (sender, e) => Dismiss ();
			mSubmitButton.Click += MSubmitButton_Click;
			return view;


		}

		void MSubmitButton_Click (object sender, EventArgs e)
		{
			api.api_create_new_user (mEmail, mPassword);
		}
	}
}

