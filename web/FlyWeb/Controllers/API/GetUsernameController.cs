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
    public class GetUsernameController : Controller
    {
        private readonly UserRepository _userRepo = new UserRepository();

        [HttpPost]
        public string Post([FromBody] GetUsernamePostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new GetUsernameResponseModel { Error = "Wrong POST body.", Success = false, Username = String.Empty });

            User user = _userRepo.GetUserByDeviceId(postModel.DeviceId);
            if (user == null)
                return JsonConvert.SerializeObject(new GetUsernameResponseModel { Error = "Cannot find user.", Success = false, Username = String.Empty });

            return JsonConvert.SerializeObject(new GetUsernameResponseModel { Error = String.Empty, Success = true, Username = user.Login });
        }
    }
}
