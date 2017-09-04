using Android.App;
using Android.OS;
using Android.Widget;
using Shared.API;

namespace FlyDroid.Activities
{
    [Activity(Label = "FlyDroid")]
    public class ShutdownActivity : Activity
    {
        private string deviceId;
        private Button shutdownButton;
        private readonly Client client = new Client();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Shutdown);

            deviceId = Intent.GetStringExtra("device_id");
            shutdownButton = FindViewById<Button>(Resource.Id.shutdown);
            shutdownButton.Click += delegate
            {
                shutdownButton.Enabled = false;
                client.SetShutdownPending(deviceId).Wait();
                Finish();
            };
        }
    }
}
