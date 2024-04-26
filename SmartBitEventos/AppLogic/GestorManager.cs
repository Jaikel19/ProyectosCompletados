using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;

namespace AppLogic
{
    public class GestorManager
    {

        private SQLDao _sql;
        public GestorManager()
        {

            _sql = new SQLDao();
        }

        public List<GananciasGestor> GetGananciasGestor(int IDUsuario)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IDUsuario", IDUsuario));

            var table = _sql.GetInformation(parameters, "spGetGananciasGestor");

            if (table != null)
            {
                List<GananciasGestor> res = ConvertToTable.BuildTable<GananciasGestor>(table);

                return res;
            }
            else
            {
                return null;
            }

        }

    }

}
