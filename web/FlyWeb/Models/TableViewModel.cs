using System.Collections.Generic;
using DatabaseController.Models;

namespace FlyWeb.Models
{
    public class TableViewModel
    {
        public List<Device> Devices { get; set; }
        public string Login { get; set; }
    }
}
