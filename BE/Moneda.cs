using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
     public class Moneda:BEEntidad
    {
        public decimal MonedaId { get; set; }
        public string MonedaNombre { get; set; }
        public decimal PaisId { get; set; }
    }
}
