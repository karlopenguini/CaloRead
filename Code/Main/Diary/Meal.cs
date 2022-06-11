using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaloRead
{
    public class Meal : AndroidX.Fragment.App.Fragment
    {

        ImageButton goBack;
        RelativeLayout addMeal;

        public AddMeal _addMeal;
        public EditMeal _editMeal;
        public string TypeMeal;
        public string date;
        TextView CalorieGoal;
        TextView CalorieIntake;
        TextView CalorieDeficit;
        TextView MacroProtein;
        TextView MacroCarbs;
        TextView MacroFat;


        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        MealItemsAdapter mAdapter;
        MealItems mMealItems;

        public double MealCalories;

        public Meal(string _type)
        {
            TypeMeal = _type;
            _addMeal = new AddMeal(TypeMeal);
            
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            //PARENT ACTIVITY + FRAGMENT
            View view = inflater.Inflate(Resource.Layout.Meal, container, false);
            var activity = Activity as App;
            activity.FindViewById<LinearLayout>(Resource.Id.header).Visibility = ViewStates.Visible;
            activity.FindViewById<ImageButton>(Resource.Id.BTN_Calendar_App).Visibility = ViewStates.Invisible;
            activity.FindViewById<TextView>(Resource.Id.header_label).Text = TypeMeal;

            mMealItems = new MealItems(TypeMeal, activity.uname, activity.date);


            //NUTRITION
            CalorieGoal = view.FindViewById<TextView>(Resource.Id.TV_Goal_Calorie_Meal);
            CalorieIntake = view.FindViewById<TextView>(Resource.Id.TV_Intake_Calorie_Meal);
            CalorieDeficit = view.FindViewById<TextView>(Resource.Id.TV_Deficit_Calorie_Meal);
            MacroProtein = view.FindViewById<TextView>(Resource.Id.TV_Protein_Meal);
            MacroCarbs = view.FindViewById<TextView>(Resource.Id.TV_Carbs_Meal);
            MacroFat = view.FindViewById<TextView>(Resource.Id.TV_Fat_Meal);
            
            Dictionary<string, string> nutritionalData = DiaryControl.GetAllNutrition(activity.uname, activity.date);
            CalorieGoal.Text = activity.goal.ToString();
            CalorieIntake.Text = nutritionalData["totalCalories"];
            CalorieDeficit.Text = (activity.goal - float.Parse(nutritionalData["totalCalories"])).ToString("n2");
            MacroProtein.Text = nutritionalData["percentProtein"];
            MacroCarbs.Text = nutritionalData["percentCarbs"];
            MacroFat.Text = nutritionalData["percentFat"];

            //RECYCLER VIEW
            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.mealRecycler);
            mRecyclerView.NestedScrollingEnabled = false;
            // Linear Layout Manager:
            mLayoutManager = new LinearLayoutManager(activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            
            // Plug in my adapter:
            mAdapter = new MealItemsAdapter(mMealItems);
            mAdapter.ItemClick += (sender, position) =>
            {
                activity.ChangeFragment(new EditMeal(mMealItems[position].MealID));
            };
            mRecyclerView.SetAdapter(mAdapter);
            

            //BUTTONS
            goBack = view.FindViewById<ImageButton>(Resource.Id.BTN_GoBack_Meal);
            addMeal = view.FindViewById<RelativeLayout>(Resource.Id.LL_Add_Food_Meal);
            goBack.Click += (s, e) =>
            {
                activity.ChangeFragment(activity.diary);
            };
            addMeal.Click += (s, e) =>
            {
                activity.ChangeFragment(_addMeal);
            };


            return view;
        }
        public class MealItem
        {
            public MealItem(int _MealID, string _FoodName, float _Calories, float _TotalGrams, float _Protein, float _Carbs, float _Fat)
            {
                MealID = _MealID;
                FoodName = _FoodName;
                Calories = _Calories;
                TotalGrams = _TotalGrams;
                Protein = _Protein;
                Carbs = _Carbs;
                Fat = _Fat;
            }
            public int MealID { get; }
            public string FoodName { get; }
            public float Calories { get; }
            public float TotalGrams { get; }
            public float Protein { get; }
            public float Carbs { get; }
            public float Fat { get; }

        }
        
        public class MealItems
        {
            
            private MealItem[] MealCards;
            public MealItems(string typeMeal, string username, string date)
            {
                
                MealCards = MealControl.GetMeals(typeMeal, username, date);
            }
            public MealItem this[int i]
            {
                get { return MealCards[i]; }
            }
            public int NumMeals
            {
                get { return MealCards.Length; }
            }
        }

        public class MealItemHolder : RecyclerView.ViewHolder
        {
            public TextView FoodName { get; private set; }
            public TextView Calories { get; private set; }
            public TextView TotalGrams { get; private set; }
            public TextView Protein { get; private set; }
            public TextView Carbs { get; private set; }
            public TextView Fat { get; private set; }

            public MealItemHolder(View itemView, Action<int> listener) : base(itemView)
            {
                FoodName = itemView.FindViewById<TextView>(Resource.Id.TV_FoodName_Recycler);
                Calories = itemView.FindViewById<TextView>(Resource.Id.TV_kcal_Recycler);
                TotalGrams = itemView.FindViewById<TextView>(Resource.Id.TV_Grams_Recycler);
                Protein = itemView.FindViewById<TextView>(Resource.Id.TV_ProteinGrams_Recycler);
                Carbs = itemView.FindViewById<TextView>(Resource.Id.TV_CarbsGrams_Recycler);
                Fat = itemView.FindViewById<TextView>(Resource.Id.TV_FatGrams_Recycler);

                itemView.Click += (sender, e) => listener (base.LayoutPosition);
            }
        }

        public class MealItemsAdapter : RecyclerView.Adapter
        {
            public MealItems mMealItems;

            public MealItemsAdapter(MealItems mealItems)
            {
                mMealItems = mealItems;
            }

            public override int ItemCount
            {
                get { return mMealItems.NumMeals; }
            }

            void OnClick(int position)
            {
                if (ItemClick != null)
                {
                    ItemClick(this, position);
                }
                    
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                MealItemHolder vh = holder as MealItemHolder;

               
                vh.FoodName.Text = mMealItems[position].FoodName;
                vh.Calories.Text = mMealItems[position].Calories.ToString();
                vh.TotalGrams.Text = mMealItems[position].TotalGrams.ToString();
                vh.Protein.Text = mMealItems[position].Protein.ToString();
                vh.Carbs.Text = mMealItems[position].Carbs.ToString();
                vh.Fat.Text = mMealItems[position].Fat.ToString();
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                // Inflate the CardView for the photo:
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.FoodMealView, parent, false);

                // Create a ViewHolder to hold view references inside the CardView:
                MealItemHolder vh = new MealItemHolder(itemView, OnClick);
                return vh;
            }

            public event EventHandler<int> ItemClick;
        }
        
    }
}