using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class MediBook:BEEntidad
    {
        public string RequiereServicioId { get; set; }
       public string Name { get; set; }
       public string Lastname { get; set; }
       public string Lastnamemother { get; set; }
      public string Email { get; set; }
      public string Cellphone { get; set; }
      public  string Ci { get; set; }
      public string Expedition { get; set; }
       public string Birthday { get; set; }
      public string  user { get; set; }
      public string password { get; set; }
    }
}
