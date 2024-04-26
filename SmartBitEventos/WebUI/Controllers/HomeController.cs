using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using WebUI.Models;
using WebUI.Utility;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LoadEvents()
        {
            HTTPRequest request = new HTTPRequest();
           
            var eventoSrc = request.Get("Event/GetEventsByUser");

            var eventos = JsonConvert.DeserializeObject<List<Evento>>(eventoSrc);
            return PartialView("~/Views/Home/Partials/EventList.cshtml", eventos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


        }
        public IActionResult LoadEventsProduct()
        {
            HTTPRequest request = new HTTPRequest();

            var eventoSrc = request.Get("Event/GetEventsByUser");

            var eventos = JsonConvert.DeserializeObject<List<Evento>>(eventoSrc);
            return PartialView("~/Views/Producto/Partials/EventList.cshtml", eventos);
        }
    }
}