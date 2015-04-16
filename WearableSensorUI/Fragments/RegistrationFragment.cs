
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
using api_interaction_kit;
using System.Net;
using System.IO;

namespace WearableSensorUI
{
    public class RegistrationFragment : Fragment
    {
        private const int server_port = 5024;
        private const string server_ip = "104.236.169.12";


        EditText mEmail = null;
        EditText mFirstName= null;
        EditText mLasstName= null;
        EditText mPassword=null;
        EditText mPasswordVerify = null;


        Button mSubmitButton = null;
        Button mCacelRegistration = null;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.registration_fragment, container, false);

            mEmail = view.FindViewById<EditText>(Resource.Id.email_registration_text);
            mFirstName = view.FindViewById<EditText>(Resource.Id.first_name_text);
            mLasstName = view.FindViewById<EditText>(Resource.Id.last_name_text);

            mPassword = view.FindViewById<EditText>(Resource.Id.password_registration);
            mPasswordVerify = view.FindViewById<EditText>(Resource.Id.password_verify_registration);

            mSubmitButton = view.FindViewById<Button>(Resource.Id.submit_registration);
            mCacelRegistration = view.FindViewById<Button>(Resource.Id.cancel_registration_button);


            mSubmitButton.Click += mButtonSubmitRegistration;


            return view;

        }

        void mButtonSubmitRegistration(object sender, EventArgs e)
        {
            // Add POST Request to server



        }

    

    }
}
