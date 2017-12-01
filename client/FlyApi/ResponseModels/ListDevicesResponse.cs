using System.Collections.Generic;

namespace FlyApi.ResponseModels
{
    public class ListDevicesResponse : BaseResponse
    {
        public List<Device> Devices { get; set; }
    }
}