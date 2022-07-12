using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class PackRequiereServicio_Prov
    {
        public List<ServiciosWeb.Datos.Modelo.RequiereServicioProveedores> requiereServicioProveedores { get; set; }
        public List<ServiciosWeb.WepApi.Models.RequiereServicio> requiereServicio { get; set; }

    }
}