using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Pago:BEEntidad
    {
        public string importe { get; set; }
        public string moneda { get; set; }
        public string referencia { get; set; }
        public string deuda_id { get; set; }
    }
}
