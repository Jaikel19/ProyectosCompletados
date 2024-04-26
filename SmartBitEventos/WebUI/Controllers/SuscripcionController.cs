using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class SuscripcionController : Controller
    {
        public IActionResult Suscripcion()
        {
            return View();
        }
    }
}
