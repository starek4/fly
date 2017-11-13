using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class GetLoggedStatePostData : BasePostData
    {
        public GetLoggedStatePostData(string deviceId)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
        }
    }
}
