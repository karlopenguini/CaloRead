using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Android.Content;

namespace CaloRead
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {
        EditText uname;
        EditText pword;
        public Intent intentRegistration;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);


            uname = FindViewById<EditText>(Resource.Id.ET_Username);
            pword = FindViewById<EditText>(Resource.Id.ET_Password);

            var btnSignIn = FindViewById<Button>(Resource.Id.BTN_SignIn);
            btnSignIn.Click += (s, e) =>
            {
                if (AccountControl.AuthenticateLogin(uname.Text, pword.Text))
                {
                    Intent intent = new Intent(this, typeof(App));
                    StartActivity(intent);
                } else
                {
                    Toast.MakeText(this, "Wrong Password or Username!", ToastLength.Short).Show();
                }
            };
            var btnRegister = FindViewById<Button>(Resource.Id.BTN_Register);
            btnRegister.Click += (s, e) =>
            {
                intentRegistration = new Intent(this, typeof(RegisterUserData));
                StartActivity(intentRegistration);
            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}