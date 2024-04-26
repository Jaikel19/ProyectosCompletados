using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartBitEventos.Security;

namespace SmartBitEventos.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PayPalController : Controller
    {
        [HttpPost]
        public async Task<string> CreatePayment(PaypalDTO paypal)
        {
            PaypalManager pm = new PaypalManager();
            var paymentResult = await pm.CreatePayment(paypal);
            return paymentResult;
        }
    }
}
