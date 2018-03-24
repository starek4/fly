namespace FlyWebApi.PostModels
{
    public class AddDevicePostModel
    {
        public string Login { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public bool Actionable { get; set; }
    }
}
