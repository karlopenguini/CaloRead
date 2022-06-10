﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaloRead
{
    public class EditFood : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var activty = Activity as App;
            activty.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activty.FindViewById<ImageButton>(Resource.Id.BTN_Calendar).Visibility = ViewStates.Gone;

            View view = inflater.Inflate(Resource.Layout.EditFood, container, false);
            return view;
        }
    }
}