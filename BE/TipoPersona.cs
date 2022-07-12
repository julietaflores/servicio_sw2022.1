using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoPersona : BEEntidad
    {
        public decimal TipoPersonaId { get; set; }
        public string TipoPersonaNombreTipo { get; set; }
        public int Estado { get; set; }
    }
}
