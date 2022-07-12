using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ServicioPersonaDocumento:BEEntidad
    {
        public decimal ServicioPersonaId { get; set; }
        public decimal ServicioPersonaDocId { get; set; }
        public string ServicioPersonaDocNombre { get; set; }
        public DateTime ServicioPersonaDocFecha { get; set; }

    }
}
