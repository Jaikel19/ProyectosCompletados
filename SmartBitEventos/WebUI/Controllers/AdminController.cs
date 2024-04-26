using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Utility;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogsAdmin()
        {
            return View();
        }
        public IActionResult UserList()
        {
            return View();
        }
        public IActionResult GetAllUsers()
        {
            HTTPRequest request = new HTTPRequest();

            var usuariosSrc = request.Get("Usuario/GetAllUsers");

            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(usuariosSrc);

            return Json(usuarios);
        }

        public IActionResult UpdateStatus([FromBody] int idUsuario)
        {
            HTTPRequest request = new HTTPRequest();

            Usuario usuario = new Usuario();

            usuario.Id = idUsuario;

            string jsonString = JsonConvert.SerializeObject(usuario);

            var result = request.Post("Usuario/UpdateStatus", jsonString);

            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(result);

            return Json(usuarios);
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult SystemLogs()
        {
            return View();
        }
        public IActionResult Finances()
        {
            return View();
        }
    }
}
