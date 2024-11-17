using Microsoft.AspNetCore.Mvc;

namespace FShare.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
