using System.Collections.Generic;
using FlyApi.Enums;

namespace FlyApi.Mappers
{
    public static class ApiPathMapper
    {
        private static readonly Dictionary<ApiPaths, string> Mapper = new Dictionary<ApiPaths, string>
        {
            { ApiPaths.VerifyUserLogin, "verifyUserLogin.php" },
            { ApiPaths.AddDevice, "addDevice.php" },
            { ApiPaths.ClearShutdownPending, "clearShutdownPending.php" },
            { ApiPaths.GetDevices, "getDevices.php" },
            { ApiPaths.SetShutdownPending, "setShutdownPending.php" },
            { ApiPaths.GetShutdownPending, "getShutdownPending.php" },
            { ApiPaths.VerifyDeviceId, "verifyDeviceId.php" },
            { ApiPaths.DeleteDevice, "deleteDevice.php" },
            { ApiPaths.ClearLoggedState, "clearLoggedState.php" },
            { ApiPaths.SetLoggedState, "setLoggedState.php" },
            { ApiPaths.GetLoggedState, "getLoggedState.php" }
        };

        public static string GetPath(ApiPaths path)
        {
            return Mapper[path];
        }
    }
}
