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
    public class Diary : AndroidX.Fragment.App.Fragment
    {


        LinearLayout viewBreakfast;
        LinearLayout viewLunch;
        LinearLayout viewDinner;
        TextView CalorieGoal;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var activity = Activity as App;
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.Diary, container, false);
            var activity = Activity as App;
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar).Visibility = ViewStates.Visible;
            activity.FindViewById<TextView>(Resource.Id.header_label).Text = "TODAY";

            viewBreakfast = view.FindViewById<LinearLayout>(Resource.Id.LL_Breakfast);
            viewBreakfast.Click += (s, e) =>
            {
                activity.ChangeFragment(activity.MealBreakfast);
            };
            viewLunch = view.FindViewById<LinearLayout>(Resource.Id.LL_Lunch);
            viewLunch.Click += (s, e) =>
            {
                activity.ChangeFragment(activity.MealLunch);
            };
            viewDinner = view.FindViewById<LinearLayout>(Resource.Id.LL_Dinner);
            viewDinner.Click += (s, e) =>
            {
                activity.ChangeFragment(activity.MealDinner);
            };

            return view;
        }
    }
}