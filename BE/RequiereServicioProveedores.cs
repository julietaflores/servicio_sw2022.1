using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RequiereServicioProveedores : BEEntidad
    {
        public string RequiereServicioId { get; set; }
        public decimal RequiereServicioProveedoresId { get; set; }
        public bool RequiereServicioProveedoresAdj { get; set; }
        public decimal RequiereServicioProvCotizacion { get; set; }
        public System.DateTime RequiereServicioProvFHTrabajo { get; set; }
        public string RequiereServicioProvDescipcion { get; set; }
        public decimal ServicioPersonaId { get; set; }
        public System.DateTime RequiereServicioProvFHResp { get; set; }
        public Nullable<decimal> StatusRequiereId { get; set; }
        public StatusRequiere statusRequiere { get; set; }
        public ServicioPersona servicioPersona { get; set; }

        public RequiereServicio requiereServicio { get; set; }
    }
    public enum relReqServProv
    {
        servicioPersona, statusRequiere, requiereServicio
    }
}
