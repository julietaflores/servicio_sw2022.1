using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class LogMovil:BEEntidad
    {
       public decimal LogId { get; set; }
        public DateTime LogFechaRegistro { get; set; }
        public decimal LogPersonaId { get; set; }
        public string LogMetodo { get; set; }
        public string LogInformacionTelefono { get; set; }
        public string LogVersion { get; set; }

        public string LogDescripcion { get; set; }
        public string LogObservacion1 { get; set; }
        public string LogObservacion2 { get; set; }
        public string LogObservacion3 { get; set; }
    }
}
