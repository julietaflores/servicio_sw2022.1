using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class IdiomaS:BEEntidad
    {
        public decimal IdiomaId { get; set; }
        public string IdiomaSigla { get; set; }
        public string Definicion { get; set; }
        public int Estado { get; set; }
    }
}
