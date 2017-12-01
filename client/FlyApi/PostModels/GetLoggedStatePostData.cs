using FlyApi.Enums;
using FlyApi.Mappers;

namespace FlyApi.PostModels
{
    public class GetLoggedStatePostData : BasePostData
    {
        public GetLoggedStatePostData(string deviceId)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
        }
    }
}
