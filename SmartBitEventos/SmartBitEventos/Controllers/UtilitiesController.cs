using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using Microsoft.AspNetCore.Cors;

namespace SmartBitEventos.Controllers
{
    //adding CORS
    [EnableCors("MyCorsPolicy")]
    //Adding Action
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        [HttpGet]
        public string GetQR(string content)
        {
            AdminQR adminQR = new AdminQR();
            return adminQR.GenerateQR(content);
        }

        [HttpGet]
        public byte[] GetPNG(string content)
        {
            AdminQR adminQR = new AdminQR();
            return adminQR.GeneratePNG(content);
        }
    }
}
