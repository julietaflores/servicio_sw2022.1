using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Ciudad:BEEntidad
    {

        public decimal CiudadId { get; set; }
        public string CiudadNombre { get; set; }
        public decimal RegionId { get; set; }
        public string CiudadGoogleTag { get; set; }
        public string CiudadGeolocalizacion { get; set; }
        public int Estado { get; set; }
        public Region Region { get; set; }
        public ConfiguracionCiudad configuracionCiudad { get; set; }


    }
    public enum relciudad
    {
        region,configuracionCiudad
    }
}
