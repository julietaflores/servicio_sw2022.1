using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ServicioDescripcion:BEEntidad
    {
        public decimal ServicioDescripcionId { get; set; }
        public decimal ServicioId { get; set; }
        public string ServicioDescripciondesc { get; set; }

    }
}
