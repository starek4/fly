using System;
using DatabaseController.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.PostModels;
using Models.ResponseModels;
using Newtonsoft.Json;

namespace FlyWeb.Controllers.API
{
    [Route("api/[controller]")]
    public class DeleteDeviceController
    {
        private readonly DeviceRepository _deviceRepo = new DeviceRepository();

        [HttpPost]
        public string Post([FromBody] DeleteDevicePostModel postModel)
        {
            if (postModel == null)
                return JsonConvert.SerializeObject(new BaseResponse { Error = "Wrong POST body.", Success = false });

            if (!_deviceRepo.DeleteDevice(postModel.DeviceId))
                return JsonConvert.SerializeObject(new BaseResponse { Error = "Cannot delete device.", Success = false });

            return JsonConvert.SerializeObject(new BaseResponse { Error = String.Empty, Success = true });
        }
    }
}
