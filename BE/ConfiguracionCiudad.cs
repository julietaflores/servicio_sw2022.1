using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ConfiguracionCiudad:BEEntidad
    {
        public decimal ConfiguracionCiudadId { get; set; }
        public decimal CiudadId { get; set; }
        public decimal BilleteraIdSeguro { get; set; }
        public decimal BilleteraIdServiceWeb { get; set; }
        public decimal ConfiguracionCiudadValorSeguro { get; set; }
        public decimal ConfiguracionCiudadValorBroker { get; set; }
    }
   
}
