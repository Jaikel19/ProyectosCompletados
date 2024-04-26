using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace SmartBitEventos.Security
{
    public static class EncryptEngine
    {
        public static string Encrypt(this string value)
        {
            string encrypt = Encriptacion.EncryptString(ConfigurationManager.AppSettings["EncryptKey"], value);
            return encrypt;
        }

        public static string Decrypt(this string value)
        {
            string encrypt = Encriptacion.DecryptString(ConfigurationManager.AppSettings["EncryptKey"], value);
            return encrypt;
        }
    }
}
