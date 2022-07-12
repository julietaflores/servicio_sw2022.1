using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo_webService
{
    class ordenes
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string fecha_creacion { get; set; }
        public string fecha_entrega { get; set; }
        public string fecha_prevista { get; set; }
        public string cod_cliente { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; }
        public string id_cliente { get; set; }
    }
}
