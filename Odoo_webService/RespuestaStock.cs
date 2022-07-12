using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo_webService
{
    public class RespuestaStock
    {
        public object DetalleStock { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
