using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class Comentario
    {
        public decimal PersonaPostId { get; set; }
        public decimal PostId { get; set; }
        public string ComentarioPersonaNombre { get; set; }
        public string ComentarioPersonaFotoUrl { get; set; }
        public string ComentarioPersonaDescripcion { get; set; }
        public DateTime ComentarioFecha { get; set; }
    }
}