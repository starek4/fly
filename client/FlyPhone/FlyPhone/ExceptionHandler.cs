using Plugin.Toasts;
using Xamarin.Forms;

namespace FlyPhone
{
    public static class ExceptionHandler
    {
        public static void NetworkError()
        {
            MakeNotification("Network connection error", "Please check your network connection and do action again.");
        }

        public static void DatabaseError()
        {
            MakeNotification("Database error", "Problem with database connection or connection query.");
        }

        private static async void MakeNotification(string title, string description)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            var options = new NotificationOptions
            {
                Title = title,
                Description = description
            };

            await notificator.Notify(options);
        }
    }
}