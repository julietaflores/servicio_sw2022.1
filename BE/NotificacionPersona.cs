using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class NotificacionPersona : BEEntidad
    {
        public decimal NotificacionPersonaId { get; set; }
        public string RequiereServicioId { get; set; }
        public decimal PersonaId { get; set; }
        public decimal TipoEstadoId { get; set; }
        public string NotificacionPersonaTitulo { get; set; }
        public string NotificacionPersonaDescripcion { get; set; }
        public DateTime NotificacionPersonaFechaRegistro { get; set; }
        public string NotificacionPersonaFragment { get; set; }
        public string NotificacionPersonaIcono { get; set; }
        public decimal EstadoNotificacionId { get; set; }
        public decimal ConceptoNotificacionId { get; set; }
        public EstadoNotificacion estadoNotificacion { get; set; }
        public ConceptoNotificacion conceptoNotificacion { get; set; }
        public TipoEstadoNotificacion tipoEstadoNotificacion { get; set; }
    }
    public enum relNotificacionPersona
    {
        estadoNotificacion, conceptoNotificacion, tipoEstadoNotificacion
    }
}
