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
    public static class FoodControl
    {
        public static Food.FoodItem[] GetFood(string username)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/getfood.php?uname={username}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String res = response.ProtocolVersion.ToString();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            response.Close();

            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            var meals = root;

            List<Food.FoodItem> fooditems = new List<Food.FoodItem>();

            foreach (JsonElement food in root.EnumerateArray())
            {
                int foodID = int.Parse(food.GetProperty("foodID").ToString());
                string foodname = food.GetProperty("foodname").ToString();
                float calories = float.Parse(food.GetProperty("calories").ToString());
                float totalgrams = float.Parse(food.GetProperty("grams").ToString());
                float protein = float.Parse(food.GetProperty("protein").ToString());
                float carbs = float.Parse(food.GetProperty("carbs").ToString());
                float fat = float.Parse(food.GetProperty("fat").ToString());


                fooditems.Add(new Food.FoodItem(foodID, foodname, calories, totalgrams, protein, carbs, fat));
            }
            return fooditems.ToArray();
        }

        public static bool Add(ref string username, float kcal, float protein, float carbs, float fat, string name, float grams)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/addfood.php?uname={username}&kcal={kcal}&protein={protein}&carbs={carbs}&fat={fat}&name={name}&grams={grams}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String res = reader.ReadToEnd();
            if (res.Contains("Data Inserted"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Remove(ref int foodID)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/removefood.php?foodID={foodID}");
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

        public static bool Edit(int foodID, float kcal, float protein, float carbs, float fats, string name, float grams)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.254.105/caloread/editfood.php?foodID={foodID}&kcal={kcal}&protein={protein}&carbs={carbs}&fats={fats}&name={name}&grams={grams}");
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