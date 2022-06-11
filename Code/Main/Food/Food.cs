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
    public class Food : AndroidX.Fragment.App.Fragment
    {
        RelativeLayout addFood;

        public AddFood _addFood;
        public EditFood _editFood;

        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        FoodItemsAdapter mAdapter;
        FoodItems mFoodItems;

        RelativeLayout food_add;
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
            View view = inflater.Inflate(Resource.Layout.Food, container, false);

            mFoodItems = new FoodItems(activity.uname);

            //RECYCLER VIEW
            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.foodRecycler);

            // Linear Layout Manager:
            mLayoutManager = new LinearLayoutManager(activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            // Plug in my adapter:
            mAdapter = new FoodItemsAdapter(mFoodItems);
            mAdapter.ItemClick += (sender, position) =>
            {
                activity.ChangeFragment(new EditFood(mFoodItems[position].FoodID));
            };
            mRecyclerView.SetAdapter(mAdapter);

            // BUTTONS
            food_add = view.FindViewById<RelativeLayout>(Resource.Id.LL_Add_Food);
            food_add.Click += (s, e) =>
            {
                activity.ChangeFragment(activity.addFood);
            };

            return view;
        }

        public class FoodItem
        {
            public FoodItem(int _FoodID, string _FoodName, float _Calories, float _TotalGrams, float _Protein, float _Carbs, float _Fat)
            {
                FoodID = _FoodID;
                FoodName = _FoodName;
                Calories = _Calories;
                TotalGrams = _TotalGrams;
                Protein = _Protein;
                Carbs = _Carbs;
                Fat = _Fat;
            }
            public int FoodID { get; }
            public string FoodName { get; }
            public float Calories { get; }
            public float TotalGrams { get; }
            public float Protein { get; }
            public float Carbs { get; }
            public float Fat { get; }

        }

        public class FoodItems
        {

            private FoodItem[] FoodCards;
            public FoodItems(string username)
            {
                FoodCards = FoodControl.GetFood(username);
            }
            public FoodItem this[int i]
            {
                get { return FoodCards[i]; }
            }
            public int NumMeals
            {
                get { return FoodCards.Length; }
            }
        }

        public class FoodItemHolder : RecyclerView.ViewHolder
        {
            public TextView FoodName { get; private set; }
            public TextView Calories { get; private set; }
            public TextView TotalGrams { get; private set; }
            public TextView Protein { get; private set; }
            public TextView Carbs { get; private set; }
            public TextView Fat { get; private set; }

            public FoodItemHolder(View itemView, Action<int> listener) : base(itemView)
            {
                FoodName = itemView.FindViewById<TextView>(Resource.Id.TV_FoodName_Recycler);
                Calories = itemView.FindViewById<TextView>(Resource.Id.TV_kcal_Recycler);
                TotalGrams = itemView.FindViewById<TextView>(Resource.Id.TV_Grams_Recycler);
                Protein = itemView.FindViewById<TextView>(Resource.Id.TV_ProteinGrams_Recycler);
                Carbs = itemView.FindViewById<TextView>(Resource.Id.TV_CarbsGrams_Recycler);
                Fat = itemView.FindViewById<TextView>(Resource.Id.TV_FatGrams_Recycler);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

        public class FoodItemsAdapter : RecyclerView.Adapter
        {
            public FoodItems mMealItems;

            public FoodItemsAdapter(FoodItems mealItems)
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
                FoodItemHolder vh = holder as FoodItemHolder;


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
                FoodItemHolder vh = new FoodItemHolder(itemView, OnClick);
                return vh;
            }

            public event EventHandler<int> ItemClick;
        }
    }
}