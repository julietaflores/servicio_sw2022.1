using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class Billetera
    {
        public decimal BilleteraId { get; set; }
        public decimal MonedaId { get; set; }
        public string BilleteraNroCuenta { get; set; }
        public decimal BilleteraSaldo { get; set; }
        public decimal PersonaBilleteraId { get; set; }
        public Nullable<System.DateTime> BilleteraFechaCreacion { get; set; }

        public Moneda moneda { get; set; }

        public List<BilleteraDetalle> billeteraDetalles { get; set; }
    }
}