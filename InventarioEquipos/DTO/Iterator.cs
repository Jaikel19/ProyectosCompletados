using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public interface IIterator<T>
    {
        T Actual();
        bool Siguiente();
    }
    public interface IIterable<T>
    {
        IIterator<T> ObtenerIterator();
    }
}
