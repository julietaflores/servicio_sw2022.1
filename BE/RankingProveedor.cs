using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BE
{
    public class RankingProveedor:BEEntidad 
    {
        public int NroTrabajos { get; set; }
        public decimal Ranking { get; set; }
        public int NroPost { get; set; }
        public decimal Distancia { get; set; }
    }
}