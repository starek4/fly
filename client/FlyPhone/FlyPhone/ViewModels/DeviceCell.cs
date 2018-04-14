namespace FlyPhone.ViewModels
{
    public class DeviceCell
    {
        public string DeviceId;
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsFavorite { get; set; }

        public bool IsNotActive => !IsActive;
        public bool IsNotFavorite => !IsFavorite;
    }
}