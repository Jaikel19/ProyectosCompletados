using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CompraController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
