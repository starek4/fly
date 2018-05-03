using System;
using DatabaseController.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.PostModels;
using Models.ResponseModels;
using Newtonsoft.Json;

namespace FlyWeb.Controllers.API
{
    [Route("api/[controller]")]
    public class VerifyUserController : Controller
    {
        private readonly UserRepository _userRepo = new UserRepository();
        private readonly DeviceRepository _deviceRepo = new DeviceRepository();

        [HttpPost]
        public string Post([FromBody] VerifyUserPostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new VerifyUserResponseModel { Error = "Wrong POST body.", Success = false, IsVerified = false });

            if (!_userRepo.VerifyUser(postModel.Login, postModel.Password))
                return JsonConvert.SerializeObject(new VerifyUserResponseModel { Error = String.Empty, Success = true, IsVerified = false });

            User user = _userRepo.GetUserByDeviceId(postModel.DeviceId);
            if (user != null && user.Login != postModel.Login)
            {
                if (_deviceRepo.ChangeOwnerOfDevice(postModel.Login, postModel.DeviceId))
                    return JsonConvert.SerializeObject(new VerifyUserResponseModel { Error = String.Empty, Success = true, IsVerified = true });
                return JsonConvert.SerializeObject(new VerifyUserResponseModel { Error = "Cannot change owner of the device.", Success = false, IsVerified = true });
            }
            return JsonConvert.SerializeObject(new VerifyUserResponseModel { Error = String.Empty, Success = true, IsVerified = true });
        }
    }
}
