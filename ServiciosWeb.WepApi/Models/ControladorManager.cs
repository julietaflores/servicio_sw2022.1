using BC;
using BE;
using ServiciosWeb.WepApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ServiciosWeb.WepApi.Models;

namespace ServiciosWeb.WepApi.Models
{
    public class ControladorManager

    {
        private BC.RequiereServicio bcReqSer = null;
        private BC.Persona bcPersona = null;
        private BC.Billetera bcBilletera = null;
        private BC.BilleteraDetalle bcBilleteraDetalle = null;
        private BC.RequiereServicio bcRequiereServicio = null;
        private BC.ConfiguracionCiudad bcSeguro = null;
        private BC.PersonaDireccion bcPerDir = null; 
        private BC.ServAsig bcServAsig = null;
        private BC.ServicioPersona bcServicioPersona = null;
        private BC.RequiereServicioProveedores bcReqServProv = null;
        private BC.Conversacion bcConversacion = null;
        private BC.ServicioPersonaDocumento bcServicioPersonaDocumento = null;
        private BC.TermCondPol bcTermCondPol = null;
        private BC.EstadoSiniestro bcEstadoSiniestro = null;
        private BC.Siniestro bcSiniestro = null;
        private BC.Siniestro_Estado bcSiniestroEstado = null;
        private BC.LogMovil bcLogMovil = null;
        private BC.NotificacionPersona bcNotificacionPersona = null;
        private BC.LogSesionesPersona bclogSesionesPersona = null;
        private BC.SearchServices bcSearchServices = null;
        private BC.envioCorreo bcenvioCorreo = null;
        private BC.Servicio bcservicio = null;
        private BC.cobranzaCBA bccobranzaCBA = null;
        private BC.requiereServicioDetalle bcrequiereServicioDetalle = null;
        private BC.UserProfile bcuserProfile = null;
        private BC.Empresa_Usuario bcempresaUsuario = null;
        private BC.Empresa_Usuario_Corporativo bcempresaUsuarioCorporativo = null;
        private BC.Promocion bcPromocion = null;
        private BC.PromocionDetallePersona bcPromocionDetallePersona = null;
        private BC.PromocionDetalleRequerimiento bcPromocionDetalleRequerimiento = null;
        private BC.BilleteraPagoTarjeta bcBilleteraPagoTarjeta = null;




        public ControladorManager(string cadConx)
        {
            bcReqSer = new BC.RequiereServicio(cadConx);
            bcPersona = new BC.Persona(cadConx);
            bcBilletera = new BC.Billetera(cadConx);
            bcBilleteraDetalle = new BC.BilleteraDetalle(cadConx);
            bcRequiereServicio = new BC.RequiereServicio(cadConx);
            bcSeguro = new BC.ConfiguracionCiudad(cadConx);
            bcPerDir = new BC.PersonaDireccion(cadConx);
            bcServAsig = new BC.ServAsig(cadConx);
            bcServicioPersona = new BC.ServicioPersona(cadConx);
            bcReqServProv = new BC.RequiereServicioProveedores(cadConx);
            bcConversacion = new BC.Conversacion(cadConx);
            bcServicioPersonaDocumento = new BC.ServicioPersonaDocumento(cadConx);
            bcTermCondPol = new BC.TermCondPol(cadConx);
            bcEstadoSiniestro = new BC.EstadoSiniestro(cadConx);
            bcSiniestro = new BC.Siniestro(cadConx);
            bcSiniestroEstado = new BC.Siniestro_Estado(cadConx);
            bcLogMovil = new BC.LogMovil(cadConx);
            bcNotificacionPersona= new BC.NotificacionPersona(cadConx);
            bclogSesionesPersona = new BC.LogSesionesPersona(cadConx);
            bcSearchServices = new BC.SearchServices(cadConx);
            bcenvioCorreo = new BC.envioCorreo(cadConx);
            bcservicio = new BC.Servicio(cadConx);
            bccobranzaCBA= new BC.cobranzaCBA(cadConx);
            bcrequiereServicioDetalle = new BC.requiereServicioDetalle(cadConx);
            bcuserProfile = new BC.UserProfile(cadConx);
            bcempresaUsuario = new BC.Empresa_Usuario(cadConx);
            bcempresaUsuarioCorporativo = new BC.Empresa_Usuario_Corporativo(cadConx);
            bcPromocion = new BC.Promocion(cadConx);
            bcPromocionDetallePersona = new BC.PromocionDetallePersona(cadConx);
            bcPromocionDetalleRequerimiento = new BC.PromocionDetalleRequerimiento(cadConx);
            bcBilleteraPagoTarjeta = new BC.BilleteraPagoTarjeta(cadConx);
        }

