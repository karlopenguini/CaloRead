using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaloRead
{
    [Activity(Label = "App")]
    public class App : AppCompatActivity
    {
        TextView _dateDisplay;
        public Diary diary = new Diary();
        public Food food = new Food();
        public Profile profile = new Profile();
        public Meal MealBreakfast = new Meal("breakfast");
        public Meal MealLunch = new Meal("lunch");
        public Meal MealDinner = new Meal("dinner");
        public AddFood addFood = new AddFood();

        string uname = "";
        string pword = "";
        double weight = 0;
        double height = 0;
        double age = 0;
        string gender = "";
        double goal = 0;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustPan);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.App);

            // Create your application here

            //user data
            uname = Intent.GetStringExtra("uname");
            pword = Intent.GetStringExtra("pword");
            weight = Intent.GetDoubleExtra("weight", 0);
            height = Intent.GetDoubleExtra("height", 0);
            age = Intent.GetDoubleExtra("age", 0);
            gender = Intent.GetStringExtra("gender");
            goal = Intent.GetDoubleExtra("goal", 0);

            ChangeFragment(diary);

            //buttons
            var btnDiary = FindViewById<ImageButton>(Resource.Id.BTN_Diary);
            btnDiary.Click += (s, e) =>
            {
                ChangeFragment(diary);
            };
            var btnFood = FindViewById<ImageButton>(Resource.Id.BTN_Food);
            btnFood.Click += (s, e) =>
            {
                ChangeFragment(food);
            };
            var btnProfile = FindViewById<ImageButton>(Resource.Id.BTN_Profile);
            btnProfile.Click += (s, e) =>
            {
                ChangeFragment(profile);
            };


        }
        public void ChangeFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            var fragmentTransaction = SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.fragment_container, fragment);
            fragmentTransaction.AddToBackStack(null);
            fragmentTransaction.Commit();
        }


    }
}