﻿using Android.App;
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

namespace CaloRead
{
    public static class AccountControl
    {

        public static bool AuthenticateLogin(string uname, string pword)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.1.6/caloread/login.php?uname={uname}&pword={pword} ");
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

        public static void GetUserData(ref string uname, ref string pword, ref double age, ref double weight, ref double height, ref string gender, ref double goal)
        {

        }

        public static bool Register(string uname, string pword, double age, double weight, double height, string gender, double goal)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://192.168.1.6/caloread/register.php?uname={uname}&pword={pword}&weight={weight}&height={height}&age={age}&gender={gender}&goal={goal}");
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

    }
}