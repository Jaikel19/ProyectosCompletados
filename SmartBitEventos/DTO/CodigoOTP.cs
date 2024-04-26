using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CodigoOTP
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Codigo { get; set; } = "";
        public DateTime Expiracion { get; set; }
    }
}
