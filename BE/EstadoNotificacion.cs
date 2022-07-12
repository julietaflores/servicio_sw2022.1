using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
     public class EstadoNotificacion:BEEntidad
    {
        public decimal EstadoNotificacionId { get; set; }
        public string EstadoNotificacionNombre { get; set; }
        public string EstadoNotificacionImagen { get; set; }
        public string EstadoNotificacionColor { get; set; }
    }
}
