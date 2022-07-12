using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class Region
    {
        public decimal RegionId { get; set; }
        public string RegionNombre { get; set; }
       public decimal PaisId { get; set; }
    }
}