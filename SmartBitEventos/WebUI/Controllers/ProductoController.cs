using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Producto()
        {
            return View();
        }
    }
}
