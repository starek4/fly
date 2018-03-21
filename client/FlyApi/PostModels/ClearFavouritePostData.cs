using FlyApi.Enums;
using FlyApi.Mappers;

namespace FlyApi.PostModels
{
    public class ClearFavouritePostData : BasePostData
    {
        public ClearFavouritePostData(string deviceId)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
        }
    }
}
