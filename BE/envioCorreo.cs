using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public  class envioCorreo:BEEntidad
    {
        public string PersonaCorreo { get; set; }
        public string Subject1 { get; set; }
        public string Body { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoCorreo { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public int Id { get; set; }

    }
}
