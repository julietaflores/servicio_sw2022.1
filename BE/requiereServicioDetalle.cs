using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public   class requiereServicioDetalle:BEEntidad
    {
        public string requiereServicioId { get; set; }
        public decimal servicioId { get; set; }
        public decimal servicioDetalleId { get; set; }

        public int servicioDetalleCantidad { get; set; }
        public int servicioDetallePUFecha { get; set; }
        public string servicioDetalleDatos { get; set; }

    }
}
