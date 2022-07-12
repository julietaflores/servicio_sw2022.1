using BC;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServiceWeb
{
    class Program
    {
        static HttpClient clientMediBook = new HttpClient();
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
            BasePath = "https://service-web-258723.firebaseio.com/"
        };
        IFirebaseClient client;
        private static Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
        private static string ServerKey = "AAAAo1TUZ_U:APA91bGZ-Lyd-fpdm8Qwcun_gwNGL997bZO6RcfA0JJQs56PzM1pGN4d4yOeDtwWDM0-5kr3qRCMrIbbAIWOPt5GpM8ngR8Aeum2AzEqmQEFUtBYoiPg7pxSFcey2XXA_8nKJpgaZuH3";
        private static string SenderIdFB = "701502875637";
        private static string UrlFBPN = "https://fcm.googleapis.com/fcm/send";
        private static BC.Persona bcPersona = null;
        private static BC.RequiereServicio bcRequiereServicio = null;
        private static BC.NotificacionPersona bcNotificacionPersona = null;
        private static BC.envioCorreo bcEnvioCorreo = null;
        private static BC.MediBook bcMediBook = null;
        private static BC.ServAsig bcServAsig = null;
        private static BC.ServAsigCosto bcServAsigCosto = null;
        private static BC.LogSesionesPersona bclogSesionesPersona = null;
        private static BC.PersonaDireccion bcPersonaDireccion = null;
        private static BC.Post bcPost = null;

        public static string valorr = "";


        SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["con"]);
        public Program()
        {

            //  contManager = new ControladorManager("cadenaCnx");
        }

        static async Task Main(string[] args)
        // static void Main(string[] args)
        {
            ////////////////////////OBTENER HORA BOLIVIANA
            //ObtenerHoraBoliviana();
            ////////////////////////////////////////////////////////////////////////ENVIAR NOTIFICACIONES A PROVEEDORES
            /* bcPersona = new BC.Persona("cadenaCnx");
                bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
                List<BE.Persona> lstPersona = new List<BE.Persona>();
               List<BE.RequiereServicio> LstrequiereServicio = new List<BE.RequiereServicio>();
               LstrequiereServicio = bcRequiereServicio.ListadoRequerimientos_Para_Adjudicar(null);
               foreach ( BE.RequiereServicio item in LstrequiereServicio)
               {
                   lstPersona = bcPersona.ListadoPersonaProveedores_paraAdjudicar(item.RequiereServicioId,null);
                   if (lstPersona.Count > 0)
                   {
                       await EnviarNotificacionesProveedores(lstPersona, "ProveedorV2", "es", item);

                   }
               }*/

            ///////////////////////////////////////////////////////////////////INSERCION MEDIBOOK
            /*   bcMediBook = new BC.MediBook("cadenaCnx");        
               InsercionMediBookV2();*/
            // Obtener_Especialidades();
            //////////////////////////////////////////////////////////////////ENVIO NOTIFICACION A TODOS
            // bcPersona = new BC.Persona("cadenaCnx");
            //  EnviarNotifcacion_a_todos();
            ////////////////////////////////////////////////////////////////////
            /*bcPersona = new BC.Persona("cadenaCnx");
               bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
               bcNotificacionPersona = new BC.NotificacionPersona("cadenaCnx");
               await EnviarNotifcacionProveedorIniciarServicioV1();*/
            ///////////////////////////////////////////////////////////////////ENVIO CORREO REGISTRO PERSONA
            /*   bcEnvioCorreo = new BC.envioCorreo("cadenaCnx");
               bcPersona = new BC.Persona("cadenaCnx");
                EnvioCorreoServiceWeb("nuevoUsuario"); 
                EnvioCorreoServiceWebRegistroPersona();*/
            ///////////////////////////////////////////////////////////////////////////////ENVIAR CORREO COTIZACIONES(CLIENTE)-------------------1
            /*  bcEnvioCorreo = new BC.envioCorreo("cadenaCnx");
                   EnvioCorreoCotizacionesServiceWeb();
                   EnvioCorreoServiceWeb("Cotizaciones");*/
            ///////////////////////////////////////////////////////////////////////////////ENVIAR CORREO ADJUDICACION (CLIENTE)(PROVEEDOR)------------2
            /* bcEnvioCorreo = new BC.envioCorreo("cadenaCnx");
                   EnvioCorreoAdjudicacionCliente();
                  EnvioCorreoServiceWeb("AdjudicacionCliente");       
                     EnvioCorreoAdjudicacionProveedor();           
                   EnvioCorreoServiceWeb("AdjudicacionProveedor");*/
            ///////////////////////////////////////////////////////////////////////////////ENVIAR CORREO FINSERVICIO (CLIENTE) (PROVEEDOR) 3
            /*  bcEnvioCorreo = new BC.envioCorreo("cadenaCnx");
                 EnvioCorreoFinServicioCliente();           
                 EnvioCorreoServiceWeb("FinServicioCliente");
                bcEnvioCorreo = new BC.envioCorreo("cadenaCnx");
                 EnvioCorreoFinServicioProveedor();
                 EnvioCorreoServiceWeb("FinServicioProveedor");*/
            //////////////////////////////////////////////////////////////////////////////////////
            // Crear objeto. Utiliza el reloj del sistema para crear una semilla.

            // Console.WriteLine(DateTime.Now.ToString() + "Eliminacion");
            //     EliminacionPersonasNoValidadas();
            /* Console.WriteLine(DateTime.Now.ToString() + "proceso Requiere Servicio Desierto:");
              CambiarEstadoDesierto();*/
            //   Console.WriteLine(DateTime.Now.ToString() + "proceso Finalizado ");

            /* Console.WriteLine(DateTime.Now.ToString() + "proceso Cotizazion:");
               await EnviarNotificacionCotizacionesv2();*/

            // Console.WriteLine(DateTime.Now.ToString() + "proceso Finalizado ");

            /*      Console.WriteLine(DateTime.Now.ToString() + "proceso Servicios:");//////////////////////////////////////INICIAR SERVICIO
                 bclogSesionesPersona = new BC.LogSesionesPersona("cadenaCnx");

                      await EnviarNotifcacionServicio();*/


            //    Console.WriteLine(DateTime.Now.ToString() + "proceso Finalizado ");
            //  Console.WriteLine(DateTime.Now.ToString() + "proceso iniciar servicio Proveedor:");

            //SendMail();
            //  Console.WriteLine(DateTime.Now.ToString() + "proceso Finalizado ");

            ///////////////////////////////////////////////////////////////////////////////////ENVIAR NOTIFICACION FINALIZANDO EL SERVICIO
            /* bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
             bcNotificacionPersona = new BC.NotificacionPersona("cadenaCnx");
             bcServAsig = new BC.ServAsig("cadenaCnx");
             bcPersona = new BC.Persona("cadenaCnx");
             await EnviarNotifcacionFinalizandoElServicio();*/
            ////////////////////////////////////////////////////////////////
            /* EnviandoCorreoServicioSolicitado();*/
            /*  bcEnvioCorreo = new BC.envioCorreo("cadenaCnx");
              EnvioCorreoAdjudicacionClienteConFormato();
               EnvioCorreoFinalizacionClienteConFormato();
               EnvioCorreoServiceWeb("AdjudicacionCliente"); 
              /* EnviandoCorreoServicioSolicitado2();
              //     EnvioCorreoPruebaHtml();*/
            /////////////////////////////////////////////////////////////////FINALIZAR TODOS LOS SERVICIOS
            /*   bcServAsigCosto = new BC.ServAsigCosto("cadenaCnx");
               bcPersona = new BC.Persona("cadenaCnx");
               bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
               bclogSesionesPersona = new BC.LogSesionesPersona("cadenaCnx");
               bcNotificacionPersona=new BC.NotificacionPersona("cadenaCnx");
              bcPost = new BC.Post("cadenaCnx");
           await FInalizarlosServicio();*/
            /////////////////////////////////////////////////////////////////VERIFICACION SERVICE WEB CODIGO VERIFICACION
            /* Console.WriteLine(DateTime.Now.ToString() + "Enviar Codigo de Verificacion:");
              bcEnvioCorreo = new BC.envioCorreo("cadenaCnx");
             EnvioCorreoServiceWeb("VerificacionWeb");*/
            ///////////////////////////////////////////////////////////////////CORREOS AUTOMATICOS
            /*bcEnvioCorreo = new BC.envioCorreo("cadenaCnx");
             EnvioCorreoAdjudicacionClienteConFormato();
              EnvioCorreoFinalizacionClienteConFormato();*/
            ////////////////////////////////////////////////////////////////////////////////////////
            //ENVIAR NOTIFIACIONES DESIERTO //n

            /*bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
            bcPersona = new BC.Persona("cadenaCnx");
            bclogSesionesPersona = new BC.LogSesionesPersona("cadenaCnx");
            bcNotificacionPersona = new BC.NotificacionPersona("cadenaCnx");
            await    CambiarEstadoDesierto_Mas_de_dosHoras_sin_Tomar();*/

            //////////////////////////////////////////ENVIAR NOTIFICACIONES DE NUEVO REQUERIMIENTO
            /*bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
            bcPersona = new BC.Persona("cadenaCnx");
            bclogSesionesPersona = new BC.LogSesionesPersona("cadenaCnx");
            bcNotificacionPersona = new BC.NotificacionPersona("cadenaCnx");
            await EnviarNotificacionesNuevoRequerimiento();*/

            ////////////////////////////////////////////////////////////////////////////////////MIGRAR INFORMACION DE SERVICEWEB VEN-a-SERVICEWEB
            bcPersonaDireccion = new BC.PersonaDireccion("cadenaCnx");
            CargarTablasServiceWeb();
            //////////////////////////////////////////////
            /*  bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
              Actualizar_Adjudicacion();*/
            //////////////////////////////////////////////// NOTIFICACION PROVEEDOR FINALIZANDO LOS SERVICIO 
            /*   bcPersona = new BC.Persona("cadenaCnx");
               bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
               bclogSesionesPersona = new BC.LogSesionesPersona("cadenaCnx");
               bcNotificacionPersona = new BC.NotificacionPersona("cadenaCnx");
               await   BloquearProveedoresSinPagarComision();
             //  await NotificacionProveedorPagarComision();
             // listar_cantidad_proveedores_x_servicio();


             ///////////////////////////////////////////////NOTIFICACION DE VENCIMIENTO DE PAGO (pagar comision)
             /*    bcPersona = new BC.Persona("cadenaCnx");
                 bcRequiereServicio = new BC.RequiereServicio("cadenaCnx");
                 bclogSesionesPersona = new BC.LogSesionesPersona("cadenaCnx");
                 bcNotificacionPersona = new BC.NotificacionPersona("cadenaCnx");
                  await NotificacionProveedorPagarComision();*/



        }

        public static void IniciandoLosServiciosPendiente()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select * from RequiereServicio rs with(nolock) inner join servasig sa with(nolock) on rs.RequiereServicioId = sa.RequiereServicioId inner join persona per with(nolock) on per.PersonaId = sa.ProveedorId and sa.ServAsigFHInicio is null and rs.EstadoReqServId = 2 and rs.RequiereServicioFHDeseada < getdate() order by  rs.RequiereServicioFHDeseada asc";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((dr["ServAsigFHInicio"].ToString() == ""))
                    {
                        cmd.Connection = cnn;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@ServAsigFHInicio", SqlDbType.DateTime).Value = DateTime.Now;

                        cmd.Parameters.Add("@ServAsigPagaCliente", SqlDbType.Bit).Value = 0;
                        cmd.Parameters.Add("@ServAsigCalificado", SqlDbType.Bit).Value = 0;
                        cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = (dr["RequiereServicioId"].ToString());
                        cmd.CommandText = "update ServAsig set ServAsigFHInicio = @ServAsigFHInicio,StatusServAsigId = 2 where RequiereServicioId = @RequiereServicioId ";
                        cmd.ExecuteNonQuery();
                    }


                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static async Task NotificacionProveedorServicioAFinalizar()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select * from RequiereServicio rs with(nolock) inner join servasig sa with(nolock) on rs.RequiereServicioId = sa.RequiereServicioId inner join persona per with(nolock) on per.PersonaId = sa.ProveedorId and sa.ServAsigFHFin is null AND SA.ServAsigFHInicio is not null and rs.EstadoReqServId = 2 and rs.RequiereServicioFHDeseada < getdate() order by  sa.ServAsigFHInicio asc";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BE.Persona PersonaProveedor = bcPersona.BuscarPersonaxId(Convert.ToDecimal(dr["ProveedorId"].ToString()), "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                    BE.RequiereServicio rs = new BE.RequiereServicio();
                    Respuesta r = new Respuesta();
                    rs = bcRequiereServicio.BuscarRequiereServicioxId(dr["RequiereServicioid"].ToString(), PersonaProveedor.PersonaIdioma, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relCategoriaServicio.Ciudad, BE.relciudad.configuracionCiudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);

                    await EnviarNotificacionesAsyncClienteFinal(PersonaProveedor, "ProveedorFinServicio", PersonaProveedor.PersonaIdioma, rs, 0);
                    InsertarLogNotificacion("NotificacionProveedorServicioAFinalizar", DateTime.Now, "", 0);
                }


            }
            catch (Exception ex)
            {
                InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS_pasada_2Horas", DateTime.Now, ex.Message, 0);
                throw ex;
            }

        }
        public static async Task NotificacionProveedorPagarComision()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select rs.requiereServicioid,sa.proveedorId,sa.ServAsigFHFin,dateadd(day, 2, sa.ServAsigFHFin)fecha2dias,convert(date, sa.ServAsigFHFin)fechaFin,DATEPART(Hour, sa.ServAsigFHFin)HoraFin from requiereServicio rs with(nolock) inner join servAsig sa with(nolock) on rs.requiereServicioId = sa.requiereServicioid   inner join persona perr with(nolock) on rs.PersonaId = perr.personaId  inner join persona per with(nolock) on sa.proveedorId = per.personaId and rs.EstadoReqServId = 2 and sa.ServAsigFHFin is not null and sa.ServAsigFHPago is not null and sa.StatusServAsigId = 3   and perr.CiudadId=2 and per.CiudadId=2 and rs.requiereServicioid not in (SELECT title as requiereServicioid  FROM[dbo].[Log_Notificacion] WHERE deviceTokens = 'ProveedorPagarComisión') order by rs.requiereServicioFechaHoraReq asc  ";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DateTime hora = Convert.ToDateTime(dr["fecha2dias"].ToString()).Date;
                    DateTime hora2 = ObtenerHoraBoliviana().Date;
                    if (hora2.Date >= hora.Date)
                    {
                        DateTime FechaFin = Convert.ToDateTime(dr["ServAsigFHFin"].ToString());
                        if (FechaFin.Hour == DateTime.Now.Hour)
                        {
                            BE.Persona PersonaProveedor = bcPersona.BuscarPersonaxId(Convert.ToDecimal(dr["ProveedorId"].ToString()), "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                            BE.RequiereServicio rs = new BE.RequiereServicio();
                            Respuesta r = new Respuesta();
                            rs = bcRequiereServicio.BuscarRequiereServicioxId(dr["RequiereServicioid"].ToString(), PersonaProveedor.PersonaIdioma, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relCategoriaServicio.Ciudad, BE.relciudad.configuracionCiudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);


                            await EnviarNotificacionesAsyncClienteFinal(PersonaProveedor, "ProveedorPagarComisión", PersonaProveedor.PersonaIdioma, rs, 0);
                            InsertarLogNotificacion(dr["RequiereServicioid"].ToString(), DateTime.Now, "ProveedorPagarComisión", Convert.ToDecimal(dr["ProveedorId"].ToString()));
                            InsertarLogNotificacion("NotificacionProveedorPagarComision", DateTime.Now, "", 0);
                        }
                    }


                }


            }
            catch (Exception ex)
            {
                InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS_paga_comision", DateTime.Now, ex.Message, 0);
                throw ex;
            }

        }
        public static async Task BloquearProveedoresSinPagarComision()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            try
            {

                //select rs.requiereServicioid,sa.proveedorId,sa.ServAsigFHFin,dateadd(day, 2, sa.ServAsigFHFin)fecha2dias,dateadd(Hour, 52, sa.ServAsigFHFin)fecha2dias4horas,convert(date, sa.ServAsigFHFin) fechaFin, DATEPART(Hour, sa.ServAsigFHFin)HoraFin from requiereServicio rs with(nolock) inner join servAsig sa with(nolock) on rs.requiereServicioId = sa.requiereServicioid  inner join persona perr with(nolock) on rs.PersonaId = perr.personaId  inner join persona per with(nolock) on sa.proveedorId = per.personaId and rs.EstadoReqServId = 2 and sa.ServAsigFHFin is not null and sa.ServAsigFHPago is not null and sa.StatusServAsigId = 3  and sa.proveedorid=1469 and perr.CiudadId=2 and per.CiudadId=2 and sa.requiereservicioid not in (SELECT title as requiereservicioid  FROM[dbo].[Log_Notificacion]  WHERE deviceTokens = 'BloquearProveedoresSinPagarComision')  order by rs.requiereServicioFechaHoraReq asc  


                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select rs.requiereServicioid,sa.proveedorId,sa.ServAsigFHFin,dateadd(day, 2, sa.ServAsigFHFin)fecha2dias,dateadd(Hour, 52, sa.ServAsigFHFin)fecha2dias4horas,convert(date, sa.ServAsigFHFin) fechaFin, DATEPART(Hour, sa.ServAsigFHFin)HoraFin from requiereServicio rs with(nolock) inner join servAsig sa with(nolock) on rs.requiereServicioId = sa.requiereServicioid  inner join persona perr with(nolock) on rs.PersonaId = perr.personaId  inner join persona per with(nolock) on sa.proveedorId = per.personaId and rs.EstadoReqServId = 2 and sa.ServAsigFHFin is not null and sa.ServAsigFHPago is not null and sa.StatusServAsigId = 3  and perr.CiudadId=2 and per.CiudadId=2 and sa.requiereservicioid not in (SELECT title as requiereservicioid  FROM[dbo].[Log_Notificacion]  WHERE deviceTokens = 'BloquearProveedoresSinPagarComision')  order by rs.requiereServicioFechaHoraReq asc  ";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DateTime hora = Convert.ToDateTime(dr["fecha2dias4horas"].ToString()).Date;
                    DateTime hora2 = ObtenerHoraBoliviana().Date;
                    if (hora2.Date >= hora.Date)
                    {
                        DateTime fecha52horas = Convert.ToDateTime(dr["fecha2dias4horas"].ToString());
                        if (fecha52horas.Hour == DateTime.Now.Hour)
                        {
                            BE.Persona PersonaProveedor = bcPersona.BuscarPersonaxId(Convert.ToDecimal(dr["ProveedorId"].ToString()), "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                            BE.RequiereServicio rs = new BE.RequiereServicio();
                            Respuesta r = new Respuesta();
                            rs = bcRequiereServicio.BuscarRequiereServicioxId(dr["RequiereServicioid"].ToString(), PersonaProveedor.PersonaIdioma, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relCategoriaServicio.Ciudad, BE.relciudad.configuracionCiudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                            //   BE.Persona PersonaProveedor1 = bcPersona.BuscarPersonaxId(1469, "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                            BloquearProveedor(PersonaProveedor);

                            await EnviarNotificacionesAsyncClienteFinal(PersonaProveedor, "ProveedorBloqueado", PersonaProveedor.PersonaIdioma, rs, 0);
                            await CantidadProveedores(PersonaProveedor);
                            BloquearProveedor_log_notificacion(PersonaProveedor);
                            //   InsertarLogNotificacion( dr["RequiereServicioid"].ToString(), DateTime.Now, "BloquearProveedoresSinPagarComision", Convert.ToDecimal(dr["ProveedorId"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertarLogNotificacion("BloquearProveedoresSinPagarComision", DateTime.Now, ex.Message, 0);
                throw ex;
            }

        }
        public static void BloquearProveedor(BE.Persona persona)
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            decimal proveedorid = persona.PersonaId;
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = "update serviciopersona set estadoservicioid=2 , statusservicioid=2 where personaid=@personaid";
                cmd.Parameters.Add("@personaid", SqlDbType.VarChar).Value = proveedorid;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                InsertarLogNotificacion("BloquearProveedor", DateTime.Now, "", proveedorid);

            }
            catch (Exception ex)
            {
                InsertarLogNotificacion("BloquearProveedor", DateTime.Now, ex.Message, proveedorid);
            }
        }
        public static void BloquearProveedor_log_notificacion(BE.Persona persona)
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            decimal proveedorid = persona.PersonaId;
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select rs.requiereServicioid,sa.proveedorId,sa.ServAsigFHFin,dateadd(day, 2, sa.ServAsigFHFin)fecha2dias,dateadd(Hour, 52, sa.ServAsigFHFin)fecha2dias4horas,convert(date, sa.ServAsigFHFin) fechaFin, DATEPART(Hour, sa.ServAsigFHFin)HoraFin from requiereServicio rs with(nolock) inner join servAsig sa with(nolock) on rs.requiereServicioId = sa.requiereServicioid  inner join persona perr with(nolock) on rs.PersonaId = perr.personaId  inner join persona per with(nolock) on sa.proveedorId = per.personaId and rs.EstadoReqServId = 2 and sa.ServAsigFHFin is not null and sa.ServAsigFHPago is not null and sa.StatusServAsigId = 3  and sa.proveedorid=@personaid  and perr.CiudadId=2 and per.CiudadId=2 and sa.requiereservicioid not in (SELECT title as requiereservicioid  FROM[dbo].[Log_Notificacion]  WHERE deviceTokens = 'BloquearProveedoresSinPagarComision')  order by rs.requiereServicioFechaHoraReq asc  ";
                cmd.Parameters.Add("@personaid", SqlDbType.VarChar).Value = proveedorid;
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    InsertarLogNotificacion(dr["RequiereServicioid"].ToString(), DateTime.Now, "BloquearProveedoresSinPagarComision", Convert.ToDecimal(dr["ProveedorId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                InsertarLogNotificacion("BloquearProveedoresSinPagarComision", DateTime.Now, ex.Message, 0);
                throw ex;
            }
        }

        public static async Task NotificacionProveedorPagarComisiontodos()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select rs.requiereServicioid,sa.proveedorId,sa.ServAsigFHFin,dateadd(day, 2, sa.ServAsigFHFin)fecha2dias,convert(date, sa.ServAsigFHFin)fechaFin,DATEPART(Hour, sa.ServAsigFHFin)HoraFin from requiereServicio rs with(nolock) inner join servAsig sa with(nolock) on rs.requiereServicioId = sa.requiereServicioid   inner join persona perr with(nolock) on rs.PersonaId = perr.personaId  inner join persona per with(nolock) on sa.proveedorId = per.personaId and rs.EstadoReqServId = 2 and sa.ServAsigFHFin is not null and sa.ServAsigFHPago is not null and sa.StatusServAsigId = 3   and perr.CiudadId=2 and per.CiudadId=2 and rs.requiereServicioid not in (SELECT title as requiereServicioid  FROM[dbo].[Log_Notificacion] WHERE deviceTokens = 'ProveedorPagarComisión') order by rs.requiereServicioFechaHoraReq asc  ";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DateTime hora = Convert.ToDateTime(dr["fecha2dias"].ToString()).Date;
                    DateTime hora2 = ObtenerHoraBoliviana().Date;
                    if (hora2.Date >= hora.Date)
                    {
                        // DateTime FechaFin = Convert.ToDateTime(dr["ServAsigFHFin"].ToString());
                        // if (FechaFin.Hour == DateTime.Now.Hour)
                        // {
                        BE.Persona PersonaProveedor = bcPersona.BuscarPersonaxId(Convert.ToDecimal(dr["ProveedorId"].ToString()), "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                        BE.RequiereServicio rs = new BE.RequiereServicio();
                        Respuesta r = new Respuesta();
                        rs = bcRequiereServicio.BuscarRequiereServicioxId(dr["RequiereServicioid"].ToString(), PersonaProveedor.PersonaIdioma, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relCategoriaServicio.Ciudad, BE.relciudad.configuracionCiudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                        await EnviarNotificacionesAsyncClienteFinal(PersonaProveedor, "ProveedorPagarComisión", PersonaProveedor.PersonaIdioma, rs, 0);
                        InsertarLogNotificacion(dr["RequiereServicioid"].ToString(), DateTime.Now, "ProveedorPagarComisión", Convert.ToDecimal(dr["ProveedorId"].ToString()));
                        InsertarLogNotificacion("NotificacionProveedorPagarComisiontodos", DateTime.Now, "", 0);
                        //}
                    }


                }


            }
            catch (Exception ex)
            {
                InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS_paga_comision_todos", DateTime.Now, ex.Message, 0);
                throw ex;
            }

        }

        public static async Task FInalizarlosServicio()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select* from RequiereServicio rs with(nolock) inner join servasig sa with(nolock) on rs.RequiereServicioId = sa.RequiereServicioId inner join servicio s on rs.ServicioId = s.ServicioId where EstadoReqServId = 2 and sa.statusServAsigId IN(1, 2) AND sa.ServAsigFHFin is null and(rs.RequiereServicioFHDeseada) <= (getdate() - 1) order by  sa.ServAsigFHInicio asc";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if ((dr["ServAsigFHInicio"].ToString() == ""))
                            {
                                cmd.Connection = cnn;
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add("@ServAsigFHInicio", SqlDbType.DateTime).Value = DateTime.Now;
                                cmd.Parameters.Add("@ServAsigFHFin", SqlDbType.DateTime).Value = DateTime.Now;
                                cmd.Parameters.Add("@ServAsigFHPago", SqlDbType.DateTime).Value = DateTime.Now;
                                cmd.Parameters.Add("@ServAsigPagaCliente", SqlDbType.Bit).Value = 0;
                                cmd.Parameters.Add("@ServAsigCalificado", SqlDbType.Bit).Value = 0;
                                cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = (dr["RequiereServicioId"].ToString());
                                cmd.CommandText = "update ServAsig set ServAsigFHInicio = @ServAsigFHInicio,ServAsigFHFin =" +
                                    " @ServAsigFHFin,ServAsigFHPago=@ServAsigFHPago,ServAsigPagaCliente=@ServAsigPagaCliente,ServAsigCalificado=@ServAsigCalificado," +
                                    "StatusServAsigId = 3 where RequiereServicioId = @RequiereServicioId ";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add("@ServAsigFHFin", SqlDbType.DateTime).Value = DateTime.Now;
                                cmd.Parameters.Add("@ServAsigFHPago", SqlDbType.DateTime).Value = DateTime.Now;
                                cmd.Parameters.Add("@ServAsigPagaCliente", SqlDbType.Bit).Value = 0;
                                cmd.Parameters.Add("@ServAsigCalificado", SqlDbType.Bit).Value = 0;
                                cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = (dr["RequiereServicioId"].ToString());
                                cmd.CommandText = "update ServAsig set ServAsigFHFin = @ServAsigFHFin ,ServAsigFHPago = @ServAsigFHPago,ServAsigPagaCliente=@ServAsigPagaCliente,ServAsigCalificado=@ServAsigCalificado,StatusServAsigId = 3 where" +
                                    " RequiereServicioId = @RequiereServicioId ";
                                cmd.ExecuteNonQuery();
                            }

                            SqlCommand command = new SqlCommand("ImporteRequiereServicio_ConDetalle", cnn);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@RequiereServicioId", (dr["RequiereServicioId"].ToString()));
                            bool ServicioDetalleTipo = false;
                            if (dr["ServicioDetalleTipo"].ToString() == "")
                            {
                            }
                            else
                            {
                                ServicioDetalleTipo = Convert.ToBoolean((dr["ServicioDetalleTipo"].ToString()));
                            }

                            if (dr["RequiereServicioOtros"].ToString() == "True") { ServicioDetalleTipo = true; }
                            command.Parameters.AddWithValue("@ServicioDetalleTipo", ServicioDetalleTipo);
                            DataSet ds1 = new DataSet();
                            SqlDataAdapter da1 = new SqlDataAdapter(command);
                            da1.Fill(ds1);
                            int i = 1;
                            while (i <= 6)
                            {
                                BE.ServAsigCosto servAsigCosto = new BE.ServAsigCosto();
                                servAsigCosto.ServAsigId = ((dr["ServAsigId"].ToString()));
                                servAsigCosto.ConceptoCostoId = i;
                                servAsigCosto.ServAsigCostoValor = 0;
                                servAsigCosto.TipoEstado = BE.TipoEstado.Insertar;
                                if (servAsigCosto.ConceptoCostoId == 1)
                                {
                                    if (ds1.Tables[0].Rows[0]["tarifaMinima"].ToString() != "")
                                        servAsigCosto.ServAsigCostoValor = Convert.ToDecimal(ds1.Tables[0].Rows[0]["tarifaMinima"].ToString());
                                }
                                if (servAsigCosto.ConceptoCostoId == 3)
                                {
                                    if (ds1.Tables[0].Rows[0]["porcentaje"].ToString() != "")
                                        servAsigCosto.ServAsigCostoValor = Convert.ToDecimal(ds1.Tables[0].Rows[0]["porcentaje"].ToString());
                                }
                                if (servAsigCosto.ConceptoCostoId == 5)
                                {
                                    servAsigCosto.ServAsigCostoValor = Convert.ToDecimal(ds1.Tables[0].Rows[0]["seguro"].ToString());

                                }
                                if (servAsigCosto.ConceptoCostoId == 4)
                                {
                                    servAsigCosto.ServAsigCostoValor = Convert.ToDecimal(ds1.Tables[0].Rows[0]["ConfiguracionCiudadValorBroker"].ToString());

                                }
                                bcServAsigCosto.Actualizar(ref servAsigCosto, false);

                                i++;

                            }

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = (dr["RequiereServicioId"].ToString());
                            decimal importe = 0;
                            if (ds1.Tables[0].Rows[0]["tarifaMinima"].ToString() != "")
                            {
                                importe = Convert.ToDecimal(ds1.Tables[0].Rows[0]["tarifaMinima"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[0]["porcentaje"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[0]["seguro"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[0]["ConfiguracionCiudadValorBroker"].ToString());
                            }
                            cmd.Parameters.Add("@ServAsigCostoTotal", SqlDbType.Money).Value = importe;
                            cmd.CommandText = "update ServAsig set ServAsigCostoTotal=@ServAsigCostoTotal where RequiereServicioId = @RequiereServicioId ";
                            cmd.ExecuteNonQuery();
                            ///////////////////////////INSERTANDO POST
                            BE.Post post = new BE.Post();
                            post.TipoPostId = 1;
                            post.PostDescripcion = "";
                            post.PostEnlace = "";
                            post.PostContenidoLast = 0;
                            post.PostFechaInsercion = DateTime.Now;
                            post.PostUsuario = "proceso_Automatico";
                            post.PostUID = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "ProcesoAutomatico";
                            post.PostLikesLast = 0;
                            post.ServAsigId = (dr["ServAsigId"].ToString());
                            post.PostComentario = "";
                            post.PersonaPostId = Convert.ToDecimal(dr["PersonaId"].ToString());
                            post.PostCalificacion = 0;
                            post.PostAutorizaPublicacionImagen = false;
                            post.PostComentarioAprobacion = false;
                            post.TipoEstado = BE.TipoEstado.Insertar;
                            bcPost.Actualizar(ref post, false);
                            /////////////////////////////

                            DataSet datos = new DataSet();
                            datos = ObtenerImporte_y_Calificacion(dr["ServAsigId"].ToString());
                            decimal ImporteProveedor = Convert.ToDecimal(datos.Tables[0].Rows[0]["ImporteProveedor"].ToString());
                            decimal calificacion = Convert.ToDecimal(datos.Tables[0].Rows[0]["PostCalificacion"].ToString());
                            if (calificacion == 0)
                            {
                                /* BE.Persona Persona = bcPersona.BuscarPersonaxId(Convert.ToDecimal(dr["PersonaId"].ToString()), "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                                 BE.Persona PersonaProveedor = bcPersona.BuscarPersonaxId(Convert.ToDecimal(dr["ProveedorId"].ToString()), "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                                 List<BE.Persona> lstPersonaServicioFinalizado = bcPersona.ListadoPersonaFinServicio(dr["ServAsigId"].ToString(), null);
                                 BE.RequiereServicio rs = new BE.RequiereServicio();
                                 Respuesta r = new Respuesta();
                                 rs = bcRequiereServicio.BuscarRequiereServicioxId(dr["RequiereServicioid"].ToString(), Persona.PersonaIdioma, BE.relRequiereServicio.servicio, BE.relServicio.categoriaServicio, BE.relCategoriaServicio.Ciudad, BE.relciudad.configuracionCiudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda
                     , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioProveedores, BE.relReqServProv.servicioPersona, BE.relRequiereServicio.personaDireccion
                     , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere, BE.relRequiereServicio.servAsig, BE.relServAsig.statusServAsig, BE.relServAsig.post, BE.relServAsig.servAsigCosto);

                                 // await EnviarNotificacionesAsyncV3(lstPersonaServicioFinalizado, "ClienteFinServicio", lang, rs, "", sa.RequiereServicioId);
                                 // await OfertaFirebase(rs.RequiereServicioId, Convert.ToInt32(rs.PersonaId), Convert.ToString(sa.StatusServAsigId), "recibido", "Requerimientos");
                                 await EnviarNotificacionesAsyncClienteFinal(Persona, "ClienteFinServicio", Persona.PersonaIdioma, rs, importe);
                                 await OfertaFirebase(dr["RequiereServicioid"].ToString(), Convert.ToInt32(dr["PersonaId"].ToString()), "3", "recibido", "Requerimientos");
                                 await EnviarNotificacionesAsyncClienteFinal(PersonaProveedor, "ProveedorFinServicioFinal", PersonaProveedor.PersonaIdioma, rs, ImporteProveedor, calificacion);*/

                                //////////////////


                                //////////////////
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
            /////////////////////////////////////////////////////////////////////////////

        }
        public static void Actualizar_Adjudicacion()
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaCnx"]);
            try
            {

                SqlCommand cmd = new SqlCommand("select C2.ServAsigId, C2.ProveedorId, C2.RequiereServicioId, C2.cant, C2.ServicioId, sp.ServicioPersonaId  from (select C1.ServAsigId, C1.ProveedorId, C1.RequiereServicioId, C1.cant, rs.ServicioId from (select * from(select sa.ServAsigId, sa.ProveedorId, rsp.RequiereServicioid,count(StatusRequiereId) cant from requiereServicioProveedores rsp with(nolock) inner join ServAsig  sa with(nolock) on rsp.requiereServicioid = sa.requiereServicioid where rsp.StatusRequiereId = 4 group by rsp.requiereServicioId, sa.ServAsigId, sa.ProveedorId)c1   where cant > 1 )C1 inner join RequiereServicio rs with(nolock) on c1.RequiereServicioId = rs.RequiereServicioId )C2 inner join servicioPersona sp with(nolock) on C2.proveedorId = sp.personaId and sp.Servicioid = c2.Servicioid and sp.EstadoServicioId = 1 AND sp.StatusServicioId = 1 ", conexion);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand comandInsFact = new SqlCommand();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                conexion.Open();
                da.Fill(ds);
                foreach (DataRow drClien in ds.Tables[0].Rows)
                {
                    comandInsFact.Connection = conexion;
                    comandInsFact.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar, 500).Value = (drClien["RequiereServicioId"].ToString());
                    comandInsFact.CommandText = "update requiereServicioProveedores set  StatusRequiereId=1 where requiereServicioiD=@RequiereServicioId";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                    comandInsFact.Connection = conexion;
                    comandInsFact.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar, 500).Value = (drClien["RequiereServicioId"].ToString());
                    comandInsFact.Parameters.Add("@ServicioPersonaId", SqlDbType.VarChar, 500).Value = (drClien["ServicioPersonaId"].ToString());
                    comandInsFact.CommandText = "update requiereServicioProveedores set  StatusRequiereId=4 where requiereServicioiD=@RequiereServicioId and ServicioPersonaId=@ServicioPersonaId";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();

                    InsertarLogNotificacion("ADJUDICACION_DOBLE", DateTime.Now, drClien["RequiereServicioId"].ToString(), 1);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

        }
        public static void CargarTablasServiceWeb()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatosServiceWeb"]);
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaCnx"]);
            SqlCommand comandInsFact = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            cnn.Open();
            try
            {
                comandInsFact.Connection = cnn;

                List<BE.PersonaDireccion> lstPersonaDireccion = bcPersonaDireccion.ListadoPersonaDireccion();

                comandInsFact.Connection = cnn;
                comandInsFact.CommandText = "delete from PersonaDireccion";
                comandInsFact.ExecuteNonQuery();
                comandInsFact.Parameters.Clear();

                foreach (var personaDireccion in lstPersonaDireccion)
                {
                    comandInsFact.Parameters.Add("@PersonaId", SqlDbType.Decimal).Value = personaDireccion.PersonaId;
                    comandInsFact.Parameters.Add("@PersonaDireccionId", SqlDbType.Decimal).Value = personaDireccion.PersonaDireccionId;
                    comandInsFact.Parameters.Add("@TipoDireccionId", SqlDbType.Decimal).Value = personaDireccion.TipoDireccionId;
                    comandInsFact.Parameters.Add("@PersonaDireccionTitulo", SqlDbType.VarChar, 200).Value = personaDireccion.PersonaDireccionTitulo;
                    comandInsFact.Parameters.Add("@PersonaDireccionGeo", SqlDbType.VarChar, 50).Value = personaDireccion.PersonaDireccionGeo;
                    comandInsFact.Parameters.Add("@PersonaDireccionDescripcion", SqlDbType.VarChar, 200).Value = personaDireccion.PersonaDireccionDescripcion;
                    comandInsFact.Parameters.Add("@CiudadDireccionId", SqlDbType.Decimal).Value = personaDireccion.CiudadDireccionId;
                    comandInsFact.Parameters.Add("@PersonaDireccionFHMod", SqlDbType.DateTime).Value = personaDireccion.PersonaDireccionFHMod;
                    comandInsFact.Parameters.Add("@PersonaDireccionUsuarioMod", SqlDbType.VarChar, 40).Value = personaDireccion.PersonaDireccionUsuarioMod;
                    comandInsFact.Parameters.Add("@EstadoDireccionId", SqlDbType.Decimal).Value = personaDireccion.EstadoDireccionId;
                    comandInsFact.Parameters.Add("@PersonaDireccionDireccion", SqlDbType.VarChar, 500).Value = personaDireccion.PersonaDireccionDireccion;
                    comandInsFact.CommandText = "insert into  PersonaDireccion(PersonaId, PersonaDireccionId, TipoDireccionId, PersonaDireccionTitulo, PersonaDireccionGeo, PersonaDireccionDescripcion,CiudadDireccionId, PersonaDireccionFHMod, PersonaDireccionUsuarioMod, EstadoDireccionId, PersonaDireccionDireccion) values (@PersonaId, @PersonaDireccionId, @TipoDireccionId, @PersonaDireccionTitulo, @PersonaDireccionGeo, @PersonaDireccionDescripcion,@CiudadDireccionId, @PersonaDireccionFHMod, @PersonaDireccionUsuarioMod, @EstadoDireccionId, @PersonaDireccionDireccion) ";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                }

                ////////////////////////////////////////////////////////////////////////////////
                DataSet ds1 = new DataSet();
                SqlCommand cmd = new SqlCommand("select * from [dbo].[PersonalPreuba]", conexion);
                da.SelectCommand = cmd;
                da.Fill(ds1);
                comandInsFact.Connection = cnn;
                comandInsFact.CommandText = "delete from [PersonalPreuba]";
                comandInsFact.ExecuteNonQuery();
                comandInsFact.Parameters.Clear();
                comandInsFact.Connection = cnn;

                foreach (DataRow drClien in ds1.Tables[0].Rows)
                {

                    comandInsFact.Parameters.Add("@Id", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["Id"].ToString());
                    comandInsFact.Parameters.Add("@PersonaId", SqlDbType.VarChar, 200).Value = Convert.ToDecimal(drClien["PersonaId"].ToString());
                    comandInsFact.CommandText = "insert into  PersonalPreuba(Id, PersonaId) values (@Id, @PersonaId) ";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                }
                //////////////////////////////////////////////////////////////////////////////
                DataSet ds2 = new DataSet();
                SqlCommand cmdcs = new SqlCommand("select cs.CategoriaServicioId,cs.CategoriaServicioNombre,s.ServicioId,s.ServicioNombre,sp.PersonaId from CategoriaServicioSP('es') cs inner join ServicioSP('es') s on cs.CategoriaServicioId = s.CategoriaServicioId inner join ServicioPersona sp with(nolock) on s.ServicioId = sp.ServicioId and sp.StatusServicioId = 1 and sp.EstadoServicioId = 1   and cs.CiudadId=2", conexion);
                da.SelectCommand = cmdcs;
                da.Fill(ds2);
                comandInsFact.Connection = cnn;

                comandInsFact.CommandText = "delete from [vwCategoriaServicioProveedor]";
                comandInsFact.ExecuteNonQuery();
                comandInsFact.Parameters.Clear();
                comandInsFact.Connection = cnn;
                foreach (DataRow drClien in ds2.Tables[0].Rows)
                {

                    comandInsFact.Parameters.Add("@CategoriaServicioId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["CategoriaServicioId"].ToString());
                    comandInsFact.Parameters.Add("@CategoriaServicioNombre", SqlDbType.VarChar, 200).Value = (drClien["CategoriaServicioNombre"].ToString());
                    comandInsFact.Parameters.Add("@ServicioId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ServicioId"].ToString());
                    comandInsFact.Parameters.Add("@ServicioNombre", SqlDbType.VarChar, 200).Value = (drClien["ServicioNombre"].ToString());
                    comandInsFact.Parameters.Add("@PersonaId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["PersonaId"].ToString());
                    comandInsFact.CommandText = "INSERT INTO [dbo].[vwCategoriaServicioProveedor] (CategoriaServicioId,CategoriaServicioNombre,ServicioId,ServicioNombre,PersonaId) VALUES (@CategoriaServicioId,@CategoriaServicioNombre,@ServicioId,@ServicioNombre,@PersonaId) ";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////COSTOS SERVICIOS EJECUTADOS
                DataSet ds3 = new DataSet();
                SqlCommand cmdse = new SqlCommand("select * from [vwCostosServiciosEjecutados]", conexion);
                da.SelectCommand = cmdse;
                da.Fill(ds3);
                comandInsFact.Connection = cnn;
                comandInsFact.CommandText = "delete from [vwCostosServiciosEjecutados]";
                comandInsFact.ExecuteNonQuery();
                comandInsFact.Parameters.Clear();
                comandInsFact.Connection = cnn;
                foreach (DataRow drClien in ds3.Tables[0].Rows)
                {
                    comandInsFact.Parameters.Add("@ServAsigId", SqlDbType.VarChar, 200).Value = (drClien["ServAsigId"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar, 200).Value = (drClien["RequiereServicioId"].ToString());
                    comandInsFact.Parameters.Add("@ProveedorId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ProveedorId"].ToString());
                    comandInsFact.Parameters.Add("@StatusServAsigNombre", SqlDbType.VarChar, 40).Value = (drClien["StatusServAsigNombre"].ToString());
                    comandInsFact.Parameters.Add("@ServAsigCostoTotal", SqlDbType.Money).Value = Convert.ToDouble(drClien["ServAsigCostoTotal"].ToString());
                    comandInsFact.Parameters.Add("@ConfiguracionCiudadValorSeguro", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ConfiguracionCiudadValorSeguro"].ToString());
                    comandInsFact.Parameters.Add("@ConfiguracionCiudadValorBroker", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ConfiguracionCiudadValorBroker"].ToString());
                    comandInsFact.Parameters.Add("@ServicioPorcentaje", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ServicioPorcentaje"].ToString());
                    comandInsFact.Parameters.Add("@ServicioTarifaMinima", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ServicioTarifaMinima"].ToString());
                    comandInsFact.Parameters.Add("@Comision", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["Comision"].ToString());
                    comandInsFact.Parameters.Add("@GanaciaProveedor", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["GanaciaProveedor"].ToString());
                    comandInsFact.CommandText = "insert into [dbo].[vwCostosServiciosEjecutados] (ServAsigId, RequiereServicioId, ProveedorId, StatusServAsigNombre, ServAsigCostoTotal, ConfiguracionCiudadValorSeguro,ConfiguracionCiudadValorBroker, ServicioPorcentaje, ServicioTarifaMinima, Comision, GanaciaProveedor)values (@ServAsigId, @RequiereServicioId, @ProveedorId, @StatusServAsigNombre, @ServAsigCostoTotal, @ConfiguracionCiudadValorSeguro,@ConfiguracionCiudadValorBroker, @ServicioPorcentaje, @ServicioTarifaMinima, @Comision, @GanaciaProveedor) ";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                DataSet ds4 = new DataSet();
                SqlCommand cmdper = new SqlCommand("select * from [vwPersonas]", conexion);
                da.SelectCommand = cmdper;
                da.Fill(ds4);
                comandInsFact.Connection = cnn;
                comandInsFact.CommandText = "delete from [vwPersonas]";
                comandInsFact.ExecuteNonQuery();
                comandInsFact.Parameters.Clear();
                comandInsFact.Connection = cnn;
                foreach (DataRow drClien in ds4.Tables[0].Rows)
                {
                    comandInsFact.Parameters.Add("@PersonaId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["PersonaId"].ToString());
                    comandInsFact.Parameters.Add("@PersonaNombres", SqlDbType.VarChar, 200).Value = (drClien["PersonaNombres"].ToString());
                    comandInsFact.Parameters.Add("@PersonaApellidos", SqlDbType.VarChar, 200).Value = (drClien["PersonaApellidos"].ToString());
                    comandInsFact.Parameters.Add("@PersonaCorreo", SqlDbType.VarChar, 100).Value = (drClien["PersonaCorreo"].ToString());
                    comandInsFact.Parameters.Add("@PersonaFechaNacimiento", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["PersonaFechaNacimiento"].ToString());
                    comandInsFact.Parameters.Add("@PersonaTelefono", SqlDbType.VarChar, 40).Value = (drClien["PersonaTelefono"].ToString());
                    comandInsFact.Parameters.Add("@PersonaURLFoto", SqlDbType.VarChar, 250).Value = (drClien["PersonaURLFoto"].ToString());
                    comandInsFact.Parameters.Add("@TipoPersonaNombreTipo", SqlDbType.VarChar, 50).Value = (drClien["TipoPersonaNombreTipo"].ToString());
                    comandInsFact.Parameters.Add("@GeneroNombreTipo", SqlDbType.VarChar, 50).Value = (drClien["GeneroNombreTipo"].ToString());
                    comandInsFact.Parameters.Add("@TipoLoginNombreTipo", SqlDbType.VarChar, 50).Value = (drClien["TipoLoginNombreTipo"].ToString());
                    comandInsFact.Parameters.Add("@CiudadNombre", SqlDbType.VarChar, 200).Value = (drClien["CiudadNombre"].ToString());
                    comandInsFact.Parameters.Add("@RegionNombre", SqlDbType.VarChar, 200).Value = (drClien["RegionNombre"].ToString());
                    comandInsFact.Parameters.Add("@PaisNombre", SqlDbType.VarChar, 200).Value = (drClien["PaisNombre"].ToString());
                    comandInsFact.Parameters.Add("@EstadoPersonaNombreTipo", SqlDbType.VarChar, 50).Value = (drClien["EstadoPersonaNombreTipo"].ToString());
                    comandInsFact.Parameters.Add("@PersonaDNI", SqlDbType.VarChar, 40).Value = (drClien["PersonaDNI"].ToString());
                    comandInsFact.Parameters.Add("@TipoDocumentoNombre", SqlDbType.VarChar, 40).Value = (drClien["TipoDocumentoNombre"].ToString());
                    comandInsFact.Parameters.Add("@PersonaCodigoTelefono", SqlDbType.VarChar, 40).Value = (drClien["PersonaCodigoTelefono"].ToString());
                    comandInsFact.Parameters.Add("@PersonaIdioma", SqlDbType.VarChar, 50).Value = (drClien["PersonaIdioma"].ToString());
                    comandInsFact.Parameters.Add("@PersonaFechaRegistro", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["PersonaFechaRegistro"].ToString());
                    comandInsFact.Parameters.Add("@PersonaGeoReal", SqlDbType.VarChar, 50).Value = (drClien["PersonaGeoReal"].ToString());
                    if ((drClien["PersonaCorreoValidado"].ToString()) != "")
                    {
                        comandInsFact.Parameters.Add("@PersonaCorreoValidado", SqlDbType.Bit).Value = Convert.ToBoolean(drClien["PersonaCorreoValidado"].ToString());
                    }
                    else
                    {
                        comandInsFact.Parameters.Add("@PersonaCorreoValidado", SqlDbType.Bit).Value = DBNull.Value;
                    }
                    comandInsFact.CommandText = "insert into [dbo].[vwPersonas] (PersonaId, PersonaNombres, PersonaApellidos, PersonaCorreo, PersonaFechaNacimiento, PersonaTelefono,PersonaURLFoto, TipoPersonaNombreTipo, GeneroNombreTipo, TipoLoginNombreTipo, CiudadNombre, RegionNombre,PaisNombre, EstadoPersonaNombreTipo, PersonaDNI, TipoDocumentoNombre, PersonaCodigoTelefono, PersonaIdioma, PersonaFechaRegistro,PersonaGeoReal, PersonaCorreoValidado, Latitud, Longitud) values (@PersonaId, @PersonaNombres, @PersonaApellidos, @PersonaCorreo, @PersonaFechaNacimiento, @PersonaTelefono,@PersonaURLFoto, @TipoPersonaNombreTipo, @GeneroNombreTipo, @TipoLoginNombreTipo, @CiudadNombre, @RegionNombre,@PaisNombre, @EstadoPersonaNombreTipo, @PersonaDNI, @TipoDocumentoNombre, @PersonaCodigoTelefono, @PersonaIdioma,@PersonaFechaRegistro,@PersonaGeoReal, @PersonaCorreoValidado, null, null) ";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
                DataSet ds5 = new DataSet();
                SqlCommand cmdR = new SqlCommand("select * from [vwReqServicio]", conexion);
                da.SelectCommand = cmdR;
                da.Fill(ds5);
                comandInsFact.Connection = cnn;
                comandInsFact.CommandText = "delete from [vwReqServicio]";
                comandInsFact.ExecuteNonQuery();
                comandInsFact.Parameters.Clear();
                comandInsFact.Connection = cnn;
                foreach (DataRow drClien in ds5.Tables[0].Rows)
                {
                    comandInsFact.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar, 200).Value = (drClien["RequiereServicioId"].ToString());
                    comandInsFact.Parameters.Add("@ClientePersonaId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ClientePersonaId"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioFechaHoraReq", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["RequiereServicioFechaHoraReq"].ToString());
                    comandInsFact.Parameters.Add("@EstadoReqServNombre", SqlDbType.VarChar, 40).Value = (drClien["EstadoReqServNombre"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioFHDeseada", SqlDbType.DateTime).Value = (drClien["RequiereServicioFHDeseada"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioDescripcion", SqlDbType.VarChar, 1000).Value = (drClien["RequiereServicioDescripcion"].ToString());
                    comandInsFact.Parameters.Add("@ServicioId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ServicioId"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioProveedoresId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["RequiereServicioProveedoresId"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioProveedoresAdj", SqlDbType.Bit).Value = Convert.ToBoolean(drClien["RequiereServicioProveedoresAdj"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioProvCotizacion", SqlDbType.Money).Value = Convert.ToDouble(drClien["RequiereServicioProvCotizacion"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioProvFHTrabajo", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["RequiereServicioProvFHTrabajo"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioProvDescipcion", SqlDbType.VarChar, 1000).Value = (drClien["RequiereServicioProvDescipcion"].ToString());
                    comandInsFact.Parameters.Add("@ProveedorPersonaId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ProveedorPersonaId"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioProvFHResp", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["RequiereServicioProvFHResp"].ToString());
                    comandInsFact.Parameters.Add("@StatusRequiereNombre", SqlDbType.VarChar, 40).Value = (drClien["StatusRequiereNombre"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioFechaMod", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["RequiereServicioFechaMod"].ToString());

                    comandInsFact.CommandText = "insert into [dbo].[vwReqServicio] (RequiereServicioId, ClientePersonaId, RequiereServicioFechaHoraReq, EstadoReqServNombre, RequiereServicioFHDeseada, RequiereServicioDescripcion,ServicioId, RequiereServicioProveedoresId, RequiereServicioProveedoresAdj, RequiereServicioProvCotizacion, RequiereServicioProvFHTrabajo, RequiereServicioProvDescipcion,ProveedorPersonaId, RequiereServicioProvFHResp, StatusRequiereNombre, RequiereServicioFechaMod) values (@RequiereServicioId, @ClientePersonaId, @RequiereServicioFechaHoraReq, @EstadoReqServNombre, @RequiereServicioFHDeseada,@RequiereServicioDescripcion,@ServicioId, @RequiereServicioProveedoresId, @RequiereServicioProveedoresAdj, @RequiereServicioProvCotizacion,@RequiereServicioProvFHTrabajo, @RequiereServicioProvDescipcion,@ProveedorPersonaId, @RequiereServicioProvFHResp, @StatusRequiereNombre, @RequiereServicioFechaMod)  ";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                DataSet ds6 = new DataSet();
                SqlCommand cmdsa = new SqlCommand("select * from [vwServAsignado]", conexion);
                da.SelectCommand = cmdsa;
                da.Fill(ds6);
                comandInsFact.Connection = cnn;

                comandInsFact.CommandText = "delete from [vwServAsignado]";
                comandInsFact.ExecuteNonQuery();
                comandInsFact.Parameters.Clear();
                comandInsFact.Connection = cnn;
                foreach (DataRow drClien in ds6.Tables[0].Rows)
                {
                    comandInsFact.Parameters.Add("@ServAsigId", SqlDbType.VarChar, 200).Value = (drClien["ServAsigId"].ToString());
                    comandInsFact.Parameters.Add("@ProveedorId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ProveedorId"].ToString());
                    if ((drClien["ServAsigFHInicio"].ToString()) != "")
                    {
                        comandInsFact.Parameters.Add("@ServAsigFHInicio", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["ServAsigFHInicio"].ToString());
                    }
                    else
                    {
                        comandInsFact.Parameters.Add("@ServAsigFHInicio", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    if ((drClien["ServAsigFHFin"].ToString()) != "")
                    {
                        comandInsFact.Parameters.Add("@ServAsigFHFin", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["ServAsigFHFin"].ToString());
                    }
                    else
                    {
                        comandInsFact.Parameters.Add("@ServAsigFHFin", SqlDbType.DateTime).Value = DBNull.Value;
                    }

                    string fecha = drClien["ServAsigFHPago"].ToString();
                    // DateTime fecja = Convert.ToDateTime(drClien["ServAsigFHPago"].ToString());
                    if ((fecha) != "")
                    {
                        comandInsFact.Parameters.Add("@ServAsigFHPago", SqlDbType.DateTime).Value = Convert.ToDateTime(drClien["ServAsigFHPago"].ToString());
                    }
                    else
                    {
                        comandInsFact.Parameters.Add("@ServAsigFHPago", SqlDbType.DateTime).Value = DBNull.Value;
                    }


                    comandInsFact.Parameters.Add("@ServAsigCostoTotal", SqlDbType.Money).Value = Convert.ToDouble(drClien["ServAsigCostoTotal"].ToString());
                    comandInsFact.Parameters.Add("@StatusServAsigNombre", SqlDbType.VarChar, 40).Value = (drClien["StatusServAsigNombre"].ToString());
                    comandInsFact.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar, 200).Value = (drClien["RequiereServicioId"].ToString());

                    if ((drClien["ServAsigPagaCliente"].ToString()) != "")
                    {
                        comandInsFact.Parameters.Add("@ServAsigPagaCliente", SqlDbType.Bit).Value = Convert.ToBoolean(drClien["ServAsigPagaCliente"].ToString());
                    }
                    else
                    {
                        comandInsFact.Parameters.Add("@ServAsigPagaCliente", SqlDbType.Bit).Value = DBNull.Value;
                    }

                    comandInsFact.CommandText = "insert into [dbo].[vwServAsignado] (ServAsigId, ProveedorId, ServAsigFHInicio, ServAsigFHFin, ServAsigFHPago, ServAsigCostoTotal, StatusServAsigNombre, RequiereServicioId, ServAsigPagaCliente) values (@ServAsigId, @ProveedorId, @ServAsigFHInicio, @ServAsigFHFin, @ServAsigFHPago, @ServAsigCostoTotal, @StatusServAsigNombre, @RequiereServicioId, @ServAsigPagaCliente)  ";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                DataSet ds7 = new DataSet();
                SqlCommand cmdps = new SqlCommand("select * from vwProveedoresPorServicios", conexion);
                da.SelectCommand = cmdps;
                da.Fill(ds7);
                comandInsFact.Connection = cnn;

                comandInsFact.CommandText = "delete from vwProveedoresPorServicios";

                comandInsFact.ExecuteNonQuery();
                comandInsFact.Parameters.Clear();
                comandInsFact.Connection = cnn;
                foreach (DataRow drClien in ds7.Tables[0].Rows)
                {
                    comandInsFact.Parameters.Add("@CategoriaServicioId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["CategoriaServicioId"].ToString());
                    comandInsFact.Parameters.Add("@CategoriaServicioNombre", SqlDbType.VarChar, 40).Value = (drClien["CategoriaServicioNombre"].ToString());
                    if ((drClien["ServicioId"].ToString()) != "")
                    {
                        comandInsFact.Parameters.Add("@ServicioId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["ServicioId"].ToString());
                    }
                    else
                    {
                        comandInsFact.Parameters.Add("@ServicioId", SqlDbType.DateTime).Value = DBNull.Value;
                    }

                    comandInsFact.Parameters.Add("@ServicioNombre", SqlDbType.VarChar, 200).Value = (drClien["ServicioNombre"].ToString());
                    comandInsFact.Parameters.Add("@PersonaId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["PersonaId"].ToString());

                    comandInsFact.Parameters.Add("@PersonaNombres", SqlDbType.VarChar, 200).Value = (drClien["PersonaNombres"].ToString());
                    comandInsFact.Parameters.Add("@PersonaApellidos", SqlDbType.VarChar, 200).Value = (drClien["PersonaApellidos"].ToString());
                    comandInsFact.Parameters.Add("@ServicioPersonaNombre", SqlDbType.VarChar, 200).Value = (drClien["ServicioPersonaNombre"].ToString());
                    comandInsFact.Parameters.Add("@EstadoServicioId", SqlDbType.Decimal).Value = Convert.ToDecimal(drClien["EstadoServicioId"].ToString());
                    comandInsFact.Parameters.Add("@EstadoServicioNombre", SqlDbType.VarChar).Value = (drClien["EstadoServicioNombre"].ToString());



                    comandInsFact.CommandText = "insert into [dbo].[vwProveedoresPorServicios] (CategoriaServicioId, CategoriaServicioNombre, ServicioId, ServicioNombre, PersonaId, PersonaNombres, PersonaApellidos, ServicioPersonaNombre, EstadoServicioId, EstadoServicioNombre) values (@CategoriaServicioId, @CategoriaServicioNombre, @ServicioId, @ServicioNombre, @PersonaId, @PersonaNombres, @PersonaApellidos, @ServicioPersonaNombre, @EstadoServicioId, @EstadoServicioNombre) ";
                    comandInsFact.CommandTimeout = 0;
                    comandInsFact.ExecuteNonQuery();
                    comandInsFact.Parameters.Clear();
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                InsertarLogNotificacion("CargarTablasServiceWeb_Verdadero", DateTime.Now, "FINALIZACION EXITOSA", 0);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cnn.Close();
                conexion.Close();
            }



        }
        public static async Task EnviarNotificacionesNuevoRequerimiento()
        {
            List<BE.RequiereServicio> lstRequiereServicio = bcRequiereServicio.ListadoRequerimientos_NuevoRequerimientos(null);

            foreach (var requiereServicio in lstRequiereServicio)
            {
                BE.Persona Persona = bcPersona.BuscarPersonaxId(requiereServicio.PersonaId);

                await EnviarNotificacionesAsyncClienteFinal_NuevosRequerimientos(Persona, "ClienteReque", Persona.PersonaIdioma, requiereServicio);
                List<BE.Persona> lstPersonas = bcPersona.ListadoTokenProveedores(requiereServicio.RequiereServicioId, 1);
                await EnviarNotificacionesAsyncV3Final_NuevosRequerimientos(lstPersonas, "ProveedorV2", "es", requiereServicio, "");
                //await EnviarNotificacionesAsyncV3(lstPersonas, "ProveedorV2", lang, requiereServicio, requiereServicio.persona.NombreCompleto(), null);
                await OfertaFirebase(requiereServicio.RequiereServicioId, Convert.ToInt32(requiereServicio.PersonaId), Convert.ToString(Convert.ToInt32(requiereServicio.EstadoReqServId)), "recibido", "Requerimientos");

            }


        }
        public static async Task CambiarEstadoDesierto_Mas_de_dosHoras_sin_Tomar()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            try
            {

                if (cnn.State != ConnectionState.Open) cnn.Open();

                cmd.Connection = cnn;
                DateTime fecha_Bolivia = ObtenerHoraBoliviana();


                //  cmd.CommandText = "select rs.requiereServicioId,p.personaId,p.personaTokenId,s.servicioNombre,per.PersonaNombres+per.PersonaApellidos as NombreProveedor, convert(varchar,rs.RequiereServicioFHDeseada,108) as Hora from requiereServicio	rs inner join[dbo].[ServAsig]  sa on rs.requiereServicioId=sa.requiereServicioId inner join servicio s on rs.servicioId=s.servicioId inner join Persona p on rs.personaId=p.personaId inner join Persona per on sa.proveedorId=per.PersonaId AND year(rs.RequiereServicioFHDeseada)=year(getdate()) AND month(rs.RequiereServicioFHDeseada)=month(getdate()) AND day(rs.RequiereServicioFHDeseada)=day(getdate())";
                cmd.CommandText = "select * from requiereservicio where Convert(DATE, RequiereServicioFHDeseada)<=Convert(DATE, (@fecha))  and EstadoReqServId = 1";
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha_Bolivia;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow myR in ds.Tables[0].Rows)
                {
                    DateTime fecha = Convert.ToDateTime(myR["RequiereServicioFHDeseada"].ToString());
                    fecha = fecha.AddHours(2);
                    if (fecha <= fecha_Bolivia)
                    {
                        /*cmd.CommandText = "update RequiereServicio set EstadoReqServId=4,RequiereServicioFechaMod=getdate() where  RequiereServicioId=@RequiereServicioid";
                        cmd.Parameters.Add("@RequiereServicioid", SqlDbType.VarChar).Value = myR["requiereServicioId"].ToString();
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS_pasada_2Horas", DateTime.Now, myR["requiereServicioId"].ToString(), 0);*/

                    }
                    BE.Persona persona = new BE.Persona();
                    BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
                    persona = bcPersona.BuscarPersonaxId(Convert.ToDecimal(myR["PersonaId"].ToString()));
                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId((myR["RequiereServicioId"].ToString()), "es");


                    await EnviarNotificacionesAsyncClienteFinal(persona, "DesiertoRechazado", "es", requiereServicio, 0, 0, null);

                }


            }
            catch (Exception ex)
            {

                InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS_pasada_2Horas", DateTime.Now, ex.Message, 0);
            }
            finally
            {
                cnn.Close();
            }

        }
        public static void CambiarEstadoDesierto_Requerimientos_Cancelados_por_proveedor()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            try
            {

                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                DateTime fecha_Bolivia = ObtenerHoraBoliviana();


                //  cmd.CommandText = "select rs.requiereServicioId,p.personaId,p.personaTokenId,s.servicioNombre,per.PersonaNombres+per.PersonaApellidos as NombreProveedor, convert(varchar,rs.RequiereServicioFHDeseada,108) as Hora from requiereServicio	rs inner join[dbo].[ServAsig]  sa on rs.requiereServicioId=sa.requiereServicioId inner join servicio s on rs.servicioId=s.servicioId inner join Persona p on rs.personaId=p.personaId inner join Persona per on sa.proveedorId=per.PersonaId AND year(rs.RequiereServicioFHDeseada)=year(getdate()) AND month(rs.RequiereServicioFHDeseada)=month(getdate()) AND day(rs.RequiereServicioFHDeseada)=day(getdate())";
                cmd.CommandText = "select * from requiereservicio where Convert(DATE, RequiereServicioFHDeseada)<=Convert(DATE, (@fecha))  and EstadoReqServId = 1";
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha_Bolivia;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow myR in ds.Tables[0].Rows)
                {
                    DateTime fecha = Convert.ToDateTime(myR["RequiereServicioFHDeseada"].ToString());
                    fecha = fecha.AddHours(2);
                    if (fecha <= fecha_Bolivia)
                    {
                        cmd.CommandText = "update RequiereServicio set EstadoReqServId=4,RequiereServicioFechaMod=getdate() where  RequiereServicioId=@RequiereServicioid";
                        cmd.Parameters.Add("@RequiereServicioid", SqlDbType.VarChar).Value = myR["requiereServicioId"].ToString();
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS_cancelados", DateTime.Now, myR["requiereServicioId"].ToString(), 0);

                    }

                }


            }
            catch (Exception ex)
            {

                InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS_cancelado", DateTime.Now, ex.Message, 0);
            }
            finally
            {
                cnn.Close();
            }

        }












        /*
                public static void listar_cantidad_proveedores_x_servicio() {
                    BE.Persona PersonaProveedor1 = bcPersona.BuscarPersonaxId(1469, "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                    try
                    {
                        CantidadProveedores(PersonaProveedor1);
                        InsertarLogNotificacion("listar_cantidad_proveedores_x_servicio", DateTime.Now, "", 0);
                    }
                    catch (Exception ex)
                    {
                        InsertarLogNotificacion("listar_cantidad_proveedores_x_servicio", DateTime.Now, ex.Message, 0);
                    }

                }
        */




        public static async Task CantidadProveedores(BE.Persona persona)
        {
            SqlCommand cmd = new SqlCommand();//modificado       
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            decimal proveedorid = persona.PersonaId;
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                DateTime fecha_Bolivia = ObtenerHoraBoliviana();
                cmd.CommandText = "select s.ServicioId, s.ServicioNombre from servicio s inner join serviciopersona sp on sp.servicioid=s.servicioid where sp.personaid=@personaid  and  s.servicioid>=500 group by s.ServicioId, s.ServicioNombre";
                cmd.Parameters.Add("@personaid", SqlDbType.VarChar).Value = proveedorid;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow myR in ds.Tables[0].Rows)
                {
                    Decimal servicioid = Convert.ToDecimal(myR["ServicioId"].ToString());
                    string valor = servicioid.ToString();
                    InsertarLogNotificacion("servicioidv", DateTime.Now, valor, proveedorid);
                    Cantidadpv(servicioid);
                }

                string bady = valorr;

                List<BE.Persona> lstPersonas = bcPersona.ListadoOperaciones();

                await EnviarNotificacionesOperacionesCP("CPNotificacionProveedores", lstPersonas, bady);
            }
            catch (Exception ex)
            {
                InsertarLogNotificacion("CantidadProveedores", DateTime.Now, ex.Message, 0);
            }

        }




        public static void Cantidadpv(decimal servicioid)
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = "select count(sp.personaid) as cantidadP , s.servicionombre  from serviciopersona sp inner join servicio s on s.servicioid=sp.servicioid where sp.servicioid=@servicioid and sp.estadoservicioid=1 and sp.statusservicioid=1 group by s.servicionombre";
                cmd.Parameters.Add("@servicioid", SqlDbType.VarChar).Value = servicioid;
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow myR in ds.Tables[0].Rows)
                {
                    int cantidadp = Convert.ToInt32(myR["cantidadP"].ToString());
                    string nombres = (myR["servicionombre"].ToString());
                    if (cantidadp == 1)
                    {
                        string mensaje = "* " + cantidadp.ToString() + " " + "Proveedor del servicio de: " + nombres;
                        InsertarLogNotificacion("Cantidadpv", DateTime.Now, mensaje, servicioid);
                        valorr = mensaje + "\n" + valorr;
                        //  valorr = mensaje + " " + valorr;
                    }
                    else
                    {

                        string mensaje = "* " + cantidadp.ToString() + " " + "Proveedores del servicio de: " + nombres;
                        InsertarLogNotificacion("Cantidadpv", DateTime.Now, mensaje, servicioid);
                        valorr = mensaje + "\n" + valorr;
                        //valorr = mensaje + " " + valorr;
                    }





                }
            }
            catch (Exception ex)
            {
                InsertarLogNotificacion("Cantidadpv", DateTime.Now, ex.Message, 0);
            }
        }



        private static async Task EnviarNotificacionesOperacionesCP(string tipo, List<BE.Persona> lstPersonas, string contenido)
        {

            foreach (var item in lstPersonas)
            {
                DataRow data = bcPersona.ListadoDatosNotificacionv2(tipo, "es");

                var title = data["title"].ToString();
                //var body = data["body"].ToString();
                var body = contenido;
                var vista = data["Fragment"].ToString();
                var BotonTexto = data["BotonTexto"].ToString();
                var jsonBody2 = JsonConvert.SerializeObject(body);
                bool Invasivo = false;
                string i = "";
                string foto = "";
                string des = "";

                string ReqId = "";
                string action1 = "inf";
                string versionTelefono = "";
                int ContadorBadge = 0;
                BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
                switch (tipo)
                {
                    case "CPNotificacionProveedores":
                        body = String.Format(body, item);
                        Invasivo = false;
                        notificacionPersona.ConceptoNotificacionId = 16;
                        break;
                }

                var to = item.PersonaTokenId;
                var senderId2 = string.Format("id={0}", SenderIdFB);
                var key = string.Format("key={0}", ServerKey);
                if (Invasivo == false)
                {

                    i = "no";
                }
                else
                {
                    i = "si";
                }
                var sound = "default";
                var invasivo = i;
                var reqservid = ReqId;
                var reqservdes = des;///esta campo va a body en el invasivo 

                var servicioNombre = body;
                var ServicioUrlFoto = foto;
                var action = action1;
                var tag = "no";
                var priority = "high";
                ////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
                    BasePath = "https://service-web-258723.firebaseio.com/"
                };

                IFirebaseClient client;
                client = new FireSharp.FirebaseClient(config);
                if (client != null)
                {
                    FirebaseResponse response = await client.GetTaskAsync("Notificacion/" + Convert.ToInt32(item.PersonaId));
                    var data1 = new Data
                    {
                        PersonaId = "",
                        Post = 0,
                        Solicitudes = 0,
                        Servicios = 0,
                        Rendimientos = 0,
                        Notificaciones = 0,
                    };

                    if (response.Body != "null")
                    {
                        Data obj = response.ResultAs<Data>();
                        data1 = obj;
                        ContadorBadge = Convert.ToInt32(obj.Notificaciones) + 1;
                    }
                }

                var badge = ContadorBadge;

                if (invasivo == "no")
                {
                    ////////////////////////////////////////////////////

                    if (versionTelefono.Contains("IOS"))
                    {
                        var data2 = new { to, notification = new { body, title, badge, sound }, priority };
                        jsonBody2 = JsonConvert.SerializeObject(data2);

                    }
                    else
                    {

                        var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                        jsonBody2 = JsonConvert.SerializeObject(data2);

                    }
                }




                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                    // httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                    httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + "  " + contenido, DateTime.Now, to, item.PersonaId);

                        }
                        else
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId);

                        }
                    }
                }



                notificacionPersona.RequiereServicioId = ReqId;
                notificacionPersona.PersonaId = item.PersonaId;
                notificacionPersona.TipoEstadoId = 1;
                notificacionPersona.NotificacionPersonaTitulo = title;

                notificacionPersona.NotificacionPersonaDescripcion = body;
                notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
                notificacionPersona.NotificacionPersonaFragment = vista;
                notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
                notificacionPersona.EstadoNotificacionId = 1;
                notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;



                if (notificacionPersona != null)
                {
                    bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
                }
                /////////////////

                /////////////////////////////
                await NotificacionFirebase(Convert.ToInt32(item.PersonaId), "recibido", "Notificaciones");




            }

        }





        private static async Task EnviarNotificacionesAsyncClienteFinal(BE.Persona persona, string tipo, string lang, BE.RequiereServicio requiereServicio = null, decimal Importe = 0, decimal Calificacion = 0, string SiniestroId = null)
        {

            DataRow data = bcPersona.ListadoDatosNotificacionv2(tipo, lang);
            var title = data["title"].ToString();
            var body = data["body"].ToString();
            var vista = data["Fragment"].ToString();
            var BotonTexto = data["BotonTexto"].ToString();
            var jsonBody2 = JsonConvert.SerializeObject(body);
            bool Invasivo = false;
            string i = "";
            string foto = "";
            string des = "";
            string ReqId = "";
            string[] titulo;
            string[] cuerpo;
            string id = "";
            string action1 = "";
            string versionTelefono = "";
            int ContadorBadge = 0;
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();

            switch (tipo)
            {

                case "ClienteReque":
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 1;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteInicioServicio":
                    body = body + " " + requiereServicio.servicio.ServicioNombre;
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 7;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteFinServicio":
                    titulo = body.Split('/');
                    body = "";
                    body = titulo[0] + string.Format("{0:N}", Convert.ToInt32(Importe)) + " " + Convert.ToString(persona.Ciudad.Region.pais.Moneda.MonedaNombre) + titulo[1];
                    Invasivo = true;
                    foto = requiereServicio.servicio.ServicioURLFoto;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 8;
                    title = title + " " + "RQ - " + ReqId;
                    break;
                case "ProveedorFinServicioFinal":
                    titulo = body.Split('/');
                    body = "";
                    title = "";
                    title = titulo[0] + string.Format("{0:N}", Convert.ToInt32(Importe)) + " " + persona.Ciudad.Region.pais.Moneda.MonedaNombre;
                    body = " " + titulo[3];
                    Invasivo = true;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 6;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ProveedorFinServicioFinalNotificacion":
                    titulo = body.Split('/');
                    body = "";
                    title = "";
                    title = "";
                    body = titulo[1] + " " + Convert.ToString(Convert.ToInt32(Calificacion)) + " " + titulo[2];
                    Invasivo = true;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 6;
                    title = title + " " + "RQ: " + ReqId;
                    break;

                case "ClienteCotizacion":
                    cuerpo = body.Split('/');
                    body = "";
                    body = cuerpo[0];
                    notificacionPersona.ConceptoNotificacionId = 4;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "AproSiniestro":
                    Invasivo = true;
                    des = BotonTexto;
                    notificacionPersona.ConceptoNotificacionId = 10;
                    ReqId = SiniestroId;
                    title = title + " " + "S: " + ReqId;
                    break;
                case "RegSiniestro":
                    notificacionPersona.ConceptoNotificacionId = 10;
                    ReqId = SiniestroId;
                    title = title + " " + "S: " + ReqId;
                    break;
                case "ProveedorConfV2":
                    Invasivo = true;
                    action1 = "inf";
                    notificacionPersona.ConceptoNotificacionId = 5;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;
                case "ClienteConfV2":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 5;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ProveedorCanV2":
                    Invasivo = true;
                    action1 = "inf";
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;
                case "ProveeorPerdioAdj":
                    Invasivo = true;
                    action1 = "inf";
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    ReqId = requiereServicio.RequiereServicioId;
                    break;
                case "ClienteCBA":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 11;
                    ReqId = SiniestroId;
                    break;

                case "DesiertoSinTomar":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 12;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;

                case "DesiertoRechazado":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 12;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;

                case "ProveedorIniciarServicio":
                    Invasivo = true;
                    notificacionPersona.ConceptoNotificacionId = 13;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    InsertarLogNotificacion(requiereServicio.RequiereServicioId, DateTime.Now, "ProveedorIniciarServicio", persona.PersonaId);
                    des = BotonTexto;
                    break;

                case "ProveedorFinServicio":
                    Invasivo = true;
                    notificacionPersona.ConceptoNotificacionId = 14;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    InsertarLogNotificacion(requiereServicio.RequiereServicioId, DateTime.Now, "ProveedorFinServicio", persona.PersonaId);
                    des = BotonTexto;
                    action1 = "pd";
                    ReqId = requiereServicio.RequiereServicioId;
                    break;
                case "ProveedorPagarComisión":
                    Invasivo = true;
                    notificacionPersona.ConceptoNotificacionId = 16;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    action1 = "inf";


                    ReqId = requiereServicio.RequiereServicioId;
                    break;



                case "ProveedorBloqueado":
                    Invasivo = true;

                    notificacionPersona.ConceptoNotificacionId = 16;
                    action1 = "inf";


                    ReqId = requiereServicio.RequiereServicioId;
                    break;



            }

            var to = persona.PersonaTokenId;
            var senderId2 = string.Format("id={0}", SenderIdFB);
            var key = string.Format("key={0}", ServerKey);
            if (Invasivo == false)
            {

                i = "no";
            }
            else
            {
                i = "si";
            }
            var sound = "default";
            var invasivo = i;
            var reqservid = ReqId;
            var reqservdes = des;///esta campo va a body en el invasivo 
            var servicioNombre = body;
            var ServicioUrlFoto = foto;
            var action = action1;
            var tag = "no";
            var priority = "high";
            ////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
                BasePath = "https://service-web-258723.firebaseio.com/"
            };
            IFirebaseClient client;
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                FirebaseResponse response = await client.GetTaskAsync("Notificacion/" + Convert.ToInt32(persona.PersonaId));
                var data1 = new Data
                {
                    PersonaId = "",
                    Post = 0,
                    Solicitudes = 0,
                    Servicios = 0,
                    Rendimientos = 0,
                    Notificaciones = 0,
                };

                if (response.Body != "null")
                {
                    Data obj = response.ResultAs<Data>();
                    data1 = obj;
                    ContadorBadge = Convert.ToInt32(obj.Notificaciones) + 1;
                }
            }
            var badge = ContadorBadge;
            versionTelefono = bclogSesionesPersona.VersionTelefono(persona.PersonaId);

            if (invasivo == "si")
            {
                ////////////////////////////////////////////////////

                tag = "si";
                if (versionTelefono.Contains("IOS"))
                {
                    tag = i;
                    var data2 = new { to, notification = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag, badge, sound }, priority };
                    jsonBody2 = JsonConvert.SerializeObject(data2);
                }
                else
                {
                    var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);

                }
            }
            else
            {
                if (versionTelefono.Contains("IOS"))
                {
                    var data2 = new { to, notification = new { body, title, badge, sound }, priority };
                    jsonBody2 = JsonConvert.SerializeObject(data2);
                    InsertarLogNotificacion("badge", DateTime.Now, Convert.ToString(badge), persona.PersonaId);

                }
                else
                {
                    //   var data2 = new { to, notification = new { sound, title, body } };
                    // jsonBody2 = JsonConvert.SerializeObject(data2);
                    var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);

                    /*   var data2 = new { to, data= new { priority,title,body,invasivo,vista,reqservid,action,ServicioUrlFoto,servicioNombre,reqservdes } };
                       jsonBody2 = JsonConvert.SerializeObject(data2);*/

                }

            }


            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
            {
                httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                // httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.SendAsync(httpRequest);

                    if (result.IsSuccessStatusCode)
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, persona.PersonaId);

                    }
                    else
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, persona.PersonaId);

                    }
                }
            }





            /////////////////////////NOTIFICACION FIREBASE
            notificacionPersona.RequiereServicioId = ReqId;
            notificacionPersona.PersonaId = persona.PersonaId;
            notificacionPersona.TipoEstadoId = 1;
            notificacionPersona.NotificacionPersonaTitulo = title;
            notificacionPersona.NotificacionPersonaDescripcion = body;
            notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
            notificacionPersona.NotificacionPersonaFragment = vista;
            notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
            notificacionPersona.EstadoNotificacionId = 1;
            notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;

            if (notificacionPersona != null)
            {
                bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
            }
            /////////////////

            /////////////////////////////
            await NotificacionFirebase(Convert.ToInt32(persona.PersonaId), "recibido", "Notificaciones");



        }
        private static async Task EnviarNotificacionesAsyncClienteFinal_NuevosRequerimientos(BE.Persona persona, string tipo, string lang, BE.RequiereServicio requiereServicio = null, decimal Importe = 0, decimal Calificacion = 0, string SiniestroId = null)
        {

            DataRow data = bcPersona.ListadoDatosNotificacionv2(tipo, lang);
            var title = data["title"].ToString();
            var body = data["body"].ToString();
            var vista = data["Fragment"].ToString();
            var BotonTexto = data["BotonTexto"].ToString();
            var jsonBody2 = JsonConvert.SerializeObject(body);
            bool Invasivo = false;
            string i = "";
            string foto = "";
            string des = "";
            string ReqId = "";
            string[] titulo;
            string[] cuerpo;
            string id = "";
            string action1 = "";
            string versionTelefono = "";
            int ContadorBadge = 0;
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();

            switch (tipo)
            {

                case "ClienteReque":
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 1;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteInicioServicio":
                    body = body + " " + requiereServicio.servicio.ServicioNombre;
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 7;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteFinServicio":
                    titulo = body.Split('/');
                    body = "";
                    body = titulo[0] + string.Format("{0:N}", Convert.ToInt32(Importe)) + " " + Convert.ToString(persona.Ciudad.Region.pais.Moneda.MonedaNombre) + titulo[1];
                    Invasivo = true;
                    foto = requiereServicio.servicio.ServicioURLFoto;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 8;
                    title = title + " " + "RQ - " + ReqId;
                    break;
                case "ProveedorFinServicioFinal":
                    titulo = body.Split('/');
                    body = "";
                    title = "";
                    title = titulo[0] + string.Format("{0:N}", Convert.ToInt32(Importe)) + " " + persona.Ciudad.Region.pais.Moneda.MonedaNombre;
                    body = " " + titulo[3];
                    Invasivo = true;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 6;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ProveedorFinServicioFinalNotificacion":
                    titulo = body.Split('/');
                    body = "";
                    title = "";
                    title = "";
                    body = titulo[1] + " " + Convert.ToString(Convert.ToInt32(Calificacion)) + " " + titulo[2];
                    Invasivo = true;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 6;
                    title = title + " " + "RQ: " + ReqId;
                    break;

                case "ClienteCotizacion":
                    cuerpo = body.Split('/');
                    body = "";
                    body = cuerpo[0];
                    notificacionPersona.ConceptoNotificacionId = 4;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "AproSiniestro":
                    Invasivo = true;
                    des = BotonTexto;
                    notificacionPersona.ConceptoNotificacionId = 10;
                    ReqId = SiniestroId;
                    title = title + " " + "S: " + ReqId;
                    break;
                case "RegSiniestro":
                    notificacionPersona.ConceptoNotificacionId = 10;
                    ReqId = SiniestroId;
                    title = title + " " + "S: " + ReqId;
                    break;
                case "ProveedorConfV2":
                    Invasivo = true;
                    action1 = "inf";
                    notificacionPersona.ConceptoNotificacionId = 5;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;
                case "ClienteConfV2":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 5;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ProveedorCanV2":
                    Invasivo = true;
                    action1 = "inf";
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;
                case "ProveeorPerdioAdj":
                    Invasivo = true;
                    action1 = "inf";
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    ReqId = requiereServicio.RequiereServicioId;
                    break;
                case "ClienteCBA":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 11;
                    ReqId = SiniestroId;
                    break;

                case "DesiertoSinTomar":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 12;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;

                case "DesiertoRechazado":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 12;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;

            }

            var to = persona.PersonaTokenId;
            var senderId2 = string.Format("id={0}", SenderIdFB);
            var key = string.Format("key={0}", ServerKey);
            if (Invasivo == false)
            {

                i = "no";
            }
            else
            {
                i = "si";
            }
            var sound = "default";
            var invasivo = i;
            var reqservid = ReqId;
            var reqservdes = des;///esta campo va a body en el invasivo 
            var servicioNombre = body;
            var ServicioUrlFoto = foto;
            var action = action1;
            var tag = "no";
            var priority = "high";
            ////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
                BasePath = "https://service-web-258723.firebaseio.com/"
            };
            IFirebaseClient client;
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                FirebaseResponse response = await client.GetTaskAsync("Notificacion/" + Convert.ToInt32(persona.PersonaId));
                var data1 = new Data
                {
                    PersonaId = "",
                    Post = 0,
                    Solicitudes = 0,
                    Servicios = 0,
                    Rendimientos = 0,
                    Notificaciones = 0,
                };

                if (response.Body != "null")
                {
                    Data obj = response.ResultAs<Data>();
                    data1 = obj;
                    ContadorBadge = Convert.ToInt32(obj.Notificaciones) + 1;
                }
            }
            var badge = ContadorBadge;
            versionTelefono = bclogSesionesPersona.VersionTelefono(persona.PersonaId);

            if (invasivo == "si")
            {
                ////////////////////////////////////////////////////

                tag = "si";
                if (versionTelefono.Contains("IOS"))
                {
                    tag = i;
                    var data2 = new { to, notification = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag, badge, sound }, priority };
                    jsonBody2 = JsonConvert.SerializeObject(data2);
                    /* var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                     jsonBody2 = JsonConvert.SerializeObject(data2);/
                    /*  var data2 = new { to, notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }, priority };
                   jsonBody2 = JsonConvert.SerializeObject(data2);*/

                }
                else
                {
                    /*var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);*/


                    var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);

                }
                /////////////////////////////////////////////////////////////

                /* var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                   jsonBody2 = JsonConvert.SerializeObject(data2);*/


            }
            else
            {
                if (versionTelefono.Contains("IOS"))
                {
                    var data2 = new { to, notification = new { body, title, badge, sound }, priority };
                    jsonBody2 = JsonConvert.SerializeObject(data2);
                    InsertarLogNotificacion("badge", DateTime.Now, Convert.ToString(badge), persona.PersonaId);

                }
                else
                {
                    //   var data2 = new { to, notification = new { sound, title, body } };
                    // jsonBody2 = JsonConvert.SerializeObject(data2);
                    var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);

                    /*   var data2 = new { to, data= new { priority,title,body,invasivo,vista,reqservid,action,ServicioUrlFoto,servicioNombre,reqservdes } };
                       jsonBody2 = JsonConvert.SerializeObject(data2);*/

                }

            }


            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
            {
                httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                // httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.SendAsync(httpRequest);

                    if (result.IsSuccessStatusCode)
                    {
                        InsertarLogNotificacion(requiereServicio.RequiereServicioId, DateTime.Now, to, persona.PersonaId);

                    }
                    else
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, persona.PersonaId);

                    }
                }
            }





            /////////////////////////NOTIFICACION FIREBASE
            notificacionPersona.RequiereServicioId = ReqId;
            notificacionPersona.PersonaId = persona.PersonaId;
            notificacionPersona.TipoEstadoId = 1;
            notificacionPersona.NotificacionPersonaTitulo = title;
            notificacionPersona.NotificacionPersonaDescripcion = body;
            notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
            notificacionPersona.NotificacionPersonaFragment = vista;
            notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
            notificacionPersona.EstadoNotificacionId = 1;
            notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;

            if (notificacionPersona != null)
            {
                bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
            }
            /////////////////

            /////////////////////////////
            await NotificacionFirebase(Convert.ToInt32(persona.PersonaId), "recibido", "Notificaciones");



        }
        private static async Task EnviarNotificacionesAsyncV3Final(List<BE.Persona> lstPersonas, string tipo, string lang, BE.RequiereServicio requiereServicio = null, string persona = "", string RequiereServicioId = null, string ConversacionContenido = "")
        {
            DataRow data = bcPersona.ListadoDatosNotificacionv2(tipo, lang);
            var titleI = data["title"].ToString(); var bodyI = data["body"].ToString(); var vista = data["Fragment"].ToString(); var jsonBody2 = JsonConvert.SerializeObject(bodyI);
            var BotonTexto = data["BotonTexto"].ToString(); string versionTelefono = "";
            bool b = false; string detalledes = ""; string inv = ""; string[] titulo; int importe = 0;
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            int ContadorBadge = 0;
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
                BasePath = "https://service-web-258723.firebaseio.com/"
            };
            IFirebaseClient client;
            switch (tipo)
            {
                case "Proveedor"://REQUIERE SERVICIO

                    detalledes = Convert.ToString(BotonTexto);
                    inv = "si";
                    break;
                case "CotizacionProveedor":


                    break;
                case "Adjudicado":

                    detalledes = BotonTexto;
                    inv = "si";
                    break;
                case "ClienteFinServicioFinal":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 2;
                    break;
                case "Pagado":

                    break;
                case "Conversacion":
                    inv = "no";
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 9;
                    break;
                case "ClienteCotizacion":

                    break;

                case "ConfSiniestro":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 10;

                    notificacionPersona.RequiereServicioId = RequiereServicioId;

                    break;
                case "RechSiniestro":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 10;
                    notificacionPersona.RequiereServicioId = RequiereServicioId;

                    break;
                case "ProveedorV2":

                    inv = "si";
                    break;

            }

            foreach (var item in lstPersonas)
            {
                DataRow dataIdioma = bcPersona.ListadoDatosNotificacionv2(tipo, item.PersonaIdioma);
                var title = dataIdioma["title"].ToString(); var body = String.Format(dataIdioma["body"].ToString(), persona); BotonTexto = dataIdioma["BotonTexto"].ToString();
                if (tipo == "Conversacion")
                {
                    body = ConversacionContenido;
                }
                Respuesta resp = new Respuesta();
                if (requiereServicio != null)
                {
                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId(requiereServicio.RequiereServicioId, item.PersonaIdioma, BE.relRequiereServicio.servicio, BE.relServicio.servicioDetalle, BE.relServicio.categoriaServicio, BE.relCategoriaServicio.Ciudad, BE.relciudad.configuracionCiudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda, BE.relServicio.servicioRequerimiento
                    , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioProveedores, BE.relReqServProv.servicioPersona, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere, BE.relRequiereServicio.servAsig, BE.relServAsig.statusServAsig, BE.relServAsig.post, BE.relServAsig.servAsigCosto, BE.relRequiereServicio.requiereServicioDetalle);

                    if (b == false)
                    {
                        title = title + " " + "RQ - " + Convert.ToString(requiereServicio.RequiereServicioId);
                        b = true;
                    }
                    notificacionPersona.RequiereServicioId = requiereServicio.RequiereServicioId;
                }
                if (tipo == "Proveedor")
                {
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 2;
                }
                if (tipo == "CotizacionProveedor")
                {
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 3;

                }
                if (tipo == "Adjudicado")
                {
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 5;
                }
                if (tipo == "ProveedorV2")
                {
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 2;
                }
                if ((tipo != "CotizacionProveedor"))
                {
                    var to = item.PersonaTokenId;
                    var senderId2 = string.Format("id={0}", SenderIdFB);
                    var key = string.Format("key={0}", ServerKey);
                    var sound = "default";
                    var invasivo = inv;
                    var action = "";
                    string id = "";
                    string sn = "";
                    string sf = "";
                    if (requiereServicio != null)
                    {
                        id = requiereServicio.RequiereServicioId;
                        sn = requiereServicio.servicio.ServicioNombre;
                        sf = requiereServicio.servicio.ServicioURLFoto;
                    }

                    if (tipo == "ProveedorV2")
                    {
                        if (requiereServicio.requiereServicioOtros == true)
                        {
                            requiereServicio.servicio.servicioDetalleTipo = true;
                        }

                        importe = bcRequiereServicio.ImporteRequiereServicio(requiereServicio.RequiereServicioId,
                        requiereServicio.servicio.servicioDetalleTipo);
                        titulo = dataIdioma["body"].ToString().Split('/');
                        sn = titulo[0] + requiereServicio.servicio.ServicioNombre + titulo[1] +
                        requiereServicio.personaDireccion.PersonaDireccionDescripcion + titulo[2]
                        + requiereServicio.RequiereServicioFHDeseada.ToString("dd/MM/yyyy",
                        CultureInfo.InvariantCulture) + titulo[3] +
                        requiereServicio.RequiereServicioFHDeseada.ToString("hh:mm:ss", CultureInfo.InvariantCulture)
                        + titulo[4] + string.Format("{0:N}", importe) + " " +
                        requiereServicio.servicio.categoriaServicio.ciudad.Region.pais.Moneda.MonedaNombre;
                        action = "as";
                        body = sn;
                    }
                    var reqservid = id;
                    var servicioNombre = sn;
                    var ServicioUrlFoto = sf;
                    var reqservdes = BotonTexto;
                    var priority = "high";
                    //////////////////////////////////////////////////////////////////
                    client = new FireSharp.FirebaseClient(config);
                    if (client != null)
                    {


                        FirebaseResponse response = await client.GetTaskAsync("Notificacion/" + Convert.ToInt32(item.PersonaId));
                        var data1 = new Data
                        {
                            PersonaId = "",
                            Post = 0,
                            Solicitudes = 0,
                            Servicios = 0,
                            Rendimientos = 0,
                            Notificaciones = 0,

                        };

                        if (response.Body != "null")
                        {

                            Data obj = response.ResultAs<Data>();
                            data1 = obj;
                            ContadorBadge = Convert.ToInt32(obj.Notificaciones) + 1;
                        }
                    }
                    var badge = ContadorBadge;
                    //  var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                    versionTelefono = bclogSesionesPersona.VersionTelefono(item.PersonaId);
                    var tag = "no";
                    if (inv == "si")
                    {
                        tag = "si";
                        if (versionTelefono.Contains("IOS"))
                        {
                            /* var data2 = new { to, notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag },priority };
                             jsonBody2 = JsonConvert.SerializeObject(data2);*/
                            /*  var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                                jsonBody2 = JsonConvert.SerializeObject(data2);*/
                            tag = inv;
                            var data2 = new { to, notification = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag, badge, sound }, priority };
                            jsonBody2 = JsonConvert.SerializeObject(data2);

                        }
                        else
                        {
                            /*  var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action,tag } };
                              jsonBody2 = JsonConvert.SerializeObject(data2);*/
                            var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);

                        }

                    }
                    else
                    {
                        if (versionTelefono.Contains("IOS"))
                        {
                            var data2 = new { to, notification = new { title, body, badge, sound }, priority };
                            jsonBody2 = JsonConvert.SerializeObject(data2);
                            InsertarLogNotificacion("badge", DateTime.Now, Convert.ToString(badge), item.PersonaId);
                        }
                        else
                        {
                            //   var data2 = new { to, notification = new { sound, title, body } };
                            // jsonBody2 = JsonConvert.SerializeObject(data2);
                            /*var data2 = new { to, notification = new { sound,title, body } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);*/
                            var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);

                        }


                    }

                    System.Diagnostics.Debug.WriteLine(jsonBody2);
                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                        httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                        httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");
                        using (var httpClient = new HttpClient())
                        {
                            var result = await httpClient.SendAsync(httpRequest);

                            if (result.IsSuccessStatusCode)
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, item.PersonaId);
                            }
                            else
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId);
                            }
                        }
                    }
                }
                notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;
                notificacionPersona.NotificacionPersonaTitulo = title;
                notificacionPersona.TipoEstadoId = 1;
                notificacionPersona.NotificacionPersonaDescripcion = body;
                notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
                notificacionPersona.NotificacionPersonaFragment = vista;
                notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
                notificacionPersona.PersonaId = item.PersonaId;
                if (notificacionPersona != null)
                {
                    bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
                    await NotificacionFirebase(Convert.ToInt32(item.PersonaId), "recibido", "Notificaciones");
                }

            }
        }

        private static async Task EnviarNotificacionesAsyncV3Final_NuevosRequerimientos(List<BE.Persona> lstPersonas, string tipo, string lang, BE.RequiereServicio requiereServicio = null, string persona = "", string RequiereServicioId = null, string ConversacionContenido = "")
        {
            DataRow data = bcPersona.ListadoDatosNotificacionv2(tipo, lang);
            var titleI = data["title"].ToString(); var bodyI = data["body"].ToString(); var vista = data["Fragment"].ToString(); var jsonBody2 = JsonConvert.SerializeObject(bodyI);
            var BotonTexto = data["BotonTexto"].ToString(); string versionTelefono = "";
            bool b = false; string detalledes = ""; string inv = ""; string[] titulo; int importe = 0;
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            int ContadorBadge = 0;
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
                BasePath = "https://service-web-258723.firebaseio.com/"
            };
            IFirebaseClient client;
            switch (tipo)
            {
                case "Proveedor"://REQUIERE SERVICIO

                    detalledes = Convert.ToString(BotonTexto);
                    inv = "si";
                    break;
                case "CotizacionProveedor":


                    break;
                case "Adjudicado":

                    detalledes = BotonTexto;
                    inv = "si";
                    break;
                case "ClienteFinServicioFinal":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 2;
                    break;
                case "Pagado":

                    break;
                case "Conversacion":
                    inv = "no";
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 9;
                    break;
                case "ClienteCotizacion":

                    break;

                case "ConfSiniestro":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 10;

                    notificacionPersona.RequiereServicioId = RequiereServicioId;

                    break;
                case "RechSiniestro":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 10;
                    notificacionPersona.RequiereServicioId = RequiereServicioId;

                    break;
                case "ProveedorV2":

                    inv = "si";
                    break;

            }

            foreach (var item in lstPersonas)
            {
                DataRow dataIdioma = bcPersona.ListadoDatosNotificacionv2(tipo, item.PersonaIdioma);
                var title = dataIdioma["title"].ToString(); var body = String.Format(dataIdioma["body"].ToString(), persona); BotonTexto = dataIdioma["BotonTexto"].ToString();
                if (tipo == "Conversacion")
                {
                    body = ConversacionContenido;
                }
                Respuesta resp = new Respuesta();
                if (requiereServicio != null)
                {
                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId(requiereServicio.RequiereServicioId, item.PersonaIdioma, BE.relRequiereServicio.servicio, BE.relServicio.servicioDetalle, BE.relServicio.categoriaServicio, BE.relCategoriaServicio.Ciudad, BE.relciudad.configuracionCiudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda, BE.relServicio.servicioRequerimiento
                    , BE.relRequiereServicio.estadoReqServ, BE.relRequiereServicio.requiereServicioProveedores, BE.relReqServProv.servicioPersona, BE.relRequiereServicio.personaDireccion
                    , BE.relRequiereServicio.persona, BE.relReqServProv.statusRequiere, BE.relRequiereServicio.servAsig, BE.relServAsig.statusServAsig, BE.relServAsig.post, BE.relServAsig.servAsigCosto, BE.relRequiereServicio.requiereServicioDetalle);

                    if (b == false)
                    {
                        title = title + " " + "RQ - " + Convert.ToString(requiereServicio.RequiereServicioId);
                        b = true;
                    }
                    notificacionPersona.RequiereServicioId = requiereServicio.RequiereServicioId;
                }
                if (tipo == "Proveedor")
                {
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 2;
                }
                if (tipo == "CotizacionProveedor")
                {
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 3;

                }
                if (tipo == "Adjudicado")
                {
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 5;
                }
                if (tipo == "ProveedorV2")
                {
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 2;
                }
                if ((tipo != "CotizacionProveedor"))
                {
                    var to = item.PersonaTokenId;
                    var senderId2 = string.Format("id={0}", SenderIdFB);
                    var key = string.Format("key={0}", ServerKey);
                    var sound = "default";
                    var invasivo = inv;
                    var action = "";
                    string id = "";
                    string sn = "";
                    string sf = "";
                    if (requiereServicio != null)
                    {
                        id = requiereServicio.RequiereServicioId;
                        sn = requiereServicio.servicio.ServicioNombre;
                        sf = requiereServicio.servicio.ServicioURLFoto;
                    }

                    if (tipo == "ProveedorV2")
                    {
                        if (requiereServicio.requiereServicioOtros == true)
                        {
                            requiereServicio.servicio.servicioDetalleTipo = true;
                        }

                        importe = bcRequiereServicio.ImporteRequiereServicio(requiereServicio.RequiereServicioId,
                        requiereServicio.servicio.servicioDetalleTipo);
                        titulo = dataIdioma["body"].ToString().Split('/');
                        sn = titulo[0] + requiereServicio.servicio.ServicioNombre + titulo[1] +
                        requiereServicio.personaDireccion.PersonaDireccionDescripcion + titulo[2]
                        + requiereServicio.RequiereServicioFHDeseada.ToString("dd/MM/yyyy",
                        CultureInfo.InvariantCulture) + titulo[3] +
                        requiereServicio.RequiereServicioFHDeseada.ToString("hh:mm:ss", CultureInfo.InvariantCulture)
                        + titulo[4] + string.Format("{0:N}", importe) + " " +
                        requiereServicio.servicio.categoriaServicio.ciudad.Region.pais.Moneda.MonedaNombre;
                        action = "as";
                        body = sn;
                    }
                    var reqservid = id;
                    var servicioNombre = sn;
                    var ServicioUrlFoto = sf;
                    var reqservdes = BotonTexto;
                    var priority = "high";
                    //////////////////////////////////////////////////////////////////
                    client = new FireSharp.FirebaseClient(config);
                    if (client != null)
                    {


                        FirebaseResponse response = await client.GetTaskAsync("Notificacion/" + Convert.ToInt32(item.PersonaId));
                        var data1 = new Data
                        {
                            PersonaId = "",
                            Post = 0,
                            Solicitudes = 0,
                            Servicios = 0,
                            Rendimientos = 0,
                            Notificaciones = 0,

                        };

                        if (response.Body != "null")
                        {

                            Data obj = response.ResultAs<Data>();
                            data1 = obj;
                            ContadorBadge = Convert.ToInt32(obj.Notificaciones) + 1;
                        }
                    }
                    var badge = ContadorBadge;
                    //  var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                    versionTelefono = bclogSesionesPersona.VersionTelefono(item.PersonaId);
                    var tag = "no";
                    if (inv == "si")
                    {
                        tag = "si";
                        if (versionTelefono.Contains("IOS"))
                        {
                            /* var data2 = new { to, notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag },priority };
                             jsonBody2 = JsonConvert.SerializeObject(data2);*/
                            /*  var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                                jsonBody2 = JsonConvert.SerializeObject(data2);*/
                            tag = inv;
                            var data2 = new { to, notification = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag, badge, sound }, priority };
                            jsonBody2 = JsonConvert.SerializeObject(data2);

                        }
                        else
                        {
                            /*  var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action,tag } };
                              jsonBody2 = JsonConvert.SerializeObject(data2);*/
                            var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);

                        }

                    }
                    else
                    {
                        if (versionTelefono.Contains("IOS"))
                        {
                            var data2 = new { to, notification = new { title, body, badge, sound }, priority };
                            jsonBody2 = JsonConvert.SerializeObject(data2);
                            InsertarLogNotificacion("badge", DateTime.Now, Convert.ToString(badge), item.PersonaId);
                        }
                        else
                        {
                            //   var data2 = new { to, notification = new { sound, title, body } };
                            // jsonBody2 = JsonConvert.SerializeObject(data2);
                            /*var data2 = new { to, notification = new { sound,title, body } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);*/
                            var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);

                        }


                    }

                    System.Diagnostics.Debug.WriteLine(jsonBody2);
                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                        httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                        httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");
                        using (var httpClient = new HttpClient())
                        {
                            var result = await httpClient.SendAsync(httpRequest);

                            if (result.IsSuccessStatusCode)
                            {

                                InsertarLogNotificacion("NUEVOREQUERIMIENTO_" + requiereServicio.RequiereServicioId, DateTime.Now, to, item.PersonaId);
                            }
                            else
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId);
                            }
                        }
                    }
                }
                notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;
                notificacionPersona.NotificacionPersonaTitulo = title;
                notificacionPersona.TipoEstadoId = 1;
                notificacionPersona.NotificacionPersonaDescripcion = body;
                notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
                notificacionPersona.NotificacionPersonaFragment = vista;
                notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
                notificacionPersona.PersonaId = item.PersonaId;
                if (notificacionPersona != null)
                {
                    bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
                    await NotificacionFirebase(Convert.ToInt32(item.PersonaId), "recibido", "Notificaciones");
                }

            }
        }



        private static void EnvioCorreoAdjudicacionClienteConFormato()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            decimal total = 0;
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            string fila1 = "";
            int cont = 1;
            string text = "";
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select rs.RequiereServicioId,p.PersonaNombres+' '+ p.PersonaApellidos as persona,p.PersonaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion, p1.PersonaNombres + ' ' + p1.PersonaApellidos as personaProveedor,pd.PersonaDireccionDescripcion,CONVERT(VARCHAR(10), rs.RequiereServicioFHDeseada, 103) + ' ' + convert(VARCHAR(8), rs.RequiereServicioFHDeseada, 14)fecha,p1.PersonaCorreo PersonaCorreoProveedor,[dbo].[ObtenerRanking] (sp.ServicioPersonaId)ranking,[dbo].[ObtenerNroTrabajos](sp.ServicioPersonaId) NroTrabajos, sa.ServAsigCostoTotal ,CAST((ROUND(cc.ConfiguracionCiudadValorSeguro, 0)) AS INT) AS ConfiguracionCiudadValorSeguro from[dbo].[RequiereServicioProveedores] rsp with(nolock) inner join ServAsig sa with(nolock) on rsp.RequiereServicioId = sa.RequiereServicioId inner join RequiereServicio rs with(nolock) on rsp.RequiereServicioId = rs.RequiereServicioId inner join Persona p1 with(nolock) on sa.ProveedorId = p1.PersonaId inner join  Persona p with(nolock) on rs.personaId = p.personaId inner join servicioPersona sp with(nolock) on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join servicio s with(nolock) on s.servicioId = rs.servicioId inner join PersonaDireccion pd on((rs.PersonaDireccionId = pd.PersonaDireccionId) and(rs.PersonaId = pd.PersonaId)) inner join ConfiguracionCiudad cc on p.CiudadId = cc.CiudadId where rsp.StatusRequiereId = 4 and sa.StatusServAsigId = 1 and rs.EstadoReqServId = 2 AND('AC_' + rs.RequiereServicioId) NOT IN(select Descripcion RequiereServicioId  from envioCorreo where Descripcion is not null ) ";

                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoFormato1"].ToString());

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (bcEnvioCorreo.ExisteId("AC_" + dr["RequiereServicioId"].ToString()) == 0)
                            {
                                if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                                {
                                    requiereServicioId = dr["RequiereServicioId"].ToString();
                                    //////////////////////////////////
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = requiereServicioId;
                                    cmd.CommandText = "select rsd.ServicioDetalleId,sd.ServicioDetalleDescripcion,rsd.ServicioDetalleCantidad,rsd.ServicioDetallePUFecha,(rsd.ServicioDetalleCantidad * rsd.ServicioDetallePUFecha)total  from RequiereServicioDetalle rsd with(nolock) inner join ServicioDetalle sd on(rsd.ServicioId = sd.ServicioId and rsd.ServicioDetalleId = sd.ServicioDetalleId)  where RequiereServicioId = @RequiereServicioId and rsd.ServicioDetalleCantidad > 0 ";
                                    cmd.CommandTimeout = 0;
                                    da.SelectCommand = cmd;
                                    da.Fill(ds2);
                                    ////////////////////////////////////
                                    if (ds2.Tables["Table"].Rows.Count > 0)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = requiereServicioId;
                                        cmd.CommandText = "select sum(rsd.ServicioDetalleCantidad*rsd.ServicioDetallePUFecha)Total from RequiereServicioDetalle rsd where RequiereServicioId = @requiereServicioId AND rsd.ServicioDetalleCantidad > 0";
                                        cmd.CommandTimeout = 0;
                                        decimal seguro = Convert.ToDecimal(dr["ConfiguracionCiudadValorSeguro"].ToString());
                                        string valortotal = Convert.ToString(cmd.ExecuteScalar());
                                        if (valortotal != "")
                                        {
                                            total = Convert.ToInt32(cmd.ExecuteScalar()) + seguro;
                                        }

                                        cont = 1;
                                        foreach (DataRow dr1 in ds2.Tables[0].Rows)
                                        {
                                            fila1 = fila1 + "<tr> <td style = [border: thin solid #CCCCCC; font-size: medium; text-align: right;[ >" + cont + "</td> <td style = [border: thin solid #CCCCCC; font-size:medium; text-align: right;[ class=[auto -style1[ >" + dr1["ServicioDetalleDescripcion"].ToString() + "</td> <td style = [border: thin solid #CCCCCC; font-size: medium; text-align: right;[ >" + (dr1["ServicioDetallePUFecha"].ToString()) + " </td>  <td style=[border: thin solid #CCCCCC; font-size: medium; text-align: right;[ >" + dr1["ServicioDetalleCantidad"].ToString() + "</td> <td style = [border: thin solid #CCCCCC; font-size: medium; text-align: right;[ >" + Convert.ToString(Convert.ToDecimal(dr1["total"].ToString())) + " </td>  </tr> ";
                                            cont = cont + 1;
                                        }
                                        //   string tabla = " <tr>< td style = [border: thin solid #CCCCCC; font-size: x-small; text-align: right;[>" +1+" </ td > < td style =[border: thin solid #CCCCCC; font-size: x-small; text-align: right;[class=[auto -style1[ > "+"Cambio de Ducha"+"</td><td style = [border: thin solid #CCCCCC; font-size: x-small; text-align: right;[ >"+ "180"+" </ td >< td style=[border: thin solid #CCCCCC; font-size: x-small; text-align: right;[ > "+1+"</td> < td style = [border: thin solid #CCCCCC; font-size: x-small; text-align: right;[ >"+ "180"+" </ td >   </ tr > ";
                                        // titulo = titulo  +"</table>";
                                        fila1 = fila1.Replace('[', '"');
                                        text = text.Replace("fila1", fila1);
                                        text = text.Replace("seguro", dr["ConfiguracionCiudadValorSeguro"].ToString());
                                        text = text.Replace("total", Convert.ToString(total));
                                    }
                                    else
                                    {
                                        text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoFormatoSinDetalle"].ToString());

                                        text = text.Replace("DescripcionReq", dr["RequiereServicioDescripcion"].ToString());
                                    }
                                    //////////////////////////////////
                                    mail.From = new MailAddress("webmaster@serviceweb.bo");
                                    mail.To.Add(dr["PersonaCorreo"].ToString());
                                    mail.Subject = "Servicio Asignado en Service Web";
                                    AlternateView plainView = AlternateView.CreateAlternateViewFromString(text, Encoding.UTF8, MediaTypeNames.Text.Plain);
                                    text = text.Replace("nombre", dr["persona"].ToString());
                                    text = text.Replace("RequiereServicioId", dr["RequiereServicioId"].ToString());
                                    text = text.Replace("PersonaDireccionDescripcion", dr["PersonaDireccionDescripcion"].ToString());
                                    text = text.Replace("fecha", dr["fecha"].ToString());
                                    text = text.Replace("Proveedor", dr["personaProveedor"].ToString());
                                    text = text.Replace("ranking", dr["ranking"].ToString());
                                    text = text.Replace("NroTrabajos", dr["NroTrabajos"].ToString());
                                    string html = text;
                                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                                    LinkedResource img = new LinkedResource(@"C:\AdjudicacionCliente\email solucion.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgs = new LinkedResource(@"C:\AdjudicacionCliente\seguro.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgf = new LinkedResource(@"C:\AdjudicacionCliente\facebook.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgi = new LinkedResource(@"C:\AdjudicacionCliente\instagram.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgd = new LinkedResource(@"C:\AdjudicacionCliente\piefinal2.png", MediaTypeNames.Image.Jpeg);
                                    img.ContentId = "imagen";
                                    imgs.ContentId = "seguro";
                                    imgf.ContentId = "facebook";
                                    imgi.ContentId = "instagram";
                                    imgd.ContentId = "direccion";
                                    htmlView.LinkedResources.Add(img);
                                    htmlView.LinkedResources.Add(imgs);
                                    htmlView.LinkedResources.Add(imgf);
                                    htmlView.LinkedResources.Add(imgi);
                                    htmlView.LinkedResources.Add(imgd);
                                    mail.AlternateViews.Add(plainView);
                                    mail.AlternateViews.Add(htmlView);
                                    var smtp = new SmtpClient
                                    {
                                        Host = "smtp.office365.com",
                                        Port = 587,
                                        EnableSsl = true,
                                        UseDefaultCredentials = false,
                                        //  Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                                        Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!"),
                                        DeliveryMethod = SmtpDeliveryMethod.Network,
                                    };
                                    try
                                    {
                                        smtp.Send(mail);
                                    }
                                    catch (Exception ex)
                                    {
                                        SqlCommand commandlog = new SqlCommand();
                                        commandlog.Connection = cnn;
                                        commandlog.Parameters.Add("@personaCorreo", SqlDbType.VarChar).Value = Convert.ToString(dr["PersonaCorreo"].ToString());
                                        commandlog.CommandText = "update envioCorreo set Estado = 'CorreoInvalido' where PersonaCorreo = @personaCorreo";
                                        int cantidad = Convert.ToInt32(commandlog.ExecuteScalar());
                                        commandlog.Parameters.Clear();
                                        cnn.Close();

                                    }
                                    string Body = text;
                                    envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                                    envioCorreo.PersonaCorreo = (dr["PersonaCorreo"].ToString());
                                    envioCorreo.Subject1 = "Ya tienes la solución a tu Solicitud";// "Adjudicación Service Web RQ " + requiereServicioId;

                                    envioCorreo.Body = Body;
                                    envioCorreo.Fecha = DateTime.Now;
                                    envioCorreo.TipoCorreo = "AdjudicacionCliente";
                                    envioCorreo.Estado = "Enviado";
                                    envioCorreo.Descripcion = "AC_" + dr["RequiereServicioId"].ToString();
                                    bcEnvioCorreo.Actualizar(ref envioCorreo, false);

                                }


                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        private static void EnvioCorreoFinalizacionClienteConFormato()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            decimal total = 0;
            decimal insumos = 0;
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            string fila1 = "";
            int cont = 1;
            string text = "";
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select* from(select rs.RequiereServicioId, p.PersonaNombres+' ' + p.PersonaApellidos as persona,p.PersonaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,p1.PersonaNombres + ' ' + p1.PersonaApellidos as personaProveedor,p1.PersonaCorreo PersonaCorreoProveedor,rsp.RequiereServicioProvCotizacion,CONVERT(VARCHAR(10),rs.RequiereServicioFHDeseada, 103) + ' ' + convert(VARCHAR(8),rs.RequiereServicioFHDeseada, 14)fecha,pd.PersonaDireccionTitulo,pd.PersonaDireccionDescripcion,CAST((ROUND(sa.ServAsigCostoTotal, 0)) AS INT)ServAsigCostoTotal ,CAST((ROUND(cc.ConfiguracionCiudadValorSeguro, 0)) AS INT) AS ConfiguracionCiudadValorSeguro, [dbo].[ObtenerRanking](sp.ServicioPersonaId)ranking,[dbo].[ObtenerNroTrabajos](sp.ServicioPersonaId) NroTrabajos,'FSC_' + rs.RequiereServicioId as IdCorreo from[dbo].[RequiereServicioProveedores] rsp with(nolock) inner join ServAsig sa with(nolock) on rsp.RequiereServicioId = sa.RequiereServicioId inner join RequiereServicio rs on rsp.RequiereServicioId = rs.RequiereServicioId inner join Persona p1 on sa.ProveedorId = p1.PersonaId inner join  Persona p on rs.personaId = p.personaId inner join servicioPersona sp on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join servicio s on s.servicioId = rs.servicioId inner join PersonaDireccion pd on rs.PersonaDireccionId = pd.PersonaDireccionId and rs.PersonaId = pd.PersonaId INNER JOIN ConfiguracionCiudad cc on cc.CiudadId = p.CiudadId where rsp.StatusRequiereId = 4 and sa.StatusServAsigId = 3 and rs.EstadoReqServId = 2 AND sa.ServAsigFHPago is not null )c1 where C1.IdCorreo not in (select Descripcion from envioCorreo WHERE Descripcion LIKE '%FSC_%') ORDER BY RequiereServicioId DESC   ";

                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoFormatoF"].ToString());

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (bcEnvioCorreo.ExisteId("FSC_" + dr["RequiereServicioId"].ToString()) == 0)
                            {
                                if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                                {
                                    requiereServicioId = dr["RequiereServicioId"].ToString();
                                    //////////////////////////////////
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = requiereServicioId;
                                    cmd.CommandText = "select rsd.ServicioDetalleId,sd.ServicioDetalleDescripcion,rsd.ServicioDetalleCantidad,rsd.ServicioDetallePUFecha,(rsd.ServicioDetalleCantidad * rsd.ServicioDetallePUFecha)total  from RequiereServicioDetalle rsd with(nolock) inner join ServicioDetalle sd on(rsd.ServicioId = sd.ServicioId and rsd.ServicioDetalleId = sd.ServicioDetalleId)  where RequiereServicioId = @RequiereServicioId and rsd.ServicioDetalleCantidad > 0 ";
                                    cmd.CommandTimeout = 0;
                                    da.SelectCommand = cmd;
                                    da.Fill(ds2);
                                    ////////////////////////////////////
                                    if (ds2.Tables["Table"].Rows.Count > 0)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = requiereServicioId;
                                        cmd.CommandText = "select sum(rsd.ServicioDetalleCantidad*rsd.ServicioDetallePUFecha)Total from RequiereServicioDetalle rsd where RequiereServicioId = @requiereServicioId AND rsd.ServicioDetalleCantidad > 0";
                                        cmd.CommandTimeout = 0;
                                        decimal seguro = Convert.ToDecimal(dr["ConfiguracionCiudadValorSeguro"].ToString());
                                        string valortotal = Convert.ToString((cmd.ExecuteScalar()));
                                        if (valortotal != "")
                                        {
                                            insumos = Convert.ToInt32(dr["ServAsigCostoTotal"].ToString()) - Convert.ToInt32(cmd.ExecuteScalar()) - seguro;
                                        }

                                        cont = 1;
                                        foreach (DataRow dr1 in ds2.Tables[0].Rows)
                                        {
                                            fila1 = fila1 + "<tr> <td style = [border: thin solid #CCCCCC; font-size: medium; text-align: right;[ >" + cont + "</td> <td style = [border: thin solid #CCCCCC; font-size:medium; text-align: right;[ class=[auto -style1[ >" + dr1["ServicioDetalleDescripcion"].ToString() + "</td> <td style = [border: thin solid #CCCCCC; font-size: medium; text-align: right;[ >" + (dr1["ServicioDetallePUFecha"].ToString()) + " </td>  <td style=[border: thin solid #CCCCCC; font-size: medium; text-align: right;[ >" + dr1["ServicioDetalleCantidad"].ToString() + "</td> <td style = [border: thin solid #CCCCCC; font-size: medium; text-align: right;[ >" + Convert.ToString(Convert.ToDecimal(dr1["total"].ToString())) + " </td>  </tr> ";
                                            cont = cont + 1;
                                        }
                                        //   string tabla = " <tr>< td style = [border: thin solid #CCCCCC; font-size: x-small; text-align: right;[>" +1+" </ td > < td style =[border: thin solid #CCCCCC; font-size: x-small; text-align: right;[class=[auto -style1[ > "+"Cambio de Ducha"+"</td><td style = [border: thin solid #CCCCCC; font-size: x-small; text-align: right;[ >"+ "180"+" </ td >< td style=[border: thin solid #CCCCCC; font-size: x-small; text-align: right;[ > "+1+"</td> < td style = [border: thin solid #CCCCCC; font-size: x-small; text-align: right;[ >"+ "180"+" </ td >   </ tr > ";
                                        // titulo = titulo  +"</table>";
                                        fila1 = fila1.Replace('[', '"');
                                        text = text.Replace("fila1", fila1);
                                        text = text.Replace("seguro", dr["ConfiguracionCiudadValorSeguro"].ToString());
                                        text = text.Replace("insumos", (string.Format("{0:N2}", Convert.ToString(insumos))));

                                    }
                                    else
                                    {
                                        text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoFormatoF_SinDetalle"].ToString());

                                        text = text.Replace("DescripcionReq", dr["RequiereServicioDescripcion"].ToString());
                                    }
                                    //////////////////////////////////
                                    mail.From = new MailAddress("webmaster@serviceweb.bo");

                                    mail.To.Add(dr["PersonaCorreo"].ToString());


                                    mail.Subject = "Notificaciones Service Web";
                                    AlternateView plainView = AlternateView.CreateAlternateViewFromString(text, Encoding.UTF8, MediaTypeNames.Text.Plain);
                                    text = text.Replace("nombre", dr["persona"].ToString());
                                    text = text.Replace("RequiereServicioId", dr["RequiereServicioId"].ToString());
                                    text = text.Replace("PersonaDireccionDescripcion", dr["PersonaDireccionDescripcion"].ToString());
                                    text = text.Replace("fecha", dr["fecha"].ToString());
                                    text = text.Replace("Proveedor", dr["personaProveedor"].ToString());
                                    text = text.Replace("ranking", dr["ranking"].ToString());
                                    text = text.Replace("NroTrabajos", dr["NroTrabajos"].ToString());
                                    text = text.Replace("total", Convert.ToString((Convert.ToInt32(dr["ServAsigCostoTotal"].ToString()))));

                                    string html = text;
                                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                                    LinkedResource img = new LinkedResource(@"C:\FinalizacionCliente\email gracias.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgs = new LinkedResource(@"C:\FinalizacionCliente\personaFinal.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgf = new LinkedResource(@"C:\FinalizacionCliente\facebook.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgi = new LinkedResource(@"C:\FinalizacionCliente\instagram.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgsescudo = new LinkedResource(@"C:\FinalizacionCliente\escudo.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgd = new LinkedResource(@"C:\FinalizacionCliente\piefinal2.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgcelular = new LinkedResource(@"C:\FinalizacionCliente\celular.png", MediaTypeNames.Image.Jpeg);
                                    LinkedResource imgseguro = new LinkedResource(@"C:\FinalizacionCliente\seguro.png", MediaTypeNames.Image.Jpeg);
                                    img.ContentId = "GraciasServiceWeb";
                                    imgs.ContentId = "ImagenServiceWeb";
                                    imgf.ContentId = "facebook";
                                    imgi.ContentId = "instagram";
                                    imgd.ContentId = "direccion";
                                    imgseguro.ContentId = "seguro";
                                    imgcelular.ContentId = "celular";
                                    imgsescudo.ContentId = "escudo";

                                    htmlView.LinkedResources.Add(img);
                                    htmlView.LinkedResources.Add(imgs);
                                    htmlView.LinkedResources.Add(imgf);
                                    htmlView.LinkedResources.Add(imgi);
                                    htmlView.LinkedResources.Add(imgd);
                                    htmlView.LinkedResources.Add(imgsescudo);
                                    htmlView.LinkedResources.Add(imgcelular);
                                    htmlView.LinkedResources.Add(imgseguro);
                                    mail.AlternateViews.Add(plainView);

                                    mail.AlternateViews.Add(htmlView);
                                    var smtp = new SmtpClient
                                    {
                                        Host = "smtp.office365.com",
                                        Port = 587,
                                        EnableSsl = true,
                                        UseDefaultCredentials = false,
                                        //  Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                                        Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!"),
                                        DeliveryMethod = SmtpDeliveryMethod.Network,
                                    };
                                    try
                                    {
                                        smtp.Send(mail);
                                    }
                                    catch (Exception ex)
                                    {
                                        SqlCommand commandlog = new SqlCommand();
                                        commandlog.Connection = cnn;
                                        commandlog.Parameters.Add("@personaCorreo", SqlDbType.VarChar).Value = Convert.ToString(dr["PersonaCorreo"].ToString());
                                        commandlog.CommandText = "update envioCorreo set Estado = 'CorreoInvalido' where PersonaCorreo = @personaCorreo";
                                        int cantidad = Convert.ToInt32(commandlog.ExecuteScalar());
                                        commandlog.Parameters.Clear();
                                        cnn.Close();

                                    }


                                    string Body = text;
                                    envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                                    envioCorreo.PersonaCorreo = (dr["PersonaCorreo"].ToString());
                                    envioCorreo.Subject1 = "Finalización Servicio Service Web RQ " + requiereServicioId;
                                    envioCorreo.Body = Body;
                                    envioCorreo.Fecha = DateTime.Now;
                                    envioCorreo.TipoCorreo = "FinServicioCliente";
                                    envioCorreo.Estado = "Enviado";
                                    envioCorreo.Descripcion = "FSC_" + dr["RequiereServicioId"].ToString();
                                    bcEnvioCorreo.Actualizar(ref envioCorreo, false);

                                }


                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        public static DateTime ObtenerHoraBoliviana()
        {
            DateTime localDateTime = DateTime.Now;
            localDateTime = localDateTime.ToLocalTime();
            DateTime utcDateTime = localDateTime.ToUniversalTime();
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SA Western Standard Time");
            DateTime worldDateTime = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(utcDateTime.ToString()), timeZoneInfo);

            return worldDateTime;
        }
        public static void CambiarEstadoDesierto()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            try
            {

                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                //  cmd.CommandText = "select rs.requiereServicioId,p.personaId,p.personaTokenId,s.servicioNombre,per.PersonaNombres+per.PersonaApellidos as NombreProveedor, convert(varchar,rs.RequiereServicioFHDeseada,108) as Hora from requiereServicio	rs inner join[dbo].[ServAsig]  sa on rs.requiereServicioId=sa.requiereServicioId inner join servicio s on rs.servicioId=s.servicioId inner join Persona p on rs.personaId=p.personaId inner join Persona per on sa.proveedorId=per.PersonaId AND year(rs.RequiereServicioFHDeseada)=year(getdate()) AND month(rs.RequiereServicioFHDeseada)=month(getdate()) AND day(rs.RequiereServicioFHDeseada)=day(getdate())";
                cmd.CommandText = "select * from requiereservicio where Convert(DATE, RequiereServicioFHCaduca)<=Convert(DATE, (GETDATE() - 1))  and EstadoReqServId = 1";
                da.SelectCommand = cmd;
                da.Fill(ds);
                foreach (DataRow myR in ds.Tables[0].Rows)
                {
                    cmd.CommandText = "update RequiereServicio set EstadoReqServId=4,RequiereServicioFechaMod=getdate() where  RequiereServicioId=@RequiereServicioid";
                    cmd.Parameters.Add("@RequiereServicioid", SqlDbType.VarChar).Value = myR["requiereServicioId"].ToString();
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS", DateTime.Now, myR["requiereServicioId"].ToString(), 0);

                }


            }
            catch (Exception ex)
            {

                InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS", DateTime.Now, ex.Message, 0);
            }
            finally
            {
                cnn.Close();
            }

        }
        public static void EliminacionPersonasNoValidadas()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            try
            {
                cnn.Open();
                cmd.Connection = cnn;
                //////////////////////////////////////////////////
                SqlCommand command = new SqlCommand("[EliminacionPersonas]", cnn);
                command.CommandType = CommandType.StoredProcedure;
                string s = Convert.ToString(command.ExecuteNonQuery());

                InsertarLogNotificacion("Eliminacion de personas ", DateTime.Now, s, 0);
            }
            catch (Exception ex)
            {

                InsertarLogNotificacion("Eliminacion de personas", DateTime.Now, ex.Message, 0);
            }
            finally
            {
                cnn.Close();
            }

        }
        private static async Task EnviarNotificacionCotizaciones()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataSet data = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            int cantidad = 0;
            string PersonaTokenId = "";


            try
            {


                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                //   cmd.CommandText = "select Count (rs.PersonaId)cantidad,rs.PersonaId,p.PersonaTokenId from requiereServicio rs inner join  [dbo].[RequiereServicioProveedores]  rsp on rs.RequiereServicioId=rsp.RequiereServicioId  inner join Persona p  on rs.PersonaId=p.PersonaId  where StatusRequiereId=2  group by rs.PersonaId,p.PersonaTokenId";
                // cmd.CommandText = "select Count(rs.PersonaId)cantidad,rs.PersonaId,p.PersonaTokenId from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.RequiereServicioId = rsp.RequiereServicioId inner join Persona p on rs.PersonaId = p.PersonaId where StatusRequiereId = 2 and rs.EstadoReqServId = 1  group by rs.PersonaId, p.PersonaTokenId"; //union all select Count(rs.PersonaId)cantidad,rs.PersonaId,p.PersonaTokenId from requiereServicio rs inner join  [dbo].[RequiereServicioProveedores] rsp  on rs.RequiereServicioId=rsp.RequiereServicioId  inner join Persona p  on rs.PersonaId=p.PersonaId  where StatusRequiereId=2  group by rs.PersonaId,p.PersonaTokenId ";

                cmd.CommandText = "select Count(rs.PersonaId)cantidad,rs.PersonaId,p.PersonaTokenId ,rs.RequiereServicioId from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.RequiereServicioId = rsp.RequiereServicioId inner join Persona p on rs.PersonaId = p.PersonaId where StatusRequiereId = 2 and rs.EstadoReqServId = 1  group by rs.PersonaId, p.PersonaTokenId,rs.RequiereServicioId ";
                da.SelectCommand = cmd;
                da.Fill(ds);

                var senderId2 = string.Format("id={0}", SenderIdFB);
                var key = string.Format("key={0}", ServerKey);

                foreach (DataRow myR in ds.Tables[0].Rows)
                {
                    cantidad = (Convert.ToInt32(myR["cantidad"].ToString()));
                    PersonaTokenId = (myR["PersonaTokenId"].ToString());
                    data = ListadoDatosNotificacionv2("ClienteCotizacion", "es");
                    var title = data.Tables[0].Rows[0]["title"].ToString();
                    var body = data.Tables[0].Rows[0]["body"].ToString() + " " + Convert.ToString(cantidad) + " cotizaciones.";
                    var vista = data.Tables[0].Rows[0]["Fragment"].ToString();
                    var jsonBody2 = JsonConvert.SerializeObject(body);
                    var to = PersonaTokenId;
                    var invasivo = "no";
                    var reqservid = myR["RequiereServicioId"].ToString();
                    var reqservdes = vista;///esta campo va a body en el invasivo 
                    var servicioNombre = body;
                    var ServicioUrlFoto = "";
                    var sound = "";
                    var action = "";
                    if ((reqservid != "A936") || (reqservid != "A950"))
                        await EnviarNotificacionesAsync(to, sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
        }
        private static async Task EnviarNotificacionCotizacionesv2()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataSet data = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            int cantidad = 0;
            string PersonaTokenId = "";


            try
            {


                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                //   cmd.CommandText = "select Count (rs.PersonaId)cantidad,rs.PersonaId,p.PersonaTokenId from requiereServicio rs inner join  [dbo].[RequiereServicioProveedores]  rsp on rs.RequiereServicioId=rsp.RequiereServicioId  inner join Persona p  on rs.PersonaId=p.PersonaId  where StatusRequiereId=2  group by rs.PersonaId,p.PersonaTokenId";
                // cmd.CommandText = "select Count(rs.PersonaId)cantidad,rs.PersonaId,p.PersonaTokenId from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.RequiereServicioId = rsp.RequiereServicioId inner join Persona p on rs.PersonaId = p.PersonaId where StatusRequiereId = 2 and rs.EstadoReqServId = 1  group by rs.PersonaId, p.PersonaTokenId"; //union all select Count(rs.PersonaId)cantidad,rs.PersonaId,p.PersonaTokenId from requiereServicio rs inner join  [dbo].[RequiereServicioProveedores] rsp  on rs.RequiereServicioId=rsp.RequiereServicioId  inner join Persona p  on rs.PersonaId=p.PersonaId  where StatusRequiereId=2  group by rs.PersonaId,p.PersonaTokenId ";

                cmd.CommandText = "select Count(rs.PersonaId)cantidad,rs.PersonaId,p.PersonaTokenId ,rs.RequiereServicioId, p.PersonaIdioma from requiereServicio rs with(nolock) inner join[dbo].[RequiereServicioProveedores]  rsp with(nolock) on rs.RequiereServicioId = rsp.RequiereServicioId inner join Persona p on rs.PersonaId = p.PersonaId where StatusRequiereId = 2 and rs.EstadoReqServId = 1 group by rs.PersonaId, p.PersonaTokenId, rs.RequiereServicioId, P.PersonaIdioma ";
                da.SelectCommand = cmd;
                da.Fill(ds);

                var senderId2 = string.Format("id={0}", SenderIdFB);
                var key = string.Format("key={0}", ServerKey);

                foreach (DataRow myR in ds.Tables[0].Rows)
                {
                    cantidad = (Convert.ToInt32(myR["cantidad"].ToString()));
                    PersonaTokenId = (myR["PersonaTokenId"].ToString());
                    data = ListadoDatosNotificacionv2("ClienteCotizacion", myR["PersonaIdioma"].ToString());
                    var title = data.Tables[0].Rows[0]["title"].ToString();
                    var body = data.Tables[0].Rows[0]["body"].ToString();
                    var vista = data.Tables[0].Rows[0]["Fragment"].ToString();
                    var BotonTexto = data.Tables[0].Rows[0]["BotonTexto"].ToString();
                    string[] cuerpo = body.Split('/');
                    body = "";
                    body = cuerpo[0] + " " + cuerpo[1] + " " + Convert.ToString(cantidad) + " " + cuerpo[2];


                    var jsonBody2 = JsonConvert.SerializeObject(body);
                    var to = PersonaTokenId;
                    var invasivo = "no";
                    var reqservid = myR["RequiereServicioId"].ToString();
                    var reqservdes = vista;///esta campo va a body en el invasivo 
                    var servicioNombre = body;
                    var ServicioUrlFoto = "";
                    var sound = "";
                    var action = "";


                    await EnviarNotificacionesAsync(to, sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
        }
        private static async Task EnviarNotifcacionServicio()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataSet data = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            int cantidad = 0;
            string PersonaTokenId = "";
            string ServicioNombre = "";
            string NombreProveedor = "";
            string Hora = "";
            string versionTelefono = "";
            try
            {


                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                //  cmd.CommandText = "select p.personaId,p.personaTokenId,s.servicioNombre,per.PersonaNombres+per.PersonaApellidos as NombreProveedor, convert(varchar,rs.RequiereServicioFHDeseada,108) as Hora from requiereServicio	rs inner join[dbo].[ServAsig]  sa on rs.requiereServicioId=sa.requiereServicioId inner join servicio s on rs.servicioId=s.servicioId inner join Persona p on rs.personaId=p.personaId inner join Persona per on sa.proveedorId=per.PersonaId AND year(rs.RequiereServicioFHDeseada)=year(getdate()) AND month(rs.RequiereServicioFHDeseada)=month(getdate()) AND day(rs.RequiereServicioFHDeseada)=day(getdate())";
                int diaBolivia = ObtenerHoraBoliviana().Day;

                cmd.CommandText = " " +
                    "select rs.requiereServicioId, p.personaId,p.personaTokenId,s.servicioNombre,per.PersonaNombres +' '+ " +
                    "per.PersonaApellidos   as NombreProveedor," +
                    "CONVERT(VARCHAR, SUbSTRING(CONVERT(VARCHAR, IsNull(rs.RequiereServicioFHDeseada, ' '), 100), 12, 100)) " +
                    " as Hora  from requiereServicio rs  inner join [dbo].[ServAsig] sa " +
                    " on rs.requiereServicioId = sa.requiereServicioId  inner join servicio s " +
                    "on rs.servicioId = s.servicioId  inner join Persona p on rs.personaId = p.personaId  inner join Persona per  on sa.proveedorId = per.PersonaId  and sa.StatusServAsigId = 1  AND year(rs.RequiereServicioFHDeseada)= year(getdate())  AND month(rs.RequiereServicioFHDeseada)= month(getdate())  AND day(rs.RequiereServicioFHDeseada)=" + diaBolivia;
                da.SelectCommand = cmd;
                da.Fill(ds);

                var senderId2 = string.Format("id={0}", SenderIdFB);
                var key = string.Format("key={0}", ServerKey);
                string[] titulo;
                foreach (DataRow myR in ds.Tables[0].Rows)
                {

                    BE.Persona persona = bcPersona.BuscarPersonaxId(Convert.ToDecimal(myR["personaId"].ToString()));
                    PersonaTokenId = (myR["PersonaTokenId"].ToString());
                    ServicioNombre = (myR["servicioNombre"].ToString());
                    NombreProveedor = (myR["NombreProveedor"].ToString());
                    Hora = (myR["Hora"].ToString());
                    data = ListadoDatosNotificacionv2("ClienteInicioaRealizar", persona.PersonaIdioma);
                    var title = data.Tables[0].Rows[0]["title"].ToString();
                    titulo = data.Tables[0].Rows[0]["body"].ToString().Split('/');
                    var body = data.Tables[0].Rows[0]["body"].ToString();
                    body = titulo[0] + Convert.ToString(NombreProveedor) + titulo[1] + Hora + titulo[2] + ServicioNombre;
                    var vista = "";
                    var jsonBody2 = JsonConvert.SerializeObject(body);
                    var to = PersonaTokenId;
                    var invasivo = "no";
                    var reqservid = (myR["requiereServicioId"].ToString());
                    var reqservdes = vista;///esta campo va a body en el invasivo 
                    var servicioNombre = body;
                    var ServicioUrlFoto = "";
                    var sound = "";
                    var action = "";
                    versionTelefono = bclogSesionesPersona.VersionTelefono(Convert.ToDecimal(myR["personaId"].ToString()));
                    var priority = "high";
                    var badge = "";
                    if (invasivo == "si")
                    {
                        ////////////////////////////////////////////////////

                        var tag = "si";
                        if (versionTelefono.Contains("IOS"))
                        {

                            var data2 = new { to, notification = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag, badge, sound }, priority };
                            jsonBody2 = JsonConvert.SerializeObject(data2);
                            /* var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                             jsonBody2 = JsonConvert.SerializeObject(data2);/
                            /*  var data2 = new { to, notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }, priority };
                           jsonBody2 = JsonConvert.SerializeObject(data2);*/

                        }
                        else
                        {
                            /*var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);*/


                            var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);

                        }
                        /////////////////////////////////////////////////////////////

                        /* var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                           jsonBody2 = JsonConvert.SerializeObject(data2);*/


                    }
                    else
                    {
                        if (versionTelefono.Contains("IOS"))
                        {

                            var data2 = new { to, notification = new { body, title, badge, sound }, priority };
                            jsonBody2 = JsonConvert.SerializeObject(data2);


                        }
                        else
                        {
                            //   var data2 = new { to, notification = new { sound, title, body } };
                            // jsonBody2 = JsonConvert.SerializeObject(data2);
                            var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);

                            /*   var data2 = new { to, data= new { priority,title,body,invasivo,vista,reqservid,action,ServicioUrlFoto,servicioNombre,reqservdes } };
                               jsonBody2 = JsonConvert.SerializeObject(data2);*/

                        }

                    }


                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                        // httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                        httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                        using (var httpClient = new HttpClient())
                        {
                            var result = await httpClient.SendAsync(httpRequest);

                            if (result.IsSuccessStatusCode)
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + "INICIOSERVICIO" + data, DateTime.Now, to, Convert.ToDecimal(myR["personaId"].ToString()));

                            }
                            else
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + "INICIOSERVICIO", DateTime.Now, to, Convert.ToDecimal(myR["personaId"].ToString()));

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
        }
        private static async Task EnviarNotifcacionProveedorIniciarServicio()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataSet data = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            int cantidad = 0;
            string requiereServicioId = "";
            string NombreCliente = "";
            string ServicioURLFoto = "";
            string Hora = "";
            string PersonaTokenId = "";
            string ns = "";
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                //  cmd.CommandText = "select rs.requiereServicioId,p.personaId,p.personaTokenId,s.servicioNombre,per.PersonaNombres+per.PersonaApellidos as NombreProveedor, convert(varchar,rs.RequiereServicioFHDeseada,108) as Hora from requiereServicio	rs inner join[dbo].[ServAsig]  sa on rs.requiereServicioId=sa.requiereServicioId inner join servicio s on rs.servicioId=s.servicioId inner join Persona p on rs.personaId=p.personaId inner join Persona per on sa.proveedorId=per.PersonaId AND year(rs.RequiereServicioFHDeseada)=year(getdate()) AND month(rs.RequiereServicioFHDeseada)=month(getdate()) AND day(rs.RequiereServicioFHDeseada)=day(getdate())";
                cmd.CommandText = " select rs.requiereServicioId,p.personaNombres + ' ' + PersonaApellidos      as NombreCliente,s.servicioNombre,s.ServicioURLFoto,p.PersonaTokenId       from requiereServicio rs inner      join servAsig sa      on rs.requiereServicioId = sa.RequiereServicioId inner      join persona p on sa.ProveedorId = p.personaId inner      join servicio s on rs.servicioId = s.servicioId AND year(rs.RequiereServicioFHDeseada) = year(getdate())  AND month(rs.RequiereServicioFHDeseada)= month(getdate())   AND day(rs.RequiereServicioFHDeseada)= day(getdate())  and DATEPART(HOUR, rs.RequiereServicioFHDeseada)+4 = DATEPART(HOUR, getdate())  and sa.ServAsigFHInicio is null ";
                da.SelectCommand = cmd;
                da.Fill(ds);

                var senderId2 = string.Format("id={0}", SenderIdFB);
                var key = string.Format("key={0}", ServerKey);

                foreach (DataRow myR in ds.Tables[0].Rows)
                {


                    requiereServicioId = (myR["requiereServicioId"].ToString());
                    NombreCliente = (myR["NombreCliente"].ToString());

                    ServicioURLFoto = (myR["ServicioURLFoto"].ToString());
                    PersonaTokenId = (myR["PersonaTokenId"].ToString());
                    var sound = "";
                    var title = "Inicia servicio en la app.";
                    var body = "iniciar tu servicio  en la app antes de iniciar el servicio donde el cliente, caso contrario no correra el seguro ";//"Iniciar el servicio: RQ= " + Convert.ToString(requiereServicioId) + " A nombre de " + Convert.ToString(NombreCliente) ;
                    var vista = "Iniciar Servicio";
                    var jsonBody2 = JsonConvert.SerializeObject(body);
                    var to = PersonaTokenId;
                    var invasivo = "si";
                    var reqservid = requiereServicioId;
                    var reqservdes = "Iniciar Servicio";///esta campo va a body en el invasivo 
                    var servicioNombre = body;
                    var ServicioUrlFoto = "";
                    var action = "is";
                    //await EnviarNotificacionesAsync(to,sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto,vista,action);
                    var tag = "si";
                    var data2 = new
                    {
                        to,
                        notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag },
                        data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }
                    };
                    jsonBody2 = JsonConvert.SerializeObject(data2);

                    System.Diagnostics.Debug.WriteLine(jsonBody2);

                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                        httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                        httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                        using (var httpClient = new HttpClient())
                        {
                            var result = await httpClient.SendAsync(httpRequest);

                            if (result.IsSuccessStatusCode)
                            {
                                // InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) +"INICIO SERVICIO" , DateTime.Now, to, reqservid, conexion);

                            }
                            else
                            {
                                //  InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId, conexion);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
        }
        public static async Task EnviarNotifcacionProveedorIniciarServicioV1()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataSet data = new DataSet();
            // SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            int cantidad = 0;
            string requiereServicioId = "";
            string NombreCliente = "";
            string ServicioURLFoto = "";
            string Hora = "";
            string PersonaTokenId = "";
            string ns = "";
            try
            {
                ///////////////////////////////////////////////////////////

                List<BE.RequiereServicio> lstRequiereServicio = bcRequiereServicio.ListadoRequerimientosaInciarServicio(ObtenerHoraBoliviana(), BE.relRequiereServicio.servAsig);
                BE.Persona persona = new BE.Persona();
                foreach (var requiereServicio in lstRequiereServicio)
                {

                    persona = bcPersona.BuscarPersonaxId(requiereServicio.servAsig.ProveedorId);
                    if (CantidadNotificacionesEnviadas("IS_" + requiereServicio.RequiereServicioId) == 0)
                    {
                        await EnviarNotificacionesAsyncCliente(persona, "ProveedorInicioServicio", persona.PersonaIdioma, requiereServicio, 0, 0, null);

                    }
                }

                //List<BE.Persona> lstProveedoresIniciarServicio = 
                //await  EnviarNotificacionesAsyncV3(lstProveedoresIniciarServicio,"tipo","lang",)

                ////////////////////////////////////////////////////////////

                /*   if (cnn.State != ConnectionState.Open) cnn.Open();
                   cmd.Connection = cnn;
                   //  cmd.CommandText = "select rs.requiereServicioId,p.personaId,p.personaTokenId,s.servicioNombre,per.PersonaNombres+per.PersonaApellidos as NombreProveedor, convert(varchar,rs.RequiereServicioFHDeseada,108) as Hora from requiereServicio	rs inner join[dbo].[ServAsig]  sa on rs.requiereServicioId=sa.requiereServicioId inner join servicio s on rs.servicioId=s.servicioId inner join Persona p on rs.personaId=p.personaId inner join Persona per on sa.proveedorId=per.PersonaId AND year(rs.RequiereServicioFHDeseada)=year(getdate()) AND month(rs.RequiereServicioFHDeseada)=month(getdate()) AND day(rs.RequiereServicioFHDeseada)=day(getdate())";
                   cmd.CommandText = " select rs.requiereServicioId,p.personaNombres + ' ' + PersonaApellidos      as NombreCliente,s.servicioNombre,s.ServicioURLFoto,p.PersonaTokenId       from requiereServicio rs inner      join servAsig sa      on rs.requiereServicioId = sa.RequiereServicioId inner      join persona p on sa.ProveedorId = p.personaId inner      join servicio s on rs.servicioId = s.servicioId AND year(rs.RequiereServicioFHDeseada) = year(getdate())  AND month(rs.RequiereServicioFHDeseada)= month(getdate())   AND day(rs.RequiereServicioFHDeseada)= day(getdate())  and DATEPART(HOUR, rs.RequiereServicioFHDeseada)+4 = DATEPART(HOUR, getdate())  and sa.ServAsigFHInicio is null ";
                   da.SelectCommand = cmd;
                   da.Fill(ds);

                   var senderId2 = string.Format("id={0}", SenderIdFB);
                   var key = string.Format("key={0}", ServerKey);

                   foreach (DataRow myR in ds.Tables[0].Rows)
                   {


                       requiereServicioId = (myR["requiereServicioId"].ToString());
                       NombreCliente = (myR["NombreCliente"].ToString());

                       ServicioURLFoto = (myR["ServicioURLFoto"].ToString());
                       PersonaTokenId = (myR["PersonaTokenId"].ToString());
                       var sound = "";
                       var title = "Inicia servicio en la app.";
                       var body = "iniciar tu servicio  en la app antes de iniciar el servicio donde el cliente, caso contrario no correra el seguro ";//"Iniciar el servicio: RQ= " + Convert.ToString(requiereServicioId) + " A nombre de " + Convert.ToString(NombreCliente) ;
                       var vista = "Iniciar Servicio";
                       var jsonBody2 = JsonConvert.SerializeObject(body);
                       var to = PersonaTokenId;
                       var invasivo = "si";
                       var reqservid = requiereServicioId;
                       var reqservdes = "Iniciar Servicio";///esta campo va a body en el invasivo 
                       var servicioNombre = body;
                       var ServicioUrlFoto = "";
                       var action = "is";
                       //await EnviarNotificacionesAsync(to,sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto,vista,action);
                       var tag = "si";
                       var data2 = new
                       {
                           to,
                           notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag },
                           data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }
                       };
                       jsonBody2 = JsonConvert.SerializeObject(data2);

                       System.Diagnostics.Debug.WriteLine(jsonBody2);

                       using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                       {
                           httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                           httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                           httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                           using (var httpClient = new HttpClient())
                           {
                               var result = await httpClient.SendAsync(httpRequest);

                               if (result.IsSuccessStatusCode)
                               {
                                   // InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) +"INICIO SERVICIO" , DateTime.Now, to, reqservid, conexion);

                               }
                               else
                               {
                                   //  InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId, conexion);

                               }
                           }
                       }
                   }*/
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                // cnn.Close();
            }
        }
        public static async Task EnviarNotifcacion_a_todos()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataSet data = new DataSet();
            // SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            int cantidad = 0;
            string requiereServicioId = "";
            string NombreCliente = "";
            string ServicioURLFoto = "";
            string Hora = "";
            string PersonaTokenId = "";
            string ns = "";
            try
            {
                ///////////////////////////////////////////////////////////
                List<BE.Persona> lstPersona = new List<BE.Persona>();
                lstPersona = bcPersona.Listado_Personas_Anuncio(null);

                string tipo = "Anuncio";
                await EnviarNotificacionesAsyncCliente(lstPersona[0], tipo, "es", null, 0, 0, null);

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                // cnn.Close();
            }
        }
        private static async Task EnviarNotificacionesAsyncCliente(BE.Persona persona, string tipo, string lang, BE.RequiereServicio requiereServicio = null, decimal Importe = 0, decimal Calificacion = 0, string SiniestroId = null)
        {
            DataRow data = bcPersona.ListadoDatosNotificacionv2(tipo, lang);
            var title = data["title"].ToString();
            var body = data["body"].ToString();
            var vista = data["Fragment"].ToString();
            var BotonTexto = data["BotonTexto"].ToString();
            var jsonBody2 = JsonConvert.SerializeObject(body);
            bool Invasivo = false;
            string i = "";
            string foto = "";
            string des = "";
            string ReqId = "";
            string[] titulo;
            string[] cuerpo;
            string id = "";
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            switch (tipo)
            {
                case "ClienteReque":
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 1;
                    title = title + " " + "RQ: " + ReqId;
                    //////////////////
                    break;
                case "ClienteInicioServicio":
                    body = body + " " + requiereServicio.servicio.ServicioNombre;
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 7;
                    title = title + " " + "RQ: " + ReqId;
                    break;

                case "ClienteFinServicio":
                    titulo = body.Split('/');
                    body = "";
                    body = titulo[0] + Convert.ToString(Importe) + " " + Convert.ToString(persona.Ciudad.Region.pais.Moneda.MonedaNombre) + titulo[1];
                    Invasivo = true;
                    foto = requiereServicio.servicio.ServicioURLFoto;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 8;
                    title = title + " " + "RQ: " + ReqId;

                    break;
                case "ProveedorFinServicioFinal":
                    titulo = body.Split('/');
                    body = "";
                    title = "";
                    title = titulo[0] + Convert.ToString(Convert.ToInt32(Importe)) + " " + persona.Ciudad.Region.pais.Moneda.MonedaNombre;
                    body = titulo[1] + " " + Convert.ToString(Convert.ToInt32(Calificacion)) + " " + titulo[2] + " " + titulo[3];
                    Invasivo = true;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 6;
                    title = title + " " + "RQ: " + ReqId;
                    break;

                case "ClienteCotizacion":
                    cuerpo = body.Split('/');
                    body = "";
                    body = cuerpo[0];
                    notificacionPersona.ConceptoNotificacionId = 4;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + ReqId;

                    break;

                case "AproSiniestro":
                    Invasivo = true;
                    des = BotonTexto;

                    notificacionPersona.ConceptoNotificacionId = 10;
                    ReqId = SiniestroId;
                    title = title + " " + "S: " + ReqId;
                    break;
                case "RegSiniestro":


                    notificacionPersona.ConceptoNotificacionId = 10;
                    ReqId = SiniestroId;
                    title = title + " " + "S: " + ReqId;
                    break;
                case "ProveedorInicioServicio":

                    body = String.Format(body, persona);
                    Invasivo = true;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 5;
                    title = title + " " + "RQ: " + ReqId;
                    notificacionPersona.NotificacionPersonaFragment = "FragmentAddOffer";
                    break;
                case "Anuncio":

                    body = String.Format(body, persona);
                    Invasivo = true;
                    ReqId = "A1400";
                    notificacionPersona.ConceptoNotificacionId = 5;
                    title = title + " " + "RQ: " + ReqId;
                    notificacionPersona.NotificacionPersonaFragment = "FragmentAddOffer";
                    break;

            }

            var to = persona.PersonaTokenId;
            var senderId2 = string.Format("id={0}", SenderIdFB);
            var key = string.Format("key={0}", ServerKey);
            if (Invasivo == false)
            {

                i = "no";
            }
            else
            {
                i = "si";
            }
            var sound = "default";
            var invasivo = i;
            var reqservid = ReqId;
            var reqservdes = des;///esta campo va a body en el invasivo 
            var servicioNombre = body;
            var ServicioUrlFoto = foto;
            var action = "is";
            var tag = "no";
            if (invasivo == "si")
            {


                var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                jsonBody2 = JsonConvert.SerializeObject(data2);

                /*  tag = "si";
                  var data2 = new
                  {
                      to,
                      notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag },
                      data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }
                  };
                  jsonBody2 = JsonConvert.SerializeObject(data2);*/
            }
            else
            {
                var data2 = new { to, notification = new { title, body } };
                jsonBody2 = JsonConvert.SerializeObject(data2);

                /*  var data2 = new
                  {
                      to,
                      notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }
                  };
                  jsonBody2 = JsonConvert.SerializeObject(data2);*/

            }
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
            {
                httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                // httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.SendAsync(httpRequest);

                    if (result.IsSuccessStatusCode)
                    {
                        InsertarLogNotificacion("IS_" + ReqId, DateTime.Now, to, persona.PersonaId);

                    }
                    else
                    {
                        InsertarLogNotificacion("IS_" + ReqId, DateTime.Now, to, persona.PersonaId);

                    }
                }
            }





            /////////////////////////NOTIFICACION FIREBASE
            notificacionPersona.RequiereServicioId = ReqId;
            notificacionPersona.PersonaId = persona.PersonaId;
            notificacionPersona.TipoEstadoId = 1;
            notificacionPersona.NotificacionPersonaTitulo = title;
            notificacionPersona.NotificacionPersonaDescripcion = body;
            notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
            notificacionPersona.NotificacionPersonaFragment = vista;
            notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
            notificacionPersona.EstadoNotificacionId = 1;
            notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;

            if (notificacionPersona != null)
            {
                bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
            }
            /////////////////

            /////////////////////////////
            await NotificacionFirebase(Convert.ToInt32(persona.PersonaId), "recibido", "Notificaciones");



        }
        private static async Task EnviarNotificacionesAsyncV3(List<BE.Persona> lstPersonas, string tipo, string lang, BE.RequiereServicio requiereServicio = null, string persona = "", string RequiereServicioId = null)
        {
            DataRow data = bcPersona.ListadoDatosNotificacionv2(tipo, lang);
            var titleI = data["title"].ToString();
            var bodyI = data["body"].ToString();
            var vista = data["Fragment"].ToString();
            var jsonBody2 = JsonConvert.SerializeObject(bodyI);
            var BotonTexto = data["BotonTexto"].ToString();
            bool b = false;
            string detalledes = "";
            string inv = "";
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            switch (tipo)
            {
                case "Proveedor"://REQUIERE SERVICIO

                    detalledes = Convert.ToString(BotonTexto);
                    inv = "si";
                    break;
                case "CotizacionProveedor":


                    break;
                case "Adjudicado":

                    detalledes = BotonTexto;
                    inv = "si";
                    break;
                case "ClienteFinServicioFinal":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 2;
                    break;
                case "Pagado":

                    break;
                case "Conversacion":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 9;
                    break;
                case "ClienteCotizacion":

                    break;

                case "ConfSiniestro":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 10;

                    notificacionPersona.RequiereServicioId = RequiereServicioId;

                    break;
                case "RechSiniestro":
                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 10;
                    notificacionPersona.RequiereServicioId = RequiereServicioId;

                    break;

                case "Anuncio":
                    inv = "si";
                    RequiereServicioId = "A1400";
                    break;
            }

            foreach (var item in lstPersonas)
            {

                DataRow dataIdioma = bcPersona.ListadoDatosNotificacionv2(tipo, item.PersonaIdioma);
                var title = dataIdioma["title"].ToString();
                var body = String.Format(dataIdioma["body"].ToString(), persona);
                BotonTexto = dataIdioma["BotonTexto"].ToString();
                //Respuesta resp = new Respuesta();
                if (requiereServicio != null)
                {

                    requiereServicio = bcRequiereServicio.BuscarRequiereServicioxId(requiereServicio.RequiereServicioId, item.PersonaIdioma);
                    if (b == false)
                    {
                        title = title + " " + "RQ: " + Convert.ToString(requiereServicio.RequiereServicioId);
                        b = true;
                    }
                    notificacionPersona.RequiereServicioId = requiereServicio.RequiereServicioId;
                }
                notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;

                notificacionPersona.NotificacionPersonaTitulo = title;
                notificacionPersona.TipoEstadoId = 1;
                notificacionPersona.NotificacionPersonaDescripcion = body;
                notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
                notificacionPersona.NotificacionPersonaFragment = vista;
                notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
                notificacionPersona.PersonaId = item.PersonaId;

                if (tipo == "Proveedor")
                {


                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 2;




                }
                if (tipo == "CotizacionProveedor")
                {

                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 3;


                }
                if (tipo == "Adjudicado")
                {


                    notificacionPersona.EstadoNotificacionId = 1;
                    notificacionPersona.ConceptoNotificacionId = 5;


                }


                if ((tipo != "CotizacionProveedor"))//ENVIA LAS NOTIFICACIONES CLIENTE 
                {
                    var to = item.PersonaTokenId;
                    var senderId2 = string.Format("id={0}", SenderIdFB);
                    var key = string.Format("key={0}", ServerKey);
                    var sound = "default";
                    var invasivo = inv;
                    string id = "";
                    string sn = "";
                    string sf = "";
                    if (requiereServicio != null)
                    {
                        id = requiereServicio.RequiereServicioId;
                        sn = requiereServicio.servicio.ServicioNombre;
                        sf = requiereServicio.servicio.ServicioURLFoto;
                    }
                    else
                    {
                        id = RequiereServicioId;
                        sn = "";
                        sf = "";
                    }
                    var reqservid = id;
                    var servicioNombre = sn;
                    var ServicioUrlFoto = sf;

                    var reqservdes = BotonTexto;
                    var action = "";
                    //  var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };

                    var tag = "no";
                    if (inv == "si")
                    {
                        tag = "si";
                        var data2 = new
                        {
                            to,
                            notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag },
                            data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }
                        };
                        jsonBody2 = JsonConvert.SerializeObject(data2);
                    }
                    else
                    {
                        var data2 = new
                        {
                            to,
                            notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }
                        };
                        jsonBody2 = JsonConvert.SerializeObject(data2);
                    }

                    System.Diagnostics.Debug.WriteLine(jsonBody2);

                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                        httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                        httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                        using (var httpClient = new HttpClient())
                        {
                            var result = await httpClient.SendAsync(httpRequest);

                            if (result.IsSuccessStatusCode)
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, item.PersonaId);

                            }
                            else
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId);

                            }
                        }
                    }
                }

                if (notificacionPersona != null)
                {
                    bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
                    await NotificacionFirebase(Convert.ToInt32(item.PersonaId), "recibido", "Notificaciones");
                }

            }




        }
        private static async Task EnviarNotificacionesProveedores(List<BE.Persona> lstPersonas, string tipo, string lang, BE.RequiereServicio requiereServicio = null, string persona = "")
        {

            var titleI = ""; var bodyI = ""; var vista = ""; var jsonBody2 = ""; var BotonTexto = "";
            bool b = false; string detalledes = ""; string inv = "";
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            int importe = 0; string[] titulo;
            switch (tipo)
            {
                case "ProveedorV2"://REQUIERE SERVICIO

                    detalledes = Convert.ToString(BotonTexto);
                    inv = "si";
                    //   notificacionPersona.EstadoNotificacionId = 1;
                    // notificacionPersona.ConceptoNotificacionId = 2;
                    break;
            }

            foreach (var item in lstPersonas)
            {

                DataRow dataIdioma = bcPersona.ListadoDatosNotificacionv2(tipo, item.PersonaIdioma);
                var title = dataIdioma["title"].ToString();
                var body = ""; String.Format(dataIdioma["body"].ToString(), item.NombreCompleto());
                BotonTexto = dataIdioma["BotonTexto"].ToString();
                List<BE.RequiereServicio> lstrequiereServicio = new List<BE.RequiereServicio>();
                lstrequiereServicio = bcRequiereServicio.ListadoRequiereServicio_A_adjudicar(item.PersonaId, lang);

                importe = bcRequiereServicio.ImporteRequiereServicio(requiereServicio.RequiereServicioId, requiereServicio.servicio.servicioDetalleTipo);
                title = "RQ: " + Convert.ToString(requiereServicio.RequiereServicioId);
                titulo = dataIdioma["body"].ToString().Split('/');
                var to = item.PersonaTokenId;
                var senderId2 = string.Format("id={0}", SenderIdFB);
                var key = string.Format("key={0}", ServerKey);
                var sound = "default";
                var invasivo = inv;
                var reqservid = requiereServicio.RequiereServicioId;
                var servicioNombre = titulo[0] + requiereServicio.servicio.ServicioNombre + titulo[1] + requiereServicio.personaDireccion.PersonaDireccionDescripcion + "," + requiereServicio.RequiereServicioDescripcion + "." + titulo[2]
                + requiereServicio.RequiereServicioFHCaduca.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + titulo[3] + requiereServicio.RequiereServicioFHCaduca.ToString("hh:mm:ss", CultureInfo.InvariantCulture) + titulo[4] + string.Format("{0:N}", Convert.ToInt32(importe + requiereServicio.servicio.categoriaServicio.ciudad.configuracionCiudad.ConfiguracionCiudadValorSeguro)) + " " + requiereServicio.servicio.categoriaServicio.ciudad.Region.pais.Moneda.MonedaNombre;
                var ServicioUrlFoto = "";
                var reqservdes = BotonTexto;
                var action = "as";
                var OrganizationId = "2";
                var priority = "high";
                var subtitle = "Elementary School";

                var tag = "si";
                if (inv == "si")
                {
                    tag = "si";
                    var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);

                }


                System.Diagnostics.Debug.WriteLine(jsonBody2);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                    httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + dataIdioma["title"].ToString() + dataIdioma["body"].ToString(), DateTime.Now, to, item.PersonaId);

                        }
                        else
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId);

                        }
                    }
                }


            }




        }
        public static DataSet ListadoDatosNotificacionv2(string tipo, string lang)
        {
            DataRow dr = null;
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = "SELECT * FROM dbo.Notificacion with(nolock)  where Nombre =@tipo and lang =@lang; ";
                cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = Convert.ToString(tipo);
                cmd.Parameters.Add("@lang", SqlDbType.VarChar).Value = Convert.ToString(lang);
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return ds;
        }
        private static async Task<int> NotificacionFirebase(int PersonaId, string tipo, string opcion)
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
                BasePath = "https://service-web-258723.firebaseio.com/"
            };
            IFirebaseClient client;
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                //  client.GetAsync()

                FirebaseResponse response = await client.GetTaskAsync("Notificacion/" + PersonaId);
                var data = new Data
                {
                    PersonaId = Convert.ToString(PersonaId),
                    Post = 0,
                    Solicitudes = 0,
                    Servicios = 0,
                    Rendimientos = 0,
                    Notificaciones = 0,

                };

                if (response.Body != "null")
                {

                    Data obj = response.ResultAs<Data>();
                    data = obj;
                    if (tipo == "recibido")
                    {
                        switch (opcion)
                        {
                            case "Notificaciones":
                                data.Notificaciones = data.Notificaciones + 1;
                                break;
                            case "Post":
                                data.Post = data.Post + 1;
                                break;

                        }

                    }
                    else
                    {
                        if (tipo == "leido" || tipo == "eliminado")
                        {

                            switch (opcion)
                            {

                                case "Notificaciones":
                                    data.Notificaciones = 0;
                                    break;
                                case "Post":
                                    data.Post = 0;
                                    break;

                            }


                        }

                    }

                    FirebaseResponse responseUpdate = await client.UpdateTaskAsync("Notificacion/" + PersonaId, data);
                    Data result = responseUpdate.ResultAs<Data>();
                }
                else
                {
                    if (tipo == "recibido")
                    {
                        SetResponse responseInsert = await client.SetTaskAsync("Notificacion/" + PersonaId, data);
                        Data result = responseInsert.ResultAs<Data>();

                    }
                }




            }

            return 0;
        }
        private static async Task EnviarNotificacionesAsync(string to, string sound, string title, string body, string invasivo, string reqservid, string reqservdes, string servicioNombre, string ServicioUrlFoto, string vista, string action)
        {

            if (CantidadNotificacionesEnviadas(reqservid) == 0)
            {
                var senderId2 = string.Format("id={0}", SenderIdFB);
                var key = string.Format("key={0}", ServerKey);
                var jsonBody2 = JsonConvert.SerializeObject(body);

                if (invasivo == "si")
                {
                    var tag = "";
                    var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);
                }
                else
                {
                    var data2 = new { to, notification = new { title, body } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);
                }


                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                    httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            InsertarLogNotificacion(reqservid, DateTime.Now, to, 0);
                            Convert.ToString(result.IsSuccessStatusCode);
                        }
                        else
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, 0);
                            string code = Convert.ToString(result.IsSuccessStatusCode);
                        }
                    }
                }
            }

        }
        private static int CantidadNotificacionesEnviadas(string RequiereServicioId)
        {

            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            if (cnn.State != ConnectionState.Open) cnn.Open();
            SqlCommand commandlog = new SqlCommand();

            commandlog.Connection = cnn;

            commandlog.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = Convert.ToString(@RequiereServicioId);

            commandlog.CommandText = "select count(title) from Log_notificacion  where  title=@RequiereServicioId";
            int cantidad = Convert.ToInt32(commandlog.ExecuteScalar());
            commandlog.Parameters.Clear();
            cnn.Close();

            return cantidad;

        }
        private static void InsertarLogNotificacion(string result, DateTime fecha, string deviceTokens, decimal PersonaId)
        {
            SqlConnection conexion = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            if (conexion.State != ConnectionState.Open) conexion.Open();
            SqlCommand commandlog = new SqlCommand();

            commandlog.Connection = conexion;
            commandlog.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = Convert.ToString(result);
            commandlog.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
            commandlog.Parameters.Add("@deviceTokens", SqlDbType.VarChar, 800).Value = Convert.ToString(deviceTokens);
            commandlog.Parameters.Add("@PersonaId", SqlDbType.Decimal).Value = Convert.ToDecimal(PersonaId);

            commandlog.CommandText = "insert into [dbo].[Log_Notificacion](deviceTokens,title,sent,Fecha,PersonaId)values(@deviceTokens,@title,1,@fecha,@PersonaId)";
            commandlog.ExecuteNonQuery();
            commandlog.Parameters.Clear();
            conexion.Close();
        }

        /*    public static void SendMail()
            {
                try
                {
                    SmtpMail oMail = new SmtpMail("TryIt");

                    // Your Offic 365 email address
                    oMail.From = "webmaster@serviceweb.bo";
                    // Set recipient email address
                    oMail.To = "yohana.cespedes@banticsoftware.com";

                    // Set email subject
                    oMail.Subject = "test email from office 365 account";
                    // Set email body
                    oMail.TextBody = "this is a test email sent from c# project.";

                    // Your Office 365 SMTP server address,
                    // You should get it from outlook web access.
                    SmtpServer oServer = new SmtpServer("smtp.office365.com");

                    // user authentication should use your
                    // email address as the user name.
                    oServer.User = "webmaster@serviceweb.bo";
                    oServer.Password = "KInsino2000!";

                    // Set 587 port
                    oServer.Port = 587;

                    // detect SSL/TLS connection automatically
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                    Console.WriteLine("start to send email over SSL...");

                    SmtpClient oSmtp = new SmtpClient();
                    oSmtp.SendMail(oServer, oMail);

                    Console.WriteLine("email was sent successfully!");
                }
                catch (Exception ep)
                {
                    Console.WriteLine("failed to send email with the following error:");
                    Console.WriteLine(ep.Message);
                }


            }*/

        private static void EnvioCorreoCotizacionesServiceWeb()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            try
            {
                cmd.Connection = cnn;

                if (cnn.State != ConnectionState.Open) cnn.Open();


                cmd.CommandText = "select rs.requiereServicioId, p1.PersonaNombres+' '+ p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres+' '+ p.PersonaApellidos as personaProveedor,rsp.RequiereServicioProvCotizacion from requiereServicio rs inner join [dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId = rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join persona p on p.personaId = sp.personaId inner join persona p1 on p1.personaId = rs.personaId inner join servicio s on s.servicioId = rs.servicioId and rsp.StatusRequiereId = 2 and rs.EstadoReqServId = 1 ";
                // cmd.CommandText = "select 'A905' RequiereServicioId,'Yohana 'Persona ,'yohana_310188@hotmail.com' PersonaCorreo, 'Limpieza' ServicioNombre,'traslado de cajas mudanza y maletas, Desde Beni y 2do anillo, condominio la glorieta hasta la Ovidio Barbery entre 2do y 3er anillo, detras del colegio La Salle' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'martina' personaProveedor,115 RequiereServicioProvCotizacion union all select 'A904' RequiereServicioId,'Yohana 'Persona ,'yohana.cespedes@banticsoftware.com' PersonaCorreo, 'Limpieza' ServicioNombre,'descripcion' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'logitec' personaProveedor,115 RequiereServicioProvCotizacion union all select rs.requiereServicioId, p1.PersonaNombres + ' ' + p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres + ' ' + p.PersonaApellidos as personaProveedor,rsp.RequiereServicioProvCotizacion  from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId=rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId=sp.ServicioPersonaId inner join persona p on p.personaId=sp.personaId inner join persona p1 on p1.personaId=rs.personaId inner join servicio s on s.servicioId=rs.servicioId and rsp.StatusRequiereId=2 and rs.EstadoReqServId=1";
                da.SelectCommand = cmd;

                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (bcEnvioCorreo.ExisteId("CT_" + dr["RequiereServicioId"].ToString()) == 0)
                            {
                                if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                                {

                                    requiereServicioId = dr["RequiereServicioId"].ToString();
                                    string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                                    string cuerpo = "Buenas Tardes " + dr["Persona"].ToString() + ",tu servicio " + dr["ServicioNombre"].ToString() + " ha sido cotizado." + "<br>" + "<br>" + "Recuerda a un proveedor y adjudica." + "<br>" + "<br>" + "Descripcion: " + (dr["RequiereServicioDescripcion"].ToString() + "<br>" + "<br>");

                                    foreach (DataRow drProveedor in ds.Tables[0].Rows)
                                    {
                                        if (drProveedor["RequiereServicioId"].ToString() == requiereServicioId)
                                        {
                                            string precio = string.Format("{0:N2}", drProveedor["RequiereServicioProvCotizacion"].ToString());
                                            precio = precio.Substring(0, precio.Length - 2);
                                            cuerpoDetalle = cuerpoDetalle + " " + "Nombre del Proveedor: " + (drProveedor["personaProveedor"].ToString()) + ", Precio: " + precio + " Bs." + "<br>" + "<br>";
                                        }

                                    }
                                    cuerpo = cuerpo + cuerpoDetalle;
                                    string Body = text.Replace("MENSAJE", cuerpo);
                                    envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                                    envioCorreo.PersonaCorreo = (dr["PersonaCorreo"].ToString());
                                    envioCorreo.Subject1 = "Cotizaciones Service Web RQ " + requiereServicioId;
                                    envioCorreo.Body = Body;
                                    envioCorreo.Fecha = DateTime.Now;
                                    envioCorreo.TipoCorreo = "Cotizaciones";
                                    envioCorreo.Estado = "Pendiente";
                                    envioCorreo.Descripcion = "CT_" + dr["RequiereServicioId"].ToString();
                                    bcEnvioCorreo.Actualizar(ref envioCorreo, false);

                                }


                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        private static void EnvioCorreoAdjudicacionCliente()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            try
            {
                cmd.Connection = cnn;

                if (cnn.State != ConnectionState.Open) cnn.Open();


                cmd.CommandText = "select rs.RequiereServicioId,p.PersonaNombres+' '+ p.PersonaApellidos as persona,p.PersonaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,p1.PersonaNombres + ' ' + p1.PersonaApellidos as personaProveedor,p1.PersonaCorreo PersonaCorreoProveedor,rsp.RequiereServicioProvCotizacion from[dbo].[RequiereServicioProveedores] rsp with(nolock) inner join ServAsig sa with(nolock) on rsp.RequiereServicioId=sa.RequiereServicioId inner join RequiereServicio rs on rsp.RequiereServicioId= rs.RequiereServicioId inner join Persona p1 on sa.ProveedorId= p1.PersonaId inner join  Persona p on rs.personaId= p.personaId inner join servicioPersona sp on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join servicio s on s.servicioId = rs.servicioId where rsp.StatusRequiereId= 4 and sa.StatusServAsigId= 1 and rs.EstadoReqServId= 2 AND  ('AC_'+rs.RequiereServicioId)  NOT IN(select Descripcion RequiereServicioId from envioCorreo where Descripcion is not null )";
                // cmd.CommandText = "select 'A905' RequiereServicioId,'Yohana 'Persona ,'yohana_310188@hotmail.com' PersonaCorreo, 'Limpieza' ServicioNombre,'traslado de cajas mudanza y maletas, Desde Beni y 2do anillo, condominio la glorieta hasta la Ovidio Barbery entre 2do y 3er anillo, detras del colegio La Salle' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'martina' personaProveedor,115 RequiereServicioProvCotizacion union all select 'A904' RequiereServicioId,'Yohana 'Persona ,'yohana.cespedes@banticsoftware.com' PersonaCorreo, 'Limpieza' ServicioNombre,'descripcion' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'logitec' personaProveedor,115 RequiereServicioProvCotizacion union all select rs.requiereServicioId, p1.PersonaNombres + ' ' + p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres + ' ' + p.PersonaApellidos as personaProveedor,rsp.RequiereServicioProvCotizacion  from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId=rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId=sp.ServicioPersonaId inner join persona p on p.personaId=sp.personaId inner join persona p1 on p1.personaId=rs.personaId inner join servicio s on s.servicioId=rs.servicioId and rsp.StatusRequiereId=2 and rs.EstadoReqServId=1";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;

                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (bcEnvioCorreo.ExisteId("AC_" + dr["RequiereServicioId"].ToString()) == 0)
                            {
                                if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                                {

                                    requiereServicioId = dr["RequiereServicioId"].ToString();
                                    string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                                    string cuerpo = "Hola " + dr["Persona"].ToString() + ",felicidades has adjudicado tu servicio " + dr["ServicioNombre"].ToString() + "<br>" + "<br>" + "Descripción: " + dr["RequiereServicioDescripcion"].ToString() + "<br>" + "<br>";

                                    //   foreach (DataRow drProveedor in ds.Tables[0].Rows)
                                    //   {
                                    //     if (drProveedor["RequiereServicioId"].ToString() == requiereServicioId)
                                    //   {
                                    string precio = string.Format("{0:N2}", dr["RequiereServicioProvCotizacion"].ToString());
                                    precio = precio.Substring(0, precio.Length - 2);
                                    cuerpoDetalle = cuerpoDetalle + " " + "Nombre del Proveedor: " + (dr["personaProveedor"].ToString()) + ", Precio: " + precio + " Bs." + "<br>" + "<br>";
                                    // }

                                    // }
                                    cuerpo = cuerpo + cuerpoDetalle;
                                    string Body = text.Replace("MENSAJE", cuerpo);
                                    envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                                    envioCorreo.PersonaCorreo = (dr["PersonaCorreo"].ToString());
                                    envioCorreo.Subject1 = "Adjudicación Service Web RQ " + requiereServicioId;

                                    envioCorreo.Body = Body;
                                    envioCorreo.Fecha = DateTime.Now;
                                    envioCorreo.TipoCorreo = "AdjudicacionCliente";
                                    envioCorreo.Estado = "Pendiente";
                                    envioCorreo.Descripcion = "AC_" + dr["RequiereServicioId"].ToString();
                                    bcEnvioCorreo.Actualizar(ref envioCorreo, false);

                                }


                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        private static void EnvioCorreoAdjudicacionProveedor()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            try
            {
                cmd.Connection = cnn;
                if (cnn.State != ConnectionState.Open) cnn.Open();
                cmd.CommandText = "select rs.RequiereServicioId,p.PersonaNombres+' '+ p.PersonaApellidos as persona,p.PersonaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,p1.PersonaNombres + ' ' + p1.PersonaApellidos as personaProveedor,p1.PersonaCorreo PersonaCorreoProveedor,rsp.RequiereServicioProvCotizacion from[dbo].[RequiereServicioProveedores] rsp with(nolock) inner join ServAsig sa with(nolock) on rsp.RequiereServicioId=sa.RequiereServicioId inner join RequiereServicio rs with(nolock) on rsp.RequiereServicioId= rs.RequiereServicioId inner join Persona p1  with(nolock) on sa.ProveedorId= p1.PersonaId inner join  Persona p  with(nolock) on rs.personaId= p.personaId inner join servicioPersona sp   with(nolock) on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join servicio s  with(nolock) on s.servicioId = rs.servicioId where rsp.StatusRequiereId= 4 and sa.StatusServAsigId= 1 and rs.EstadoReqServId= 2 AND  ('AP_'+rs.RequiereServicioId)  NOT IN(select Descripcion RequiereServicioId from envioCorreo where Descripcion is not null ) ";
                // cmd.CommandText = "select 'A905' RequiereServicioId,'Yohana 'Persona ,'yohana_310188@hotmail.com' PersonaCorreo, 'Limpieza' ServicioNombre,'traslado de cajas mudanza y maletas, Desde Beni y 2do anillo, condominio la glorieta hasta la Ovidio Barbery entre 2do y 3er anillo, detras del colegio La Salle' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'martina' personaProveedor,115 RequiereServicioProvCotizacion union all select 'A904' RequiereServicioId,'Yohana 'Persona ,'yohana.cespedes@banticsoftware.com' PersonaCorreo, 'Limpieza' ServicioNombre,'descripcion' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'logitec' personaProveedor,115 RequiereServicioProvCotizacion union all select rs.requiereServicioId, p1.PersonaNombres + ' ' + p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres + ' ' + p.PersonaApellidos as personaProveedor,rsp.RequiereServicioProvCotizacion  from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId=rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId=sp.ServicioPersonaId inner join persona p on p.personaId=sp.personaId inner join persona p1 on p1.personaId=rs.personaId inner join servicio s on s.servicioId=rs.servicioId and rsp.StatusRequiereId=2 and rs.EstadoReqServId=1";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (bcEnvioCorreo.ExisteId("AP_" + dr["RequiereServicioId"].ToString()) == 0)
                            {
                                if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                                {

                                    requiereServicioId = dr["RequiereServicioId"].ToString();
                                    string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                                    string cuerpo = "Hola " + dr["personaProveedor"].ToString() + ",te han adjudicado un servicio: " + dr["ServicioNombre"].ToString() + "<br>" + "<br>" + "Descripción: " + dr["RequiereServicioDescripcion"].ToString() + "<br>" + "<br>";

                                    //   foreach (DataRow drProveedor in ds.Tables[0].Rows)
                                    //   {
                                    //     if (drProveedor["RequiereServicioId"].ToString() == requiereServicioId)
                                    //   {
                                    string precio = string.Format("{0:N2}", dr["RequiereServicioProvCotizacion"].ToString());
                                    precio = precio.Substring(0, precio.Length - 2);
                                    cuerpoDetalle = " Precio: " + precio + " Bs." + "<br>" + "<br>";
                                    // }

                                    // }
                                    cuerpo = cuerpo + cuerpoDetalle;
                                    string Body = text.Replace("MENSAJE", cuerpo);
                                    envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                                    envioCorreo.PersonaCorreo = (dr["PersonaCorreoProveedor"].ToString());
                                    envioCorreo.Subject1 = "Adjudicación Service Web RQ " + requiereServicioId;
                                    envioCorreo.Body = Body;
                                    envioCorreo.Fecha = DateTime.Now;
                                    envioCorreo.TipoCorreo = "AdjudicacionProveedor";
                                    envioCorreo.Estado = "Pendiente";
                                    envioCorreo.Descripcion = "AP_" + dr["RequiereServicioId"].ToString();
                                    bcEnvioCorreo.Actualizar(ref envioCorreo, false);

                                }


                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        private static void EnvioCorreoFinServicioCliente()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            try
            {
                cmd.Connection = cnn;

                if (cnn.State != ConnectionState.Open) cnn.Open();


                cmd.CommandText = " select* from(select rs.RequiereServicioId, p.PersonaNombres+' ' + p.PersonaApellidos as persona,p.PersonaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,p1.PersonaNombres + ' ' + p1.PersonaApellidos " +
                    "                      as personaProveedor,p1.PersonaCorreo PersonaCorreoProveedor, rsp.RequiereServicioProvCotizacion,rs.RequiereServicioFHDeseada, " +
                    "                    pd.PersonaDireccionTitulo,pd.PersonaDireccionDescripcion,sa.ServAsigCostoTotal ,'FSC_' + rs.RequiereServicioId as IdCorreo " +
                    "                    from[dbo].[RequiereServicioProveedores] rsp with(nolock) inner join ServAsig sa with(nolock) on rsp.RequiereServicioId = sa.RequiereServicioId " +
                    "    inner join RequiereServicio rs on rsp.RequiereServicioId = rs.RequiereServicioId inner join Persona p1 " +
                    "                    on sa.ProveedorId = p1.PersonaId inner join  Persona p on rs.personaId = p.personaId inner join servicioPersona sp " +
                    "                    on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join servicio s on s.servicioId = rs.servicioId " +
                    "                    inner join PersonaDireccion pd on rs.PersonaDireccionId = pd.PersonaDireccionId and rs.PersonaId = pd.PersonaId " +
                    "                    where rsp.StatusRequiereId = 4 and sa.StatusServAsigId = 3 and rs.EstadoReqServId = 2     AND sa.ServAsigFHPago is not null " +
                    "					)c1             " +
                    "        where C1.IdCorreo not in (select Descripcion from envioCorreo WHERE Descripcion LIKE '%FSC_%')";
                // cmd.CommandText = "select 'A905' RequiereServicioId,'Yohana 'Persona ,'yohana_310188@hotmail.com' PersonaCorreo, 'Limpieza' ServicioNombre,'traslado de cajas mudanza y maletas, Desde Beni y 2do anillo, condominio la glorieta hasta la Ovidio Barbery entre 2do y 3er anillo, detras del colegio La Salle' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'martina' personaProveedor,115 RequiereServicioProvCotizacion union all select 'A904' RequiereServicioId,'Yohana 'Persona ,'yohana.cespedes@banticsoftware.com' PersonaCorreo, 'Limpieza' ServicioNombre,'descripcion' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'logitec' personaProveedor,115 RequiereServicioProvCotizacion union all select rs.requiereServicioId, p1.PersonaNombres + ' ' + p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres + ' ' + p.PersonaApellidos as personaProveedor,rsp.RequiereServicioProvCotizacion  from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId=rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId=sp.ServicioPersonaId inner join persona p on p.personaId=sp.personaId inner join persona p1 on p1.personaId=rs.personaId inner join servicio s on s.servicioId=rs.servicioId and rsp.StatusRequiereId=2 and rs.EstadoReqServId=1";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;

                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (bcEnvioCorreo.ExisteId("FSC_" + dr["RequiereServicioId"].ToString()) == 0)
                            {
                                if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                                {

                                    requiereServicioId = dr["RequiereServicioId"].ToString();
                                    string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                                    string precio = string.Format("{0:N2}", dr["ServAsigCostoTotal"].ToString());
                                    precio = precio.Substring(0, precio.Length - 2);
                                    string cuerpo = "Hola " + dr["persona"].ToString() + ",tu servicio ha concluido " + "<br>" + "<br>" +
                                        "-Servicio: " + dr["ServicioNombre"].ToString() + "<br>" +
                                        "-Proveedor: " + dr["personaProveedor"].ToString() + "<br>" +
                                        "-Descripción: " + dr["RequiereServicioDescripcion"].ToString() + "<br>" +
                                        "-Fecha y Hora: " + dr["RequiereServicioFHDeseada"].ToString() + "<br>" +
                                         "-Lugar: " + dr["PersonaDireccionTitulo"].ToString() + "<br>" +
                                         "-Dirección: " + dr["PersonaDireccionDescripcion"].ToString() + "<br>" +
                                          "-Costo del Servicio:" + precio + " Bs." + "<br>" + "<br>" +
                                          "Muchas Gracias, ha sido un placer poder servirte.";

                                    //   foreach (DataRow drProveedor in ds.Tables[0].Rows)
                                    //   {
                                    //     if (drProveedor["RequiereServicioId"].ToString() == requiereServicioId)
                                    //   {
                                    //  string precio = string.Format("{0:N2}", dr["RequiereServicioProvCotizacion"].ToString());
                                    //precio = precio.Substring(0, precio.Length - 2);
                                    //cuerpoDetalle = " Precio: " + precio + " Bs." + "<br>" + "<br>";
                                    // }

                                    // }
                                    cuerpo = cuerpo + cuerpoDetalle;
                                    string Body = text.Replace("MENSAJE", cuerpo);
                                    envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                                    envioCorreo.PersonaCorreo = (dr["PersonaCorreo"].ToString());
                                    envioCorreo.Subject1 = "Finalización Servicio Service Web RQ " + requiereServicioId;
                                    envioCorreo.Body = Body;
                                    envioCorreo.Fecha = DateTime.Now;
                                    envioCorreo.TipoCorreo = "FinServicioCliente";
                                    envioCorreo.Estado = "Pendiente";
                                    envioCorreo.Descripcion = "FSC_" + dr["RequiereServicioId"].ToString();
                                    bcEnvioCorreo.Actualizar(ref envioCorreo, false);

                                }


                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        private static void EnvioCorreoFinServicioProveedor()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            DataSet ds2 = new DataSet(); SqlDataAdapter da1 = new SqlDataAdapter();
            SqlDataAdapter da2 = new SqlDataAdapter();
            try
            {
                cmd.Connection = cnn;

                if (cnn.State != ConnectionState.Open) cnn.Open();


                cmd.CommandText = "	SELECT  * FROM (                    select rs.RequiereServicioId,p.PersonaNombres + ' ' + p.PersonaApellidos " +
                    "                    as persona,p.PersonaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,p1.PersonaNombres + ' ' + p1.PersonaApellidos " +
                    "                    as personaProveedor,p1.PersonaCorreo PersonaCorreoProveedor, rsp.RequiereServicioProvCotizacion,rs.RequiereServicioFHDeseada, " +
                    "                    pd.PersonaDireccionTitulo,pd.PersonaDireccionDescripcion,sa.ServAsigCostoTotal,SA.ServAsigFHFin,SA.ServAsigId,'FSP_' + rs.RequiereServicioId as IdCorreo " +
                    "                    from[dbo].[RequiereServicioProveedores] rsp with(nolock) inner join ServAsig sa with(nolock) " +
                    "                     on rsp.RequiereServicioId = sa.RequiereServicioId inner join RequiereServicio rs on rsp.RequiereServicioId = rs.RequiereServicioId " +
                    "                  inner join Persona p1 on sa.ProveedorId = p1.PersonaId inner join  Persona p on rs.personaId = p.personaId inner join servicioPersona sp " +
                    "                   on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join servicio s on s.servicioId = rs.servicioId inner join PersonaDireccion pd " +
                    "                    on rs.PersonaDireccionId = pd.PersonaDireccionId and rs.PersonaId = pd.PersonaId " +
                    "                    where rsp.StatusRequiereId = 4 and sa.StatusServAsigId = 3 and rs.EstadoReqServId = 2     AND sa.ServAsigFHPago is not null " +
                    "					)C1 " +
                    "                       where C1.IdCorreo not in (select Descripcion from envioCorreo WHERE Descripcion LIKE '%FSP_%')";
                // cmd.CommandText = "select 'A905' RequiereServicioId,'Yohana 'Persona ,'yohana_310188@hotmail.com' PersonaCorreo, 'Limpieza' ServicioNombre,'traslado de cajas mudanza y maletas, Desde Beni y 2do anillo, condominio la glorieta hasta la Ovidio Barbery entre 2do y 3er anillo, detras del colegio La Salle' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'martina' personaProveedor,115 RequiereServicioProvCotizacion union all select 'A904' RequiereServicioId,'Yohana 'Persona ,'yohana.cespedes@banticsoftware.com' PersonaCorreo, 'Limpieza' ServicioNombre,'descripcion' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'logitec' personaProveedor,115 RequiereServicioProvCotizacion union all select rs.requiereServicioId, p1.PersonaNombres + ' ' + p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres + ' ' + p.PersonaApellidos as personaProveedor,rsp.RequiereServicioProvCotizacion  from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId=rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId=sp.ServicioPersonaId inner join persona p on p.personaId=sp.personaId inner join persona p1 on p1.personaId=rs.personaId inner join servicio s on s.servicioId=rs.servicioId and rsp.StatusRequiereId=2 and rs.EstadoReqServId=1";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;

                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (bcEnvioCorreo.ExisteId("FSP_" + dr["RequiereServicioId"].ToString()) == 0)
                            {
                                if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                                {
                                    /////////////////////////////////////////////

                                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM ServAsigCosto WHERE ServAsigId=@ServAsigId", cnn);
                                    cmd1.Parameters.Add("@ServAsigId", SqlDbType.VarChar).Value = dr["ServAsigId"].ToString();
                                    da2.SelectCommand = cmd1;
                                    da2.Fill(ds2);

                                    /////////////////////////////////////////////////
                                    requiereServicioId = dr["RequiereServicioId"].ToString();
                                    string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                                    string precioGanado = Convert.ToString(ds2.Tables[0].Rows[0]["ServAsigCostoValor"]);
                                    precioGanado = precioGanado.Substring(0, precioGanado.Length - 2);
                                    string Insumos = Convert.ToString(ds2.Tables[0].Rows[1]["ServAsigCostoValor"]);
                                    Insumos = Insumos.Substring(0, Insumos.Length - 2);
                                    string ComSw = Convert.ToString(ds2.Tables[0].Rows[2]["ServAsigCostoValor"]);
                                    ComSw = ComSw.Substring(0, ComSw.Length - 2);
                                    int valorS = Convert.ToInt32(ds2.Tables[0].Rows[3]["ServAsigCostoValor"]);
                                    int valorS1 = Convert.ToInt32(ds2.Tables[0].Rows[4]["ServAsigCostoValor"]);
                                    string valorSeguro = Convert.ToString(valorS + valorS1);
                                    // valorSeguro = valorSeguro.Substring(0, valorSeguro.Length - 2);
                                    string cuerpo = "Hola " + dr["personaProveedor"].ToString() + ",tu servicio ha concluido " + "<br>" + "<br>" +
                                        "-Servicio: " + dr["ServicioNombre"].ToString() + "<br>" +
                                        "-Cliente: " + dr["persona"].ToString() + "<br>" +
                                        "-Descripción: " + dr["RequiereServicioDescripcion"].ToString() + "<br>" +
                                        "-Fecha y Hora: " + dr["ServAsigFHFin"].ToString() + "<br>" +
                                         "-Lugar: " + dr["PersonaDireccionTitulo"].ToString() + "<br>" +
                                         "-Dirección: " + dr["PersonaDireccionDescripcion"].ToString() + "<br>" +
                                          "-Valor Ganado: " + precioGanado + " Bs." + "<br>" +
                                           "-Insumos: " + Insumos + " Bs." + "<br>" +
                                            "-Comisión a Service Web: " + ComSw + " Bs." + "<br>" +
                                               "-Seguro: " + valorSeguro + " Bs." + "<br>" + "<br>" +
                                            "Recuerda pagar la comisión a Service Web, que tengas un excelente día.";

                                    //   foreach (DataRow drProveedor in ds.Tables[0].Rows)
                                    //   {
                                    //     if (drProveedor["RequiereServicioId"].ToString() == requiereServicioId)
                                    //   {
                                    //  string precio = string.Format("{0:N2}", dr["RequiereServicioProvCotizacion"].ToString());
                                    //precio = precio.Substring(0, precio.Length - 2);
                                    //cuerpoDetalle = " Precio: " + precio + " Bs." + "<br>" + "<br>";
                                    // }

                                    // }
                                    cuerpo = cuerpo + cuerpoDetalle;
                                    string Body = text.Replace("MENSAJE", cuerpo);
                                    envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                                    envioCorreo.PersonaCorreo = (dr["PersonaCorreoProveedor"].ToString());
                                    envioCorreo.Subject1 = "Finalización Servicio Service Web RQ " + requiereServicioId;
                                    envioCorreo.Body = Body;
                                    envioCorreo.Fecha = DateTime.Now;
                                    envioCorreo.TipoCorreo = "FinServicioProveedor";
                                    envioCorreo.Estado = "Pendiente";
                                    envioCorreo.Descripcion = "FSP_" + dr["RequiereServicioId"].ToString();
                                    bcEnvioCorreo.Actualizar(ref envioCorreo, false);

                                }


                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        private static void EnvioCorreoServiceWebRegistroPersona()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            BE.envioCorreo envioCorreo = new BE.envioCorreo();
            DataSet ds2 = new DataSet(); SqlDataAdapter da1 = new SqlDataAdapter();
            SqlDataAdapter da2 = new SqlDataAdapter();
            try
            {


                cmd.Connection = cnn;

                if (cnn.State != ConnectionState.Open) cnn.Open();


                cmd.CommandText = "	SELECT  * FROM (                    select rs.RequiereServicioId,p.PersonaNombres + ' ' + p.PersonaApellidos " +
                    "                    as persona,p.PersonaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,p1.PersonaNombres + ' ' + p1.PersonaApellidos " +
                    "                    as personaProveedor,p1.PersonaCorreo PersonaCorreoProveedor, rsp.RequiereServicioProvCotizacion,rs.RequiereServicioFHDeseada, " +
                    "                    pd.PersonaDireccionTitulo,pd.PersonaDireccionDescripcion,sa.ServAsigCostoTotal,SA.ServAsigFHFin,SA.ServAsigId,'FSP_' + rs.RequiereServicioId as IdCorreo " +
                    "                    from[dbo].[RequiereServicioProveedores] rsp with(nolock) inner join ServAsig sa with(nolock) " +
                    "                     on rsp.RequiereServicioId = sa.RequiereServicioId inner join RequiereServicio rs on rsp.RequiereServicioId = rs.RequiereServicioId " +
                    "                  inner join Persona p1 on sa.ProveedorId = p1.PersonaId inner join  Persona p on rs.personaId = p.personaId inner join servicioPersona sp " +
                    "                   on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join servicio s on s.servicioId = rs.servicioId inner join PersonaDireccion pd " +
                    "                    on rs.PersonaDireccionId = pd.PersonaDireccionId and rs.PersonaId = pd.PersonaId " +
                    "                    where rsp.StatusRequiereId = 4 and sa.StatusServAsigId = 3 and rs.EstadoReqServId = 2     AND sa.ServAsigFHPago is not null " +
                    "					)C1 " +
                    "                       where C1.IdCorreo not in (select Descripcion from envioCorreo WHERE Descripcion LIKE '%FSP_%')";
                // cmd.CommandText = "select 'A905' RequiereServicioId,'Yohana 'Persona ,'yohana_310188@hotmail.com' PersonaCorreo, 'Limpieza' ServicioNombre,'traslado de cajas mudanza y maletas, Desde Beni y 2do anillo, condominio la glorieta hasta la Ovidio Barbery entre 2do y 3er anillo, detras del colegio La Salle' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'martina' personaProveedor,115 RequiereServicioProvCotizacion union all select 'A904' RequiereServicioId,'Yohana 'Persona ,'yohana.cespedes@banticsoftware.com' PersonaCorreo, 'Limpieza' ServicioNombre,'descripcion' RequiereServicioDescripcion,'2020-02-10 11:45:29' RequiereServicioFechaHoraReq,'logitec' personaProveedor,115 RequiereServicioProvCotizacion union all select rs.requiereServicioId, p1.PersonaNombres + ' ' + p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres + ' ' + p.PersonaApellidos as personaProveedor,rsp.RequiereServicioProvCotizacion  from requiereServicio rs inner join[dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId=rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId=sp.ServicioPersonaId inner join persona p on p.personaId=sp.personaId inner join persona p1 on p1.personaId=rs.personaId inner join servicio s on s.servicioId=rs.servicioId and rsp.StatusRequiereId=2 and rs.EstadoReqServId=1";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;

                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (bcEnvioCorreo.ExisteId("FSP_" + dr["RequiereServicioId"].ToString()) == 0)
                            {
                                if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                                {
                                    /////////////////////////////////////////////

                                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM ServAsigCosto WHERE ServAsigId=@ServAsigId", cnn);
                                    cmd1.Parameters.Add("@ServAsigId", SqlDbType.VarChar).Value = dr["ServAsigId"].ToString();
                                    da2.SelectCommand = cmd1;
                                    da2.Fill(ds2);

                                    /////////////////////////////////////////////////
                                    requiereServicioId = dr["RequiereServicioId"].ToString();
                                    string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                                    string precioGanado = Convert.ToString(ds2.Tables[0].Rows[0]["ServAsigCostoValor"]);
                                    precioGanado = precioGanado.Substring(0, precioGanado.Length - 2);
                                    string Insumos = Convert.ToString(ds2.Tables[0].Rows[1]["ServAsigCostoValor"]);
                                    Insumos = Insumos.Substring(0, Insumos.Length - 2);
                                    string ComSw = Convert.ToString(ds2.Tables[0].Rows[2]["ServAsigCostoValor"]);
                                    ComSw = ComSw.Substring(0, ComSw.Length - 2);
                                    int valorS = Convert.ToInt32(ds2.Tables[0].Rows[3]["ServAsigCostoValor"]);
                                    int valorS1 = Convert.ToInt32(ds2.Tables[0].Rows[4]["ServAsigCostoValor"]);
                                    string valorSeguro = Convert.ToString(valorS + valorS1);
                                    // valorSeguro = valorSeguro.Substring(0, valorSeguro.Length - 2);
                                    string cuerpo = "Hola " + dr["personaProveedor"].ToString() + ",tu servicio ha concluido " + "<br>" + "<br>" +
                                        "-Servicio: " + dr["ServicioNombre"].ToString() + "<br>" +
                                        "-Cliente: " + dr["persona"].ToString() + "<br>" +
                                        "-Descripción: " + dr["RequiereServicioDescripcion"].ToString() + "<br>" +
                                        "-Fecha y Hora: " + dr["ServAsigFHFin"].ToString() + "<br>" +
                                         "-Lugar: " + dr["PersonaDireccionTitulo"].ToString() + "<br>" +
                                         "-Dirección: " + dr["PersonaDireccionDescripcion"].ToString() + "<br>" +
                                          "-Valor Ganado: " + precioGanado + " Bs." + "<br>" +
                                           "-Insumos: " + Insumos + " Bs." + "<br>" +
                                            "-Comisión a Service Web: " + ComSw + " Bs." + "<br>" +
                                               "-Seguro: " + valorSeguro + " Bs." + "<br>" + "<br>" +
                                            "Recuerda pagar la comisión a Service Web, que tengas un excelente día.";

                                    //   foreach (DataRow drProveedor in ds.Tables[0].Rows)
                                    //   {
                                    //     if (drProveedor["RequiereServicioId"].ToString() == requiereServicioId)
                                    //   {
                                    //  string precio = string.Format("{0:N2}", dr["RequiereServicioProvCotizacion"].ToString());
                                    //precio = precio.Substring(0, precio.Length - 2);
                                    //cuerpoDetalle = " Precio: " + precio + " Bs." + "<br>" + "<br>";
                                    // }

                                    // }
                                    cuerpo = cuerpo + cuerpoDetalle;
                                    string Body = text.Replace("MENSAJE", cuerpo);
                                    envioCorreo.TipoEstado = BE.TipoEstado.Insertar;
                                    envioCorreo.PersonaCorreo = (dr["PersonaCorreoProveedor"].ToString());
                                    envioCorreo.Subject1 = "Finalización Servicio Service Web RQ " + requiereServicioId;
                                    envioCorreo.Body = Body;
                                    envioCorreo.Fecha = DateTime.Now;
                                    envioCorreo.TipoCorreo = "FinServicioProveedor";
                                    envioCorreo.Estado = "Pendiente";
                                    envioCorreo.Descripcion = "FSP_" + dr["RequiereServicioId"].ToString();
                                    bcEnvioCorreo.Actualizar(ref envioCorreo, false);

                                }


                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        private static void EnvioCorreoServiceWeb(string TipoCorreo)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            string requiereServicioId = "";
            string cuerpoDetalle = "";

            try
            {

                List<BE.envioCorreo> lstenvioCorreo = bcEnvioCorreo.ListadoEnvioCorreo(TipoCorreo, null);
                foreach (var item in lstenvioCorreo)
                {
                    enviarCorreo(item.PersonaCorreo, item.Body);
                    item.Estado = "Enviado";
                    bcEnvioCorreo.actualizarEnvioCorreo(item);
                }

            }
            catch (Exception ex)
            {


                int i = 0;

            }
            finally
            {

            }
        }
        private static void enviarCorreo(string personaCorreo, string Body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail = new MailMessage();

                mail.To.Add(personaCorreo);
                mail.From = new MailAddress("webmaster@serviceweb.bo");
                mail.Subject = "Notificaciones Service Web";//dr["Asunto"].ToString();
                mail.Body = Body;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    //  Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                    Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!"),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
                    if (cnn.State != ConnectionState.Open) cnn.Open();
                    SqlCommand commandlog = new SqlCommand();

                    commandlog.Connection = cnn;

                    commandlog.Parameters.Add("@personaCorreo", SqlDbType.VarChar).Value = Convert.ToString(personaCorreo);

                    commandlog.CommandText = "update envioCorreo set Estado = 'CorreoInvalido' where PersonaCorreo = @personaCorreo";
                    int cantidad = Convert.ToInt32(commandlog.ExecuteScalar());
                    commandlog.Parameters.Clear();
                    cnn.Close();

                }


            }
            catch (Exception ex)
            {

                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
                if (cnn.State != ConnectionState.Open) cnn.Open();
                SqlCommand commandlog = new SqlCommand();

                commandlog.Connection = cnn;

                commandlog.Parameters.Add("@personaCorreo", SqlDbType.VarChar).Value = Convert.ToString(personaCorreo);

                commandlog.CommandText = "update envioCorreo set Estado = 'CorreoInvalido' where PersonaCorreo = @personaCorreo";
                int cantidad = Convert.ToInt32(commandlog.ExecuteScalar());
                commandlog.Parameters.Clear();
                cnn.Close();

            }



        }
        private static void EnvioCorreoAdjudicadoServiceWeb()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            MailMessage mail = new MailMessage();
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            try
            {
                cmd.Connection = cnn;

                if (cnn.State != ConnectionState.Open) cnn.Open();


                //  cmd.CommandText = "select rs.requiereServicioId, p1.PersonaNombres+' '+ p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres+' '+ p.PersonaApellidos as persona,rsp.RequiereServicioProvCotizacion from requiereServicio rs inner join [dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId = rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join persona p on p.personaId = sp.personaId inner join persona p1 on p1.personaId = rs.personaId inner join servicio s on s.servicioId = rs.servicioId and rsp.StatusRequiereId = 2 and rs.EstadoReqServId = 1 ";
                cmd.CommandText = "";
                da.SelectCommand = cmd;

                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (dr["RequiereServicioId"].ToString() != requiereServicioId)
                            {
                                requiereServicioId = dr["RequiereServicioId"].ToString();
                                mail = new MailMessage();
                                string PersonaCoreeo = (dr["PersonaCorreo"].ToString());
                                mail.To.Add(dr["PersonaCorreo"].ToString());
                                //mail.To.Add("mauricio.banegas@banticsoftware.com");

                                mail.From = new MailAddress("webmaster@serviceweb.bo");
                                mail.Subject = "Notificaciones Service Web";//dr["Asunto"].ToString();

                                string Body;
                                string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                                string cuerpo = "Buenas Tardes " + dr["Persona"].ToString() + "." + "<br>" + "Tu servicio " + dr["ServicioNombre"].ToString() + " ha sido cotizado." + "<br>" + "Recuerda a un proveedor y adjudica." + "<br>" + "Descripcion: " + (dr["RequiereServicioDescripcion"].ToString());
                                foreach (DataRow drProveedor in ds.Tables[0].Rows)
                                {
                                    if (drProveedor["RequiereServicioId"].ToString() == requiereServicioId)
                                    {
                                        cuerpoDetalle = cuerpoDetalle + " " + "Nombre del Proveedor: " + (drProveedor["personaProveedor"].ToString()) + "Precio: " + (drProveedor["RequiereServicioProvCotizacion"].ToString()) + "Bs." + "<br>";
                                    }

                                }
                                cuerpo = cuerpo + cuerpoDetalle;
                                Body = text.Replace("MENSAJE", cuerpo);
                                mail.Body = Body;

                                mail.IsBodyHtml = true;

                                var smtp = new SmtpClient
                                {
                                    Host = "smtp.office365.com",
                                    Port = 587,
                                    EnableSsl = true,
                                    UseDefaultCredentials = false,
                                    Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!"),
                                    DeliveryMethod = SmtpDeliveryMethod.Network,
                                };

                                if (cnn.State != ConnectionState.Open) cnn.Open();

                                smtp.Send(mail);

                                /* cmd.CommandText = "update EnvioCorreos set Estado='ENVIADO', Enviado=@Enviado where ProcesoId=@ProcesoId and ActividadId=@ActividadId and TipoCorreo=@TipoCorreo and Para=@Para";
                                 cmd.Parameters.Add("@Enviado", SqlDbType.DateTime).Value = DateTime.Now;
                                 cmd.Parameters.Add("@ProcesoId", SqlDbType.Int).Value = dr["ProcesoId"].ToString();
                                 cmd.Parameters.Add("@ActividadId", SqlDbType.Int).Value = dr["ActividadId"].ToString();
                                 cmd.Parameters.Add("@TipoCorreo", SqlDbType.VarChar, 50).Value = dr["TipoCorreo"].ToString();
                                 cmd.Parameters.Add("@Para", SqlDbType.VarChar, 100).Value = dr["Para"].ToString();

                                 cmd.ExecuteNonQuery();
                                 cmd.Parameters.Clear();*/

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }
        public static void InsercionMediBook()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            string ciudad = "";
            string CiudadId = "";
            DateTime fechaNac = DateTime.Now;
            string fecha_nacimiento = "";
            string expediciones = "";
            try
            {
                cnn.Open();
                cmd.Connection = cnn;
                ///////////////////////////////////////////////7
                ///  ////////////////////////////////////////////
                MediBook.wsComelecServerPortTypeClient mb = new MediBook.wsComelecServerPortTypeClient();
                MediBook.WsTransaccion data = new MediBook.WsTransaccion();
                MediBook.RespTransaccion resp = new MediBook.RespTransaccion();
                SqlDataAdapter da1;

                da1 = new SqlDataAdapter("select rs.requiereServicioId,p.personaNombres,p.personaApellidos,p.personaCorreo,p.PersonaTelefono,p.personaDNI,td.TipoDocumentoAbreviatura," +
                    "p.CiudadId,p.PersonaFechaNacimiento from requiereServicio  rs inner join persona p on rs.personaId = p.personaId inner join TipoDocumento td on p.TipoDocumentoId = td.tipoDocumentoId where  " +
                    "rs.servicioId in (select ServicioId from servicioPersona where personaId in (select PersonaId from persona where personaNombres = 'MediBook' and PersonaCorreo = 'medibookbolivia@gmail.com')) " +
                    "and rs.EstadoReqServId not in (4, 3,5) ", cnn);
                da1.Fill(ds);
                foreach (DataRow drClien in ds.Tables[0].Rows)
                {
                    expediciones = drClien["CiudadId"].ToString();
                    switch (CiudadId)
                    {
                        case "2":
                            expediciones = "SANTA CRUZ";
                            break;
                        case "15":
                            expediciones = "LA PAZ";
                            break;
                        case "9":
                            expediciones = "COCHABAMBA";
                            break;

                        case "13":
                            expediciones = "ORURO";
                            break;
                        case "10":
                            expediciones = "POTOSI";
                            break;
                        case "14":
                            expediciones = "TARIJA";
                            break;
                        case "12":
                            expediciones = "BENI";
                            break;
                    }
                    data.Name = drClien["personaNombres"].ToString();
                    data.Lastname = drClien["personaApellidos"].ToString();
                    data.Lastnamemother = "";
                    data.Email = drClien["personaCorreo"].ToString();
                    data.Cellphone = drClien["PersonaTelefono"].ToString();
                    data.Ci = drClien["personaDNI"].ToString();
                    data.Expedition = expediciones;
                    fechaNac = Convert.ToDateTime(drClien["PersonaFechaNacimiento"].ToString());

                    fecha_nacimiento = (fechaNac.Year) + "-" + (fechaNac.Month) + "-" + (fechaNac.Day);

                    data.Birthday = fecha_nacimiento;
                    resp = mb.registerPatient(data, "ws_external_medibook", "ws_external_medibook");
                    if (resp.CodError == 0)
                    {
                        cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar, 200).Value = drClien["requiereServicioId"].ToString();
                        cmd.CommandText = "update requiereServicio set EstadoReqServId=5 ,RequiereServicioFechaMod=getdate() where RequiereServicioId=@RequiereServicioId";
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        InsertarLogNotificacion("REQUERIMIENTOS TRANSFERIDOS", DateTime.Now, drClien["requiereServicioId"].ToString(), 0);
                    }
                    else
                    {
                        InsertarLogNotificacion("REQUERIMIENTOS TRANSFERIDOS", DateTime.Now, drClien["requiereServicioId"].ToString(), 0);
                    }
                }

            }
            catch (Exception ex)
            {

                InsertarLogNotificacion("REQUERIMIENTOS TRANSFERIDOS", DateTime.Now, ex.Message, 0);
            }
            finally
            {
                cnn.Close();
            }

        }
        public static void InsercionMediBookV2()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            string ciudad = "";
            string CiudadId = "";
            DateTime fechaNac = DateTime.Now;
            string fecha_nacimiento = "";
            string expediciones = "";
            try
            {
                cnn.Open();
                cmd.Connection = cnn;
                List<BE.MediBook> LstmediBooks = new List<BE.MediBook>();
                LstmediBooks = bcMediBook.ObtenerDatosMediBook();

                foreach (BE.MediBook item in LstmediBooks)
                {
                    ////////////////////////////////////////////////////////

                    if (clientMediBook.BaseAddress == null)
                    {

                        clientMediBook.BaseAddress = new Uri("https://www.medibook.com.bo/");
                        clientMediBook.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    }
                    HttpResponseMessage resp = clientMediBook.PostAsJsonAsync("medibook/ws/Server_External_Web/registerPatientServiceWeb", item).Result;

                    //Add headers

                    //Call client.PostAsJsonAsync to send a POST request to the appropriate URI   

                    //This method throws an exception if the HTTP response status is an error code.  
                    //var xx = resp.EnsureSuccessStatusCode();
                    if (resp.IsSuccessStatusCode)
                    {
                        Respuesta respuestaFin = resp.Content.ReadAsAsync<Respuesta>().Result;

                        if (respuestaFin.CodError == "0")
                        {
                            cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar, 200).Value = item.RequiereServicioId;
                            cmd.CommandText = "update requiereServicio set EstadoReqServId=5 ,RequiereServicioFechaMod=getdate() where RequiereServicioId=@RequiereServicioId";
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            InsertarLogNotificacion("REQUERIMIENTOS TRANSFERIDOS MEDIBOOK", DateTime.Now, item.RequiereServicioId, 0);
                        }
                        else
                        {
                            InsertarLogNotificacion("REQUERIMIENTOS TRANSFERIDOS MEDIBOOK", DateTime.Now, item.RequiereServicioId.ToString(), 0);
                        }
                    }
                    else
                    {
                        /* var resultado = resp.Content.ReadAsStringAsync().Result;
                         var result = JsonConvert.DeserializeObject<ResultServer>(resultado);
                         throw new Exception(string.Format("Message:{0}, ExceptionMessage: {1}", result.Message, result.ExceptionMessage));*/
                    }


                    ///////////////////////////////////////////////////////

                }




            }
            catch (Exception ex)
            {

                InsertarLogNotificacion("REQUERIMIENTOS TRANSFERIDOS MEDIBOOK", DateTime.Now, ex.Message, 0);
            }
            finally
            {
                cnn.Close();
            }

        }
        public static void Obtener_Especialidades()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            string ciudad = "";
            string CiudadId = "";
            DateTime fechaNac = DateTime.Now;
            string fecha_nacimiento = "";
            string expediciones = "";
            try
            {
                cnn.Open();
                cmd.Connection = cnn;
                ///////////////////////////////////////////////7
                ///  ////////////////////////////////////////////
                MediBook.wsComelecServerPortTypeClient mb = new MediBook.wsComelecServerPortTypeClient();

                MediBook.WsTransaccion data = new MediBook.WsTransaccion();
                MediBook.WsSpecialties dataEsp = new MediBook.WsSpecialties();
                MediBook.RespTransaccion resp = new MediBook.RespTransaccion();
                MediBook.RespSpecialties respEsp = new MediBook.RespSpecialties();
                SqlDataAdapter da1;
                dataEsp.Cityid = "2";
                respEsp = mb.getSpecialties(dataEsp, "ws_external_medibook", "ws_external_medibook");
                // ds = respEsp.Descripcion;
                da1 = new SqlDataAdapter("select rs.requiereServicioId,p.personaNombres,p.personaApellidos,p.personaCorreo,p.PersonaTelefono,p.personaDNI,td.TipoDocumentoAbreviatura,p.CiudadId,p.PersonaFechaNacimiento from requiereServicio  rs inner join persona p on rs.personaId = p.personaId inner join TipoDocumento td on p.TipoDocumentoId = td.tipoDocumentoId where  rs.servicioId in (select ServicioId from servicioPersona where personaId in (select PersonaId from persona where personaNombres = 'MediBook' and PersonaCorreo = 'medibookbolivia@gmail.com')) and rs.EstadoReqServId not in (4, 3,5) ", cnn);
                da1.Fill(ds);
                foreach (DataRow drClien in ds.Tables[0].Rows)
                {
                    expediciones = drClien["CiudadId"].ToString();
                    switch (CiudadId)
                    {
                        case "2":
                            expediciones = "SANTA CRUZ";
                            break;
                        case "15":
                            expediciones = "LA PAZ";
                            break;
                        case "9":
                            expediciones = "COCHABAMBA";
                            break;

                        case "13":
                            expediciones = "ORURO";
                            break;
                        case "10":
                            expediciones = "POTOSI";
                            break;
                        case "14":
                            expediciones = "TARIJA";
                            break;
                        case "12":
                            expediciones = "BENI";
                            break;
                    }
                    data.Name = drClien["personaNombres"].ToString();
                    data.Lastname = drClien["personaApellidos"].ToString();
                    data.Lastnamemother = "";
                    data.Email = drClien["personaCorreo"].ToString();
                    data.Cellphone = drClien["PersonaTelefono"].ToString();
                    data.Ci = drClien["personaDNI"].ToString();
                    data.Expedition = expediciones;
                    fechaNac = Convert.ToDateTime(drClien["PersonaFechaNacimiento"].ToString());

                    fecha_nacimiento = (fechaNac.Year) + "-" + (fechaNac.Month) + "-" + (fechaNac.Day);

                    data.Birthday = fecha_nacimiento;
                    resp = mb.registerPatient(data, "ws_external_medibook", "ws_external_medibook");
                    mb.getSpecialties(dataEsp, "ws_external_medibook", "ws_external_medibook");
                    if (resp.CodError == 0)
                    {
                        cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar, 200).Value = drClien["requiereServicioId"].ToString();
                        cmd.CommandText = "update requiereServicio set EstadoReqServId=5 ,RequiereServicioFechaMod=getdate() where RequiereServicioId=@RequiereServicioId";
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        InsertarLogNotificacion("REQUERIMIENTOS TRANSFERIDOS", DateTime.Now, drClien["requiereServicioId"].ToString(), 0);
                    }
                    else
                    {
                        InsertarLogNotificacion("REQUERIMIENTOS TRANSFERIDOS", DateTime.Now, drClien["requiereServicioId"].ToString(), 0);
                    }
                }

            }
            catch (Exception ex)
            {

                InsertarLogNotificacion("REQUERIMIENTOS DESIERTOS", DateTime.Now, ex.Message, 0);
            }
            finally
            {
                cnn.Close();
            }

        }
        public static async Task EnviarNotifcacionFinalizandoElServicio()
        {
            SqlCommand cmd = new SqlCommand();//modificado 
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataSet data = new DataSet();
            // SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            int cantidad = 0;
            string requiereServicioId = "";
            string NombreCliente = "";
            string ServicioURLFoto = "";
            string Hora = "";
            string PersonaTokenId = "";
            string ns = "";
            try
            {
                ///////////////////////////////////////////////////////////
                List<BE.ServAsig> lstServAsig = bcServAsig.ListadoRequerimientosNofinalizadosServicios("es", ObtenerHoraBoliviana(), BE.relServAsig.requiereServicio
                    , BE.relRequiereServicio.servicio);
                BE.Persona persona = new BE.Persona();
                foreach (var servAsig in lstServAsig)
                {
                    BE.Persona Persona = bcPersona.BuscarPersonaxId(servAsig.requiereServicio.PersonaId, "", BE.relPersona.ciudad, BE.relciudad.region, BE.relRegion.pais, BE.relpais.moneda);
                    BE.ServAsig sa = new BE.ServAsig();
                    sa = servAsig;
                    sa.ServAsigFHFin = DateTime.Now;
                    sa.ServAsigFHPago = DateTime.Now;
                    sa.TipoEstado = BE.TipoEstado.Modificar;
                    bcServAsig.Actualizar(ref sa, false);
                    await EnviarNotificacionesAsyncCliente1(Persona, "ClienteFinServicio", Persona.PersonaIdioma, sa.requiereServicio, Convert.ToDecimal(sa.ServAsigCostoTotal));
                    await OfertaFirebase(servAsig.requiereServicio.RequiereServicioId, Convert.ToInt32(sa.requiereServicio.PersonaId), "3", "recibido", "Requerimientos");


                }


            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                // cnn.Close();
            }
        }
        public static async Task OfertaFirebase(string idRequest, int PersonaId, string idEstado, string tipo, string opcion)
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
                BasePath = "https://service-web-258723.firebaseio.com/"
            };
            IFirebaseClient client;
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {


                FirebaseResponse response = await client.GetTaskAsync("Requerimientos/" + idRequest);
                var Requerimientos = new Requerimientos
                {
                    idPersona = Convert.ToString(PersonaId),
                    idRequest = idRequest,
                    servicioId = "",
                    fecha = "",
                    hora = "",
                    estado = "",
                    descripcion = "",
                    arch1 = "",
                    arch2 = "",
                    arch3 = "",
                    arch4 = "",
                    dirLatitud = "",
                    dirLong = "",
                    dirTitulo = "",
                    tipoSolicitud = "",
                    diasRest = "",
                    cantOfer = "",
                    colorServicio = "",          ///////////////////////
                    idEstado = idEstado,
                };

                if (response.Body != "null")
                {

                    Requerimientos obj = response.ResultAs<Requerimientos>();
                    Requerimientos = obj;
                    if (tipo == "recibido")
                    {
                        switch (opcion)
                        {
                            case "Requerimientos":
                                if (Requerimientos.cantOfer == "")
                                {
                                    Requerimientos.cantOfer = "0";
                                }
                                if (idEstado == "2")
                                {

                                    int valor = Convert.ToInt32(Requerimientos.cantOfer) + 1;
                                    Requerimientos.cantOfer = Convert.ToString(valor);
                                }

                                Requerimientos.idEstado = idEstado;
                                break;


                        }

                    }
                    else
                    {
                        if (tipo == "leido" || tipo == "eliminado")
                        {

                            switch (opcion)
                            {

                                case "Requerimientos":
                                    Requerimientos.cantOfer = "0";
                                    break;

                            }


                        }

                    }

                    FirebaseResponse responseUpdate = await client.UpdateTaskAsync("Requerimientos/" + idRequest, Requerimientos);
                    Requerimientos result = responseUpdate.ResultAs<Requerimientos>();
                }
                else
                {
                    if (tipo == "recibido")
                    {
                        SetResponse responseInsert = await client.SetTaskAsync("Requerimientos/" + idRequest, Requerimientos);
                        Requerimientos result = responseInsert.ResultAs<Requerimientos>();

                    }
                }




            }


        }
        private static void EnviandoCorreoServicioSolicitado()
        {
            // SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            // SqlCommand cmd = new SqlCommand();
            // cmd.Connection = cnn;
            //if (cnn.State != ConnectionState.Open) cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter();

            //  cmd.CommandText = "select rs.requiereServicioId, p1.PersonaNombres+' '+ p1.PersonaApellidos as persona ,p1.personaCorreo,s.ServicioNombre,rs.RequiereServicioDescripcion,rs.RequiereServicioFechaHoraReq,p.PersonaNombres+' '+ p.PersonaApellidos as persona,rsp.RequiereServicioProvCotizacion from requiereServicio rs inner join [dbo].[RequiereServicioProveedores] rsp on rs.requiereServicioId = rsp.requiereServicioId inner join servicioPersona sp on rsp.ServicioPersonaId = sp.ServicioPersonaId inner join persona p on p.personaId = sp.personaId inner join persona p1 on p1.personaId = rs.personaId inner join servicio s on s.servicioId = rs.servicioId and rsp.StatusRequiereId = 2 and rs.EstadoReqServId = 1 ";
            // cmd.CommandText = "";
            //da.SelectCommand = cmd;
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //Susanaharriette@gmail.com"
            ///////////////////////////////////////////////////////////////////////////////////////////
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("webmaster@serviceweb.bo");
            mail.To.Add("yobi31018812@gmail.com");
            mail.Subject = "Notificaciones Service Web";

            //  string text = "Gracias, ayer estuve disfrutando de un paisaje estupendo.";
            string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoFormato1"].ToString());

            AlternateView plainView = AlternateView.CreateAlternateViewFromString(text, Encoding.UTF8, MediaTypeNames.Text.Plain);
            // Ahora creamos la vista para clientes que // pueden mostrar contenido HTML...
            string nombre = "Juan Perez";
            text = text.Replace("nombre", nombre);
            /* String tabla = "<table>
               foreach (Dato in datos)
                  tabla += "<tr>" + dato + "</tr>";*/
            //  string tabla = "<table class=" + "auto - style1" + ">" + "<tr><td>nombre & nbsp;</td ><td>< asp:TextBox ID = +" + "TextBox1" + "runat = +" + "server" + " ></ asp:TextBox ></ td ></ tr >< tr >< td > &nbsp;</ td >< td > &nbsp;</ td ></ tr ></ table > ";

            //string html = "<img src='cid:imagen'/><h2 style="+"background-color:#3EAD37;"+">Gracias,"+nombre + "!</h2> "+tabla;

            string html = text;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
            //   LinkedResource img = new LinkedResource(@"C:\encabezado del servicio.png", MediaTypeNames.Image.Jpeg);
            LinkedResource img = new LinkedResource(@"C:\encabezado final.png", MediaTypeNames.Image.Jpeg);

            LinkedResource imgs = new LinkedResource(@"C:\seguro.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgf = new LinkedResource(@"C:\facebook.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgi = new LinkedResource(@"C:\instagram.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgd = new LinkedResource(@"C:\ìefinal.png", MediaTypeNames.Image.Jpeg);
            img.ContentId = "imagen";
            imgs.ContentId = "seguro";
            imgf.ContentId = "facebook";
            imgi.ContentId = "instagram";
            imgd.ContentId = "direccion";
            htmlView.LinkedResources.Add(img);
            htmlView.LinkedResources.Add(imgs);
            htmlView.LinkedResources.Add(imgf);
            htmlView.LinkedResources.Add(imgi);
            htmlView.LinkedResources.Add(imgd);
            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);
            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                //  Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            smtp.Send(mail);
            ////////////////////////////////////////////////////////////////////////////////////////////

        }
        private static void EnviandoCorreoServicioSolicitado2()
        {

            ///////////////////////////////////////////////////////////////////////////////////////////
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("webmaster@serviceweb.bo");
            mail.To.Add("Susanaharriette@gmail.com");
            mail.Subject = "Notificaciones Service Web";

            //  string text = "Gracias, ayer estuve disfrutando de un paisaje estupendo.";
            string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW4"].ToString());

            AlternateView plainView = AlternateView.CreateAlternateViewFromString(text, Encoding.UTF8, MediaTypeNames.Text.Plain);
            // Ahora creamos la vista para clientes que // pueden mostrar contenido HTML...
            string nombre = "Juan Perez";
            /* String tabla = "<table>
               foreach (Dato in datos)
                  tabla += "<tr>" + dato + "</tr>";*/
            //  string tabla = "<table class=" + "auto - style1" + ">" + "<tr><td>nombre & nbsp;</td ><td>< asp:TextBox ID = +" + "TextBox1" + "runat = +" + "server" + " ></ asp:TextBox ></ td ></ tr >< tr >< td > &nbsp;</ td >< td > &nbsp;</ td ></ tr ></ table > ";

            //string html = "<img src='cid:imagen'/><h2 style="+"background-color:#3EAD37;"+">Gracias,"+nombre + "!</h2> "+tabla;

            string html = text;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
            LinkedResource img = new LinkedResource(@"C:\GraciasServiceWeb.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgs = new LinkedResource(@"C:\ImagenServiceWeb.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgf = new LinkedResource(@"C:\facebook.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgi = new LinkedResource(@"C:\instagram.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgseguro = new LinkedResource(@"C:\seguro.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgsescudo = new LinkedResource(@"C:\escudo.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgcelular = new LinkedResource(@"C:\celular.png", MediaTypeNames.Image.Jpeg);
            LinkedResource imgd = new LinkedResource(@"C:\direccion.png", MediaTypeNames.Image.Jpeg);
            img.ContentId = "GraciasServiceWeb";
            imgs.ContentId = "ImagenServiceWeb";
            imgf.ContentId = "facebook";
            imgi.ContentId = "instagram";
            imgseguro.ContentId = "seguro";
            imgsescudo.ContentId = "escudo";
            imgcelular.ContentId = "celular";
            imgd.ContentId = "direccion";
            htmlView.LinkedResources.Add(img);
            htmlView.LinkedResources.Add(imgs);
            htmlView.LinkedResources.Add(imgf);
            htmlView.LinkedResources.Add(imgi);
            htmlView.LinkedResources.Add(imgseguro);
            htmlView.LinkedResources.Add(imgsescudo);
            htmlView.LinkedResources.Add(imgcelular);
            htmlView.LinkedResources.Add(imgd);
            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);
            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                //  Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            smtp.Send(mail);
            ////////////////////////////////////////////////////////////////////////////////////////////

        }
        private static void EnvioCorreoPruebaHtml()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);

            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string requiereServicioId = "";
            string cuerpoDetalle = "";
            string cuerpo = "cuerpo";
            try
            {


                string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                string Body = text.Replace("MENSAJE", cuerpo);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("webmaster@serviceweb.bo");
                mail.To.Add("yobi31018812@gmail.com");
                mail.Subject = "Notificaciones Service Web";
                mail.Body = Body;
                // mail.AlternateViews.Add(htmlView);
                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    //  Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                    Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };
                smtp.Send(mail);




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.Read();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }


        private static DataSet ObtenerImporte_y_Calificacion(string ServAsigId)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["BaseDatos"]);
            if (cnn.State != ConnectionState.Open) cnn.Open();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand sqlCmd = new SqlCommand("ObtenerImporte_y_Calificacion", cnn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", ServAsigId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                sqlCmd.Parameters.Clear();
                da.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }
            return ds;
        }
















        private static async Task EnviarNotificacionesAsyncCliente1(BE.Persona persona, string tipo, string lang, BE.RequiereServicio requiereServicio = null, decimal Importe = 0, decimal Calificacion = 0, string SiniestroId = null)
        {
            DataRow data = bcPersona.ListadoDatosNotificacionv2(tipo, lang);
            var title = data["title"].ToString();
            var body = data["body"].ToString();
            var vista = data["Fragment"].ToString();
            var BotonTexto = data["BotonTexto"].ToString();
            var jsonBody2 = JsonConvert.SerializeObject(body);
            bool Invasivo = false;
            string i = "";
            string foto = "";
            string des = "";
            string ReqId = "";
            string[] titulo;
            string[] cuerpo;
            string id = "";
            string action1 = "";
            string versionTelefono = "";
            int ContadorBadge = 0;


            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
                BasePath = "https://service-web-258723.firebaseio.com/"
            };
            IFirebaseClient client;
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            switch (tipo)
            {

                case "ClienteReque":
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 1;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteInicioServicio":
                    body = body + " " + requiereServicio.servicio.ServicioNombre;
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 7;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteFinServicio":
                    titulo = body.Split('/');
                    body = "";
                    body = titulo[0] + string.Format("{0:N}", Convert.ToInt32(Importe)) + " " + Convert.ToString(persona.Ciudad.Region.pais.Moneda.MonedaNombre) + titulo[1];
                    Invasivo = true;
                    foto = requiereServicio.servicio.ServicioURLFoto;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 8;
                    title = title + " " + "RQ - " + ReqId;
                    break;
                case "ProveedorFinServicioFinal":
                    titulo = body.Split('/');
                    body = "";
                    title = "";
                    title = titulo[0] + string.Format("{0:N}", Convert.ToInt32(Importe)) + " " + persona.Ciudad.Region.pais.Moneda.MonedaNombre;
                    body = titulo[1] + " " + Convert.ToString(Convert.ToInt32(Calificacion)) + " " + titulo[2] + " " + titulo[3];
                    Invasivo = true;
                    des = BotonTexto;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 6;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteCotizacion":
                    cuerpo = body.Split('/');
                    body = "";
                    body = cuerpo[0];
                    notificacionPersona.ConceptoNotificacionId = 4;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "AproSiniestro":
                    Invasivo = true;
                    des = BotonTexto;
                    notificacionPersona.ConceptoNotificacionId = 10;
                    ReqId = SiniestroId;
                    title = title + " " + "S: " + ReqId;
                    break;
                case "RegSiniestro":
                    notificacionPersona.ConceptoNotificacionId = 10;
                    ReqId = SiniestroId;
                    title = title + " " + "S: " + ReqId;
                    break;
                case "ProveedorConfV2":
                    Invasivo = true;
                    action1 = "inf";
                    notificacionPersona.ConceptoNotificacionId = 5;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;
                case "ClienteConfV2":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 5;
                    ReqId = requiereServicio.RequiereServicioId;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ProveedorCanV2":
                    Invasivo = true;
                    action1 = "inf";
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;
                case "ProveeorPerdioAdj":
                    Invasivo = true;
                    action1 = "inf";
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    ReqId = requiereServicio.RequiereServicioId;
                    break;
                case "ClienteCBA":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 11;
                    ReqId = SiniestroId;
                    break;
            }

            var to = persona.PersonaTokenId;
            var senderId2 = string.Format("id={0}", SenderIdFB);
            var key = string.Format("key={0}", ServerKey);
            if (Invasivo == false)
            {

                i = "no";
            }
            else
            {
                i = "si";
            }
            var sound = "default";
            var invasivo = i;
            var reqservid = ReqId;
            var reqservdes = des;///esta campo va a body en el invasivo 
            var servicioNombre = body;
            var ServicioUrlFoto = foto;
            var action = action1;
            var tag = "no";
            var priority = "high";
            ////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////

            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {


                FirebaseResponse response = await client.GetTaskAsync("Notificacion/" + Convert.ToInt32(persona.PersonaId));
                var data1 = new Data
                {
                    PersonaId = "",
                    Post = 0,
                    Solicitudes = 0,
                    Servicios = 0,
                    Rendimientos = 0,
                    Notificaciones = 0,
                };

                if (response.Body != "null")
                {
                    Data obj = response.ResultAs<Data>();
                    data1 = obj;
                    ContadorBadge = Convert.ToInt32(obj.Notificaciones) + 1;
                }
            }
            var badge = ContadorBadge;
            versionTelefono = bclogSesionesPersona.VersionTelefono(persona.PersonaId);

            if (invasivo == "si")
            {
                ////////////////////////////////////////////////////

                tag = "si";
                if (versionTelefono.Contains("IOS"))
                {
                    tag = i;
                    var data2 = new { to, notification = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag, badge, sound }, priority };
                    jsonBody2 = JsonConvert.SerializeObject(data2);
                    /* var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                     jsonBody2 = JsonConvert.SerializeObject(data2);/
                    /*  var data2 = new { to, notification = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }, priority };
                   jsonBody2 = JsonConvert.SerializeObject(data2);*/

                }
                else
                {
                    /*var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);*/

                    var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);


                }
                /////////////////////////////////////////////////////////////

                /* var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                   jsonBody2 = JsonConvert.SerializeObject(data2);*/


            }
            else
            {
                if (versionTelefono.Contains("IOS"))
                {
                    var data2 = new { to, notification = new { body, title, badge, sound }, priority };
                    jsonBody2 = JsonConvert.SerializeObject(data2);
                    InsertarLogNotificacion("badge", DateTime.Now, Convert.ToString(badge), persona.PersonaId);

                }
                else
                {
                    //   var data2 = new { to, notification = new { sound, title, body } };
                    // jsonBody2 = JsonConvert.SerializeObject(data2);


                    var data2 = new { to, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);

                }

            }


            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlFBPN))
            {
                httpRequest.Headers.TryAddWithoutValidation("Authorization", key);
                // httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.SendAsync(httpRequest);

                    if (result.IsSuccessStatusCode)
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, persona.PersonaId);

                    }
                    else
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, persona.PersonaId);

                    }
                }
            }





            /////////////////////////NOTIFICACION FIREBASE
            notificacionPersona.RequiereServicioId = ReqId;
            notificacionPersona.PersonaId = persona.PersonaId;
            notificacionPersona.TipoEstadoId = 1;
            notificacionPersona.NotificacionPersonaTitulo = title;
            notificacionPersona.NotificacionPersonaDescripcion = body;
            notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
            notificacionPersona.NotificacionPersonaFragment = vista;
            notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
            notificacionPersona.EstadoNotificacionId = 1;
            notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;

            if (notificacionPersona != null)
            {
                bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
            }
            /////////////////

            /////////////////////////////
            await NotificacionFirebase(Convert.ToInt32(persona.PersonaId), "recibido", "Notificaciones");
        }



    }


    internal class Data
    {
        public string PersonaId { get; set; }
        public int Post { get; set; }
        public int Solicitudes { get; set; }
        public int Servicios { get; set; }
        public int Rendimientos { get; set; }
        public int Notificaciones { get; set; }
    }
    internal class Requerimientos
    {
        public string idPersona { get; set; }
        public string idRequest { get; set; }
        public string servicioId { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string estado { get; set; }
        public string descripcion { get; set; }
        public string arch1 { get; set; }
        public string arch2 { get; set; }
        public string arch3 { get; set; }
        public string arch4 { get; set; }
        public string dirLatitud { get; set; }
        public string dirLong { get; set; }
        public string dirTitulo { get; set; }
        public string tipoSolicitud { get; set; }
        public string diasRest { get; set; }
        public string cantOfer { get; set; }
        public string colorServicio { get; set; }
        public string idEstado { get; set; }
    }
}
