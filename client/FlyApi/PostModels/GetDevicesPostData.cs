using FlyApi.Enums;
using FlyApi.Mappers;

namespace FlyApi.PostModels
{
    public class GetDevicesPostData : BasePostData
    {
        public GetDevicesPostData(string login)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.Login), login);
        }
    }
}
