using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
namespace AppLogic
{
    public class LoginManager
    {
        private SQLDao _sql;
        public LoginManager()
        {
            _sql = new SQLDao();
        }

        public Usuario Login(string usuario, string contrasena)
        {
            List<Parameters> parameters = new List<Parameters>();

            parameters.Add(new Parameters("usuario", usuario));
            parameters.Add(new Parameters("contrasena", contrasena));

            var table = _sql.GetInformation(parameters, "spLogin");

            //aqui como quiero tomar solo uno le puse FirstorDefault 
            Usuario res = ConvertToTable.BuildTable<Usuario>(table).FirstOrDefault();

            // Si quiero devolver mas datos lo hago asi   :
            //List<Usuario> res = ConvertToTable.BuildTable<Usuario>(table);

            string[] parts = res.Direccion.Split(',');
            if (parts.Length == 2)
            {
                res.Latitud = parts[0];
                res.Longitud = parts[1];
            }

            return res;
        }
    }
}