using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Promocion : BEEntidad
    {
        public decimal PromocionId { get; set; }
        public string PromocionDescripcion { get; set; }
        public string PromocionDescripcionCliente { get; set; }

        public decimal PromocionValor { get; set; }
        public bool  PromocionPorc { get; set; }

        public bool PromocionEstado { get; set; }

    }
}
