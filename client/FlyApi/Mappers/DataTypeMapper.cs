using System.Collections.Generic;
using FlyApi.Enums;

namespace FlyApi.Mappers
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
