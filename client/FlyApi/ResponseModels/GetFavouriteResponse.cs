namespace FlyApi.ResponseModels
{
    public class GetFavouriteResponse : BaseResponse
    {
        public bool Favourite { get; set; }
        public string Login { get; set; }
    }
}
