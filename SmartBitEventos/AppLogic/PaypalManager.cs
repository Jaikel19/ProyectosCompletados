using System;
using PayPalCheckoutSdk.Orders;
using PayPalCheckoutSdk.Core;
using DTO;
using DataAccess.Dao;
using DataAccess.Mapper;
using System.Threading.Tasks;

namespace AppLogic
{
    public class PaypalManager
    {
        private SQLDao _sql;

        public PaypalManager()
        {
            _sql = new SQLDao();
        }

        public async Task<string> CreatePayment(PaypalDTO paypal)
        {
            try
            {
                var environment = new SandboxEnvironment("AXHIiCkr1hG9pmGo2vWVKsk-mudSbbeAogOAo4jY-h99itjBNt9ugYp3LF2j-CzcRY9ExYvPRmE6MDJJ", "EAqB-zePIvHGda40BNVAfHZ3Uz8c7GEpzYNuubR3VVBvp2_hjGouuxi_Nmb2gP8aHDz-o2WyipHqUg4Q");
                var client = new PayPalHttpClient(environment);

                var requestBody = new OrderRequest()
                {
                    CheckoutPaymentIntent = "CAPTURE",
                    PurchaseUnits = new List<PurchaseUnitRequest>
                    {
                        new PurchaseUnitRequest
                        {
                            AmountWithBreakdown = new AmountWithBreakdown
                            {
                                CurrencyCode = paypal.Currency,
                                Value = paypal.Amount
                            }
                        }
                    }
                };

                var request = new OrdersCreateRequest();
                request.RequestBody(requestBody);

                var response = await client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var result = response.Result<Order>();
                    return "Pago creado exitosamente";
                }
                else
                {
                    return "Error al crear el pago";
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                throw;
            }
        }
    }
}
