using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AppLogic
{
    public class AdminSMS
    {
        public void SendConfirmationMessage(string phone)
        {
            string company = "SmartBit Eventos - ";
            string textMessage = company + "Hemos recibido su Pedido! Muchas Gracias";
            SendSMS(phone, textMessage);
        }
        public void SendWelcomeMessage(string phone)
        {
            string company = "SmartBit Eventos";
            string textMessage = "Bienvenido a " + company;
            SendSMS(phone, textMessage);
        }
        private void SendSMS(string phone, string text)
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "AC6c72b75435ba4283fa3dedf3868dce3f";
            string authToken = "b76a876ae1aa92c5c78f805faac7225a";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: text,
                from: new Twilio.Types.PhoneNumber("+13257506369"),
                to: new Twilio.Types.PhoneNumber(phone)
            );
            Console.WriteLine(message.Sid);
        }
    }
}