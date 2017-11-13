using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class SetLoggedStatePostData : BasePostData
    {
        public SetLoggedStatePostData(string deviceId)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
        }
    }
}
