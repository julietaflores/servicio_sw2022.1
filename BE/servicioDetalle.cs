using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class servicioDetalle:BEEntidad
    {
        public decimal servicioId { get; set; }
        public decimal servicioDetalleId { get; set; }
        public string  servicioDetalleDescripcion { get; set; }
        public int servicioDetallePrecioUnitario { get; set; }
        public int servicioDetalleCostoInicial { get; set; }
        public int servicioDetalleCostoFinal { get; set; }

        public Servicio servicio { get; set; }

    }
    public enum relServicioDetalle
    {
        servicio
    }
}
