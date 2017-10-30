using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Shared.API;

namespace FlyDroid.Activities
{
    [Activity(Label = "Fly client", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private EditText _login;
        private EditText _password;
        private Button _loginButton;
        private TextView _status;
        readonly Client _client = new Client();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            _login = FindViewById<EditText>(Resource.Id.login);
            _password = FindViewById<EditText>(Resource.Id.password);
            _loginButton = FindViewById<Button>(Resource.Id.loginButton);
            _status = FindViewById<TextView>(Resource.Id.status);

            _loginButton.Click += delegate
            {
                _loginButton.Enabled = false;
                new Thread(VerifyUserLogin).Start();
            };
        }

        private async void VerifyUserLogin()
        {
            if (! await _client.VerifyUserLogin(_login.Text, _password.Text))
            {
                RunOnUiThread(() =>
                {
                    _loginButton.Enabled = true;
                    _status.Text = "Wrong username or password";
                });
            }
            else
            {
                RunOnUiThread(() =>
                {
                    _loginButton.Enabled = true;
                    _status.Text = String.Empty;
                    var intent = new Intent(this, typeof(TableActivity));
                    intent.PutExtra("login", _login.Text);
                    StartActivity(intent);
                });
            }
        }
    }
}
