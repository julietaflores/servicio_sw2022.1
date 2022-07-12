using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchServices.Models
{
    public class Respuesta
    {
        public int estado { get; set; }
        public Object valor { get; set; }
        public string mensaje { get; set; }
    }
}