using System;
using FlyPhone.iOS;
using Foundation;
using Security;

[assembly: Xamarin.Forms.Dependency(typeof(IosDevice))]
namespace FlyPhone.iOS
{
    public class IosDevice : IDevice
    {
        public string GetIdentifier()
        {
            var query = new SecRecord(SecKind.GenericPassword);
            query.Service = NSBundle.MainBundle.BundleIdentifier;
            query.Account = "UniqueID";

            NSData uniqueId = SecKeyChain.QueryAsData(query);
            if (uniqueId == null)
            {
                query.ValueData = NSData.FromString(Guid.NewGuid().ToString());
                SecKeyChain.Add(query);
                return query.ValueData.ToString();
            }
            return uniqueId.ToString();
        }
    }
}
