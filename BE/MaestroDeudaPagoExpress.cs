using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class MaestroDeudaPagoExpress : BEEntidad
    {
        public string MaestroDeudaId { get; set; }
        public decimal PersonaId { get; set; }
        public decimal TipoDeudaId { get; set; }
        public decimal MaestroDeudaImporte { get; set; }
        public DateTime MaestroDeudaVencimiento { get; set; }
        public bool MaestroDeudaPago { get; set; }
        public string MaestroDeudaObservacion { get; set; }
        public string MaestroDeudaMotivo { get; set; }
        public DateTime MaestroDeudaFechaRegistro { get; set; }
        public string MaestroDeudaUsuarioMod { get; set; }
        public DateTime? MaestroDeudaFechaPago { get; set; }
    }
}
