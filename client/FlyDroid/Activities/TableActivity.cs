using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Shared.API;
using Shared.API.Models;

namespace FlyDroid.Activities
{
    [Activity(Label = "FlyDroid")]
    public class TableActivity : ListActivity
    {
        private Client client = new Client();
        private List<Device> devices = new List<Device>();
        private List<string> devicesNames = new List<string>();

        private string login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Table);
            ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, devicesNames);

            login = Intent.GetStringExtra("login");

            new Thread(GetDevices).Start();
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var device = devices[position];
            var intent = new Intent(this, typeof(ShutdownActivity));
            intent.PutExtra("device_id", device.DeviceId);
            StartActivity(intent);
        }

        private void GetDevices()
        {
            devices = new List<Device>(client.GetDevices(login));
            RunOnUiThread(() =>
            {
                foreach (Device device in devices)
                {
                    devicesNames.Clear();
                    devicesNames.Add(device.Name);
                    ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, devicesNames);
                }
            });
        }
    }
}
