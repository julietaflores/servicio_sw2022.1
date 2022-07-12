using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class Moneda
    {
        public decimal MonedaId { get; set; }
        public string MonedaNombre { get; set; }
        public decimal PaisId { get; set; }
    }
}