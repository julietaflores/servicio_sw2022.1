using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class TipoPersona
    {
        public decimal TipoPersonaId { get; set; }
        public string TipoPersonaNombreTipo { get; set; }
        public int Estado { get; set; }
    }
}