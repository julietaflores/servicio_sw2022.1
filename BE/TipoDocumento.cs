using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoDocumento : BEEntidad
    {
        public decimal TipoDocumentoId { get; set; }
        public string TipoDocumentoNombre { get; set; }
        public int Estado { get; set; }
    }
}
