using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
     public class TermCondPol:BEEntidad
    {
       public decimal PaisId { get; set; }
        public string TermCond { get; set; }

        public string PoliticasSeg { get; set; }

        public string MensajeSeguro { get; set; }

    }
}
