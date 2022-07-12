using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PromocionDetalleRequerimiento : BEEntidad
    {
        public decimal PPromocionId { get; set; }
        public decimal PPersonaId { get; set; }
        public DateTime PFechaInsercion { get; set; }
        public string RequiereServicioId { get; set; }
        public bool  Estado { get; set; }
        public Promocion promocion { get; set; }



    }


    public enum relPromocionDetalleRequerimiento
    {
        Promocion
    }
}
