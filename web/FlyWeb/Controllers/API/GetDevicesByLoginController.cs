using System;
using System.Collections.Generic;
using DatabaseController.Models;
using DatabaseController.Repositories;
using FlyWebApi.PostModels;
using FlyWebApi.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlyWeb.Controllers.API
{
    [Route("api/[controller]")]
    public class GetDevicesByLoginController : Controller
    {
        private readonly DeviceRepository _deviceRepo = new DeviceRepository();

        [HttpPost]
        public string Post([FromBody] GetDevicesByLoginPostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new GetDevicesByLoginResponseModel { Error = "Wrong POST body.", Success = false });

            List<Device> devices = new List<Device>(_deviceRepo.GetDevicesByLogin(postModel.Login));
            return JsonConvert.SerializeObject(new GetDevicesByLoginResponseModel { Error = String.Empty, Success = true, Devices = devices});
        }
    }
}
