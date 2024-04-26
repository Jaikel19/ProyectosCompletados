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
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public List<Dashboard> GetDashboard(int? IdUsuario)
        {
            DashboardManager em = new DashboardManager();
            return em.GetDashboard(IdUsuario);
        }



        [HttpGet]
        public List<GananciasAdmin> GetGananciasAdmin()
        {
            DashboardManager em = new DashboardManager();
            return em.GetDGananciasAdmin();
        }
    }
}
