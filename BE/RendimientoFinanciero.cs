using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RendimientoFinanciero : BEEntidad
    {

        public int IndiceMesActual { get; set; }
        public decimal MesActual { get; set; }
        public int IndiceMesAnterior { get; set; }
        public decimal MesAnterior { get; set; }
        public int IndiceYearActual { get; set; }
        public decimal YearActual { get; set; }

    }
}
