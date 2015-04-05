//
//  ActionBarDrawerToggler.cs
//
//  Author:
//       Faustino Lukolo <faustino.lukolo@wsu.edu>
//
//  Copyright (c) 2015 GPL
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Views.Accessibility;

namespace WearableSensorUI
{

    public class ActionBarDrawerEventArgs
    {
        public View DrawerView { get; set; }

        public float SlideOffset{ get; set; }

        public int NewState { get; set; }
    }

    public class MyActionBarDrawerToggle : Android.Support.V7.App.ActionBarDrawerToggle
    {
        private ActionBarActivity mHostActivity;
        private int mOpenedResource;
        private int mClosedResource;

        public delegate void ActionBarDrawerChangedEventHandler(object s,ActionBarDrawerEventArgs e);

        public MyActionBarDrawerToggle(ActionBarActivity host, DrawerLayout drawerLayout, int openedResourceDesc, int closedResourceDesc)
            : base(host, drawerLayout, openedResourceDesc, closedResourceDesc)
        {
            mHostActivity = host;
            mOpenedResource = openedResourceDesc;
            mClosedResource = closedResourceDesc;
        }


        // Overloaded with the new toolbar
        public MyActionBarDrawerToggle(ActionBarActivity host, DrawerLayout drawerLayout, Toolbar toolbar, int openedResourceDesc, int closedResourceDesc)
            : base(host, drawerLayout, toolbar, openedResourceDesc, closedResourceDesc)
        {
            mHostActivity = host;
            mOpenedResource = openedResourceDesc;
            mClosedResource = closedResourceDesc;
        }

        // Event delegates
        public event ActionBarDrawerChangedEventHandler DrawerClosed;
        public event ActionBarDrawerChangedEventHandler DrawerOpened;
        public event ActionBarDrawerChangedEventHandler DrawerSlide;
        public event ActionBarDrawerChangedEventHandler DrawerStateChanged;




        // Override base class methods
        public override void OnDrawerOpened(Android.Views.View drawerView)
        {
            // Proper way to trigger opened drawer event
            if (this.DrawerOpened != null)
                this.DrawerOpened(this, new ActionBarDrawerEventArgs { DrawerView = drawerView });

            base.OnDrawerOpened(drawerView);
         
        }

        public override void OnDrawerClosed(Android.Views.View drawerView)
        {
            if (this.DrawerClosed != null)
                this.DrawerClosed(this, new ActionBarDrawerEventArgs { DrawerView = drawerView });
            
            base.OnDrawerClosed(drawerView);
           
        }

        public override void OnDrawerStateChanged(int newState)
        {
            if (null != this.DrawerStateChanged)
                this.DrawerStateChanged(this, new ActionBarDrawerEventArgs{ NewState = newState });
        }

        public override void OnDrawerSlide(Android.Views.View drawerView, float slideOffset)
        {
            if (this.DrawerSlide != null)
                this.DrawerSlide(this, new ActionBarDrawerEventArgs{ DrawerView = drawerView, SlideOffset = slideOffset });
            

            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}

