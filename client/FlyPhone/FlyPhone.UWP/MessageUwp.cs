using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using FlyPhone.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessageUwp))]
namespace FlyPhone.UWP
{
    public class MessageUwp : IMessage
    {

        private void pushNotification(string message)
        {
            // template to load for showing Toast Notification
            var xmlToastTemplate = "<toast launch=\"app-defined-string\">" +
                                   "<visual>" +
                                   "<binding template =\"ToastGeneric\">" +
                                   "<text>Fly client</text>" +
                                   "<text>" +
                                   message +
                                   "</text>" +
                                   "</binding>" +
                                   "</visual>" +
                                   "</toast>";

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlToastTemplate);

            var toastNotification = new ToastNotification(xmlDocument);

            var notification = ToastNotificationManager.CreateToastNotifier();
            notification.Show(toastNotification);
        }

        public void LongAlert(string message)
        {
            pushNotification(message);
        }

        public void ShortAlert(string message)
        {
            pushNotification(message);
        }


    }
}
