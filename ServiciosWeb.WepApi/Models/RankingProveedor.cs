using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class RankingProveedor
    {
        public int NroTrabajos { get; set; }
        public decimal Ranking { get; set; }
        public int NroPost { get; set; }
        public decimal Distancia { get; set; }
    }
}