namespace FlyApi.ResponseModels
{
    public class GetLoggedStateResponse : BaseResponse
    {
        public bool Logged { get; set; }
        public string Login { get; set; }
    }
}