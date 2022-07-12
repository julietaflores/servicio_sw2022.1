using System;

namespace ServiciosWeb.WepApi.Controllers
{
    internal class Sincronizacion
    {
        public long Create { get; set; }
        public object RequiereServicioS { get; set; }
        public object ServAsigS { get; set; }
        public System.Collections.Generic.List<BE.RequiereServicioProveedoresF> RequiereServicioProveedoresS { get; set; }
    }
}