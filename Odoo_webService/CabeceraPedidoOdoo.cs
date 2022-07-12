using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo_webService
{
   public  class CabeceraPedidoOdoo
    {
    public int id { get; set; }
public string codigo { get; set; }
public string fecha { get; set; }
public string cod_cliente { get; set; }
public decimal total { get; set; }
    public object detalle { get; set; }
    }
}
