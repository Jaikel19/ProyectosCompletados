using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioManager _usuarioManager;

        //public AuthController(UsuarioManager usuarioManager)
        //{
        //    _usuarioManager = usuarioManager;
        //}

        public AuthController(UsuarioManager usuarioManager)
        {
            _usuarioManager = usuarioManager ?? throw new ArgumentNullException(nameof(usuarioManager));
        }

        //[HttpPost("login")]
        //public ActionResult<string> Login([FromBody] LoginRequest request)
        //{
        //    try
        //    {
        //        var usuario = _usuarioManager.AutenticarUsuario(request.Username, request.Password);

        //        if (usuario == null)
        //        {
        //            return Unauthorized();
        //        }

        //        // ... lógica de autenticación exitosa

        //        return Ok($"Bienvenido, {usuario.Username}!");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Registra la excepción para diagnóstico
        //        Console.Error.WriteLine(ex);
        //        return StatusCode(500, "Error interno del servidor");
        //    }
        //}

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginRequest request)
        {
            try
            {
                var usuario = _usuarioManager.AutenticarUsuario(request.Username, request.Password);

                if (usuario == null)
                {
                    return Unauthorized();
                }

                // ... lógica de autenticación exitosa

                return Ok($"Bienvenido, {usuario.Username}!");
            }
            catch (Exception ex)
            {
                // Registra la excepción para diagnóstico
                Console.Error.WriteLine(ex);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}