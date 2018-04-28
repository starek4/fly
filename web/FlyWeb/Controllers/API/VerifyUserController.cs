using System;
using DatabaseController.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.PostModels;
using Models.ResponseModels;
using Newtonsoft.Json;

namespace FlyWeb.Controllers.API
{
    [Route("api/[controller]")]
    public class VerifyUserController : Controller
    {
        private readonly UserRepository _userRepo = new UserRepository();

        [HttpPost]
        public string Post([FromBody] VerifyUserPostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new VerifyUserResponseModel { Error = "Wrong POST body.", Success = false, IsVerified = false });

            if (!_userRepo.VerifyUser(postModel.Login, postModel.Password))
                return JsonConvert.SerializeObject(new VerifyUserResponseModel { Error = String.Empty, Success = true, IsVerified = false });

            return JsonConvert.SerializeObject(new VerifyUserResponseModel { Error = String.Empty, Success = true, IsVerified = true });
        }
    }
}
