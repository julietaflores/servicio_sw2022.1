using System;
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
    public class PersonaControllerBackup : ApiController
    {     
       
        SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["con"]);

        BC.Persona bcPersona = null;
        private PersonaManager perManager = null;
        private ControladorManager contManager = null;


        public PersonaControllerBackup()
        {
            bcPersona = new BC.Persona("cadenaCnx");
            perManager = new PersonaManager("cadenaCnx");
            contManager = new ControladorManager("cadenaCnx");
        }














        [HttpPut]
        [Route("api/ActualizarPersona")]
        public IHttpActionResult actualizarPersona(BE.Persona persona)
        {
            Respuesta resp = new Respuesta();
            persona.TipoEstado = BE.TipoEstado.Modificar;

            resp = perManager.actualizarPersona(ref persona);

            return Ok(resp);

        }

        // PUT: api/Persona/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersona(decimal id, BE.Persona persona)
        {
            Respuesta resp = new Respuesta();
            persona.TipoEstado = BE.TipoEstado.Modificar;
            resp = perManager.savePersona(id, ref persona);

            return Ok(resp);

        }



        [HttpPost]
        [ResponseType(typeof(BE.Persona))]
        public IHttpActionResult PostPersona(BE.Persona persona, string lang)
        {
            Respuesta resp = new Respuesta();

            int cantidad = perManager.ValidarExistePersonaCorreo(persona.PersonaCorreo, persona.PersonaCodigoTelefono, persona.PersonaTelefono);
            if (cantidad == 0)
            {
                persona.TipoEstado = BE.TipoEstado.Insertar;
                resp = perManager.savePersona(0, ref persona);
            }
            else
            {

                //BE.Persona persona1 = bcPersona.BuscarPersonaxId(cantidad);
                //persona.PersonaId = persona1.PersonaId;
                //if (persona1.TipoLoginId == 1)
                //{
                //    persona.PersonaFacebookUid = persona1.PersonaFacebookUid;

                //}
                //if (persona1.TipoLoginId == 2)
                //{
                //    persona.PersonaGmailUid = persona1.PersonaGmailUid;

                //}
                //if (persona1.TipoLoginId == 3)
                //{
                //    persona.PersonaPhoneUid = persona1.PersonaPhoneUid;

                //}

                //persona.TipoEstado = BE.TipoEstado.Modificar;
                //resp = perManager.savePersona(0, ref persona);

            }
            return Ok(resp);
        }




        [HttpGet]
        [Route("api/ActualizarPIN")]//
        public Respuesta ActualizarPIN(decimal PersonaId, string PersonaCorreo, string lang)
        {
            Respuesta resp = new Respuesta();
            ///////////
            string pin = perManager.GenerarPIN();

            DataRow data = perManager.ListadoDatosNotificacion(lang);
            var title = data["title"].ToString();
            var body = data["body"].ToString();
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            envioCorreo.PersonaCorreo = PersonaCorreo;
            envioCorreo.Subject1 = title;
            envioCorreo.Body = body + pin;
            envioCorreo.Fecha = DateTime.Now;
            envioCorreo.TipoCorreo = "nuevoUsuario";
            envioCorreo.Estado = "Pendiente";
            envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
            contManager.saveEnvioCorreo(ref envioCorreo);


            //////////////

            BE.Persona persona1 = bcPersona.BuscarPersonaxId(PersonaId);
            persona1.PersonaCodigoVerificacion = pin;
            persona1.TipoEstado = BE.TipoEstado.Modificar;
            resp = perManager.savePersona(0, ref persona1);

            if (resp.estado == 1)
            {
                resp.valor = pin;
            }
            return resp;


        }

















        [HttpPut]
        [Route("api/ActualizarGeolocalizacion")]
        public IHttpActionResult ActualizarGeolocalizacion(decimal IdPersona, string PersonaGeoReal)
        {
            Respuesta resp = new Respuesta();
            resp = perManager.ActualizarGeolocalizacion(IdPersona, PersonaGeoReal);

            return Ok(resp);

        }


        public IHttpActionResult GetPersona()
        {
            // return db.Persona;
            Respuesta resp = new Respuesta();
            resp = perManager.VerPersona();
            return Ok(resp);


        }
        // GET: api/Persona/5
        [ResponseType(typeof(BE.Persona))]
        public IHttpActionResult GetPersona(string PersonaTokenId)
        {
            // Persona persona = db.Persona.Find(PersonaTokenId);
            Respuesta resp = new Respuesta();
            resp = perManager.VerPersonaPersonaTokenId(PersonaTokenId);
            return Ok(resp);
            //pROCE
        }
   

        [HttpGet]
        [Route("api/ObtenerPersonav2")]
        public IHttpActionResult GetPersonaXPersonaIdv2(decimal PersonaId)
        {
            Respuesta resp = new Respuesta();
            try
            {
                
                BE.Persona persona = bcPersona.BuscarPersonaxId(PersonaId);

                if (persona == null)
                {
                    resp.estado = 2;
                    resp.mensaje = "Listado null";
                }
                else
                {
                    resp.estado = 1;
                    resp.mensaje = "";
                }
                resp.valor = persona;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.mensaje = ex.Message;
            }



            return Ok(resp);
            //pROCE
        }
      
    




        [HttpGet]
        [Route("api/ObtenerPersona")] ///METODO UTILIZADO

        public IHttpActionResult GetPersonaXPersonaId(decimal PersonaId)
        {


            Respuesta resp = new Respuesta();
            resp = perManager.BuscarPersonaxId(PersonaId);

            return Ok(resp);



            //pROCE
        }

        [HttpGet]
        [Route("api/ObtenerPersonaUID")] //METODO UTILIZADO

        public IHttpActionResult GetPersonaXPersonaUId(string uid)
        {


            Respuesta resp = new Respuesta();
            resp = perManager.BuscarPorUID(uid);

            return Ok(resp);



            //pROCE
        }


  


        [HttpGet]
        [Route("api/Mostrar_Usuarios")]
        public IHttpActionResult Mostrar_Usuarios()
        {
            Respuesta resp = new Respuesta();
            try
            {
                resp = perManager.ListadoUsuariosTotales();
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.mensaje = ex.Message;
            }
            return Ok(resp);

        }




        [HttpGet]
        [Route("api/Publicidad_Service_Web")]
        public IHttpActionResult Publicidad_Service_Web()
        {
            Respuesta resp = new Respuesta();
            try
            {
                resp = perManager.Publicidad_Service_Web();

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.mensaje = ex.Message;
            }
            return Ok(resp);
        }




        [HttpGet]
        [Route("api/registrar_promocionp")]
        public IHttpActionResult registrar_promocionp(decimal PersonaId)
        {
            Respuesta resp = new Respuesta();
            resp = perManager.registrar_promocionp(PersonaId);
            return Ok(resp);
        }






        [HttpGet]
        [Route("api/ValidarLoginWeb")]
        public IHttpActionResult ValidarLoginWeb(string PersonaUsuario,string PersonaClave)
        {
            Respuesta resp = new Respuesta();
            resp = perManager.ValidarLoginWeb(PersonaUsuario, PersonaClave);
            return Ok(resp);
        }




 

        #region "METODOS ACTUALIZADOS VERSION 2" 
       
        [HttpGet]
        //  [ActionName("ValidarExistePersona")]
        [Route("api/ValidarExistePersona")]//MEOTODO QUE VALIDA QUE EL CORREO NO SE REPITA
        public IHttpActionResult ValidarExistePersona2(string PersonaCorreo, decimal TipoLoginId, string PersonaCodigoTelefono, string PersonaTelefono, string PersonaUID)
        {
            Respuesta resp = new Respuesta();
            resp = perManager.ValidarExistePersona(PersonaCorreo, TipoLoginId, PersonaCodigoTelefono, PersonaTelefono, PersonaUID, "es");

            return Ok(resp);


        }
        [HttpGet]

        [Route("api/ConfirmarCorreoPersona")]
        public IHttpActionResult ConfirmarCorreoPersona(decimal PersonaId, string pin )
        {
            Respuesta resp = new Respuesta();
            resp = perManager.ConfirmarCorreoPersona(PersonaId,pin);

            return Ok(resp);
        }

        #endregion

        #region "METODOS WEB"
        [HttpGet]
        //  [ActionName("ValidarExistePersona")]
        [Route("api/ValidarExistePersona_Web")]//MEOTODO QUE VALIDA QUE EL CORREO NO SE REPITA
        public Respuesta ValidarExistePersona_Web(string usuario, string password)
        {
            Respuesta resp = new Respuesta();
           resp = perManager.ValidarExistePersona_Web(usuario, password);

            return resp;


        }





        #endregion
      









        [HttpGet]
        [Route("api/EnviarCorreo")]
        public  string EnviarCorreo(string Personacorreo,string lang)
        {
            string pin = perManager.GenerarPIN();

            DataRow data = perManager.ListadoDatosNotificacion(lang);
            var title = data["title"].ToString();
            var body = data["body"].ToString();
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            envioCorreo.PersonaCorreo = Personacorreo;
            envioCorreo.Subject1 = title;
            envioCorreo.Body = body + pin;
            envioCorreo.Fecha = DateTime.Now;
            envioCorreo.TipoCorreo = "nuevoUsuario";
            envioCorreo.Estado = "Pendiente";
            envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
            contManager.saveEnvioCorreo(ref envioCorreo);

            return pin;                                    
        }

        [HttpGet]
        [Route("api/ExistePersona")]
        public IHttpActionResult ExistePersona(decimal PersonaId)
        {
            Respuesta resp = new Respuesta();
            resp = perManager.ExistePersona(PersonaId);

            return Ok(resp);


        }









    }
}