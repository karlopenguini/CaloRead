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
    public class AddMeal : AndroidX.Fragment.App.Fragment
    {
        ImageButton goBack;
        Button Add;
        Spinner spinner;
        EditText Servings;
        EditText kcal;
        EditText protein;
        EditText carbs;
        EditText fat;
        private string Type;
        int selectedFoodID;
        List<KeyValuePair<string, int>> AvailableFood;
        public AddMeal(string _type)
        {
            Type = _type;

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
            View view = inflater.Inflate(Resource.Layout.AddMeal, container, false);
            var activity = Activity as App;
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar_App).Visibility = ViewStates.Gone;
            activity.FindViewById<TextView>(Resource.Id.header_label).Text = Type;

            //VIEWS
            kcal = view.FindViewById<EditText>(Resource.Id.ET_kcal_AddMeal);
            protein = view.FindViewById<EditText>(Resource.Id.ET_Protein_AddMeal);
            carbs = view.FindViewById<EditText>(Resource.Id.ET_Carbs_AddMeal);
            fat = view.FindViewById<EditText>(Resource.Id.ET_Fat_AddMeal);




            //SPINNER SELECT FOOD
            AvailableFood = MealControl.GetAvailableFood(activity.uname);
            List<string> FoodNames = new List<string>();
            foreach (var food in AvailableFood) FoodNames.Add(food.Key);

            spinner = view.FindViewById<Spinner>(Resource.Id.Select_Food);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var spinnerAdapter = new ArrayAdapter<string>(activity, Android.Resource.Layout.SimpleSpinnerItem, FoodNames);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = spinnerAdapter;

            //SERVINGS
            Servings = view.FindViewById<EditText>(Resource.Id.ET_Servings_AddMeal);
            Servings.Text = "1";
            Servings.EditorAction += (o, eventArgs) =>
            {
                if(int.Parse(Servings.Text) <= 0)
                {
                    Servings.Error = "Please enter a valid value";
                }
                else
                {
                    var actionId = eventArgs.ActionId;
                    if (actionId == Android.Views.InputMethods.ImeAction.Done)
                    {
                        HideKeyboard();
                        RefreshNutrition();
                    }
                }
            };
            //BUTTONS
            Add = view.FindViewById<Button>(Resource.Id.BTN_Add_AddMeal);
            Add.Click += (s, e) =>
            {
                if (MealControl.AddMeal(selectedFoodID, activity.uname, float.Parse(Servings.Text), Type, activity.date))
                {
                    switch (Type)
                    {
                        case "breakfast":

                            activity.ChangeFragment(activity.MealBreakfast);
                            break;
                        case "lunch":
                            activity.ChangeFragment(activity.MealLunch);
                            break;
                        case "dinner":
                            activity.ChangeFragment(activity.MealDinner);
                            break;
                    }
                }

            };


            goBack = view.FindViewById<ImageButton>(Resource.Id.BTN_GoBack_Add_Meal);
            goBack.Click += (s, e) =>
            {
                switch (Type)
                {
                    case "breakfast":
                        activity.ChangeFragment(activity.MealBreakfast);
                        break;
                    case "lunch":
                        activity.ChangeFragment(activity.MealLunch);
                        break;
                    case "dinner":
                        activity.ChangeFragment(activity.MealDinner);
                        break;
                }
            };

            return view;
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedFoodID = AvailableFood[e.Position].Value;
            RefreshNutrition();
        }
        private void RefreshNutrition()
        {
            Dictionary<string, string> selectedFood = MealControl.GetFood(selectedFoodID);
            float servings = float.Parse(Servings.Text.ToString());
            kcal.Text = (float.Parse(selectedFood["calories"].ToString()) * servings).ToString();
            protein.Text = (float.Parse(selectedFood["protein"].ToString()) * servings).ToString();
            carbs.Text = (float.Parse(selectedFood["carbs"].ToString()) * servings).ToString();
            fat.Text = (float.Parse(selectedFood["fat"].ToString()) * servings).ToString();
        }

        public override void OnStop()
        {
            HideKeyboard();
            base.OnStop();
        }

        private void HideKeyboard()
        {
            var activity = Activity as App;
            var imm = (Android.Views.InputMethods.InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(Servings.WindowToken, 0);
        }
    }
}