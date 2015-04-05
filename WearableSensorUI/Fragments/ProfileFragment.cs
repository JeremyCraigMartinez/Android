

using Android.Views;
using WearableSensorUI;
using Android.OS;
using Android.Support.V4.App;


namespace WearableSensorUI
{
    public class ProfileFragment : Fragment
    {
        Fragment mUser;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
//            mUser = new Fragment();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_profile_page, container, false);
            // Create your fragment here
            return view;
        }

        public string Name
        {
            set;
            get;
        }

        public string DOB
        {
            set;
            get;
        }

        public string Email
        {
            set;
            get;
        }

    }
}

