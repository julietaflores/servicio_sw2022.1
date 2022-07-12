using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BilleteraDetalleAnulacion:BEEntidad
    {
        public decimal BilleteraDetalleAnulacionId { get; set; }
        public string BilleteraDetalleAnulaciondeuda_id { get; set; }
       public string  BilleteraDetalleAnulacionReferencia { get; set; }
        public DateTime BilleteraDetalleAnulacionFecha { get; set; }
    }
}
