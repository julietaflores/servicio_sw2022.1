using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class CategoriaServicio
    {
        public decimal CategoriaServicioId { get; set; }
        public string CategoriaServicioNombre { get; set; }
        public string CategoriaServicioURLFoto { get; set; }
        public decimal CiudadId { get; set; }
        public string CategoriaServicioDescripcion { get; set; }
        public string CategoriaServicioUsuario { get; set; }
        public Nullable<System.DateTime> CategoriaServicioFechaHoraMod { get; set; }
        public Nullable<decimal> CategoriaServicioHijoId { get; set; }
        public Nullable<decimal> CategoriaServicioDestLast { get; set; }


    }
}