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
    public class OtpCodeManager
    {
        private SQLDao _sql;

        public OtpCodeManager()
        {
            _sql = new SQLDao();
        }

        public void CreateOtpCode(CodigoOTP codigo)
        {
            List<Parameters> parameters = new List<Parameters>();

            parameters.Add(new Parameters("IdUsuario", codigo.IdUsuario));
            parameters.Add(new Parameters("Email", codigo.Email));
            parameters.Add(new Parameters("Codigo", codigo.Codigo));
            parameters.Add(new Parameters("Expiracion", codigo.Expiracion));

            _sql.ExecuteProcedure(parameters, "spCreateOtpCode");
        }

        public CodigoOTP GetOtpCode(string codigo)
        {
            List<Parameters> parameters = new List<Parameters>();

            parameters.Add(new Parameters("Codigo", codigo));

            var table = _sql.GetInformation(parameters, "spGetOtpCode");

            CodigoOTP res = ConvertToTable.BuildTable<CodigoOTP>(table).FirstOrDefault();

            return res;
        }
    }
}
