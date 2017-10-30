using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Shared.API;
using Shared.API.ResponseModels;

namespace FlyDroid.Activities
{
    [Activity(Label = "FlyDroid")]
    public class TableActivity : ListActivity
    {
        private readonly Client _client = new Client();
        private List<Device> _devices = new List<Device>();
        private readonly List<string> _devicesNames = new List<string>();

        private string _login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Table);
            _login = Intent.GetStringExtra("login");
            new Thread(GetDevices).Start();
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var device = _devices[position];
            var intent = new Intent(this, typeof(ShutdownActivity));
            intent.PutExtra("device_id", device.DeviceId);
            StartActivity(intent);
        }

        private async void GetDevices()
        {
            _devices = new List<Device>(await _client.GetDevices(_login));
            _devicesNames.Clear();
            foreach (Device device in _devices)
            {
                _devicesNames.Add(device.Name);
            }
            RunOnUiThread(() =>
            {
                ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, _devicesNames);
            });
        }
    }
}
