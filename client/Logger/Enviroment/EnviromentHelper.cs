using System;
using System.Runtime.InteropServices;
using Logger.Logging;

namespace Logger.Enviroment
{
    public static class EnviromentHelper
    {
        public static PlatformType GetPlatformType()
        {
            if (RuntimeInformation.FrameworkDescription.Contains("Mono"))
                return PlatformType.Phone;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return PlatformType.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return PlatformType.Linux;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return PlatformType.Osx;
            return PlatformType.Unknown;
        }

        public static ILogger GetLogger()
        {
            var type = GetPlatformType();
            switch (type)
            {
                case PlatformType.Linux:
                    return new UnixLogger();
                case PlatformType.Windows:
                    return new WindowsLogger();
                case PlatformType.Osx:
                    return new UnixLogger();
                case PlatformType.Phone:
                    return new PhoneLogger();
                default:
                    throw new NotImplementedException("This type of device has no implemented logger...");
            }
        }
    }
}
