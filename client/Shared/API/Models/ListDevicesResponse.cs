using System.Collections.Generic;

namespace Shared.API.Models
{
    public class ListDevicesResponse : BaseResponse
    {
        public List<Device> Devices { get; set; }
    }
}