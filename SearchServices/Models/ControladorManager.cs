using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using BE;
namespace SearchServices.Models
{
    public class ControladorManager : ApiController
    {
        private BC.SearchServices bcSearchServices = null;
        private BC.Persona bcpersona = null;
        private BC.PersonaDireccion bcpersonaDireccion = null;
        private BC.RequiereServicio bcReqSer = null;
        private BC.set bcset = null;
        private BC.RequiereServicioProveedores bcReqServProv = null;
        private BC.ServAsig bcServAsig = null;
        public ControladorManager(string cadConx)
        {
            
        bcSearchServices = new BC.SearchServices(cadConx);
            bcset = new BC.set(cadConx);
            bcpersona = new BC.Persona(cadConx);
            bcpersonaDireccion = new BC.PersonaDireccion(cadConx);
            bcReqSer = new BC.RequiereServicio(cadConx);
            bcReqServProv = new BC.RequiereServicioProveedores(cadConx);
            bcServAsig = new BC.ServAsig(cadConx);
        }
        public Respuesta verSearchServices(string nombre)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                resp.valor = bcSearchServices.verSearchServices(nombre);



            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public List<BE.SearchServices> verSearchServicesv1(string nombre)

        {
               return bcSearchServices.verSearchServices(nombre);



         
        }
        public List<BE.set> Listarservicio(decimal search_tex_form)

        {
            return bcset.Listarservicio(search_tex_form);
        



        }
        public int ValidarExistePersonaCorreo(string PersonaCorreo, decimal TipoLoginId, string PersonaCodigoTelefono, string PersonaTelefono)
        {
            Boolean bolok = false;
            Respuesta resp = new Respuesta();
            int cantidad = 0;
            try
            {


                resp.estado = 1;
                resp.mensaje = "";
                List<BE.Persona> lstpersona = new List<BE.Persona>();
                lstpersona = null;
                BE.Persona persona = new BE.Persona();

                cantidad = bcpersona.ValidarExistePersonaPorCorreo(PersonaCorreo, TipoLoginId, PersonaCodigoTelefono, PersonaTelefono);


            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return cantidad;
        }
        public Respuesta savePersona(decimal id, ref BE.Persona persona)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (persona.TipoEstado == TipoEstado.Insertar)
                {



                    bolOk = bcpersona.RegistrarSolicitud(ref persona);

                    if (!bolOk)
                    {
                        throw new Exception("Error al registrar Solicitud.");
                    }





                }
                else
                {
                    if (persona.TipoEstado == TipoEstado.Modificar)
                    {

                        bolOk = bcpersona.Actualizar(ref persona);


                    }




                }
                //  bcReqSer.CargarRelaciones(ref requiereServicio, lang,relRequiereServicio.servicio);
                resp.valor = persona;
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

        public Respuesta saveRequiereServicio(ref BE.RequiereServicio requiereServicio, string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (requiereServicio.TipoEstado == TipoEstado.Insertar)
                {
                    BE.RequiereServicio req = bcReqSer.BuscarPorUID(requiereServicio.RequiereServicioUID);
                    //req = bcReqSer.CargarRelaciones()
                    if (req == null)
                    {
                        bolOk = bcReqSer.RegistrarSolicitud(ref requiereServicio);


                        if (!bolOk)
                        {
                            throw new Exception("Error al registrar Solicitud.");
                        }

                    }
                    else
                    {
                        requiereServicio.RequiereServicioId = req.RequiereServicioId;
                    }
                    if (requiereServicio.persona == null)
                    {
                        requiereServicio.persona = bcpersona.BuscarPersonaxId(requiereServicio.PersonaId);
                    }
                    bcReqSer.CargarRelaciones(ref requiereServicio, null, lang, 0, relRequiereServicio.servicio);

                }
                else
                {
                    bolOk = bcReqSer.RegistrarSolicitud(ref requiereServicio);

                    if (requiereServicio.RequiereServicioProveedores != null)
                    {
                        List<BE.RequiereServicioProveedores> lstrequiereServicioProveedores = new List<BE.RequiereServicioProveedores>();
                        lstrequiereServicioProveedores = requiereServicio.RequiereServicioProveedores;
                        bcReqServProv.CargarRelaciones(ref lstrequiereServicioProveedores, "", relReqServProv.servicioPersona);
                        requiereServicio.RequiereServicioProveedores = lstrequiereServicioProveedores;
                        foreach (BE.RequiereServicioProveedores item in requiereServicio.RequiereServicioProveedores)
                        {
                            if (item.StatusRequiereId == 4)
                            {
                                BE.ServAsig servAsig1 = new BE.ServAsig();
                                servAsig1 = bcServAsig.BuscarServAsigxRequiereServicioId(requiereServicio.RequiereServicioId);
                                if (servAsig1 == null)
                                {
                                    BE.ServAsig servAsig = new BE.ServAsig();
                                    servAsig.ServAsigId = "0";
                                    servAsig.ProveedorId = item.servicioPersona.PersonaId;
                                    //Adjudicacion 
                                    servAsig.ServAsigFHUbicacion = DateTime.Now;
                                    servAsig.ServAsigFHEstimadaLlegada = item.RequiereServicioProvFHTrabajo;
                                    servAsig.ServAsigCostoTotal = 0;
                                    servAsig.StatusServAsigId = 1;
                                    servAsig.RequiereServicioId = item.RequiereServicioId;
                                    servAsig.TipoEstado = BE.TipoEstado.Insertar;

                                    bcServAsig.RegistrarSolicitud(ref servAsig);
                                }
                            }

                        }
                    }
                }
                //  bcReqSer.CargarRelaciones(ref requiereServicio, lang,relRequiereServicio.servicio);
                resp.valor = requiereServicio.RequiereServicioId;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public BE.Persona BuscarPersonaxId(decimal PersonaId)
        {
            BE.Persona Persona = null;
            try
            {
                Persona = bcpersona.BuscarPersonaxId(PersonaId, "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Persona;
        }

    }
}