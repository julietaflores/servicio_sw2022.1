using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWebPE.Models.Entities
{
    public class RespuestaPago
    {

        public string codigo_respuesta { get; set; }
        public string mensaje_respuesta { get; set; }
        public string autorizacion { get; set; }
    }
}