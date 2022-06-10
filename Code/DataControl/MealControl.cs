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
    public class MealControl
    {
        public Meal.MealItem[] GetMeals(string type, string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.1.6/caloread/getmeal.php?type={type}&uname={username}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            using JsonDocument doc = JsonDocument.Parse(result);

            JsonElement root = doc.RootElement;

            var meals = root;

            List<Meal.MealItem> mealitems = new List<Meal.MealItem>();
            foreach(JsonElement meal in root.EnumerateArray())
            {
                string date = meal.GetProperty("date").ToString();
                int foodID = int.Parse(meal.GetProperty("foodID").ToString());
                int mealID = int.Parse(meal.GetProperty("mealID").ToString());
                float servings = float.Parse(meal.GetProperty("servings").ToString());
                string _type = meal.GetProperty("type").ToString();


                var foodDoc = GetFood(foodID);
                JsonElement foodRoot = foodDoc.RootElement;
                var food = foodRoot[0];

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

        private JsonDocument GetFood(int FoodID)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.1.6/caloread/searchfood.php?foodID={FoodID}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            JsonDocument doc = JsonDocument.Parse(result);

            return doc;
        }

        private class MealData
        {
            public string date { get; set; }
            public int foodID { get; set; }
            public int mealID { get; set; }
            public float servings { get; set; }
            public string type { get; set; }
        }
    }
}