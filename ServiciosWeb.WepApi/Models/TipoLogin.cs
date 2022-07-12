using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class TipoLogin
    {
        public decimal TipoLoginId { get; set; }
        public string TipoLoginNombreTipo { get; set; }
        public int Estado { get; set; }
    }
}