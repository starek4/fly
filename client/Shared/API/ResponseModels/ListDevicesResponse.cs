using System.Collections.Generic;

namespace Shared.API.ResponseModels
{
    public class ListDevicesResponse : BaseResponse
    {
        public List<Device> Devices { get; set; }
    }
}