using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WebUI.Utility;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        #region Login
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cancel()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            HTTPRequest request = new HTTPRequest();
            var result = request.Get(string.Format("Login/SecureLogin?userID={0}&password={1}", user.Email, user.Password));


            TokenInformation token = JsonConvert.DeserializeObject<TokenInformation>(result);


            if (token.Message != null || token.LoggedUser == null)
            {
                ViewBag.Message = token.Message;
                return View("Index");

            }
            HttpContext.Session.SetString("userInformation", JsonConvert.SerializeObject(token));
            HttpContext.Session.SetString("user", token.LoggedUser == null ? string.Empty : token.LoggedUser.Usuario.Nombre);
            HttpContext.Session.SetInt32("userRole", token.LoggedUser == null ? -1 : token.LoggedUser.Usuario.Rol); //se agrega e lrol
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ReturnToLogin()
        {
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Forgot password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.Result = null;
            return View();
        }

        [HttpPost]
        public IActionResult ExecuteForgotPassword(string email)
        {
            var otpCode = new CodigoOTP { Email = email };

            HTTPRequest request = new HTTPRequest();
            var result = request.Post("Login/GenerateOtp", JsonConvert.SerializeObject(otpCode));

            ViewBag.Result = !string.IsNullOrEmpty(result);
            ViewBag.Email = email;
            return View("ForgotPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword(string otp)
        {
            HTTPRequest request = new HTTPRequest();
            var result = request.Get(string.Format("Login/UpdatePasswordRequest?otp={0}", otp));

            if (string.IsNullOrEmpty(result))
            {
                TempData["Result"] = false;
                TempData["PasswordError"] = false;
                return RedirectToAction("ResetPassword", "Login");
            }

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(result);

            if (TempData["Result"] != null)
            {
                ViewBag.Result = (bool)TempData["Result"];
                ViewBag.PasswordError = (bool)TempData["PasswordError"];
                TempData["Result"] = null;
                TempData["PasswordError"] = null;
            }
            else
            {
                ViewBag.Result = true;
                ViewBag.PasswordError = false;
            }
            ViewBag.IdUsuario = usuario.Id;
            ViewBag.OTP = otp;
            return View();
        }


        [HttpPost]
        public IActionResult UpdatePassword(int idUsuario, string password, string confirmPassword, string otp)
        {
            if (string.Compare(password, confirmPassword, false) == 0)
            {
                UpdatePasswordDTO usuario = new UpdatePasswordDTO {  IdUsuario = idUsuario, Password = password, CodigoOTP = otp  };
                HTTPRequest request = new HTTPRequest();
                var result = request.Post("Login/UpdatePassword", JsonConvert.SerializeObject(usuario));

                return View("UpdatePasswordSuccess");
            }
            else
            {
                TempData["Result"] = true;
                TempData["PasswordError"] = true;
                return RedirectToAction("ResetPassword", "Login", new { otp = otp });
            }
        }

        [HttpGet]
        public IActionResult UpdatePasswordSuccess()
        {
            return View();
        }
        #endregion

    }

}

