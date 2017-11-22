using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class AddDevicePostData : BasePostData
    {
        public AddDevicePostData(string deviceId, string login, string name, bool shutdownable)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.Login), login);
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
            Data.Add(DataTypeMapper.GetPath(DataTypes.Name), name);
            Data.Add(DataTypeMapper.GetPath(DataTypes.Shutdownable), shutdownable.ToString());
        }
    }
}
