using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;

namespace SmartBitEventos.Controllers
{
    //adding CORS
    [EnableCors("MyCorsPolicy")]
    //Adding Action
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoletosAsignadosController : ControllerBase
    {
        [HttpGet]
        public List<BoletosAsignados> GetBoletosAsignados(int? IdUsuario)
        {
            BoletosAsignadosManager em = new BoletosAsignadosManager();
            return em.GetBoletosAsignados(IdUsuario);
        }
    }
}
