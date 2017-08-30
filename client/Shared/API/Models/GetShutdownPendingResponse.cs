namespace Shared.API.Models
{
    public class GetShutdownPendingResponse : BaseResponse
    {
        public bool Shutdown { get; set; }
    }
}