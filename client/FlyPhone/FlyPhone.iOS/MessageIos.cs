using FlyPhone.iOS;
using GlobalToast;

[assembly: Xamarin.Forms.Dependency(typeof(MessageIos))]
namespace FlyPhone.iOS
{
    public class MessageIos : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.ShowToast("This is a message").SetDuration(ToastDuration.Long);
        }
        public void ShortAlert(string message)
        {
            Toast.ShowToast("This is a message").SetDuration(ToastDuration.Regular);
        }
    }
}