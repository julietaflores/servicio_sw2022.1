using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class BilleteraDetalle
    {
        public int BilleteraDetalleId { get; set; }
        public decimal BilleteraId { get; set; }
        public Nullable<System.Int32> CajeroId { get; set; }
        public decimal MedioPagoId { get; set; }
        public decimal BilleteraConceptoId { get; set; }
        public string ServAsigId { get; set; }
        public string BilleteraDebeHaber { get; set; }
        public decimal BilleteraValor { get; set; }
        public Nullable<System.DateTime> BilleteraFechaTransaccion { get; set; }
        public string BilleteraNroTransaccion { get; set; }
        public string BilleteraObservacion { get; set; }

        public BilleteraConcepto billeteraConcepto { get; set; }
        public Servicio servicio { get; set; }
    }
}