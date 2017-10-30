using Android.App;
using Android.OS;
using Android.Widget;
using Shared.API;

namespace FlyDroid.Activities
{
    [Activity(Label = "FlyDroid")]
    public class ShutdownActivity : Activity
    {
        private string _deviceId;
        private Button _shutdownButton;
        private readonly Client _client = new Client();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Shutdown);

            _deviceId = Intent.GetStringExtra("device_id");
            _shutdownButton = FindViewById<Button>(Resource.Id.shutdown);
            _shutdownButton.Click += delegate
            {
                _shutdownButton.Enabled = false;
                _client.SetShutdownPending(_deviceId).Wait();
                Finish();
            };
        }
    }
}
