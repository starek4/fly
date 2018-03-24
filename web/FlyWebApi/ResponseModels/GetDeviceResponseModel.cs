using DatabaseController.Models;

namespace FlyWebApi.ResponseModels
{
    public class GetDeviceResponseModel : BaseResponse
    {
        public Device Device { get; set; }
    }
}
