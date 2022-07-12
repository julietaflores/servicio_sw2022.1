using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using PayPal.Api;
using ServiciosWeb.WepApi.Models;
using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.WepApi.Models.Entities;
using System.Web.Http.Cors;
namespace ServiciosWeb.WepApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PackParameterController : ApiController
    {
        SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["con"]);
        ServiciosWeb.WepApi.Models.PackParameter PackParameter = new ServiciosWeb.WepApi.Models.PackParameter();
        ServiciosWeb.Datos.Modelo.Respuesta<PackParameter> Respuesta = new ServiciosWeb.Datos.Modelo.Respuesta<PackParameter>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<Ciudad>> RespuestaCiudad = new ServiciosWeb.Datos.Modelo.Respuesta<List<Ciudad>>();
        ServiciosWeb.Datos.Modelo.Respuesta<DatosPaypal> RespuestaPayPal = new ServiciosWeb.Datos.Modelo.Respuesta<DatosPaypal>();
        // ServiciosWeb.WepApi.Models.Configuration Configuration = new ServiciosWeb.WepApi.Models.Configuration();
        // GET: PackParameter
        private PackParameterManager packParameterManager = null;
        public PackParameterController()
        {
            packParameterManager = new PackParameterManager("cadenaCnx");
        }

        private decimal decVersionD = 0;

        [HttpPost]
        [Route("api/RealizarPago")]
        public void _9RealizarPago(decimal ServAsigCostoTotal, string RequiereServicioId, string ServAsigId, decimal ProveedorId)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            int resp = 0;
           // decimal PersonaId = ObtenerPersonaId(RequiereServicioId, conexion);
            //decimal Saldo = ValidarSaldo(PersonaId);
            using (SqlTransaction DataTransaction = conexion.BeginTransaction())
            {
                try
                {


                    if (1 >= ServAsigCostoTotal)
                    {

                        ///////////////////////Insertando detalle billetera
                        SqlCommand sqlCmd = new SqlCommand("[InsertarMedioPago_Billetera]", conexion);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@ServAsigId", ServAsigId);
                        sqlCmd.Parameters.AddWithValue("@PersonaId", 1);
                        sqlCmd.Parameters.AddWithValue("@ProveedorId", ProveedorId);
                        sqlCmd.Parameters.AddWithValue("@ServAsigCostoTotal", ServAsigCostoTotal);
                        sqlCmd.Transaction = DataTransaction;
                        resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
                        if (resp > 0) { DataTransaction.Commit(); } else { DataTransaction.Rollback(); }


                        //DataTransaction.Commit();

                        //////////////////////////

                    }





                }

                catch (Exception ex)
                {

                    throw;
                }



            }




        }


        /// <summary>
        /// Obtiene el paquete de Parametros
        /// </summary>
        [HttpGet]
        [Route("api/PackParameter")]
        public ServiciosWeb.Datos.Modelo.Respuesta<PackParameter> GetPackParameter2(String lang)
        {
            //return listEmp.First(e => e.ID == id);  
            try
            {
                //, String v = null, String d = null
                //String message = "";
                //if (!validarVersion( ref message, v, d))
                //{
                //    throw new Exception(message);
                //}               

                conexion.Open();
                ///////////////////////////////////////////Pais
                SqlCommand sqlCmd = new SqlCommand("PaisSP", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<Pais> LPais = dt.AsEnumerable().Select(row => 
                new Pais { PaisId = row.Field<decimal?>(0).GetValueOrDefault(), PaisNombre = row.Field<string>(1) }).ToList();
                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ///////////////////////////////////////////////EstadoPersona
                SqlCommand sqlCmd1 = new SqlCommand("EstadoPersonaSP", conexion);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                List<EstadoPersona> LEstadoPersona = dt1.AsEnumerable().Select(row => new EstadoPersona { EstadoPersonaId = row.Field<decimal?>(0).GetValueOrDefault(), EstadoPersonaNombreTipo = row.Field<string>(1) }).ToList();
                sqlCmd1.Parameters.Clear();
                da1.Dispose();
                dt1.Dispose();
                //////////////////////////////////////////////Genero
                SqlCommand sqlCmd2 = new SqlCommand("GeneroSP", conexion);
                sqlCmd2.CommandType = CommandType.StoredProcedure;
                sqlCmd2.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da2 = new SqlDataAdapter(sqlCmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                List<Genero> LGenero = dt2.AsEnumerable().Select(row => new Genero { GeneroId = row.Field<decimal?>(0).GetValueOrDefault(), GeneroNombreTipo = row.Field<string>(1) }).ToList();
                sqlCmd2.Parameters.Clear();
                da2.Dispose();
                dt2.Dispose();
                //////////////////////////////////////////////Region
                SqlCommand sqlCmd3 = new SqlCommand("RegionSP", conexion);
                sqlCmd3.CommandType = CommandType.StoredProcedure;
                sqlCmd3.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da3 = new SqlDataAdapter(sqlCmd3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                List<Region> LRegion = dt3.AsEnumerable().Select(row => new Region { RegionId = row.Field<decimal?>(0).GetValueOrDefault(), RegionNombre = row.Field<string>(1), PaisId = row.Field<decimal>(2) }).ToList();
                sqlCmd3.Parameters.Clear();
                da3.Dispose();
                dt3.Dispose();
                //////////////////////////////////////////////Ciudad
                SqlCommand sqlCmd4 = new SqlCommand("CiudadSP", conexion);
                sqlCmd4.CommandType = CommandType.StoredProcedure;
                sqlCmd4.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da4 = new SqlDataAdapter(sqlCmd4);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                List<Ciudad> LCiudad = dt4.AsEnumerable().Select(row => new Ciudad { CiudadId = row.Field<decimal?>(0).GetValueOrDefault(), CiudadNombre = row.Field<string>(1), RegionId = row.Field<decimal>(2) }).ToList();
                sqlCmd4.Parameters.Clear();
                da4.Dispose();
                dt4.Dispose();
                /////////////////////////////////////////////TipoDireccion
                SqlCommand sqlCmd5 = new SqlCommand("TipoDireccionSP", conexion);
                sqlCmd5.CommandType = CommandType.StoredProcedure;
                sqlCmd5.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da5 = new SqlDataAdapter(sqlCmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                List<TipoDireccion> LTipoDireccion = dt5.AsEnumerable().Select(row => new TipoDireccion { TipoDireccionId = row.Field<decimal?>(0).GetValueOrDefault(), TipoDireccionNombreTipo = row.Field<string>(1) }).ToList();
                sqlCmd5.Parameters.Clear();
                da5.Dispose();
                dt5.Dispose();
                //////////////////////////////////////////////TipoDocumento
                SqlCommand sqlCmd6 = new SqlCommand("TipoDocumentoSP", conexion);
                sqlCmd6.CommandType = CommandType.StoredProcedure;
                sqlCmd6.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da6= new SqlDataAdapter(sqlCmd6);
                DataTable dt6 = new DataTable();
                da6.Fill(dt6);
                List<TipoDocumento> LTipoDocumento = dt6.AsEnumerable().Select(row => new TipoDocumento { TipoDocumentoId = row.Field<decimal?>(0).GetValueOrDefault(), TipoDocumentoNombre = row.Field<string>(1) }).ToList();
                sqlCmd6.Parameters.Clear();
                da6.Dispose();
                dt6.Dispose();
                ////////////////////////////////////////////////////////////tipoLogin
                SqlCommand sqlCmd7 = new SqlCommand("TipoLoginSP", conexion);
                sqlCmd7.CommandType = CommandType.StoredProcedure;
                sqlCmd7.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da7 = new SqlDataAdapter(sqlCmd7);
                DataTable dt7 = new DataTable();
                da7.Fill(dt7);
                List<TipoLogin> LTipoLogin = dt7.AsEnumerable().Select(row => new TipoLogin { TipoLoginId = row.Field<decimal?>(0).GetValueOrDefault(), TipoLoginNombreTipo = row.Field<string>(1) }).ToList();
                sqlCmd7.Parameters.Clear();
                da7.Dispose();
                dt7.Dispose();

                //////////////////////////////////////////////////////////////TipoPersona
                SqlCommand sqlCmd8 = new SqlCommand("TipoPersonaSP", conexion);
                sqlCmd8.CommandType = CommandType.StoredProcedure;
                sqlCmd8.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da8 = new SqlDataAdapter(sqlCmd8);
                DataTable dt8 = new DataTable();
                da8.Fill(dt8);
                List<TipoPersona> LTipoPersona = dt8.AsEnumerable().Select(row => new TipoPersona { TipoPersonaId = row.Field<decimal?>(0).GetValueOrDefault(), TipoPersonaNombreTipo = row.Field<string>(1) }).ToList();
                sqlCmd8.Parameters.Clear();
                da8.Dispose();
                dt8.Dispose();

                //////////////////////////////////////////////////////////////TipoPost
                SqlCommand sqlCmd9 = new SqlCommand("TipoPostSP", conexion);
                sqlCmd9.CommandType = CommandType.StoredProcedure;
                sqlCmd9.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da9 = new SqlDataAdapter(sqlCmd9);
                DataTable dt9 = new DataTable();
                da9.Fill(dt9);
                List<TipoPost> LTipoPost = dt9.AsEnumerable().Select(row => new TipoPost { TipoPostId = row.Field<decimal?>(0).GetValueOrDefault(), TipoPostNombre = row.Field<string>(1) }).ToList();
                sqlCmd9.Parameters.Clear();
                da9.Dispose();
                dt9.Dispose();

                ////////////////////////////////////////////////////////////////
                Respuesta.estado = 1;
                PackParameter.paises = LPais;
                PackParameter.estadoPersonas = LEstadoPersona;
                PackParameter.generos = LGenero;
                PackParameter.regiones = LRegion;
                PackParameter.ciudades = LCiudad;
                PackParameter.tipoDirecciones = LTipoDireccion;
                PackParameter.tipoDocumentos = LTipoDocumento;
                PackParameter.tipoPersonas = LTipoPersona;
                PackParameter.tipoPosts = LTipoPost;
                Respuesta.mensaje = "";
                Respuesta.valor = PackParameter;

            }
            catch (Exception ex)
            {
               Respuesta.estado = 2;
               Respuesta.mensaje = ex.Message;
            }
            finally
            {
                conexion.Close();
            }              
            return Respuesta;
        }

        [HttpGet]
        [Route("api/GetCities")]
        public ServiciosWeb.Datos.Modelo.Respuesta<List<Ciudad>> GetCities2(String lang)
        {
            try
            {
                conexion.Open();
                SqlCommand sqlCmd4 = new SqlCommand("CiudadesSP", conexion);
                sqlCmd4.CommandType = CommandType.StoredProcedure;
                sqlCmd4.Parameters.AddWithValue("@IdiomaSigla", lang);
                SqlDataAdapter da4 = new SqlDataAdapter(sqlCmd4);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                List<Ciudad> LCiudad = dt4.AsEnumerable().Select(row => new Ciudad { CiudadId = row.Field<decimal?>(0).GetValueOrDefault(), CiudadNombre = row.Field<string>(1), RegionId = row.Field<decimal>(2) }).ToList();
                sqlCmd4.Parameters.Clear();
                da4.Dispose();
                dt4.Dispose();

                RespuestaCiudad.mensaje = "";
                RespuestaCiudad.valor = LCiudad;
            }
            catch (Exception ex)
            {
                RespuestaCiudad.estado = 2;
                RespuestaCiudad.mensaje = ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return RespuestaCiudad;
        }
      
        [HttpGet]
        [Route("api/GetPackParameter2")]
        public Respuesta GetPackParameter(String lang)
        {
            Respuesta resp = new Respuesta();
            resp = packParameterManager.GetPackParameter(lang);

            return (resp);


        }

        [HttpGet]
        [Route("api/GetCities2")]
        public Respuesta GetCities(String lang)
        {

            Respuesta resp = new Respuesta();
            resp = packParameterManager.GetCities(lang);

            return (resp);


        }

        ///////////////////////////////
        [HttpGet]
        [Route("api/PaymentWithCreditCard")]
        public HttpResponseMessage Get(string cardData)
        {
            CardInput cartInput = JsonConvert.DeserializeObject<CardInput>(cardData);
            PayPalRequest payPalRequest = new PayPalRequest(cartInput);

            try
            {
                RequestFlow flow = payPalRequest.GetFlow();
                return Request.CreateResponse(HttpStatusCode.OK, flow);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Fail");
            }
        }
        ///////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("api/PaymentWithCreditCard1")]
        public ServiciosWeb.Datos.Modelo.Respuesta<DatosPaypal> PaymentWithCreditCard1(DatosPaypal payPal)
        {
            //create and item for which you are taking payment
            //if you need to add more items in the list
            //Then you will need to create multiple item objects or use some loop to instantiate object
            Item item = new Item();
            item.name = payPal.item_name;
            item.currency = payPal.item_currency;
            item.price = payPal.item_price;
            item.quantity = payPal.item_quantity;
            item.sku = payPal.string_sku;

            //Now make a List of Item and add the above item to it
            //you can create as many items as you want and add to this list
            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

            //Address for the payment
            Address billingAddress = new Address();
            billingAddress.city = payPal.billingAddress_city;
            billingAddress.country_code = payPal.billingAddress_country_code ;
            billingAddress.line1 = payPal.billingAddress_line1;
            billingAddress.postal_code = payPal.billingAddress_postal_code ;
            billingAddress.state = payPal.billingAddress_state ;


            //Now Create an object of credit card and add above details to it
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = payPal.crdtCard_cvv2;
            crdtCard.expire_month = payPal.crdtCard_expire_month;
            crdtCard.expire_year = payPal.crdtCard_expire_year;
            crdtCard.first_name = payPal.crdtCard_first_name;
            crdtCard.last_name = payPal.crdtCard_last_name;
            crdtCard.number = payPal.crdtCard_number;
            crdtCard.type = payPal.crdtCard_type;

            // Specify details of your payment amount.
            Details details = new Details();
            details.shipping = payPal.details_shipping;
            details.subtotal = payPal.details_subtotal;
            details.tax = payPal.details_tax;

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount();
            amnt.currency = payPal.amnt_currency;
            // Total = shipping tax + subtotal.
            amnt.total =payPal.amnt_total;
            amnt.details = details;

            // Now make a trasaction object and assign the Amount object
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = payPal.tran_description;
            tran.item_list = itemList;
            tran.invoice_number = payPal.tran_invoice_number;

            // Now, we have to make a list of trasaction and add the trasactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of FundingIntrument

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // Now create Payer object and assign the fundinginstrument list to the object
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = payPal.payr_payment_method;

            // finally create the payment object and assign the payer object & transaction list to it
            Payment pymnt = new Payment();
            pymnt.intent = payPal.pymnt_intent;
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {
                //getting context from the paypal, basically we are sending the clientID and clientSecret key in this function 
                //to the get the context from the paypal API to make the payment for which we have created the object above.

                //Code for the configuration class is provided next

                // Basically, apiContext has a accesstoken which is sent by the paypal to authenticate the payment to facilitator account. An access token could be an alphanumeric string

                //APIContext apiContext = Configuration.GetAPIContext();

                // Create is a Payment class function which actually sends the payment details to the paypal API for the payment. The function is passed with the ApiContext which we received above.

               // Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.State is "approved" it means the payment was successfull else not

            /*    if (createdPayment.state.ToLower() != "approved")
                {
                    //return View("FailureView");
                    RespuestaPayPal.mensaje = "FailureView No fue aprobado";
                }*/
            }
            catch (PayPal.PayPalException ex)
            {
                Models.Logger.Log("Error: " + ex.Message);
                RespuestaPayPal.mensaje = "FailureView"+ex.Message;
              //  return View("FailureView");

            }
            RespuestaPayPal.mensaje = "SuccessView";
            // return View("SuccessView");
            return RespuestaPayPal;
        }
        [HttpGet]
        [Route("api/PaymentWithPaypal")]
        public string PaymentWithPaypal()
        {
            //getting the apiContext as earlier.

         //   APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = "";// Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist

                    //it is returned by the create function call of the payment class

                    // Creating a payment

                    // baseURL is the url on which paypal sendsback the data.

                    // So we have provided URL of this controller only
                    //"http://localhost:58563/Paypal/PaymentWithPayPal?";
                //    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session

                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url

                    //on which payer is redirected for paypal acccount payment

               //     var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal in response to Create function call

                   // var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                  /*  while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }*/

                    // saving the paymentID in the key guid
                 //  Session.Add(guid, createdPayment.id);

                    return (paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                   // var guid = Request.Params["guid"];

               //    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                
                 //   if (ExecutedPayment.state.ToLower() != "approved")
                   // {
                     //   return View("FailureView");
                    //}

                }
            }
            catch (Exception ex)
            {
                Models.Logger.Log("Error" + ex.Message);
                //return View("FailureView");
                return  ex.Message;
            }
            return "realizado";
             //return View("SuccessView");
        }

        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Item Name",
                currency = "USD",
                price = "5",
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "5"
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = "7", // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = "your invoice number",
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);

        }

        /////////////////////////////////////////////////////////////////
        ///
       

    }
}
