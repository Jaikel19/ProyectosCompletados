using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using WebUI.Models;
using WebUI.Utility;


namespace WebUI.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult ReporteAdministrador()
        {
            return View();
        }
        public IActionResult ReporteAuditoria()
        {
            return View();
        }
        public IActionResult LoggedEvents()
        {
            return View();
        }
        public IActionResult ReporteBoletos()
        {
            var user = HttpContext.Session.GetLoggedUser();

            return View(user);
        }
        public IActionResult ReporteComprador()
        {
            var user = HttpContext.Session.GetLoggedUser();

            return View(user);
        }
        public IActionResult ReporteGanancias()
        {
            var user = HttpContext.Session.GetLoggedUser();

            return View(user);
        }
        public IActionResult ReporteGestor()
        {
            var user = HttpContext.Session.GetLoggedUser();

            return View(user);
        }
        public IActionResult ReporteRendimientoPersonal()
        {
            var user = HttpContext.Session.GetLoggedUser();

            return View(user);
        }
        public IActionResult FillGrid()
        {
            HTTPRequest request = new HTTPRequest();

            var eventoSrc = request.Get("Event/GetEventsByUser");
            
            var eventos = JsonConvert.DeserializeObject<List<Evento>>(eventoSrc);

            return Json(eventos);
        }
        public IActionResult FillGridByID(int userID)
        {
            HTTPRequest request = new HTTPRequest();

            var eventoSrc = request.Get($"Event/GetEventsByUser?IdUsuario={userID}");

            var eventos = JsonConvert.DeserializeObject<List<Evento>>(eventoSrc);

            return Json(eventos);
        }
        public IActionResult GetGananciasGestor(int userID)
        {
            HTTPRequest request = new HTTPRequest();

            var gananciasSrc = request.Get($"Gestor/GetGananciasGestor?IDUsuario={userID}");

            var ganancias = JsonConvert.DeserializeObject<List<GananciasGestor>>(gananciasSrc);

            return Json(ganancias);
        }
        public IActionResult GetGananciasAdmin()
        {
            HTTPRequest request = new HTTPRequest();

            var gananciasSrc = request.Get($"Dashboard/GetGananciasAdmin");

            var ganancias = JsonConvert.DeserializeObject<List<GananciasAdmin>>(gananciasSrc);

            return Json(ganancias);
        }
        public IActionResult FillGridAuditoria()
        {
            HTTPRequest request = new HTTPRequest();

            var auditoriaSrc = request.Get("Auditoria/GetAuditorias");


            var auditoria = JsonConvert.DeserializeObject<List<Auditoria>>(auditoriaSrc);

            return Json(auditoria);
        }
        public IActionResult FillGridBoletos(int userID)
        {
            HTTPRequest request = new HTTPRequest();

            var auditoriaSrc = request.Get($"BoletosAsignados/GetBoletosAsignados?IdUsuario={userID}");


            var auditoria = JsonConvert.DeserializeObject<List<BoletosAsignados>>(auditoriaSrc);

            return Json(auditoria);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
