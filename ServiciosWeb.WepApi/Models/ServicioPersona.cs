using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class ServicioPersona
    {
        public decimal ServicioPersonaId { get; set; }
        public decimal PersonaId { get; set; }
        public decimal ServicioId { get; set; }
        public string ServicioPersonaURLFoto { get; set; }
        public decimal EstadoServicioId { get; set; }
        public decimal StatusServicioId { get; set; }
        public string ServicioPersonaUsuario { get; set; }
        public System.DateTime ServicioPersonaFechaHora { get; set; }
        public decimal ServicioPersonaGaleriaLast { get; set; }
        public short ServicioPersonaHorarioLast { get; set; }
        public string ServicioPersonaNombre { get; set; }
        public string ServicioPersonaDescripcion { get; set; }
        public bool ServicioPersonaReqDelivery { get; set; }
        public decimal MonedaId { get; set; }
        public Nullable<decimal> PersonaDireccionId { get; set; }
        public Nullable<bool> ServicioPersonaEnDomicilio { get; set; }
        public Nullable<bool> ServicioPersonaEnOficina { get; set; }
        public PersonaDireccion personaDireccion { get; set; }
        public RankingProveedor rankingProveedor { get; set; }
        public List<ServicioPersonaGaleria> servicioPersonaGaleria { get; set; }
        public List<ServicioPersonaHorario> servicioPersonaHorario { get; set; }
    }
}