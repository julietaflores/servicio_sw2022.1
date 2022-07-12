using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PromocionDetallePersona : BEEntidad
    {
        public decimal PromocionId { get; set; }
        public decimal PersonaId { get; set; }

        public DateTime FechaInsercion { get; set; }
        public bool  Estado { get; set; }

        public decimal Valor { get; set; }

        public Promocion promocion { get; set; }

    }
    public enum relPromocionDetallePersona
    {
        Promocion
    }


}
