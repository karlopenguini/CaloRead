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
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar).Visibility = ViewStates.Visible;
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

        public void RefreshNutrionStats(App activity)
        {
            CalorieGoal.Text = activity.goal.ToString();
            MacroFat.Text = activity.currFat.ToString();
            MacroCarbs.Text = activity.currCarbs.ToString();
            MacroProtein.Text = activity.currProtein.ToString();
        }
        public void RefreshMealCalories(App activity)
        {
            BreakfastCalories.Text = $"{activity.MealBreakfast.MealCalories} KCAL";
            LunchCalories.Text = $"{activity.MealLunch.MealCalories} KCAL";
            DinnerCalories.Text = $"{activity.MealDinner.MealCalories} KCAL";
        }
    }
}