using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RendimientoOperativo : BEEntidad
    {

        public int IndiceMesActual { get; set; }
        public long MesActual { get; set; }
        public int IndiceMesAnterior { get; set; }
        public long MesAnterior { get; set; }
        public int IndiceYearActual { get; set; }
        public long YearActual { get; set; }

    }
}
