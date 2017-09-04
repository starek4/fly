using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class DeleteDevicePostData : BasePostData
    {
        public DeleteDevicePostData(string deviceId, string login)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
            Data.Add(DataTypeMapper.GetPath(DataTypes.Login), login);
        }
    }
}
