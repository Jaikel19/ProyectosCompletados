using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
