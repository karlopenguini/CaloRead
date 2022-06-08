using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaloRead
{
    [Activity(Label = "Register2")]
    public class RegisterCalorieGoal : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterUserData);
            // Create your application here
            var btn = FindViewById<ImageButton>(Resource.Id.BTN_ConfirmRegistration);
            btn.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(App));
                StartActivity(intent);
            };
        }
    }
}