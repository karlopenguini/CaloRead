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
        TextView CalorieGoal_Diary;


        public Diary diary;
        public Food food;
        public Profile profile;
        public Meal MealBreakfast;
        public Meal MealLunch;
        public Meal MealDinner;
        public AddFood addFood;

        public string uname = "asd";
        public string pword = "asd";
        public float weight = 0;
        public float height = 0;
        public float age = 0;
        public string gender = "";
        public float goal = 0;

        public float currProtein = 0;
        public float currCarbs = 0;
        public float currFat = 0;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustPan);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.App);




            //USER DATA
            uname = Intent.GetStringExtra("uname");
            pword = Intent.GetStringExtra("pword");
            weight = Intent.GetFloatExtra("weight", 0);
            height = Intent.GetFloatExtra("height", 0);
            age = Intent.GetFloatExtra("age", 0);
            gender = Intent.GetStringExtra("gender");
            goal = Intent.GetFloatExtra("goal", 0);

            //INITIALIZE FRAGMENTS
            diary = new Diary();
            food = new Food();
            profile = new Profile();
            MealBreakfast = new Meal("breakfast");
            MealLunch = new Meal("lunch");
            MealDinner = new Meal("dinner");
            addFood = new AddFood();

            // INIT FRAGMENT
            ChangeFragment(diary);
            //BUTTONS
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