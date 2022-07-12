using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo_webService
{
   public class RespuestaOdoo
    {
        public int estado { get; set; }
        public object valor { get; set; }
        public string mensaje { get; set; }
    }
}
