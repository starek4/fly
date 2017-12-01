using FlyApi.Enums;
using FlyApi.Mappers;

namespace FlyApi.PostModels
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
