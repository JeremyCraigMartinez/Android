
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Org.Apache.Http.Authentication;
using Android.Provider;
using Java.Lang;
using Android.Support.V7.App;

namespace WearableSensorUI
{
    [Activity(Label = "LoginActivity", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/DefaultAppTheme.Base")]			
    public class LoginActivity : Activity
    {
        private EditText mEmail = null;
        private EditText mPassword = null;
        private TextView mAttempts = null;
        private Button mLogin = null;
        private TextView continueAsGuest = null;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.login_activity);
            // Create your application here

            InitViews();
            mLogin.Click += Login;   

            // Init views           
            continueAsGuest.Click += (sender, e) =>
            {
                // Using Gues Account
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("", "Guest");
                StartActivity(intent);
            };
        }


        public void Login(object sender, EventArgs e)
        {
            // TODO: Implement Authentication here
            if (AuthenticateUser(mEmail.Text, mPassword.Text) == true)
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("", mEmail.Text);

                StartActivity(intent);                
            }

        }

        public bool AuthenticateUser(string email, string password)
        {
            return true;
            
        }

        public void InitViews()
        {          

            mEmail = FindViewById<EditText>(Resource.Id.textEmail);
            mPassword = FindViewById<EditText>(Resource.Id.Password);
            mLogin = FindViewById<Button>(Resource.Id.buttonLogIn);

            // Check to see if user wants to skip login
            continueAsGuest = FindViewById<TextView>(Resource.Id.button_skip);           
                       
        }

    }
}

