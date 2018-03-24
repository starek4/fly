using System;

namespace DatabaseController.Models
{
    public class Device
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DeviceId { get; set; }
        public string Name { get; set; }

        public bool IsShutdownPending { get; set; } = false;
        public bool IsRestartPending { get; set; } = false;
        public bool IsSleepPending { get; set; } = false;
        public bool IsMutePending { get; set; } = false;

        public DateTime LastActive { get; set; } = DateTime.Now;
        public bool IsLogged { get; set; } = false;
        public bool IsActionable { get; set; } = false;
        public bool IsFavourite { get; set; } = false;
    }
}
