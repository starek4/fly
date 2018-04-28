using FlyPhone.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(MessageIos))]
namespace FlyPhone.iOS
{
    public class MessageIos : IMessage
    {
        const double LongDelay = 3.5;
        const double ShortDelay = 2.0;

        NSTimer _alertDelay;
        UIAlertController _alert;

        public void LongAlert(string message)
        {
            ShowAlert(message, LongDelay);
        }
        public void ShortAlert(string message)
        {
            ShowAlert(message, ShortDelay);
        }

        void ShowAlert(string message, double seconds)
        {
            _alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                DismissMessage();
            });
            _alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(_alert, true, null);
        }

        void DismissMessage()
        {
            _alert?.DismissViewController(true, null);
            _alertDelay?.Dispose();
        }
    }
}