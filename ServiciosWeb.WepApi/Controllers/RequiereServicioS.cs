using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Controllers
{
    internal class RequiereServicioS
    {

        public string RequiereServicioid { get; set; }
        public decimal PersonaId { get; set; }
        public string PersonaNombre { get; set; }
        public string PersonaURLFoto { get; set; }
        public string RequerimientoPrecio { get; set; }
        public string RequiereServicioFHDeseada { get; set; }
        public string RequiereServicioDescripcion { get; set; }
        public string EstadoReqServId { get; set;}
        public string PersonaDireccionS { get; set; }
        public string PersonaDireccionNombre { get; set; }
        public List<BE.IdiomaS> ServicioId { get; set; }
        public string RequiereServicioURLFoto1 { get; set; }
        public string RequiereServicioURLFoto2 { get; set; }
        public string RequiereServicioURLFoto3 { get; set; }
        public string RequiereServicioURLVideo { get; set; }

        public string PersonaTelefono { get; set; }
        public string RequiereServicioFHCaduca { get; set; }



    }
}