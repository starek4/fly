using System.Collections.Generic;
using Shared.API.Enums;

namespace Shared.API.Mappers
{
    public class DataTypeMapper
    {
        private static readonly Dictionary<DataTypes, string> Mapper = new Dictionary<DataTypes, string>
        {
            { DataTypes.Login, "Login" },
            { DataTypes.Password, "Password" },
            { DataTypes.DeviceId, "Device_id" },
            { DataTypes.Name, "Name" },
            { DataTypes.Shutdownable, "Shutdownable" }
        };

        public static string GetPath(DataTypes path)
        {
            return Mapper[path];
        }
    }
}
