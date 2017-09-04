using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class GetDevicesPostData : BasePostData
    {
        public GetDevicesPostData(string login)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.Login), login);
        }
    }
}
