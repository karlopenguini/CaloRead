using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaloRead
{
    [Activity(Label = "Register1")]
    public class RegisterUserData : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterUserData);
            // Create your application here
            var btn = FindViewById<ImageButton>(Resource.Id.BTN_Next);
            btn.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(RegisterCalorieGoal));
                StartActivity(intent);
            };
        }
    }
}