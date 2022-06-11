﻿using Android.App;
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
    public class EditFood : AndroidX.Fragment.App.Fragment
    {
        ImageButton goBack;
        EditText kcal, protein, carbs, fats, name, grams, fat, foodname;
        Button update, remove;

        #region VARIABLES
        int FoodID = 0;

        private Dictionary<string, string> Food;

        float Calories = 0;
        float Protein = 0;
        float Carbs = 0;
        float Fat = 0;
        string FoodName = "";
        float Grams = 0;
        #endregion

        public EditFood(int _FoodID)
        {
            FoodID = _FoodID;
            Food = FoodControl.GetFoodItem(FoodID);
        }

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
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar_App).Visibility = ViewStates.Gone;

            View view = inflater.Inflate(Resource.Layout.EditFood, container, false);

            //kcal = view.FindViewById<EditText>(Resource.Id.ET_kcal_EditFood);
            //protein = view.FindViewById<EditText>(Resource.Id.ET_Protein_EditFood);
            //carbs = view.FindViewById<EditText>(Resource.Id.ET_Carbs_EditFood);
            //fats = view.FindViewById<EditText>(Resource.Id.ET_Fats_EditFood);
            //name = view.FindViewById<EditText>(Resource.Id.ET_FoodName_EditFood);
            //grams = view.FindViewById<EditText>(Resource.Id.ET_Grams_EditFood);

            #region INITIALIZE VARIABLES
            FoodID = int.Parse(Food["foodID"]);
            Grams = float.Parse(Food["grams"]);
            Calories = float.Parse(Food["calories"]);
            Protein = float.Parse(Food["protein"]);
            Carbs = float.Parse(Food["carbs"]);
            Fat = float.Parse(Food["fat"]);
            FoodName = Food["foodname"];
            #endregion

            #region INITIALIZE VIEWS
            kcal = view.FindViewById<EditText>(Resource.Id.ET_kcal_EditFood);
            protein = view.FindViewById<EditText>(Resource.Id.ET_Protein_EditFood);
            carbs = view.FindViewById<EditText>(Resource.Id.ET_Carbs_EditFood);
            fat = view.FindViewById<EditText>(Resource.Id.ET_Fats_EditFood);
            foodname = view.FindViewById<EditText>(Resource.Id.ET_FoodName_EditFood);
            grams = view.FindViewById<EditText>(Resource.Id.ET_Grams_EditFood);
            #endregion

            #region POPULATE VIEWS
            RefreshData();
            #endregion

            goBack = view.FindViewById<ImageButton>(Resource.Id.BTN_GoBack_EditFood);
            update = view.FindViewById<Button>(Resource.Id.BTN_Update_EditFood);
            remove = view.FindViewById<Button>(Resource.Id.BTN_Remove_EditFood);

            goBack.Click += (s, e) =>
            {
                activity.ChangeFragment(activity.food);
            };

            update.Click += (s, e) =>
            {
                if (FoodControl.Edit(FoodID, float.Parse(kcal.Text), float.Parse(protein.Text), float.Parse(carbs.Text), float.Parse(fat.Text), foodname.Text, float.Parse(grams.Text)))
                {
                    activity.ShowMessage("Food Updated!");
                    activity.ChangeFragment(activity.food);
                }
                else
                {
                    activity.ShowMessage("Unable to Update Food!");
                }
            };

            remove.Click += (s, e) =>
            {
                if (FoodControl.Remove(FoodID))
                {
                    activity.ShowMessage("Food Removed!");
                    activity.ChangeFragment(activity.food);
                }
                else
                {
                    activity.ShowMessage("Unable to Remove Food!");
                }
            };

            return view;
        }

        private void RefreshData()
        {
            kcal.Text = Calories.ToString();
            protein.Text = Protein.ToString();
            carbs.Text = Carbs.ToString();
            fat.Text = Fat.ToString();
            foodname.Text = FoodName;
            grams.Text = Grams.ToString();
        }
    }
}