using System;

namespace Shared.API.ResponseModels
{
    public class Device
    {
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime LastActive { get; set; }
    }
}