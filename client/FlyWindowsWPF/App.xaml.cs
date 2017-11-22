using System;
using System.Threading;

namespace FlyWindowsWPF
{
    public partial class App
    {
        static readonly Mutex Mutex = new Mutex(true, "SingleInstanceMutex");
        public App()
        {
            if (!Mutex.WaitOne(TimeSpan.Zero, true))
            {
                Shutdown();
            }
        }
    }
}
