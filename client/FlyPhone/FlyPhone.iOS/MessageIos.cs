using FlyPhone.iOS;
using GlobalToast;

[assembly: Xamarin.Forms.Dependency(typeof(MessageIos))]
namespace FlyPhone.iOS
{
    public class MessageIos : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.ShowToast(message).SetDuration(ToastDuration.Long);
        }
        public void ShortAlert(string message)
        {
            Toast.ShowToast(message).SetDuration(ToastDuration.Regular);
        }
    }
}