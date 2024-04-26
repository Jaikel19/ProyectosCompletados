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
    public class AuditoriaController : ControllerBase
    {
        [HttpGet]
        public List<Auditoria> GetAuditorias()
        {
            AuditoriaManager em = new AuditoriaManager();
            return em.GetAuditorias();
        }
    }
}
