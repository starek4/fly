using FlyApi.Enums;
using FlyApi.Mappers;

namespace FlyApi.PostModels
{
    public class GetActionPostData : BasePostData
    {
        public GetActionPostData(string deviceId, ApiAction action)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
            Data.Add(DataTypeMapper.GetPath(DataTypes.Action), action.ToString());
        }
    }
}
