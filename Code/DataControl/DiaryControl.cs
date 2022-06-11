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
    static class DiaryControl
    {
        static string IP = "192.168.1.2";
        public static Dictionary<string, string> GetAllNutrition(string username, string date)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/getallnutrition.php?uname={username}&date={date}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String res = response.ProtocolVersion.ToString();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            response.Close();

            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            var mealnutrition = root;

            float totalCalories = 0;
            float percentProtein = 0;
            float percentCarbs = 0;
            float percentFat = 0;
            float totalBreakfast = 0;
            float totalLunch = 0;
            float totalDinner = 0;
            string type = "";

            foreach(JsonElement row in root.EnumerateArray())
            {
                float servings = float.Parse(row.GetProperty("servings").ToString());

                totalCalories += float.Parse(row.GetProperty("calories").ToString())*servings;

                percentProtein += float.Parse(row.GetProperty("protein").ToString()) * servings;
                percentCarbs += float.Parse(row.GetProperty("carbs").ToString()) * servings;
                percentFat += float.Parse(row.GetProperty("fat").ToString()) * servings;

                type = row.GetProperty("type").ToString();

                if (type == "breakfast")
                {
                    totalBreakfast += float.Parse(row.GetProperty("calories").ToString()) * servings;
                }
                else if(type == "lunch")
                {
                    totalLunch += float.Parse(row.GetProperty("calories").ToString()) * servings;
                }
                else if(type == "dinner")
                {
                    totalDinner += float.Parse(row.GetProperty("calories").ToString()) * servings;
                }
            }

            float sumMacros = percentProtein + percentCarbs + percentFat;

            percentProtein = (percentProtein / sumMacros)*100;
            percentCarbs = (percentCarbs / sumMacros)*100;
            percentFat = (percentFat / sumMacros)*100;


            Dictionary<string, string> Nutrition = new Dictionary<string, string>()
            {
                ["totalCalories"] = totalCalories.ToString("n2"),
                ["percentProtein"] = percentProtein.ToString("n2"),
                ["percentCarbs"] = percentCarbs.ToString("n2"),
                ["percentFat"] = percentFat.ToString("n2"),
                ["totalBreakfast"] = totalBreakfast.ToString("n2"),
                ["totalLunch"] = totalLunch.ToString("n2"),
                ["totalDinner"] = totalDinner.ToString("n2"),
            };

            return Nutrition;
        }
    }
}