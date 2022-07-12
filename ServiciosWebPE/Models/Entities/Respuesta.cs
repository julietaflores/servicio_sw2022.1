using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWebPE.Models.Entities
{
    public class Respuesta
    {
        
            public string codigo_respuesta { get; set; }
            public Object datos { get; set; }
            public string mensaje_respuesta { get; set; }
        
    }
}