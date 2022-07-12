using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class ServicioPersonaHorario
    {
        public decimal ServicioPersonaId { get; set; }
        public decimal ServicioPersonaHorarioId { get; set; }
        public decimal DiaSemanaId { get; set; }
        public DateTime  ServicioPersonaHorarioHoraIni1 { get; set; }
        public DateTime ServicioPersonaHorarioHoraFin1 { get; set; }
        public DateTime ServicioPersonaHorarioHoraIni2 { get; set; }
        public DateTime ServicioPersonaHorarioHoraFin2 { get; set; }
        public DiaSemana diaSemana { get; set; }
    }
}