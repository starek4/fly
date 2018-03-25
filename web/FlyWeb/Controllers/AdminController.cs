using Microsoft.AspNetCore.Mvc;

namespace FlyWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
