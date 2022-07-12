using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoDireccion : BEEntidad
    {
        public decimal TipoDireccionId { get; set; }
        public string TipoDireccionNombreTipo { get; set; }
        public int Estado { get; set; }
    }
}
