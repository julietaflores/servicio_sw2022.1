using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Controllers
{
    internal class Requerimientos
    {
    public string    idPersona { get; set; }
     public   string   idRequest { get; set; }
        public string servicioId { get; set; }
         public string fecha { get; set; }
        public string hora { get; set; }
        public string estado { get; set; }
        public string descripcion { get; set; }
        public string arch1  { get; set; }
        public string arch2  { get; set; }
        public string arch3 { get; set; }
        public string arch4 { get; set; }
        public string dirLatitud { get; set; }
        public string dirLong { get; set; }
        public string dirTitulo { get; set; }
        public string tipoSolicitud { get; set; }
        public  string diasRest { get; set; }
        public string cantOfer { get; set; }
        public string colorServicio { get; set; }
        public string idEstado { get; set; }
    }
}