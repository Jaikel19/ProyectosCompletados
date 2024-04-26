using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Utility;

namespace WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult AdminDashboard()
        {

            HTTPRequest request = new HTTPRequest();

            var dashboardSrc = request.Get($"Dashboard/GetDashboard");


            var dashboard = JsonConvert.DeserializeObject<List<Dashboard>>(dashboardSrc);

            return View(dashboard);
        }

        public IActionResult CompradorDashboard()
        {
            var user = HttpContext.Session.GetLoggedUser();

            HTTPRequest request = new HTTPRequest();

            var dashboardSrc = request.Get($"Dashboard/GetDashboard?IdUsuario={user.Id}");


            var dashboard = JsonConvert.DeserializeObject<List<Dashboard>>(dashboardSrc);

            return View(dashboard);
        }

        public IActionResult GestorDashboard()
        {
            var user = HttpContext.Session.GetLoggedUser();

            HTTPRequest request = new HTTPRequest();

            var dashboardSrc = request.Get($"Dashboard/GetDashboard?IdUsuario={user.Id}");


            var dashboard = JsonConvert.DeserializeObject<List<Dashboard>>(dashboardSrc);

            return View(dashboard);
        }
    }
}
