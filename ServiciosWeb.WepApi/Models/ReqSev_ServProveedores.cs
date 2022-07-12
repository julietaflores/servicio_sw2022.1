using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiciosWeb.Datos.Modelo;
namespace ServiciosWeb.WepApi.Models
{
    public class ReqSev_ServProveedores
    {

        public string RequiereServicioId { get; set; }
        public decimal PersonaId { get; set; }
        public System.DateTime RequiereServicioFechaHoraReq { get; set; }
        public System.DateTime RequiereServicioFHCaduca { get; set; }
        public decimal EstadoReqServId { get; set; }
        public System.DateTime RequiereServicioFHDeseada { get; set; }
        public string RequiereServicioDescripcion { get; set; }
        public string RequiereServicioURLFoto1 { get; set; }
        public string RequiereServicioURLFoto2 { get; set; }
        public string RequiereServicioURLFoto3 { get; set; }
        public string RequiereServicioURLVideo { get; set; }
        public decimal RequiereServicioProvLast { get; set; }
        public Nullable<decimal> PersonaDireccionId { get; set; }
        public Nullable<decimal> ServicioId { get; set; }
        public Nullable<System.DateTime> RequiereServicioFechaMod { get; set; }

        public List<RequiereServicioProveedoresM> RequiereServicioProveedores { get; set; }

        public ServicioPersona serviciopersona_Persona { get; set; }

    }
}