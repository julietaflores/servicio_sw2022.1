using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class cobranzaCBA : BEEntidad

    {
        public decimal cobranzaCBAId { get; set; }
        public decimal personaId { get; set; }
        public string cobranzaCBANombre { get; set; }
        public string cobranzaCBACarnet { get; set; }
        public string cobranzaCBACodigoEstudiante { get; set; }
        public string cobranzaCBANivel { get; set; }
        public string cobranzaCBAModulo { get; set; }
        public Boolean cobranzaCBAQuiereLibro { get; set; }
        public string cobranzaCBALibro { get; set; }
        public Boolean cobranzaCBABecado { get; set; }
        public decimal personaDireccionId { get; set; }
        public DateTime cobranzaCBAFechaRegistro { get; set; }

        public DateTime cobranzaCBAFechaCobranza { get; set; }
        public string cobranzaCBAEstado { get; set; }

        public string cobranzaCBAUID { get; set; }


    }

}
