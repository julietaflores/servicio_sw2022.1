using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoPost : BEEntidad
    {
        public decimal TipoPostId { get; set; }
        public string TipoPostNombre { get; set; }
    }
}