using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TipoEstadoNotificacion : BEEntidad
    {
        public decimal TipoEstadoId { get; set; }
        public string TipoEstadoNombre { get; set; }
    }
}
