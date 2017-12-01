using System.Threading;
using FlyPhone.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplicationHandler))]
namespace FlyPhone.iOS
{
    public class CloseApplicationHandler : ICloseApplication
    {
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}