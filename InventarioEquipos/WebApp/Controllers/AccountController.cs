using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using DTO; // Asegúrate de tener el espacio de nombres correcto para la clase Usuario
using AppLogic;

namespace WebApp.Controllers
{
    public class AccountController : Controller
{
        private readonly UsuarioManager _usuarioManager;

        public AccountController(UsuarioManager usuarioManager)
        {
            _usuarioManager = usuarioManager;
        }

        //public AccountController()
        //{
        //}

        public IActionResult Login()
    {
        return View();
    }

        [HttpPost]
        public IActionResult Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuarioManager.AutenticarUsuario(model.Username, model.Password);

                if (usuario != null)
                {

                    return RedirectToAction("Index", "Home"); // Redirige a la página principal después del inicio de sesión.
                }
                else
                {
                    ModelState.AddModelError("", "Error de inicio de sesión. Por favor, verifica tus credenciales.");
                }
            }

            return View(model);
        }
    }
}
