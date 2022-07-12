
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class BilleteraPagoTarjeta : BEEntidad
    {
        public decimal PagoTarjetaId { get; set; }
        public decimal Secuencia { get; set; }
        public decimal PersonaId { get; set; }

        public decimal Importe { get; set; }
        public DateTime RegistroFechaHora { get; set; }

        public bool Estado { get; set; }

        public decimal MonedaId { get; set; }

        public string Codigo { get; set; }

    }
}
