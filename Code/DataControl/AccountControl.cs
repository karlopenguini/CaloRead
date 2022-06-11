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
    public static class AccountControl
    {
        static string IP = "192.168.254.105";
        public static bool AuthenticateLogin(string uname, string pword, ref int age, ref float weight, ref float height, ref string gender, ref float goal)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/login.php?uname={uname}&pword={pword} ");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String res = reader.ReadToEnd();
            if (res.Contains("OK!"))
            {
                request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/getuser.php?uname={uname}");
                response = (HttpWebResponse)request.GetResponse();
                res = response.ProtocolVersion.ToString();
                reader = new StreamReader(response.GetResponseStream());
                var result = reader.ReadToEnd();
                using JsonDocument doc = JsonDocument.Parse(result);
                JsonElement root = doc.RootElement;

                var user = root[0];

                age = int.Parse(user.GetProperty("age").ToString());
                weight = float.Parse(user.GetProperty("weight").ToString());
                height = float.Parse(user.GetProperty("height").ToString());
                gender = user.GetProperty("gender").ToString();
                goal = float.Parse(user.GetProperty("calorie_goal").ToString());


                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Register(string uname, string pword, int age, float weight, float height, string gender, float goal)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/register.php?uname={uname}&pword={pword}&weight={weight}&height={height}&age={age}&gender={gender}&goal={goal}");
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

        public static bool Update(string uname, string pword, int age, float weight, float height, string gender, float goal)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/updateaccount.php?uname={uname}&pword={pword}&weight={weight}&height={height}&age={age}&gender={gender}&goal={goal}");
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

        public static Dictionary<string, string> GetUserData(string username)
        {
            var userRes = GetUser(username);
            using JsonDocument docFood = JsonDocument.Parse(userRes);
            JsonElement rootFood = docFood.RootElement;
            var user = rootFood[0];

            Dictionary<string, string> User = new Dictionary<string, string>()
            {
                ["password"] = user.GetProperty("password").ToString(),
                ["weight"] = user.GetProperty("weight").ToString(),
                ["height"] = user.GetProperty("height").ToString(),
                ["gender"] = user.GetProperty("gender").ToString(),
                ["age"] = user.GetProperty("age").ToString(),
                ["calorie_goal"] = user.GetProperty("calorie_goal").ToString(),
            };
            return User;
        }

        public static string GetUser(string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/searchuser.php?username={username}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            response.Close();

            return result;
        }

        public static bool IsExistUser(string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://{IP}/caloread/checkuser.php?username={username}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String res = reader.ReadToEnd();
            if (res.Contains("OK!"))
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