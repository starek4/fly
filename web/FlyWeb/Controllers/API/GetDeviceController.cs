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
    public class GetDeviceController : Controller
    {
        private readonly DeviceRepository _deviceRepo = new DeviceRepository();

        [HttpPost]
        public string Post([FromBody] GetDevicePostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new GetDeviceResponseModel { Error = "Wrong POST body.", Success = false });

            Device device = _deviceRepo.GetDevice(postModel.DeviceId);
            if (device == null)
                return JsonConvert.SerializeObject(new GetDeviceResponseModel { Error = String.Empty, Success = true });

            return JsonConvert.SerializeObject(new GetDeviceResponseModel { Error = String.Empty, Success = true, Device = device});
        }
    }
}
