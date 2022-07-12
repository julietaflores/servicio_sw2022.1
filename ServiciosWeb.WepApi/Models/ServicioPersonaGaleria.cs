using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class ServicioPersonaGaleria
    {
        public decimal ServicioPersonaId { get; set; }
        public decimal ServicioPersonaGaleriaId { get; set; }
        public string ServicioPersonaGaleriaURLFoto { get; set; }
    }
}