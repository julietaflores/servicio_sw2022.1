using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ServicioTexto:BEEntidad
    {


        public decimal ServicioTextoId { get; set; }
        public decimal ServicioId { get; set; }
        public string ServicioTextodesc { get; set; }
    }
}
