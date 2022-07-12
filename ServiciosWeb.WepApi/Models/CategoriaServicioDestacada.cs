using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class CategoriaServicioDestacada
    {
        public decimal CategoriaServicioId { get; set; }
        public decimal CategoriaServicioDestacadaId { get; set; }
        public string CategoriaServicioDestacadaURL { get; set; }
        public System.DateTime CategoriaServicioDestacadaFini { get; set; }
        public System.DateTime CategoriaServicioDestacadaFFin { get; set; }
    }
}