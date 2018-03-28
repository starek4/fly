using System;
using DatabaseController.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.PostModels;
using Models.ResponseModels;
using Newtonsoft.Json;

namespace FlyWeb.Controllers.API
{
    [Route("api/[controller]")]
    public class AddUserController : Controller
    {
        private readonly UserRepository _userRepo = new UserRepository();

        [HttpPost]
        public string Post([FromBody] AddUserPostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new BaseResponse {Error = "Wrong POST body.", Success = false});

            if (!_userRepo.AddUser(postModel.Login, postModel.Password, postModel.Mail))
                return JsonConvert.SerializeObject(new BaseResponse { Error = "Cannot add user.", Success = false });

            return JsonConvert.SerializeObject(new BaseResponse { Error = String.Empty, Success = true });
        }
    }
}
