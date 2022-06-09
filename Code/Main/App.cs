﻿using Android.App;
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
        public int year, month, date;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.App);
            // Create your application here
            var diary = new Diary();
            var food = new Food();
            var profile = new Profile();
            ChangeFragment(diary);

            _dateDisplay = FindViewById<TextView>(Resource.Id.header_label);

            var btnCalendar = FindViewById<ImageButton>(Resource.Id.BTN_Calendar);
            btnCalendar.Click += delegate
            {
                
            };


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