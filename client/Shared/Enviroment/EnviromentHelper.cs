using System.Runtime.InteropServices;
using Shared.Logging;

namespace Shared.Enviroment
{
    public static class EnviromentHelper
    {
        private static PlatformType GetPlatformType()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return PlatformType.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return PlatformType.Linux;
            if (RuntimeInformation.FrameworkDescription.Contains("Mono"))
                return PlatformType.Phone;
            return PlatformType.Unknown;
        }

        public static ILogger GetLogger()
        {
            if (GetPlatformType() == PlatformType.Windows)
                return new WindowsLogger();
            if (GetPlatformType() == PlatformType.Linux)
                return new LinuxLogger();
            if (GetPlatformType() == PlatformType.Phone)
                return new PhoneLogger();
            return null;
        }
    }
}
