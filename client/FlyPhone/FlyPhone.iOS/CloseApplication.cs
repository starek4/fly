using System.Threading;
using FlyPhone.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace FlyPhone.iOS
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}