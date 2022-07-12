using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo_webService
{
   public  class DetallePedidoOdoo
    {

     public decimal nro { get; set; }
     public string codigo { get; set; }
       public decimal cantidad { get; set; }
       public decimal precio { get; set; }
       public decimal subtotal { get; set; }
        public string u_m { get; set; }

    }
}
