using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ServAsigCosto : BEEntidad

    {
        public string ServAsigCostoId { get; set; }
        public string ServAsigId { get; set; }
        public decimal ConceptoCostoId { get; set; }
        public decimal ServAsigCostoValor { get; set; }
        public ConceptoCosto conceptoCosto { get; set; }


    }
    public enum relservAsigCosto {

        conceptoCosto
    }
}
