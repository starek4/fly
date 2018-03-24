using System;
using System.Collections.Generic;

namespace DatabaseController.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
