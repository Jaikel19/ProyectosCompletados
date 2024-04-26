using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using SmartBitEventos.Security;

namespace SmartBitEventos.Controllers
{

    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Usuario> GetAllUsers()
        {
            UsuarioManager um = new UsuarioManager();

            return um.GetAllUsers();
        }

        [HttpPost]
        public void UpdateStatus(int idUsuario)
        {
            UsuarioManager um = new UsuarioManager();
            um.UpdateStatus(idUsuario);
        }

        [HttpPost]
        [ValidLoginAuthorize(Access = DTO.Enum.EnumAccess.Usuario)]
        public Usuario CreateUsuario([FromBody]Usuario usuario)
        {
            try
            {
                var um = new UsuarioManager();

                //Encriptar contrasena
                usuario.Password = usuario.Password.Encrypt();
                Usuario usuarioCreado = um.CreateUsuario(usuario);
                return usuarioCreado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
  
        public ActualizarUsuario UpdateUsuario([FromBody] ActualizarUsuario usuarioInfo)
        {
            try
            {
                var um = new UsuarioManager();
                ActualizarUsuario actualizarUsuarioinfo = um.ActualizarUsuario(usuarioInfo);
                return actualizarUsuarioinfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Usuario RegistrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var um = new UsuarioManager();
                //Encriptar contrasena
                usuario.Password = usuario.Password.Encrypt();
                Usuario usuarioCreado = um.CreateUsuario(usuario);
                return usuarioCreado;
            }
            catch (Exception ex)
            {
                throw ex;
            }             
        }
    }
}





