using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class UsuarioManager
    {
        private SQLDao _sql;

        public UsuarioManager()
        {
            _sql = new SQLDao();
        }

        public Usuario CreateUsuario(Usuario usuario)
        {
            List<Parameters> parameters = new List<Parameters>();

            parameters.Add(new Parameters("Id", usuario.Id));
            parameters.Add(new Parameters("Nombre", usuario.Nombre));
            parameters.Add(new Parameters("Apellido", usuario.Apellido));
            parameters.Add(new Parameters("Email", usuario.Email));
            parameters.Add(new Parameters("Password", usuario.Password));
            parameters.Add(new Parameters("Telefono", usuario.Telefono));
            parameters.Add(new Parameters("Direccion", usuario.Direccion));
            parameters.Add(new Parameters("Cedula", usuario.Cedula));
            parameters.Add(new Parameters("CuentaConfirmada", usuario.CuentaConfirmada));
            parameters.Add(new Parameters("Rol", usuario.Rol));
            parameters.Add(new Parameters("Activo", usuario.Activo));
            parameters.Add(new Parameters("Bloqueado", usuario.Bloqueado));
            parameters.Add(new Parameters("IdUsuarioCreacion", usuario.IdUsuarioCreacion));

            var table = _sql.GetInformation(parameters, "spCreateOrUpdateUsuario");


            // en este caso no necesito devolver valores por eso la deje null ya que el SP solo postea el user 
            return null;
        }

        public Usuario GetUsers(string email, int? idUsuario = null)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdUsuario", idUsuario));
            parameters.Add(new Parameters("Email", email));

            var table = _sql.GetInformation(parameters, "spSeleccionarUsuarios");

            if (table != null)
            {
                Usuario res = ConvertToTable.BuildTable<Usuario>(table).FirstOrDefault();

                return res;
            }
            else
            {
                return null;
            }
        }

        public List<Usuario> GetAllUsers()
        {
            var table = _sql.GetInformation(new List<Parameters>(), "spSeleccionarUsuarios");

            if (table != null)
            {
                List<Usuario> res = ConvertToTable.BuildTable<Usuario>(table);

                return res;
            }
            else
            {
                return null;
            }
        }

        public void UpdateStatus(int idUsuario)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("Id", idUsuario));
            _sql.ExecuteProcedure(parameters, "spUpdateStatus");
        }

        public void DeleteOtp(CodigoOTP codigo)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("Codigo", codigo.Codigo));

            _sql.ExecuteProcedure(parameters, "DeleteCodigoOTP");
        }



        public ActualizarUsuario ActualizarUsuario(ActualizarUsuario actualizarUsuario)
        {
            List<Parameters> parameters = new List<Parameters>();

            parameters.Add(new Parameters("Id", actualizarUsuario.Id));
            parameters.Add(new Parameters("Nombre", actualizarUsuario.Nombre));
            parameters.Add(new Parameters("Apellido", actualizarUsuario.Apellido));
            parameters.Add(new Parameters("Email", actualizarUsuario.Email));
            parameters.Add(new Parameters("Telefono", actualizarUsuario.Telefono));
            parameters.Add(new Parameters("Direccion", actualizarUsuario.Direccion)); 
            parameters.Add(new Parameters("Cedula", actualizarUsuario.Cedula));      

            var table = _sql.GetInformation(parameters, "spActualizarUsuario");

  

            return actualizarUsuario;
        }



    }



}

