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
    public class AuditoriaManager
    {
        private SQLDao _sql;
        public AuditoriaManager()
        {

            _sql = new SQLDao();
        }

        public List<Auditoria> GetAuditorias()
        {
            List<Parameters> parameters = new List<Parameters>();
            var table = _sql.GetInformation(parameters, "spGetAuditorias");

            List<Auditoria> res = ConvertToTable.BuildTable<Auditoria>(table);

            return res;
        }
    }
}
