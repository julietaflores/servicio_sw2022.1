using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Controllers
{
    public class message
    {
        public notification notification { get; set; }
        public android android { get; set; }
        public List<string> token { get; set; }
    }
}