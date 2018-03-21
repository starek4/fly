using FlyApi.Enums;
using FlyApi.Mappers;

namespace FlyApi.PostModels
{
    public class GetFavouritePostData : BasePostData
    {
        public GetFavouritePostData(string deviceId)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
        }
    }
}
