using System.Collections.Generic;
using FlyClientApi.Enums;

namespace FlyClientApi.Mappers
{
    public static class ApiPathMapper
    {
        private static readonly Dictionary<ApiPaths, string> Mapper = new Dictionary<ApiPaths, string>
        {
            { ApiPaths.AddUser, "Adduser" },
            { ApiPaths.VerifyUser, "VerifyUser" },
            { ApiPaths.AddDevice, "AddDevice" },
            { ApiPaths.DeleteDevice, "DeleteDevice" },
            { ApiPaths.GetDevice, "GetDevice" },
            { ApiPaths.GetDevicesByLogin, "GetDevicesByLogin" },
            { ApiPaths.UpdateDevice, "UpdateDevice" },
            { ApiPaths.GetUsername, "GetUsername" }
        };

        public static string GetPath(ApiPaths path)
        {
            return Mapper[path];
        }
    }
}
