using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartBitEventos.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommunicationsController : ControllerBase
    {
        [HttpPost]
        public async Task<API_Response> SendEmail(string emailAddress, string qrContent)
        {
            AdminEmail ae = new AdminEmail();
            await ae.SendEmail(emailAddress, qrContent);
            return new API_Response { Result = "OK" };
        }

        [HttpPost]
        public async Task<API_Response> SendSMS(string phone)
        {
            AdminSMS ae = new AdminSMS();
            ae.SendConfirmationMessage("+506" + phone);
            return new API_Response { Result = "OK" };
        }
    }
}
