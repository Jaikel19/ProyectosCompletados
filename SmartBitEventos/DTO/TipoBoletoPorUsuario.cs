using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TipoBoletoPorUsuario
    {
        public int Id { get; set; }
        public int IdTipoBoleto { get; set; }
        public int IdUsuario { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    }
}
