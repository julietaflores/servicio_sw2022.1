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
using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.WepApi.Models;
using ServiciosWeb.WepApi.Models.Entities;
using System.Web.Http.Cors;

namespace ServiciosWeb.WepApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PersonaDireccionController : ApiController
    {
     
        private ControladorManager contManager = null;
        private PersonaDireccionManager personaDireccionManager = null;


        
        
        SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["con"]);

        public PersonaDireccionController()
        {
            contManager = new ControladorManager("cadenaCnx");
            personaDireccionManager = new PersonaDireccionManager("cadenaCnx");
        }
         
        [HttpGet]
        [Route("api/PersonaDireccion")]
        public IHttpActionResult getPersonaDireccion(decimal PersonaId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.verPersonaDireccion(PersonaId);
            return Ok(resp);
        }


        /// <summary>
        /// Actualizar Estado PersonaDireccion
        /// </summary>
        /// <param name="PersonaId"></param>
        /// <param name="PersonaDireccionId"></param>
        /// <param name="EstadoDireccionId"></param>
        /// <returns></returns>       
        [HttpPut]
        [Route("api/ActEstadoPersDirec")]
        public IHttpActionResult ActEstadoPersDirec(decimal PersonaId, decimal PersonaDireccionId, decimal EstadoDireccionId)
        {
            Respuesta resp = new Respuesta();
            resp = personaDireccionManager.ActEstadoPersDirec(PersonaId, PersonaDireccionId, EstadoDireccionId);

            return Ok(resp);

        }
        /// //////////////////////////////////////

        /////////////////////////////////////////////




        // PUT: api/PersonaDireccion/5


        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/EditPersonaDireccion")]
        public IHttpActionResult PutEditPersonaDireccion(BE.PersonaDireccion ps)
        {

            Respuesta resp = new Respuesta();
            resp = personaDireccionManager.PutEditPersonaDireccion(ps.PersonaId, ps.PersonaDireccionId, ps);
     
            return Ok(resp);

        }

        [HttpPost]
        [Route("api/PersonaDireccion")]
        public IHttpActionResult PosPersonaDireccion(BE.PersonaDireccion personaDireccion)
        {
            Respuesta resp = new Respuesta();
            resp = personaDireccionManager.PostPersonaDireccion(personaDireccion);

            return Ok(resp);

        }





        [Route("api/PersonaGeolocalizacion")]
        public IHttpActionResult PostPersonaGeolocalizacion(BE.PersonaGeoLocalizacion personaGeo)
        {
            Respuesta resp = new Respuesta();
            resp = personaDireccionManager.PostPersonaGeolocalizacion(personaGeo);
            
            return Ok(resp);

        }

        public decimal ObtenerPersonaGeolocalizacionId(decimal PersonaId)
        {
            decimal GeolocalizacionId = 0;
            try
            {
                conexion.Open();
                ///////////////////////////////////////////Pais
                SqlCommand sqlCmd = new SqlCommand("MaxIdPersonGeo", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@PersonaId", PersonaId);

                GeolocalizacionId = (Convert.ToDecimal(sqlCmd.ExecuteScalar())+1);

            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                conexion.Close();
            }
            return GeolocalizacionId;
        }
        public decimal ObtenerPersonaDireccionId(decimal PersonaId)
        {
            decimal PersonaDireccionId = 0;
            try
            {
                conexion.Open();
                ///////////////////////////////////////////Pais
                SqlCommand sqlCmd = new SqlCommand("MaxIdPersonDir", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@PersonaId", PersonaId);

                PersonaDireccionId = (Convert.ToDecimal(sqlCmd.ExecuteScalar()) + 1);

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conexion.Close();
            }
            return PersonaDireccionId;
        }
        /// <summary>
        /// GetPersonaGeolocalizacion
        /// </summary>
        [ResponseType(typeof(BE.PersonaGeoLocalizacion))]
        [HttpGet]
        [Route("api/PersonaGeolocalizacion")]
        public IHttpActionResult GetPersonaGeolocalizacion()
        {
            Respuesta resp = new Respuesta();
        resp = personaDireccionManager.GetPersonaGeolocalizacion();
           
            return Ok(resp);

        }

    }
}