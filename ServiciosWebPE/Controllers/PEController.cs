using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ServiciosWebPE.Models;
using ServiciosWebPE.Models.Entities;
using BE;

namespace ServiciosWebPE.Controllers
{
    public class PEController : ApiController
    {
        // GET: PE
        private PEManager  pEManager = null;
        public PEController()
        {
            pEManager = new PEManager("cadenaCnx");
        }

        [HttpGet]
       [Authorize(Roles = "USUARIO")]
        [Route("api/Consulta")]
        public IHttpActionResult VerDeuda(string tipo_documento, string documento)
        {
            Respuesta resp = new Respuesta();
            resp = pEManager.VerDeudaMaestro(tipo_documento,documento);
            return Ok(resp);
        }
        [HttpPost]
       [Authorize(Roles = "USUARIO")]
        [Route("api/Pago")]
        public IHttpActionResult RealizarPago(BE.Pago obj)
        {
            
            RespuestaPago resp = new RespuestaPago();
            resp = pEManager.RealizarPagoMaestro(obj);
            return Ok(resp);
        }
        [HttpPost]
      [Authorize(Roles = "USUARIO")]
        [Route("api/Anulacion")]
        public IHttpActionResult RevertirPago(string referencia)
        {

            RespuestaPago resp = new RespuestaPago();
            resp = pEManager.RevertirPagoMaestro(Convert.ToString(referencia));
            return Ok(resp);
        }



    }
}