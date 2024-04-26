using QRCoder;
using static QRCoder.QRCodeGenerator;

namespace AppLogic
{
    public class AdminQR
    {
        public string GenerateQR(string informationText)
        {
            var qrGenerator = new QRCodeGenerator();

            var qrCodeData = qrGenerator.CreateQrCode(informationText, ECCLevel.M, true, false);

            var qrCode = new SvgQRCode(qrCodeData);

            return qrCode.GetGraphic(5);
        }

        public byte[] GeneratePNG(string informationText)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(informationText, QRCodeGenerator.ECCLevel.L);
            var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(5);
        }
    }
}
