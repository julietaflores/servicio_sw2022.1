using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public  class ServicioRequerimiento:BEEntidad
    {
        public decimal ServicioRequerimientoId { get; set; }
        public decimal ServicioId { get; set; }
        public string ServicioRequerimientoDesc { get; set; }
    }
}
