using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Region:BEEntidad
    {
        public decimal RegionId { get; set; }
        public string RegionNombre { get; set; }
        public decimal PaisId { get; set; }

        public Pais pais { get; set; }
    }

    public enum relRegion
    {
        pais
    }
}
