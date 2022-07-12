using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class PackCatSer_CatServDesta
    {

        public List<ServiciosWeb.WepApi.Models.CategoriaServicio> categoriaServicio { get; set; }
        public List<ServiciosWeb.WepApi.Models.CategoriaServicioDestacada> categoriaservicioDestacado { get; set; }
    }
}