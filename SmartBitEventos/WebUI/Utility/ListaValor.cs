using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ListaValor : BaseEntity
    {
        public int IdLista { get; set; }
        public string IdValor { get; set; }
        public string Descripcion { get; set; }

        public ListaValor()
        {

        }

    }
}
