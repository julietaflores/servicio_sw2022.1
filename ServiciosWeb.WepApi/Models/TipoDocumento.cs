using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class TipoDocumento
    {
        public decimal TipoDocumentoId { get; set; }
        public string TipoDocumentoNombre { get; set; }
        public int Estado { get; set; }
    }
}