        public Respuesta buscar_billeteraPagoTarjeta_X_codigo(string requiereservicioid, string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                IEnumerable<string> llaves = new List<string>() { requiereservicioid };
                //   bcPromocionDetalleRequerimiento.CargarRelaciones(ref promocionDetalleRequerimiento, lang, BE.relPromocionDetalleRequerimiento.Promocion);
                BE.BilleteraPagoTarjeta obj = bcBilleteraPagoTarjeta.ObtenerHijos(llaves);


                resp.estado = 1;
                resp.valor = obj;
                resp.mensaje = "Registrado";


            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }
        public Respuesta BuscarPorSecuencia(decimal secuencia)
        {
         
            Respuesta resp = new Respuesta();
            try
            {

                BE.BilleteraPagoTarjeta obj = bcBilleteraPagoTarjeta.BuscarPorSecuencia(secuencia);


                resp.estado = 1;
                resp.valor = obj;
                resp.mensaje = "Listado";


            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }



        public Respuesta registrar_billeteraPagoTarjeta(ref BE.BilleteraPagoTarjeta billeteraPagoTarjeta, string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                if (billeteraPagoTarjeta.TipoEstado == TipoEstado.Insertar)
                {
                    bolOk = bcBilleteraPagoTarjeta.Actualizar(ref billeteraPagoTarjeta);
                    if (bolOk == true)
                    {
                        IEnumerable<string> llaves = new List<string>() { billeteraPagoTarjeta.Codigo };
                        //   bcPromocionDetalleRequerimiento.CargarRelaciones(ref promocionDetalleRequerimiento, lang, BE.relPromocionDetalleRequerimiento.Promocion);
                        BE.BilleteraPagoTarjeta obj = bcBilleteraPagoTarjeta.ObtenerHijos(llaves);



                        resp.estado = 1;
                        resp.valor = obj;
                        resp.mensaje = "Registrado";
                    }

                }
                else
                {

                    if (billeteraPagoTarjeta.TipoEstado == TipoEstado.Modificar)
                    {
                        bolOk = bcBilleteraPagoTarjeta.Actualizar(ref billeteraPagoTarjeta);
                        if (bolOk == true)
                        {
                            IEnumerable<string> llaves = new List<string>() {billeteraPagoTarjeta.Codigo };
                            //   bcPromocionDetalleRequerimiento.CargarRelaciones(ref promocionDetalleRequerimiento, lang, BE.relPromocionDetalleRequerimiento.Promocion);
                            BE.BilleteraPagoTarjeta obj = bcBilleteraPagoTarjeta.ObtenerHijos(llaves);



                            resp.estado = 1;
                            resp.valor = obj;
                            resp.mensaje = "Actualizado";
                        }


                    }


                }


            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }








        public Respuesta registrar_promocion_persona(ref BE.PromocionDetallePersona promocionDetallePersona, string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                if (promocionDetallePersona.TipoEstado == TipoEstado.Insertar)
                {
                    bolOk = bcPromocionDetallePersona.Actualizar(ref promocionDetallePersona);
                    if (bolOk == true)
                    {
                        //   bcPromocionDetalleRequerimiento.CargarRelaciones(ref promocionDetalleRequerimiento, lang, BE.relPromocionDetalleRequerimiento.Promocion);
                        List<BE.PromocionDetallePersona> obj = bcPromocionDetallePersona.Lista1(promocionDetallePersona.PersonaId, promocionDetallePersona.PromocionId);
                    

                        resp.estado = 1;
                        resp.valor = obj;
                        resp.mensaje = "Registrado";
                    }
               
                }
                else
                {

                    if (promocionDetallePersona.TipoEstado == TipoEstado.Modificar)
                    {
                        bolOk = bcPromocionDetallePersona.Actualizar(ref promocionDetallePersona);
                        if (bolOk == true)
                        {
                            List<BE.PromocionDetallePersona> obj = bcPromocionDetallePersona.Lista1(promocionDetallePersona.PersonaId, promocionDetallePersona.PromocionId);
                           
                            resp.estado = 1;
                            resp.valor = obj;
                            resp.mensaje = "Actualizado";
                        }
                     

                    }


                }


            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }



        public List<BE.PromocionDetalleRequerimiento> existe_reqservid_en_promodetallereq(string requiereservicioid , string lang ) {
            List<BE.PromocionDetalleRequerimiento> lista = null;
            lista = bcPromocionDetalleRequerimiento.ListarPromocionRequiereServicioId(requiereservicioid, lang);
            return lista;
        }


        public List<BE.Promocion> listar_promocion(decimal promocionid, string lang)
        {
            List<BE.Promocion> lista = null;

            IEnumerable<decimal> llaves = new List<decimal>() {promocionid };
            lista = bcPromocion.Lista_ObtenerHijos(llaves, lang);
            return lista;
        }




        public List<BE.PromocionDetallePersona> listar_promociondetalleper(decimal promocionid,decimal personaid, string lang)
        {
            IEnumerable<decimal> llaves = new List<decimal>() { promocionid };
            IEnumerable<decimal> llavesp = new List<decimal>() { personaid };
            List<BE.PromocionDetallePersona> colpromociondetallepersona = bcPromocionDetallePersona.Lista_ObtenerHijospromocion(llaves,llavesp);
            return colpromociondetallepersona;
        }



        public Respuesta registrar_promocion_requiereservicioid(ref BE.PromocionDetalleRequerimiento promocionDetalleRequerimiento, string lang)
        {
            Boolean bolOk = false;
            Boolean bolOkp = false;
            Respuesta resp = new Respuesta();
            try
            {
                if (promocionDetalleRequerimiento.TipoEstado == TipoEstado.Insertar)
                {
                    bolOk = bcPromocionDetalleRequerimiento.Actualizar(ref promocionDetalleRequerimiento);
                    if (bolOk == true)
                    {

                        List<BE.PromocionDetalleRequerimiento> obj = bcPromocionDetalleRequerimiento.ListarPromocionDetalleRequerimiento(promocionDetalleRequerimiento.PPersonaId, promocionDetalleRequerimiento.PPromocionId,promocionDetalleRequerimiento.RequiereServicioId,lang,BE.relPromocionDetalleRequerimiento.Promocion);
                     
                        IEnumerable<decimal> llaves = new List<decimal>() { promocionDetalleRequerimiento.PPromocionId };
                        IEnumerable<decimal> llavesp = new List<decimal>() { promocionDetalleRequerimiento.PPersonaId };
                        List<BE.Promocion> colPromocion = bcPromocion.Lista_ObtenerHijos(llaves, lang);
                        List<BE.RequiereServicioProveedores> colproveedores = bcReqServProv.ListadoRequiereServicioProveedores_primero(promocionDetalleRequerimiento.RequiereServicioId,lang);
                        List<BE.PromocionDetallePersona> colpromociondetallepersona = bcPromocionDetallePersona.Lista_ObtenerHijospromocion(llaves,llavesp);
                      
                        BE.PromocionDetallePersona obj45 = new BE.PromocionDetallePersona();
                    
                        bool por_cantidad = false;
                        decimal valor = 0;
                        foreach (BE.Promocion promocion in colPromocion)
                        {
                            valor = promocion.PromocionValor;
                            por_cantidad = promocion.PromocionPorc;
                        }
                        decimal valor_cotizado = 0;
                        foreach (BE.RequiereServicioProveedores rspro in colproveedores)
                        {
                            valor_cotizado = rspro.RequiereServicioProvCotizacion;
                        }                  
                        decimal valor2 = 0;
                     
                        foreach (BE.PromocionDetallePersona promociondetalleper in colpromociondetallepersona)
                        {
                            valor2 = promociondetalleper.Valor;
                         
                        }
                        if (por_cantidad == false)
                        {

                            if (valor2 == 0)
                            {
                                obj45.PromocionId = promocionDetalleRequerimiento.PPromocionId;
                                obj45.PersonaId = promocionDetalleRequerimiento.PPersonaId;
                                obj45.FechaInsercion = DateTime.Now;
                                obj45.Estado = false;
                                obj45.Valor = 0;
                                obj45.TipoEstado = BE.TipoEstado.Modificar;
                                bolOkp = bcPromocionDetallePersona.Actualizar(ref obj45);

                            }
                            else {
                                obj45.PromocionId = promocionDetalleRequerimiento.PPromocionId;
                                obj45.PersonaId = promocionDetalleRequerimiento.PPersonaId;
                                obj45.FechaInsercion = DateTime.Now;
                                obj45.Estado = false;
                                obj45.Valor = valor2;
                                obj45.TipoEstado = BE.TipoEstado.Modificar;
                                bolOkp = bcPromocionDetallePersona.Actualizar(ref obj45);

                            }



                        }
                        else
                        {

                            

                            obj45.PromocionId = promocionDetalleRequerimiento.PPromocionId;
                            obj45.PersonaId = promocionDetalleRequerimiento.PPersonaId;
                            obj45.FechaInsercion = DateTime.Now;
                            obj45.Estado = false;
                            obj45.Valor = 0;
                            obj45.TipoEstado = BE.TipoEstado.Modificar;                          
                            bolOkp = bcPromocionDetallePersona.Actualizar(ref obj45);

                         
                        }
                        resp.estado = 1;
                       resp.valor = obj;
                       resp.mensaje = "Registrado";
                    }
                   


                }
                else {

                    if (promocionDetalleRequerimiento.TipoEstado == TipoEstado.Modificar)
                    {
                        bolOk = bcPromocionDetalleRequerimiento.Actualizar(ref promocionDetalleRequerimiento);
                        if (bolOk == true)
                        {
                            List<BE.PromocionDetalleRequerimiento> obj = bcPromocionDetalleRequerimiento.ListarPromocionDetalleRequerimiento(promocionDetalleRequerimiento.PPersonaId, promocionDetalleRequerimiento.PPromocionId, promocionDetalleRequerimiento.RequiereServicioId, lang, BE.relPromocionDetalleRequerimiento.Promocion);
                            IEnumerable<decimal> llaves = new List<decimal>() { promocionDetalleRequerimiento.PPromocionId };
                            List<BE.Promocion> colPromocion = bcPromocion.Lista_ObtenerHijos(llaves, lang);
                            decimal valor = 0;
                            foreach (BE.Promocion promocion in colPromocion)
                            {
                                valor = promocion.PromocionValor;
                            }


                            resp.estado = 1;
                            resp.valor = valor;
                            resp.mensaje = "Actualizado";
                        }
                    }


                }
               

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }








        public Respuesta Lista_Promociones_x_Persona(decimal personaid, string lang)
        {
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;

                List<BE.PromocionDetallePersona> obj = bcPromocionDetallePersona.Lista_ObtenerHijos(personaid, lang, BE.relPromocionDetallePersona.Promocion);
                if (obj.Count == 0)
                {
                    obj = null;
                }
                resp.valor = obj;
                
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }


        public Respuesta saveRequiereServicio(ref BE.RequiereServicio requiereServicio,string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
              
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
                        resp.estado = 1;
                        resp.mensaje = "";
                    }
                    else
                    {
                        requiereServicio.RequiereServicioId = req.RequiereServicioId;
                    }
                   if (requiereServicio.persona == null)
                   {
                       requiereServicio.persona = bcPersona.BuscarPersonaxId(requiereServicio.PersonaId);
                 }
                   bcReqSer.CargarRelaciones(ref requiereServicio,null, lang,0, relRequiereServicio.servicio,relRequiereServicio.requiereServicioDetalle);

                }
                else
                {

                    if ((bcRequiereServicio.validarSiEstaAdjudicado(requiereServicio.RequiereServicioId)==true)&&(requiereServicio.EstadoReqServId==3))
                    {
                        if (bcRequiereServicio.validarSiEstaAdjudicadoyEsMayora6horas(requiereServicio.RequiereServicioId)==true)
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
                            resp.estado = 1;
                            resp.mensaje = "";
                        }
                        else
                        {
                            resp.estado = 2;
                            resp.valor = null;
                            resp.mensaje = "Requerimiento adjudicado pasaron 6 horas , no se puede Cancelar";
                        }
                    }
                    else
                    {
                        resp.estado = 1;
                        resp.mensaje = "";
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

                    /////////////////PROCESO PROBADO ----------------REVISION
                   /* resp.estado = 1;
                    resp.mensaje = "";
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
                    }*/
                    ///////////////////----revision


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

 

 



        public Respuesta SaveAdjudicacion(string requiereServicioId, decimal ProveedorId,string StatusRequiereId,string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                        

                BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
                requiereServicio = bcReqSer.BuscarRequiereServicioxId(requiereServicioId, lang, relRequiereServicio.requiereServicioProveedores,relReqServProv.servicioPersona,relRequiereServicio.servAsig);
                requiereServicio.TipoEstado = BE.TipoEstado.Modificar;
                resp.valor = requiereServicio;
                bolOk = bcReqSer.RegistrarAdjudicacion(ref requiereServicio, ProveedorId,StatusRequiereId);
                requiereServicio = bcReqSer.BuscarRequiereServicioxId(requiereServicioId, lang, relRequiereServicio.requiereServicioProveedores, relReqServProv.servicioPersona, relRequiereServicio.servAsig);


                if (!bolOk)
                        {
                            throw new Exception("Error al registrar Solicitud.");
                        }

                    
                 
                
           /*     else
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
                resp.valor = requiereServicio.RequiereServicioId;*/
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta saveSiniestro(ref BE.Siniestro Siniestro)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (Siniestro.TipoEstado == TipoEstado.Insertar)
                {
                    BE.Siniestro sini = bcSiniestro.BuscarPorUID(Siniestro.SiniestroUID);
                    //req = bcReqSer.CargarRelaciones()
                    if (sini == null)
                    {
                        bolOk = bcSiniestro.RegistrarSolicitud(ref Siniestro);

                        if (!bolOk)
                        {
                            throw new Exception("Error al registrar Solicitud.");
                        }

                    }
                    else
                    {
                     
                        Siniestro = sini;
                    }
                  
                    /*   if (requiereServicio.persona == null)
                       {
                           requiereServicio.persona = bcPersona.BuscarPersonaxId(requiereServicio.PersonaId);
                       }
                       bcReqSer.CargarRelaciones(ref requiereServicio, lang, relRequiereServicio.servicio);*/

                }
                else
                {
                    bolOk = bcSiniestro.RegistrarSolicitud(ref Siniestro);

                  /*  if (requiereServicio.RequiereServicioProveedores != null)
                    {
                        List<BE.RequiereServicioProveedores> lstrequiereServicioProveedores = new List<BE.RequiereServicioProveedores>();
                        lstrequiereServicioProveedores = requiereServicio.RequiereServicioProveedores;
                        bcReqServProv.CargarRelaciones(ref lstrequiereServicioProveedores, "", relReqServProv.servicioPersona);
                        requiereServicio.RequiereServicioProveedores = lstrequiereServicioProveedores;
                        foreach (BE.RequiereServicioProveedores item in requiereServicio.RequiereServicioProveedores)
                        {
                            if (item.StatusRequiereId == 4)
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
                    }*/
                }
                //  bcReqSer.CargarRelaciones(ref requiereServicio, lang,relRequiereServicio.servicio);
                List<BE.Siniestro_Estado> Lstsiniestro_Estado = new List<BE.Siniestro_Estado>();
                BE.Siniestro_Estado siniestro_Estado = new BE.Siniestro_Estado();
                siniestro_Estado.SiniestroId = 0;
                siniestro_Estado.SiniestroEstadoId = 0;
                siniestro_Estado.Siniestro_EstadoFechaHoraMod = DateTime.Now;
                siniestro_Estado.Siniestro_EstadoObservacion = "";
                Lstsiniestro_Estado.Add(siniestro_Estado);
                Siniestro.Siniestro_Estado = Lstsiniestro_Estado;

                resp.valor = Siniestro;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta saveSiniestro_Estado(ref BE.Siniestro_Estado siniestro_Estado)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (siniestro_Estado.TipoEstado == TipoEstado.Insertar)
                {
                  
                    
                        bolOk = bcSiniestroEstado.RegistrarSolicitud(ref siniestro_Estado);

                        if (!bolOk)
                        {
                            throw new Exception("Error al registrar Solicitud.");
                        }

                
           
                
                    /*   if (requiereServicio.persona == null)
                       {
                           requiereServicio.persona = bcPersona.BuscarPersonaxId(requiereServicio.PersonaId);
                       }
                       bcReqSer.CargarRelaciones(ref requiereServicio, lang, relRequiereServicio.servicio);*/

                }
                else
                {
                    bolOk = bcSiniestroEstado.RegistrarSolicitud(ref siniestro_Estado);

                    /*  if (requiereServicio.RequiereServicioProveedores != null)
                      {
                          List<BE.RequiereServicioProveedores> lstrequiereServicioProveedores = new List<BE.RequiereServicioProveedores>();
                          lstrequiereServicioProveedores = requiereServicio.RequiereServicioProveedores;
                          bcReqServProv.CargarRelaciones(ref lstrequiereServicioProveedores, "", relReqServProv.servicioPersona);
                          requiereServicio.RequiereServicioProveedores = lstrequiereServicioProveedores;
                          foreach (BE.RequiereServicioProveedores item in requiereServicio.RequiereServicioProveedores)
                          {
                              if (item.StatusRequiereId == 4)
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
                      }*/
                }
                //  bcReqSer.CargarRelaciones(ref requiereServicio, lang,relRequiereServicio.servicio);
                resp.valor = siniestro_Estado.SiniestroId;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta saveServAsig(ref BE.ServAsig servAsig)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (servAsig.TipoEstado == TipoEstado.Insertar)
                {
                   /* BE.RequiereServicio req = bcReqSer.BuscarPorUID(requiereServicio.RequiereServicioUID);
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
                        requiereServicio.persona = bcPersona.BuscarPersonaxId(requiereServicio.PersonaId);
                    }*/
                }
                else
                {//PARA ELIMINAR
                    bolOk = bcServAsig.RegistrarSolicitud(ref servAsig);

                }
                resp.valor = servAsig;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta saveRequiereServicioProveedores(ref BE.RequiereServicioProveedores requiereServicioProveedores)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (requiereServicioProveedores.TipoEstado == TipoEstado.Insertar)
                {
                    /* BE.RequiereServicio req = bcReqSer.BuscarPorUID(requiereServicio.RequiereServicioUID);
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
                         requiereServicio.persona = bcPersona.BuscarPersonaxId(requiereServicio.PersonaId);
                     }*/
                }
                else
                {//PARA ELIMINAR
                    bolOk = bcReqServProv.RegistrarSolicitud(ref requiereServicioProveedores);

                }
                resp.valor = requiereServicioProveedores;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta saveConversacion(ref BE.Conversacion conversacion)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (conversacion.TipoEstado == TipoEstado.Insertar)
                {
                    BE.ServAsig servAsig = new BE.ServAsig();
                  servAsig=  bcServAsig.BuscarServAsigxRequiereServicioId(conversacion.ServAsigId);
                    conversacion.ServAsigId = servAsig.ServAsigId;
                    bolOk = bcConversacion.Actualizar(ref conversacion);
                }
                else
                {//PARA ELIMINAR
                    

                }
                resp.valor = conversacion;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta saveEnvioCorreo(ref BE.envioCorreo envioCorreo)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (envioCorreo.TipoEstado == TipoEstado.Insertar)
                {
                 
                    bolOk = bcenvioCorreo.Actualizar(ref envioCorreo,false);
                    if (!bolOk)
                    {
                        throw new Exception("Error al registrar Solicitud.");
                    }

                }
                else
                {

                    bolOk = bcenvioCorreo.Actualizar(ref envioCorreo, false);
                    if (!bolOk)
                    {
                        throw new Exception("Error al registrar Solicitud.");
                    }

                }
                resp.valor = envioCorreo;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta saveBilletera(ref BE.Billetera billetera)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (billetera.TipoEstado == TipoEstado.Insertar)
                {

                    bolOk = bcBilletera.RegistrarBilletera(ref billetera);
                }
                if (billetera.TipoEstado == TipoEstado.Modificar)
                {

                    bolOk = bcBilletera.RegistrarBilletera(ref billetera);
                }
                if (bolOk == true)
                    resp.valor = billetera.BilleteraId;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta saveBilleteraDetalle(ref BE.BilleteraDetalle billeteraDetalle)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (billeteraDetalle.TipoEstado == TipoEstado.Insertar)
                {

                 //   bolOk = bcBilletera.RegistrarBilletera(ref billeteraDetalle);
                }
                if (billeteraDetalle.TipoEstado == TipoEstado.Modificar)
                {

                   // bolOk = bcBilletera.RegistrarBilletera(ref billeteraDetalle);
                }
                if (bolOk == true)
                    resp.valor = billeteraDetalle.BilleteraId;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public List<BE.Persona> ListadoTokenProveedores(string requiereServicioId,decimal StatusRequiereId)
        {
            List<BE.Persona> lstPersonas = null;
            try
            {
                lstPersonas = bcPersona.ListadoTokenProveedores(requiereServicioId,StatusRequiereId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPersonas;
        }


        public Boolean ver_Si_se_Envio_Notificacion(string requiereServicioId)
        {
            Boolean BolOk = false;
            try
            {
                BolOk = bcRequiereServicio.ver_Si_se_Envio_Notificacion(requiereServicioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BolOk;
        }
        public Boolean validarSiEstaAdjudicadoyEsMayora6horas(string requiereServicioId)
        {
            Boolean BolOk = false;
            try
            {
                BolOk = bcRequiereServicio.validarSiEstaAdjudicadoyEsMayora6horas(requiereServicioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BolOk;
        }

        


        public BE.Persona ListadoIdSiniestrosCliente(string ServAsigId)
        {
            BE.Persona PersonaCliente = null;
            try
            {
                PersonaCliente = bcPersona.ListadoSiniestrosCliente(ServAsigId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PersonaCliente;
        }
        public BE.Persona ListadoIdSiniestrosProveedor(string ServAsigId)
        {
            BE.Persona PersonaCliente = null;
            try
            {
                PersonaCliente = bcPersona.ListadoSiniestrosProveedor(ServAsigId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PersonaCliente;
        }
        public BE.Persona BuscarPersonaxId(decimal PersonaId)
        {
            BE.Persona Persona= null;
            try
            {
                Persona = bcPersona.BuscarPersonaxId(PersonaId, "",BE.relPersona.ciudad,BE.relciudad.region,BE.relRegion.pais,BE.relpais.moneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Persona;
        }





        public BE.ServAsig BuscarServAsigxRequiereServicioId(string requiereServicioId, params Enum[] relaciones)
        {
            BE.ServAsig servAsig = null;
            try
            {
                servAsig = bcServAsig.BuscarServAsigxRequiereServicioId(requiereServicioId, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return servAsig;
        }
        
        public Respuesta ListadoObtenerIdPersonaProvedorAdj(string requiereServicioId)
        {
            Respuesta resp = new Respuesta();
            List<BE.Persona> lstPersonas = new List<BE.Persona>();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lstPersonas = bcPersona.ListadoObtenerIdPersonaProvedorAdj(requiereServicioId);
                resp.valor = lstPersonas;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public List<BE.Persona> ListadoProveedoresCotizados(string requiereServicioId)
        {
            List<BE.Persona> lstPersonas = null;
            try
            {
                lstPersonas = bcPersona.ListadoTokenProveedoresCotizado(requiereServicioId,null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPersonas;
        }
        public List<BE.Persona> ListadoTokenClientePagado(string ServAsigId)
        {
            List<BE.Persona> lstPersonas = null;
            try
            {
                lstPersonas = bcPersona.ListadoTokenClientePagado(ServAsigId, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPersonas;
        }
        public List<BE.Persona> ListadoPersonaFinServicio(string ServAsigId)
        {
            List<BE.Persona> lstPersonas = null;
            try
            {
                lstPersonas = bcPersona.ListadoPersonaFinServicio(ServAsigId, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPersonas;
        }          
        public DataRow ListadoDatosNotificacion(string tipo, string lang)
        {
            DataRow dr = null;
            try
            {

                dr = bcPersona.ListadoDatosNotificacion(tipo, lang);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }
        public DataRow ListadoDatosNotificacionv2(string tipo, string lang)
        {
            DataRow dr = null;
            try
            {

                dr = bcPersona.ListadoDatosNotificacionv2(tipo, lang);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }
        public List<BE.RequiereServicio> ListadoRequiereServicio_A_adjudicar(decimal PersonaId, string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            List<BE.RequiereServicio> lstRequiereServicio = new List<BE.RequiereServicio>();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                lstRequiereServicio = bcRequiereServicio.ListadoRequiereServicio_A_adjudicar(PersonaId, lang);

           //     resp.valor = lstRequiereServicio;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return lstRequiereServicio;
       
        }
        public int ImporteRequiereServicio(string RequiereServicioId, bool servicioDetalleTipo)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            int importe = 0;
            List<BE.RequiereServicio> lstRequiereServicio = new List<BE.RequiereServicio>();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                importe = bcRequiereServicio.ImporteRequiereServicio(RequiereServicioId, servicioDetalleTipo);

           //     resp.valor = lstRequiereServicio;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return importe;

        }
        public Respuesta VerBilletera(long personaId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.Billetera> lstBilletera = new List<BE.Billetera>();

                lstBilletera = bcBilletera.ListadoBilletera(personaId, BE.relBilletera.moneda);

                resp.valor = lstBilletera;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }             
      public Respuesta GetServicioProveedoresV2(string servicioId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.ServicioPersona> lstServicioPersona = new List<BE.ServicioPersona>();

                lstServicioPersona = bcServicioPersona.VerServicioPersona(servicioId,relServicioPersona.personaDireccion);

                resp.valor = lstServicioPersona;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }
        public Respuesta VerRequierServicio(long personaId, string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                List<BE.RequiereServicio> lstreqSer = new List<BE.RequiereServicio>();

                lstreqSer = bcRequiereServicio.ListadoRequiereServicio(personaId, lang, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio,BE.relServicio.servicioRequerimiento
                    , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioProveedores,BE.relReqServProv .servicioPersona, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere,BE.relRequiereServicio.servAsig, BE.relServAsig.statusServAsig,BE.relServAsig.servAsigCosto );

             

                foreach (BE.RequiereServicio item in lstreqSer)
                {

                    if (item.personaDireccion == null)
                    {

                        BE.PersonaDireccion personaDireccion = new BE.PersonaDireccion();

                        personaDireccion.PersonaId = 0;
                        personaDireccion.PersonaDireccionId = 0;
                        personaDireccion.TipoDireccionId = 0;
                        personaDireccion.PersonaDireccionTitulo = "";
                        personaDireccion.PersonaDireccionGeo = item.RequiereServicioGeoInmediato;
                        personaDireccion.PersonaDireccionDescripcion = "";
                        personaDireccion.CiudadDireccionId = 0;
                        personaDireccion.PersonaDireccionFHMod = DateTime.Now;
                        personaDireccion.PersonaDireccionUsuarioMod = "";
                        personaDireccion.EstadoDireccionId = 0;
                        personaDireccion.PersonaDireccionDireccion = "";


                        item.personaDireccion = personaDireccion;

                    }
                   
                }
                resp.valor = lstreqSer;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta VerSiniestro(decimal personaId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                List<BE.Siniestro> lstsiniestros = new List<BE.Siniestro>();

                lstsiniestros = bcSiniestro.LisTadoSiniestro(personaId,BE.relSiniestro.siniestro_estado);

                resp.valor = lstsiniestros;

               

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta VerRequierServicioPaginacion(long personaId,int index, int max, string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                List<BE.RequiereServicio> lstreqSer = new List<BE.RequiereServicio>();

                lstreqSer = bcRequiereServicio.ListadoRequiereServicioPaginacion(personaId,index,max, lang, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relServicio.servicioRequerimiento,BE.relServicio.servicioDetalle,BE.relCategoriaServicio.Ciudad,BE.relciudad.region,BE.relRegion.pais,BE.relpais.moneda
                    , BE.relRequiereServicio.estadoReqServ,BE.relRequiereServicio.requiereServicioDetalle, BE.relRequiereServicio.requiereServicioProveedores, BE.relReqServProv.servicioPersona, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere, BE.relRequiereServicio.servAsig,BE.relServAsig.servAsigCosto, BE.relServAsig.statusServAsig,BE.relServAsig.post,BE.relServAsig.proveedor,BE.Post.relPost.PostContenido);


                foreach (BE.RequiereServicio item in lstreqSer)
                {

                    if (item.personaDireccion == null)
                    {

                        BE.PersonaDireccion personaDireccion = new BE.PersonaDireccion();

                        personaDireccion.PersonaId = 0;
                        personaDireccion.PersonaDireccionId = 0;
                        personaDireccion.TipoDireccionId = 0;
                        personaDireccion.PersonaDireccionTitulo = "";
                        personaDireccion.PersonaDireccionGeo = item.RequiereServicioGeoInmediato;
                        personaDireccion.PersonaDireccionDescripcion = "";
                        personaDireccion.CiudadDireccionId = 0;
                        personaDireccion.PersonaDireccionFHMod = DateTime.Now;
                        personaDireccion.PersonaDireccionUsuarioMod = "";
                        personaDireccion.EstadoDireccionId = 0;
                        personaDireccion.PersonaDireccionDireccion = "";


                        item.personaDireccion = personaDireccion;

                    }

                }
                resp.valor = lstreqSer;

              
                

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta VerRequierServicioPaginacionWeb(long personaId, int index, int max, string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                List<BE.RequiereServicio> lstreqSer = new List<BE.RequiereServicio>();

                lstreqSer = bcRequiereServicio.ListadoRequiereServicioPaginacion(personaId, index, max, lang, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relServicio.servicioRequerimiento, BE.relServicio.servicioDetalle, BE.relCategoriaServicio.Ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda
                    , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioDetalleWeb, BE.relRequiereServicio.requiereServicioProveedores, BE.relReqServProv.servicioPersona, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere, BE.relRequiereServicio.servAsig, BE.relServAsig.statusServAsig);


                foreach (BE.RequiereServicio item in lstreqSer)
                {

                    if (item.personaDireccion == null)
                    {

                        BE.PersonaDireccion personaDireccion = new BE.PersonaDireccion();

                        personaDireccion.PersonaId = 0;
                        personaDireccion.PersonaDireccionId = 0;
                        personaDireccion.TipoDireccionId = 0;
                        personaDireccion.PersonaDireccionTitulo = "";
                        personaDireccion.PersonaDireccionGeo = item.RequiereServicioGeoInmediato;
                        personaDireccion.PersonaDireccionDescripcion = "";
                        personaDireccion.CiudadDireccionId = 0;
                        personaDireccion.PersonaDireccionFHMod = DateTime.Now;
                        personaDireccion.PersonaDireccionUsuarioMod = "";
                        personaDireccion.EstadoDireccionId = 0;
                        personaDireccion.PersonaDireccionDireccion = "";


                        item.personaDireccion = personaDireccion;

                    }

                }
                resp.valor = lstreqSer;




            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta VerRequierServicioProveedores(string RequiereServicioId, string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                List<BE.RequiereServicioProveedores> lstreqSerProv = new List<BE.RequiereServicioProveedores>();

                lstreqSerProv = bcReqServProv.ListadoRequiereServicioProveedores(RequiereServicioId, lang, BE.relReqServProv.requiereServicio,BE.relReqServProv.servicioPersona);

                resp.valor = lstreqSerProv;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }        
       public Respuesta VerServicioPersonaDocumento(decimal ServicioPersonaId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                List<BE.ServicioPersonaDocumento> lstServPerDoc = new List<BE.ServicioPersonaDocumento>();

                lstServPerDoc = bcServicioPersonaDocumento.ListadoServicioPersonaDocumento(ServicioPersonaId,null);

                resp.valor = lstServPerDoc;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta VerServicioAsigXid(string ServicioAsigId, string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                BE.ServAsig bcServAsg = new BE.ServAsig();

                bcServAsg = bcServAsig.BuscarServAsigxId(ServicioAsigId, BE.relServAsig.requiereServicio,BE.relServAsig.servAsigCosto,BE.relServAsig.post,BE.Post.relPost.PostContenido, BE.relservAsigCosto.conceptoCosto,BE.relRequiereServicio.persona,BE.relPersona.ciudad,BE.relciudad.region,BE.relpais.moneda);

                resp.valor = bcServAsg;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }       
        public Respuesta verRequiereServicioXid(string RequiereServicioId, string lang, bool moreValues = false)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
                if (moreValues)
                {
                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId(RequiereServicioId, lang,  BE.relRequiereServicio.servicio,BE.relServicio.servicioDetalle, BE.relServicio.categoriaServicio,BE.relCategoriaServicio.Ciudad,BE.relciudad.configuracionCiudad,BE.relciudad.region,BE.relRegion.pais,BE.relpais.moneda, BE.relServicio.servicioRequerimiento
                    , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioProveedores, BE.relReqServProv.servicioPersona, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere, BE.relRequiereServicio.servAsig, BE.relServAsig.statusServAsig,BE.relServAsig.post,BE.relServAsig.servAsigCosto,BE.relRequiereServicio.requiereServicioDetalle);
                }
                else
                {
                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId(RequiereServicioId, lang, BE.relRequiereServicio.servicio);
                }
                                        
                resp.valor = requiereServicio;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta verRequiereServicioXidFirebase(string RequiereServicioId, string lang, bool moreValues = false)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
                if (moreValues)
                {
                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId(RequiereServicioId,lang, BE.relRequiereServicio.IdiomaS,BE.relRequiereServicio.persona,BE.relRequiereServicio.personaDireccion,BE.relRequiereServicio.IdiomaServ,BE.relRequiereServicio.RequiereServicioProveedoresF, BE.relReqServProvF.EstadoProvReqId,BE.relRequiereServicio.servicio,BE.relServicio.categoriaServicio);
                }
               
                resp.valor = requiereServicio;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }


        public Respuesta verRequiereServicioXidWeb(string RequiereServicioId, string lang, bool moreValues = false)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
                if (moreValues)
                {
                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId(RequiereServicioId, lang, BE.relRequiereServicio.servicio,BE.relRequiereServicio.requiereServicioDetalleWeb,BE.relServicio.servicioDetalle, BE.relServicio.categoriaServicio, BE.relCategoriaServicio.Ciudad, BE.relciudad.configuracionCiudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda, BE.relServicio.servicioRequerimiento
                    , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioProveedores, BE.relReqServProv.servicioPersona, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere, BE.relRequiereServicio.servAsig, BE.relServAsig.proveedor, BE.relServAsig.statusServAsig, BE.relRequiereServicio.requiereServicioDetalle);
                }
                else
                {
                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId(RequiereServicioId, lang, BE.relRequiereServicio.servicio);
                }

                resp.valor = requiereServicio;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public bool ver_Si_se_AdjudicoProveedores(string RequiereServicioId, string lang, bool moreValues = false)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                bolOk= bcReqServProv.ver_Si_se_AdjudicoProveedores(RequiereServicioId, lang, null);

            }
            catch (Exception ex)
            {
                
            }
            return bolOk;
        }

        public bool ver_Si_se_EstaCancelado(string RequiereServicioId, string lang, bool moreValues = false)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                bolOk = bcRequiereServicio.ver_Si_se_EstaCancelado(RequiereServicioId, lang, null);

            }
            catch (Exception ex)
            {

            }
            return bolOk;
        }
        public string verServAsigId(decimal SiniestroId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
      
              return bcSiniestroEstado.VerServAsigId(SiniestroId);
             

        }


        public Respuesta vergetReqSerxSerAsigId(string serAsigId, string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
            //    if (moreValues)
            //    {
                    requiereServicio = bcRequiereServicio.BuscargetReqSerxSerAsigId(serAsigId, lang, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relServicio.servicioRequerimiento
                    , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioProveedores, BE.relReqServProv.servicioPersona, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere, BE.relRequiereServicio.servAsig, BE.relServAsig.statusServAsig);

                    ///////////////
         
                    ////////////////
              //  }
             //   else
            //    {
           //         requiereServicio = bcRequiereServicio.BuscargetReqSerxSerAsigId(serAsigId, lang, BE.relRequiereServicio.servicio);
           //     }

                resp.valor = requiereServicio;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta VerRequierServicioXEstado(long personaId, string lang, string EstadoReqServId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                List<BE.RequiereServicio> lstReqSer = new List<BE.RequiereServicio>();

                lstReqSer = bcRequiereServicio.ListadoRequiereServicioXEstado(personaId, lang, EstadoReqServId,
                    BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relServicio.servicioRequerimiento,BE.relServicio.servicioDetalle,BE.relCategoriaServicio.Ciudad,BE.relciudad.region,BE.relRegion.pais,BE.relpais.moneda
                    , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioProveedores, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere,BE.relRequiereServicio.servAsig,BE.relRequiereServicio.requiereServicioDetalle);

                foreach (BE.RequiereServicio item in lstReqSer)
                {
                    
                    if (item.personaDireccion == null)
                    {

                        BE.PersonaDireccion personaDireccion = new BE.PersonaDireccion();

                        personaDireccion.PersonaId = 0;
                        personaDireccion.PersonaDireccionId = 0;
                        personaDireccion.TipoDireccionId = 0;
                        personaDireccion.PersonaDireccionTitulo = "";
                        personaDireccion.PersonaDireccionGeo = item.RequiereServicioGeoInmediato;
                        personaDireccion.PersonaDireccionDescripcion = "";
                        personaDireccion.CiudadDireccionId = 0;
                        personaDireccion.PersonaDireccionFHMod = DateTime.Now;
                        personaDireccion.PersonaDireccionUsuarioMod = "";
                        personaDireccion.EstadoDireccionId = 0;
                        personaDireccion.PersonaDireccionDireccion = "";


                        item.personaDireccion = personaDireccion;

                    }

                }
                resp.valor = lstReqSer;


            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta VerBilleteraDetalle(long BilleteraId, int index, int max,string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.BilleteraDetalle> lstBilleteraDetalle = new List<BE.BilleteraDetalle>();

                lstBilleteraDetalle = bcBilleteraDetalle.ListadoBilleteraDetalle(BilleteraId, index, max,lang,BE.relBilleteraDetalle.billeteraConcepto );

                resp.valor = lstBilleteraDetalle;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta verSiniestroPaginacion(decimal PersonaId, int index, int max)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.Siniestro> lstSiniestro = new List<BE.Siniestro>();

                lstSiniestro = bcSiniestro.LisTadoSiniestroPaginacion(PersonaId, index, max, BE.relSiniestro.siniestro_estado);
                             
                 
                          


                resp.valor = lstSiniestro;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }       
        public Respuesta verNotificacionPersonaPaginacion(decimal PersonaId,string lang , int index, int max)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.NotificacionPersona> lstObjeto = new List<BE.NotificacionPersona>();

               

                lstObjeto = bcNotificacionPersona.LisTadoNotificacionPersonaPaginacion(PersonaId, lang,index, max, BE.relNotificacionPersona.estadoNotificacion, BE.relNotificacionPersona.conceptoNotificacion, BE.relNotificacionPersona.tipoEstadoNotificacion);




                resp.valor = lstObjeto;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta cantidadSiniestro(decimal personaId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                int cantRS = 0;

                cantRS = bcSiniestro.cantidadSiniestro(personaId);

                resp.valor = cantRS;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta cantidadNotificacionPersona(decimal personaId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                int cantRS = 0;

             
                cantRS = bcNotificacionPersona.cantidadNotificacionPersona(personaId);
                resp.valor = cantRS;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }



        public Respuesta VerTermCondPol(decimal PaisId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.TermCondPol termCondPol = new BE.TermCondPol();

                termCondPol = bcTermCondPol.BuscarTermCondPol(PaisId);

                resp.valor = termCondPol;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta VerEstadoSiniestros(string lang = "")
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.EstadoSiniestro> estadoSiniestro = new List<BE.EstadoSiniestro>();
                IEnumerable<decimal> llaves = new List<decimal> (){ 1, 2, 3, 4, 5 };
                estadoSiniestro = bcEstadoSiniestro.VerEstadoSiniestro(lang,null);

                resp.valor = estadoSiniestro;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta IngresoDinero(ref BE.BilleteraDetalle billeteraDetalle)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.BilleteraDetalle billdet = bcBilleteraDetalle.BuscarPorUID(billeteraDetalle.BilleteraDetalleUID);
                if (billdet==null)
                {
                    BE.Billetera billetera = bcBilletera.BuscarBilleteraxBilleteraId(Convert.ToInt32(billeteraDetalle.BilleteraId));
                    bolOk = bcBilleteraDetalle.RegistrarBilleteraDetalle(ref billeteraDetalle, billetera);
                }
                else
                {
                    billeteraDetalle.BilleteraId = billdet.BilleteraId;
                }
             
             
                resp.valor = billeteraDetalle;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta Verseguro(decimal ciudadId,string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";              

                List<BE.ConfiguracionCiudad> lstSeguro = new List<BE.ConfiguracionCiudad>();

                lstSeguro = bcSeguro.ListadoSeguro(ciudadId, lang);

                resp.valor = lstSeguro[0];

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta VerServicioPersona(decimal PersonaId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                List<BE.ServicioPersonaDocumento> lstServicioPersona = new List<BE.ServicioPersonaDocumento>();

                lstServicioPersona = bcServicioPersonaDocumento.ListadoServicioPersonaDocumento(PersonaId,"",null);

                resp.valor = lstServicioPersona;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public BE.ServicioPersona VerServicioPersonaxId(decimal spId)

        {
            BE.ServicioPersona obj = null;


            try
            {
             

               // obj =  bcServicioPersona.
               

            }
            catch (Exception ex)
            {                
            }
            return obj;
        }
        public Respuesta RegistrarPagoBilletera(decimal ServicioId, string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                List<BE.ConfiguracionCiudad> lstSeguro = new List<BE.ConfiguracionCiudad>();

              //  lstSeguro = bcSeguro.ListadoSeguro(ServicioId, lang, BE.relseguro.servicio);

                resp.valor = lstSeguro;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta verPersonaDireccion(decimal personaId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                List<BE.PersonaDireccion > lst = new List<BE.PersonaDireccion>();

                lst = bcPerDir .ListadoxPersona (personaId);

                resp.valor = lst;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta RegistrarPagoCliente(string ServAsig, SqlTransaction DataTransactionCom)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            BE.ServAsig item = new BE.ServAsig();
            BE.BilleteraDetalle itemBD = new BE.BilleteraDetalle();
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            decimal BilleteraId = 0;
            IEnumerable<string> llaves;
            try
            {
                if (item != null)
                {

                  item = bcServAsig.BuscarServAsigxId(ServAsig, BE.relServAsig.requiereServicio, BE.relRequiereServicio.persona, BE.relServAsig.servAsigCosto, BE.relServAsig.requiereServicio, BE.relRequiereServicio.servicio);
                  
                    bcBilleteraDetalle.RegistrarPagoCliente(ref item,  DataTransactionCom);

                  //  resp.codigo_respuesta = "00";
                    //resp.mensaje_respuesta = "PAGO APROBADO";
                    //resp.autorizacion = "";
                }
            }
            catch (Exception ex)
            {


             //   resp.codigo_respuesta = "22";
               // resp.mensaje_respuesta = ex.Message;
                //resp.autorizacion = "";
            }

            return resp;

        }
        public Respuesta getRendimientoFinanciero(decimal personaId)
        { 
                    
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                BE.RendimientoFinanciero obj = new BE.RendimientoFinanciero();
                obj = bcBilletera.getRendimientoFinanciero(personaId,DateTime .Now );
                resp.valor = obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta getRendimientoOperativo(decimal personaId)
        {

            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                BE.RendimientoOperativo obj = new BE.RendimientoOperativo();
                obj = bcBilletera.getRendimientoOperativo(personaId, DateTime.Now);
                resp.valor = obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public List<BE.Persona> ListadoEstadosSiniestros(decimal SiniestroId, decimal SiniestroEstadoId)
        {
            List<BE.Persona> lstPersonas = null;
            try
            {
                lstPersonas = bcPersona.ListadoEstadoSiniestros(SiniestroId, SiniestroEstadoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPersonas;
        }
        public int  ExisteRegistroSiniestroId(decimal SiniestroId)
        {
            int cant=0;
            try
            {
              cant = bcSiniestro.ExisteRegistroSiniestroId(SiniestroId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cant;
        }

        ///METODOS PARA NOTIFICACION 
        #region "NOTIFICACIONES"
        public DataSet ObtenerImporte_y_Calificacion(string ServAsigId)
        {
            DataSet datos = null;
            try
            {
                datos = bcServAsig.ObtenerImporte_y_Calificacion(ServAsigId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public Respuesta verNotificacionPersona(long personaId, string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                List<BE.NotificacionPersona> lstreqSer = new List<BE.NotificacionPersona>();

                lstreqSer = bcNotificacionPersona.ListadoNotificacionPersona(personaId, lang, BE.relNotificacionPersona.estadoNotificacion, BE.relNotificacionPersona.conceptoNotificacion,BE.relNotificacionPersona.tipoEstadoNotificacion);



            
                resp.valor = lstreqSer;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        #endregion
        public BE.Persona BuscarPersonaProveedroxId(decimal PersonaId)
        {
            BE.Persona Persona = null;
            try
            {
                Persona = bcPersona.BuscarPersonaxId(PersonaId, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Persona;
        }
        #region "METODOS AGREGADOS DESPUESDE UNION CON IOS"
        #endregion
        public Respuesta saveLogMovil(ref BE.LogMovil logMovil)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (logMovil.TipoEstado == TipoEstado.Insertar)
                {


            
                    logMovil.LogInformacionTelefono = logMovil.LogInformacionTelefono.Replace("\r\n"," ");
                    logMovil.LogVersion=logMovil.LogVersion.Replace("\r\n", " "); 
                    logMovil.LogDescripcion=logMovil.LogDescripcion.Replace("\r\n", " ");
                    logMovil.LogObservacion1=logMovil.LogObservacion1.Replace("\r\n", " ");
                    logMovil.LogObservacion2=logMovil.LogObservacion2.Replace("\r\n", " ");
                    bolOk = bcLogMovil.Actualizar(ref logMovil);

                    if (!bolOk)
                    {
                        throw new Exception("Error al registrar Solicitud.");
                    }
                

                  

                }
            
                //  bcReqSer.CargarRelaciones(ref requiereServicio, lang,relRequiereServicio.servicio);
                
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        #region "NotificacionesV2"
        public Respuesta saveNotificacionPersona(ref BE.NotificacionPersona notificacionPersona)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

              bolOk=  bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
                if (bolOk == true)
                {
                    resp.valor = notificacionPersona;
                }
                else
                {

                }
              
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public string VersionTelefono(decimal personaId)
        {
            string  bolOk = "";
            Respuesta resp = new Respuesta();
            try
            {
              
                bolOk = bclogSesionesPersona.VersionTelefono(personaId);
             

            }
            catch (Exception ex)
            {
             
            }
            return bolOk;
        }
        

        #endregion

        public Respuesta saveLogSesionesPersona(ref BE.LogSesionesPersona logSesionesPersona)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (logSesionesPersona.TipoEstado == TipoEstado.Insertar)
                {
                                                                          
                    bolOk = bclogSesionesPersona.RegistrarLogSesionesPersona(ref logSesionesPersona);
                    if (bolOk==true)
                    {
                        BE.Persona persona = new BE.Persona();
                        bcPersona.BuscarPersonaxId(logSesionesPersona.PersonaId);
                        resp.valor = persona;
                    }

                    if (!bolOk)
                    {
                        throw new Exception("Error al registrar Solicitud.");
                    }

              }

                //  bcReqSer.CargarRelaciones(ref requiereServicio, lang,relRequiereServicio.servicio);

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public int CantidadLeidoNotificacionPersona(long personaId, params Enum[] relaciones)
        {
            return bcNotificacionPersona.CantidadLeidoNotificacionPersona(personaId, null);

        }

        public Respuesta CantidadRequierServicioXEstado(long personaId, string lang, string EstadoReqServId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                int cantResSev = 0;

                cantResSev = bcRequiereServicio.CantidadRequiereServicioXEstado(personaId, lang, EstadoReqServId);


                resp.valor = cantResSev;


            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta cantidadVerRequiereServicio(long personaId, string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                int cantRS = 0;

                cantRS = bcRequiereServicio.cantidadRequiereServicio(personaId, lang);

                resp.valor = cantRS;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
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



        public Respuesta VerServicioDetalle(decimal servicioId, string lang,Boolean otros)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Servicio Obj = new BE.Servicio();
                Obj=  bcservicio.ListadoXServicioId(servicioId, lang,otros, BE.relServicio.servicioDetalle, relServicioDetalle.servicio,BE.relServicio.categoriaServicio,BE.relCategoriaServicio.Ciudad,BE.relciudad.region,BE.relRegion.pais,BE.relpais.moneda,BE.relServicio.servicioRequerimiento,BE.relServicio.servicioTexto, BE.relServicio.servicioDescripcion, BE.relciudad.configuracionCiudad);

                List<BE.RequiereServicio> lstreqSer = new List<BE.RequiereServicio>();             

                           
                resp.valor = Obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }




        public Respuesta saveCobranaCBA(ref BE.cobranzaCBA cobranzaCBA)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (cobranzaCBA.TipoEstado == TipoEstado.Insertar)
                {
                    BE.cobranzaCBA sini = bccobranzaCBA.BuscarPorUID(cobranzaCBA.cobranzaCBAUID);
                    //req = bcReqSer.CargarRelaciones()
                    if (sini == null)
                    {
                        bolOk = bccobranzaCBA.Actualizar(ref cobranzaCBA);
                        if (!bolOk)
                        {
                            throw new Exception("Error al registrar Solicitud.");
                        }

                    }
                    else
                    {

                        cobranzaCBA = sini;
                    }


                }
                else
                {

                }

                resp.valor = cobranzaCBA.cobranzaCBAId;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta verRequiereServicioDetalle(string RequiereServicioId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.requiereServicioDetalle> Obj = new List<BE.requiereServicioDetalle>();
                Obj = bcrequiereServicioDetalle.ListadoXrequiereServicioDetalle(RequiereServicioId);            
                 resp.valor = Obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta enviarCorreoVerificacionWeb(string correo, string URL, decimal personaId)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                ///////////////
                resp.estado = 1;
                resp.mensaje = "";
                string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoVerificacion"].ToString());
                text = text.Replace("URL", URL);
                BE.envioCorreo envioCorreo = new BE.envioCorreo();
                envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                envioCorreo.PersonaCorreo = correo;
                envioCorreo.Subject1 = "Enlace de Verificacion ServiceWeb";
                envioCorreo.Body = text;
                envioCorreo.Fecha = DateTime.Now;
                envioCorreo.TipoCorreo = "VerificacionWeb";
                envioCorreo.Estado = "Pendiente";
                envioCorreo.Descripcion = "VW_"+correo;
                bcenvioCorreo.Actualizar(ref envioCorreo, false);
                /////////////////
            
                resp.valor = "";

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta verServicio(decimal ServicioId,string lang)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Servicio Obj = new BE.Servicio();
                Obj = bcservicio.ListadoXServicioId(ServicioId, lang, true, relServicio.servicioRequerimiento);
                resp.valor = Obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta actualizar_passwordWeb(decimal idp, string password)

        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Persona Obj = new BE.Persona();
                Obj = bcPersona.actualizar_passwordWeb(idp,password, relServicio.servicioRequerimiento);
                resp.valor = Obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }



      
        public bool ver_Si_Rechazaron_todos_Prov(string requiereServicioId)
        {
            Boolean bolOk = false;
         
            try
            {
               
                bolOk = bcReqServProv.ver_Si_Rechazaron_todos_Prov(requiereServicioId,"",null);
            

            }
            catch (Exception ex)
            {
               
            }
            return bolOk;
        }

        public Respuesta UserProfile(string UserName, string Password)
        {
            
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.UserProfile Obj = new BE.UserProfile();
                Obj = bcuserProfile.verUserProfile(UserName,Password);
                resp.valor = Obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }


        public Respuesta Empresa_Usuario(int NIT)
        {

            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Empresa_Usuario Obj = new BE.Empresa_Usuario();
                Obj = bcempresaUsuario.verEmpresaUsuario(NIT, relEmpresa_Usuario.UserProfile);
                resp.valor = Obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta Empresa_Usuario_Corporativo(decimal PersonaId)
        {

            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Empresa_Usuario_Corporativo Obj = new BE.Empresa_Usuario_Corporativo();
                Obj = bcempresaUsuarioCorporativo.verEmpresaUsuarioCorporativo(PersonaId, relEmpresa_Usuario_Corporativo.Persona);
                resp.valor = Obj;

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }


        public List<BE.Persona> ver_lista_operaciones()
        {
            List<BE.Persona> lstPersonas = null;
            try
            {
                lstPersonas = bcPersona.ListadoOperaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPersonas;
        }




        public BE.Persona BuscarPersonaxId_enprueba(decimal PersonaId)
        {
            BE.Persona Persona = null;
            try
            {
                Persona = bcPersona.BuscarPersonaxId_enprueba(PersonaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Persona;
        }




    }
}