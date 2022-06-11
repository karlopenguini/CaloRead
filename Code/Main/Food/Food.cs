using Android.App;
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
    public class Food : AndroidX.Fragment.App.Fragment
    {
        RelativeLayout food_add;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
            
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var activity = Activity as App;
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar_App).Visibility = ViewStates.Gone;

            
            

            View view = inflater.Inflate(Resource.Layout.Food, container, false);

            food_add = view.FindViewById<RelativeLayout>(Resource.Id.LL_Add_Food);

            food_add.Click += (s, e) =>
            {
                activity.ChangeFragment(activity.addFood);
            };
            return view;
        }
        
    }
}