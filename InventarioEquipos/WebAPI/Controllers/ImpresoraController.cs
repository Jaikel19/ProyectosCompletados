using AppLogic;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpresoraController : ControllerBase
    {
        private readonly ImpresoraManager _ImpresoraManager;

        public ImpresoraController(ImpresoraManager ImpresoraManager)
        {
            _ImpresoraManager = ImpresoraManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Impresora>> GetAllImpresoras()
        {
            var impresoras = _ImpresoraManager.GetAllImpresoras();
            return Ok(impresoras);
        }

        [HttpGet("{id}")]
        public ActionResult<Impresora> GetImpresoraById(string id)
        {
            var impresoras = _ImpresoraManager.GetImpresoraById(id);

            if (impresoras == null)
            {
                return NotFound();
            }

            return Ok(impresoras);
        }

        [HttpGet("byLocalidad")]
        public ActionResult<IEnumerable<Impresora>> GetImpresorasByLocalidad([FromQuery] string localidad)
        {
            var impresoras = _ImpresoraManager.GetImpresorasByLocalidad(localidad);

            if (impresoras == null || impresoras.Count() == 0)
            {
                return NotFound();
            }

            return Ok(impresoras);
        }


        [HttpPost("registrar")]
        public IActionResult RegistrarImpresora([FromBody] Impresora nuevoImpresora)
        {
            try
            {
                _ImpresoraManager.RegistrarImpresora(nuevoImpresora);
                return Ok("Impresora registrada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar la Impresora: {ex.Message}");
            }
        }

        [HttpPut("actualizar/{id}")]
        public IActionResult ActualizarImpresora(string id, [FromBody] Impresora ImpresoraActualizado)
        {
            try
            {
                _ImpresoraManager.ActualizarImpresora(id, ImpresoraActualizado);
                return Ok("Impresora actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la Impresora: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Eliminar(string id)
        {
            try
            {
                _ImpresoraManager.EliminarImpresora(id);
                return Ok("Celular eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                // Manejo de errores: Puedes personalizar según tus necesidades
                return BadRequest($"Error al eliminar el celular: {ex.Message}");
            }
        }

        [HttpGet("count")]
        public ActionResult<long> ContarImpresoras()
        {
            var count = _ImpresoraManager.ContarImpresoras();
            return Ok(count);
        }


    }
}