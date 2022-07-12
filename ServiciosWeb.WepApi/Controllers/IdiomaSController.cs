﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServiciosWeb.WepApi.Models;
using ServiciosWeb.WepApi.Models.Entities;
using System.Web.Http.Cors;




using Newtonsoft.Json;

using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json.Linq;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

using System.Globalization;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;




namespace ServiciosWeb.WepApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IdiomaSController : ApiController
    {     
       
        SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["con"]);

        BC.IdiomaS bcIdiomaS = null;
        private IdiomaSManager tlManager = null;
        private ControladorManager contManager = null;


        public IdiomaSController()
        {
            bcIdiomaS = new BC.IdiomaS("cadenaCnx");
            tlManager = new IdiomaSManager("cadenaCnx");
            contManager = new ControladorManager("cadenaCnx");
        }

     

        [HttpGet]
        [Route("api/ObtenerListadoIdiomaS")]
        public IHttpActionResult ObtenerListadoIdiomaS(string lang)
        {
            Respuesta resp = new Respuesta();
            resp = tlManager.ListadoIdiomaS(lang);
            return Ok(resp);
        }
      
      


        









    }
}