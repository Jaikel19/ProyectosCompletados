using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AccesosPorRol
    {
        public AccesosPorRol(){}

        public int ID { get; set; }
        public int IDRol { get; set; }
        public int IDAcceso { get; set; }
        public string Nivel { get; set; }
    }
}
