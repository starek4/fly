using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class AddDevicePostData : BasePostData
    {
        public AddDevicePostData(string deviceId, string login, string name)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.Login), login);
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
            Data.Add(DataTypeMapper.GetPath(DataTypes.Name), name);
        }
    }
}
