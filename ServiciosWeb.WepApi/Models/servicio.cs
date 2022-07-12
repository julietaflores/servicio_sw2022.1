using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class Servicio
    {
        public decimal ServicioId { get; set; }
        public string ServicioNombre { get; set; }
        public string ServicioURLFoto { get; set; }
        public decimal CategoriaServicioId { get; set; }
        public string ServicioUsuario { get; set; }
        public System.DateTime ServicioFechaHoraMod { get; set; }
        public string ServicioKeyWords { get; set; }
        public CategoriaServicio categoriaServicio { get; set; }
        public ServicioRequerimiento servicioRequerimiento { get; set; }
    }
}