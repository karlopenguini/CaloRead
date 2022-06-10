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
    public class AddFood : AndroidX.Fragment.App.Fragment
    {
        ImageButton goBack;
        EditText kcal, protein, carbs, fats, name, grams;
        Button add;

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
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar).Visibility = ViewStates.Gone;

            View view = inflater.Inflate(Resource.Layout.AddFood, container, false);

            // Instantiate Widgets
            goBack = view.FindViewById<ImageButton>(Resource.Id.BTN_GoBack_Add_Food);
            kcal = view.FindViewById<EditText>(Resource.Id.ET_kcal_AddFood);
            protein = view.FindViewById<EditText>(Resource.Id.ET_Protein_AddFood);
            carbs = view.FindViewById<EditText>(Resource.Id.ET_Carbs_AddFood);
            fats = view.FindViewById<EditText>(Resource.Id.ET_Fats_AddFood);
            name = view.FindViewById<EditText>(Resource.Id.ET_FoodName_AddFood);
            grams = view.FindViewById<EditText>(Resource.Id.ET_Grams_AddFood);
            add = view.FindViewById<Button>(Resource.Id.BTN_Add_AddFood);

            goBack.Click += (s, e) =>
            {
                activity.ChangeFragment(activity.food);

            };

            add.Click += (s, e) =>
            {
                if (FoodControl.Add(float.Parse(kcal.Text), float.Parse(protein.Text), float.Parse(carbs.Text), float.Parse(fats.Text), name.Text, float.Parse(grams.Text)))
                {
                    activity.ShowMessage("Food Added!");
                    activity.ChangeFragment(activity.food);
                }
                else
                {
                    activity.ShowMessage("Unable to Add Food!");
                }
            };

            return view;
        }
    }
}