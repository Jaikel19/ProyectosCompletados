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
    public class AccesosManager
    {
        private SQLDao _sql;

        public AccesosManager()
        {
            _sql = new SQLDao();
        }

        public List<Accesos> AccesosPorRol(int idRol)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdRol", idRol));

            var table = _sql.GetInformation(parameters, "spAccesosPorRol");
            List<Accesos> res = ConvertToTable.BuildTable<Accesos>(table);

            return res;
        }
    }
}
