using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class Ciudad
    {
        public decimal CiudadId { get; set; }
        public string CiudadNombre { get; set; }
        public decimal RegionId { get; set; }
        public string CiudadGoogleTag { get; set; }
        public string CiudadGeolocalizacion { get; set; }
        public int Estado { get; set; }

    }
}