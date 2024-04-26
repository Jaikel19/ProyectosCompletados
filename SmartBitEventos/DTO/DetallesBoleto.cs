using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class DetallesBoleto
    {
        public string Nombre { get; set; }
        public string Hora { get; set; }
        public string Fecha { get; set; }
        public string Tipoboleto { get; set; }
        public int Cantidad { get; set; }
        public Double Precio { get; set; }
        public string Asientos { get; set; }
        public bool Estado { get; set; }
    }
}
