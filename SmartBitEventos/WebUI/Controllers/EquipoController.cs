using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
	public class EquipoController : Controller
	{
		public IActionResult Equipo()
		{
			return View();
		}
	}
}
