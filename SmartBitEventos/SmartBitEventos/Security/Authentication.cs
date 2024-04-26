using AppLogic;
using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace SmartBitEventos.Security
{
    public static class Authentication
    {

        public static TokenInformation GenerarateToken(LoggedUser usuario)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            DateTime expirationDate = DateTime.Now.AddMinutes(30);

            var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSettings["JWT:ValidIssuer"],
                audience: ConfigurationManager.AppSettings["JWT:ValidAudience"],
                claims: new List<Claim>
                {
                    new Claim("UsuarioActivo", JsonConvert.SerializeObject(usuario))
                },
                expires: expirationDate,
                signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return new TokenInformation() { ExpirationDate = expirationDate, Token = tokenString };
        }

        public static void GenerarateOTP(CodigoOTP data)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JWT:OTPSecret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            DateTime expirationDate = DateTime.Now.AddMinutes(10);
            data.Expiracion = expirationDate;

            var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSettings["JWT:ValidIssuer"],
                audience: ConfigurationManager.AppSettings["JWT:ValidAudience"],
                claims: new List<Claim>
                {
                    new Claim("data", data.Email)
                },
                expires: expirationDate,
                signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            data.Codigo = tokenString;
        }

        public static LoggedUser? ValidateToken(string token)
        {
            LoggedUser usuario = new LoggedUser();
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken.Claims.First(x => x.Type == "UsuarioActivo").Value == null) return null;

                usuario = JsonConvert.DeserializeObject<LoggedUser>(jwtToken.Claims.First(x => x.Type == "UsuarioActivo").Value);

                return usuario;
            }
            catch
            {
                return usuario;
            }
        }
    }
}