using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PersonaGeoLocalizacion : BEEntidad
    {
        public decimal PersonaId { get; set; }
        public decimal PersonaGeoLocalizacionId { get; set; }
        public string PersonaGeoLocalizacionLangLat { get; set; }
        public System.DateTime PersonaGeoLocalizacionFechaHor { get; set; }
    }
}
