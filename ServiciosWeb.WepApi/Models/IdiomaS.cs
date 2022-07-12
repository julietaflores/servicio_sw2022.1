using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class IdiomaS
    {
        public decimal IdiomaId { get; set; }
        public string IdiomaSigla { get; set; }
        public string Definicion { get; set; }
        public int Estado { get; set; }
    }
}