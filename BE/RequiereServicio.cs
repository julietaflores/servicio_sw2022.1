using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
        public class RequiereServicio:BEEntidad
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
            public string RequiereServicioUID { get; set; }
            public string RequiereServicioGeoInmediato { get; set; }
            public bool RequiereServicioInmediato { get; set; }
           public bool requiereServicioOtros { get; set; }
            public Servicio servicio { get; set; }
            public EstadoReqServ estadoReqServ { get; set; }
            public PersonaDireccion personaDireccion { get; set; }
            public List<RequiereServicioProveedores> RequiereServicioProveedores { get; set; }
            public List<requiereServicioDetalle> requiereServicioDetalle { get; set; }
            public List<requiereServicioDetalleWeb> requiereServicioDetalleWeb { get; set; }
            public Persona persona { get; set; }
            public ServAsig servAsig { get; set; }

            public List<IdiomaS> IdiomaS { get; set; }
            public List<IdiomaS> IdiomaServ { get; set; }
             public List<RequiereServicioProveedoresF> RequiereServicioProveedoresF { get; set; }
}
    public enum relRequiereServicio
    {
        servicio, estadoReqServ, personaDireccion, persona, servAsig, requiereServicioProveedores, reqServProvAdj,reqServProvPersona, requiereServicioDetalle, requiereServicioDetalleWeb,IdiomaS, IdiomaServ, RequiereServicioProveedoresF
    }


}
