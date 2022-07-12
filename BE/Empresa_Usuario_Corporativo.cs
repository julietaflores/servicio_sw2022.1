using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Empresa_Usuario_Corporativo:BEEntidad
    {
        public int Nit { get; set; }
        public decimal PersonaId { get; set; }
        public DateTime Fecha_Asig { get; set; }
        public int UserId { get; set; }
        public string Asignacion_Nombre_UserId { get; set; }

        public BE.Persona Persona { get; set; }
    }
    public enum relEmpresa_Usuario_Corporativo
    {
      Persona
    }
}
