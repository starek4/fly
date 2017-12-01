using FlyApi.Enums;
using FlyApi.Mappers;

namespace FlyApi.PostModels
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
