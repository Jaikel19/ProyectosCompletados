using AppLogic;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoManager _departamentoManager;

        public DepartamentoController(DepartamentoManager departamentoManager)
        {
            _departamentoManager = departamentoManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Departamento>> GetAllDepartamentos()
        {
            var departamentos = _departamentoManager.GetAllDepartamentos();
            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        public ActionResult<Departamento> GetDepartamentoById(string id)
        {
            var departamento = _departamentoManager.GetDepartamentoById(id);

            if (departamento == null)
            {
                return NotFound();
            }

            return Ok(departamento);
        }

        [HttpGet("{id}/computadoras")]
        public ActionResult<IEnumerable<Computadora>> GetComputadorasByDepartamentoId(string id)
        {
            var computadoras = _departamentoManager.GetComputadorasByDepartamentoId(id);

            if (computadoras == null || computadoras.Count() == 0)
            {
                return NotFound();
            }

            return Ok(computadoras);
        }


        [HttpGet("{id}/celulares")]
        public ActionResult<IEnumerable<Celular>> GetCelularesByDepartamentoId(string id)
        {
            var celulares = _departamentoManager.GetCelularesByDepartamentoId(id);

            if (celulares == null || celulares.Count() == 0)
            {
                return NotFound();
            }

            return Ok(celulares);
        }


        [HttpGet("{id}/impresoras")]
        public ActionResult<IEnumerable<Computadora>> GetImpresorasByDepartamentoId(string id)
        {
            var impresoras = _departamentoManager.GetImpresorasByDepartamentoId(id);

            if (impresoras == null || impresoras.Count() == 0)
            {
                return NotFound();
            }

            return Ok(impresoras);
        }

        [HttpPost]
        public ActionResult AddDepartamento([FromBody] Departamento departamento)
        {
            _departamentoManager.AddDepartamento(departamento);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDepartamento(string id, [FromBody] Departamento departamento)
        {
            var existingDepartamento = _departamentoManager.GetDepartamentoById(id);

            if (existingDepartamento == null)
            {
                return NotFound();
            }

            _departamentoManager.UpdateDepartamento(id, departamento);

            return Ok();
        }
    }
}