using AppLogic;
using Azure.Communication.Email;
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
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public TokenInformation SecureLogin(string userID, string password)
        {

            try
            {
                var login = new LoginManager();
                Usuario user = login.Login(userID, password.Encrypt());

                var acccessManager = new AccesosManager();
                LoggedUser loggedUser = new LoggedUser();
                loggedUser.Usuario = user;
                loggedUser.Accesos = acccessManager.AccesosPorRol(user.Rol);

                var token = Security.Authentication.GenerarateToken(loggedUser);
                token.LoggedUser = loggedUser;
                return token;
            }
            catch (Exception ex)
            {
                return new TokenInformation() { Message = ex.Message };
            }

        }

        [HttpGet]
        public Usuario UpdatePasswordRequest(string otp)
        {
            OtpCodeManager otpManager = new OtpCodeManager();
            var result = otpManager.GetOtpCode(otp);

            if (result == null)
            {
                return null;
            }

            UsuarioManager usuarioManager = new UsuarioManager();
            Usuario usuario = usuarioManager.GetUsers(null, result.IdUsuario);

            return usuario;
        }

        [HttpPost]
        public Usuario UpdatePassword(UpdatePasswordDTO updatePassword)
        {
            try
            {
                UsuarioManager usuarioManager = new UsuarioManager();
                Usuario usuario = new Usuario { Id = updatePassword.IdUsuario, Password = updatePassword.Password.Encrypt() };
                usuarioManager.CreateUsuario(usuario);

                CodigoOTP codigo = new CodigoOTP { Codigo = updatePassword.CodigoOTP };
                usuarioManager = new UsuarioManager();
                usuarioManager.DeleteOtp(codigo);

                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public string GenerateOtp(CodigoOTP codigoOTP)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            Usuario usuario = usuarioManager.GetUsers(codigoOTP.Email);

            if (usuario == null) { return null; }

            codigoOTP.IdUsuario = (int)usuario.Id;

            Security.Authentication.GenerarateOTP(codigoOTP);

            OtpCodeManager otpManager = new OtpCodeManager();
            otpManager.CreateOtpCode(codigoOTP);

            var result = SendEmail(usuario, string.Format("{0}login/ResetPassword?otp={1}", ConfigurationManager.AppSettings["webUrl"], codigoOTP.Codigo));

            return result;
        }


        private string SendEmail(Usuario usuario, string url)
        {
            AdminEmail emailService = new AdminEmail(ConfigurationManager.AppSettings["emailConnectionString"]);
            EmailDTO emailDTO = new EmailDTO();

            emailDTO.Email = usuario.Email;
            emailDTO.Subject = "Olvido su contrasena";
            emailDTO.EmailBody = string.Format("Por favor ingrese al siguiente enlace para actualizar su contrasena: <a href='{0}'>Restablecer Contraseña</a> Gracias por preferirnos.", url);
            var result = emailService.SendEmail(emailDTO);

            return result;
        }

    }
}