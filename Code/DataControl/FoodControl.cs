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
        static string IP = "192.168.1.2";
        public static bool Add(ref string username, float kcal, float protein, float carbs, float fat, string name, float grams)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/addfood.php?uname={username}&kcal={kcal}&protein={protein}&carbs={carbs}&fat={fat}&name={name}&grams={grams}");
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/removefood.php?foodID={foodID}");
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

        public static bool Edit(ref int foodID, float kcal, float protein, float carbs, float fats, string name, float grams)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/editfood.php?foodID={foodID}&kcal={kcal}&protein={protein}&carbs={carbs}&fats={fats}&name={name}&grams={grams}");
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