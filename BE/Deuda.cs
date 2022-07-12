using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Deuda
    {
        public string cliente { get; set; }
        public string descripcion { get; set; }
        public string deuda_id { get; set; }
        public int cuota { get; set; }
        public string moneda { get; set; }
        public string importe { get; set; }
        public string vencimiento { get; set; }

    }
}
