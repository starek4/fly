using System;
using DatabaseController.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.PostModels;
using Models.ResponseModels;
using Newtonsoft.Json;

namespace FlyWeb.Controllers.API
{
    [Route("api/[controller]")]
    public class AddDeviceController : Controller
    {
        private readonly DeviceRepository _deviceRepo = new DeviceRepository();

        [HttpPost]
        public string Post([FromBody] AddDevicePostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new BaseResponse { Error = "Wrong POST body.", Success = false });

            if (!_deviceRepo.AddDevice(postModel.Login, postModel.DeviceId, postModel.Name, postModel.Actionable))
                return JsonConvert.SerializeObject(new BaseResponse { Error = "Cannot add device.", Success = false });

            return JsonConvert.SerializeObject(new BaseResponse { Error = String.Empty, Success = true });
        }
    }
}
