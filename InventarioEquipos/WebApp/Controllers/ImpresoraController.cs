using AppLogic;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ImpresoraController : Controller
    {
        private readonly ImpresoraManager _impresoraManager;

        public ImpresoraController(ImpresoraManager impresoraManager)
        {
            _impresoraManager = impresoraManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Impresora>> GetImpresoras()
        {
            var impresoras = _impresoraManager.GetAllImpresoras();
            return View("GetImpresoras", impresoras);
        }

        public IActionResult HomeImpresora()
        {
            return View();
        }

        [HttpGet("GetImpresoras")]
        public ActionResult<IEnumerable<Impresora>> GetImpresoras(string Localidad)
        {
            IEnumerable<Impresora> Impresoras;

            if (!string.IsNullOrEmpty(Localidad))
            {
                // Filtrar por departamento si se proporciona
                Impresoras = _impresoraManager.GetImpresorasByLocalidad(Localidad);
            }
            else
            {
                // Obtener todos los Impresoras si no se proporciona un departamento
                Impresoras = _impresoraManager.GetAllImpresoras();
            }

            return View("GetImpresoras", Impresoras);
        }

        [HttpGet("EditI/{id}")]
        public ActionResult<Impresora> GetEditI(string id)
        {
            var Impresora = _impresoraManager.GetImpresoraById(id);

            if (Impresora == null)
            {
                return NotFound();
            }

            return View("EditImpresora", Impresora);
        }

        [HttpPost("EditI/{id}")]
        public ActionResult PostEditI(string id, Impresora editedImpresora)
        {
            var existingImpresora = _impresoraManager.GetImpresoraById(id);

            if (existingImpresora == null)
            {
                return NotFound();
            }

            // Actualiza las propiedades del Impresora existente con los valores del formulario enviado
            existingImpresora.IP = editedImpresora.IP;
            existingImpresora.Marca = editedImpresora.Marca;
            existingImpresora.Modelo = editedImpresora.Modelo;
            existingImpresora.Serie = editedImpresora.Serie;
            existingImpresora.Localidad = editedImpresora.Localidad;

            // Luego, guarda los cambios en la base de datos o realiza las operaciones necesarias

            _impresoraManager.ActualizarImpresora(id, editedImpresora);
            // Redirige a la acción que muestra la lista de Impresoras después de la edición
            return RedirectToAction("GetImpresoras");
        }


        [HttpGet("RegistrarI")]
        public IActionResult RegistrarI()
        {
            return View();
        }

        [HttpPost("RegistrarI")]
        public IActionResult RegistrarI([FromForm] Impresora nuevoImpresora)
        {
            try
            {
                _impresoraManager.RegistrarImpresora(nuevoImpresora);
                return RedirectToAction("GetImpresoras");
            }
            catch (Exception ex)
            {
                // Manejo de errores: Puedes personalizar según tus necesidades
                ViewBag.ErrorMessage = $"Error al registrar el Impresora: {ex.Message}";
                return View();
            }
        }

        [HttpGet]
        public IActionResult EliminarImpresora(string id)
        {
            try
            {
                _impresoraManager.EliminarImpresora(id);
                return RedirectToAction("GetImpresoras");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al eliminar el Impresora: {ex.Message}";
                return View("Eliminar", _impresoraManager.GetImpresoraById(id));
            }
        }

    }
}

