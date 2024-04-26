using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Utility;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            var user = HttpContext.Session.GetLoggedUser();


            return View(user);
        }


        public IActionResult ProfileEdit()
        {
            var user = HttpContext.Session.GetLoggedUser();


            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(ActualizarUsuario user)
        {

            Usuario loggedUser = HttpContext.Session.GetLoggedUser();

            user.Id = loggedUser.Id;
            user.Direccion = $"{user.Latitud},{user.Longitud}";
            string newUserJson = JsonConvert.SerializeObject(user);

            HTTPRequest request = new HTTPRequest();

            string token = HttpContext.Session.GetToken();

    
            request.Post("usuario/UpdateUsuario", newUserJson, token);

            HttpContext.Session.Clear();


            return RedirectToAction("Index", "Login");
        }
   

        [HttpPost]
        public IActionResult RegistroUsuario(Usuario user)
        {
            Usuario loggedUser = HttpContext.Session.GetLoggedUser();

            user.CuentaConfirmada = false;
            user.Rol = 2;
            user.Activo = true;
            user.Bloqueado = false;
            user.IdUsuarioCreacion = loggedUser == null ? 0 : (int)loggedUser.Id;
            user.Direccion = $"{user.Latitud},{user.Longitud}";
            user.NombreUsuario = loggedUser == null ? "" : loggedUser.Email;

            string newUserJson = JsonConvert.SerializeObject(user);

            HTTPRequest request = new HTTPRequest();

            string token = HttpContext.Session.GetToken();

            if (string.IsNullOrEmpty(token))
            {
                request.Post("usuario/registrarusuario", newUserJson);
            }
            else
            {
                request.Post("usuario/Createusuario", newUserJson, token);
            }

            return RedirectToAction("Index", "Login");                                  
        }
        public IActionResult ConfirmAccount()
        {
            ViewBag.StatusMessage = "Cuenta creada satisfactoriamente";
            return View();

        }
        public IActionResult SendCode()
        {
            return View();
        }
        public IActionResult ReadQR()
        {
            return View();
        }
        public IActionResult ConfirmOTP()
        {
            return View();
        }
        public IActionResult MyEvents()
        {
            return View();
        }
    }
}


