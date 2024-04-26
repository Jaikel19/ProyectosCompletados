using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Utility;
using System.Text.Json;
using WebUI.Utility;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using AppLogic;

namespace WebUI.Controllers
{
    public class EventsController : Controller
    {

        private IHostingEnvironment env;

        public EventsController(IHostingEnvironment _environment)
        {
            env = _environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EventList()
        {
            return View();
        }

        public IActionResult EventDetails(int eventId)
        {
            HTTPRequest request = new HTTPRequest();
            var eventDetailsJson = request.Get(string.Format("event/GetEventsByID?IdEvento={0}", eventId));
            List<Evento> evento = JsonConvert.DeserializeObject<List<Evento>>(eventDetailsJson);

            ViewBag.UsuarioLogueado = HttpContext.Session.GetIsUserLoggedIn();

            return View(evento.FirstOrDefault());
        }

        public IActionResult CreateEvent()
        {
            HTTPRequest request = new HTTPRequest();
            Evento evento = new Evento();

            string token = HttpContext.Session.GetToken();
            var boletos = request.Get("event/GetTipoBoleto", token);

            if (boletos != null)
            {
                evento.TipoBoletoEventos = JsonConvert.DeserializeObject<List<TipoBoletoEvento>>(boletos);
            }
            return View(evento);
        }

        [HttpPost]
        public IActionResult Event(Evento evento)
        {
            string token = HttpContext.Session.GetToken();
            Usuario loggedUser = HttpContext.Session.GetLoggedUser();
            evento.Direccion = $"{evento.Latitud},{evento.Longitud}";
            evento.IdUsuario = (int)loggedUser.Id;

            string imagen = ImagesUtilities.SaveFile(evento.Imagen, env.ContentRootPath, DTO.Enum.ImagesType.Event);
            evento.EventoImagen = imagen;

            evento.TipoBoletoEventos = JsonConvert.DeserializeObject<List<TipoBoletoEvento>>(evento.TipoBoletosStr);
            evento.Imagen = null;

            string newEventJson = JsonConvert.SerializeObject(evento, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            HTTPRequest request = new HTTPRequest();
            request.Post("Event/CreateEvento", newEventJson, token);

            return RedirectToAction("Index", "Home");

        }

        public ActionResult LoadEvents()
        {
            HTTPRequest request = new HTTPRequest();
            string token = HttpContext.Session.GetToken();
            Usuario loggedUser = HttpContext.Session.GetLoggedUser();
            var eventoSrc = request.Get("Event/GetEventsByUser?IdUsuario=" + loggedUser.Id, token);

            var eventos = JsonConvert.DeserializeObject<List<Evento>>(eventoSrc);
            return PartialView("~/Views/Events/Partials/GridEvents.cshtml", eventos);
        }

        public ActionResult MyEvents()
        {

            return View();
        }

        [HttpPost]
        public int ComprarBoleto(string asientosSolicitados, int eventoId, string tiposDeAsiento)
        {
            try
            {
                HTTPRequest request = new HTTPRequest();
                string token = HttpContext.Session.GetToken();
                Usuario loggedUser = HttpContext.Session.GetLoggedUser();
                Boleto boleto = new Boleto();
                boleto.IdEvento = eventoId;
                boleto.IdUsuario = (int)loggedUser.Id;
                boleto.Estado = false; 
                boleto.asientosPorBoleto = JsonConvert.DeserializeObject<List<AsientoPorUsuario>>(asientosSolicitados);
                boleto.TipoBoletosPorUsuario = JsonConvert.DeserializeObject<List<TipoBoletoPorUsuario>>(tiposDeAsiento);
                string newBoletoJson = JsonConvert.SerializeObject(boleto);
                var res = request.Post("Event/CreateBoletos", newBoletoJson);

                var nuevoBoleto = JsonConvert.DeserializeObject<NuevoBoleto>(res);

                return nuevoBoleto.Idboleto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        public IActionResult ValidateQR([FromBody] int boletoId)
        {
            HTTPRequest request = new HTTPRequest();
            Boleto boleto = new Boleto();
            boleto.Id = boletoId;
            string jsonContent = JsonConvert.SerializeObject(boleto);
            var result = request.Post($"Event/ChangeBoletoState", jsonContent);
            return Json(result);
        }
            public IActionResult GetDetallesBoleto(int boletoId)
        {
            HTTPRequest request = new HTTPRequest();

            var boletosSrc = request.Get($"Event/DetallesBoleto?IdBoleto={boletoId}");

            var boleto = JsonConvert.DeserializeObject<List<DetallesBoleto>>(boletosSrc);

            return Json(boleto);
        }
    }
}
