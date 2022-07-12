using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RequiereServicioProveedoresF:BEEntidad
    {
        public string RequiereServicioId { get; set; }
        public string ProveedorNombre { get; set; }

        public int  EstadoRecepcion { get; set; }
        public Decimal StatusRequiereId { get; set; }
        public  List<IdiomaS> EstadoProvReqId { get; set; }

        public string PersonaProveedorId { get; set; }
        
    }
    public enum relReqServProvF
    {
        EstadoProvReqId
    }
}
