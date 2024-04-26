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
    public class BoletosAsignadosManager
    {
        private SQLDao _sql;
        public BoletosAsignadosManager()
        {

            _sql = new SQLDao();
        }

        public List<BoletosAsignados> GetBoletosAsignados(int? IdUsuario)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdUsuario", IdUsuario));
            var table = _sql.GetInformation(parameters, "GetBoletosByUser");

            List<BoletosAsignados> res = ConvertToTable.BuildTable<BoletosAsignados>(table);

            return res;
        }
    }
}
