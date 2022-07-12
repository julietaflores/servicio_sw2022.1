using SearchServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

  
namespace SearchServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ControladorController : ApiController
    {
        private ControladorManager contManager = null;
        BC.Persona bcPersona = null;

        public ControladorController()
        {
            contManager = new ControladorManager("cadenaCnx");
            bcPersona = new BC.Persona("cadenaCnx");
        }
        [HttpGet]
        [Route("api/SearchServices")]////Metodo unificado en requiereServicio

        public async Task<Respuesta> SearchServices(string nombre)
        {
            Respuesta resp = new Respuesta();
            resp.valor = contManager.verSearchServices(nombre);

            return resp;
        }
        [HttpGet]
        [Route("api/SearchServicesV1")]////Metodo unificado en requiereServicio

        public  List<BE.SearchServices> SearchServicesV1(string nombre)
        {
           
           return contManager.verSearchServicesv1(nombre);

            
        }

        //////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("api/servicio")]////Metodo unificado en requiereServicio

        public List<BE.set> Listarservicio(decimal search_tex_form)
        {

            return contManager.Listarservicio(search_tex_form);


        }
        //////////////////////////////////////////////////////////////////////////7

        [HttpGet]
        [Route("api/Persona")]
        public IHttpActionResult PostPersona(BE.Persona persona, string lang)
        {


            Respuesta resp = new Respuesta();

            int cantidad = contManager.ValidarExistePersonaCorreo(persona.PersonaCorreo, persona.TipoLoginId, persona.PersonaCodigoTelefono, persona.PersonaTelefono);

            // int cantidad = 0;
            if (cantidad == 0)
            {
                /*   if (persona.TipoLoginId != 2)
                   {
                       string pin = perManager.GenerarPIN();

                       DataRow data = perManager.ListadoDatosNotificacion(lang);
                       var title = data["title"].ToString();
                       var body = data["body"].ToString();
                       BE.envioCorreo envioCorreo = new BE.envioCorreo();
                       envioCorreo.PersonaCorreo = persona.PersonaCorreo;
                       envioCorreo.Subject1 = title;
                       envioCorreo.Body = body + pin;
                       envioCorreo.Fecha = DateTime.Now;
                       envioCorreo.TipoCorreo = "nuevoUsuario";
                       envioCorreo.Estado = "Pendiente";
                       envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                      contManager.saveEnvioCorreo(ref envioCorreo);
                       persona.PersonaCodigoVerificacion = pin;
                       persona.PersonaCorreoValidado = true;
                   }*/

                persona.TipoEstado = BE.TipoEstado.Insertar;
                resp = contManager.savePersona(0, ref persona);


            }
            else
            {
                // string pin = perManager.EnviarCorreo(persona.PersonaCorreo,"es");
                /*  string pin = perManager.GenerarPIN();

                  DataRow data = perManager.ListadoDatosNotificacion(lang);
                  var title = data["title"].ToString();
                  var body = data["body"].ToString();
                  BE.envioCorreo envioCorreo = new BE.envioCorreo();
                  envioCorreo.PersonaCorreo = persona.PersonaCorreo;
                  envioCorreo.Subject1 = title;
                  envioCorreo.Body = body + pin;
                  envioCorreo.Fecha = DateTime.Now;
                  envioCorreo.TipoCorreo = "nuevoUsuario";
                  envioCorreo.Estado = "Pendiente";
                  envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                  contManager.saveEnvioCorreo(ref envioCorreo);
                  persona.PersonaCodigoVerificacion = pin;
                  persona.PersonaCorreoValidado = true;*/
                ///
                BE.Persona persona1 = bcPersona.BuscarPersonaxId(cantidad);
                persona.PersonaId = persona1.PersonaId;
                if (persona1.TipoLoginId == 1)
                {
                    persona.PersonaFacebookUid = persona1.PersonaFacebookUid;

                }
                if (persona1.TipoLoginId == 2)
                {
                    persona.PersonaGmailUid = persona1.PersonaGmailUid;

                }
                if (persona1.TipoLoginId == 3)
                {
                    persona.PersonaPhoneUid = persona1.PersonaPhoneUid;

                }


                persona.TipoEstado = BE.TipoEstado.Modificar;
                resp = contManager.savePersona(0, ref persona);
            }



            //}

            return Ok(resp);




        }


        ///////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Route("api/PersonaDireccion")]
        public IHttpActionResult PosPersonaDireccion(BE.PersonaDireccion personaDireccion)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.PostPersonaDireccion(personaDireccion);

            return Ok(resp);

        }


        [HttpPost]
        [Route("api/saveRequiereServicio")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> SaveRequiereServicio(BE.RequiereServicio requiereServicio, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.saveRequiereServicio(ref requiereServicio, lang);

            if (resp.estado == 1)
            {

                if (requiereServicio.TipoEstado == BE.TipoEstado.Insertar)
                {  /////////////////INSERCION FIREBASE

                  //  BE.Persona Persona = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                    //await EnviarNotificacionesAsyncCliente(Persona, "ClienteReque", Persona.PersonaIdioma, requiereServicio);
                 //   List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(requiereServicio.RequiereServicioId, 1);
                    //await EnviarNotificacionesAsyncV3(lstPersonas, "Proveedor", lang, requiereServicio, requiereServicio.persona.NombreCompleto(), null);
                    //await OfertaFirebase(requiereServicio.RequiereServicioId, Convert.ToInt32(requiereServicio.PersonaId), Convert.ToString(requiereServicio.EstadoReqServId), "recibido", "Requerimientos");

                }



            }
            return resp;
        }

    }
}
