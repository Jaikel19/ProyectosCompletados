using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Mapper;

namespace DataAccess.Dao
{
    public class SQLDao
    {
        private SqlConnection _conn;
        private SqlCommand _cmd;

        //los campos se inicializan en el constructor para ser globales en la clase
        public SQLDao()
        {
            _conn = new SqlConnection();
            _cmd = new SqlCommand();
        }

           //Comment Rafa:  El get information reciben los datos para ejecutar el SP , en formato tabla
        public DataTable GetInformation(List<Parameters> parametros, string procedimiento)
        {
            DataTable tabla = new DataTable();
            StartConnection(procedimiento);
            SetParameters(parametros, _cmd);

            _conn.Open();
            using (var da = new SqlDataAdapter(_cmd))
            {
                 da.Fill(tabla);
            }
            _conn.Close();

            return tabla;
        }

        public void ExecuteProcedure(List<Parameters> parametros, string procedimiento)
        {
            StartConnection(procedimiento);
            SetParameters(parametros, _cmd);

            _conn.Open();
            _cmd.ExecuteNonQuery();
            _conn.Close();
        }

        private void StartConnection(string procedimiento)
        {
            _conn.ConnectionString = "Server=tcp:raf-isbn-server.database.windows.net,1433;Initial Catalog=RAF-123-Data;Persist Security Info=False;User ID=sys_rafisbn_admin;Password=ComplianceA1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            _cmd.Connection = _conn;
            _cmd.CommandType = CommandType.StoredProcedure;
            _cmd.CommandText = procedimiento;
        }

        private void SetParameters(List<Parameters> parametros, SqlCommand cmd)
        {
            foreach (var item in parametros)
            {
                cmd.Parameters.AddWithValue(string.Format("@{0}", item.Nombre), item.Valor);
            }
        }
    }
}
