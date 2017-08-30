using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Shared.API;

namespace FlyDroid.Activities
{
    [Activity(Label = "FlyDroid", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private EditText login;
        private EditText password;
        private Button loginButton;
        private TextView status;
        Client client = new Client();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            login = FindViewById<EditText>(Resource.Id.login);
            password = FindViewById<EditText>(Resource.Id.password);
            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            status = FindViewById<TextView>(Resource.Id.status);

            loginButton.Click += delegate
            {
                loginButton.Enabled = false;
                new Thread(VerifyUserLogin).Start();
            };
        }

        private async void VerifyUserLogin()
        {
            if (! await client.VerifyUserLogin(login.Text, password.Text))
            {
                RunOnUiThread(() =>
                {
                    loginButton.Enabled = true;
                    status.Text = "Wrong username or password";
                });
            }
            else
            {
                RunOnUiThread(() =>
                {
                    loginButton.Enabled = true;
                    status.Text = String.Empty;
                    var intent = new Intent(this, typeof(TableActivity));
                    intent.PutExtra("login", login.Text);
                    StartActivity(intent);
                });
            }
        }
    }
}
