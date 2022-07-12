using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Genero : BEEntidad
    {
        public decimal GeneroId { get; set; }
        public string GeneroNombreTipo { get; set; }


        public int  Estado { get; set; }
    }
}
