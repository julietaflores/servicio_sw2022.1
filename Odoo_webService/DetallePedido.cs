using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo_webService
{
  public  class DetallePedido
    {
        public int Nro { get; set; }
        public string Codigo { get; set; }
        public int Cantidad { get; set; }
        public string Und_Medida { get; set; }
    
        public float ICE { get; set; }
      

    
    }
}
