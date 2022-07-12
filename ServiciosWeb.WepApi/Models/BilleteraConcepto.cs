using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class BilleteraConcepto:BEEntidad
    {
        public decimal BilleteraConceptoId { get; set; }
        public string BilleteraConceptoNombre { get; set; }
    }
}