using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class PackBusquedaServicio
    {
        public List<BE.CategoriaServicio> categoriaServicio { get; set; }
        public List<BE.Servicio> servicios { get; set; }
    }
}