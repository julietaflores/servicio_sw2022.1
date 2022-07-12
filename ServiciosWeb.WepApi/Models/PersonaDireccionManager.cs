using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiciosWeb.WepApi.Models.Entities;

namespace ServiciosWeb.WepApi.Models
{
    public class PersonaDireccionManager
    {
        private BC.PersonaDireccion bcpersonaDireccion = null;
        private BC.Persona bcpersona = null;
        private BC.PersonaGeoLocalizacion bcpersonaGeoLocalizacion = null;
        public PersonaDireccionManager(string cadConx)
        {
            bcpersonaDireccion = new BC.PersonaDireccion(cadConx);
            bcpersona = new BC.Persona(cadConx);
           bcpersonaGeoLocalizacion = new BC.PersonaGeoLocalizacion(cadConx);


        }

        public Respuesta ActEstadoPersDirec(decimal PersonaId, decimal PersonaDireccionId, decimal EstadoDireccionId)
        {


            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.PersonaDireccion personaDireccion = new BE.PersonaDireccion();
                BE.Persona persona = new BE.Persona();
                personaDireccion = bcpersonaDireccion.ListadoxPersonaDireccion(PersonaId, PersonaDireccionId);
              
                persona = bcpersona.BuscarPersonaxId(PersonaId);
                personaDireccion.EstadoDireccionId = EstadoDireccionId;
                personaDireccion.PersonaDireccionUsuarioMod = persona.PersonaUsuario;
                personaDireccion.TipoEstado = BE.TipoEstado.Modificar;
                bolOk = bcpersonaDireccion.Actualizar(ref personaDireccion);
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }

                resp.valor = personaDireccion;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta PutEditPersonaDireccion(decimal idperdir, decimal PersonaDireccionId, BE.PersonaDireccion ps)
        {


            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.PersonaDireccion personaDireccion = new BE.PersonaDireccion();
                BE.Persona persona = new BE.Persona();
                personaDireccion = bcpersonaDireccion.ListadoxPersonaDireccion(idperdir, PersonaDireccionId);
                persona = bcpersona.BuscarPersonaxId(idperdir);
                ps.PersonaDireccionUsuarioMod = persona.PersonaUsuario;

                personaDireccion.TipoEstado = BE.TipoEstado.Modificar;
                bolOk = bcpersonaDireccion.Actualizar(ref ps);
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }

                resp.valor = ps;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta PostPersonaDireccion(BE.PersonaDireccion personaDireccion)
        {


            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                personaDireccion.TipoEstado = BE.TipoEstado.Insertar;
                bolOk = bcpersonaDireccion.Actualizar(ref personaDireccion);
                bcpersonaDireccion.ActualizarPersonaDireccionLasT(personaDireccion.PersonaId);
               
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }

                resp.valor = personaDireccion;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta GetPersonaGeolocalizacion()
        {


            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                List<BE.PersonaGeoLocalizacion> personaGeoLocalizacion = new List<BE.PersonaGeoLocalizacion>();

                personaGeoLocalizacion = bcpersonaGeoLocalizacion.ListadoGeolocalizacion();

                
                resp.valor = personaGeoLocalizacion;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta PostPersonaGeolocalizacion(BE.PersonaGeoLocalizacion personaGeo)
        {


            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                personaGeo.TipoEstado = BE.TipoEstado.Insertar;
                bolOk = bcpersonaGeoLocalizacion.Actualizar(ref personaGeo);
                bcpersonaGeoLocalizacion.ActualizarPersonaGeoLocalizacionLast(personaGeo.PersonaId);

                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }

                resp.valor = personaGeo;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }


    }
}