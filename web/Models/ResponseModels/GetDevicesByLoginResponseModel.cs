using System.Collections.Generic;

namespace Models.ResponseModels
{
    public class GetDevicesByLoginResponseModel : BaseResponse
    {
        public List<Device> Devices { get; set; }
    }
}
