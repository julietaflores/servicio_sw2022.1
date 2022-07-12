using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public partial class RespuestaPack<TGenerico>
    {
        public int estado { get; set; }
        public TGenerico valor { get; set; }
        public string mensaje { get; set; }
    }
}

