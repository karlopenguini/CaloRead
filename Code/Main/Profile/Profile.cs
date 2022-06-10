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
        public string username;
        private int age;

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
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Gone;

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

            // Hide Confirm and Cancel Image Buttons
            confirm.Visibility = ViewStates.Gone;
            cancel.Visibility = ViewStates.Gone;
            edit.Visibility = ViewStates.Visible;

            edit.Click += (s, e) =>
            {
                BTN_Edit_Profile_Click();
            };

            confirm.Click += (s, e) =>
            {
                if (AccountControl.Update(ref username, password.Text, ref age , float.Parse(weight.Text), float.Parse(height.Text), gender.Text, float.Parse(calorie_goal.Text)))
                {
                    activity.ShowMessage("Profile Updated!");
                    BTN_Cancel_Profile_Click();
                }
                else
                {
                    activity.ShowMessage("Unable to Update Profile!");
                }
            };

            cancel.Click += (s, e) =>
            {
                BTN_Cancel_Profile_Click();
            };

            return view;
        }

        private void BTN_Edit_Profile_Click()
        {
            // Unhide Confirm and Cancel Image Buttons and hide Edit Image button
            confirm.Visibility = ViewStates.Visible;
            cancel.Visibility = ViewStates.Visible;
            edit.Visibility = ViewStates.Gone;

            // Enable EditTexts
            password.Enabled = true;
            weight.Enabled = true;
            height.Enabled = true;
            gender.Enabled = true;
            calorie_goal.Enabled = true;
            bmr.Enabled = true;
        }

        private void BTN_Cancel_Profile_Click()
        {
            // Hide Confirm and Cancel Image Buttons
            confirm.Visibility = ViewStates.Gone;
            cancel.Visibility = ViewStates.Gone;
            edit.Visibility = ViewStates.Visible;

            // Disable EditTexts
            password.Enabled = false;
            weight.Enabled = false;
            height.Enabled = false;
            gender.Enabled = false;
            calorie_goal.Enabled = false;
            bmr.Enabled = false;

            LoadUserData();
        }

        private void LoadUserData()
        {
            // Populate EditTexts with user data
        }
    }
}