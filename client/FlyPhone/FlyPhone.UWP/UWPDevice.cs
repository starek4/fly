using System;
using Windows.System.Profile;
using FlyPhone.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(UwpDevice))]
namespace FlyPhone.UWP
{
    public sealed class UwpDevice : IDevice
    {
        private static UwpDevice _instance;
        public static UwpDevice Instance => _instance ?? (_instance = new UwpDevice());

        private readonly string _id;

        private UwpDevice()
        {
            _id = GetId();
        }

        private static string GetId()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.System.Profile.HardwareIdentification"))
            {
                var token = HardwareIdentification.GetPackageSpecificToken(null);
                var hardwareId = token.Id;
                var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

                byte[] bytes = new byte[hardwareId.Length];
                dataReader.ReadBytes(bytes);

                return BitConverter.ToString(bytes).Replace("-", "");
            }

            throw new Exception("No api for getting device ID detected.");
        }


        public string GetIdentifier()
        {
            return _id;
        }
    }
}
