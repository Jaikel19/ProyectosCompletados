using AppLogic;
using DTO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using static AppLogic.CelularManager;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CelularController : ControllerBase
    {
        private readonly CelularManager _celularManager;

        public CelularController(CelularManager celularManager)
        {
            _celularManager = celularManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Celular>> GetAllCelulares()
        {
            var celulares = _celularManager.GetAllCelulares();
            return Ok(celulares);
        }

        [HttpGet("{id}")]
        public ActionResult<Celular> GetCelularById(string id)
        {
            var celular = _celularManager.GetCelularById(id);

            if (celular == null)
            {
                return NotFound();
            }

            return Ok(celular);
        }

        [HttpGet("byMarca")]
        public ActionResult<IEnumerable<Celular>> GetCelularesByMarca([FromQuery] string marca)
        {
            var celulares = _celularManager.GetCelularesByMarca(marca);

            if (celulares == null || celulares.Count() == 0)
            {
                return NotFound();
            }

            return Ok(celulares);
        }

        [HttpGet("byModelo")]
        public ActionResult<IEnumerable<Celular>> GetCelularesByModelo([FromQuery] string modelo)
        {
            var celulares = _celularManager.GetCelularesByModelo(modelo);

            if (celulares == null || celulares.Count() == 0)
            {
                return NotFound();
            }

            return Ok(celulares);
        }



        [HttpGet("bySIM")]
        public ActionResult<IEnumerable<Celular>> GetCelularesBySIM([FromQuery] string sim)
        {
            var celulares = _celularManager.GetCelularesBySIM(sim);

            if (celulares == null || celulares.Count() == 0)
            {
                return NotFound();
            }

            return Ok(celulares);
        }


        [HttpGet("byLocalidad")]
        public ActionResult<IEnumerable<Celular>> GetCelularesByLocalidad([FromQuery] string localidad)
        {
            var celulares = _celularManager.GetCelularesByLocalidad(localidad);

            if (celulares == null || celulares.Count() == 0)
            {
                return NotFound();
            }

            return Ok(celulares);
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarCelular([FromBody] Celular nuevoCelular)
        {
            try
            {
                _celularManager.RegistrarCelular(nuevoCelular);
                return Ok("Celular registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar el celular: {ex.Message}");
            }
        }

        [HttpPut("actualizar/{id}")]
        public IActionResult ActualizarCelular(string id, [FromBody] Celular celularActualizado)
        {
            try
            {
                _celularManager.ActualizarCelular(id, celularActualizado);
                return Ok("Celular actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el celular: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(string id)
        {
            try
            {
                _celularManager.EliminarCelular(id);
                return Ok("Celular eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                // Manejo de errores: Puedes personalizar según tus necesidades
                return BadRequest($"Error al eliminar el celular: {ex.Message}");
            }
        }

        [HttpGet("count")]
        public ActionResult<long> ContarCelulares()
        {
            var count = _celularManager.ContarCelulares();
            return Ok(count);
        }

    }

    public class RepositorioCelular : IIterable<Celular>
    {
        private readonly IMongoCollection<Celular> _celularCollection;

        public RepositorioCelular(IMongoDatabase database)
        {
            _celularCollection = database.GetCollection<Celular>("celulares");
        }

        public IIterator<Celular> ObtenerIterator()
        {
            var celulares = _celularCollection.Find(FilterDefinition<Celular>.Empty).ToList();
            return new IteratorConcreto<Celular>(celulares);
        }
    }
}