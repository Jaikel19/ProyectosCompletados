using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CelularController : Controller, IObserver
    {
        private readonly CelularManager _celularManager;

        public string NotificationMessage { get; private set; }

        public void Update(int message)
        {
            // Manejar la notificación, por ejemplo, loguear el mensaje
            if (message != 1)
            {
                // Filtrar por departamento si se proporciona

                Console.WriteLine($"CelularController ha recibido la notificación: {message}");
            }
            NotificationMessage = $"Notificación de dispositivos registrados diariamente. Cantidad Actual: {message} ";

            TempData["NotificationMessage"] = NotificationMessage;
        }

        [HttpGet("RealizarAccionEnManager")]
        public IActionResult RealizarAccionEnManager()
        {
            _celularManager.RealizarAccion();

            return RedirectToAction("GetCelulares");
        }

        [HttpGet("count")]
        public int ContarCelulares()
        {
            var count = _celularManager.ContarCelulares();
            return (int)count;
        }

        public IActionResult HomeCelular()
        {
            // Recupera el mensaje de TempData
            ViewBag.NotificationMessage = TempData["NotificationMessage"] as string;

            return View();
        }

        public CelularController(CelularManager celularManager)
        {
            _celularManager = celularManager;
            _celularManager.AgregarObserver(this);
            NotificationMessage = "no se han agregado hoy dispositivos";
        }

        [HttpGet]
        public ActionResult<IEnumerable<Celular>> GetCelulares()
        {
            var celulares = _celularManager.GetAllCelulares();
            return View("GetCelulares", celulares);
        }


        [HttpGet("GetCelulares")]
        public ActionResult<IEnumerable<Celular>> GetCelulares(string Localidad)
        {
            IEnumerable<Celular> celulares;

            if (!string.IsNullOrEmpty(Localidad))
            {
                // Filtrar por departamento si se proporciona
                celulares = _celularManager.GetCelularesByLocalidad(Localidad);
            }
            else
            {
                // Obtener todos los celulares si no se proporciona un departamento
                celulares = _celularManager.GetAllCelulares();
            }

            return View("GetCelulares", celulares);
        }

        [HttpGet("Edit/{id}")]
        public ActionResult<Celular> GetEdit(string id)
        {
            var celular = _celularManager.GetCelularById(id);

            if (celular == null)
            {
                return NotFound();
            }

            return View("EditCelular", celular);
        }

        [HttpPost("Edit/{id}")]
        public ActionResult PostEdit(string id, Celular editedCelular)
        {
            var existingCelular = _celularManager.GetCelularById(id);

            if (existingCelular == null)
            {
                return NotFound();
            }

            // Actualiza las propiedades del celular existente con los valores del formulario enviado
            existingCelular.Marca = editedCelular.Marca;
            existingCelular.Modelo = editedCelular.Modelo;
            existingCelular.SIM = editedCelular.SIM;
            existingCelular.Localidad = editedCelular.Localidad;

            // Luego, guarda los cambios en la base de datos o realiza las operaciones necesarias

            _celularManager.ActualizarCelular(id, editedCelular);
            // Redirige a la acción que muestra la lista de celulares después de la edición
            return RedirectToAction("GetCelulares");
        }


        [HttpGet("Registrar")]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost("Registrar")]
        public IActionResult Registrar([FromForm] Celular nuevoCelular)
        {
            try
            {
                _celularManager.RegistrarCelular(nuevoCelular);
                _celularManager.NotificarObservers(ContarCelulares());
                return RedirectToAction("GetCelulares");
            }
            catch (Exception ex)
            {
                // Manejo de errores: Puedes personalizar según tus necesidades
                ViewBag.ErrorMessage = $"Error al registrar el celular: {ex.Message}";
                return View();
            }
        }

        [HttpGet]
        public IActionResult EliminarCelular(string id)
        {
            try
            {
                _celularManager.EliminarCelular(id);
                return RedirectToAction("GetCelulares");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al eliminar el celular: {ex.Message}";
                return View("Eliminar", _celularManager.GetCelularById(id));
            }
        }

    }
}

