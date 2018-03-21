using FlyApi.Enums;
using FlyApi.Mappers;

namespace FlyApi.PostModels
{
    public class SetFavouritePostData : BasePostData
    {
        public SetFavouritePostData(string deviceId)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.DeviceId), deviceId);
        }
    }
}
