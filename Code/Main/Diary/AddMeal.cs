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
    public class AddMeal : AndroidX.Fragment.App.Fragment
    {
        ImageButton goBack;
        private string Type;
        public AddMeal(string _type)
        {
            Type = _type;
            
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.AddMeal, container, false);
            var activity = Activity as App;
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar).Visibility = ViewStates.Invisible;
            activity.FindViewById<TextView>(Resource.Id.header_label).Text = Type;

            goBack = view.FindViewById<ImageButton>(Resource.Id.BTN_GoBack_Add_Meal);

            goBack.Click += (s, e) =>
            {
                switch (Type){
                    case "breakfast":
                        activity.ChangeFragment(activity.MealBreakfast);
                        break;
                    case "lunch":
                        activity.ChangeFragment(activity.MealLunch);
                        break;
                    case "dinner":
                        activity.ChangeFragment(activity.MealDinner);
                        break;
                }
            };
            
            return view;
        }
    }
}