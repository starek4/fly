using System.Collections.Generic;
using DatabaseController.Models;

namespace FlyWebApi.ResponseModels
{
    public class GetDevicesByLoginResponseModel : BaseResponse
    {
        public List<Device> Devices { get; set; }
    }
}
