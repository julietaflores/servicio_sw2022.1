using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Controllers
{
    public class RespuestaHuawei
    {

        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
    }
}