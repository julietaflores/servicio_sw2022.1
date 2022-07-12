using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo_webService
{
    class cabeceraPedido
    {
        public string Id { get; set; }
        public string Fecha { get; set; }
        public string FechaEntrega { get; set; }
        public string Id_Cliente { get; set; }     
        public string id_Distribuidora { get; set; }
    
        public List<DetallePedido> DetallePedido { get; set; }

  
    }
}
