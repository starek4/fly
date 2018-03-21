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
            { ApiPaths.GetDevices, "getDevices.php" },
            { ApiPaths.VerifyDeviceId, "verifyDeviceId.php" },
            { ApiPaths.DeleteDevice, "deleteDevice.php" },
            { ApiPaths.ClearLoggedState, "clearLoggedState.php" },
            { ApiPaths.SetLoggedState, "setLoggedState.php" },
            { ApiPaths.GetLoggedState, "getLoggedState.php" },
            { ApiPaths.SetAction, "Actions/setAction.php" },
            { ApiPaths.GetAction, "Actions/getAction.php" },
            { ApiPaths.ClearAction, "Actions/clearAction.php" },
            { ApiPaths.ClearFavourite, "clearFavourite.php" },
            { ApiPaths.SetFavourite, "setFavourite.php" },
            { ApiPaths.GetFavourite, "getFavourite.php" }
        };

        public static string GetPath(ApiPaths path)
        {
            return Mapper[path];
        }
    }
}
