using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo_webService
{
    class ordenes_detalle
    {
        public int Nro { get; set; }
        public string order_id { get; set; }
        public string codigo { get; set; }
        public decimal cantidad { get; set; }
        public string u_m { get; set; }
        public decimal precio { get; set; }
        public decimal sub_Total { get;set; }
    }
}
