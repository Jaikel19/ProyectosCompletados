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
    public class DashboardManager
    {
        private SQLDao _sql;
        public DashboardManager()
        {

            _sql = new SQLDao();
        }

        public List<GananciasAdmin> GetDGananciasAdmin()
        {
            List<Parameters> parameters = new List<Parameters>();
            var table = _sql.GetInformation(parameters, "spGetGananciasAdmin"); 

            List<GananciasAdmin> res = ConvertToTable.BuildTable<GananciasAdmin>(table);

            return res;
        }

        public List<Dashboard> GetDashboard(int? IdUsuario)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdUsuario", IdUsuario));
            var table = _sql.GetInformation(parameters, "spGetDashboard");

            List<Dashboard> res = ConvertToTable.BuildTable<Dashboard>(table);

            return res;
        }
    }
}
