using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class ServAsigCosto

    {
        public string ServAsigCostoId { get; set; }
        public string ServAsigId { get; set; }
        public decimal ConceptoCostoId { get; set; }
        public decimal ServAsigCostoValor { get; set; }
        public ConceptoCosto conceptoCosto { get; set; }


    }
    public enum relservAsigCosto
    {
        conceptoCosto

    }
}