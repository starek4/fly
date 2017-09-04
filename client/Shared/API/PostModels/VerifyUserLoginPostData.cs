using Shared.API.Enums;
using Shared.API.Mappers;

namespace Shared.API.PostModels
{
    public class VerifyUserLoginPostData : BasePostData
    {
        public VerifyUserLoginPostData(string login, string password)
        {
            Data.Add(DataTypeMapper.GetPath(DataTypes.Login), login);
            Data.Add(DataTypeMapper.GetPath(DataTypes.Password), password);
        }
    }
}
