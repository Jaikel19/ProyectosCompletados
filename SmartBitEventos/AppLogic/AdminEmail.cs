using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using DTO;
using System;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace AppLogic
{
    public class AdminEmail
    {

        private string _conn;

        public AdminEmail() { }
        public AdminEmail(string conn) { _conn = conn; }
        public async Task<string> SendEmail(string emailAddress, string qrContent)
        {
            string connectionString = "endpoint=https://cha-ieee-communicationservice.unitedstates.communication.azure.com/;accesskey=iEYUmCqAdD8Tg1b2oSHaDbAWcGkaFJhuStpQbYKZKSBFUkzYe+six3ePRe3heYlNql6Xa8LQyCJjq9sjQvuG8w==";
            EmailClient emailClient = new EmailClient(connectionString);
            EmailContent emailContent = new EmailContent("SmartBit Eventos le agradece por su compra");

            //codigo imagenes azure
            string csImages = "DefaultEndpointsProtocol=https;AccountName=victor123storageaccount;AccountKey=jomLWWobYq0K3s1ocEQQLo7m06OZLzacmrOkmXlPyFGJY3UZoUl3D9wr2BUf9q109JzziThYuhAe+AStsLxbug==;EndpointSuffix=core.windows.net";
            string containerName = "proyecto2container";
            string blob1 = "logo2.png";

            BlobServiceClient blobServiceClient = new BlobServiceClient(csImages);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(blob1);
            string logo = blobClient.Uri.ToString();

            //instancia generando QR PNG
            AdminQR aqr = new AdminQR();
            byte[] QRImage = aqr.GeneratePNG(qrContent);

            //guardar imagen generada en azure
            string qrBlobName = "qrcode.png";

            //eliminar el blob por si ya existe
            await containerClient.DeleteBlobIfExistsAsync(qrBlobName);

            //nombrarlo nuevamente
            qrBlobName = "qrcode.png";

            //guardarlo
            using (MemoryStream qrStream = new MemoryStream(QRImage))
            {
                await containerClient.UploadBlobAsync(qrBlobName, qrStream);
            }
            string qrImageUrl = containerClient.Uri + "/" + qrBlobName;
            //Crear QR en la BD

            //html
            string htmlContent = $@"<!DOCTYPE html>
<html>
    <head>
        <meta charset='utf-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>
        <!-- Bootstrap CSS -->
        <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.0.0/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-wEmeIV1mKuiNpC+IOBjI7aAzPcEZeedi5yW5f2yOq55WWLwNGmvvx4Um1vskeMj0' crossorigin='anonymous'>
        <title>Correo de Confirmación</title>
    </head>
    <body>
        
        <div class= 'container mb-3 rounded' style='background-color: #1E1D1C; text-align: center; width: 38%; height: 900px; color: white; margin-top: 20px;'>
            
            <div class='container' style='align-content: center; color: white;'>
                    <div class='container' style='align-content: center; padding-top: 20px; color: white;'>
                        <a style='width: 100%; text-align: center; padding-top: 10px; color: white;'>
                            <h1 style='color: white;'>SmartBit</h1>
                        </a>
                    </div>
            </div>
    
            <div class='container mb-3 rounded' style='color: white;'>
                <div style='width: 100%; text-align: center; color: white;'>
                    <img src='{logo}' class='me-0' height='300' alt='' class='d-inline-block align-text-top' style='text-align: center; margin-top: 10px;'/>
                </div>
                <div style='width: 100%; text-align: center; color: white;'>
                    <h1 style='margin-top: 7px; text-align: center;'>Hemos recibido tu pedido</h1>
                    <p style='margin-top: 25px; text-align: center;'>Acá te proporcionamos tus boletos:</p>
                    <p style='text-align: center;'> </p>
                </div>
            </div>
            <img src='{qrImageUrl}' alt='QR Code'>
            <div class='container mb-5' style='width: 100%; text-align: center; margin-top: 80px;'>
                <div class='container'>
                    <div class='container'>
                        <img src='{logo}' class='me-2' height='60' alt='' class='d-inline-block align-text-top'/>
                    </div>
                    <div class='container'>
                        &copy; 2023 - SmartBit - Grupo3
                    </div>
                </div>
            </div>
        </div>

        <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.0.0/dist/js/bootstrap.bundle.min.js' integrity='sha384-p34f1UUtsS3wqzfto5wAAmdvj+osOnFyQFpp4Ua3gs/ZVWx6oOypYoCJhGGScy+8' crossorigin='anonymous'></script>
    </body>
</html>";
            emailContent.Html = htmlContent;

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Suscriptor de SmartBit Eventos") };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);


            EmailMessage emailMessage = new EmailMessage("smartbit_compras@24316c7e-8ef9-4165-9dd0-c19c4d32726e.azurecomm.net", emailRecipients, emailContent);


            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                    emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            return emailSendOperation.Value.Status.ToString();
        }

        public string SendEmail(EmailDTO email)
        {
            EmailClient emailClient = new EmailClient(_conn);
            EmailContent emailContent = new EmailContent(email.Subject);

            emailContent.Html = email.EmailBody;

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(email.Email, "Suscriptor de SmartBit Eventos") };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("smartbit_compras@24316c7e-8ef9-4165-9dd0-c19c4d32726e.azurecomm.net", emailRecipients, emailContent);

            EmailSendOperation emailSendOperation = emailClient.SendAsync(WaitUntil.Completed, emailMessage, CancellationToken.None).Result;
            EmailSendResult statusMonitor = emailSendOperation.Value;

            return emailSendOperation.Value.Status.ToString();
        }
    }
}