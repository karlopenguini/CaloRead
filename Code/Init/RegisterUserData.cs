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

        EditText uname;
        EditText pword;
        EditText weight;
        EditText height;
        EditText gender;
        EditText age;
        List<EditText> Fields = new List<EditText>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterUserData);
            // Create your application here

            uname = FindViewById<EditText>(Resource.Id.ET_Username_Register);
            pword = FindViewById<EditText>(Resource.Id.ET_Password_Register);
            weight = FindViewById<EditText>(Resource.Id.ET_Weight_Register);
            height = FindViewById<EditText>(Resource.Id.ET_Height_Register);
            gender = FindViewById<EditText>(Resource.Id.ET_Gender_Register);
            age = FindViewById<EditText>(Resource.Id.ET_Age_Register); 

            var btn = FindViewById<ImageButton>(Resource.Id.BTN_Next);
            btn.Click += (s, e) =>
            {
                CheckFields();

                if(Fields.Count == 0)
                {
                    Intent intent = new Intent(this, typeof(RegisterCalorieGoal));
                    intent.PutExtra("uname", uname.Text);
                    intent.PutExtra("pword", pword.Text);
                    intent.PutExtra("weight", float.Parse(weight.Text));
                    intent.PutExtra("height", float.Parse(height.Text));
                    intent.PutExtra("gender", gender.Text);
                    intent.PutExtra("age", int.Parse(age.Text));
                    StartActivity(intent);
                }
                else
                {
                    DisplayError();
                }
                
            };
        }

        protected void CheckFields()
        {
            Fields.Clear();

            if(uname.Text == "" || AccountControl.IsExistUser(uname.Text))
            {
                Fields.Add(uname);
            }

            if(pword.Text == "")
            {
                Fields.Add(pword);
            }

            if(weight.Text == "" || float.Parse(weight.Text) <= 0)
            {
                Fields.Add(weight);
            }

            if (height.Text == "" || float.Parse(height.Text) <= 0)
            {
                Fields.Add(height);
            }

            if (gender.Text == "")
            {
                Fields.Add(gender);
            }

            if (age.Text == "" || int.Parse(age.Text) <= 0)
            {
                Fields.Add(age);
            }
        }

        protected void DisplayError()
        {
            foreach(EditText field in Fields)
            {
                field.Error = "Please enter a valid value";
            }
        }
    }
}