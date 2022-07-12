using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public  class LogSesionesPersona:BEEntidad
    {

        public decimal LogSesionesPersonaId { get; set; }
        public decimal PersonaId { get; set; }
        public DateTime LogSesionesPersonaFecha { get; set; }
        public decimal TipoLoginId { get; set; }
        public string LogSesionesPersonaVersion { get; set; }
        public string LogSesionesPersonaSO { get; set; }
       public string LogSesionesPersonaIdioma { get; set; }
    }
}
