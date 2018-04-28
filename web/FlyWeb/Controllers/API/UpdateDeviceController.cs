using System;
using DatabaseController.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.PostModels;
using Models.ResponseModels;
using Newtonsoft.Json;

namespace FlyWeb.Controllers.API
{
    [Route("api/[controller]")]
    public class UpdateDeviceController
    {
        private readonly DeviceRepository _deviceRepo = new DeviceRepository();

        [HttpPost]
        public string Post([FromBody] UpdateDevicePostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new BaseResponse { Error = "Wrong POST body.", Success = false });

            _deviceRepo.UpdateDevice(postModel.Device);
            return JsonConvert.SerializeObject(new BaseResponse { Error = String.Empty, Success = true });
        }
    }
}
