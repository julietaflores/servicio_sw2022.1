using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BE
{
    public class ServicioPersonaGaleria : BEEntidad
    {
        public decimal ServicioPersonaId { get; set; }
        public decimal ServicioPersonaGaleriaId { get; set; }
        public string ServicioPersonaGaleriaURLFoto { get; set; }
    }
}