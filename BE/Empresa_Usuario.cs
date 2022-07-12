using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   
    public class Empresa_Usuario : BEEntidad
    {
        public int Nit { get; set; }
        public int UserId { get; set; }
        public DateTime Fecha_Asig { get; set;}

        public BE.UserProfile UserProfile { get; set; }
    }
    public enum relEmpresa_Usuario
    {
        UserProfile
    }
}
