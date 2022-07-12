using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoLogin : BEEntidad
    {
        public decimal TipoLoginId { get; set; }
        public string TipoLoginNombreTipo { get; set; }
        public int Estado { get; set; }
    }
}
