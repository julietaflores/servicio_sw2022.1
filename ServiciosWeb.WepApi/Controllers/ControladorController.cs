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

using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.WepApi.Models;
using Newtonsoft.Json;
using ServiciosWeb.WepApi.Models.Entities;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json.Linq;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Web.Http.Cors;
using System.Globalization;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Net.Http.Headers;
using RestSharp;
using FireSharp.Extensions;


namespace ServiciosWeb.WepApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ControladorController : ApiController
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "l6nc5NF2IKi6HZGribJbaUeLk1tBDUIUzNglVXlS",
            BasePath = "https://service-web-258723.firebaseio.com/"
        };
        IFirebaseClient client;
        private ControladorManager contManager = null;
        private PostManager postManager = null;
        static HttpClient ClienteHuawei = new HttpClient();
        public ControladorController()
        {
            contManager = new ControladorManager("cadenaCnx");
            postManager = new PostManager("cadenaCnx");
        }

        private RequiereSercicioProveedoresConnection db = new RequiereSercicioProveedoresConnection();
        private PostConnection dbPost = new PostConnection();

        SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["con"]);

        ServiciosWeb.WepApi.Models.PackCatSer_CatServDesta PackCatSer_CatServDesta = new ServiciosWeb.WepApi.Models.PackCatSer_CatServDesta();
        ServiciosWeb.Datos.Modelo.Respuesta<PackCatSer_CatServDesta> RespuestaDest = new ServiciosWeb.Datos.Modelo.Respuesta<PackCatSer_CatServDesta>();
        ServiciosWeb.Datos.Modelo.Respuesta<ReqSev_ServProveedores> RespuestaSerProv = new ServiciosWeb.Datos.Modelo.Respuesta<ReqSev_ServProveedores>();
        ServiciosWeb.Datos.Modelo.Respuesta<ServAsig> RespuestaServAsig = new ServiciosWeb.Datos.Modelo.Respuesta<ServAsig>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ServAsig>> RespuestaListServAsig = new ServiciosWeb.Datos.Modelo.Respuesta<List<ServAsig>>();
        ServiciosWeb.Datos.Modelo.Respuesta<ReqSev_ServProveedores> Respuesta = new ServiciosWeb.Datos.Modelo.Respuesta<ReqSev_ServProveedores>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.RequiereServicio>> RespuestaServ = new ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.RequiereServicio>>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.ServAsigCosto>> RespuestaServAsigCosto = new ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.ServAsigCosto>>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.ServicioPersona>> RespuestaServicioPersona = new ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.ServicioPersona>>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.CategoriaServicioDestacada>> RespuestaServDest = new ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.CategoriaServicioDestacada>>();
        ServiciosWeb.Datos.Modelo.Respuesta<ServiciosWeb.WepApi.Models.RequiereServicio> RespuestaReqServ = new ServiciosWeb.Datos.Modelo.Respuesta<ServiciosWeb.WepApi.Models.RequiereServicio>();
        ServiciosWeb.Datos.Modelo.Respuesta<RequiereServicioProveedores> PersonaMensaje = new ServiciosWeb.Datos.Modelo.Respuesta<RequiereServicioProveedores>();
        ServiciosWeb.Datos.Modelo.Respuesta<PackBusquedaServicio> RespuestaServ_Cat = new ServiciosWeb.Datos.Modelo.Respuesta<PackBusquedaServicio>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ServicioPersona>> RespuestaSerPer = new ServiciosWeb.Datos.Modelo.Respuesta<List<ServicioPersona>>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ConceptoCosto>> RespuestaConCos = new ServiciosWeb.Datos.Modelo.Respuesta<List<ConceptoCosto>>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ServicioPersonaGaleria>> RespuestaSePeGa = new ServiciosWeb.Datos.Modelo.Respuesta<List<ServicioPersonaGaleria>>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<ServicioPersonaHorario>> RespuestaSePeHo = new ServiciosWeb.Datos.Modelo.Respuesta<List<ServicioPersonaHorario>>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<Comentario>> RespuestaComen = new ServiciosWeb.Datos.Modelo.Respuesta<List<Comentario>>();
        ServiciosWeb.Datos.Modelo.Respuesta<Billetera> RespuestaBill = new ServiciosWeb.Datos.Modelo.Respuesta<Billetera>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<BilleteraDetalle>> RespuestaBiDet = new ServiciosWeb.Datos.Modelo.Respuesta<List<BilleteraDetalle>>();
    


        private static Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
        private static string ServerKey = "AAAAo1TUZ_U:APA91bGZ-Lyd-fpdm8Qwcun_gwNGL997bZO6RcfA0JJQs56PzM1pGN4d4yOeDtwWDM0-5kr3qRCMrIbbAIWOPt5GpM8ngR8Aeum2AzEqmQEFUtBYoiPg7pxSFcey2XXA_8nKJpgaZuH3";
        private static string SenderIdFB = "701502875637";
        private static string UrlFBPN = "https://fcm.googleapis.com/fcm/send";

        //////////////////////////////////////////////////////
        /// <summary>
        ///POST A LA TABLA RequiereServicio atributo Servicio=null
        /// </summary>
        /// 


        [ResponseType(typeof(ServiciosWeb.WepApi.Models.ServAsig))]
        [HttpPost]
        [Route("api/ServAsig")] // INICIALIZAR SERVICIO ,Finalizar Servicio
        public IHttpActionResult _4PostServAsig(ServiciosWeb.WepApi.Models.ServAsig sa, decimal TipoPersonaId)
        {
            int resp = 0;
            int resPC = 0;
            string IdCosto = "";
            PostController postController = new PostController();


            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                string Id = ObtenerId(conexion, "ServAsig", null);
                SqlCommand sqlCmd = new SqlCommand("InsertarServAsig", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", Id);
                sqlCmd.Parameters.AddWithValue("@ProveedorId", sa.ProveedorId);
                sqlCmd.Parameters.AddWithValue("@ServAsigFHUbicacion", sa.ServAsigFHUbicacion);
                sqlCmd.Parameters.AddWithValue("@ServAsigFHEstimadaLlegada", sa.ServAsigFHEstimadaLlegada);
                if (sa.ServAsigFHInicio == null) { sqlCmd.Parameters.Add("@ServAsigFHInicio", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHInicio", sa.ServAsigFHInicio); }
                if (sa.ServAsigFHFin == null) { sqlCmd.Parameters.Add("@ServAsigFHFin", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHFin", sa.ServAsigFHFin); }
                if (sa.ServAsigFHPago == null) { sqlCmd.Parameters.Add("@ServAsigFHPago", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHPago", sa.ServAsigFHPago);
                }
                if (sa.ServAsigCostoTotal == null)
                {
                    sqlCmd.Parameters.Add("@ServAsigCostoTotal", SqlDbType.Decimal).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@ServAsigCostoTotal", sa.ServAsigCostoTotal);
                }
                if (sa.StatusServAsigId == null)
                {
                    sqlCmd.Parameters.Add("@StatusServAsigId", SqlDbType.Decimal).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@StatusServAsigId", sa.StatusServAsigId);
                }
                if (sa.RequiereServicioId == null)
                {
                    sqlCmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@RequiereServicioId", sa.RequiereServicioId);
                }
                if (sa.ServAsigPagaCliente == null)
                {
                    sqlCmd.Parameters.Add("@ServAsigPagaCliente", SqlDbType.Bit).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@ServAsigPagaCliente", sa.ServAsigPagaCliente);
                }

                resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
                sqlCmd.Parameters.Clear();
                ///////////////////////////////////////////////////////////////////////////INSERCIONSERVICIOASIGNADOCOSTO
                foreach (ServAsigCosto i in sa.servAsigCosto)
                {
                    //  PostContenido  posCon= (postCont.PostContenido[0]);
                    ///////////////////

                    IdCosto = ObtenerId(conexion, "servAsigCosto", null);
                    ////////////////////
                    SqlCommand sqlCmd1 = new SqlCommand("[InsertarservAsigCosto]", conexion);
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.CommandTimeout = 0;
                    sqlCmd1.Parameters.AddWithValue("@ServAsigCostoId", IdCosto);
                    sqlCmd1.Parameters.AddWithValue("@ServAsigId", Id);
                    sqlCmd1.Parameters.AddWithValue("@ConceptoCostoId", i.ConceptoCostoId);
                    sqlCmd1.Parameters.AddWithValue("@ServAsigCostoValor", i.ServAsigCostoValor);

                    resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());

                    ////////////////////////////////////////////////////////////////////////////
                }
                ///////////////////////////////////////////////////////////////////////////INSERCION DE POST
                PostCont postCont = new PostCont();
                postCont = sa.post;
                postCont.ServAsigId = Id;

                postController.PostPostContenido(postCont);



                if ((resp != 0) && (resPC != 0))
                {


                    RespuestaServAsig.mensaje = "OK";
                    RespuestaServAsig.estado = 1;
                    RespuestaServAsig.valor = null;


                }


            }
            catch (Exception ex)
            {
                RespuestaServAsig.mensaje = ex.Message;
                RespuestaServAsig.estado = 2;
                RespuestaServAsig.valor = null;

            }
            finally
            {
                conexion.Close();
            }
            return Ok(RespuestaServAsig);
        }





        //////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("api/ServAsig")]
        public ServiciosWeb.Datos.Modelo.Respuesta<ServAsig> _5GetServAsig(string ServAsigId, string lang)
        {
            PostController postController = new PostController();
            decimal PostId = 0;

            ///////////////////////////////////////////Post
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                //////////////////////////////////////////////

                SqlCommand sqlCmd = new SqlCommand("[VerServAsig]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", ServAsigId);
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ServiciosWeb.WepApi.Models.ServAsig ServAsig = new ServiciosWeb.WepApi.Models.ServAsig();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ServAsig = Conversor.toServAsig(dt.Rows[0]);

                }
                ServiciosWeb.WepApi.Models.StatusServAsig StatusServAsig = new ServiciosWeb.WepApi.Models.StatusServAsig();
                StatusServAsig.StatusServAsigId = Convert.ToDecimal(dt.Rows[0]["StatusServAsigId"].ToString());
                StatusServAsig.StatusServAsigNombre = Convert.ToString(dt.Rows[0]["StatusServAsigNombre"].ToString());
                ServAsig.statusServAsig = StatusServAsig;
                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                SqlCommand sqlCmd1 = new SqlCommand("[VerServAsigCosto_ConceptoCosto]", conexion);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.CommandTimeout = 0;
                sqlCmd1.Parameters.AddWithValue("@ServAsigId", (dt.Rows[0]["ServAsigId"].ToString()));
                sqlCmd1.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                List<ServAsigCosto> lst = Conversor.toServAsigCosto(dt1.Select());
                ServAsig.servAsigCosto = lst;

                ///////////////////////////////////////////// ver portcontenido
                ServiciosWeb.Datos.Modelo.Respuesta<PostCont> RespuestaCont = new ServiciosWeb.Datos.Modelo.Respuesta<PostCont>();
                PostId = ObtenerPostId(conexion, (dt.Rows[0]["ServAsigId"].ToString()));
                RespuestaCont = postController.GetPostContenido(PostId);
                ServAsig.post = RespuestaCont.valor;
                ////////////////////////////////

                ////////////////////////////////

                RespuestaServAsig.estado = 1;
                RespuestaServAsig.valor = ServAsig;
                RespuestaServAsig.mensaje = "OK";

                return RespuestaServAsig;
            }
            catch (Exception ex)
            {
                RespuestaServAsig.estado = 2;
                RespuestaServAsig.valor = null;
                RespuestaServAsig.mensaje = ex.Message;
                return RespuestaServAsig;
            }
            finally
            {
                conexion.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////




        }
        ////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("api/ServAsig")]
        public ServiciosWeb.Datos.Modelo.Respuesta<ServAsig> _8GetServAsig(string RequiereServicioId, string lang)
        {

            ///////////////////////////////////////////Post
            decimal PostId = 0;
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                PostController postController = new PostController();
                //////////////////////////////////////////////
                List<ServiciosWeb.WepApi.Models.ServAsig> lstServAsig = new List<ServiciosWeb.WepApi.Models.ServAsig>();
                SqlCommand sqlCmd = new SqlCommand("[VerServAsigxRequiereServicio]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ServiciosWeb.WepApi.Models.ServAsig ServAsig = new ServiciosWeb.WepApi.Models.ServAsig();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ServAsig = Conversor.toServAsig(dt.Rows[0]);

                }

                ////////////////////////////////////////////
                if (dt.Rows.Count > 0)
                {
                    ServiciosWeb.WepApi.Models.StatusServAsig StatusServAsig = new ServiciosWeb.WepApi.Models.StatusServAsig();
                    StatusServAsig.StatusServAsigId = Convert.ToDecimal(dt.Rows[0]["StatusServAsigId"].ToString());
                    StatusServAsig.StatusServAsigNombre = Convert.ToString(dt.Rows[0]["StatusServAsigNombre"].ToString());
                    ServAsig.statusServAsig = StatusServAsig;
                    sqlCmd.Parameters.Clear();
                    da.Dispose();
                    dt.Dispose();
                    SqlCommand sqlCmd1 = new SqlCommand("[VerServAsigCosto_ConceptoCosto]", conexion);
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.CommandTimeout = 0;
                    sqlCmd1.Parameters.AddWithValue("@ServAsigId", (dt.Rows[0]["ServAsigId"].ToString()));
                    sqlCmd1.Parameters.AddWithValue("@lang", lang);
                    SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    List<ServAsigCosto> lst = Conversor.toServAsigCosto(dt1.Select());
                    ServAsig.servAsigCosto = lst;
                    ///////////////////////////////////////////// ver portcontenido
                    ServiciosWeb.Datos.Modelo.Respuesta<PostCont> RespuestaCont = new ServiciosWeb.Datos.Modelo.Respuesta<PostCont>();
                    PostId = ObtenerPostId(conexion, (dt.Rows[0]["ServAsigId"].ToString()));
                    RespuestaCont = postController.GetPostContenido(PostId);
                    ServAsig.post = RespuestaCont.valor;

                }

                ////////////////////////////////

                ////////////////////////////////

                RespuestaServAsig.estado = 1;
                RespuestaServAsig.valor = ServAsig;
                RespuestaServAsig.mensaje = "OK";

                return RespuestaServAsig;
            }
            catch (Exception ex)
            {
                RespuestaServAsig.estado = 2;
                RespuestaServAsig.valor = null;
                RespuestaServAsig.mensaje = ex.Message;
                return RespuestaServAsig;
            }
            finally
            {
                conexion.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////




        }
        ////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("api/ServAsigCosto")]
        public ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.ServAsigCosto>> _7GetSServAsigCosto(string ServAsigId, string lang)
        {

            ///////////////////////////////////////////Post
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();

                ////////////////////////////////////////////
                SqlCommand sqlCmd1 = new SqlCommand("[VerServAsigCosto_ConceptoCosto]", conexion);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.CommandTimeout = 0;
                sqlCmd1.Parameters.AddWithValue("@ServAsigId", ServAsigId);
                sqlCmd1.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                List<ServAsigCosto> lst = Conversor.toServAsigCosto(dt1.Select());


                /////////////////////////////////////////////    


                ////////////////////////////////

                RespuestaServAsigCosto.estado = 1;
                RespuestaServAsigCosto.valor = lst;
                RespuestaServAsigCosto.mensaje = "OK";

                return RespuestaServAsigCosto;
            }
            catch (Exception ex)
            {
                RespuestaServAsigCosto.estado = 2;
                RespuestaServAsigCosto.valor = null;
                RespuestaServAsig.mensaje = ex.Message;
                return RespuestaServAsigCosto;
            }
            finally
            {
                conexion.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////




        }
        ////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Actualizacion
        /// </summary>
        /// <param name="sa"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        //  [ResponseType(typeof(ServiciosWeb.WepApi.Models.ServAsig))]
        // [HttpPut]
        //[Route("api/ServAsig2")]






        [ResponseType(typeof(ServiciosWeb.WepApi.Models.ServAsig))]
        [HttpPut]
        [Route("api/ServAsig")]
        public async Task<ServiciosWeb.Datos.Modelo.Respuesta<ServiciosWeb.WepApi.Models.ServAsig>> _6PutServAsig(ServiciosWeb.WepApi.Models.ServAsig sa, string lang)
        {

            try
            {
                string ServicioPersonaURLFoto = ""; string IdCosto = ""; PostController postController = new PostController(); int resPC = 0; int respPago = 0; decimal Saldo = -1; decimal PersonaId = -1; decimal ImporteTotal = 0;
                decimal ImporteFormatoPagoExpress = 0;
                if (conexion.State != ConnectionState.Open) conexion.Open();
                if (sa.StatusServAsigId == 4)
                {
                    using (SqlTransaction DataTransactionCom = conexion.BeginTransaction(IsolationLevel.Unspecified))
                    {
                        if (sa.ServAsigPagaCliente == true)
                        {
                            PersonaId = ObtenerPersonaId(sa.RequiereServicioId, conexion, DataTransactionCom); Saldo = ValidarSaldo(PersonaId, DataTransactionCom); ImporteTotal = Convert.ToDecimal(sa.ServAsigCostoTotal);

                        }
                        else {
                            Saldo = ValidarSaldo(sa.ProveedorId, DataTransactionCom); ImporteTotal = ObtenerImporte(sa.ServAsigId, DataTransactionCom);

                        }

                        if (Saldo >= ImporteTotal)
                        {
                            SqlCommand sqlCmd = new SqlCommand("ActualizarServAsig", conexion);sqlCmd.CommandTimeout = 0; ; sqlCmd.CommandType = CommandType.StoredProcedure; sqlCmd.Parameters.AddWithValue("@ServAsigId", sa.ServAsigId); sqlCmd.Parameters.AddWithValue("@ProveedorId", sa.ProveedorId); sqlCmd.Parameters.AddWithValue("@ServAsigFHUbicacion", sa.ServAsigFHUbicacion); sqlCmd.Parameters.AddWithValue("@ServAsigFHEstimadaLlegada", sa.ServAsigFHEstimadaLlegada); if (sa.ServAsigFHInicio == null) { sqlCmd.Parameters.Add("@ServAsigFHInicio", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHInicio", sa.ServAsigFHInicio); } if (sa.ServAsigFHFin == null) { sqlCmd.Parameters.Add("@ServAsigFHFin", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHFin", sa.ServAsigFHFin); } if (sa.ServAsigFHPago == null) { sqlCmd.Parameters.Add("@ServAsigFHPago", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHPago", sa.ServAsigFHPago); } if (sa.ServAsigCostoTotal == null) { sqlCmd.Parameters.Add("@ServAsigCostoTotal", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigCostoTotal", sa.ServAsigCostoTotal); }
                            if (sa.StatusServAsigId == null)
                            { sqlCmd.Parameters.Add("@StatusServAsigId", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@StatusServAsigId", sa.StatusServAsigId); }
                            if (sa.RequiereServicioId == null) { sqlCmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioId", sa.RequiereServicioId); } if (sa.ServAsigPagaCliente == null) { sqlCmd.Parameters.Add("@ServAsigPagaCliente", SqlDbType.Bit).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigPagaCliente", sa.ServAsigPagaCliente); } sqlCmd.Transaction = DataTransactionCom; int resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());

                            if ((sa.post != null && sa.post.PostId == 0))
                            {
                                foreach (ServAsigCosto i in sa.servAsigCosto)
                                {
                                    IdCosto = ObtenerId(conexion, "servAsigCosto", DataTransactionCom);

                                    SqlCommand sqlCmd1 = new SqlCommand("[InsertarservAsigCosto]", conexion);
                                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                                    sqlCmd1.CommandTimeout = 0;
                                    sqlCmd1.Parameters.AddWithValue("@ServAsigCostoId", IdCosto);
                                    sqlCmd1.Parameters.AddWithValue("@ServAsigId", sa.ServAsigId); sqlCmd1.Parameters.AddWithValue("@ConceptoCostoId", i.ConceptoCostoId); sqlCmd1.Parameters.AddWithValue("@ServAsigCostoValor", i.ServAsigCostoValor); sqlCmd1.Transaction = DataTransactionCom; resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());

                                }
                            }
                            else //actualizamos el Servicio Asignado costo
                            {
                                int cont = 0;
                                foreach (ServAsigCosto i in sa.servAsigCosto)
                                {
                                    SqlCommand sqlCmd1 = new SqlCommand("[ActualizarservAsigCosto]", conexion);
                                    sqlCmd1.CommandTimeout = 0;
                                    sqlCmd1.CommandType = CommandType.StoredProcedure; sqlCmd1.Parameters.AddWithValue("@ServAsigCostoId", sa.servAsigCosto[cont].ServAsigCostoId); sqlCmd1.Parameters.AddWithValue("@ServAsigId", sa.ServAsigId); sqlCmd1.Parameters.AddWithValue("@ConceptoCostoId", i.ConceptoCostoId); sqlCmd1.Parameters.AddWithValue("@ServAsigCostoValor", i.ServAsigCostoValor); sqlCmd1.Transaction = DataTransactionCom; cont = cont + 1; resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());

                                }
                            }
                            if (sa.ServAsigPagaCliente == true)
                            {
                                respPago = _9RealizarPagoCliente(Convert.ToDecimal(sa.ServAsigCostoTotal), sa.RequiereServicioId, sa.ServAsigId, sa.ProveedorId, PersonaId, DataTransactionCom);
                            }
                            else
                            {
                                respPago = _11RealizarPagoProveedor(Convert.ToDecimal(sa.ServAsigCostoTotal), sa.RequiereServicioId, sa.ServAsigId, sa.ProveedorId, PersonaId, DataTransactionCom);
                            }

                            if (respPago > 0)
                            {
                                if (resp > 0 && resPC > 0 && sa.ServAsigPagaCliente == true)
                                {
                              //      PostCont post = new PostCont(); ServicioPersonaURLFoto = postController.SubirArchivo_MediaIcons_MediaPost(sa.ProveedorId, DataTransactionCom, conexion);
                                    DataTransactionCom.Commit();
                                    List<PostContenido> ListPostContenido = new List<PostContenido>(); PostContenido postContenido = new PostContenido(); postContenido.PostContenidoId = 1; postContenido.PostContenidoImagen = ServicioPersonaURLFoto; postContenido.PostContenidoVisible = true; ListPostContenido.Add(postContenido);
                                    if (sa.post == null)//(post.PostId == 0)
                                    {
                                    //    post.TipoPostId = 1; post.ServAsigId = sa.ServAsigId; post.PostFechaInsercion = DateTime.Now; post.PostEnlace = " "; post.PostContenidoLast = 0;
                                      //  post.PostCompartidoLast = 0; post.PostDescripcion = " "; post.PostLikesLast = 0; post.PersonaPostId = PersonaId; post.PostContenido = ListPostContenido;
                                      //  postController.PostPostContenido(post);
                                    }
                                    else
                                    {
                                        //post = sa.post; postContenido.PostId = post.PostId;
                                        if (sa.post.PostAutorizaPublicacionImagen == true)
                                        {
                                         ///   postController.PostContenido(postContenido); postController.ActualizarPostContenido(post);
                                        }
                                        else
                                        {
                                          //  post.TipoPostId = 1; post.ServAsigId = sa.ServAsigId; post.PostFechaInsercion = DateTime.Now; post.PostEnlace = " "; post.PostContenidoLast = 0;
                                         //   post.PostCompartidoLast = 0; post.PostDescripcion = " "; post.PostLikesLast = 0; post.PersonaPostId = PersonaId; post.PostContenido = ListPostContenido;
                                          //  postController.PostPostContenido(post);
                                        }


                                    }


                                    List<BE.Persona> lstPersonaServicioPagado = contManager.ListadoTokenClientePagado(sa.ServAsigId);
                                    await EnviarNotificacionesAsyncV3(lstPersonaServicioPagado, "Pagado", lang, null, "");


                                }
                                else
                                {
                                    DataTransactionCom.Commit();
                                }
                                ////////////////////////////////
                                RespuestaServAsig = _5GetServAsig(sa.ServAsigId, lang);
                            }
                            if (respPago == 0)
                            {

                                DataTransactionCom.Rollback();
                            }




                            ////////////////////////////////////////////////////////////////////ACTUALIZACION DE Pagado
                        }
                        else
                        {


                            RespuestaServAsig.estado = 2;
                            RespuestaServAsig.valor = null;
                            RespuestaServAsig.mensaje = "Cliente sin saldo";


                        }



                    }


                }
                else
                {

                    BE.Persona PersonaProveedor = contManager.BuscarPersonaxId(sa.ProveedorId);
                    BE.RequiereServicio rs = new BE.RequiereServicio();
                    if (sa.ServAsigFHPago != null)
                    {
                        PostCont post = new PostCont();
                        ServicioPersonaURLFoto = postController.SubirArchivo_MediaIcons_MediaPost(sa.ServAsigId, null, conexion);
                        List<PostContenido> ListPostContenido = new List<PostContenido>();
                        PostContenido postContenido = new PostContenido();
                        postContenido.PostContenidoId = 1;
                        postContenido.PostContenidoImagen = ServicioPersonaURLFoto;
                        postContenido.PostContenidoVisible = true;
                        ListPostContenido.Add(postContenido);
                        post = sa.post;
                        postContenido.PostId = post.PostId;
                        postController.PostContenido(postContenido);
                        postController.ActualizarPostContenido(post);                      
                     }
                 
                    BE.Servicio S = new BE.Servicio();
                    Respuesta r = new Respuesta();
                    r = contManager.verRequiereServicioXid(sa.RequiereServicioId, lang);
                    rs = (BE.RequiereServicio)r.valor;
                    BE.Persona Persona = contManager.BuscarPersonaxId(rs.PersonaId);
                    RespuestaServAsig = _10PutServAsig(sa, lang);
                    if (sa.ServAsigFHFin != null && sa.StatusServAsigId == 3 )
                    {
                                        
                       if (sa.ServAsigFHPago != null)
                        {
                                                     
                            DataSet datos = new DataSet();
                            datos = ObtenerImporte_y_Calificacion(sa.ServAsigId);
                            decimal ImporteProveedor = Convert.ToDecimal(datos.Tables[0].Rows[0]["ImporteProveedor"].ToString());
                            decimal calificacion = Convert.ToDecimal(datos.Tables[0].Rows[0]["PostCalificacion"].ToString());
                            if (calificacion > 0)
                            {
                                await EnviarNotificacionesAsyncCliente(PersonaProveedor, "ProveedorFinServicioFinalNotificacion", PersonaProveedor.PersonaIdioma, rs, ImporteProveedor, calificacion);

                                sa.servAsigCalificado = true;
                            }
                            if (calificacion == 0)
                            {
                                List<BE.Persona> lstPersonaServicioFinalizado = contManager.ListadoPersonaFinServicio(sa.ServAsigId);
                                //  await EnviarNotificacionesAsyncV3(lstPersonaServicioFinalizado, "ClienteFinServicio", lang, rs, "", sa.RequiereServicioId);
                                //    await OfertaFirebase(rs.RequiereServicioId, Convert.ToInt32(rs.PersonaId), Convert.ToString(sa.StatusServAsigId), "recibido", "Requerimientos");
                                await EnviarNotificacionesAsyncCliente(Persona, "ClienteFinServicio", Persona.PersonaIdioma, rs, Convert.ToDecimal(sa.ServAsigCostoTotal));
                                await OfertaFirebase(rs.RequiereServicioId, Convert.ToInt32(rs.PersonaId), "3", "recibido", "Requerimientos");
                                await EnviarNotificacionesAsyncCliente(PersonaProveedor, "ProveedorFinServicioFinal", PersonaProveedor.PersonaIdioma, rs, ImporteProveedor, calificacion);

                                sa.servAsigCalificado = false;
                            }

                        }


                        ///////////////////////////////
                        /*  if (sa.ServAsigFHPago == null)
                           {
                               await EnviarNotificacionesAsyncCliente(Persona, "ClienteFinServicio",Persona.PersonaIdioma, rs, Convert.ToDecimal(sa.ServAsigCostoTotal));
                               await OfertaFirebase(rs.RequiereServicioId, Convert.ToInt32(rs.PersonaId), "3", "recibido", "Requerimientos");

                           }*/


                    }
                    if (sa.StatusServAsigId == 2)
                    {

                        await EnviarNotificacionesAsyncCliente(Persona, "ClienteInicioServicio", lang, rs);
                        await OfertaFirebase(rs.RequiereServicioId, Convert.ToInt32(rs.PersonaId), "2", "recibido", "Requerimientos");

                    }
                    RespuestaServAsig = _10PutServAsig(sa, lang);
                }



                //////////////////////////////////////////////////////////////////////////////////////




            }
            catch (Exception ex)
            {
                RespuestaServAsig.estado = 2;
                RespuestaServAsig.valor = null;
                RespuestaServAsig.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaServAsig;
        }






        public async Task<ServiciosWeb.Datos.Modelo.Respuesta<ServiciosWeb.WepApi.Models.ServAsig>> _6PutServAsigV2(BE.ServAsig sa, string lang)
        {

            try
            {
                string ServicioPersonaURLFoto = ""; string IdCosto = ""; PostController postController = new PostController(); int resPC = 0; int respPago = 0; decimal Saldo = -1; decimal PersonaId = -1; decimal ImporteTotal = 0;
                decimal ImporteFormatoPagoExpress = 0;
              
                  //  RespuestaServAsig = _10PutServAsig(sa, lang);
                    /////////////////////////ENVIANDO NOTIFICACION DE FINALIZAR SERVICIO
                    BE.RequiereServicio rs = new BE.RequiereServicio();
                    BE.Servicio S = new BE.Servicio();
                    Respuesta r = new Respuesta();
                    r = contManager.verRequiereServicioXid(sa.RequiereServicioId, lang);

                    rs = (BE.RequiereServicio)r.valor;
                    BE.Persona Persona = contManager.BuscarPersonaxId(rs.PersonaId);
                    BE.Persona PersonaProveedor = contManager.BuscarPersonaxId(sa.ProveedorId);
                    ////////////////////////////////
                    if (sa.ServAsigFHFin != null && sa.StatusServAsigId == 3)
                    {

                        List<BE.Persona> lstPersonaServicioFinalizado = contManager.ListadoPersonaFinServicio(sa.ServAsigId);
                  //      await EnviarNotificacionesAsyncV2(lstPersonaServicioFinalizado, "ClienteFinServicioFinal", lang, null, "", sa.RequiereServicioId);
                        /////Enviando al proveedor
                        if (sa.ServAsigFHPago != null)
                        {
                            DataSet datos = new DataSet();
                            datos = ObtenerImporte_y_Calificacion(sa.ServAsigId);
                            decimal ImporteProveedor = Convert.ToDecimal(datos.Tables[0].Rows[0]["ImporteProveedor"].ToString());
                            decimal calificacion = Convert.ToDecimal(datos.Tables[0].Rows[0]["PostCalificacion"].ToString());
                            await EnviarNotificacionesAsyncCliente(PersonaProveedor, "ProveedorFinServicioFinal", lang, rs, ImporteProveedor, calificacion);

                        }

                        ///////////////////////////////
                        if (sa.ServAsigFHPago == null)
                        {
                            await EnviarNotificacionesAsyncCliente(Persona, "ClienteFinServicio", lang, rs, Convert.ToDecimal(sa.ServAsigCostoTotal));

                        }


                    }
                    if (sa.StatusServAsigId == 2)
                    {

                        await EnviarNotificacionesAsyncCliente(Persona, "ClienteInicioServicio", lang, rs);

                    }

                


            }
            catch (Exception ex)
            {
                RespuestaServAsig.estado = 2;
                RespuestaServAsig.valor = null;
                RespuestaServAsig.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaServAsig;
        }


        [HttpPost]
        [Route("api/RealizarPagoServicio")]
        public int _9RealizarPagoCliente(decimal ServAsigCostoTotal, string RequiereServicioId, string ServAsigId, decimal ProveedorId, decimal PersonaId, SqlTransaction DataTransactionCom)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            int resp = 0;

            try
            {

                ///////////////////////Insertando detalle billetera
                SqlCommand sqlCmd = new SqlCommand("[InsertarMedioPago_Billetera2]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", ServAsigId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                sqlCmd.Parameters.AddWithValue("@ProveedorId", ProveedorId);
                sqlCmd.Parameters.AddWithValue("@ServAsigCostoTotal", ServAsigCostoTotal);
                if (DataTransactionCom != null)
                    sqlCmd.Transaction = DataTransactionCom;
                resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());






            }

            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (DataTransactionCom == null)
                {
                    conexion.Close();
                }
            }


            return resp;





        }

        [HttpPost]
        [Route("api/RealizarPagoServicio2")]
        public int _9RealizarPagoCliente2(decimal ServAsigCostoTotal, string RequiereServicioId, string ServAsigId, decimal ProveedorId, decimal PersonaId, SqlTransaction DataTransactionCom)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();


            try
            {



                Respuesta resp = new Respuesta();
                resp = contManager.RegistrarPagoCliente(ServAsigId, DataTransactionCom);
                //return Ok(resp);
                //return 1;



            }

            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (DataTransactionCom == null)
                {
                    conexion.Close();
                }
            }


            return 1;





        }

        public int _11RealizarPagoProveedor(decimal ServAsigCostoTotal, string RequiereServicioId, string ServAsigId, decimal ProveedorId, decimal PersonaId, SqlTransaction DataTransactionCom)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            int resp = 0;

            try
            {


                ///////////////////////Insertando detalle billetera
                SqlCommand sqlCmd = new SqlCommand("[InsertarMedioPago_Billetera_Proveedor2]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", ServAsigId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                sqlCmd.Parameters.AddWithValue("@ProveedorId", ProveedorId);
                sqlCmd.Parameters.AddWithValue("@ServAsigCostoTotal", ServAsigCostoTotal);
                sqlCmd.Parameters.AddWithValue("@BilleteraObservacion", "");
                if (DataTransactionCom != null)
                    sqlCmd.Transaction = DataTransactionCom;
                resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());

            }

            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (DataTransactionCom == null)
                {
                    conexion.Close();
                }
            }


            return resp;





        }

        public int _12RegistroPagoPendienteProveedor(string ServAsigId, decimal StatusServAsigId, decimal ImporteFormatoPagoExpress, SqlTransaction DataTransactionCom)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            int resp = 0;
            string Importe = "";
            decimal pagoAerviciovalor = 0;
            string pagoAerviciovPE = "";
            try
            {

                pagoAerviciovPE = _13ObtenerImporteFormatoPagoExpress(Convert.ToString(ImporteFormatoPagoExpress));

                ///////////////////////Insertando detalle billetera
                SqlCommand sqlCmd = new SqlCommand("[RegistroPagoPendienteProveedor]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", ServAsigId);
                sqlCmd.Parameters.AddWithValue("@StatusServAsigId", StatusServAsigId);
                sqlCmd.Parameters.AddWithValue("@Importe", Convert.ToString(Convert.ToString(ImporteFormatoPagoExpress)));
                sqlCmd.Parameters.AddWithValue("@ImportePagoServicioPE", pagoAerviciovPE);
                sqlCmd.Parameters.AddWithValue("@ImportePagoServicioValor", ImporteFormatoPagoExpress);

                if (DataTransactionCom != null)
                    sqlCmd.Transaction = DataTransactionCom;
                resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());

            }

            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (DataTransactionCom == null)
                {
                    conexion.Close();
                }
            }


            return resp;





        }
        public decimal _14ObtenerPorcentajeDelServicio(string ServAsigId, SqlTransaction DataTransactionCom)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            decimal ServicioPorcentaje = 0;
            string Importe = "";
            try
            {
                ///////////////////////Insertando detalle billetera
                SqlCommand sqlCmd = new SqlCommand("[ObtenerPorcentajeDelServicio]", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", ServAsigId);

                if (DataTransactionCom != null)
                    sqlCmd.Transaction = DataTransactionCom;
                ServicioPorcentaje = Convert.ToDecimal(sqlCmd.ExecuteScalar());

            }

            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                if (DataTransactionCom == null)
                {
                    conexion.Close();
                }
            }


            return ServicioPorcentaje;





        }
        [HttpGet]
        [Route("api/VerImporte")]
        public string _13ObtenerImporteFormatoPagoExpress(string ImporteFormatoPagoExpress)
        {

            int resp = 0;
            string Importe = "";
            try
            {
                string[] valor;

                valor = Convert.ToString(ImporteFormatoPagoExpress).Split(',');
                //  valor = Convert.ToString(ImporteFormatoPagoExpress).Split('.');
                if (valor[1].Length == 1)
                    valor[1] = valor[1].Insert(1, "0");
                else
                    valor[1] = valor[1].Substring(0, 2);

                return valor[0] + valor[1];
                ///////////////////////Insertando detalle billetera


            }

            catch (Exception ex)
            {

                throw;
            }





        }









        public ServiciosWeb.Datos.Modelo.Respuesta<ServiciosWeb.WepApi.Models.ServAsig> _10PutServAsig(ServiciosWeb.WepApi.Models.ServAsig sa, string lang)
        {
            ///METODO QUE REALIZA EL INICIAR Y FINALIZAR SERVICIO

            try
            {
                string IdCosto = "";
                PostController postController = new PostController();
                int resPC = 0;
                if (conexion.State != ConnectionState.Open) conexion.Open();

                SqlCommand sqlCmd = new SqlCommand("ActualizarServAsig2", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout =0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", sa.ServAsigId);
                sqlCmd.Parameters.AddWithValue("@ProveedorId", sa.ProveedorId);
                sqlCmd.Parameters.AddWithValue("@ServAsigFHUbicacion", sa.ServAsigFHUbicacion);
                sqlCmd.Parameters.AddWithValue("@ServAsigFHEstimadaLlegada", sa.ServAsigFHEstimadaLlegada);
                if (sa.ServAsigFHInicio == null) { sqlCmd.Parameters.Add("@ServAsigFHInicio", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHInicio", sa.ServAsigFHInicio); }
                if (sa.ServAsigFHFin == null) { sqlCmd.Parameters.Add("@ServAsigFHFin", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHFin", sa.ServAsigFHFin); }
                if (sa.ServAsigFHPago == null) { sqlCmd.Parameters.Add("@ServAsigFHPago", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigFHPago", sa.ServAsigFHPago); }
                if (sa.ServAsigCostoTotal == null) { sqlCmd.Parameters.Add("@ServAsigCostoTotal", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigCostoTotal", sa.ServAsigCostoTotal); }
                if (sa.StatusServAsigId == null) { sqlCmd.Parameters.Add("@StatusServAsigId", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@StatusServAsigId", sa.StatusServAsigId); }
                if (sa.RequiereServicioId == null) { sqlCmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioId", sa.RequiereServicioId); }
                if (sa.ServAsigPagaCliente == null) { sqlCmd.Parameters.Add("@ServAsigPagaCliente", SqlDbType.Bit).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigPagaCliente", sa.ServAsigPagaCliente); }
                if (sa.servAsigCalificado == null) { sqlCmd.Parameters.Add("@ServAsigCalificado", SqlDbType.Bit).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServAsigCalificado", sa.servAsigCalificado); }

                //////INSERTANDO SERVICIO ASGINADO COSTO                                

 

                if ((sa.post != null && sa.post.PostId == 0))
                {
                    int io = 1;
                    foreach (ServAsigCosto i in sa.servAsigCosto)
                    {

                        decimal valorp = i.ServAsigCostoValor;

                     
                        


                        //PostContenido  posCon= (postCont.PostContenido[0]);
                        ///////////////////

                        IdCosto = ObtenerId(conexion, "servAsigCosto", null);
                        ////////////////////
                        SqlCommand sqlCmd1 = new SqlCommand("[InsertarservAsigCosto]", conexion);
                        sqlCmd1.CommandType = CommandType.StoredProcedure;
                        sqlCmd1.CommandTimeout = 0;
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoId", IdCosto);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigId", sa.ServAsigId);
                        sqlCmd1.Parameters.AddWithValue("@ConceptoCostoId", i.ConceptoCostoId);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoValor", valorp); ;

                        resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());
                        io = io + 1;
                        ////////////////////////////////////////////////////////////////////////////
                    }
                }
                else //actualizamos el Servicio Asignado costo
                {
                    int cont = 0;
                    int io = 1;
                    foreach (ServAsigCosto i in sa.servAsigCosto)
                    {

                        decimal valorp = i.ServAsigCostoValor;

                    
                        //  PostContenido  posCon= (postCont.PostContenido[0]);
                        ////////////////////
                        SqlCommand sqlCmd1 = new SqlCommand("[ActualizarservAsigCosto]", conexion);
                        sqlCmd1.CommandType = CommandType.StoredProcedure;
                        sqlCmd1.CommandTimeout = 0;
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoId", sa.servAsigCosto[cont].ServAsigCostoId);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigId", sa.ServAsigId);
                        sqlCmd1.Parameters.AddWithValue("@ConceptoCostoId", i.ConceptoCostoId);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoValor", valorp);
                        cont = cont + 1;
                        resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());
                        io = io + 1;
                        ////////////////////////////////////////////////////////////////////////////
                    }
                    if (cont == 0)
                    {
                        resPC = 1;
                    }

                }

                ////////////////////////////////////////////////
                int resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
                if (resp > 0 && resPC > 0)
                {
                    if (sa.post != null)
                    {
                        PostCont post = new PostCont();
                        post = sa.post;
                        post.ServAsigId = sa.ServAsigId;
                        if (post.PostId == 0)
                        {
                            postController.PostPostContenido(post);
                        }
                        else
                        {
                            postController.ActualizarPostContenido(post);
                        }


                        //if ((post.PostContenido.Count>0))
                        //{

                        //   postController.PostPostContenido(post);

                        //}
                        //else
                        //{

                        ////    postController.ActualizarPostContenido(post);
                        //}

                    }
                    ////////////////////////////////ENVIANDO NOTIFICACIONES


                    ///////////////////////////////////////////////////////////


                    RespuestaServAsig = _5GetServAsig(sa.ServAsigId, lang);


                }

                ////////////////////////////////////////////////////////////////////ACTUALIZACION DE Pagado

            }
            catch (Exception ex)
            {
                RespuestaServAsig.estado = 2;
                RespuestaServAsig.valor = null;
                RespuestaServAsig.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaServAsig;
        }





                       
        [HttpPost]
        [Route("api/saveServAsig")]////Metodo unificado en requiereServicio
                                   // [ResponseType(typeof(ServiciosWeb.WepApi.Models.ServAsig))]
                                   // [HttpPut]
                                   //[Route("api/ServAsig")]
        public async Task<Respuesta> SaveServicioAsig(BE.ServAsig servAsig, string lang)
        {
            Respuesta resp = new Respuesta();
            servAsig.TipoEstado = BE.TipoEstado.Modificar;
            resp = contManager.saveServAsig(ref servAsig);
            if (resp.estado == 1)
            {


            }
            return resp;
        }



        //  [HttpPost]
        // [Route("api/saveRequiereServicioProveedores")]////Metodo unificado en requiereServicio
        [HttpPut]
        [Route("api/RequiereServicioProveedores")]////Metodo unificado en requiereServicio

        public async Task<Respuesta> saveRequiereServicioProveedores(BE.RequiereServicioProveedores requiereServicioProveedores, string lang)
        {

            Respuesta resp = new Respuesta();
            requiereServicioProveedores.TipoEstado = BE.TipoEstado.Modificar;
            requiereServicioProveedores.requiereServicio.TipoEstado = BE.TipoEstado.Modificar;
            resp = contManager.saveRequiereServicioProveedores(ref requiereServicioProveedores);
            if (resp.estado == 1)
            {

                /////SI ACTUALIZO REQUIERE SERVICIO PROVEDORES StatusRequiere COTIZADO
                if ((requiereServicioProveedores.TipoEstado == BE.TipoEstado.Modificar))
                {
                    Respuesta resp1 = new Respuesta();
                    BE.RequiereServicio rs = new BE.RequiereServicio();
                    resp1 = contManager.verRequiereServicioXid(requiereServicioProveedores.RequiereServicioId, lang);
                    rs = (BE.RequiereServicio)resp1.valor;

                    if (requiereServicioProveedores.StatusRequiereId == 2)//Cotizado  Verdadero
                    {
                        List<BE.Persona> lstPersonasProvCot = contManager.ListadoProveedoresCotizados(requiereServicioProveedores.RequiereServicioId);
                        await EnviarNotificacionesAsyncV3(lstPersonasProvCot, "CotizacionProveedor", lang, rs, "");
                        BE.Persona persona = contManager.BuscarPersonaxId(rs.PersonaId);
                        await EnviarNotificacionesAsyncCliente(persona, "ClienteCotizacion", persona.PersonaIdioma, rs);
                        await OfertaFirebase(requiereServicioProveedores.RequiereServicioId,Convert.ToInt32(rs.PersonaId),Convert.ToString(Convert.ToInt32(requiereServicioProveedores.StatusRequiereId)), "recibido","Requerimientos");


                    }
                    if (requiereServicioProveedores.StatusRequiereId == 4)//Adjudicado
                    {
                        //   List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(requiereServicioProveedores.RequiereServicioId, 4);

                        //   await EnviarNotificacionesAsyncV2(lstPersonas, "Adjudicado", lang,rs, "");


                    }


                }

            }
            return resp;
        }

        [HttpPost]
        [Route("api/saveConversacion")]

        public async Task<Respuesta> saveConversacion(BE.Conversacion conversacion, string lang)
        {

            Respuesta resp = new Respuesta();

            Respuesta respReqId = new Respuesta();
            Respuesta respServId = new Respuesta();
            conversacion.TipoEstado = BE.TipoEstado.Insertar;

            resp = contManager.saveConversacion(ref conversacion);
            if (resp.estado == 1)
            {
                BE.Persona PersonaReceptor = contManager.BuscarPersonaxId(conversacion.ConversacionPersonaIdReceptor);
                respServId = contManager.VerServicioAsigXid(conversacion.ServAsigId, lang);
                BE.ServAsig servAsig = (BE.ServAsig)respServId.valor;
                respReqId = contManager.verRequiereServicioXid(servAsig.RequiereServicioId, lang);
                BE.RequiereServicio requiereServicio = (BE.RequiereServicio)respReqId.valor;
                string ReqServId = requiereServicio.RequiereServicioId;
                List<BE.Persona> lstPersona = new List<BE.Persona>();
                lstPersona.Add(PersonaReceptor);
                await EnviarNotificacionesAsyncV3(lstPersona, "Conversacion", lang, requiereServicio,Convert.ToString(conversacion.ConversacionPersonaIdReceptor), ReqServId,conversacion.ConversacionContenido);
            }

            return resp;
        }

        public decimal ValidarSaldo(decimal PersonaId, SqlTransaction DataTransactionCom)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            SqlCommand sqlCmd = new SqlCommand("ObtenerSaldo", conexion);
            sqlCmd.CommandTimeout =0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PersonaId", PersonaId);
            sqlCmd.Transaction = DataTransactionCom;
            decimal Saldo = Convert.ToDecimal(sqlCmd.ExecuteScalar());
            return Saldo;
        }

        public decimal ObtenerImporte(string ServAsigId, SqlTransaction DataTransactionCom)
        {
            decimal ImporteTotal = 0;

            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandTimeout = 0;
             
                cmd.CommandText = "select * from VerImportePagoServicio('" + ServAsigId + "') ";

                if (DataTransactionCom != null)
                    cmd.Transaction = DataTransactionCom;
                ImporteTotal = Convert.ToDecimal(cmd.ExecuteScalar());


            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (DataTransactionCom == null)
                {
                    conexion.Close();
                }
            }
            return ImporteTotal;
        }
        
        public decimal ObtenerPersonaId(string RequiereServicioId, SqlConnection conexion, SqlTransaction DataTransactionCom)
        {
            decimal PersonaId = 0;

            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandTimeout = 0;
                cmd.CommandText = "select PersonaId from RequiereServicio with(nolock) where RequiereServicioId =@RequiereServicioId ";
                cmd.Parameters.Add("@RequiereServicioId", SqlDbType.VarChar).Value = RequiereServicioId;
                if (DataTransactionCom != null)
                    cmd.Transaction = DataTransactionCom;
                PersonaId = Convert.ToDecimal(cmd.ExecuteScalar());


            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (DataTransactionCom == null)
                {
                    conexion.Close();
                }

            }




            return PersonaId;

        }

        //////////////////////////////////////////NOTIFICacion
        /// <summary>
        ///NOTIFICACION
        /// </summary>    
      /*  public async Task<bool> SendPushNotification(string deviceTokens, string title, string body)
        {
            bool sent = false;
            //string ServerKey = "AAAAPATzEYs:APA91bHTtGHkyLSt9LQaIsvbqsD6DlNSH0Qg4_qXbP9wyIBjdqPusViDk2hWWCZwShvh0WjH4MpKaw62woruRCATc15eROUqRJmqCyUOJ8e04CInnxlpTL65uy1_P-TltCiCUz0QlN0x";

            if (deviceTokens.Count() > 0)
            {
                //Object creation

                var messageInformation = new Message()
                {
                    notification = new Notification()
                    {
                        title = title,
                        text = body

                    },
                    // data = data,
                    registration_ids = deviceTokens
                };

                //Object to JSON STRUCTURE => using Newtonsoft.Json;
                string jsonMessage = JsonConvert.SerializeObject(messageInformation);

                /*
                 ------ JSON STRUCTURE ------
                 {
                    notification: {
                                    title: "",
                                    text: ""
                                    },
                    data: {
                            action: "Play",
                            playerId: 5
                            },
                    registration_ids = ["id1", "id2"]
                 }
                 ------ JSON STRUCTURE ------
                 */

                //Create request to Firebase API
              /*  var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);

                request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    sent = result.IsSuccessStatusCode;
                }


                /*using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(FireBasePushNotificationsURL).Result)
                    {
                        using (HttpContent content = request.Content)
                        {
                            var json = content.ReadAsStringAsync().Result;
                            sent = response.IsSuccessStatusCode;
                        }
                    }
                }*/
           /* }

            return sent;
        }*/

        ///////////////////////////////////////////
        [HttpGet]
        [Route("api/NotifyAsync")]
        public async Task<bool> NotifyAsync(decimal PersonaId, string lang)
        {
            try
            {
                string to = "";
                string title = "";
                string body = "";
                DataSet dsTokensCliente = new DataSet();
                DataSet datosNot = new DataSet();
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", ServerKey);

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "701502875637");
                dsTokensCliente = ObtenerPersonaTokenIdCliente(conexion, PersonaId);
                to = dsTokensCliente.Tables[0].Rows[0]["PersonaTokenId"].ToString();
                datosNot = ObtenerDatosNotificacion(lang, "ProvCamino", conexion);
                title = datosNot.Tables[0].Rows[0]["title"].ToString();
                body = datosNot.Tables[0].Rows[0]["body"].ToString();
                var data = new
                {
                    to, // Recipient device token
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Exception thrown in Notify Service: {ex}");
            }
            finally
            {
                conexion.Close();
            }

            return false;
        }

        /// <summary>
        ///CATEGORIA SERVICIO_CATSERV DESTACADO
        /// </summary>

        [HttpGet]
        [Route("api/CatServ_CatServDestacado")]
        public IHttpActionResult CatServ_CatServDestacado(string CategoriaServicioHijoId, decimal CiudadId, string lang)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerCategoriaServicio_ServDestacado", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@CategoriaServicioHijoId", CategoriaServicioHijoId);
                sqlCmd.Parameters.AddWithValue("@CiudadId", CiudadId);
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ServiciosWeb.WepApi.Models.CategoriaServicio CategoriaServicio = new ServiciosWeb.WepApi.Models.CategoriaServicio();
                List<CategoriaServicio> lst = Conversor.toCategoriaServicio(dt.Select());
                ////////////////////////////////////////////////////////////////


                SqlCommand sqlCmd1 = new SqlCommand("VerCategoriaServicioDestacada", conexion);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.CommandTimeout = 0;
                sqlCmd1.Parameters.AddWithValue("@CategoriaServicioHijoId", CategoriaServicioHijoId);
                sqlCmd1.Parameters.AddWithValue("@CiudadId", CiudadId);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                ServiciosWeb.WepApi.Models.CategoriaServicioDestacada CategoriaServicioDest = new ServiciosWeb.WepApi.Models.CategoriaServicioDestacada();
                List<CategoriaServicioDestacada> lstdest = Conversor.toCategoriaServicioDestacada(dt1.Select());
                ///////////////////////////////////////////////////////////////////
                PackCatSer_CatServDesta.categoriaServicio = lst;
                PackCatSer_CatServDesta.categoriaservicioDestacado = lstdest;
                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();

                RespuestaDest.estado = 1;
                RespuestaDest.valor = PackCatSer_CatServDesta;

                ///////////////////////////////////////////////////////////////////////////////////////

                return Ok(RespuestaDest);
            }
            catch (Exception ex)
            {
                RespuestaDest.estado = 2;
                RespuestaDest.valor = null;
                RespuestaDest.mensaje = ex.Message;
                return Ok(RespuestaDest);
            }
            finally
            {
                conexion.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////




        }



        /// <summary>
        ///REQUERIMIENTO SERVICIO_SERVICIO PROVEEDORES[InsertarRequiereServicio]
        /// </summary>

        [HttpGet]
        [Route("api/ReqSev_ServProveedores2")]
        public ServiciosWeb.Datos.Modelo.Respuesta<ReqSev_ServProveedores> GetReqSev_ServProveedores(string RequiereServicioId, string lang)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerrequiereServicio", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ServiciosWeb.WepApi.Models.RequiereServicio requiereServicio = new ServiciosWeb.WepApi.Models.RequiereServicio();

                requiereServicio = Conversor.toRequiereServicio(dt.Rows[0]);
                /*    for (int i = 0; i < dt.Rows.Count; i++)



                     /*   requiereServicio.RequiereServicioId = dt.Rows[i]["RequiereServicioId"].ToString();
                        requiereServicio.PersonaId = Convert.ToDecimal(dt.Rows[i]["PersonaId"].ToString());
                        requiereServicio.RequiereServicioFechaHoraReq = Convert.ToDateTime(dt.Rows[i]["RequiereServicioFechaHoraReq"].ToString());
                        requiereServicio.EstadoReqServId = Convert.ToDecimal(dt.Rows[i]["EstadoReqServId"].ToString());
                        requiereServicio.RequiereServicioFHDeseada = Convert.ToDateTime(dt.Rows[i]["RequiereServicioFHDeseada"].ToString());
                        requiereServicio.RequiereServicioDescripcion = dt.Rows[i]["RequiereServicioDescripcion"].ToString();
                        requiereServicio.RequiereServicioURLFoto1 = dt.Rows[i]["RequiereServicioURLFoto1"].ToString();
                        requiereServicio.RequiereServicioURLFoto2 = dt.Rows[i]["RequiereServicioURLFoto2"].ToString();
                        requiereServicio.RequiereServicioURLFoto3 = dt.Rows[i]["RequiereServicioURLFoto3"].ToString();
                        requiereServicio.RequiereServicioURLVideo = dt.Rows[i]["RequiereServicioURLVideo"].ToString();
                        requiereServicio.RequiereServicioProvLast = Convert.ToDecimal(dt.Rows[i]["RequiereServicioProvLast"].ToString());
                        if (dt.Rows[i]["PersonaDireccionId"].ToString() != "")
                        {
                            requiereServicio.PersonaDireccionId = Convert.ToDecimal(dt.Rows[i]["PersonaDireccionId"].ToString());

                        }
                        if (dt.Rows[i]["ServicioId"].ToString() != "")
                        {
                            requiereServicio.ServicioId = Convert.ToDecimal(dt.Rows[i]["ServicioId"].ToString());
                        }
                        if (dt.Rows[i]["RequiereServicioFechaMod"].ToString() != "")
                        {
                            requiereServicio.RequiereServicioFechaMod = Convert.ToDateTime(dt.Rows[i]["RequiereServicioFechaMod"].ToString());
                        }

                        requiereServicio.servicio = null;*/
                //   }

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido
                SqlCommand sqlCmd1 = new SqlCommand("VerrequiereServicio_ServiProveedores1", conexion);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.CommandTimeout = 0;
                sqlCmd1.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                sqlCmd1.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                List<RequiereServicioProveedoresM> lstProv = Conversor.toRequiereServicioProveedores(dt1.Select());

                sqlCmd1.Parameters.Clear();
                da1.Dispose();
                dt1.Dispose();
                ///////////////////////////////////////////////////////////////////////////////////////
                ReqSev_ServProveedores RSP = new ReqSev_ServProveedores();

                ////////////////////////////////

                RSP.RequiereServicioId = requiereServicio.RequiereServicioId;
                RSP.PersonaId = requiereServicio.PersonaId;
                RSP.RequiereServicioFechaHoraReq = requiereServicio.RequiereServicioFechaHoraReq;
                RSP.EstadoReqServId = requiereServicio.EstadoReqServId;
                RSP.RequiereServicioFHDeseada = requiereServicio.RequiereServicioFHDeseada;
                RSP.RequiereServicioDescripcion = requiereServicio.RequiereServicioDescripcion;
                RSP.RequiereServicioURLFoto1 = requiereServicio.RequiereServicioURLFoto1;
                RSP.RequiereServicioURLFoto2 = requiereServicio.RequiereServicioURLFoto2;
                RSP.RequiereServicioURLFoto3 = requiereServicio.RequiereServicioURLFoto3;
                RSP.RequiereServicioURLVideo = requiereServicio.RequiereServicioURLVideo;
                RSP.RequiereServicioProvLast = requiereServicio.RequiereServicioProvLast;
                RSP.PersonaDireccionId = requiereServicio.PersonaDireccionId;
                RSP.ServicioId = requiereServicio.ServicioId;


                RSP.RequiereServicioProveedores = lstProv;
                ///////////////////////////////

                RespuestaSerProv.valor = RSP;
                RespuestaSerProv.estado = 1;

                RespuestaSerProv.mensaje = "OK";

            }
            catch (Exception ex)
            {
                RespuestaSerProv.estado = 2;
                RespuestaSerProv.valor = null;
                RespuestaSerProv.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////


            return RespuestaSerProv;

        }

        /// <summary>
        ///devuelve la clase ReqSev_ServProveedores, de la lista de estados enviado
        /// </summary>

        [HttpGet]
        [Route("api/ListadoReqSev_ServProveedores")]
        public IHttpActionResult _2ListadoReqSev_ServProveedoresXStatus(string RequiereServicioId, string StatusRequiereId)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerrequiereServicioV2", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ServiciosWeb.WepApi.Models.RequiereServicio requiereServicio = new ServiciosWeb.WepApi.Models.RequiereServicio();


                // for (int i = 0; i < dt.Rows.Count; i++)
                //{
                requiereServicio = Conversor.toRequiereServicio(dt.Rows[0]);

                // }

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido
                SqlCommand sqlCmd1 = new SqlCommand("VerrequiereServicioProvXStatus", conexion);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.CommandTimeout = 0;
                sqlCmd1.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                sqlCmd1.Parameters.AddWithValue("@StatusRequiereId", StatusRequiereId);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                List<RequiereServicioProveedoresM> lstProv = Conversor.toRequiereServicioProveedores(dt1.Select());

                //foreach (RequiereServicioProveedoresM rsp in lstProv )
                //{
                ReqSev_ServProveedores RSP = new ReqSev_ServProveedores();
                DataRow dr = dt.Select(String.Format("PersonaId={0}", requiereServicio.PersonaId )).FirstOrDefault();
                if (dr!=null)
                {
                  
                    if ((dr["ServicioPersonaId"].ToString() != ""))
                    {
                        RSP.serviciopersona_Persona = Conversor.toServicioPersona(dr);

                    }

                }
                //}

                sqlCmd1.Parameters.Clear();
                da1.Dispose();
                dt1.Dispose();
                ///////////////////////////////////////////////////////////////////////////////////////
             

                ////////////////////////////////

                RSP.RequiereServicioId = requiereServicio.RequiereServicioId;
                RSP.PersonaId = requiereServicio.PersonaId;
                RSP.RequiereServicioFechaHoraReq = requiereServicio.RequiereServicioFechaHoraReq;
                RSP.EstadoReqServId = requiereServicio.EstadoReqServId;
                RSP.RequiereServicioFHDeseada = requiereServicio.RequiereServicioFHDeseada;
                RSP.RequiereServicioDescripcion = requiereServicio.RequiereServicioDescripcion;
                RSP.RequiereServicioURLFoto1 = requiereServicio.RequiereServicioURLFoto1;
                RSP.RequiereServicioURLFoto2 = requiereServicio.RequiereServicioURLFoto2;
                RSP.RequiereServicioURLFoto3 = requiereServicio.RequiereServicioURLFoto3;
                RSP.RequiereServicioURLVideo = requiereServicio.RequiereServicioURLVideo;
                RSP.RequiereServicioProvLast = requiereServicio.RequiereServicioProvLast;
                RSP.PersonaDireccionId = requiereServicio.PersonaDireccionId;
                RSP.ServicioId = requiereServicio.ServicioId;


                foreach (RequiereServicioProveedoresM reqprov in lstProv)
                {
                    BE.ServicioPersona serPer = new BE.ServicioPersona();
                   // Conversor.toServicioPersona(dt.Select());
                    // serPer = contManager.VerServicioPersona
                }

                RSP.RequiereServicioProveedores = lstProv;
                ///////////////////////////////

                RespuestaSerProv.valor = RSP;
                RespuestaSerProv.estado = 1;

                RespuestaSerProv.mensaje = "OK";
                return Ok(RespuestaSerProv);
            }
            catch (Exception ex)
            {
                RespuestaSerProv.estado = 2;
                RespuestaSerProv.valor = null;
                RespuestaSerProv.mensaje = ex.Message;
                return Ok(RespuestaSerProv);
            }
            finally
            {
                conexion.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////




        }


        /////////////////////////
        /// <summary>
        ///devuelve la clase ReqSev_ServProveedores, de la lista de estados enviado
        /// </summary>

        [HttpGet]
        [Route("api/ListadoServicioPersona_PersonDirecc")]
        public IHttpActionResult _3ListadoServicioPersona_PersonDireccXPersonaId(string ServicioPersonaId)
        {

            if (conexion.State != ConnectionState.Open) conexion.Open();
            List<ServiciosWeb.WepApi.Models.ServicioPersona> LServPer = new List<ServiciosWeb.WepApi.Models.ServicioPersona>();
            ///////////////////////////////////////////Post
            try
            {


                SqlCommand sqlCmd = new SqlCommand("ListadoServicioPersona_PersonDireccXPersonaId", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServicioPersonaId", ServicioPersonaId);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ServiciosWeb.WepApi.Models.ServicioPersona servicioPersona = new ServiciosWeb.WepApi.Models.ServicioPersona();
                ServiciosWeb.WepApi.Models.PersonaDireccion personaDireccion = new ServiciosWeb.WepApi.Models.PersonaDireccion();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    servicioPersona = Conversor.toServicioPersona(dt.Rows[i]);

                    personaDireccion = Conversor.toPersonaDireccion(dt.Rows[i]);
                    servicioPersona.personaDireccion = personaDireccion;
                    LServPer.Add(servicioPersona);
                }




                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();

                ///////////////////////////////

                RespuestaServicioPersona.valor = LServPer;
                RespuestaServicioPersona.estado = 1;

                RespuestaServicioPersona.mensaje = "OK";
                return Ok(RespuestaServicioPersona);
            }
            catch (Exception ex)
            {
                RespuestaServicioPersona.estado = 2;
                RespuestaServicioPersona.valor = null;
                RespuestaServicioPersona.mensaje = ex.Message;
                return Ok(RespuestaServicioPersona);
            }
            finally
            {
                conexion.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////




        }


        /////////////////////////

        /// <summary>
        ///Obtener la tabala requiere servicio por persona Id
        /// </summary>
        [ResponseType(typeof(RequiereServicio))]
        [HttpGet]
        [Route("api/RequiereServicioProveedor")]
        public ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.RequiereServicio>> _1RequiereServicioProveedor(decimal PersonaId, string lang)
        {

            List<ServiciosWeb.WepApi.Models.RequiereServicio> LReqServ = new List<ServiciosWeb.WepApi.Models.RequiereServicio>();
            ///////////////////////////////////////////Post
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                //////////////////////////////////////////////

                //////////////////////////////////////////////

                SqlCommand sqlCmd = new SqlCommand("VerRequiereServicioPersonaXPersonaId", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@PersonaId", PersonaId);
                sqlCmd.Parameters.AddWithValue("@lang", lang);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string CategoriaServicioFechaHoraMod = "";
                string PersonaDir = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ServiciosWeb.WepApi.Models.RequiereServicio ReqServ = new ServiciosWeb.WepApi.Models.RequiereServicio();
                    ServiciosWeb.WepApi.Models.EstadoReqServ estadoReqServ = new ServiciosWeb.WepApi.Models.EstadoReqServ();
                    /* ServiciosWeb.WepApi.Models.Servicio servicio = new ServiciosWeb.WepApi.Models.Servicio();
                     ServiciosWeb.WepApi.Models.CategoriaServicio catServ = new ServiciosWeb.WepApi.Models.CategoriaServicio();
                     ServiciosWeb.WepApi.Models.EstadoReqServ estadoReqServ = new ServiciosWeb.WepApi.Models.EstadoReqServ();*/
                    //ReqServ.RequiereServicioId = dt.Rows[i][0].ToString();
                    //ReqServ.PersonaId = Convert.ToDecimal(dt.Rows[i][1].ToString());
                    //ReqServ.RequiereServicioFechaHoraReq = Convert.ToDateTime(dt.Rows[i][2].ToString());
                    //ReqServ.RequiereServicioFHCaduca = Convert.ToDateTime(dt.Rows[i][3].ToString());
                    //ReqServ.EstadoReqServId = Convert.ToDecimal(dt.Rows[i][4].ToString());
                    //ReqServ.RequiereServicioFHDeseada = Convert.ToDateTime(dt.Rows[i][5].ToString());
                    //ReqServ.RequiereServicioDescripcion = Convert.ToString(dt.Rows[i][6].ToString());
                    //ReqServ.RequiereServicioURLFoto1 = Convert.ToString(dt.Rows[i][7].ToString());
                    //ReqServ.RequiereServicioURLFoto2 = Convert.ToString(dt.Rows[i][8].ToString());
                    //ReqServ.RequiereServicioURLFoto3 = Convert.ToString(dt.Rows[i][9].ToString());
                    //ReqServ.RequiereServicioURLVideo = Convert.ToString(dt.Rows[i][10].ToString());
                    //ReqServ.RequiereServicioProvLast = Convert.ToDecimal(dt.Rows[i][11].ToString());

                    ReqServ = Conversor.toRequiereServicio(dt.Rows[i]);
                    ReqServ.servicio = Conversor.toServicio(dt.Rows[i]); ReqServ.servicio.categoriaServicio = Conversor.toCategoriaServicio(dt.Rows[i]);
                    estadoReqServ.EstadoReqServId = Convert.ToDecimal(dt.Rows[i]["EstadoReqServId"].ToString());
                    estadoReqServ.EstadoReqServNombre = Convert.ToString(dt.Rows[i]["EstadoReqServNombre"].ToString());
                    ReqServ.estadoReqServ = estadoReqServ;
                    ReqServ.personaDireccion = Conversor.toPersonaDireccion(dt.Rows[i]);

                    /////////////////////ver requiere servicio proveedores

                    SqlCommand sqlCmd1 = new SqlCommand("VerrequiereServicio_ServiProveedores1", conexion);
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.CommandTimeout = 0;
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioId", ReqServ.RequiereServicioId);
                    sqlCmd1.Parameters.AddWithValue("@PersonaId", PersonaId);
                    sqlCmd1.Parameters.AddWithValue("@lang", lang);

                    SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    List<RequiereServicioProveedoresM> lstProv = Conversor.toRequiereServicioProveedores(dt1.Select());
                    ReqServ.RequiereServicioProveedores = lstProv;
                    ReqServ.persona = Conversor.toPersona(dt.Rows[i]);

                    if (dt.Rows[i]["ServAsigId"].ToString() != "")
                    {
                        ReqServ.servAsig = Conversor.toServAsig(dt.Rows[i]);
                        ReqServ.servAsig.statusServAsig = Conversor.toStatusServAsig(dt.Rows[i]);
                    }


                  
                    sqlCmd1.Parameters.Clear();
                    da1.Dispose();
                    dt1.Dispose();
                    ///////////////////////////////////////////////////////////////////////////////////////
                    LReqServ.Add(ReqServ);
                }

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                RespuestaServ.estado = 1;
                RespuestaServ.valor = LReqServ;
                RespuestaServ.mensaje = "OK";


            }
            catch (Exception ex)
            {
                RespuestaServ.estado = 2;
                RespuestaServ.valor = null;
                RespuestaServ.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }

            return RespuestaServ;
            ////////////////////////////////////////////////////////////////////////////////

        }

        /////////////////////////////////////REQUIERE SERVICIO

        /// <summary>
        ///REQUERIMIENTO SERVICIO_SERVICIO 
        /// </summary>
        [ResponseType(typeof(ServiciosWeb.WepApi.Models.RequiereServicio))]

        [HttpGet]
        [Route("api/ReqSev_ServicioCliente")]////METODO CON ERRORES VERIFICAR ANTES DE OPTIMIZAR 
        public ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.RequiereServicio>> ReqSev_ServicioCliente(decimal PersonaId, string lang)
        {
            List<ServiciosWeb.WepApi.Models.RequiereServicio> LReqServ = new List<ServiciosWeb.WepApi.Models.RequiereServicio>();
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("VerRequiereServicioPersona", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                sqlCmd.Parameters.AddWithValue("@PersonaId", PersonaId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string CategoriaServicioFechaHoraMod = "";
                string PersonaDir = "";


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ServiciosWeb.WepApi.Models.RequiereServicio ReqServ = new ServiciosWeb.WepApi.Models.RequiereServicio();
                    ServiciosWeb.WepApi.Models.EstadoReqServ estadoReqServ = new ServiciosWeb.WepApi.Models.EstadoReqServ();


                    ReqServ = Conversor.toRequiereServicio(dt.Rows[i]);
                    ReqServ.servicio = Conversor.toServicio(dt.Rows[i]); ReqServ.servicio.categoriaServicio = Conversor.toCategoriaServicio(dt.Rows[i]);
                    estadoReqServ.EstadoReqServId = Convert.ToDecimal(dt.Rows[i]["EstadoReqServId"].ToString());
                    estadoReqServ.EstadoReqServNombre = Convert.ToString(dt.Rows[i]["EstadoReqServNombre"].ToString());
                    ReqServ.estadoReqServ = estadoReqServ;
                    ReqServ.personaDireccion = Conversor.toPersonaDireccion(dt.Rows[i]);

                    /////////////////////ver requiere servicio proveedores

                    SqlCommand sqlCmd1 = new SqlCommand("VerrequiereServicio_ServiProveedores1", conexion);
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.CommandTimeout = 0;
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioId", ReqServ.RequiereServicioId);
                    sqlCmd1.Parameters.AddWithValue("@PersonaId", PersonaId);
                    sqlCmd1.Parameters.AddWithValue("@lang", lang);

                    SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    List<RequiereServicioProveedoresM> lstProv = Conversor.toRequiereServicioProveedores(dt1.Select());
                    ReqServ.RequiereServicioProveedores = lstProv;
                    ReqServ.persona = Conversor.toPersona(dt.Rows[i]);

                    if (dt.Rows[i]["ServAsigId"].ToString() != "")
                    {
                        ReqServ.servAsig = Conversor.toServAsig(dt.Rows[i]);
                        ReqServ.servAsig.statusServAsig = Conversor.toStatusServAsig(dt.Rows[i]);
                    }




                    sqlCmd1.Parameters.Clear();
                    da1.Dispose();
                    dt1.Dispose();
                    ///////////////////////////////////////////////////////////////////////////////////////
                    LReqServ.Add(ReqServ);
                }

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                RespuestaServ.estado = 1;
                RespuestaServ.valor = LReqServ;
                RespuestaServ.mensaje = "OK";


            }
            catch (Exception ex)
            {
                RespuestaServ.estado = 2;
                RespuestaServ.valor = null;
                RespuestaServ.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaServ;
        }


















        //////////////////////////////////////////////////////
        [ResponseType(typeof(ServiciosWeb.WepApi.Models.RequiereServicio))]
        [HttpGet]
        [Route("api/ReqSev_ServicioClienteXEstado2")]
        public ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.RequiereServicio>> ReqSev_ServicioClientexEstado(decimal PersonaId, string lang, string EstadoReqServId)
        {
            List<ServiciosWeb.WepApi.Models.RequiereServicio> LReqServ = new List<ServiciosWeb.WepApi.Models.RequiereServicio>();
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("VerRequiereServicioPersonaXEstadoReqServId", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                sqlCmd.Parameters.AddWithValue("@PersonaId", PersonaId);
                sqlCmd.Parameters.AddWithValue("@EstadoReqServId", EstadoReqServId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string CategoriaServicioFechaHoraMod = "";
                string PersonaDir = "";
                ////////////////////////////////////////////////////////

                ///////////////////////////////////////////////////////////


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ServiciosWeb.WepApi.Models.RequiereServicio ReqServ = new ServiciosWeb.WepApi.Models.RequiereServicio();
                    ServiciosWeb.WepApi.Models.EstadoReqServ estadoReqServ = new ServiciosWeb.WepApi.Models.EstadoReqServ();


                    ReqServ = Conversor.toRequiereServicio(dt.Rows[i]);
                    ReqServ.servicio = Conversor.toServicio(dt.Rows[i]); ReqServ.servicio.categoriaServicio = Conversor.toCategoriaServicio(dt.Rows[i]);
                    estadoReqServ.EstadoReqServId = Convert.ToDecimal(dt.Rows[i]["EstadoReqServId"].ToString());
                    estadoReqServ.EstadoReqServNombre = Convert.ToString(dt.Rows[i]["EstadoReqServNombre"].ToString());
                    ReqServ.estadoReqServ = estadoReqServ;
                    ReqServ.personaDireccion = Conversor.toPersonaDireccion(dt.Rows[i]);

                    /////////////////////ver requiere servicio proveedores

                    SqlCommand sqlCmd1 = new SqlCommand("VerrequiereServicio_ServiProveedores1", conexion);
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.CommandTimeout = 0;
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioId", ReqServ.RequiereServicioId);
                    sqlCmd1.Parameters.AddWithValue("@PersonaId", PersonaId);
                    sqlCmd1.Parameters.AddWithValue("@lang", lang);

                    SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    List<RequiereServicioProveedoresM> lstProv = Conversor.toRequiereServicioProveedores(dt1.Select());
                    ReqServ.RequiereServicioProveedores = lstProv;
                    //ReqServ.persona = Conversor.toPersona(dt.Rows[i]);

                    //  if (dt.Rows[i]["ServAsigId"].ToString() != "")
                    //{
                    //  ReqServ.servAsig = Conversor.toServAsig(dt.Rows[i]);
                    //ReqServ.servAsig.statusServAsig = Conversor.toStatusServAsig(dt.Rows[i]);
                    //}




                    sqlCmd1.Parameters.Clear();
                    da1.Dispose();
                    dt1.Dispose();
                    ///////////////////////////////////////////////////////////////////////////////////////
                    LReqServ.Add(ReqServ);
                }
                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                RespuestaServ.estado = 1;
                RespuestaServ.valor = LReqServ;
                RespuestaServ.mensaje = "OK";


            }
            catch (Exception ex)
            {
                RespuestaServ.estado = 2;
                RespuestaServ.valor = null;
                RespuestaServ.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaServ;
        }
        /////////////////////////////////////////////////////
        [HttpGet]
        [Route("api/ReqSev_ServicioClienteXEstado")] // REEMPLAZO AL METODO verRequiereServicioXEstado metodo optimizado
        public IHttpActionResult VerRequiereServicioXEstado(long personaId, string lang, string EstadoReqServId, int index = 0, int max = 0, String v = null, String d = null)
        {
            Respuesta resp = new Respuesta();
            String message = "";
            if (validarVersion(ref message, v, d))
            {
                resp = contManager.VerRequierServicioXEstado(personaId, lang, EstadoReqServId);
                if (index == 1)
                {
                    resp.mensaje = contManager.CantidadRequierServicioXEstado(personaId, lang, EstadoReqServId).valor.ToString();
                }
                else
                {
                    resp.mensaje = "1";
                }
            }
            else
            {
                resp.estado = 2;
                resp.mensaje = message;
            }

            return Ok(resp);
        }


        ////////////////////////////////////////////////////////
        /// <summary>
        ///ACTUALIZAR REQUIERE SERVICIO PROVEEDORES
        /// </summary>
        [ResponseType(typeof(RequiereServicioProveedores))]
        [HttpPut]
        [Route("api/RequiereServicioProveedores2")]///PENDIENTE ENVIAR EL PARAMETRO DE LANG
        public async Task<ServiciosWeb.Datos.Modelo.Respuesta<RequiereServicioProveedores>> PutRequiereServicioProveedores(RequiereServicioProveedoresM rsp, string lang)
        {
            var serverKey = string.Format("key={0}", ServerKey);

            DataSet TokenId = new DataSet(); DataSet datosNot = new DataSet(); string TokenIdCliente = ""; string TokenIdProv = ""; decimal PersonaId = 0;
            string title = ""; string body = ""; string to = ""; string RsId = "";
            if (!ModelState.IsValid)
            {
                var Error = BadRequest(ModelState).ToString();
                PersonaMensaje.mensaje = Error;
            }


            try
            {
                var ProvActualizar = db.RequiereServicioProveedores.FirstOrDefault(x => (x.RequiereServicioId == rsp.RequiereServicioId) && (x.RequiereServicioProveedoresId == rsp.RequiereServicioProveedoresId));

                ProvActualizar.RequiereServicioProveedoresAdj = rsp.RequiereServicioProveedoresAdj; ProvActualizar.RequiereServicioProvCotizacion = rsp.RequiereServicioProvCotizacion;
                ProvActualizar.RequiereServicioProvFHTrabajo = rsp.RequiereServicioProvFHTrabajo; ProvActualizar.RequiereServicioProvDescipcion = rsp.RequiereServicioProvDescipcion;
                ProvActualizar.ServicioPersonaId = rsp.ServicioPersonaId; ProvActualizar.RequiereServicioProvFHResp = rsp.RequiereServicioProvFHResp;
                ProvActualizar.StatusRequiereId = rsp.StatusRequiereId;
                if (db.SaveChanges() > 0)
                {
                    PersonaMensaje.estado = 1;
                    ProvActualizar = db.RequiereServicioProveedores.FirstOrDefault(x => (x.RequiereServicioId == rsp.RequiereServicioId) && (x.RequiereServicioProveedoresId == rsp.RequiereServicioProveedoresId));
                    PersonaMensaje.valor = ProvActualizar;
                    PersonaMensaje.mensaje = "";
                    ////////////////////////////////////
                    ///Actualizando fechaMod RequiereServicio

                    try
                    {
                        if (conexion.State != ConnectionState.Open) conexion.Open();
                        RequiereServicio rs = rsp.requiereServicio;
                        RsId = rsp.RequiereServicioId;
                        SqlCommand sqlCmd = new SqlCommand("ActualizarRequiereServicio", conexion);
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@RequiereServicioId", rs.RequiereServicioId);
                        sqlCmd.Parameters.AddWithValue("@PersonaId", rs.PersonaId);
                        sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaHoraReq", rs.RequiereServicioFechaHoraReq);
                        sqlCmd.Parameters.AddWithValue("@RequiereServicioFHCaduca", rs.RequiereServicioFHCaduca);
                        sqlCmd.Parameters.AddWithValue("@EstadoReqServId", rs.EstadoReqServId);
                        sqlCmd.Parameters.AddWithValue("@RequiereServicioFHDeseada", rs.RequiereServicioFHDeseada);
                        sqlCmd.Parameters.AddWithValue("@RequiereServicioDescripcion", rs.RequiereServicioDescripcion);
                        if (rs.RequiereServicioURLFoto1 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto1", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto1", rs.RequiereServicioURLFoto1); }
                        if (rs.RequiereServicioURLFoto2 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto2", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto2", rs.RequiereServicioURLFoto2); }
                        if (rs.RequiereServicioURLFoto3 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto3", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto3", rs.RequiereServicioURLFoto3); }
                        if (rs.RequiereServicioURLVideo == null) { sqlCmd.Parameters.Add("@RequiereServicioURLVideo", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLVideo", rs.RequiereServicioURLVideo); }

                        sqlCmd.Parameters.AddWithValue("@RequiereServicioProvLast", rs.RequiereServicioProvLast);
                        if ((rs.PersonaDireccionId == null) || (rs.PersonaDireccionId == 0))
                        { sqlCmd.Parameters.Add("@PersonaDireccionId", SqlDbType.Decimal).Value = DBNull.Value; }
                        else { sqlCmd.Parameters.AddWithValue("@PersonaDireccionId", rs.PersonaDireccionId); }
                        if (rs.ServicioId == null) { sqlCmd.Parameters.Add("@ServicioId", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServicioId", rs.ServicioId); }
                        if (rs.RequiereServicioFechaMod == null) { sqlCmd.Parameters.Add("@RequiereServicioFechaMod", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaMod", rs.RequiereServicioFechaMod); }


                        sqlCmd.Parameters.Add("@Identity", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;

                        int resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
                        if (resp <= 0)
                        {
                            throw new Exception("Error al actualizar fecha Mod Solicitud. ");
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error al actualizar fecha Mod Solicitud. " + e.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }

                    ///////////////////////////////////
                    if (rsp.StatusRequiereId == 2)//COTIZADO envio al cliente
                    {
                        // List<BE.Persona> lstPersonasProvCot = contManager.ListadoProveedoresCotizados(RsId);
                        //await EnviarNotificacionesAsyncV2(lstPersonasProvCot, "ClienteCot", lang,null,"");

                    }

                    if (rsp.StatusRequiereId == 3)//CANCELADO enviado al proveedor
                    {
                        ////////////////////////////
                    }
                    if (rsp.StatusRequiereId == 4)
                    {
                        //   List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(RsId, 4);
                        //    await EnviarNotificacionesAsyncV2(lstPersonas, "Adjudicado", lang,null, "");

                    }

                }
                else
                {
                    PersonaMensaje.estado = 2;
                    PersonaMensaje.valor = null;
                }


            }
            catch (Exception ex)
            {
                if (!PersonaExists(rsp.RequiereServicioId))
                {
                    var error3 = NotFound().ToString();
                    PersonaMensaje.mensaje = error3;
                    PersonaMensaje.estado = 2;
                }
                else
                {
                    PersonaMensaje.estado = 2;
                    PersonaMensaje.mensaje = ex.Message;
                }
            }
            finally
            {
                conexion.Close();
            }
            return (PersonaMensaje);
            // return StatusCode(HttpStatusCode.NoContent);
        }
        /// /////////////////////////

        /// /////////////////////////
        private bool PersonaExists(string id)
        {
            return db.RequiereServicioProveedores.Count(e => e.RequiereServicioId == id) > 0;
        }
        ////////////////////////////////
        /// <summary>
        ///ACTUALIZAR REQUIERE SERVICIO_SERVICIOS 
        /// </summary>
        [ResponseType(typeof(RequiereServicio))]
        [HttpGet]
        [Route("api/ReqSev_Servicios")]
        public IHttpActionResult GetReqSev_Servicios(string PersonaId, string lang)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerRequiereServicioPersona", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@PersonaId", PersonaId);
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<RequiereServicio> lstRequiereServicio = new List<RequiereServicio>();
                ///////////////////////////////////////////////
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RequiereServicio requiereServicio = new RequiereServicio();
                    requiereServicio = Conversor.toRequiereServicio(dt.Rows[0]);
                    /*  requiereServicio.RequiereServicioId = dt.Rows[i]["RequiereServicioId"].ToString();
                      requiereServicio.PersonaId = Convert.ToDecimal(dt.Rows[i]["PersonaId"].ToString());
                      requiereServicio.RequiereServicioFechaHoraReq = Convert.ToDateTime(dt.Rows[i]["RequiereServicioFechaHoraReq"].ToString());
                      requiereServicio.EstadoReqServId = Convert.ToDecimal(dt.Rows[i]["EstadoReqServId"].ToString());
                      requiereServicio.RequiereServicioFHDeseada = Convert.ToDateTime(dt.Rows[i]["RequiereServicioFHDeseada"].ToString());
                      requiereServicio.RequiereServicioDescripcion = dt.Rows[i]["RequiereServicioDescripcion"].ToString();
                      requiereServicio.RequiereServicioURLFoto1 = dt.Rows[i]["RequiereServicioURLFoto1"].ToString();
                      requiereServicio.RequiereServicioURLFoto2 = dt.Rows[i]["RequiereServicioURLFoto2"].ToString();
                      requiereServicio.RequiereServicioURLFoto3 = dt.Rows[i]["RequiereServicioURLFoto3"].ToString();
                      requiereServicio.RequiereServicioURLVideo = dt.Rows[i]["RequiereServicioURLVideo"].ToString();
                      requiereServicio.ServicioId = Convert.ToDecimal(dt.Rows[i]["ServicioId"].ToString());*/
                    //  requiereServicio.RequiereServicioProvLast= Convert.ToInt32(dt.Rows[i]["RequiereServicioProvLast"].ToString());
                    lstRequiereServicio.Add(requiereServicio);
                }
                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido

                RespuestaServ.estado = 1;
                RespuestaServ.valor = lstRequiereServicio;
                RespuestaServ.mensaje = "OK";
                return Ok(RespuestaServ);
            }
            catch (Exception ex)
            {
                RespuestaServ.estado = 2;
                RespuestaServ.valor = null;
                RespuestaServ.mensaje = ex.Message;
                return Ok(RespuestaServ);
            }
            finally
            {
                conexion.Close();
            }

            ////////////////////////////////////////////////////////////////////////////////




        }

        [HttpPost]
        [Route("api/saveRequiereServicio")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> SaveRequiereServicio(BE.RequiereServicio requiereServicio, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.saveRequiereServicio(ref requiereServicio, lang);
            //resp.estado = 1;
            if (resp.estado == 1)
            {

                if (requiereServicio.TipoEstado == BE.TipoEstado.Insertar)
                {  /////////////////INSERCION FIREBASE 

                    await EstaAdjudicadoFirebase(requiereServicio.RequiereServicioId, "recibido");
                    System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(
                    token => EnviarNotificacionesRequerimiento(requiereServicio, lang, token));
                }
                else {

                    if ((requiereServicio.TipoEstado == BE.TipoEstado.Modificar) && (requiereServicio.EstadoReqServId==3)) {
                        System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(
                    token => EnviarNotificacionescancelar(requiereServicio, lang, token));


                    }
                
                }
            }
            return resp;
        }











        [HttpGet]
        [Route("api/validarSiEstaAdjudicadoyEsMayora6horas")]///
        public Respuesta validarSiEstaAdjudicadoyEsMayora6horas(string RequiereServicioId)
        {
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            resp.valor = contManager.validarSiEstaAdjudicadoyEsMayora6horas(RequiereServicioId);
            resp.mensaje = "";


            return resp;
        }

        [HttpPost]
        [Route("api/EmailEverybody")]///
        public string EmailEverybody(string text)
        {
           /* System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(
               // token => SendEmailTo1000ContactsAsync(text, token)
            );*/
            string valor = "Your message is being delivered";
            return valor;
        }







        private async Task EnviarNotificacionescancelar(BE.RequiereServicio requiereServicio, string lang, CancellationToken token)
        {
            bool rqAdsiono = contManager.validarSiEstaAdjudicadoyEsMayora6horas(requiereServicio.RequiereServicioId);
            if (rqAdsiono)
            {
                BE.Persona  Persona = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                await EnviarNotificacionesAsyncCliente(Persona, "ClienteCancela1P", Persona.PersonaIdioma, requiereServicio);
                List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(requiereServicio.RequiereServicioId, 4);
                string[] Tokens = new string[lstPersonas.Count];
                int i = 0;


                foreach (var persona in lstPersonas)
                {

                    Tokens[i] = persona.PersonaTokenId;
                    i = i + 1;
                }
                await EnviarNotificacionesCancelacion(Tokens, "ProveedoresCancelarI", lstPersonas, requiereServicio.RequiereServicioId, "es");


            }
            else
            {
                BE.Persona Persona = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                await EnviarNotificacionesAsyncCliente(Persona, "ClienteCancela", Persona.PersonaIdioma, requiereServicio);
                await EnviarNotificacionCancelacion(requiereServicio.RequiereServicioId);
           
            }
        }











        [HttpPost]
        [Route("api/EnviarNotificacionCancelacion")]
        public async Task EnviarNotificacionCancelacion(string RequiereServicioid)
        {
            List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(RequiereServicioid, 1);
            string[] Tokens = new string[lstPersonas.Count];
            int i = 0;

            foreach (var persona in lstPersonas)
            {

                Tokens[i] = persona.PersonaTokenId;
                i = i + 1;
            }
            await EnviarNotificacionesCancelacion(Tokens, "ProveedoresCancelarI", lstPersonas, RequiereServicioid, "es");
        }







        public async Task EnviarNotificacionesCancelacion(string[] Tokens,string tipo, List<BE.Persona> lstPersonas, string requiereServicioId, string PersonaIdioma = null)
        {
            int importe = 0;
            string invasivo = "";
            string reqservid = "";
            string action = "";
            string body = "";
            string title = "";
            string servicioNombre = "";
            string ServicioUrlFoto = "";
            string BotonTexto = "";
            string reqservdes = "";
            string priority = "high";
            string tag = "si";
            string vista = "";
            string jsonBody2 = "";
            var senderId2 = string.Format("id={0}", SenderIdFB);
            var key = string.Format("key={0}", ServerKey);
            DataRow dataIdioma = null; Respuesta resp = new Respuesta();

            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
            resp = contManager.verRequiereServicioXid(requiereServicioId, PersonaIdioma, true);
            requiereServicio = (BE.RequiereServicio)resp.valor;
            title = title + " RQ - " + Convert.ToString(requiereServicio.RequiereServicioId);
            notificacionPersona.RequiereServicioId = requiereServicio.RequiereServicioId;
            servicioNombre = requiereServicio.servicio.ServicioNombre;
            ServicioUrlFoto = requiereServicio.servicio.ServicioURLFoto;
            reqservid = requiereServicio.RequiereServicioId;
            string[] registration_ids = Tokens;
            string vista12 = "";
            string str1 = tipo;
            string[] titulo;

            switch (tipo)
            {
                case "ProveedoresCancelarI":
                
           
                dataIdioma = contManager.ListadoDatosNotificacionv2(tipo, "es");
                reqservdes = dataIdioma["BotonTexto"].ToString();
                vista = dataIdioma["Fragment"].ToString();
                vista12 = vista;
                invasivo = "si";
                if (requiereServicio.requiereServicioOtros == true)
                {
                    requiereServicio.servicio.servicioDetalleTipo = true;
                }
                importe = contManager.ImporteRequiereServicio(requiereServicio.RequiereServicioId, requiereServicio.servicio.servicioDetalleTipo);
                titulo = dataIdioma["body"].ToString().Split('/');
                servicioNombre = titulo[0] + requiereServicio.servicio.ServicioNombre + titulo[1];
                action = "inf";
                body = servicioNombre;
                break;
            }




            if (invasivo == "si")
            {
                var data2 = new { registration_ids, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                jsonBody2 = JsonConvert.SerializeObject(data2);

            }
            else
            {
                var data2 = new { registration_ids, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
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

                        InsertarLogNotificacion(requiereServicio.RequiereServicioId, DateTime.Now, "ProcesoCancelarSA1", 0, conexion);




                    }
                    else
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + "ErrorInsercion", DateTime.Now, "ProcesoCancelarSA1", 0, conexion);
                    }
                }
            }








            tipo = "ProveedoresCancelarNI";
            switch (tipo)
            {
                case "ProveedoresCancelarNI":
                    dataIdioma = this.contManager.ListadoDatosNotificacionv2(tipo, "es");
                title = dataIdioma["title"].ToString() + " RQ - " + Convert.ToString(requiereServicio.RequiereServicioId);
                body = "Este requerimiento fue cancelado.";
                vista = dataIdioma["Fragment"].ToString();
                BotonTexto = dataIdioma["BotonTexto"].ToString();
                jsonBody2 = JsonConvert.SerializeObject((object)body);
                action = "as";
                invasivo = "no";
                    break;
            }


            if (invasivo == "no")
            {
                var data3 = new { registration_ids, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                jsonBody2 = JsonConvert.SerializeObject(data3);

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

                        InsertarLogNotificacion(requiereServicio.RequiereServicioId, DateTime.Now, "ProcesoCancelarSA2", 0, conexion);




                    }
                    else
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + "ErrorInsercion", DateTime.Now, "ProcesoCancelarSA2", 0, conexion);
                    }
                }
            }




            body = servicioNombre;
            vista = vista12;

            foreach (var persona in lstPersonas)
            {

                notificacionPersona.PersonaId = persona.PersonaId;
                notificacionPersona.TipoEstadoId = 1;
                notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;
                notificacionPersona.NotificacionPersonaTitulo = title;
                notificacionPersona.NotificacionPersonaDescripcion = body;
                notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
                notificacionPersona.NotificacionPersonaFragment = vista;
                notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
                notificacionPersona.EstadoNotificacionId = 1;
                notificacionPersona.ConceptoNotificacionId = 16;

                if (notificacionPersona != null)
                {
                    contManager.saveNotificacionPersona(ref notificacionPersona);
                    await NotificacionFirebase(Convert.ToInt32(persona.PersonaId), "recibido", "Notificaciones");
                }
            }



        
 
        }

















        private async Task EnviarNotificacionesRequerimiento(BE.RequiereServicio requiereServicio,string lang, CancellationToken token)
        {
            await FirebaseSaveRequiereServicio(requiereServicio.RequiereServicioId);
            BE.Persona Persona = contManager.BuscarPersonaxId(requiereServicio.PersonaId);

            if ((Persona.PersonaTokenId) != null && (Persona.TipoLoginId != 5)) 
            {

                await EnviarNotificacionesAsyncCliente(Persona, "ClienteReque", Persona.PersonaIdioma, requiereServicio);
                List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(requiereServicio.RequiereServicioId, 1);
                //  await EnviarNotificacionesAsyncV3(lstPersonas, "ProveedorV2", lang, requiereServicio, requiereServicio.persona.NombreCompleto(), null);
                await EnviarNotificacion(requiereServicio.RequiereServicioId);
                await OfertaFirebase(requiereServicio.RequiereServicioId, Convert.ToInt32(requiereServicio.PersonaId), Convert.ToString(requiereServicio.EstadoReqServId), "recibido", "Requerimientos");
                await FirebaseSaveRequiereServicio(requiereServicio.RequiereServicioId);
                BE.Persona Personag = contManager.BuscarPersonaxId_enprueba(requiereServicio.PersonaId);
                if (Personag == null)
                {
                    List<BE.Persona> lstPersonasfg = contManager.ver_lista_operaciones();
                    await EnviarNotificacionesjulieta("noticicaiones_1j", lstPersonasfg, "es");
                }


            }
            else
            {

                DataRow data = contManager.ListadoDatosNotificacionv2("ClienteReque", Persona.PersonaIdioma);
                var title = data["title"].ToString();
                var body = data["body"].ToString();
                RespuestaHuawei respuestaHuawei = new RespuestaHuawei();
                Task<string> tokenn=GetToken2();
                EstructuraHuawei estructuraHuawei = new EstructuraHuawei();
                estructuraHuawei.validate_only = "false";
                estructuraHuawei.message.notification.title = title;
                estructuraHuawei.message.notification.body = body;
                estructuraHuawei.message.android.notification.title = title;
                estructuraHuawei.message.android.notification.body = body;
                estructuraHuawei.message.android.notification.click_action.type = 1;
                estructuraHuawei.message.android.notification.click_action.intent = "#Intent;compo=com.rvr/.Activity;S.W=U;end";
                List<string> ListaToken = new List<string>();
                ListaToken.Add(Persona.PersonaTokenIdHuawei);
                estructuraHuawei.message.token =ListaToken ;



               await EnviarNotificacionHuawei(estructuraHuawei, Convert.ToString(tokenn));

            }

          




        }



        private async Task EnviarNotificacionesAdjudicados2(BE.RequiereServicio requiereServicio, string lang,string StatusRequiereId, CancellationToken token)
        {
            List<BE.RequiereServicioProveedores> requiereServicioProveedores = new List<BE.RequiereServicioProveedores>();
            Respuesta respuesta = new Respuesta();
            bool bolOk = contManager.ver_Si_se_AdjudicoProveedores(requiereServicio.RequiereServicioId, lang);
           string  adjudicado = await VerAdjudicado(requiereServicio.RequiereServicioId);
            bool bolOkCancelado = contManager.ver_Si_se_EstaCancelado(requiereServicio.RequiereServicioId, lang);
            respuesta = contManager.VerRequierServicioProveedores(requiereServicio.RequiereServicioId, "");
            requiereServicioProveedores = (List<BE.RequiereServicioProveedores>)respuesta.valor;
       
            if (bolOkCancelado == false)
            {

                if (adjudicado == "NO" && bolOk == false)
                {
                    foreach (var item in requiereServicioProveedores)
                    {                     
                                                                                      
                        if ((item.StatusRequiereId == 3 )&& (StatusRequiereId=="0"))
                        {
                            BE.Persona Persona = contManager.BuscarPersonaxId(item.servicioPersona.PersonaId);
                            await EnviarNotificacionesAsyncCliente(Persona, "ProveedorCanV2", Persona.PersonaIdioma, requiereServicio);
                        }


                    }
                    ////////////

                }
                else
                {
                    foreach (var item in requiereServicioProveedores)
                    {
                        BE.Persona Persona = contManager.BuscarPersonaxId(item.servicioPersona.PersonaId);

                        if ((item.StatusRequiereId == 1)&& (StatusRequiereId == "1"))
                        {
                            await EnviarNotificacionesAsyncCliente(Persona, "ProveeorPerdioAdj", Persona.PersonaIdioma, requiereServicio);

                        }
                        if ((item.StatusRequiereId == 4)&& (StatusRequiereId == "1"))
                        {

                            BE.Persona PersonaCliente = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                            await EnviarNotificacionesAsyncCliente(Persona, "ProveedorConfV2", Persona.PersonaIdioma, requiereServicio);
                            await OfertaFirebase(requiereServicio.RequiereServicioId, Convert.ToInt32(requiereServicio.PersonaId), "4", "recibido", "Requerimientos");
                            //respCliente = contManager.verRequiereServicioXid(requiereServicio.RequiereServicioId, "es", false);
                            await EnviarNotificacionesAsyncCliente(PersonaCliente, "ClienteConfV2", PersonaCliente.PersonaIdioma, requiereServicio);
                        }

                        if ((item.StatusRequiereId == 3) && (StatusRequiereId == "0"))
                        {
                           
                            await EnviarNotificacionesAsyncCliente(Persona, "ProveedorCanV2", Persona.PersonaIdioma, requiereServicio);
                        }

                    }

                }
                if (contManager.ver_Si_Rechazaron_todos_Prov(requiereServicio.RequiereServicioId) == true)
                {
                    BE.Persona PersonaCliente = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                    await EnviarNotificacionesAsyncCliente(PersonaCliente, "DesiertoRechazado", PersonaCliente.PersonaIdioma, requiereServicio);

                }
            }
            else
            {
             
              
                foreach (var item in requiereServicioProveedores)
                {
                    BE.Persona Persona = contManager.BuscarPersonaxId(item.servicioPersona.PersonaId);
                 
                    await EnviarNotificacionesAsyncCliente(Persona, "RequerimientoCancelado", Persona.PersonaIdioma, requiereServicio);



                }
            }


        }
        private async Task EnviarNotificacionesAdjudicados(BE.RequiereServicio requiereServicio, string lang, string StatusRequiereId ,decimal ProveedorId,CancellationToken token)
        {
            List<BE.RequiereServicioProveedores> requiereServicioProveedores = new List<BE.RequiereServicioProveedores>();
            Respuesta respuesta = new Respuesta();
            bool bolOk = contManager.ver_Si_se_AdjudicoProveedores(requiereServicio.RequiereServicioId, lang);
            string adjudicado = await VerAdjudicado(requiereServicio.RequiereServicioId);
            bool bolOkCancelado = contManager.ver_Si_se_EstaCancelado(requiereServicio.RequiereServicioId, lang);
          

            if (bolOkCancelado == false)
            {                
                if (contManager.ver_Si_Rechazaron_todos_Prov(requiereServicio.RequiereServicioId) == true)
                {
                    BE.Persona PersonaCliente = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                    await EnviarNotificacionesAsyncCliente(PersonaCliente, "DesiertoRechazado", PersonaCliente.PersonaIdioma, requiereServicio);
                }
                else
                {
                    /////////////////////////////
                    BE.Persona Persona = contManager.BuscarPersonaxId(ProveedorId);
                    BE.ServAsig servAsig = contManager.BuscarServAsigxRequiereServicioId(requiereServicio.RequiereServicioId);

                    if (StatusRequiereId == "0") {
                        await EnviarNotificacionesAsyncCliente(Persona, "ProveedorCanV2", Persona.PersonaIdioma, requiereServicio);

                    }
                    if ((StatusRequiereId == "1")&&(servAsig.ProveedorId==ProveedorId))
                    {
                        BE.Persona PersonaCliente = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                        await EnviarNotificacionesAsyncCliente(Persona, "ProveedorConfV2", Persona.PersonaIdioma, requiereServicio);
                        await OfertaFirebase(requiereServicio.RequiereServicioId, Convert.ToInt32(requiereServicio.PersonaId), "4", "recibido", "Requerimientos");
                        await EnviarNotificacionesAsyncCliente(PersonaCliente, "ClienteConfV2", PersonaCliente.PersonaIdioma, requiereServicio);

                    }
                    ////////////////////////////////////

                 
                 


                }
            }
            else
            {


                foreach (var item in requiereServicioProveedores)
                {
                    BE.Persona Persona = contManager.BuscarPersonaxId(item.servicioPersona.PersonaId);

                    await EnviarNotificacionesAsyncCliente(Persona, "RequerimientoCancelado", Persona.PersonaIdioma, requiereServicio);



                }
            }


        }
        private async Task EnviarNotificacionesAdjudicadosV1(BE.RequiereServicio requiereServicio, string lang, string StatusRequiereId, CancellationToken token)
        {
            List<BE.RequiereServicioProveedores> requiereServicioProveedores = new List<BE.RequiereServicioProveedores>();
            Respuesta respuesta = new Respuesta();
            bool bolOk = contManager.ver_Si_se_AdjudicoProveedores(requiereServicio.RequiereServicioId, lang);
            string adjudicado = await VerAdjudicado(requiereServicio.RequiereServicioId);
            bool bolOkCancelado = contManager.ver_Si_se_EstaCancelado(requiereServicio.RequiereServicioId, lang);


            if (bolOkCancelado == false)
            {


                if (contManager.ver_Si_Rechazaron_todos_Prov(requiereServicio.RequiereServicioId) == true)
                {
                    BE.Persona PersonaCliente = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                    await EnviarNotificacionesAsyncCliente(PersonaCliente, "DesiertoRechazado", PersonaCliente.PersonaIdioma, requiereServicio);

                }
                else
                {
                    respuesta = contManager.VerRequierServicioProveedores(requiereServicio.RequiereServicioId, "");
                    requiereServicioProveedores = (List<BE.RequiereServicioProveedores>)respuesta.valor;
                    foreach (var item in requiereServicioProveedores)
                    {
                        BE.Persona Persona = contManager.BuscarPersonaxId(item.servicioPersona.PersonaId);

                        /* if ((item.StatusRequiereId == 1) && (StatusRequiereId == "1"))
                         {
                             await EnviarNotificacionesAsyncCliente(Persona, "ProveeorPerdioAdj", Persona.PersonaIdioma, requiereServicio);

                         }*/
                        if ((item.StatusRequiereId == 4) && (StatusRequiereId == "1"))
                        {

                            BE.Persona PersonaCliente = contManager.BuscarPersonaxId(requiereServicio.PersonaId);
                            await EnviarNotificacionesAsyncCliente(Persona, "ProveedorConfV2", Persona.PersonaIdioma, requiereServicio);
                            await OfertaFirebase(requiereServicio.RequiereServicioId, Convert.ToInt32(requiereServicio.PersonaId), "4", "recibido", "Requerimientos");
                            //respCliente = contManager.verRequiereServicioXid(requiereServicio.RequiereServicioId, "es", false);
                            await EnviarNotificacionesAsyncCliente(PersonaCliente, "ClienteConfV2", PersonaCliente.PersonaIdioma, requiereServicio);
                        }

                        if ((item.StatusRequiereId == 3) && (StatusRequiereId == "0"))
                        {

                            await EnviarNotificacionesAsyncCliente(Persona, "ProveedorCanV2", Persona.PersonaIdioma, requiereServicio);
                        }

                    }


                }
            }
            else
            {


                foreach (var item in requiereServicioProveedores)
                {
                    BE.Persona Persona = contManager.BuscarPersonaxId(item.servicioPersona.PersonaId);

                    await EnviarNotificacionesAsyncCliente(Persona, "RequerimientoCancelado", Persona.PersonaIdioma, requiereServicio);



                }
            }


        }
        private void metodoNotificaciones()
        {

            BE.Persona Persona = contManager.BuscarPersonaxId(123);
            Respuesta respuesta = new Respuesta();
            respuesta= contManager.verRequiereServicioXid("A1700", "es", false);
            BE.RequiereServicio requiereServicio = (BE.RequiereServicio)respuesta.valor;
         //   await EnviarNotificacionesAsyncCliente(Persona, "ClienteReque", Persona.PersonaIdioma, requiereServicio);
            /*List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(requiereServicio.RequiereServicioId, 1);
            await EnviarNotificacionesAsyncV3(lstPersonas, "ProveedorV2", "es", requiereServicio, "");
            //await EnviarNotificacionesAsyncV3(lstPersonas, "ProveedorV2", lang, requiereServicio, requiereServicio.persona.NombreCompleto(), null);
            await OfertaFirebase(requiereServicio.RequiereServicioId, Convert.ToInt32(requiereServicio.PersonaId), Convert.ToString(Convert.ToInt32(requiereServicio.EstadoReqServId)), "recibido", "Requerimientos");*/


        }
        public  void HacerAlgo(Object o, DoWorkEventArgs e)
        {
           
                 metodoNotificaciones();
          
          
        }
        [Route("api/ObtenerIdPersonaProveedorAdj")]
        public async Task<Respuesta> ObtenerIdPersonaProvedorAdj(string requiereServicioId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.ListadoObtenerIdPersonaProvedorAdj(requiereServicioId);

          
            return resp;
        }

        /* private async Task EnviarNotificacionesAsync(List<BE.Persona> lstPersonas, string tipo, string lang, BE.RequiereServicio requiereServicio = null, string persona = "", string RequiereServicioId = null)
         {
             DataRow data = contManager.ListadoDatosNotificacion(tipo, lang);
             var title = data["title"].ToString();
             var body = data["body"].ToString();
             var jsonBody2 = JsonConvert.SerializeObject(body);
             bool b = false;

             switch (tipo)
             {
                 case "Proveedor":
                     body = String.Format(body, persona);
                     break;
                 case "ClienteCot":
                     body = String.Format(body, persona);
                     break;
                 case "Adjudicado":
                     body = String.Format(body, persona);
                     break;

                 case "ClienteFinServicioFinal":
                     body = String.Format(body, persona);
                     break;
                 case "Pagado":
                     body = String.Format(body, persona);
                     break;
                 case "Conversacion":
                     body = String.Format(body, persona);
                     break;
             }
             foreach (var item in lstPersonas)
             {
                 var to = item.PersonaTokenId;

                 var senderId2 = string.Format("id={0}", SenderIdFB);
                 var key = string.Format("key={0}", ServerKey);
                 if (requiereServicio == null && RequiereServicioId == null)
                 {
                     var invasivo = "no";
                     var reqservid = "";
                     var reqservdes = "";
                     var servicioNombre = "";
                     var ServicioUrlFoto = "";
                     var data2 = new { to, data = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto } };
                     jsonBody2 = JsonConvert.SerializeObject(data2);
                 }
                 if ((RequiereServicioId != null) && (requiereServicio == null))
                 {
                     var invasivo = "no";
                     var reqservid = RequiereServicioId;
                     var reqservdes = "";
                     var servicioNombre = "";
                     var ServicioUrlFoto = "";
                     title = title + " " + "RQ=" + Convert.ToString(RequiereServicioId);

                     var data2 = new { to, data = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto } };
                     jsonBody2 = JsonConvert.SerializeObject(data2);
                 }
                 //else
                 // {

                 if (requiereServicio != null)
                 {
                     var invasivo = "si";
                     var reqservid = requiereServicio.RequiereServicioId;
                     var reqservdes = requiereServicio.RequiereServicioDescripcion;
                     var servicioNombre = requiereServicio.servicio.ServicioNombre;
                     var ServicioUrlFoto = requiereServicio.servicio.ServicioURLFoto;
                     if (b == false) {
                         title = title + " " + "RQ=" + Convert.ToString(requiereServicio.RequiereServicioId);
                         b = true;
                     }
                     var data2 = new { to, data = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto } };
                     jsonBody2 = JsonConvert.SerializeObject(data2);
                 }

                 //}


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
                             InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, item.PersonaId, conexion);

                         }
                         else
                         {
                             InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId, conexion);

                         }
                     }
                 }
             }
         }*/
        /*    private async Task EnviarNotificacionesAsyncV2(List<BE.Persona> lstPersonas, string tipo, string lang, BE.RequiereServicio requiereServicio = null, string persona = "", string RequiereServicioId = null)
            {
                DataRow data = contManager.ListadoDatosNotificacionv2(tipo, lang);
                var title = data["title"].ToString();
                var body = data["body"].ToString();
                var vista = data["Fragment"].ToString();
                var jsonBody2 = JsonConvert.SerializeObject(body);
                bool b = false;
                string detalledes = "";
                BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
                switch (tipo)
                {
                    case "Proveedor"://REQUIERE SERVICIO
                        body = String.Format(body, persona);

                        break;
                    case "ClienteCot":
                        body = String.Format(body, persona);  ///METODO
                        break;
                    case "Adjudicado":
                        body = String.Format(body, persona);
                        break;
                    case "ClienteFinServicioFinal":
                        body = String.Format(body, persona);
                        break;
                    case "Pagado":
                        body = String.Format(body, persona);
                        break;
                    case "Conversacion":
                        body = String.Format(body, persona);
                        break;
                }
                foreach (var item in lstPersonas)
                {
                    var to = item.PersonaTokenId;

                    var senderId2 = string.Format("id={0}", SenderIdFB);
                    var key = string.Format("key={0}", ServerKey);
                    if (requiereServicio == null && RequiereServicioId == null)
                    {
                        var sound = "default";
                        var invasivo = "no";
                        var reqservid = "";
                        var reqservdes = "";
                        //servicionombe =nonbre del boton de la notificacion
                        var servicioNombre = "Ir al menu";
                        var ServicioUrlFoto = "";
                        var action = "";
                        var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                        // var data2 = new { to, notification = new { title, body }, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };

                        jsonBody2 = JsonConvert.SerializeObject(data2);
                    }
                    if ((RequiereServicioId != null) && (requiereServicio == null))
                    {
                        var sound = "default";
                        var invasivo = "no";
                        var reqservid = RequiereServicioId;
                        var reqservdes = "";
                        var servicioNombre = "";
                        var ServicioUrlFoto = "";
                        if (tipo != "ClienteFinServicioFinal")
                        {
                            title = title + " " + "RQ=" + Convert.ToString(RequiereServicioId);

                        }

                        var action = "";//
                        var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                        jsonBody2 = JsonConvert.SerializeObject(data2);
                    }
                    if (requiereServicio != null)
                    {
                        ////PROCESO PARA NUEVO REQUERIMIENTO

                        if (tipo == "Proveedor")
                        {
                            detalledes = "Ver requerimiento";
                            //SAVE REQUIERE SERVICIO NOTIFICACIONES  A Proveedores
                            notificacionPersona.RequiereServicioId = requiereServicio.RequiereServicioId;
                            notificacionPersona.PersonaId = item.PersonaId;
                            notificacionPersona.TipoEstadoId = 1;
                            notificacionPersona.NotificacionPersonaTitulo = title;
                            notificacionPersona.NotificacionPersonaDescripcion = body;
                            notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
                            notificacionPersona.NotificacionPersonaFragment = vista;
                            notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
                            notificacionPersona.EstadoNotificacionId = 1;
                            notificacionPersona.ConceptoNotificacionId = 2;
                            notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;


                            //////

                        }
                        if (tipo == "Adjudicado")
                        {
                            detalledes = "Ver mas";

                        }

                        // requiereServicio.RequiereServicioDescripcion;
                        var sound = "default";
                        var invasivo = "si";
                        var reqservid = requiereServicio.RequiereServicioId;
                        var servicioNombre = requiereServicio.servicio.ServicioNombre;
                        var ServicioUrlFoto = requiereServicio.servicio.ServicioURLFoto;
                        var reqservdes = detalledes;
                        if (b == false)
                        {
                            title = title + " " + "RQ=" + Convert.ToString(requiereServicio.RequiereServicioId);
                            b = true;
                        }
                        var action = "";
                        var data2 = new { to, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };
                        //    var data2 = new { to, notification = new { title, body }, data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action } };

                        jsonBody2 = JsonConvert.SerializeObject(data2);

                    }

                    if (notificacionPersona != null)
                    {
                        contManager.saveNotificacionPersona(ref notificacionPersona);
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
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, item.PersonaId, conexion);

                            }
                            else
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId, conexion);

                            }
                        }
                    }
                }
            }*/

                [HttpPost]
        [Route("api/EnviarNotificacion")]
        public async Task EnviarNotificacion(string RequereServicioid)
        {
            if (contManager.ver_Si_se_Envio_Notificacion(RequereServicioid) == false)
            {
                List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(RequereServicioid, 1);
                string[] Tokens = new string[lstPersonas.Count];
                int i = 0;
                foreach (var persona in lstPersonas)
                {

                    Tokens[i] = persona.PersonaTokenId;
                    i = i + 1;
                }
                await EnviarNotificaciones(Tokens, "ProveedorV2", lstPersonas, RequereServicioid, "es");

            }

        }



        public async Task EnviarNotificaciones(string[] Tokens,string tipo,List<BE.Persona>lstPersonas, string requiereServicioId,string PersonaIdioma=null)
        {
            int importe = 0; var invasivo = "";var reqservid = ""; var action =""; var body = ""; var title = ""; var servicioNombre = "";
            var ServicioUrlFoto = ""; var BotonTexto = ""; var reqservdes =""; var priority = "high";var tag = "si"; var vista = "";
            var jsonBody2 = ""; string[] titulo;
            var senderId2 = string.Format("id={0}", SenderIdFB);
            var key = string.Format("key={0}", ServerKey);
            DataRow dataIdioma = null ;  Respuesta resp = new Respuesta();
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
            ////////////////////////Trayendo el requiereServicio
            resp = contManager.verRequiereServicioXid(requiereServicioId, PersonaIdioma, true);
            requiereServicio = (BE.RequiereServicio)resp.valor;
            title = title + " " + "RQ - " + Convert.ToString(requiereServicio.RequiereServicioId);
            notificacionPersona.RequiereServicioId = requiereServicio.RequiereServicioId;
            servicioNombre = requiereServicio.servicio.ServicioNombre;ServicioUrlFoto = requiereServicio.servicio.ServicioURLFoto; reqservid = requiereServicio.RequiereServicioId;
            var   registration_ids = Tokens;
            var vista12 = "";
            switch (tipo)
            {
                case "ProveedorV2":
                    dataIdioma = contManager.ListadoDatosNotificacionv2(tipo, "es");
                    reqservdes = dataIdioma["BotonTexto"].ToString();
                    vista = dataIdioma["Fragment"].ToString();
                    vista12 = vista;
                    invasivo = "si";
                    if (requiereServicio.requiereServicioOtros == true)
                    {
                        requiereServicio.servicio.servicioDetalleTipo = true;
                    }
                    importe = contManager.ImporteRequiereServicio(requiereServicio.RequiereServicioId,
                    requiereServicio.servicio.servicioDetalleTipo);
                    titulo = dataIdioma["body"].ToString().Split('/');
                    servicioNombre = titulo[0] + requiereServicio.servicio.ServicioNombre + titulo[1] + requiereServicio.personaDireccion.PersonaDireccionDescripcion + titulo[2] + requiereServicio.RequiereServicioFHDeseada.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + titulo[3] + requiereServicio.RequiereServicioFHDeseada.ToString("hh:mm:ss", CultureInfo.InvariantCulture) + titulo[4] + string.Format("{0:N}", importe) + " " +  requiereServicio.servicio.categoriaServicio.ciudad.Region.pais.Moneda.MonedaNombre;
                    action = "as";
                    body = servicioNombre;
                    break;
            }

            if (invasivo == "si")
            {                          
                    var data2 = new { registration_ids, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                    jsonBody2 = JsonConvert.SerializeObject(data2);

            }
            else
            {              
                    var data2 = new { registration_ids, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
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

                        InsertarLogNotificacion(requiereServicio.RequiereServicioId, DateTime.Now, "ProcesoProveedores",0, conexion);




                    }
                    else
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + "ErrorInsercion", DateTime.Now, "ProcesoProveedores", 0, conexion);
                    }
                }
            }



            tipo = "NNI_Proveedores";

            switch (tipo)
            {
                case "NNI_Proveedores":
                    dataIdioma = contManager.ListadoDatosNotificacionv2(tipo, "es");
                    title = dataIdioma["title"].ToString() + " " + "RQ - " + Convert.ToString(requiereServicio.RequiereServicioId);
                    body = dataIdioma["body"].ToString();
                    vista = dataIdioma["Fragment"].ToString();
                    BotonTexto = dataIdioma["BotonTexto"].ToString();
                    jsonBody2 = JsonConvert.SerializeObject(body);
                    action = "as";
                    invasivo = "no";
                    break;
            }


            if (invasivo == "no")
            {
                var data3 = new { registration_ids, data = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                jsonBody2 = JsonConvert.SerializeObject(data3);

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

                        InsertarLogNotificacion(requiereServicio.RequiereServicioId, DateTime.Now, "ProcesoProveedoresnn", 0, conexion);




                    }
                    else
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + "ErrorInsercion", DateTime.Now, "ProcesoProveedoresnn", 0, conexion);
                    }
                }
            }


       
            body = servicioNombre;
            vista = vista12;
            foreach (var persona in lstPersonas)
            {

                notificacionPersona.PersonaId = persona.PersonaId;
                notificacionPersona.TipoEstadoId = 1;
                notificacionPersona.TipoEstado = BE.TipoEstado.Insertar;
                notificacionPersona.NotificacionPersonaTitulo = title;
                notificacionPersona.NotificacionPersonaDescripcion = body;
                notificacionPersona.NotificacionPersonaFechaRegistro = DateTime.Now;
                notificacionPersona.NotificacionPersonaFragment = vista;
                notificacionPersona.NotificacionPersonaIcono = "Ser_463_894.png";
                notificacionPersona.EstadoNotificacionId = 1;
                notificacionPersona.ConceptoNotificacionId = 2;

                if (notificacionPersona != null)
                {
                    contManager.saveNotificacionPersona(ref notificacionPersona);
                    await NotificacionFirebase(Convert.ToInt32(persona.PersonaId), "recibido", "Notificaciones");
                }
            }


        }

        private async Task EnviarNotificacionesAsyncV3(List<BE.Persona> lstPersonas, string tipo, string lang, BE.RequiereServicio requiereServicio = null, string persona = "", string RequiereServicioId = null,string ConversacionContenido="")
        {
            DataRow data = contManager.ListadoDatosNotificacionv2(tipo, lang);
            var titleI = data["title"].ToString(); var bodyI = data["body"].ToString();var vista = data["Fragment"].ToString();  var jsonBody2 = JsonConvert.SerializeObject(bodyI);
            var BotonTexto = data["BotonTexto"].ToString();string versionTelefono = "";
            bool b = false; string detalledes = ""; string inv = ""; string[] titulo;int importe = 0;
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
            int ContadorBadge = 0;
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
            /*    case "ProveedorV2":
                   
                    inv = "si";

                    break;*/

            }

            foreach (var item in lstPersonas)
            {
                DataRow dataIdioma = contManager.ListadoDatosNotificacionv2(tipo, item.PersonaIdioma);
                var title = "";
                title = dataIdioma["title"].ToString();
                var body = String.Format(dataIdioma["body"].ToString(), persona); BotonTexto = dataIdioma["BotonTexto"].ToString();
                if (tipo == "Conversacion") {
                    body = ConversacionContenido;
                }
                Respuesta resp = new Respuesta();
                if (requiereServicio != null) { 
                resp = contManager.verRequiereServicioXid(requiereServicio.RequiereServicioId, item.PersonaIdioma,true);
                    requiereServicio =(BE.RequiereServicio) resp.valor;
                  
                    
                        title = title + " " + "RQ - " + Convert.ToString(requiereServicio.RequiereServicioId);
                       
                    
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
                        id= requiereServicio.RequiereServicioId;
                        sn = requiereServicio.servicio.ServicioNombre;
                        sf= requiereServicio.servicio.ServicioURLFoto;
                    }

                    if (tipo == "ProveedorV2")
                    {
                        if (requiereServicio.requiereServicioOtros == true)
                        {
                            requiereServicio.servicio.servicioDetalleTipo = true;
                        }

                    importe =contManager.ImporteRequiereServicio(requiereServicio.RequiereServicioId,
                    requiereServicio.servicio.servicioDetalleTipo);
                    titulo = dataIdioma["body"].ToString().Split('/');
                    sn = titulo[0] + requiereServicio.servicio.ServicioNombre + titulo[1] +
                    requiereServicio.personaDireccion.PersonaDireccionDescripcion + titulo[2]
                    + requiereServicio.RequiereServicioFHDeseada.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + titulo[3] +  requiereServicio.RequiereServicioFHDeseada.ToString("hh:mm:ss", CultureInfo.InvariantCulture)   + titulo[4] + string.Format("{0:N}", importe) + " " +    requiereServicio.servicio.categoriaServicio.ciudad.Region.pais.Moneda.MonedaNombre;
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
                    versionTelefono = contManager.VersionTelefono(item.PersonaId);
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
                            if (tipo == "Adjudicado") 
                            {
                                vista = "TabAgenda";
                            }
                            //var data2 = new {to,notification = new {title, body, badge,sound},priority};
                            //jsonBody2 = JsonConvert.SerializeObject(data2);
                            var data2 = new { to, notification = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
                            jsonBody2 = JsonConvert.SerializeObject(data2);
                            // InsertarLogNotificacion("badge", DateTime.Now, Convert.ToString(badge), item.PersonaId, conexion);
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
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + title, DateTime.Now, to, item.PersonaId, conexion);
                            }
                            else
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode)+ title, DateTime.Now, to, item.PersonaId, conexion);
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
                    contManager.saveNotificacionPersona(ref notificacionPersona);
                    await NotificacionFirebase(Convert.ToInt32(item.PersonaId),"recibido","Notificaciones");
                }
                
            }
        }

        /// <summary>
        ///REQUERIMIENTO SERVICIO_SERVICIO PROVEEDORES
        /// </summary>
        [ResponseType(typeof(ReqSev_ServProveedores))]
        [HttpPost]
        [Route("api/ReqSev_ServProveedores")]
        public async Task<ServiciosWeb.Datos.Modelo.Respuesta<ReqSev_ServProveedores>> ReqSev_ServProveedores(ReqSev_ServProveedores RSP, string lang)
        {
            SqlCommand commandlog = new SqlCommand(); int resp, resPC = 0; string valorServAsigId = ""; decimal valorPersonaPostId = 0;
            string ValorServicioId = ""; decimal ValorProveedoresId = 0; DataSet dsTokensCliente = new DataSet(); bool sent = false; string deviceTokensCliente = "";
            string to = ""; string title = ""; string body = ""; string deviceTokensProv = ""; string nombreCliente = "";
            var serverKey = string.Format("key={0}", ServerKey);


            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                ///////////////////////////////////////////Pais
                string RequiereServicioId = ObtenerRequiereServicioId(conexion);
                SqlCommand sqlCmd = new SqlCommand("InsertarRequiereServicio", conexion);
                sqlCmd.CommandTimeout = 0;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //sqlCmd.Parameters.AddWithValue("@PostId", postCont.PostId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                sqlCmd.Parameters.AddWithValue("@PersonaId", RSP.PersonaId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaHoraReq", RSP.RequiereServicioFechaHoraReq);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFHCaduca", RSP.RequiereServicioFHCaduca);
                sqlCmd.Parameters.AddWithValue("@EstadoReqServId", RSP.EstadoReqServId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFHDeseada", RSP.RequiereServicioFHDeseada);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioDescripcion", RSP.RequiereServicioDescripcion);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioProvLast", RSP.RequiereServicioProvLast);
                if (RSP.PersonaDireccionId == null) { sqlCmd.Parameters.Add("@PersonaDireccionId", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@PersonaDireccionId", RSP.PersonaDireccionId); }
                if (RSP.ServicioId == null) { sqlCmd.Parameters.Add("@ServicioId", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServicioId", RSP.ServicioId); }
                if (RSP.RequiereServicioURLFoto1 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto1", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto1", RSP.RequiereServicioURLFoto1); }
                if (RSP.RequiereServicioURLFoto2 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto2", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto2", RSP.RequiereServicioURLFoto2); }
                if (RSP.RequiereServicioURLFoto3 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto3", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto3", RSP.RequiereServicioURLFoto3); }
                if (RSP.RequiereServicioURLVideo == null) { sqlCmd.Parameters.Add("@RequiereServicioURLVideo", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLVideo", RSP.RequiereServicioURLVideo); }

                if (RSP.RequiereServicioFechaMod == null) { sqlCmd.Parameters.Add("@RequiereServicioFechaMod", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaMod", RSP.RequiereServicioFechaMod); }

                sqlCmd.Parameters.Add("@Identity", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
                ValorServicioId = Convert.ToString(sqlCmd.Parameters["@Identity"].Value.ToString());

                //////////////////////////////////////////////ENVIAR NOTIFICACION AL CLIENTE/////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////Enviando Token Id
                dsTokensCliente = ObtenerPersonaTokenIdCliente(conexion, Convert.ToDecimal(RSP.PersonaId));
                deviceTokensCliente = dsTokensCliente.Tables[0].Rows[0]["PersonaTokenId"].ToString();
                nombreCliente = dsTokensCliente.Tables[0].Rows[0]["PersonaNombres"].ToString() + " " + dsTokensCliente.Tables[0].Rows[0]["PersonaApellidos"].ToString();
                /////////////////////////////////////////////////////
                // Get the server key from FCM console
                to = Convert.ToString(deviceTokensCliente);// "cfowtQ8Ng04:APA91bGrqpOdLsk6VcYaoNf4bdGTeMJrah8yRWRACHPgwG6d0vOl5Ld1LOtbTQgjC9ViouGBUhfRvTvgtELL-QDq0Tdo6MlqU9y4Zb-JF88FbSP9YGxC2bYFxgGb66FsSvHPCSMhzeUw";
                DataSet ds = new DataSet();
                ds = ObtenerDatosNotificacion(lang, "Cliente", conexion);
                title = ds.Tables[0].Rows[0]["title"].ToString();
                body = ds.Tables[0].Rows[0]["body"].ToString();
                /////////////////////////////////////////////////////
                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "701502875637");
                var data = new { to, notification = new { title, body } };                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);
                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, deviceTokensCliente, Convert.ToDecimal(RSP.PersonaId), conexion);
                        }
                        else
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + "False", DateTime.Now, deviceTokensCliente, Convert.ToDecimal(RSP.PersonaId), conexion);

                        }
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                sqlCmd.Parameters.Clear();

                foreach (RequiereServicioProveedoresM i in RSP.RequiereServicioProveedores)
                {
                    ValorProveedoresId = ObtenerRequiereServicioProveedoresId(conexion, ValorServicioId);
                    SqlCommand sqlCmd1 = new SqlCommand("InsertarRequiereServicioProveedores", conexion);
                    sqlCmd1.CommandTimeout = 0;
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioId", ValorServicioId);
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioProveedoresId", ValorProveedoresId);
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioProveedoresAdj", i.RequiereServicioProveedoresAdj);
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioProvCotizacion", i.RequiereServicioProvCotizacion);
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioProvFHTrabajo", i.RequiereServicioProvFHTrabajo);
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioProvDescipcion", i.@RequiereServicioProvDescipcion);
                    sqlCmd1.Parameters.AddWithValue("@ServicioPersonaId", i.ServicioPersonaId);
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioProvFHResp", i.RequiereServicioProvFHResp);
                    sqlCmd1.Parameters.AddWithValue("@StatusRequiereId", i.StatusRequiereId);
                    resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());
                    ///////////////////////////Enviando Token Id
                    DataSet dsProv = new DataSet();
                    dsProv = ObtenerPersonaTokenIdProveedores(conexion, ValorServicioId, ValorProveedoresId);
                    deviceTokensProv = dsProv.Tables[0].Rows[0]["PersonaTokenId"].ToString();

                    // Get the server key from FCM console
                    to = Convert.ToString(deviceTokensProv);// "cfowtQ8Ng04:APA91bGrqpOdLsk6VcYaoNf4bdGTeMJrah8yRWRACHPgwG6d0vOl5Ld1LOtbTQgjC9ViouGBUhfRvTvgtELL-QDq0Tdo6MlqU9y4Zb-JF88FbSP9YGxC2bYFxgGb66FsSvHPCSMhzeUw";
                                                            ///////////////obteniendo titulo y body en base al lenguaje enviado 
                    DataSet dstoken = new DataSet();
                    dstoken = ObtenerDatosNotificacion(lang, "Proveedor", conexion); title = dstoken.Tables[0].Rows[0]["title"].ToString(); body = dstoken.Tables[0].Rows[0]["body"].ToString() + " " + nombreCliente;

                    /////////////////////////////////////////////////////
                    // Get the sender id from FCM console
                    var senderId2 = string.Format("id={0}", "701502875637");
                    var data2 = new { to, notification = new { title, body } };

                    // Using Newtonsoft.Json
                    var jsonBody2 = JsonConvert.SerializeObject(data2);

                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                    {
                        httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                        httpRequest.Headers.TryAddWithoutValidation("Sender", senderId2);
                        httpRequest.Content = new StringContent(jsonBody2, Encoding.UTF8, "application/json");

                        using (var httpClient = new HttpClient())
                        {
                            var result = await httpClient.SendAsync(httpRequest);

                            if (result.IsSuccessStatusCode)
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, deviceTokensProv, Convert.ToDecimal(i.ServicioPersonaId), conexion);

                            }
                            else
                            {
                                InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + "false", DateTime.Now, deviceTokensProv, Convert.ToDecimal(i.ServicioPersonaId), conexion);

                            }
                        }
                    }


                }

                if ((resp != 0) && (resPC != 0))
                {
                    Respuesta.mensaje = "OK";
                    Respuesta.estado = 1;
                    ServiciosWeb.Datos.Modelo.Respuesta<ReqSev_ServProveedores> obtenerListaProv = new ServiciosWeb.Datos.Modelo.Respuesta<ReqSev_ServProveedores>();
                    obtenerListaProv = GetReqSev_ServProveedores(RequiereServicioId, lang);
                    Respuesta.valor = obtenerListaProv.valor;

                }
            }
            catch (Exception ex)
            {
                Respuesta.mensaje = ex.Message;
                Respuesta.estado = 2;
                Respuesta.valor = null;
            }
            finally
            {
                conexion.Close();
                commandlog.Dispose();

            }
            return Respuesta;
        }

        //METODOS INTERNOS//////////////////////////UTILIZADOS PARA NOTIFICACION
        private void InsertarLogNotificacion(string result, DateTime fecha, string deviceTokens, decimal PersonaId, SqlConnection conexion)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            SqlCommand commandlog = new SqlCommand();

            commandlog.Connection = conexion;
            commandlog.CommandTimeout = 0;
            commandlog.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = Convert.ToString(result);
            commandlog.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
            commandlog.Parameters.Add("@deviceTokens", SqlDbType.VarChar, 800).Value = Convert.ToString(deviceTokens);
            commandlog.Parameters.Add("@PersonaId", SqlDbType.Decimal).Value = Convert.ToDecimal(PersonaId);

            commandlog.CommandText = "insert into [dbo].[Log_Notificacion](deviceTokens,title,sent,Fecha,PersonaId)values(@deviceTokens,@title,1,@fecha,@PersonaId)";
            commandlog.ExecuteNonQuery();
            commandlog.Parameters.Clear();
            conexion.Close();
        }
        private DataSet ObtenerDatosNotificacion(string lang, string Nombre, SqlConnection conexion)
        {
            SqlDataAdapter datoken = new SqlDataAdapter();
            DataSet dstoken = new DataSet();
            SqlCommand cmdNoti = new SqlCommand();

            cmdNoti.Connection = conexion;
            cmdNoti.CommandTimeout = 0;
            cmdNoti.CommandText = "select * from [dbo].[Notificacion] where Nombre=@proveedor";
            cmdNoti.Parameters.AddWithValue("@lang", lang);
            cmdNoti.Parameters.AddWithValue("@proveedor", Nombre);
            datoken.SelectCommand = cmdNoti;
            datoken.Fill(dstoken);

            return dstoken;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataSet ObtenerPersonaTokenIdCliente(SqlConnection conexion, decimal PersonaId)
        {

            SqlDataAdapter datoken = new SqlDataAdapter();
            DataSet dstoken = new DataSet();
            SqlCommand sqlCmd2 = new SqlCommand("[ObtenerPersonaTokenIdCliente]", conexion);
            sqlCmd2.CommandType = CommandType.StoredProcedure;
            sqlCmd2.CommandTimeout = 0;
            sqlCmd2.Parameters.AddWithValue("@PersonaId", PersonaId);

            datoken.SelectCommand = sqlCmd2;
            datoken.Fill(dstoken);


            return dstoken;
        }


        private DataSet ObtenerPersonaTokenIdClienteCotizacion(SqlConnection conexion, string RequiereServicioId, decimal RequiereServicioProveedoresId)
        {
            SqlDataAdapter datoken = new SqlDataAdapter();
            DataSet dstoken = new DataSet();
            SqlCommand sqlCmd2 = new SqlCommand("[ObtenerPersonaTokenIdClienteCotizacion]", conexion);
            sqlCmd2.CommandType = CommandType.StoredProcedure;
            sqlCmd2.CommandTimeout = 0;
            sqlCmd2.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
            sqlCmd2.Parameters.AddWithValue("@RequiereServicioProveedoresId", RequiereServicioProveedoresId);
            datoken.SelectCommand = sqlCmd2;
            datoken.Fill(dstoken);

            return dstoken;
        }

        private decimal ObtenerPostId(SqlConnection conexion, string ServAsigId)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            SqlCommand cmd = new SqlCommand();
            
            cmd.Connection = conexion;
            cmd.CommandTimeout = 0;
              cmd.CommandTimeout = 0;
            cmd.CommandText = "select p.PostId AS PostId from Post p where p.ServAsigId = @ServAsigId ";
            cmd.Parameters.Add("@ServAsigId", SqlDbType.VarChar, 800).Value = ServAsigId;




            decimal PostId = Convert.ToDecimal(cmd.ExecuteScalar());


            return PostId;
        }

        private decimal ObtenerPersonaId_deProveedor(SqlConnection conexion, decimal ServicioPersonaId)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandTimeout = 0;
            cmd.CommandText = "select s.PersonaId from RequiereServicioProveedores rsp  inner join ServicioPersona s on  rsp.ServicioPersonaId = s.servicioPersonaId  and s.ServicioPersonaId = @ServicioPersonaId ";
            cmd.Parameters.Add("@ServicioPersonaId", SqlDbType.Decimal).Value = ServicioPersonaId;




            decimal PersonaId = Convert.ToDecimal(cmd.ExecuteScalar());


            return PersonaId;
        }
        private decimal ObtenerRequiereServicioProveedoresId(SqlConnection conexion, string ValorServicioId)
        {
            decimal ValorProveedoresId = 0;
            SqlCommand cmd = new SqlCommand("ObtenerRequiereServicioProveedoresId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@RequiereServicioId", ValorServicioId);
            cmd.Parameters.Add("@RequiereServicioProveedoresId", SqlDbType.Decimal).Direction = ParameterDirection.Output;
            int v = Convert.ToInt32(cmd.ExecuteNonQuery());
            ValorProveedoresId = Convert.ToDecimal(cmd.Parameters["@RequiereServicioProveedoresId"].Value.ToString());
            return ValorProveedoresId;
        }
        private DataSet ObtenerPersonaTokenIdProveedores(SqlConnection conexion, string ValorServicioId, decimal ValorProveedoresId)
        {
            string deviceTokensProv = "";
            SqlDataAdapter datoken = new SqlDataAdapter();
            DataSet dstoken = new DataSet();
            SqlCommand sqlCmdtoken = new SqlCommand("ObtenerPersonaTokenIdProveedores", conexion);
            sqlCmdtoken.CommandType = CommandType.StoredProcedure;
            sqlCmdtoken.CommandTimeout = 0;
            sqlCmdtoken.Parameters.AddWithValue("@RequiereServicioId", ValorServicioId);
            sqlCmdtoken.Parameters.AddWithValue("@RequiereServicioProveedoresId", ValorProveedoresId);
            datoken.SelectCommand = sqlCmdtoken;
            datoken.Fill(dstoken);

            return dstoken;

        }

        /////////////////////////////////////REQUIERE SERVICIO

        /// <summary>
        ///POST A LA TABLA RequiereServicio atributo Servicio=null
        /// </summary>
        [ResponseType(typeof(ServiciosWeb.WepApi.Models.RequiereServicio))]
        [HttpPost]
        [Route("api/ReqSev_Servicio")]
        public ServiciosWeb.Datos.Modelo.Respuesta<ServiciosWeb.WepApi.Models.RequiereServicio> PostReqSev_Servicio(ServiciosWeb.WepApi.Models.RequiereServicio rs)
        {

            try
            {

                if (conexion.State != ConnectionState.Open) conexion.Open();
                string RequiereServicioId = ObtenerRequiereServicioId(conexion);
                SqlCommand sqlCmd = new SqlCommand("InsertarRequiereServicio", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", RequiereServicioId);
                sqlCmd.Parameters.AddWithValue("@PersonaId", rs.PersonaId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaHoraReq", rs.RequiereServicioFechaHoraReq);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFHCaduca", rs.RequiereServicioFHCaduca);
                sqlCmd.Parameters.AddWithValue("@EstadoReqServId", rs.EstadoReqServId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFHDeseada", rs.RequiereServicioFHDeseada);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioDescripcion", rs.RequiereServicioDescripcion);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto1", rs.RequiereServicioURLFoto1);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto2", rs.RequiereServicioURLFoto2);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto3", rs.RequiereServicioURLFoto3);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioURLVideo", rs.RequiereServicioURLVideo);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioProvLast", rs.RequiereServicioProvLast);
                sqlCmd.Parameters.AddWithValue("@PersonaDireccionId", rs.PersonaDireccionId);
                sqlCmd.Parameters.AddWithValue("@ServicioId", rs.ServicioId);

                if (rs.RequiereServicioFechaMod == null) { sqlCmd.Parameters.Add("@RequiereServicioFechaMod", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaMod", rs.RequiereServicioFechaMod); }

                /////////
                //servicio = rs.servicio;
                ///////////////////////////
                //  sqlCmd.Parameters.AddWithValue("@ServicioNombre", rs.servicio.ServicioNombre);
                //sqlCmd.Parameters.AddWithValue("@ServicioURLFoto", rs.servicio.ServicioURLFoto);
                // sqlCmd.Parameters.AddWithValue("@CategoriaServicioId", rs.servicio.CategoriaServicioId);

                //sqlCmd.Parameters.AddWithValue("@ServicioUsuario", rs.servicio.ServicioUsuario);
                //sqlCmd.Parameters.AddWithValue("@ServicioFechaHoraMod", rs.servicio.ServicioFechaHoraMod);
                //////////
                sqlCmd.Parameters.Add("@Identity", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                int resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
                string ValorServicioId = Convert.ToString(sqlCmd.Parameters["@Identity"].Value.ToString());
                sqlCmd.Parameters.Clear();
                ////////////////////////////////VER_REQUIERE_SERVICIO/////////////////////
                SqlCommand sqlCmd1 = new SqlCommand("VerrequiereServicio", conexion);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.CommandTimeout = 0;
                sqlCmd1.Parameters.AddWithValue("@RequiereServicioId", ValorServicioId);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                ServiciosWeb.WepApi.Models.RequiereServicio requiereServicio = new ServiciosWeb.WepApi.Models.RequiereServicio();


                requiereServicio = Conversor.toRequiereServicio(dt1.Rows[0]);

                /*  requiereServicio.RequiereServicioId = dt1.Rows[i]["RequiereServicioId"].ToString();
                  requiereServicio.PersonaId = Convert.ToDecimal(dt1.Rows[i]["PersonaId"].ToString());
                  requiereServicio.RequiereServicioFechaHoraReq = Convert.ToDateTime(dt1.Rows[i]["RequiereServicioFechaHoraReq"].ToString());
                  requiereServicio.EstadoReqServId = Convert.ToDecimal(dt1.Rows[i]["EstadoReqServId"].ToString());
                  requiereServicio.RequiereServicioFHDeseada = Convert.ToDateTime(dt1.Rows[i]["RequiereServicioFHDeseada"].ToString());
                  requiereServicio.RequiereServicioDescripcion = dt1.Rows[i]["RequiereServicioDescripcion"].ToString();
                  requiereServicio.RequiereServicioURLFoto1 = dt1.Rows[i]["RequiereServicioURLFoto1"].ToString();
                  requiereServicio.RequiereServicioURLFoto2 = dt1.Rows[i]["RequiereServicioURLFoto2"].ToString();
                  requiereServicio.RequiereServicioURLFoto3 = dt1.Rows[i]["RequiereServicioURLFoto3"].ToString();
                  requiereServicio.RequiereServicioURLVideo = dt1.Rows[i]["RequiereServicioURLVideo"].ToString();
                  requiereServicio.RequiereServicioProvLast= Convert.ToDecimal(dt1.Rows[i]["RequiereServicioProvLast"].ToString());
                  requiereServicio.RequiereServicioProvLast = Convert.ToDecimal(dt1.Rows[i]["RequiereServicioProvLast"].ToString());
                  requiereServicio.RequiereServicioProvLast = Convert.ToDecimal(dt1.Rows[i]["RequiereServicioProvLast"].ToString());
                  requiereServicio.PersonaDireccionId = Convert.ToDecimal(dt1.Rows[i]["PersonaDireccionId"].ToString());
                  requiereServicio.ServicioId = Convert.ToDecimal(dt1.Rows[i]["ServicioId"].ToString());
                  requiereServicio.servicio = null;*/


                sqlCmd.Parameters.Clear();
                da1.Dispose();
                dt1.Dispose();


                //////////////////////////////////////////////////////////////////////////////////////


                RespuestaReqServ.estado = 1;
                RespuestaReqServ.valor = requiereServicio;
                RespuestaReqServ.mensaje = "OK";


            }
            catch (Exception ex)
            {
                RespuestaReqServ.estado = 2;
                RespuestaReqServ.valor = null;
                RespuestaReqServ.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaReqServ;
        }

        /// <summary>
        ///Actualizar tabla requiere Servicio
        /// </summary>
        [ResponseType(typeof(ServiciosWeb.WepApi.Models.RequiereServicio))]
        [HttpPut]
        [Route("api/ReqSev_Servicio")]
        public ServiciosWeb.Datos.Modelo.Respuesta<ServiciosWeb.WepApi.Models.RequiereServicio> PutReqSev_Servicio(ServiciosWeb.WepApi.Models.RequiereServicio rs)
        {

            try
            {

                if (conexion.State != ConnectionState.Open) conexion.Open();

                SqlCommand sqlCmd = new SqlCommand("ActualizarRequiereServicio", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", rs.RequiereServicioId);
                sqlCmd.Parameters.AddWithValue("@PersonaId", rs.PersonaId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaHoraReq", rs.RequiereServicioFechaHoraReq);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFHCaduca", rs.RequiereServicioFHCaduca);
                sqlCmd.Parameters.AddWithValue("@EstadoReqServId", rs.EstadoReqServId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFHDeseada", rs.RequiereServicioFHDeseada);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioDescripcion", rs.RequiereServicioDescripcion);
                if (rs.RequiereServicioURLFoto1 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto1", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto1", rs.RequiereServicioURLFoto1); }
                if (rs.RequiereServicioURLFoto2 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto2", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto2", rs.RequiereServicioURLFoto2); }
                if (rs.RequiereServicioURLFoto3 == null) { sqlCmd.Parameters.Add("@RequiereServicioURLFoto3", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto3", rs.RequiereServicioURLFoto3); }
                if (rs.RequiereServicioURLVideo == null) { sqlCmd.Parameters.Add("@RequiereServicioURLVideo", SqlDbType.VarChar).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioURLVideo", rs.RequiereServicioURLVideo); }

                sqlCmd.Parameters.AddWithValue("@RequiereServicioProvLast", rs.RequiereServicioProvLast);
                if (rs.PersonaDireccionId == null) { sqlCmd.Parameters.Add("@PersonaDireccionId", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@PersonaDireccionId", rs.PersonaDireccionId); }
                if (rs.ServicioId == null) { sqlCmd.Parameters.Add("@ServicioId", SqlDbType.Decimal).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@ServicioId", rs.ServicioId); }
                if (rs.RequiereServicioFechaMod == null) { sqlCmd.Parameters.Add("@RequiereServicioFechaMod", SqlDbType.DateTime).Value = DBNull.Value; } else { sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaMod", rs.RequiereServicioFechaMod); }


                sqlCmd.Parameters.Add("@Identity", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;

                ServiciosWeb.WepApi.Models.RequiereServicio requiereServicio = new ServiciosWeb.WepApi.Models.RequiereServicio();
                int resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
                if (resp > 0)
                {
                    string ValorServicioId = Convert.ToString(sqlCmd.Parameters["@Identity"].Value.ToString());
                    sqlCmd.Parameters.Clear();
                    ////////////////////////////////VER_REQUIERE_SERVICIO/////////////////////
                    SqlCommand sqlCmd1 = new SqlCommand("VerrequiereServicio", conexion);
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.CommandTimeout = 0;
                    sqlCmd1.Parameters.AddWithValue("@RequiereServicioId", rs.RequiereServicioId);
                    SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    //  for (int i = 0; i < dt1.Rows.Count; i++)
                    //{
                    requiereServicio = Conversor.toRequiereServicio(dt1.Rows[0]);

                    //}
                    sqlCmd.Parameters.Clear();
                    da1.Dispose();
                    dt1.Dispose();
                }




                //////////////////////////////////////////////////////////////////////////////////////


                RespuestaReqServ.estado = 1;
                RespuestaReqServ.valor = requiereServicio;
                RespuestaReqServ.mensaje = "OK";


            }
            catch (Exception ex)
            {
                RespuestaReqServ.estado = 2;
                RespuestaReqServ.valor = null;
                RespuestaReqServ.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaReqServ;
        }


        /// <summary>
        ///PAQUETE BUSQUEDA DE SERVICIO
        /// </summary>
        [ResponseType(typeof(ServiciosWeb.WepApi.Models.Servicio))]
        [HttpGet]
        [Route("api/PaqBusquedaServicio")]///////////////////////////////////////REVISION
        public ServiciosWeb.Datos.Modelo.Respuesta<PackBusquedaServicio> PaqBusquedaServicio(string ServicioKeyWords, string lang, decimal CiudadId, decimal CategoriaServicioId)
        {
            List<BE.Servicio> LServ = new List<BE.Servicio>();
            List<BE.CategoriaServicio> LCatServ = new List<BE.CategoriaServicio>();
            ServiciosWeb.WepApi.Models.PackBusquedaServicio PBusSer = new ServiciosWeb.WepApi.Models.PackBusquedaServicio();
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("PaqBusquedaServicio_v2", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                sqlCmd.Parameters.AddWithValue("@ServicioKeyWords", ServicioKeyWords);
                sqlCmd.Parameters.AddWithValue("@CiudadId", CiudadId);
                sqlCmd.Parameters.AddWithValue("@CategoriaServicioId", CategoriaServicioId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string CategoriaServicioFechaHoraMod = "";
                string PersonaDir = "";


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    BE.Servicio servicio = new BE.Servicio();
                    BE.CategoriaServicio catServ = new BE.CategoriaServicio();

                    ///////////////////////////////////////
                    if (dt.Rows[i]["ServicioId"].ToString() != "") { servicio.ServicioId = Convert.ToDecimal(dt.Rows[i]["ServicioId"].ToString()); }
                    if (dt.Rows[i]["ServicioNombre"].ToString() != "") { servicio.ServicioNombre = Convert.ToString(dt.Rows[i]["ServicioNombre"].ToString()); }
                    if (dt.Rows[i]["ServicioURLFoto"].ToString() != "") { servicio.ServicioURLFoto = Convert.ToString(dt.Rows[i]["ServicioURLFoto"].ToString()); }
                    if (dt.Rows[i]["CategoriaServicioId"].ToString() != "") { servicio.CategoriaServicioId = Convert.ToDecimal(dt.Rows[i]["CategoriaServicioId"].ToString()); }
                    servicio.ServicioUsuario = Convert.ToString(dt.Rows[i]["ServicioUsuario"].ToString());
                    if (dt.Rows[i]["ServicioFechaHoraMod"].ToString() != "") { servicio.ServicioFechaHoraMod = Convert.ToDateTime(dt.Rows[i]["ServicioFechaHoraMod"].ToString()); }

                    servicio.ServicioKeyWords = Convert.ToString(dt.Rows[i]["ServicioKeyWords"].ToString());
                    if (dt.Rows[i]["ServicioPorcentaje"].ToString() != "")
                    {
                        servicio.ServicioPorcentaje = Convert.ToDecimal(dt.Rows[i]["ServicioPorcentaje"].ToString());

                    }
                    if (dt.Rows[i]["ServicioTarifaMinima"].ToString() != "")
                    {
                        servicio.ServicioTarifaMinima = Convert.ToDecimal(dt.Rows[i]["ServicioTarifaMinima"].ToString());

                    }



                    if (dt.Rows[i]["servicioDetalleTipo"].ToString() != "")
                    {
                        servicio.servicioDetalleTipo = Convert.ToBoolean(dt.Rows[i]["ServicioDetalleTipo"].ToString());

                    }
                    if (dt.Rows[i]["ServicioSabado"].ToString() != "")
                    {
                        servicio.servicioSabado = Convert.ToBoolean(dt.Rows[i]["ServicioSabado"].ToString());

                    }
                    if (dt.Rows[i]["ServicioDomingo"].ToString() != "")
                    {
                        servicio.servicioDomingo = Convert.ToBoolean(dt.Rows[i]["ServicioDomingo"].ToString());

                    }
                    if (dt.Rows[i]["ServicioHorarioRegularIni"].ToString() != "")
                    {
                        servicio.servicioHorarioRegularIni = Convert.ToDateTime(dt.Rows[i]["ServicioHorarioRegularIni"].ToString());

                    }
                    if (dt.Rows[i]["ServicioHorarioRegularFin"].ToString() != "")
                    {
                        servicio.servicioHorarioRegularFin = Convert.ToDateTime(dt.Rows[i]["ServicioHorarioRegularFin"].ToString());

                    }
                    if (dt.Rows[i]["ServicioHorarioSabadoIni"].ToString() != "")
                    {
                        servicio.servicioHorarioSabadoIni = Convert.ToDateTime(dt.Rows[i]["ServicioHorarioSabadoIni"].ToString());

                    }
                    if (dt.Rows[i]["ServicioHorarioSabadoFin"].ToString() != "")
                    {
                        servicio.servicioHorarioSabadoFin = Convert.ToDateTime(dt.Rows[i]["ServicioHorarioSabadoFin"].ToString());

                    }
                    if (dt.Rows[i]["ServicioHorarioDomingoIni"].ToString() != "")
                    {
                        servicio.servicioHorarioDomingoIni = Convert.ToDateTime(dt.Rows[i]["ServicioHorarioDomingoIni"].ToString());

                    }
                    if (dt.Rows[i]["ServicioHorarioDomingoFin"].ToString() != "")
                    {
                        servicio.servicioHorarioDomingoFin = Convert.ToDateTime(dt.Rows[i]["ServicioHorarioDomingoFin"].ToString());

                    }
                    if (dt.Rows[i]["servicioPersonaEnTurno"].ToString() != "")
                    {
                        servicio.servicioPersonaEnTurno = Convert.ToDecimal(dt.Rows[i]["servicioPersonaEnTurno"].ToString());

                    }
                    if (dt.Rows[i]["servicioDetalleFormulario"].ToString() != "")
                    {
                        servicio.servicioDetalleFormulario = (dt.Rows[i]["servicioDetalleFormulario"].ToString());

                    }

                    if (dt.Rows[i]["TipoServicioId"].ToString() != "")
                    {
                        servicio.tipoServicioId = Convert.ToDecimal(dt.Rows[i]["TipoServicioId"].ToString());

                    }

                    if (dt.Rows[i]["ServicioTarifaPlana"].ToString() != "")
                    {
                        servicio.servicioTarifaPlana = Convert.ToDecimal(dt.Rows[i]["ServicioTarifaPlana"].ToString());

                    }
                    if (dt.Rows[i]["ServicioTarifaInsumos_Extras"].ToString() != "")
                    {
                        servicio.servicioTarifaInsumos_Extras = Convert.ToDecimal(dt.Rows[i]["ServicioTarifaInsumos_Extras"].ToString());

                    }
                    if (dt.Rows[i]["nroProveedores"].ToString() != "")
                    {
                        servicio.nroProveedores = Convert.ToInt32(dt.Rows[i]["nroProveedores"].ToString());

                    }
                    if (dt.Rows[i]["ServicioHoras"].ToString() != "")
                    {
                        servicio.ServicioHoras = Convert.ToInt32(dt.Rows[i]["ServicioHoras"].ToString());

                    }
                    if (dt.Rows[i]["ServicioDescripcion"].ToString() != "")
                    {
                        servicio.ServicioDescripcion = dt.Rows[i]["ServicioDescripcion"].ToString();

                    }
                    servicio.categoriaServicio = null;

                    if (dt.Rows[i]["ServicioId"].ToString() != "")
                    {
                        servicio.servicioRequerimiento = new BE.ServicioRequerimiento();
                        servicio.servicioRequerimiento.ServicioId = Convert.ToDecimal(dt.Rows[i]["ServicioId"].ToString());
                        if (dt.Rows[i]["ServicioRequerimientoId"].ToString() != "")
                        {
                            servicio.servicioRequerimiento.ServicioRequerimientoId = Convert.ToDecimal(dt.Rows[i]["ServicioRequerimientoId"].ToString());
                        }

                        servicio.servicioRequerimiento.ServicioRequerimientoDesc = dt.Rows[i]["ServicioRequerimientoDesc"].ToString();
                    }


                    //////////////////////
                    if (dt.Rows[i]["CategoriaServicioId1"].ToString() != "")
                    { catServ.CategoriaServicioId = Convert.ToDecimal(dt.Rows[i]["CategoriaServicioId1"].ToString()); }
                    if (dt.Rows[i]["CategoriaServicioNombre"].ToString() != "")
                    { catServ.CategoriaServicioNombre = Convert.ToString(dt.Rows[i]["CategoriaServicioNombre"].ToString()); }
                    if (dt.Rows[i]["CategoriaServicioURLFoto"].ToString() != "")
                    { catServ.CategoriaServicioURLFoto = Convert.ToString(dt.Rows[i]["CategoriaServicioURLFoto"].ToString()); }

                    if (dt.Rows[i]["CiudadId"].ToString() != "") { catServ.CiudadId = Convert.ToDecimal(dt.Rows[i]["CiudadId"].ToString()); }

                    catServ.CategoriaServicioDescripcion = Convert.ToString(dt.Rows[i]["CategoriaServicioDescripcion"].ToString());
                    catServ.CategoriaServicioUsuario = Convert.ToString(dt.Rows[i]["CategoriaServicioUsuario"].ToString());
                    CategoriaServicioFechaHoraMod = dt.Rows[i]["CategoriaServicioFechaHoraMod"].ToString();
                    if (CategoriaServicioFechaHoraMod != "")
                    {
                        catServ.CategoriaServicioFechaHoraMod = Convert.ToDateTime(dt.Rows[i]["CategoriaServicioFechaHoraMod"].ToString());

                    }
                    if (dt.Rows[i]["CategoriaServicioHijoId"].ToString() != "")
                    {
                        catServ.CategoriaServicioHijoId = Convert.ToDecimal(dt.Rows[i]["CategoriaServicioHijoId"].ToString());

                    }
                    if (dt.Rows[i]["CategoriaServicioDestLast"].ToString() != "")
                    {
                        catServ.CategoriaServicioDestLast = Convert.ToDecimal(dt.Rows[i]["CategoriaServicioDestLast"].ToString());

                    }


                    ////////////Asignando la clase servicio

                    ////////////Asignando la clase categoria Servicio

                    LServ.Add(servicio);
                    LCatServ.Add(catServ);
                    PBusSer.categoriaServicio = LCatServ;
                    PBusSer.servicios = LServ;
                    ///////////////////////////////////////////

                }

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                RespuestaServ_Cat.estado = 1;
                RespuestaServ_Cat.valor = PBusSer;

                RespuestaServ_Cat.mensaje = "OK";


            }
            catch (Exception ex)
            {
                RespuestaServ_Cat.estado = 2;
                RespuestaServ_Cat.valor = null;

                RespuestaServ_Cat.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaServ_Cat;
        }



        /// <summary>
        ///RANDOM SERVICIO DESTACADO
        /// </summary>
        [ResponseType(typeof(ServiciosWeb.WepApi.Models.Servicio))]
        [HttpGet]
        [Route("api/RandomServicioDestacado")]
        public ServiciosWeb.Datos.Modelo.Respuesta<List<ServiciosWeb.WepApi.Models.CategoriaServicioDestacada>> RandomServicioDestacado(decimal CiudadId)
        {

            List<ServiciosWeb.WepApi.Models.CategoriaServicioDestacada> LCatServDest = new List<ServiciosWeb.WepApi.Models.CategoriaServicioDestacada>();
            ServiciosWeb.WepApi.Models.PackBusquedaServicio PBusSer = new ServiciosWeb.WepApi.Models.PackBusquedaServicio();
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                SqlCommand sqlCmd = new SqlCommand("RandomServicioDestacado", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@CiudadId", CiudadId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string CategoriaServicioFechaHoraMod = "";



                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ServiciosWeb.WepApi.Models.CategoriaServicioDestacada CatServDest = new ServiciosWeb.WepApi.Models.CategoriaServicioDestacada();


                    ///////////////////////////////////////
                    CatServDest.CategoriaServicioId = Convert.ToDecimal(dt.Rows[i][1].ToString());
                    CatServDest.CategoriaServicioDestacadaId = Convert.ToDecimal(dt.Rows[i][2].ToString());
                    CatServDest.CategoriaServicioDestacadaURL = Convert.ToString(dt.Rows[i][3].ToString());
                    CatServDest.CategoriaServicioDestacadaFini = Convert.ToDateTime(dt.Rows[i][4].ToString());
                    CatServDest.CategoriaServicioDestacadaFFin = Convert.ToDateTime(dt.Rows[i][5].ToString());

                    ////////////Asignando la clase servicio

                    ////////////Asignando la clase categoria Servicio

                    LCatServDest.Add(CatServDest);

                    ///////////////////////////////////////////

                }

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                RespuestaServDest.estado = 1;
                RespuestaServDest.valor = LCatServDest;

                RespuestaServDest.mensaje = "OK";


            }
            catch (Exception ex)
            {
                RespuestaServDest.estado = 2;
                RespuestaServDest.valor = null;

                RespuestaServDest.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaServDest;
        }

        public string ObtenerRequiereServicioId(SqlConnection conexion)
        {
            string RequiereServicioId = "";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();                //CONEXION BASE DE DATOS  
            SqlCommand cmd = new SqlCommand();//modificado 
            cmd.Connection = conexion;
            cmd.CommandTimeout = 0;
            cmd.CommandText = "select * from RequiereServicioId";
            da.SelectCommand = cmd;
            da.Fill(ds);

            string Letra = Convert.ToString(ds.Tables[0].Rows[0][0].ToString());
            char Lid = Convert.ToChar(Letra);
            int ascii = Encoding.ASCII.GetBytes(Letra)[0];
            string Nro = ds.Tables[0].Rows[0][1].ToString();
            if (Nro.Length == 199)
            {
                Lid = (Convert.ToChar(ascii + 1));
                Nro = "1";

            }
            else
            {
                Decimal ID = Convert.ToDecimal(Nro) + 1;
                Nro = Convert.ToString(ID);


            }
            cmd.Parameters.Clear();
            cmd.CommandText = "UPDATE RequiereServicioId SET Id=@Nro ,Inicio=@Inicio";
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@Nro", SqlDbType.VarChar, 200).Value = Nro;
            cmd.Parameters.Add("@Inicio", SqlDbType.Char, 200).Value = Lid;
            cmd.ExecuteNonQuery();
            RequiereServicioId = Lid + Nro;
            return RequiereServicioId;
        }


        public string ObtenerId(SqlConnection conexion, string NombreTabla, SqlTransaction DataTransactionCom)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            string Id = "";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();                //CONEXION BASE DE DATOS  
            SqlCommand cmd = new SqlCommand();//modificado 
            cmd.Connection = conexion;
            cmd.CommandTimeout = 0;
            cmd.CommandText = " SELECT * FROM Generacion_Id with(nolock) where NombreTabla=@NombreTabla";
            cmd.Parameters.Add("@NombreTabla", SqlDbType.VarChar, 200).Value = NombreTabla;
            if (DataTransactionCom != null)
            {
                cmd.Transaction = DataTransactionCom;

            }
            da.SelectCommand = cmd;
            da.Fill(ds);

            string Letra = Convert.ToString(ds.Tables[0].Rows[0]["Inicio"].ToString().Trim());
            char Lid = Convert.ToChar(Letra);
            int ascii = Encoding.ASCII.GetBytes(Letra)[0];
            string Nro = ds.Tables[0].Rows[0]["Id"].ToString();
            if (Nro.Length == 10)
            {
                Lid = (Convert.ToChar(ascii + 1));
                Nro = "1";

            }
            else
            {
                Decimal ID = Convert.ToDecimal(Nro) + 1;
                Nro = Convert.ToString(ID);


            }
            cmd.Parameters.Clear();
            cmd.CommandText = "UPDATE Generacion_Id SET Id=@Nro ,Inicio=@Inicio where NombreTabla=@NombreTabla";
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@Nro", SqlDbType.VarChar, 200).Value = Nro;
            cmd.Parameters.Add("@Inicio", SqlDbType.Char, 200).Value = Lid;
            cmd.Parameters.Add("@NombreTabla", SqlDbType.VarChar, 200).Value = NombreTabla;
            if (DataTransactionCom != null)
            {
                cmd.Transaction = DataTransactionCom;

            }
            cmd.ExecuteNonQuery();
            Id = Lid + Nro;
            return Id;
        }


        /// <summary>
        ///SERVICIO PERSONA
        /// </summary>
    [ResponseType(typeof(ServicioPersona))]
        [HttpGet]
        [Route("api/GetServicioProveedores")]
        public IHttpActionResult GetServicioProveedores(string servicioId, string lang)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerServicioPersona", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@servicioId", servicioId);
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<ServicioPersona> lst = Conversor.toServicioPersona(dt.Select());


                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido

                RespuestaSerPer.estado = 1;
                RespuestaSerPer.valor = lst;
                RespuestaSerPer.mensaje = "OK";
                return Ok(RespuestaSerPer);
            }
            catch (Exception ex)
            {
                RespuestaSerPer.estado = 2;
                RespuestaSerPer.valor = null;
                RespuestaSerPer.mensaje = ex.Message;
                return Ok(RespuestaSerPer);
            }
            finally
            {
                conexion.Close();
            }

        }

  /* [ResponseType(typeof(ServicioPersona))]
        [HttpGet]
        [Route("api/GetServicioProveedoresV2")]
        public IHttpActionResult GetServicioProveedoresV2(string servicioId, string lang)
        {
            Respuesta resp = new Respuesta();
            String message = "";
          
             resp = contManager.GetServicioProveedoresV2(servicioId); 
           
            return Ok(resp);
            
        }*/


        [ResponseType(typeof(RequiereServicio))]
        [HttpPut]
        [Route("api/ReqSerOferta2")]
        public IHttpActionResult PutReqSerOferta(RequiereServicio rs)
        {
            if (!ModelState.IsValid)
            {
                var Error = BadRequest(ModelState).ToString();
                RespuestaServ.mensaje = Error;
            }

            try
            {

                if (conexion.State != ConnectionState.Open) conexion.Open();

                SqlCommand sqlCmd = new SqlCommand("ActualizarRequiereServicio", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@RequiereServicioId", rs.RequiereServicioId);
                sqlCmd.Parameters.AddWithValue("@PersonaId", rs.PersonaId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaHoraReq", rs.RequiereServicioFechaHoraReq);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFHCaduca", rs.RequiereServicioFHCaduca);
                sqlCmd.Parameters.AddWithValue("@EstadoReqServId", rs.EstadoReqServId);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFHDeseada", rs.RequiereServicioFHDeseada);
                sqlCmd.Parameters.AddWithValue("@RequiereServicioDescripcion", rs.RequiereServicioDescripcion);
                if (rs.RequiereServicioURLFoto1 == null)
                {
                    sqlCmd.Parameters.Add("@RequiereServicioURLFoto1", SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto1", rs.RequiereServicioURLFoto1);
                }
                if (rs.RequiereServicioURLFoto2 == null)
                {
                    sqlCmd.Parameters.Add("@RequiereServicioURLFoto2", SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto2", rs.RequiereServicioURLFoto2);
                }
                if (rs.RequiereServicioURLFoto3 == null)
                {
                    sqlCmd.Parameters.Add("@RequiereServicioURLFoto3", SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@RequiereServicioURLFoto3", rs.RequiereServicioURLFoto3);
                }
                if (rs.RequiereServicioURLVideo == null)
                {
                    sqlCmd.Parameters.Add("@RequiereServicioURLVideo", SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@RequiereServicioURLVideo", rs.RequiereServicioURLVideo);
                }
                sqlCmd.Parameters.AddWithValue("@RequiereServicioProvLast", rs.RequiereServicioProvLast);
                if ((rs.PersonaDireccionId == null) || (rs.PersonaDireccionId == 0))
                {
                    sqlCmd.Parameters.Add("@PersonaDireccionId", SqlDbType.Decimal).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PersonaDireccionId", rs.PersonaDireccionId);

                }
                if (rs.ServicioId == null)
                {
                    sqlCmd.Parameters.Add("@ServicioId", SqlDbType.Decimal).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@ServicioId", rs.ServicioId);

                }
                sqlCmd.Parameters.AddWithValue("@RequiereServicioFechaMod", rs.RequiereServicioFechaMod);


                sqlCmd.Parameters.Add("@Identity", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;

                ServiciosWeb.WepApi.Models.RequiereServicio requiereServicio = new ServiciosWeb.WepApi.Models.RequiereServicio();
                int resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
                if (resp <= 0)
                {
                    throw new Exception("Error al actualizar RequiereServicio");
                }
                RequiereServicioProveedores rsp = Conversor.toRequiereServicioProveedoresDM(rs.RequiereServicioProveedores[0]);

                var ProvActualizar = db.RequiereServicioProveedores.FirstOrDefault(x =>
                    (x.RequiereServicioId == rsp.RequiereServicioId) &&
                    (x.RequiereServicioProveedoresId == rsp.RequiereServicioProveedoresId));

                ProvActualizar.RequiereServicioProveedoresAdj = rsp.RequiereServicioProveedoresAdj;
                ProvActualizar.RequiereServicioProvCotizacion = rsp.RequiereServicioProvCotizacion;
                ProvActualizar.RequiereServicioProvFHTrabajo = rsp.RequiereServicioProvFHTrabajo;
                ProvActualizar.RequiereServicioProvDescipcion = rsp.RequiereServicioProvDescipcion;
                ProvActualizar.ServicioPersonaId = rsp.ServicioPersonaId;
                ProvActualizar.RequiereServicioProvFHResp = rsp.RequiereServicioProvFHResp;
                ProvActualizar.StatusRequiereId = rsp.StatusRequiereId;
                if (db.SaveChanges() > 0)
                {

                    if (rsp.StatusRequiereId == 4)
                    {
                        decimal PersonaId = ObtenerPersonaId_deProveedor(conexion, Convert.ToDecimal(rsp.ServicioPersonaId));
                        ServiciosWeb.WepApi.Models.ServAsig ServAsig = new ServiciosWeb.WepApi.Models.ServAsig();
                        ServAsig.ServAsigId = "0";
                        ServAsig.ProveedorId = PersonaId;
                        //Adjudicacion 
                        ServAsig.ServAsigFHUbicacion = DateTime.Now;
                        ServAsig.ServAsigFHEstimadaLlegada = rsp.RequiereServicioProvFHTrabajo;
                        ServAsig.ServAsigCostoTotal = 0;

                        ServAsig.StatusServAsigId = 1;
                        ServAsig.RequiereServicioId = rsp.RequiereServicioId;

                        _4PostServAsig(ServAsig, 1);

                    }



                    RespuestaReqServ.estado = 1;
                    RespuestaReqServ.valor = null;
                    RespuestaReqServ.mensaje = "OK";
                }
                else
                {
                    RespuestaReqServ.estado = 2;
                    RespuestaReqServ.valor = null;
                }
                //////////////////////////////////////////////////////////////////////////////////////

            }
            catch (Exception ex)
            {
                RespuestaReqServ.estado = 2;
                RespuestaReqServ.valor = null;
                RespuestaReqServ.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return Ok(RespuestaReqServ);
        }
    
        
        [ResponseType(typeof(RequiereServicio))]
        [HttpPut]
        [Route("api/ReqSerOferta")]
        public async Task<Respuesta> PutReqSerOferta2(BE.RequiereServicio rs, string lang)
        {

            Respuesta resp = new Respuesta();
            try
            {

                rs.TipoEstado = BE.TipoEstado.Modificar;
                foreach (BE.RequiereServicioProveedores item in rs.RequiereServicioProveedores)
                {
                    item.TipoEstado = BE.TipoEstado.Modificar;
                }
                resp = contManager.saveRequiereServicio(ref rs, "");
                if (resp.estado == 1)
                {

                    /////SI ACTUALIZO REQUIERE SERVICIO PROVEDORES StatusRequiere COTIZADO
                    if ((rs.TipoEstado == BE.TipoEstado.Modificar))
                    {
                        if (rs.EstadoReqServId == 3)
                        {
                            // List<BE.Persona> lstPersonasProvCot = contManager.ListadoProveedoresCotizados(rs.RequiereServicioId);
                            // await EnviarNotificacionesAsyncV2(lstPersonasProvCot, "ClienteCot", "es", null, "");
                            await OfertaFirebase(rs.RequiereServicioId, Convert.ToInt32(rs.PersonaId), "5", "recibido", "Requerimientos");


                        }
                        foreach (BE.RequiereServicioProveedores item in rs.RequiereServicioProveedores)
                        {
                            if (item.StatusRequiereId == 4)//Adjudicado
                            {
                                List<BE.Persona> lstPersonas = contManager.ListadoTokenProveedores(rs.RequiereServicioId, 4);
                            //    await EnviarNotificacionesAsyncV3(lstPersonas, "Adjudicado", lang, rs, "");
                             await OfertaFirebase(rs.RequiereServicioId, Convert.ToInt32(rs.PersonaId),Convert.ToString(item.StatusRequiereId), "recibido", "Requerimientos");

                            }
                        }

                      
                    }

                }



                //////////////////////////////////////////////////////////////////////////////////////

            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return resp;
        }


        /// <summary>
        ///SERVICIO PERSONA
        /// </summary>
        [ResponseType(typeof(ConceptoCosto))]
        [HttpGet]
        [Route("api/GetConceptoCosto")]
        public IHttpActionResult GetConceptoCosto(string lang)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerConceptoCosto", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<ConceptoCosto> lst = Conversor.toConceptoCosto(dt.Select());

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido

                RespuestaConCos.estado = 1;
                RespuestaConCos.valor = lst;
                RespuestaConCos.mensaje = "OK";
                return Ok(RespuestaConCos);
            }
            catch (Exception ex)
            {
                RespuestaConCos.estado = 2;
                RespuestaConCos.valor = null;
                RespuestaConCos.mensaje = ex.Message;
                return Ok(RespuestaConCos);
            }
            finally
            {
                conexion.Close();
            }

        }

        /// <summary>
        ///SERVICIO PERSONA
        /// </summary>
        [ResponseType(typeof(ServicioPersonaGaleria))]
        [HttpGet]
        [Route("api/GetServicioPersonaGaleria")]
        public IHttpActionResult getServicioPersonaGaleria(string servicioPersonaId)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerServicioPersonaGaleria", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServicioPersonaId", servicioPersonaId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<ServicioPersonaGaleria> lst = Conversor.toServicioPersonaGaleria(dt.Select());

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido

                RespuestaSePeGa.estado = 1;
                RespuestaSePeGa.valor = lst;
                RespuestaSePeGa.mensaje = "OK";
                return Ok(RespuestaSePeGa);
            }
            catch (Exception ex)
            {
                RespuestaSePeGa.estado = 2;
                RespuestaSePeGa.valor = null;
                RespuestaSePeGa.mensaje = ex.Message;
                return Ok(RespuestaSePeGa);
            }
            finally
            {
                conexion.Close();
            }

        }

        /// <summary>
        ///SERVICIO PERSONA
        /// </summary>
        [ResponseType(typeof(ServicioPersonaHorario))]
        [HttpGet]
        [Route("api/GetServicioPersonaHorario")]
        public IHttpActionResult getServicioPersonaHorario(string servicioPersonaId, string lang)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerServicioPersonaHorario", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServicioPersonaId", servicioPersonaId);
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<ServicioPersonaHorario> lst = Conversor.toServicioPersonaHorario(dt.Select());

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido

                RespuestaSePeHo.estado = 1;
                RespuestaSePeHo.valor = lst;
                RespuestaSePeHo.mensaje = "OK";
                return Ok(RespuestaSePeHo);
            }
            catch (Exception ex)
            {
                RespuestaSePeHo.estado = 2;
                RespuestaSePeHo.valor = null;
                RespuestaSePeHo.mensaje = ex.Message;
                return Ok(RespuestaSePeHo);
            }
            finally
            {
                conexion.Close();
            }

        }

        [ResponseType(typeof(Comentario))]
        [HttpGet]
        [Route("api/GetComentarios")]
        public IHttpActionResult getComentarios(string servicioPersonaId, string lang, int index, int max)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerComentarioServicioPersona", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServicioPersonaId", servicioPersonaId);
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                sqlCmd.Parameters.AddWithValue("@index", index);
                sqlCmd.Parameters.AddWithValue("@max", max);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<Comentario> lst = Conversor.toComentario(dt.Select());

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido

                RespuestaComen.estado = 1;
                RespuestaComen.valor = lst;
                RespuestaComen.mensaje = "OK";
                return Ok(RespuestaComen);
            }
            catch (Exception ex)
            {
                RespuestaComen.estado = 2;
                RespuestaComen.valor = null;
                RespuestaComen.mensaje = ex.Message;
                return Ok(RespuestaComen);
            }
            finally {
                conexion.Close();
            }

        }

        [ResponseType(typeof(Billetera))]
        [HttpGet]
        [Route("api/GetBilletera")]
        public IHttpActionResult getBilletera(string personaId, string lang, String v = null, String d = null)
        {
            String message = "";
            if (validarVersion(ref message, v, d))
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                ///////////////////////////////////////////Post
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("VerBilletera", conexion);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@PersonaId", personaId);
                    //sqlCmd.Parameters.AddWithValue("@lang", lang);                
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Billetera billetera = Conversor.toBilletera(dt.Select().FirstOrDefault());
                    sqlCmd.Parameters.Clear();
                    da.Dispose();
                    dt.Dispose();
                    RespuestaBill.estado = 1;
                    RespuestaBill.valor = billetera;
                    RespuestaBill.mensaje = "OK";
                    return Ok(RespuestaBill);
                }
                catch (Exception ex)
                {
                    RespuestaBill.estado = 2;
                    RespuestaBill.valor = null;
                    RespuestaBill.mensaje = ex.Message;
                    return Ok(RespuestaBill);
                }
                finally
                {
                    conexion.Close();

                }
            }
            else
            {
                RespuestaBill.estado = 2;
                RespuestaBill.valor = null;
                RespuestaBill.mensaje = message;
                return Ok(RespuestaBill);
            }

        }
        //PROCESO CON OPTIMIZACION//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //REEMPLAZANFO AL METODO        [Route("api/GetBilletera")] DE ControladorController  string LANG NO SE USA
        [ResponseType(typeof(RequiereServicio))]
        [HttpGet]
        [Route("api/GetBilletera2")]
        public IHttpActionResult getBilletera2(long personaId, string lang, String v = null, String d = null)
        {
            Respuesta resp = new Respuesta();
            String message = "";
            if (validarVersion(ref message, v, d))
            { resp = contManager.VerBilletera(personaId); }
            else
            {
                resp.estado = 2;
                resp.mensaje = message;
            }

            return Ok(resp);
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        [ResponseType(typeof(BilleteraDetalle))]
        [HttpGet]
        [Route("api/GetBilleteraDetalle2")]
        public IHttpActionResult getBilleteraDetalle(string billeteraId, int index, int max, string lang)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerBilleteraDetalle", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@BilleteraId", billeteraId);
                sqlCmd.Parameters.AddWithValue("@index", index);
                sqlCmd.Parameters.AddWithValue("@max", max);
                sqlCmd.Parameters.AddWithValue("@lang", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<BilleteraDetalle> lst = Conversor.toBilleteraDetalle(dt.Select());

                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido

                RespuestaBiDet.estado = 1;
                RespuestaBiDet.valor = lst;
                RespuestaBiDet.mensaje = "OK";
                return Ok(RespuestaBiDet);
            }
            catch (Exception ex)
            {
                RespuestaBiDet.estado = 2;
                RespuestaBiDet.valor = null;
                RespuestaBiDet.mensaje = ex.Message;
                return Ok(RespuestaBiDet);
            }
            finally
            {
                conexion.Close();
            }

        }


        //PROCESO DE OPTIMIZACIONE///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //REEMPLAZANFO AL METODO        [Route("api/GetBilleteraDetalle")] DE ControladorController 
        [ResponseType(typeof(Post))]
        [HttpGet]
        [Route("api/GetBilleteraDetalle")]
        public IHttpActionResult GetBilleteraDetalle2(long billeteraId, int index, int max, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.VerBilleteraDetalle(billeteraId, index, max, lang);
            return Ok(resp);
        }
        [ResponseType(typeof(BilleteraDetalle))]
        [HttpPost]
        [Route("api/IngresoDinero")]
        public IHttpActionResult IngresoDinero(BE.BilleteraDetalle billeteraDetalle)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.IngresoDinero(ref billeteraDetalle);
            return Ok(resp);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Route("api/saveBilletera")]
        public IHttpActionResult SaveBilletera(BE.Billetera billetera)
        {
            Respuesta resp = new Respuesta();

            resp = contManager.saveBilletera(ref billetera);

            return Ok(resp);
        }
        [HttpPost]
        [Route("api/saveBilleteraDetalle")]
        public IHttpActionResult SaveBilleteraDetalle(BE.BilleteraDetalle billeteraDetalle)
        {
            Respuesta resp = new Respuesta();

            //  resp = contManager.saveBilletera(ref billetera);

            return Ok(resp);
        }


        [HttpGet]
        [Route("api/verRequiereServicio")]///detalla todos los adjudicados por el cliente
        public IHttpActionResult VerRequiereServicio(long personaId, string lang, String v = null, String d = null)
        {
            Respuesta resp = new Respuesta();
            String message = "";
            if (validarVersion(ref message, v, d))
            {

                resp = contManager.VerRequierServicio(personaId, lang);
            }
            else
            {
                resp.estado = 2;
                resp.mensaje = message;

            }
            return Ok(resp);
        }




        [HttpGet]
        [Route("api/verRequiereServicioPaginacion")]///detalla todos los adjudicados por el cliente
        public IHttpActionResult VerRequiereServicioPaginacion(long personaId, int index, int max, string lang, String v = null, String d = null)
        {
            Respuesta resp = new Respuesta();
            String message = "";
            if (validarVersion(ref message, v, d))
            {
                resp = contManager.VerRequierServicioPaginacion(personaId, index, max, lang);
                if (index == 1)
                {
                    resp.mensaje = contManager.cantidadVerRequiereServicio(personaId, lang).valor.ToString();
                }
                else
                {
                    resp.mensaje = "1";
                }
            }
            else
            {
                resp.estado = 2;
                resp.mensaje = message;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/verRequiereServicioPaginacionWeb")]///detalla todos los adjudicados por el cliente
        public IHttpActionResult VerRequiereServicioPaginacionWeb(long personaId, int index, int max, string lang, String v = null, String d = null)
        {
            Respuesta resp = new Respuesta();
            String message = "";
            if (validarVersion(ref message, v, d))
            {
                resp = contManager.VerRequierServicioPaginacionWeb(personaId, index, max, lang);
                if (index == 1)
                {
                    resp.mensaje = contManager.cantidadVerRequiereServicio(personaId, lang).valor.ToString();
                }
                else
                {
                    resp.mensaje = "1";
                }
            }
            else
            {
                resp.estado = 2;
                resp.mensaje = message;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/verRequiereServicioxId")]
        public IHttpActionResult VerRequiereServicioxId(string reqSerId, string lang)
        {
            Respuesta resp = new Respuesta();            
            resp = contManager.verRequiereServicioXid (reqSerId , lang, true);                            
            return Ok(resp);
        }
        [HttpGet]
        [Route("api/verRequiereServicioxIdWeb")]
        public IHttpActionResult VerRequiereServicioxIdWeb(string reqSerId, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.verRequiereServicioXidWeb(reqSerId, lang, true);
            return Ok(resp);
        }
        [HttpGet]
        [Route("api/getReqSerxSerAsigId")]
        public IHttpActionResult getReqSerxSerAsigId(string serAsigId, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.vergetReqSerxSerAsigId(serAsigId, lang);
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/verRequiereServicioProveedores")]
        public IHttpActionResult VerRequiereServicioProveedores(string RequiereServicioId, string lang)
        {
            //SE REQUIERE   PROBAR
            Respuesta resp = new Respuesta();
            resp = contManager.VerRequierServicioProveedores(RequiereServicioId, lang);
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/verCertificado")]///detalla todos los adjudicados por el cliente
        public IHttpActionResult VerCertificado(decimal ServicioPersonaId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.VerServicioPersonaDocumento(ServicioPersonaId);
            return Ok(resp);
        }
        [HttpGet]
        [Route("api/verServicioAsig")]
        public IHttpActionResult VerServicioAsigXid(string ServAsigId, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.VerServicioAsigXid(ServAsigId, lang);
            return Ok(resp);
        }













        [HttpGet]
        [Route("api/getSeguro")]
        public IHttpActionResult VerSeguro(decimal ciudadId, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.Verseguro(ciudadId, lang);
            return Ok(resp);
        }
        [HttpGet]
        [Route("api/verServicioPersona")]
        public IHttpActionResult verServicioPersona(decimal personaId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.VerServicioPersona(personaId);
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/getRendimientoFinanciero")]
        public IHttpActionResult getRendimientoFinanciero(decimal personaId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.getRendimientoFinanciero(personaId);
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/getRendimientoOperativo")]
        public IHttpActionResult getRendimientoOperativo(decimal personaId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.getRendimientoOperativo(personaId);
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/getTermCondPol")]
        public IHttpActionResult VerTermCondPol(decimal PaisId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.VerTermCondPol(PaisId);
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/getStatusServicio")]
        public IHttpActionResult getStatusServicio()
        {
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/getAddressService")]
        public async Task<Respuesta> obtenerDireccionesServicio(string origen, string destino, string modo)
        {
            Respuesta resp = new Respuesta();
            resp.estado = 2;
            String apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKeyDirections"];
            String url = String.Format("https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&key={2}", origen, destino, apiKey);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            try
            {
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                var jsonContent = JsonConvert.DeserializeObject(json);
                resp.valor = jsonContent;
                resp.estado = 1;
                //using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                //using (Stream stream = response.GetResponseStream())
                //using (StreamReader reader = new StreamReader(stream))
                //{                    
                //    resp.valor = await reader.ReadToEndAsync();
                //    resp.estado = 1;
                //}
            }
            catch (Exception ex)
            {
                resp.mensaje = ex.Message.ToString();
            }
            return resp;
        }


        public Boolean validarVersion(ref String message, String v = null, String d = null)
        {
            Boolean bolOK = false;
            String versionA = System.Configuration.ConfigurationManager.AppSettings["VersionAndroid"];
            String versionI = System.Configuration.ConfigurationManager.AppSettings["VersionIOS"];
            Boolean validarV = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["ValidarVersion"]);
            message = "Esta versión ha quedado obsoleta. Por favor actualicese a la nueva versión mayor o igual a: ";

            if (!validarV)
            {
                bolOK = true;

            } else if (v == null)
            {
                message += versionA + " desde Google Play.";
            }
            else if (d == null)
            {
                message = "Marca de Dispositivo no soportado.";
            }
            else
            {
                decimal decV = 0;
                decimal decVersionD = 0;
                decV = Convert.ToDecimal(v);
                if (d == "a")
                {
                    decVersionD = Convert.ToDecimal(versionA);
                    if (decV >= decVersionD)
                    {
                        bolOK = true;
                    }
                    else
                    {
                        message += versionA
                            + "<br/><a href=\"https://play.google.com/store/apps/details?id=com.serviceweb.bo\">Descargar desde Google Play</a>";
                    }
                }
                else if (d == "i")
                {
                    decVersionD = Convert.ToDecimal(versionI);
                    if (decV >= decVersionD)
                    {
                        bolOK = true;
                    }
                }
                else
                {
                    message = "Versión de dispositivo no soportada.";
                }
            }

            return bolOK;
        }

        /////////////////////////////
        // SINIESTRO
        [HttpPost]
        [Route("api/saveSiniestro")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> SaveSiniestro(BE.Siniestro Siniestro)
        {
            Respuesta resp = new Respuesta();
          resp = contManager.saveSiniestro(ref Siniestro);
         
            if (resp.estado == 1)
            {
                BE.Persona persona = new BE.Persona();
                List<BE.Persona> lstPersonas = new List<BE.Persona>();
                persona = contManager.ListadoIdSiniestrosCliente(Siniestro.ServAsigId);
                lstPersonas.Add(persona);
                persona = contManager.ListadoIdSiniestrosProveedor(Siniestro.ServAsigId);
                lstPersonas.Add(persona);
              
                if (Siniestro.TipoEstado == BE.TipoEstado.Insertar)
                {
                    if (Siniestro.SiniestroCreadoPor == true)//PROVEEDOR
                    {
                       
                        await EnviarNotificacionesAsyncCliente(lstPersonas[1], "RegSiniestro", "es", null, 0, 0, Siniestro.ServAsigId);
                        await EnviarNotificacionesAsyncCliente(lstPersonas[0], "AproSiniestro", "es", null, 0, 0, Siniestro.ServAsigId);

                    }
                    if (Siniestro.SiniestroCreadoPor == false)// CLIENTE
                    {
                        await EnviarNotificacionesAsyncCliente(lstPersonas[0], "RegSiniestro", "es", null, 0, 0, Siniestro.ServAsigId);
                        await EnviarNotificacionesAsyncCliente(lstPersonas[1], "AproSiniestro", "es", null, 0, 0, Siniestro.ServAsigId);

                    }

                }
                else
                {


                }




            }

            return resp;
        }
        [HttpPost]
        [Route("api/saveSiniestro_Estado")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> SaveSiniestro_Estado(BE.Siniestro_Estado siniestro_Estado,string lang)
        {
            Respuesta resp = new Respuesta();
           resp = contManager.saveSiniestro_Estado(ref siniestro_Estado);
          // resp.estado = 1; 
            if (resp.estado == 1)
            {
                string ServAsigId = contManager.verServAsigId(siniestro_Estado.SiniestroId);

            
                if ((siniestro_Estado.SiniestroEstadoId == 2) ||(siniestro_Estado.SiniestroEstadoId == 5))//cliente
                {
                   List<BE.Persona> lstPersonas = contManager.ListadoEstadosSiniestros(siniestro_Estado.SiniestroId, siniestro_Estado.SiniestroEstadoId);
           

                    await EnviarNotificacionesAsyncV3(lstPersonas, "ConfSiniestro",lang, null,"",ServAsigId);

                }
                if ((siniestro_Estado.SiniestroEstadoId == 4) || (siniestro_Estado.SiniestroEstadoId == 6))//cliente
                {
                    List<BE.Persona> lstPersonas = contManager.ListadoEstadosSiniestros(siniestro_Estado.SiniestroId, siniestro_Estado.SiniestroEstadoId);


                    await EnviarNotificacionesAsyncV3(lstPersonas, "RechSiniestro", lang, null,"",ServAsigId);

                }




            }


            return resp;
        }
        [HttpGet]
        [Route("api/getEstadoSiniestros")]
        public IHttpActionResult VerEstadoSiniestros(string lang ="")
        {
            Respuesta resp = new Respuesta();
            resp = contManager.VerEstadoSiniestros(lang);
            return Ok(resp);
        }
        [HttpGet]
        [Route("api/verSiniestro")]
        public IHttpActionResult verSiniestro(decimal PersonaId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.VerSiniestro(PersonaId);
            return Ok(resp);
        }

        [HttpGet]
        [Route("api/verSiniestroPaginacion")]
        public IHttpActionResult verSiniestroPaginacion(decimal PersonaId, int index, int max)
        {
            // Respuesta resp = new Respuesta();
            // resp = contManager.verSiniestroPaginacion(PersonaId, index, max);
            // return Ok(resp);
            Respuesta resp = new Respuesta();
            String message = "";
          
                resp = contManager.verSiniestroPaginacion(PersonaId, index, max);
                if (index == 1)
                {
                    resp.mensaje = contManager.cantidadSiniestro(PersonaId).valor.ToString();
                }
                else
                {
                    resp.mensaje = "1";
                }
         
            return Ok(resp);

        }

        private async Task EnviarNotificacionesAsyncSiniestro(List<BE.Persona> lstPersonas, string tipo, string lang, string ServAsigId = null)
        {
            DataRow data = contManager.ListadoDatosNotificacion(tipo, lang);
            var title = data["title"].ToString();
            var body = data["body"].ToString();
            var jsonBody2 = JsonConvert.SerializeObject(body);
            bool b = false;

            switch (tipo)
            {
                case "RegSiniestro":
                    body = String.Format(body, "");
                    break;
                case "AproSiniestro":
                    body = String.Format(body, "");
                    break;


            }
            foreach (var item in lstPersonas)
            {
                var to = item.PersonaTokenId;

                var senderId2 = string.Format("id={0}", SenderIdFB);
                var key = string.Format("key={0}", ServerKey);



                var invasivo = "si";
                var reqservid = "";
                var reqservdes = "";
                var servicioNombre = "Ver Siniestro";
                var ServicioUrlFoto = "";
                if (b == false)
                {
                    title = title + " " + "Sinestro=" + Convert.ToString(ServAsigId);
                    b = true;
                }
                var data2 = new { to, data = new { title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto } };
                jsonBody2 = JsonConvert.SerializeObject(data2);


                //}


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
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, item.PersonaId, conexion);

                        }
                        else
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId, conexion);

                        }
                    }
                }
            }
        }

        private  async Task EnviarNotificacionesProveedores(List<BE.Persona> lstPersonas, string tipo, string lang, BE.RequiereServicio requiereServicio = null, string persona = "")
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
                  
                    break;
            }

            foreach (var item in lstPersonas)
            {

                DataRow dataIdioma = contManager.ListadoDatosNotificacionv2(tipo, item.PersonaIdioma);
                var title = dataIdioma["title"].ToString();
                var body = ""; String.Format(dataIdioma["body"].ToString(), item.NombreCompleto());
                BotonTexto = dataIdioma["BotonTexto"].ToString();
            
               
                    importe = contManager.ImporteRequiereServicio(requiereServicio.RequiereServicioId, requiereServicio.servicio.servicioDetalleTipo);
                    title = "RQ: " + Convert.ToString(requiereServicio.RequiereServicioId);
                    titulo = dataIdioma["body"].ToString().Split('/');
                    var to = item.PersonaTokenId;
                    var senderId2 = string.Format("id={0}", SenderIdFB);
                    var key = string.Format("key={0}", ServerKey);
                    var sound = "default";
                    var invasivo = inv;
                    var reqservid = requiereServicio.RequiereServicioId;
                    var servicioNombre = titulo[0] + requiereServicio.servicio.ServicioNombre + titulo[1] + requiereServicio.personaDireccion.PersonaDireccionDescripcion + "," + requiereServicio.RequiereServicioDescripcion + "." + titulo[2]
                    + requiereServicio.RequiereServicioFechaHoraReq.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + titulo[3] + requiereServicio.RequiereServicioFechaHoraReq.ToString("hh:mm:ss", CultureInfo.InvariantCulture) + titulo[4] + Convert.ToString(importe + requiereServicio.servicio.categoriaServicio.ciudad.configuracionCiudad.ConfiguracionCiudadValorSeguro) + " " + requiereServicio.servicio.categoriaServicio.ciudad.Region.pais.Moneda.MonedaNombre;
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
                        var data2 = new
                        {
                            to,
                            notification = new { sound, title, body, OrganizationId, priority, subtitle },
                            data = new { sound, title, body, invasivo, reqservid, reqservdes, servicioNombre, ServicioUrlFoto, vista, action, tag }
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
                               // InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + dataIdioma["title"].ToString() + dataIdioma["body"].ToString(), DateTime.Now, to, item.PersonaId);

                            }
                            else
                            {
                              //  InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, item.PersonaId);

                            }
                        }
                    }


                    /*  if (notificacionPersona != null)
                      {
                          bcNotificacionPersona.saveNotificacionPersona(ref notificacionPersona);
                          await NotificacionFirebase(Convert.ToInt32(item.PersonaId), "recibido", "Notificaciones");
                      }*/
                


            }




        }

        ///////////////////////////////
        #region "NOTIFICACIONES"  

        [HttpPut]
        [Route("api/EnviarNotificacionesAsyncCliente")]
        private async Task EnviarNotificacionesAsyncCliente(BE.Persona persona, string tipo, string lang, BE.RequiereServicio requiereServicio = null, decimal Importe = 0, decimal Calificacion = 0,string SiniestroId=null)
        {
            DataRow data = contManager.ListadoDatosNotificacionv2(tipo, persona.PersonaIdioma);
            var title= data["title"].ToString();
            var body= data["body"].ToString();
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
                case "ClienteCancela":
                    body = string.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId=16;
                    title = title + " RQ: " + ReqId;
                    break;
                case "ClienteCancela1P":
                    body = string.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 16;
                    title = title + " RQ: " + ReqId;
                    break;




                case "ClienteReque":
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;                
                    notificacionPersona.ConceptoNotificacionId = 1;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteInicioServicio":
                    body = body + " " + requiereServicio.servicio.ServicioNombre ;
                    body = String.Format(body, persona);
                    Invasivo = false;
                    ReqId = requiereServicio.RequiereServicioId;
                    notificacionPersona.ConceptoNotificacionId = 7;
                    title = title + " " + "RQ: " + ReqId;
                    break;
                case "ClienteFinServicio":
                    titulo = body.Split('/');
                    body = "";                   
                    body = titulo[0] + string.Format("{0:N}", Convert.ToInt32(Importe)) + " " + Convert.ToString(persona.Ciudad.Region.pais.Moneda.MonedaNombre)+titulo[1] ;                 
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
                    title = titulo[0] + string.Format("{0:N}", Convert.ToInt32(Importe)) + " "+ persona.Ciudad.Region.pais.Moneda.MonedaNombre;
                    body = " "+ titulo[3];
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
                    notificacionPersona.ConceptoNotificacionId =10;
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
                        case "DesiertoRechazado":
                    Invasivo = false;
                    notificacionPersona.ConceptoNotificacionId = 12;
                    title = title + " " + "RQ: " + requiereServicio.RequiereServicioId;
                    break;
                case "RequerimientoCancelado":
                    Invasivo = true;
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
            versionTelefono = contManager.VersionTelefono(persona.PersonaId);

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
                    if (tipo == "ClienteInicioServicio" || tipo== "ClienteConfV2")
                    {
                        vista = "TabAgenda";
                    }
                    var data2 = new { to, notification = new { priority, title, body, invasivo, vista, reqservid, action, ServicioUrlFoto, servicioNombre, reqservdes } };
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
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, persona.PersonaId, conexion);

                        }
                        else
                        {
                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, persona.PersonaId, conexion);

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
                    contManager.saveNotificacionPersona(ref notificacionPersona);
                }
                /////////////////

                /////////////////////////////
                await NotificacionFirebase(Convert.ToInt32(persona.PersonaId), "recibido", "Notificaciones");
            

         
        }


        private async void EnviarNotificacionesAsyncClienteSinTask(BE.Persona persona, string tipo, string lang, BE.RequiereServicio requiereServicio = null, decimal Importe = 0, decimal Calificacion = 0, string SiniestroId = null)
        {
            DataRow data = contManager.ListadoDatosNotificacionv2(tipo, persona.PersonaIdioma);
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
            versionTelefono = contManager.VersionTelefono(persona.PersonaId);

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
                    InsertarLogNotificacion("badge", DateTime.Now, Convert.ToString(badge), persona.PersonaId, conexion);

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
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + data["title"].ToString() + data["body"].ToString(), DateTime.Now, to, persona.PersonaId, conexion);

                    }
                    else
                    {
                        InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode), DateTime.Now, to, persona.PersonaId, conexion);

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
                contManager.saveNotificacionPersona(ref notificacionPersona);
            }
            /////////////////

            /////////////////////////////
            await NotificacionFirebase(Convert.ToInt32(persona.PersonaId), "recibido", "Notificaciones");



        }

        [HttpGet]
        [Route("api/verNotificacionPersona")]///detalla todos los adjudicados por el cliente
        public async Task<Respuesta> verNotificacionPersona(long personaId, string lang)
        {
            Respuesta resp = new Respuesta();
            String message = "";


            resp = contManager.verNotificacionPersona(personaId, lang);
 

            return (resp);
        }

        [HttpGet]
        [Route("api/verNotificacionPersonaPaginacion")]
        public IHttpActionResult verNotificacionPersonaPaginacion(decimal PersonaId,string lang , int index, int max)
        {
            // Respuesta resp = new Respuesta();
            // resp = contManager.verSiniestroPaginacion(PersonaId, index, max);
            // return Ok(resp);
            Respuesta resp = new Respuesta();
            String message = "";

            resp = contManager.verNotificacionPersonaPaginacion(PersonaId,lang, index, max);
            if (index == 1)
            {
                resp.mensaje = contManager.cantidadNotificacionPersona(PersonaId).valor.ToString();
            }
            else
            {
                resp.mensaje = "1";
            }

            return Ok(resp);

        }

        [HttpPut]
        [Route("api/NotificacionPersona")]///detalla todos los adjudicados por el cliente
        public async Task<Respuesta> saveNotificacionPersona(BE.NotificacionPersona notificacionPersona)
        {
            Respuesta resp = new Respuesta();
            String message = "";

            resp = contManager.saveNotificacionPersona(ref notificacionPersona);
            if (resp.estado == 1)
            {
                if (notificacionPersona.TipoEstado == BE.TipoEstado.Modificar)
                {
                    if (notificacionPersona.EstadoNotificacionId == 2)
                    {
                       await NotificacionFirebase(Convert.ToInt32(notificacionPersona.PersonaId), "leido","Notificaciones");

                    }
                }
            }


            return (resp);
        }
        #endregion
        ////////////////////////////////


        public DataSet ObtenerImporte_y_Calificacion(string ServAsigId)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            DataSet ds = new DataSet();
            try
            {
                SqlCommand sqlCmd = new SqlCommand("ObtenerImporte_y_Calificacion", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.AddWithValue("@ServAsigId", ServAsigId);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

                da.Fill(ds);


                sqlCmd.Parameters.Clear();
                da.Dispose();

                ////////////////////////////////////////////
                ////////////////////////////////////////////////PostContenido


            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.Close();
            }
            return ds;
        }


        [HttpPost]
        [Route("api/saveLog")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> SaveLog(BE.LogMovil logMovil)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.saveLogMovil(ref logMovil);

            return resp;
        }

        [HttpPut]
        [Route("api/NotificacionFirebase")]
        public async Task<int> NotificacionFirebase(int PersonaId, string tipo,string opcion)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {


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
                                    data.Post = 0 ;
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

        [HttpPut]
        [Route("api/OfertaFirebase")]
        public async Task<int> OfertaFirebase(string idRequest, int PersonaId,string idEstado,string tipo, string opcion)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                FirebaseResponse response = await client.GetTaskAsync("Requerimientos/" + idRequest);
                var Requerimientos = new Requerimientos{
                      idPersona =Convert.ToString(PersonaId),           
                     idRequest=idRequest,
                     servicioId ="",
                     fecha ="",
                     hora="",
                     estado= "",              
                     descripcion="",
                     arch1="",
                     arch2="",
                     arch3="",
                     arch4="",
                     dirLatitud="",
                    dirLong="",
                    dirTitulo="",
                    tipoSolicitud ="",
                    diasRest="",
                    cantOfer="",
                    colorServicio="",
                    idEstado= idEstado,
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

            return 0;
        }


        [HttpPut]
        [Route("api/EstaAdjudicadoFirebase")]
        public async Task<string> EstaAdjudicadoFirebase(string idRequest, string tipo)
        {
            client = new FireSharp.FirebaseClient(config);
            string estado = "";
            if (client != null)
            {
                FirebaseResponse response = await client.GetTaskAsync("EstadoRequerimiento/" + idRequest);
                var EstadoAdjudicado = new EstadoAdjudicado
                {
                    estaAdjudicado = "NO",
                  
                };

                if (response.Body != "null")
                {

                    EstadoAdjudicado obj = response.ResultAs<EstadoAdjudicado>();
                    EstadoAdjudicado = obj;
                    if (tipo == "adjudicado")
                    {
                        EstadoAdjudicado.estaAdjudicado = "SI";
                        estado = "SI";

                    }
                   
                  

                    FirebaseResponse responseUpdate = await client.UpdateTaskAsync("EstadoRequerimiento/" + idRequest, EstadoAdjudicado);
                    Requerimientos result = responseUpdate.ResultAs<Requerimientos>();
                }
                else
                {
                    if (tipo == "recibido")
                    {
                        SetResponse responseInsert = await client.SetTaskAsync("EstadoRequerimiento/" + idRequest, EstadoAdjudicado);
                        Requerimientos result = responseInsert.ResultAs<Requerimientos>();

                        estado = "NO";
                    }
                }




            }

            return estado;
        }


        [HttpPost]
        [Route("api/FirebaseSaveRequiereServicio")]
        public async Task<string> FirebaseSaveRequiereServicio(string idRequest)
        {
           
            client = new FireSharp.FirebaseClient(config);
            string estado = "";
            Respuesta respuesta = new Respuesta();
            respuesta=contManager.verRequiereServicioXidFirebase(idRequest, "es", true);
            BE.RequiereServicio requiereServicio = (BE.RequiereServicio)respuesta.valor;
            if (client != null)
            {
                
                FirebaseResponse response = await client.GetTaskAsync("Sincronizacion/" + requiereServicio.RequiereServicioId);
                var RequiereServicioS = new RequiereServicioS
                {
                    RequiereServicioid = requiereServicio.RequiereServicioId,
                    PersonaId=requiereServicio.PersonaId,
                    PersonaNombre = requiereServicio.persona.PersonaNombres,
                    PersonaURLFoto=requiereServicio.persona.PersonaURLFoto,
                    RequerimientoPrecio = "",
                    RequiereServicioFHDeseada = Convert.ToString(requiereServicio.RequiereServicioFHDeseada),
                    RequiereServicioDescripcion = requiereServicio.RequiereServicioDescripcion,
                    EstadoReqServId = Convert.ToString(requiereServicio.EstadoReqServId),
                    PersonaDireccionS = requiereServicio.personaDireccion.PersonaDireccionGeo,
                    PersonaDireccionNombre = requiereServicio.personaDireccion.PersonaDireccionTitulo + "@" + requiereServicio.personaDireccion.PersonaDireccionDescripcion,
                    ServicioId = requiereServicio.IdiomaServ,
                    RequiereServicioURLFoto1 = requiereServicio.RequiereServicioURLFoto1,
                    RequiereServicioURLFoto2= requiereServicio.RequiereServicioURLFoto2,
                RequiereServicioURLFoto3 = requiereServicio.RequiereServicioURLFoto3,
                RequiereServicioURLVideo = requiereServicio.RequiereServicioURLVideo,
                PersonaTelefono =requiereServicio.persona.PersonaTelefono,
                    RequiereServicioFHCaduca=Convert.ToString(requiereServicio.RequiereServicioFHCaduca)
                };               
                var ServAsigS = new ServAsigS
                {

                    ServAsigId = "",
                    ProveedorNombre = "",
                    ServAsigFHInicio ="",
                    ServAsigFHFin="",
                    ServAsigFHPago="",
                    StatusServAsigId="",
                    servAsigCalificacion =""
                };

              
                var Sincronizacion = new Sincronizacion
                {
                   
                Create = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                    RequiereServicioS = RequiereServicioS,
                    ServAsigS = ServAsigS,
                    RequiereServicioProveedoresS = requiereServicio.RequiereServicioProveedoresF
                };

                if (response.Body != "null")
                {

                }
                else
                {
                    SetResponse responseInsert = await client.SetTaskAsync("Sincronizacion/" + idRequest, Sincronizacion);


                    
                }
            }

                return estado;
        }

        [HttpGet]
        [Route("api/VerAdjudicado")]
        public async Task<string> VerAdjudicado(string idRequest)
        {
            client = new FireSharp.FirebaseClient(config);
            string estado = "";
            if (client != null)
            {
                FirebaseResponse response = await client.GetTaskAsync("EstadoRequerimiento/" + idRequest);
                var EstadoAdjudicado = new EstadoAdjudicado
                {
                    estaAdjudicado = "NO",

                };

                if (response.Body != "null")
                {

                    EstadoAdjudicado obj = response.ResultAs<EstadoAdjudicado>();
                    EstadoAdjudicado = obj;

                    estado = obj.estaAdjudicado;
                 



                    FirebaseResponse responseUpdate = await client.UpdateTaskAsync("EstadoRequerimiento/" + idRequest, EstadoAdjudicado);
                    Requerimientos result = responseUpdate.ResultAs<Requerimientos>();
                }
               
                



            }

            return estado;
        }


        [HttpGet]
        [Route("api/BuscarPersonaxId")]
        public BE.Persona BuscarPersonaxId(decimal personaId)
        {
             BE.Persona Persona = contManager.BuscarPersonaxId(personaId);

            return Persona;
        }

        [HttpPost]
        [Route("api/LogSesionesPersona")]////Metodo unificado en requiereServicio

        public async Task<Respuesta> SaveLogSesionesPersona(BE.LogSesionesPersona logSesionesPersona)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.saveLogSesionesPersona(ref logSesionesPersona);



            return resp;
        }

        [HttpGet]
        [Route("api/ListadoIdSiniestros")]////Metodo unificado en requiereServicio

        public async Task<Respuesta> ListadoIdSiniestros(string ServAsigId)
        {
            Respuesta resp = new Respuesta();
          //  resp.valor = contManager.ListadoIdSiniestros(ServAsigId);
          


            return resp;
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
        [Route("api/verServicioDetalle")]
        public IHttpActionResult verServicioDetalle(decimal servicioID,string lang)
        {
            Respuesta resp = new Respuesta();
            String message = "";
          
                resp = contManager.VerServicioDetalle(servicioID, lang,true);
                        
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/saveAdjudicacion")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> SaveAdjudicacion(string requiereServicioId,decimal ProveedorId,string  StatusRequiereId)
        {
            Respuesta resp = new Respuesta();
            Respuesta respCliente = new Respuesta();
            string adjudicado = "";
            string lang = "";
            Boolean bolOk = false;
            Boolean bolOkCancelado = false;
          
           bolOk = contManager.ver_Si_se_AdjudicoProveedores(requiereServicioId,lang);
         // adjudicado = await VerAdjudicado(requiereServicioId);
            bolOkCancelado = contManager.ver_Si_se_EstaCancelado(requiereServicioId, lang);
            Respuesta respr = new Respuesta();          
            respr = contManager.verRequiereServicioXid(requiereServicioId, "es", false);
            BE.RequiereServicio requiereServicio = (BE.RequiereServicio)respr.valor;
            if (bolOkCancelado == false) {

                if (bolOk == false) {
                    if ((StatusRequiereId == "1"))
                    {
                        //await EstaAdjudicadoFirebase(requiereServicio.RequiereServicioId, "adjudicado");
                        resp = contManager.SaveAdjudicacion(requiereServicioId, ProveedorId, StatusRequiereId, lang);

                    }


                }
                if (StatusRequiereId == "0")
                {
                    resp = contManager.SaveAdjudicacion(requiereServicioId, ProveedorId, StatusRequiereId, lang);
                 
                }
                if (bolOk == true)
                {
                    BE.Persona Persona = contManager.BuscarPersonaxId(ProveedorId);
                    await EnviarNotificacionesAsyncCliente(Persona, "ProveeorPerdioAdj", Persona.PersonaIdioma, requiereServicio);
                }


            }




            System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(
                    token => EnviarNotificacionesAdjudicados(requiereServicio, lang,StatusRequiereId, ProveedorId, token ));

            return resp;


          

        }


        [HttpPost]
        [Route("api/saveCobranzaCBA")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> saveCobranzaCBA(BE.cobranzaCBA cobranzaCBA)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.saveCobranaCBA(ref cobranzaCBA);
            if (resp.estado == 1)
            {

                if (cobranzaCBA.TipoEstado == BE.TipoEstado.Insertar)
                {  /////////////////INSERCION FIREBASE

                    BE.Persona Persona = contManager.BuscarPersonaxId(cobranzaCBA.personaId);
                    await EnviarNotificacionesAsyncCliente(Persona, "ClienteCBA", Persona.PersonaIdioma, null, 0, 0, Convert.ToString(cobranzaCBA.cobranzaCBAId));

                }



            }


            return resp;
        }


        [HttpGet]
        [Route("api/verRequiereServicioDetalle")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> verRequiereServicioDetalle(string RequiereServicioId )
        {
            Respuesta resp = new Respuesta();
            resp = contManager.verRequiereServicioDetalle(RequiereServicioId);
        


            return resp;
        }

        [HttpPost]
        [Route("api/enviarCorreoVerificacionWeb")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> enviarCorreoVerificacion(string correo,string URL, decimal personaId)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.enviarCorreoVerificacionWeb(correo,URL,personaId);



            return resp;
        }

        [HttpGet]
        [Route("api/verServicio")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> enviarCorreoVerificacion(decimal ServicioId,string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.verServicio(ServicioId, lang);



            return resp;
        }
        [HttpPut]
        [Route("api/actualizar_passwordWeb")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> actualizar_passwordWeb(decimal idp, string password)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.actualizar_passwordWeb(idp, password);



            return resp;
        }

        [HttpGet]
        [Route("api/ver_Si_se_AdjudicoProveedores")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> ver_Si_se_AdjudicoProveedores(String RequiereServicioId)
        {
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            resp.valor = contManager.ver_Si_se_AdjudicoProveedores(RequiereServicioId, "es");
            resp.mensaje = "";


            return resp;
        }

        [HttpGet]
        [Route("api/UserProfile")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> UserProfile(string UserName, string Password)
        {
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            resp.valor = contManager.UserProfile(UserName,Password);
            resp.mensaje = "";


            return resp;
        }

        [HttpGet]
        [Route("api/Empresa_Usuario")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> Empresa_Usuario(int Nit)
        {
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            resp.valor = contManager.Empresa_Usuario(Nit);
            resp.mensaje = "";


            return resp;
        }

        [HttpGet]
        [Route("api/Empresa_Usuario_Corporativo")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> Empresa_Usuario_Corporativo(decimal PersonaId)
        {
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            resp.valor = contManager.Empresa_Usuario_Corporativo(PersonaId);
            resp.mensaje = "";


            return resp;
        }



        [HttpGet]
        [Route("api/Empresa_Usuario_Corporativo2")]////Metodo unificado en requiereServicio
        public async Task<Respuesta> Empresa_Usuario_Corporativo2(decimal PersonaId)
        {
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            resp.valor = contManager.Empresa_Usuario_Corporativo(PersonaId);
            resp.mensaje = "";


            return resp;
        }




        [HttpGet]
        [Route("api/Persona_existe_Prueba")]
        public async Task<Respuesta> Persona_existe_Prueba(decimal personaid)
        {
            BE.Persona Personag = contManager.BuscarPersonaxId_enprueba(personaid);

            Respuesta resp = new Respuesta();
            resp.estado = 1;
            resp.valor = Personag;
            resp.mensaje = "";
            return resp;
        }










        [HttpGet]
        [Route("api/EnviarNotificacionOperaciones")]
        public async Task<Respuesta> EnviarNotificacionOperaciones()
        {
            List<BE.Persona> lstPersonas = contManager.ver_lista_operaciones();
            await EnviarNotificacionesjulieta("noticicaiones_1j", lstPersonas, "es");
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            resp.valor = lstPersonas;
            resp.mensaje = "";
            return resp;
        }






        public async Task EnviarNotificacionesjulieta(string tipo, List<BE.Persona> lstPersonas, string lang)
        {

            foreach (var item in lstPersonas)
            {



                DataRow data = contManager.ListadoDatosNotificacionv2(tipo, lang);
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
                string action1 = "inf";
                string versionTelefono = "";
                int ContadorBadge = 0;
                BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();
                switch (tipo)
                {
                    case "noticicaiones_1j":
                        body = String.Format(body, item);
                        Invasivo = false;
                        notificacionPersona.ConceptoNotificacionId = 15;
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

                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + title, DateTime.Now, to, item.PersonaId, conexion);
                        }
                        else
                        {

                            InsertarLogNotificacion(Convert.ToString(result.IsSuccessStatusCode) + title, DateTime.Now, to, item.PersonaId, conexion);
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
                    contManager.saveNotificacionPersona(ref notificacionPersona);
                    await NotificacionFirebase(Convert.ToInt32(item.PersonaId), "recibido", "Notificaciones");
                }
            }

        }
        [HttpPost]
        [Route("api/registrar_promocion_requiereservicioid")]
        public async Task<Respuesta> registrar_promocion_requiereservicioid(BE.PromocionDetalleRequerimiento promocionDetalleRequerimiento, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.registrar_promocion_requiereservicioid(ref promocionDetalleRequerimiento, lang);
            return resp;
        }
                [HttpPost]
        [Route("api/registrar_promocion_persona")]
        public async Task<Respuesta> registrar_promocion_persona(BE.PromocionDetallePersona promocionDetallePersona, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.registrar_promocion_persona(ref promocionDetallePersona, lang);
            return resp;
        }








        [HttpGet]
        [Route("Api/Lista_Promociones_x_Persona")]
        public IHttpActionResult Lista_Promociones_x_Persona(decimal PersonaId, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.Lista_Promociones_x_Persona(PersonaId, lang);
            return Ok(resp);
        }

        
        
        
        
        
        [HttpGet]
        [Route("Api/existe_requ")]
        public IHttpActionResult existe_requ(string requiereservicioid, string lang)
        {

            List<BE.PromocionDetalleRequerimiento> lista1 = contManager.existe_reqservid_en_promodetallereq(requiereservicioid, lang);
            int exitesino = lista1.Count;

            return Ok(exitesino);
        }
        [HttpGet]
        [Route("Api/guardarcosto1")]
        public Respuesta guardarcosto1(string requiereservicioid, decimal costof, string lang)
        {

            Respuesta RespuestaServAsig = new Respuesta();
        
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();


                List<BE.PromocionDetalleRequerimiento> lista1 = contManager.existe_reqservid_en_promodetallereq(requiereservicioid, lang);
                int exitesino = lista1.Count;
                decimal resultadot = 0;

                if (exitesino > 0)
                {
                    decimal promoid = 0;
                    decimal personaidd = 0;
                    foreach (var lispromodet_req in lista1)
                    {
                        promoid = lispromodet_req.PPromocionId;
                        personaidd = lispromodet_req.PPersonaId;
                    }

                    List<BE.Promocion> listap = contManager.listar_promocion(promoid, lang);

                    decimal valor = 0;
                    bool por_cantidad = false;
                    foreach (var lispromo in listap)
                    {
                        valor = lispromo.PromocionValor;
                        por_cantidad = lispromo.PromocionPorc;
                    }

                    List<BE.PromocionDetallePersona> listapp = contManager.listar_promociondetalleper(promoid,personaidd, lang);


                    decimal valor2 = 0;
                    foreach (var lispromodp in listapp)
                    {
                        valor2 = lispromodp.Valor;
                    }
                    var valor_cotizado = costof;


                    if (por_cantidad == false)
                    {

                        if (valor2 == 0)
                        {
                            if (valor_cotizado >= valor)
                            {
                                var resultat = valor_cotizado - valor;
                                resultadot = Convert.ToDecimal(resultat);
                            }
                            else {
                             
                                Respuesta resp45 = new Respuesta();
                                var resultado = valor - valor_cotizado;
                                BE.PromocionDetallePersona obj45 = new BE.PromocionDetallePersona();
                                obj45.PromocionId = promoid;
                                obj45.PersonaId = personaidd;
                                obj45.FechaInsercion = DateTime.Now;
                                obj45.Estado = true;
                                obj45.Valor = resultado;
                                obj45.TipoEstado = BE.TipoEstado.Modificar;


                                resp45 = contManager.registrar_promocion_persona(ref obj45, lang);

                                resultadot = 0;
                            }


                        }
                        else {


                            if ( valor_cotizado >= valor2)
                            {
                                Respuesta resp45 = new Respuesta();
                                var resultado = valor_cotizado - valor2;
                                BE.PromocionDetallePersona obj45 = new BE.PromocionDetallePersona();
                                obj45.PromocionId = promoid;
                                obj45.PersonaId = personaidd;
                                obj45.FechaInsercion = DateTime.Now;
                                obj45.Estado = false;
                                obj45.Valor = 0;
                                obj45.TipoEstado = BE.TipoEstado.Modificar;


                                resp45 = contManager.registrar_promocion_persona(ref obj45, lang);
                                resultadot = resultado;


                            }
                            else {
                                Respuesta resp45 = new Respuesta();
                                var resultado = valor2 - valor_cotizado;
                                BE.PromocionDetallePersona obj45 = new BE.PromocionDetallePersona();
                                obj45.PromocionId = promoid;
                                obj45.PersonaId = personaidd;
                                obj45.FechaInsercion = DateTime.Now;
                                obj45.Estado = true;
                                obj45.Valor = resultado;
                                obj45.TipoEstado = BE.TipoEstado.Modificar;


                                resp45 = contManager.registrar_promocion_persona(ref obj45, lang);
                                resultadot = 0;


                            }



                         



                        }




                    }
                    else
                    {
                        var resul1 = ((valor / 100) * costof);
                        var resultat = costof - resul1;
                        resultadot = Convert.ToDecimal(resultat);
                    }

                }
                    
                  /*  for (int io=1; io<=1;io++) {

                          decimal valorp = 0;
                        if (io == 1)
                        {
                         valorp= resultadot;


                        }
                        string IdCosto = ObtenerId(conexion, "servAsigCosto", null);
                        SqlCommand sqlCmd1 = new SqlCommand("[InsertarservAsigCosto]", conexion);
                        sqlCmd1.CommandType = CommandType.StoredProcedure;
                        sqlCmd1.CommandTimeout = 0;
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoId", IdCosto);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigId", "CJ1023");
                        sqlCmd1.Parameters.AddWithValue("@ConceptoCostoId",1);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoValor", valorp); ;

                        resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());
             
                        ////////////////////////////////////////////////////////////////////////////
                    }*/


                RespuestaServAsig.estado = 1;
                RespuestaServAsig.valor = resultadot;
                RespuestaServAsig.mensaje = "registrado";

            }
            catch (Exception ex)
            {
                RespuestaServAsig.estado = 2;
                RespuestaServAsig.valor = null;
                RespuestaServAsig.mensaje = ex.Message;

            }
            finally
            {
                conexion.Close();
            }
            return RespuestaServAsig;
        }

        /////////////////////////NOTIFICACIONES HUAWEI 
     

        [HttpGet]
        [Route("Api/ObtenerToken")]

        public  async Task<RespuestaHuawei> GetToken()
        {
            string clientId = "103014213";
            string clientSecret = "dc762e9851f704725c4d542d2d1a65d89e4cca32b104a6b5a1d2a744ae2fe1f9";
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            using (var client = new HttpClient())
            {
                //Define Headers
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

               // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", (credentials));

                //Prepare Request Body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                List<KeyValuePair<string, string>> requestData1 = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("clientId", "103014213"));
                List<KeyValuePair<string, string>> requestData2 = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("clientSecret", "dc762e9851f704725c4d542d2d1a65d89e4cca32b104a6b5a1d2a744ae2fe1f9"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);
                FormUrlEncodedContent requestBody1 = new FormUrlEncodedContent(requestData1);

                //Request Token
                var request = await client.PostAsync("https://oauth-login.cloud.huawei.com/oauth2/v3/token", requestBody);
                var response = await request.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RespuestaHuawei>(response);
            }
        }


        [HttpGet]
        [Route("Api/ObtenerToken2")]

        public async Task<string> GetToken2()
        {
            var client = new RestClient("https://oauth-login.cloud.huawei.com/oauth2/v3/token");
            var request = new RestRequest(Method.POST);
            RespuestaHuawei respuestaHuawei = new RespuestaHuawei();
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
           
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=103014213&client_secret=dc762e9851f704725c4d542d2d1a65d89e4cca32b104a6b5a1d2a744ae2fe1f9", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            respuestaHuawei=JsonConvert.DeserializeObject<RespuestaHuawei>(response.Content);

            return respuestaHuawei.access_token;
        }

        [HttpGet]
        [Route("Api/EnviarNotificacionHuawei")]

        public async Task<string> EnviarNotificacionHuawei(EstructuraHuawei estructuraHuawei,string token)
        {

           /* var client = new RestClient("https://push-api.cloud.huawei.com/v1/103014213/messages:send");
            var request = new RestRequest(Method.POST);
       
            request.AddHeader("authorization", token);
            request.AddBody(estructuraHuawei);
            IRestResponse response = client.Execute(request);

            int i = 0;
           /* return JsonConvert.DeserializeObject<RespuestaHuawei>(response.Content);

            //////////////////////////////

            // var client = new RestClient("https://push-api.cloud.huawei.com/v1/103014213/messages:send");

            HttpClient client = new HttpClient();
            var request = new RestRequest(Method.POST);
            //  request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=103014213&client_secret=dc762e9851f704725c4d542d2d1a65d89e4cca32b104a6b5a1d2a744ae2fe1f9", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            client.DefaultRequestHeaders.Add("Bearer Token", "CgB6e3x9XDewWLJdajPmADQX87aq6JQwdBbej/fRey3BoL/COemR306a/u702OC39ed90cjXgTZ8Dk1sP1Wb/ty2");
            HttpResponseMessage resp = client.PostAsJsonAsync("https://push-api.cloud.huawei.com/v1/103014213/messages:send", estructuraHuawei).Result;*/
            return "";
        }

        [HttpGet]
        [Route("Api/EnviarNotificacionHuaweiV2")]

        public async Task<string> EnviarNotificacionHuawei2(EstructuraHuawei estructuraHuawei, string token)
        {

            //var client = new RestClient("https://push-api.cloud.huawei.com/v1/103014213/messages:send");
           // var request = new RestRequest(Method.POST);

          //  request.AddHeader("authorization", token);
          //  request.AddBody(JsonConvert.SerializeObject(estructuraHuawei));
          //  IRestResponse response = client.Execute(request);

          //  int i = 0;
          //  return JsonConvert.DeserializeObject<RespuestaHuawei>(response.Content);

             //////////////////////////////

          //*  var client = new RestClient("https://push-api.cloud.huawei.com/v1/103014213/messages:send");

          //   HttpClient client1 = new HttpClient();
         //    var request = new RestRequest(Method.POST);
          //    request.AddHeader("cache-control", "no-cache");
          //   request.AddHeader("content-type", "application/x-www-form-urlencoded");
          //  request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=103014213&client_secret=dc762e9851f704725c4d542d2d1a65d89e4cca32b104a6b5a1d2a744ae2fe1f9", ParameterType.RequestBody);
          //   IRestResponse response = client.Execute(request);
          //   client.DefaultRequestHeaders.Add("Bearer Token", "CgB6e3x9XDewWLJdajPmADQX87aq6JQwdBbej/fRey3BoL/COemR306a/u702OC39ed90cjXgTZ8Dk1sP1Wb/ty2");
           //  HttpResponseMessage resp = client.PostAsJsonAsync("https://push-api.cloud.huawei.com/v1/103014213/messages:send", estructuraHuawei).Result;*/
            return "";
        }



        /*
        [HttpGet]
        [Route("Api/EnviarNotificacionHuaweiV3")]

        public async Task<string> EnviarNotificacionHuawei3(EstructuraHuawei estructuraHuawei, string token)
        {
            RestClient client = new RestClient("http://www.example.com/");
            RestRequest request = new RestRequest("login", Method.POST);
            request.AddHeader("Accept", "application/json");
            var body = new
            {
                Host = "host_environment",
                Username = "UserID",
                Password = "Password"
            };
            request.AddJsonBody(body);

            var response = client.Execute(request).Content;
        }

        */


        [HttpPost]
        [Route("api/registrar_billeteraPagoTarjeta")]
        public async Task<Respuesta> registrar_billeteraPagoTarjeta(BE.BilleteraPagoTarjeta billeteraPagoTarjeta, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.registrar_billeteraPagoTarjeta(ref billeteraPagoTarjeta, lang);
            return resp;
        }


        [HttpGet]
        [Route("api/buscar_billeteraPagoTarjeta_X_codigo")]
        public async Task<Respuesta> buscar_billeteraPagoTarjeta_X_codigo(string requiereservicioid, string lang)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.buscar_billeteraPagoTarjeta_X_codigo(requiereservicioid,lang);
            return resp;
        }



        [HttpGet]
        [Route("api/BuscarPorSecuencia")]
        public async Task<Respuesta> BuscarPorSecuencia(decimal secuencia)
        {
            Respuesta resp = new Respuesta();
            resp = contManager.BuscarPorSecuencia(secuencia);
            return resp;
        }

        /*
                [HttpGet]
                [Route("Api/url_pago")]
                public IHttpActionResult url_pago(decimal PersonaId, decimal monto)
                {

                    string valorencriptar1 = PersonaId.ToString();
                    string encriptado1 = contManager.devolver_encriptado(valorencriptar1);
                    string valorencriptar2 = monto.ToString();
                    string encriptado2 = contManager.devolver_encriptado(valorencriptar2);




                }

                */



    }
}
