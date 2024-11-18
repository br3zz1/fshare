using FShare.Services;
using Microsoft.AspNetCore.Mvc;

namespace FShare.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userService.GetCurrentUser();

            ViewBag.Username = currentUser?.Username ?? "Not logged in.";

            return View();
        }
    }
}
