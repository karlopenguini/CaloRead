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

        public static bool Add(float kcal, float protein, float carbs, float fats, string name, float grams)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.1.6/caloread/addfood.php?kcal={kcal}&protein={protein}&carbs={carbs}&fats={fats}&name={name}&grams={grams}");
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

        public static bool Remove(float kcal, float protein, float carbs, float fats, string name, float grams)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.1.6/caloread/removefood.php?kcal={kcal}&protein={protein}&carbs={carbs}&fats={fats}&name={name}&grams={grams}");
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

        public static bool Edit(float kcal, float protein, float carbs, float fats, string name, float grams)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.1.6/caloread/updatefood.php?kcal={kcal}&protein={protein}&carbs={carbs}&fats={fats}&name={name}&grams={grams}");
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