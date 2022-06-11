using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaloRead
{
    [Activity(Label = "App")]
    public class App : AppCompatActivity
    {
        TextView _dateDisplay;
        ImageButton CalendarBTN;

        public Diary diary;
        public Food food;
        public Profile profile;
        public Meal MealBreakfast;
        public Meal MealLunch;
        public Meal MealDinner;
        public AddFood addFood;

        public string uname = "asd";
        public string pword = "asd";
        public float weight = 0;
        public float height = 0;
        public float age = 0;
        public string gender = "";
        public float goal = 0;
        public string date;
        public float currProtein = 0;
        public float currCarbs = 0;
        public float currFat = 0;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustPan);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.App);

            //USER DATA
            date = DateTime.Now.ToString("yyyy-MM-dd");
            uname = Intent.GetStringExtra("uname");
            pword = Intent.GetStringExtra("pword");
            weight = Intent.GetFloatExtra("weight", 0);
            height = Intent.GetFloatExtra("height", 0);
            age = Intent.GetFloatExtra("age", 0);
            gender = Intent.GetStringExtra("gender");
            goal = Intent.GetFloatExtra("goal", 0);
            _dateDisplay = FindViewById<TextView>(Resource.Id.header_label);
            
            //INITIALIZE FRAGMENTS
            diary = new Diary();
            food = new Food();
            profile = new Profile();
            MealBreakfast = new Meal("breakfast");
            MealLunch = new Meal("lunch");
            MealDinner = new Meal("dinner");
            addFood = new AddFood();

            // INITIATE FRAGMENT
            ChangeFragment(diary);
            //BUTTONS
            var btnDiary = FindViewById<ImageButton>(Resource.Id.BTN_Diary);
            btnDiary.Click += (s, e) =>
            {
                ChangeFragment(diary);
            };
            var btnFood = FindViewById<ImageButton>(Resource.Id.BTN_Food);
            btnFood.Click += (s, e) =>
            {
                ChangeFragment(food);
            };
            var btnProfile = FindViewById<ImageButton>(Resource.Id.BTN_Profile);
            btnProfile.Click += (s, e) =>
            {
                ChangeFragment(profile);
            };

            CalendarBTN = FindViewById<ImageButton>(Resource.Id.BTN_Calendar_App);
            CalendarBTN.Click += (s, e) =>
            {

                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                    {
                        date = time.Date.ToString("yyyy-MM-dd");
                        if(date == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            _dateDisplay.Text = "TODAY";
                        }
                        else
                        {
                            _dateDisplay.Text = date;
                        }
                        

                        diary.RefreshData(this);
                    }
                );
                frag.Show(SupportFragmentManager, DatePickerFragment.TAG);
                
            };

        }
        public void ChangeFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            try
            {
                var fragmentTransaction = SupportFragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.fragment_container, fragment);
                fragmentTransaction.AddToBackStack(null);
                fragmentTransaction.Commit();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public void ShowMessage(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }
        public class DatePickerFragment : AndroidX.Fragment.App.DialogFragment,
                                  DatePickerDialog.IOnDateSetListener
        {
            // TAG can be any string of your choice.
            public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

            // Initialize this value to prevent NullReferenceExceptions.
            Action<DateTime> _dateSelectedHandler = delegate { };

            public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
            {
                DatePickerFragment frag = new DatePickerFragment();
                frag._dateSelectedHandler = onDateSelected;
                return frag;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                DateTime currently = DateTime.Now;
                DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                               this,
                                                               currently.Year,
                                                               currently.Month - 1,
                                                               currently.Day);
                return dialog;
            }

            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
                DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                Log.Debug(TAG, selectedDate.ToLongDateString());
                _dateSelectedHandler(selectedDate);
            }
        }
    }
}