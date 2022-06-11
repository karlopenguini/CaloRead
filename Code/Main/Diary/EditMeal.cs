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
    public class EditMeal : AndroidX.Fragment.App.Fragment
    {

        ImageButton goBack;
        EditText kcal;
        EditText protein;
        EditText carbs;
        EditText fat;
        EditText foodname;
        EditText servings;
        Button UpdateBTN;
        Button RemoveBTN;

        #region VARIABLES
        int FoodID = 0;
        int MealID = 0;
        string TypeMeal = "";

        private Dictionary<string, string> Meal;
        private Dictionary<string, string> Food;

        float Calories = 0;
        float Protein = 0;
        float Carbs = 0;
        float Fat = 0;
        string FoodName = "";
        float Servings = 0;
        #endregion

        public override void OnStop()
        {
            HideKeyboard();
            base.OnStop();
        }

        private void HideKeyboard()
        {
            var activity = Activity as App;
            var imm = (Android.Views.InputMethods.InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(servings.WindowToken, 0);
        }

        public EditMeal(int mealID)
        {
            MealID = mealID;
            Meal = MealControl.GetMeal(MealID);

        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //INITIALIZE STUFF
            View view = inflater.Inflate(Resource.Layout.EditMeal, container, false);
            var activity = Activity as App;
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar_App).Visibility = ViewStates.Gone;

            #region INITIALIZE VARIABLES
            FoodID = int.Parse(Meal["foodID"]);
            Servings = float.Parse(Meal["servings"]);
            TypeMeal = Meal["type"];
            Food = MealControl.GetFood(FoodID);
            Calories = float.Parse(Food["calories"]);
            Protein = float.Parse(Food["protein"]);
            Carbs = float.Parse(Food["carbs"]);
            Fat = float.Parse(Food["fat"]);
            FoodName = Food["foodname"];
            #endregion

            #region INITIALIZE VIEWS
            kcal = view.FindViewById<EditText>(Resource.Id.ET_kcal_EditMeal);
            protein = view.FindViewById<EditText>(Resource.Id.ET_Protein_EditMeal);
            carbs = view.FindViewById<EditText>(Resource.Id.ET_Carbs_EditMeal);
            fat = view.FindViewById<EditText>(Resource.Id.ET_Fats_EditMeal);
            foodname = view.FindViewById<EditText>(Resource.Id.ET_FoodName_EditMeal);
            servings = view.FindViewById<EditText>(Resource.Id.ET_Servings_EditMeal);
            #endregion

            #region POPULATE VIEWS
            RefreshData(Servings);
            servings.Text = Servings.ToString();
            #endregion

            //SERVINGS CONTROL

            servings.EditorAction += (o, eventArgs) =>
            {
                var actionId = eventArgs.ActionId;
                if (actionId == Android.Views.InputMethods.ImeAction.Done)
                {
                    HideKeyboard();
                    RefreshData(float.Parse(view.FindViewById<EditText>(Resource.Id.ET_Servings_EditMeal).Text.ToString()));
                }
            };

            //BUTTONS

            UpdateBTN = view.FindViewById<Button>(Resource.Id.BTN_Update_EditMeal);
            UpdateBTN.Click += (s, e) =>
            {
                MealControl.UpdateMeal(MealID, Servings);
            };
            RemoveBTN = view.FindViewById<Button>(Resource.Id.BTN_Remove_EditMeal);
            RemoveBTN.Click += (s, e) =>
            {
                if (MealControl.DeleteMeal(MealID))
                {
                    switch (TypeMeal)
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

            goBack = view.FindViewById<ImageButton>(Resource.Id.BTN_GoBack_EditMeal);
            goBack.Click += (s, e) =>
            {
                switch (TypeMeal)
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
        private void RefreshData(float _Servings)
        {
            if (_Servings != null)
            {
                Servings = _Servings;
                kcal.Text = (Calories * Servings).ToString();
                protein.Text = (Protein * Servings).ToString();
                carbs.Text = (Carbs * Servings).ToString();
                fat.Text = (Fat * Servings).ToString();
                foodname.Text = FoodName;
            }

        }
    }
}