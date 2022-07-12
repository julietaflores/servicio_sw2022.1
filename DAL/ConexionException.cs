using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class ConexionException:ApplicationException
    {
        public ConexionException(string mensaje, Exception original) : base(mensaje, original)
        {
        }

        public ConexionException(string mensaje) : base(mensaje)
        {
        }
    }
}
