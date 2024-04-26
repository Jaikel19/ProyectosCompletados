using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebUI.Utility;

namespace WebUI.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Carrito(int IdBoleto)
        {
            HTTPRequest request = new HTTPRequest();
            var eventDetailsJson = request.Get(string.Format("event/DetallesBoleto?IdBoleto={0}", IdBoleto));
            List<DetallesBoleto> evento = JsonConvert.DeserializeObject<List<DetallesBoleto>>(eventDetailsJson);

            return View(evento);
        }
    }
}
