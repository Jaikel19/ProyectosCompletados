using AppLogic;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputadoraController : ControllerBase
    {
        private readonly ComputadoraManager _computadoraManager;

        public ComputadoraController(ComputadoraManager computadoraManager)
        {
            _computadoraManager = computadoraManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Computadora>> GetAllComputadoras()
        {
            var computadoras = _computadoraManager.GetAllComputadoras();
            return Ok(computadoras);
        }

        [HttpGet("{id}")]
        public ActionResult<Computadora> GetComputadoraById(string id)
        {
            var computadora = _computadoraManager.GetComputadoraById(id);

            if (computadora == null)
            {
                return NotFound();
            }

            return Ok(computadora);
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarComputadora([FromBody] Computadora nuevoComputadora)
        {
            try
            {
                _computadoraManager.RegistrarComputadora(nuevoComputadora);
                return Ok("Computadora registrada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar la computadora: {ex.Message}");
            }
        }

        [HttpPut("actualizar/{id}")]
        public IActionResult ActualizarComputadora(string id, [FromBody] Computadora computadoraActualizado)
        {
            try
            {
                _computadoraManager.ActualizarComputadora(id, computadoraActualizado);
                return Ok("Computadora actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la computadora: {ex.Message}");
            }
        }

    }
}