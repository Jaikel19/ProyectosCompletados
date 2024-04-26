using DTO;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;

namespace WebUI.Utility
{
    public static class SessionUtilities
    {
        public static string GetToken(this ISession session)
        {
            var userInfo = session.GetString("userInformation");

            if (userInfo == null) {  return string.Empty; }

            TokenInformation tokenInformation = JsonConvert.DeserializeObject<TokenInformation>(userInfo);

            return tokenInformation.Token;
        }

        public static Usuario? GetLoggedUser(this ISession session)
        {
            var userInfo = session.GetString("userInformation");

            if (userInfo == null) { return null; }

            TokenInformation tokenInformation = JsonConvert.DeserializeObject<TokenInformation>(userInfo);

            return tokenInformation.LoggedUser.Usuario;
        }

        public static List<Accesos> GetLoggedUserAccess(this ISession session)
        {
            var userInfo = session.GetString("userInformation");

            if (userInfo == null) { return null; }

            TokenInformation tokenInformation = JsonConvert.DeserializeObject<TokenInformation>(userInfo);

            return tokenInformation.LoggedUser.Accesos;
        }

        public static bool GetIsUserLoggedIn(this ISession session)
        {
            var userInfo = session.GetString("userInformation");

            return userInfo != null;
        }
    }
}
