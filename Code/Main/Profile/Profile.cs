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
    public class Profile : AndroidX.Fragment.App.Fragment
    {
        EditText password, weight, height, gender, calorie_goal, bmr;
        ImageButton edit, confirm, cancel;

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
            activty.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Gone;

            View view = inflater.Inflate(Resource.Layout.Profile, container, false);

            // Instantiate Widgets
            password = view.FindViewById<EditText>(Resource.Id.ET_Password_Profile);
            weight = view.FindViewById<EditText>(Resource.Id.ET_Weight_Profile);
            height = view.FindViewById<EditText>(Resource.Id.ET_Height_Profile);
            gender = view.FindViewById<EditText>(Resource.Id.ET_Gender_Profile);
            calorie_goal = view.FindViewById<EditText>(Resource.Id.ET_CalorieGoal_Profile);
            bmr = view.FindViewById<EditText>(Resource.Id.ET_BMI_Profile);
            edit = view.FindViewById<ImageButton>(Resource.Id.BTN_Edit_Profile);
            confirm = view.FindViewById<ImageButton>(Resource.Id.BTN_Confirm_Profile);
            cancel = view.FindViewById<ImageButton>(Resource.Id.BTN_Cancel_Profile);

            return view;
        }

    }
}