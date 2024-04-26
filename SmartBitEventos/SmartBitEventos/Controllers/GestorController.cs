using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartBitEventos.Security;

namespace SmartBitEventos.Controllers
{



    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GestorController : Controller
    {
        [HttpGet]
        public List<GananciasGestor> GetGananciasGestor(int IDUsuario)
        {
            GestorManager gm = new GestorManager();
            List<GananciasGestor> GestorGanancias = gm.GetGananciasGestor(IDUsuario);

            return GestorGanancias;
        }
    }
}
