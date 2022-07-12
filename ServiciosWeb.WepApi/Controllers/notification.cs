using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Controllers
{
    public class notification
    {
        public string title { get; set; }
        public string body { get; set; }
        public click_action click_action { get; set; }
    }
}