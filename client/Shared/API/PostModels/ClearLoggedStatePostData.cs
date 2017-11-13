using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class ClearLoggedStatePostData : BasePostData
    {
        public ClearLoggedStatePostData(string deviceId)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
        }
    }
}
