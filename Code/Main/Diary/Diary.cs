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
        TextView CalorieIntake;
        TextView CalorieDeficit;
        TextView MacroProtein;
        TextView MacroCarbs;
        TextView MacroFat;
        TextView BreakfastCalories;
        TextView LunchCalories;
        TextView DinnerCalories;
        string date;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            //PARENT ACTIVITY + FRAGMENT
            View view = inflater.Inflate(Resource.Layout.Diary, container, false);
            var activity = Activity as App;
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar_App).Visibility = ViewStates.Visible;
            activity.FindViewById<TextView>(Resource.Id.header_label).Text = "TODAY";


            //VIEWS
            CalorieGoal = view.FindViewById<TextView>(Resource.Id.TV_Goal_Calorie);
            CalorieIntake = view.FindViewById<TextView>(Resource.Id.TV_Intake_Calorie);
            CalorieDeficit = view.FindViewById<TextView>(Resource.Id.TV_Deficit_Calorie);
            MacroProtein = view.FindViewById<TextView>(Resource.Id.TV_Protein);
            MacroCarbs = view.FindViewById<TextView>(Resource.Id.TV_Carbs);
            MacroFat = view.FindViewById<TextView>(Resource.Id.TV_Fat);
            BreakfastCalories = view.FindViewById<TextView>(Resource.Id.TV_Calories_Breakfast);
            LunchCalories = view.FindViewById<TextView>(Resource.Id.TV_Calories_Lunch);
            DinnerCalories = view.FindViewById<TextView>(Resource.Id.TV_Calories_Dinner);

            //POPULATE VIEWS

            Dictionary<string, string> nutritionalData = DiaryControl.GetAllNutrition(activity.uname, activity.date);
            CalorieGoal.Text = activity.goal.ToString();
            CalorieIntake.Text = nutritionalData["totalCalories"];
            CalorieDeficit.Text = (activity.goal - float.Parse(nutritionalData["totalCalories"])).ToString("n2");
            MacroProtein.Text = nutritionalData["percentProtein"];
            MacroCarbs.Text = nutritionalData["percentCarbs"];
            MacroFat.Text = nutritionalData["percentFat"];
            BreakfastCalories.Text = nutritionalData["totalBreakfast"];
            LunchCalories.Text = nutritionalData["totalLunch"];
            DinnerCalories.Text = nutritionalData["totalDinner"];

            //BUTTONS

            

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
        public void RefreshData(App activity)
        {
            Dictionary<string, string> nutritionalData = DiaryControl.GetAllNutrition(activity.uname, activity.date);
            CalorieGoal.Text = activity.goal.ToString();
            CalorieIntake.Text = nutritionalData["totalCalories"];
            CalorieDeficit.Text = (activity.goal - float.Parse(nutritionalData["totalCalories"])).ToString("n2");
            MacroProtein.Text = nutritionalData["percentProtein"];
            MacroCarbs.Text = nutritionalData["percentCarbs"];
            MacroFat.Text = nutritionalData["percentFat"];
            BreakfastCalories.Text = nutritionalData["totalBreakfast"];
            LunchCalories.Text = nutritionalData["totalLunch"];
            DinnerCalories.Text = nutritionalData["totalDinner"];
        }
    }
    
}