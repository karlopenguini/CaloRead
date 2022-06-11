using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;


namespace CaloRead
{
    public static class MealControl
    {
        public static Meal.MealItem[] GetMeals(string type, string username)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/getmeals.php?type={type}&uname={username}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String res = response.ProtocolVersion.ToString();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            response.Close();

            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            var meals = root;

            List<Meal.MealItem> mealitems = new List<Meal.MealItem>();

            foreach (JsonElement meal in root.EnumerateArray())
            {
                string date = meal.GetProperty("date").ToString();

                int foodID = int.Parse(meal.GetProperty("foodID").ToString());
                int mealID = int.Parse(meal.GetProperty("mealID").ToString());
                float servings = float.Parse(meal.GetProperty("servings").ToString());
                string _type = meal.GetProperty("type").ToString();

                var foodRes = _GetFood(foodID);
                using JsonDocument docFood = JsonDocument.Parse(foodRes);
                JsonElement rootFood = docFood.RootElement;


                var food = rootFood[0];
                string foodname = food.GetProperty("foodname").ToString();
                float calories = float.Parse(food.GetProperty("calories").ToString()) * servings;
                float totalgrams = float.Parse(food.GetProperty("grams").ToString()) * servings;
                float protein = float.Parse(food.GetProperty("protein").ToString()) * servings;
                float carbs = float.Parse(food.GetProperty("carbs").ToString()) * servings;
                float fat = float.Parse(food.GetProperty("fat").ToString()) * servings;


                mealitems.Add(new Meal.MealItem(mealID, foodname, calories, totalgrams, protein, carbs, fat));
            }
            return mealitems.ToArray();
        }

        private static string _GetFood(int foodID)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/searchfood.php?foodID={foodID}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            response.Close();

            return result;
        }

        private static string _GetMeal(int mealID)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/getmeal.php?mealID={mealID}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            response.Close();

            return result;
        }
        public static Dictionary<string, string> GetMeal(int mealID)
        {
            var mealRes = _GetMeal(mealID);
            using JsonDocument docMeal = JsonDocument.Parse(mealRes);
            JsonElement rootMeal = docMeal.RootElement;
            var meal = rootMeal[0];

            Dictionary<string, string> Meal = new Dictionary<string, string>()
            {
                ["foodID"] = meal.GetProperty("foodID").ToString(),
                ["username"] = meal.GetProperty("username").ToString(),
                ["type"] = meal.GetProperty("type").ToString(),
                ["servings"] = meal.GetProperty("servings").ToString(),
                ["date"] = meal.GetProperty("date").ToString(),
                ["mealID"] = meal.GetProperty("mealID").ToString(),
            };
            return Meal;
        }
        public static Dictionary<string, string> GetFood(int foodID)
        {
            var foodRes = _GetFood(foodID);
            using JsonDocument docFood = JsonDocument.Parse(foodRes);
            JsonElement rootFood = docFood.RootElement;
            var meal = rootFood[0];

            Dictionary<string, string> Food = new Dictionary<string, string>()
            {
                ["foodID"] = meal.GetProperty("foodID").ToString(),
                ["username"] = meal.GetProperty("username").ToString(),
                ["calories"] = meal.GetProperty("calories").ToString(),
                ["grams"] = meal.GetProperty("grams").ToString(),
                ["protein"] = meal.GetProperty("protein").ToString(),
                ["carbs"] = meal.GetProperty("carbs").ToString(),
                ["fat"] = meal.GetProperty("fat").ToString(),
                ["foodname"] = meal.GetProperty("foodname").ToString(),
            };
            return Food;
        }
        public static bool DeleteMeal(int mealID)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/removemeal.php?mealID={mealID}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String res = reader.ReadToEnd();
            if (res.Contains("Data Removed"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdateMeal(int mealID, float servings)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/editmeal.php?mealID={mealID}&servings={servings}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String res = reader.ReadToEnd();
            if (res.Contains("Data Updated"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
