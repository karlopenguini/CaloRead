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
    public class EditFood : AndroidX.Fragment.App.Fragment
    {
        ImageButton goBack;
        EditText kcal, protein, carbs, fats, name, grams;
        Button update, remove;
        int foodID;

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

            // Instantiate Widgets
            goBack = view.FindViewById<ImageButton>(Resource.Id.BTN_GoBack_EditFood);
            kcal = view.FindViewById<EditText>(Resource.Id.ET_kcal_EditFood);
            protein = view.FindViewById<EditText>(Resource.Id.ET_Protein_EditFood);
            carbs = view.FindViewById<EditText>(Resource.Id.ET_Carbs_EditFood);
            fats = view.FindViewById<EditText>(Resource.Id.ET_Fats_EditFood);
            name = view.FindViewById<EditText>(Resource.Id.ET_FoodName_EditFood);
            grams = view.FindViewById<EditText>(Resource.Id.ET_Grams_EditFood);
            update = view.FindViewById<Button>(Resource.Id.BTN_Update_EditFood);
            remove = view.FindViewById<Button>(Resource.Id.BTN_Remove_EditFood);

            update.Click += (s, e) =>
            {
                if (FoodControl.Edit(ref foodID, float.Parse(kcal.Text), float.Parse(protein.Text), float.Parse(carbs.Text), float.Parse(fats.Text), name.Text, float.Parse(grams.Text)))
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
                if (FoodControl.Remove(ref foodID))
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
    }
}