using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Pais : BEEntidad
    {
        public decimal PaisId { get; set; }
        public string PaisNombre { get; set; }
        public Moneda Moneda { get; set; }
    }

    public enum relpais
   {
        moneda
   }
}
