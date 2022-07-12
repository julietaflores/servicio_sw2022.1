using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public  class Siniestro_Estado:BEEntidad
    {
        public decimal SiniestroId { get; set; }
        public decimal SiniestroEstadoId { get; set; }
        public DateTime Siniestro_EstadoFechaHoraMod { get; set; }
        public string Siniestro_EstadoObservacion { get; set; }


    }
}
