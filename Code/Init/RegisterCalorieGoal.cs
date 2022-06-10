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
    [Activity(Label = "Register2")]
    public class RegisterCalorieGoal : AppCompatActivity
    {
        EditText goal;
        TextView tv_goal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterCalorieGoal);
            // Create your application here


            string uname = Intent.GetStringExtra("uname");
            string pword = Intent.GetStringExtra("pword");
            double weight = Intent.GetDoubleExtra("weight", 0);
            double height = Intent.GetDoubleExtra("height", 0);
            double age = Intent.GetDoubleExtra("age", 0);
            string gender = Intent.GetStringExtra("gender");
            goal = FindViewById<EditText>(Resource.Id.ET_CalorieGoal);
            double bmr = 0;

            switch (gender)
            {
                case "MALE":
                    bmr = (10 * weight) + (6.25 * height) - (5 * age) + 5;
                    break;
                case "FEMALE":
                    bmr = (10 * weight) + (6.25 * height) - (5 * age) - 161;
                    break;
            }

            tv_goal = FindViewById<TextView>(Resource.Id.TV_Calorie_Goal_Register);
            tv_goal.Text = bmr + " kcal";

            var register = FindViewById<ImageButton>(Resource.Id.BTN_ConfirmRegistration);
            register.Click += (s, e) =>
            {

                double _goal = double.Parse(goal.Text);

                if (AccountControl.Register(uname, pword, age, weight, height, gender, _goal, bmr))
                {
                    Toast.MakeText(this, "Account Created!", ToastLength.Short).Show();

                    Intent intent = new Intent(this, typeof(App));
                    intent.PutExtra("uname", uname);
                    intent.PutExtra("pword", pword);
                    intent.PutExtra("weight", weight);
                    intent.PutExtra("height", height);
                    intent.PutExtra("gender", gender);
                    intent.PutExtra("age", age);
                    StartActivity(intent);
                }

            };
            var back = FindViewById<ImageButton>(Resource.Id.BTN_GoBack);
            back.Click += (s, e) =>
            {
                Intent intent = new Intent(this, typeof(RegisterUserData));
                StartActivity(intent);
            };
        }
    }
}