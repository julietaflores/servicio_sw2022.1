using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Conversacion:BEEntidad
    {
        public decimal ConversacionId { get; set; }
        public decimal ConversacionPersonaIdEmisor { get; set; }
        public decimal ConversacionPersonaIdReceptor { get; set; }
        public string ServAsigId { get; set; }
        public string ConversacionContenido { get; set; }
        public DateTime ConversacionFechaHora { get; set; }



    }
}
