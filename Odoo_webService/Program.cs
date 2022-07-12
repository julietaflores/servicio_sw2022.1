using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.ComponentModel;

using System.Reflection;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Odoo_webService
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static HttpClient client1 = new HttpClient();
        static void Main(string[] args)
        {

            /*  bool Fin = false;
             int cant = 0;
             while (Fin == false)
              {
                  ObtenerDatosOdooProductos(cant, ref Fin);
                  cant = cant + 10;
              }
              cant = 0;
             Fin = false;
              while (Fin==false)
              {                         
                 ObtenerDatosOdooclientesManual(cant,ref Fin);              
                  cant = cant + 10;
              }
              cant = 0;
              Fin = false;
              while (Fin ==false)
              {
                   ObtenerDatosOdooOrdenesManual(cant,ref Fin);
                   cant = cant + 10;
              }*/
            /*int  cant = 0;
             Boolean    Fin = false;
                while (Fin == false)
                 {
                 ObtenerDatosOdoodetalleManual2(cant,ref Fin);               
                 cant = cant + 5;
                 }
                 cant = 0;
                 Fin = false;*/

            int cant = 0;
            int ciclo = 0;
            Boolean Fin = false;
            DateTime fecha = DateTime.Now.AddDays(-136);

            while (ciclo <= 20)
            {
                string fecha_Orden = fecha.Year + "-" + string.Format("{0:00}", fecha.Month) + "-" + string.Format("{0:00}", fecha.Day);
                 /*while (Fin == false)
                  {
                      ObtenerDatosOdooOrdenesManual(cant, ref Fin, fecha_Orden);
                      cant = cant + 3;
                  }*/

                cant = 0;
                Fin = false;
                while (Fin == false)
                {
                    ObtenerDatosOdoodetalleManual2(cant, ref Fin, fecha_Orden);
                    cant = cant + 2;
                }
                cant = 0;
                Fin = false;
                ciclo = ciclo + 1;
                fecha = fecha.AddDays(-1);
            }

        }
        static void ObtenerDatosOdoodetalleManual2(int cant, ref Boolean Fin, string fecha_Orden)
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);

            SqlCommand cmd = new SqlCommand();
            RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
            if (conexion.State != ConnectionState.Open) conexion.Open();
            try
            {
                /////////////////////////////
                const string ex1 = "C:\\";
                const string ex2 = @"C:\Odoo_exe\ecp_main.exe";

                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = @"C:\Odoo_exe\ecp_main.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = " -m orders_detail -i " + fecha_Orden + " -o " + cant + " -l 2";

                try
                {
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                        var resultado = exeProcess.StandardOutput.ReadToEnd();
                        if (resultado != "")
                        {
                            respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);

                            if (respuestaOdoo.estado == 1)
                            {


                                ordenes_detalle obj = new ordenes_detalle();
                                var printObj = (respuestaOdoo.valor);
                                string valor = printObj.ToString();
                                if (valor != "[]")
                                {

                                    List<ordenes_detalle> lstobj = JsonConvert.DeserializeObject<List<ordenes_detalle>>(valor);
                                    foreach (ordenes_detalle item in lstobj)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.Connection = conexion;
                                        cmd.Parameters.Add("@Nro", SqlDbType.VarChar).Value = item.Nro;
                                        cmd.Parameters.Add("@Order_id", SqlDbType.VarChar).Value = item.order_id;
                                        cmd.CommandText = "select count(Nro) from Ordenes_Detalle where Nro=@Nro and Order_id=@Order_id and Order_id in (select ID FROM Ordenes)";
                                        int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                                        if (cantidad == 0)
                                        {
                                            cmd.Parameters.Clear();
                                            cmd.Connection = conexion;
                                            cmd.Parameters.Add("@Nro", SqlDbType.Int).Value = item.Nro;
                                            cmd.Parameters.Add("@Order_id", SqlDbType.VarChar).Value = item.order_id;
                                            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = item.codigo;
                                            cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = item.cantidad;
                                            cmd.Parameters.Add("@U_m", SqlDbType.VarChar).Value = item.u_m;
                                            cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = item.precio;
                                            cmd.Parameters.Add("@Sub_Total", SqlDbType.Decimal).Value = item.sub_Total;
                                            cmd.CommandText = "insert into [dbo].[Ordenes_Detalle](Nro,Order_id,Codigo,Cantidad,U_m,Precio,Sub_Total)" +
                                                "values(@Nro,@Order_id,@Codigo,@Cantidad,@U_m,@Precio,@Sub_Total)";
                                            cmd.ExecuteNonQuery();


                                        }




                                    }
                                }
                                else
                                {
                                    Fin = true;
                                }

                            }


                        }
                        else
                        {
                            cant = cant - 2;
                            cmd.Connection = conexion;
                            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooOrdenesManual";
                            cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                            cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = "VALOR=vacio";

                            cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                                "values(@fecha,@descripcion,@estado,@nombreProceso)";
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                        }


                    }
                }
                catch (Exception ex)
                {

                    cant = cant - 2;
                    cmd.Connection = conexion;
                    cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooOrdenesManual";
                    cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                    cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = ex.Message;

                    cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                        "values(@fecha,@descripcion,@estado,@nombreProceso)";
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                ////////////////////////////////
            }
            catch (Exception ex)
            {

                cant = cant - 5;
                cmd.Connection = conexion;
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooOrdenesManual";
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = ex.Message;

                cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                    "values(@fecha,@descripcion,@estado,@nombreProceso)";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            finally
            {

                conexion.Close();
            }


        }

        static void ObtenerDatosOdooOrdenesManual(int cant, ref Boolean Fin, string fecha_Orden)
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);

            SqlCommand cmd = new SqlCommand();
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                if (conexion.State != ConnectionState.Open) conexion.Open();

                string valor = "datos";
                string ejecutable = @"C:\Odoo_exe\ecp_main.exe";
                string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, " -m orders -i " + fecha_Orden + " -o" + cant + " -l 3");
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p = System.Diagnostics.Process.Start(psi);
                p.WaitForExit();
                string er = p.StandardError.ReadToEnd();
                if (er != "")
                {

                }
                else
                {
                    var resultado = p.StandardOutput.ReadToEnd();
                    respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);
                    if (respuestaOdoo.estado == 1)
                    {


                        ordenes clientes = new ordenes();
                        var printObj = (respuestaOdoo.valor);
                        valor = printObj.ToString();
                        if (valor != "[]")
                        {
                            List<ordenes> lstobj = JsonConvert.DeserializeObject<List<ordenes>>(valor);
                            foreach (ordenes item in lstobj)
                            {
                                cmd.Parameters.Clear();
                                cmd.Connection = conexion;
                                cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = item.id;
                                cmd.CommandText = "select count(Id) from Ordenes where Id=@Id";
                                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                                if (cantidad == 0)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Connection = conexion;
                                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.id;
                                    cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = item.codigo;
                                    if (item.fecha_creacion != "false")
                                    {
                                        cmd.Parameters.Add("@Fecha_Creacion", SqlDbType.DateTime).Value = Convert.ToDateTime(item.fecha_creacion);

                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@Fecha_Creacion", SqlDbType.DateTime).Value = DBNull.Value;
                                    }

                                    cmd.Parameters.Add("@Cod_cliente", SqlDbType.VarChar).Value = item.cod_cliente;
                                    cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = item.total;
                                    cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = item.estado;
                                    cmd.Parameters.Add("@Id_cliente", SqlDbType.Int).Value = item.id_cliente;
                                    if (item.fecha_entrega != "false")
                                    {
                                        cmd.Parameters.Add("@Fecha_Entrega", SqlDbType.DateTime).Value = Convert.ToDateTime(item.fecha_entrega);

                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@Fecha_Entrega", SqlDbType.DateTime).Value = DBNull.Value;
                                    }
                                    if (item.fecha_prevista != "false")
                                    {
                                        cmd.Parameters.Add("@Fecha_Prevista", SqlDbType.DateTime).Value = Convert.ToDateTime(item.fecha_prevista);

                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@Fecha_Prevista", SqlDbType.DateTime).Value = DBNull.Value;
                                    }

                                    cmd.CommandText = "insert into [dbo].[Ordenes](Id,Codigo,Fecha_Creacion,Cod_cliente,Total,Estado,Id_Cliente,Fecha_Entrega,Fecha_Prevista)" +
                                    "values(@Id,@Codigo,@Fecha_Creacion,@Cod_cliente,@Total,@Estado,@Id_cliente,@Fecha_Entrega,@Fecha_Prevista)";
                                    cmd.ExecuteNonQuery();


                                }



                            }
                        }
                        else
                        {
                            Fin = true;
                        }

                    }


                }





            }
            catch (Exception ex)
            {

                cant = cant - 3;
                cmd.Parameters.Clear();
                cmd.Connection = conexion;
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooOrdenesManual";
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = ex.Message;

                cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                    "values(@fecha,@descripcion,@estado,@nombreProceso)";
                cmd.ExecuteNonQuery();

            }
            finally
            {

                conexion.Close();
            }


        }
        static async Task<string> CreateProductAsync(Parametros parametros)
        {
            Respuesta respuesta = null;
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync(
                   
                    "wWebApi.Web/api/login/authenticate", parametros);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode== System.Net.HttpStatusCode.OK)
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    
                    respuesta = JsonConvert.DeserializeObject<Respuesta>(resultString);
                              

                }
                // return URI of the created resource.
               // return response.Headers.Location;
            }
            catch (Exception EX)
            {

                throw EX;
            }
            return respuesta.Data;
        }
     
        static string ObtenerToken()
        {
            string Token = "";
            Respuesta respuesta = new Respuesta();
            if (client.BaseAddress == null)
                client.BaseAddress = new Uri("http://190.104.2.122/wWebApi.Web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Parametros parametros = new Parametros();
            parametros.UserName = "admin";
            parametros.Password = "123456";
            parametros.Code = "IV001";
            HttpResponseMessage resp = client.PostAsJsonAsync("api/login/authenticate", parametros).Result;

            if (resp.IsSuccessStatusCode)
            {
                respuesta = resp.Content.ReadAsAsync<Respuesta>().Result;
                //   return resultado;
            }
            else
            {
                /* var resultado = resp.Content.ReadAsStringAsync().Result;
                 var result = JsonConvert.DeserializeObject<ResultServer>(resultado);
                 throw new Exception(string.Format("Message:{0}, ExceptionMessage: {1}", result.Message, result.ExceptionMessage));*/
            }
            return respuesta.Data; ;
        }
        static void InsertarPedidos(cabeceraPedido cabeceraPedido)
        {
            if (client.BaseAddress == null)
            {

                client.BaseAddress = new Uri("http://190.104.2.122/wWebApi.Web/");
             
            }
            HttpResponseMessage resp = client.PostAsJsonAsync("api/Pedido", cabeceraPedido).Result;

            //Add headers

            //Call client.PostAsJsonAsync to send a POST request to the appropriate URI   

            //This method throws an exception if the HTTP response status is an error code.  
            //var xx = resp.EnsureSuccessStatusCode();
            if (resp.IsSuccessStatusCode)
            {
                var resultado = resp.Content.ReadAsAsync<RespuestaPedido>().Result;
                //   return resultado;
                int i = 0;
            }
            else
            {
                /* var resultado = resp.Content.ReadAsStringAsync().Result;
                 var result = JsonConvert.DeserializeObject<ResultServer>(resultado);
                 throw new Exception(string.Format("Message:{0}, ExceptionMessage: {1}", result.Message, result.ExceptionMessage));*/
            }

        }
        static void ObtenerDatosOdoo()
        {
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                //  btnExec.Enabled = false;
                //this.UseWaitCursor = true;
               //tring Token = ObtenerToken();
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                // string ejecutable = @"E:\INSUMOS\DESCARGAS_01042020\PROYECTO_ODOO\ecp_main.exe"; // aqui puedes poner la ruta a python.exe si no está 
                // en la variable de entorno PATH
                //  string ejecutable = @"E:\INSUMOS\DESCARGAS_01042020\PROYECTO_ODOO\ecp_main.exe";
                string ejecutable = @"C:\Odoo_exe\ecp_main.exe";
                string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, "-m quotations -i " + fecha + "  -f " + fecha);
                // psi.WindowStyle = ProcessWindowStyle.Hidden
                // psi.WorkingDirectory = IO.Path.GetDirectoryName(txtDir.Text.Trim) ' Cambias el directorio activo a tu path
                // psi.Arguments = txtDir.Text.Trim & "\" & txtFile.Text.Trim
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p = System.Diagnostics.Process.Start(psi);
                p.WaitForExit();
                string er = p.StandardError.ReadToEnd();
                if (er != "")
                {

                }
                else
                {
                    var resultado = p.StandardOutput.ReadToEnd();
                    respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);


                    if (respuestaOdoo.estado == 1)
                    {
                        cabeceraPedido cabeceraPedido = new cabeceraPedido();
                        var printObj = (respuestaOdoo.valor);
                        string valor = printObj.ToString();
                        List<CabeceraPedidoOdoo> lstcabeceraPedidoOdoos = JsonConvert.DeserializeObject<List<CabeceraPedidoOdoo>>(valor);
                        foreach (CabeceraPedidoOdoo item in lstcabeceraPedidoOdoos)
                        {
                            /////////////////////////////////
                            fechaOdoo = Convert.ToDateTime(item.fecha);
                            fechaFin = Convert.ToString(fechaOdoo.Day) + "/" + Convert.ToString(fechaOdoo.Month) + "/" + Convert.ToString(fechaOdoo.Year); ;
                            cabeceraPedido.Id = Convert.ToString(item.codigo);
                            cabeceraPedido.Fecha = fechaFin;
                            cabeceraPedido.FechaEntrega =fechaFin;
                            cabeceraPedido.Id_Cliente = "187936"; //Convert.ToString(item.cod_cliente);
                            cabeceraPedido.id_Distribuidora = "1";

                            //////////////////////////////////////////
                            string stringDetalle = item.detalle.ToString();
                            List<DetallePedidoOdoo> lstdetallePedidoOdoo = JsonConvert.DeserializeObject<List<DetallePedidoOdoo>>(stringDetalle);
                            //////ESTRUCTURANDO LA CLASE A INSERTAR

                            List<DetallePedido> lstdetallePedidos = new List<DetallePedido>();
                            //////////////////////////////////////
                            foreach (DetallePedidoOdoo itemdp in lstdetallePedidoOdoo)
                            {
                                DetallePedido detallePedido = new DetallePedido();
                                detallePedido.Nro = Convert.ToInt32(itemdp.nro);
                                detallePedido.Codigo = itemdp.codigo;
                                detallePedido.Cantidad = Convert.ToInt32(itemdp.cantidad);
                                if (itemdp.u_m == "Display10Unidades") itemdp.u_m = "DISPLAY";
                                if (itemdp.u_m == "Display20Unidades") itemdp.u_m = "DISPLAY";
                                if (itemdp.u_m == "Unidades") itemdp.u_m = "UNIDAD";
                                detallePedido.Und_Medida = itemdp.u_m ;
                                detallePedido.ICE = 0;
                                lstdetallePedidos.Add(detallePedido);
                            }
                            cabeceraPedido.DetallePedido = lstdetallePedidos;
                            ////////

                            InsertarPedidos( cabeceraPedido);
                        }


                        // clients = clientarray.ToObject<List<valor>>();
                        //  CabeceraPedidoOdoo cabeceraPedido1 = (CabeceraPedidoOdoo)va;
                        //    var clientarray = respuestaOdoo["items"].Value<JArray>();

                        // return View(clients);

                        int i = 0; ;
                    }
                    ///////////////////////////////////

                    //////////////////////////////////7

                }

            }
            catch ( Exception ex )
            {

                throw ex;
            }
         
        }

        static void ObtenerDatosOdooclientes()
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);

            SqlCommand cmd = new SqlCommand();
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                if (conexion.State != ConnectionState.Open) conexion.Open();
                /////////////////
                cmd.Parameters.Clear();
                cmd.Connection = conexion;              
                cmd.CommandText = "delete from clientes";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                ////////////////
                string valor = "datos";
                int cant = 0;
                while (valor !="[]")
                {
                    string ejecutable = @"C:\Odoo_exe\ecp_main.exe";
                    string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, "-m clients -o "+cant+" -l 10");
                    psi.UseShellExecute = false;
                  
                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p = System.Diagnostics.Process.Start(psi);
                    p.WaitForExit();
                    string er = p.StandardError.ReadToEnd();
                    if (er != "")
                    {

                    }
                    else
                    {
                        var resultado = p.StandardOutput.ReadToEnd();
                        //Unexpected error: <class 'TimeoutError'>{"estado": 2, "mensaje": "<class 'TimeoutError'>"}

                        
                        respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);
                        if (respuestaOdoo.estado == 1)
                        {

                      
                            clientes clientes = new clientes();
                            var printObj = (respuestaOdoo.valor);
                            valor = printObj.ToString();
                            if (valor != "[]")
                            { 
                            List<clientes> lstclientes = JsonConvert.DeserializeObject<List<clientes>>(valor);
                            foreach (clientes item in lstclientes)
                            {
                                cmd.Connection = conexion;
                                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.id;
                                cmd.Parameters.Add("@Cod_Cliente", SqlDbType.VarChar).Value = item.cod_cliente;
                                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = item.nombre;
                                cmd.Parameters.Add("@Nit", SqlDbType.VarChar).Value = item.nit;
                                cmd.Parameters.Add("@Movil", SqlDbType.VarChar, 100).Value = item.movil;
                                cmd.Parameters.Add("@Ciudad", SqlDbType.VarChar).Value = item.ciudad;
                                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = item.direccion;
                                cmd.CommandText = "insert into [dbo].[Clientes](Id,Cod_Cliente,Nombre,Nit,Movil,Ciudad,Direccion)" +
                                    "values(@Id,@Cod_Cliente,@Nombre,@Nit,@Movil,@Ciudad,@Direccion)";
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                System.Threading.Thread.Sleep(2000);
                                
                                }
                            }

                        }
                    

                    }
                    if (cant == 0)
                    {
                        cant = 10;
                    }
                    else 
                    {
                        cant = cant + 10;
                    }
                    System.Threading.Thread.Sleep(2000);
                }

              

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {

                conexion.Close();
            }
          

        }

        static void ObtenerDatosOdooclientesManual( int cant,ref Boolean fin )
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);

            SqlCommand cmd = new SqlCommand();
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                if (conexion.State != ConnectionState.Open) conexion.Open();
                /////////////////
               /* cmd.Parameters.Clear();
                cmd.Connection = conexion;
                cmd.CommandText = "delete from clientes";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();*/
                ////////////////
                string valor = "datos";
            
               
                    string ejecutable = @"C:\Odoo_exe\ecp_main.exe";
                    string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, "-m clients -o " + cant + " -l 20");
                    psi.UseShellExecute = false;

                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p = System.Diagnostics.Process.Start(psi);
                    p.WaitForExit();
                    string er = p.StandardError.ReadToEnd();
                    if (er != "")
                    {

                    }
                    else
                    {
                        var resultado = p.StandardOutput.ReadToEnd();
                        //Unexpected error: <class 'TimeoutError'>{"estado": 2, "mensaje": "<class 'TimeoutError'>"}


                        respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);
                        if (respuestaOdoo.estado == 1)
                        {


                            clientes clientes = new clientes();
                            var printObj = (respuestaOdoo.valor);
                            valor = printObj.ToString();
                        if (valor != "[]")
                        {
                            List<clientes> lstclientes = JsonConvert.DeserializeObject<List<clientes>>(valor);
                            foreach (clientes item in lstclientes)
                            {
                                cmd.Parameters.Clear();
                                cmd.Connection = conexion;
                                cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = item.id;
                                cmd.CommandText = "select count(Id) from Clientes where Id=@Id";
                                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                                if (cantidad == 0)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Connection = conexion;
                                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.id;
                                    cmd.Parameters.Add("@Cod_Cliente", SqlDbType.VarChar).Value = item.cod_cliente;
                                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = item.nombre;
                                    cmd.Parameters.Add("@Nit", SqlDbType.VarChar).Value = item.nit;
                                    cmd.Parameters.Add("@Movil", SqlDbType.VarChar, 100).Value = item.movil;
                                    cmd.Parameters.Add("@Ciudad", SqlDbType.VarChar).Value = item.ciudad;
                                    cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = item.direccion;
                                    cmd.CommandText = "insert into [dbo].[Clientes](Id,Cod_Cliente,Nombre,Nit,Movil,Ciudad,Direccion)" +
                                        "values(@Id,@Cod_Cliente,@Nombre,@Nit,@Movil,@Ciudad,@Direccion)";
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();

                                }



                            }
                        }
                        else 
                        {
                            fin = true;
                        }

                        }


                    }
                   
                 
                



            }
            catch (Exception ex)
            {
                cant = cant - 10;
                cmd.Connection = conexion;
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooclientesManual";
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value =ex.Message;

                cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                    "values(@fecha,@descripcion,@estado,@nombreProceso)";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            finally
            {

                conexion.Close();
            }


        }
        static void ObtenerDatosOdooProductos(int cant,ref Boolean Fin)
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);
            SqlCommand cmd = new SqlCommand();
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                if (conexion.State != ConnectionState.Open) conexion.Open();
               
                string valor = "datos";

           
                    string ejecutable = @"C:\Odoo_exe\ecp_main.exe";
                    string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, "-m products -o " + cant + " -l 10");
                    psi.UseShellExecute = false;
                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p = System.Diagnostics.Process.Start(psi);
                    p.WaitForExit();
                    string er = p.StandardError.ReadToEnd();if (er != ""){    }
                    else
                    {
                        var resultado = p.StandardOutput.ReadToEnd();
                        respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);
                        if (respuestaOdoo.estado == 1)
                        {

                            if (conexion.State != ConnectionState.Open) conexion.Open();
                            productos obj = new productos();
                            var printObj = (respuestaOdoo.valor);
                            valor = printObj.ToString();
                            if (valor != "[]")
                            {
                             
                                    List<productos> lstobj = JsonConvert.DeserializeObject<List<productos>>(valor);
                                    foreach (productos item in lstobj)
                                    {

                                    cmd.Parameters.Clear();
                                    cmd.Connection = conexion;
                                    cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = item.id;
                                    cmd.CommandText = "select count(Id) from Productos where Id=@Id";
                                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                                    if (cantidad == 0)
                                    {
                                        cmd.Parameters.Clear();

                                        cmd.Connection = conexion;
                                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.id;
                                        cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = item.codigo;
                                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = item.nombre;
                                        cmd.Parameters.Add("@Empresa", SqlDbType.VarChar).Value = item.empresa;

                                        cmd.CommandText = "insert into [dbo].[Productos](Id,Codigo,Nombre,Empresa)" +
                                            "values(@Id,@Codigo,@Nombre,@Empresa)";
                                        cmd.ExecuteNonQuery();
                                    
                                    }
                                        

                                    }


                                

                            }
                            else 
                            {
                                Fin = true;
                            }






                        }


                    }
                 
                



            }
            catch (Exception ex)
            {

                cant = cant - 10;
                cmd.Connection = conexion;
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooProductos";
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = ex.Message;

                cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                    "values(@fecha,@descripcion,@estado,@nombreProceso)";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            finally
            {

                conexion.Close();
            }


        }


        static void ObtenerDatosOdooOrdenesManual( int cant,ref Boolean Fin)
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);

            SqlCommand cmd = new SqlCommand();
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                if (conexion.State != ConnectionState.Open) conexion.Open();
            
                string valor = "datos";
                string ejecutable = @"C:\Odoo_exe\ecp_main.exe";
                string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, " -m orders -i 2020-04-01 -o " + cant + " -l 10");                
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p = System.Diagnostics.Process.Start(psi);
                p.WaitForExit();
                string er = p.StandardError.ReadToEnd();
                if (er != "")
                {

                }
                else
                {
                    var resultado = p.StandardOutput.ReadToEnd();                  
                    respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);
                    if (respuestaOdoo.estado == 1)
                    {


                        ordenes clientes = new ordenes();
                        var printObj = (respuestaOdoo.valor);
                        valor = printObj.ToString();
                        if (valor != "[]")
                        {
                            List<ordenes> lstobj = JsonConvert.DeserializeObject<List<ordenes>>(valor);
                            foreach (ordenes item in lstobj)
                            {
                                 cmd.Parameters.Clear();
                                  cmd.Connection = conexion;
                                  cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = item.id;
                                  cmd.CommandText = "select count(Id) from Ordenes where Id=@Id";
                                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                                if (cantidad == 0)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Connection = conexion;
                                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.id;
                                    cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = item.codigo;
                                    if (item.fecha_creacion != "false")
                                    {
                                        cmd.Parameters.Add("@Fecha_Creacion", SqlDbType.DateTime).Value = Convert.ToDateTime(item.fecha_creacion);

                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@Fecha_Creacion", SqlDbType.DateTime).Value =DBNull.Value;
                                    }

                                    cmd.Parameters.Add("@Cod_cliente", SqlDbType.VarChar).Value = item.cod_cliente;
                                    cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = item.total;
                                    cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = item.estado;
                                    cmd.Parameters.Add("@Id_cliente", SqlDbType.Int).Value = item.id_cliente;
                                    if (item.fecha_entrega != "false")
                                    {
                                        cmd.Parameters.Add("@Fecha_Entrega", SqlDbType.DateTime).Value = Convert.ToDateTime(item.fecha_entrega);

                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@Fecha_Entrega", SqlDbType.DateTime).Value = DBNull.Value;
                                    }
                                    if (item.fecha_prevista != "false")
                                    {
                                        cmd.Parameters.Add("@Fecha_Prevista", SqlDbType.DateTime).Value = Convert.ToDateTime(item.fecha_prevista);

                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@Fecha_Prevista", SqlDbType.DateTime).Value = DBNull.Value;
                                    }

                                   cmd.CommandText = "insert into [dbo].[Ordenes](Id,Codigo,Fecha_Creacion,Cod_cliente,Total,Estado,Id_Cliente,Fecha_Entrega,Fecha_Prevista)" +
                                   "values(@Id,@Codigo,@Fecha_Creacion,@Cod_cliente,@Total,@Estado,@Id_cliente,@Fecha_Entrega,@Fecha_Prevista)";
                                    cmd.ExecuteNonQuery();


                                }



                            }
                        }
                        else 
                        {
                            Fin = true;
                        }

                    }


                }

              



            }
            catch (Exception ex)
            {

            cant = cant - 10;
                cmd.Parameters.Clear();
                cmd.Connection = conexion;
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooOrdenesManual";
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = ex.Message;

                cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                    "values(@fecha,@descripcion,@estado,@nombreProceso)";
                cmd.ExecuteNonQuery();
              
            }
            finally
            {

                conexion.Close();
            }


        }


        static void ObtenerDatosOdoodetalleManual(int cant, ref Boolean Fin)
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);

            SqlCommand cmd = new SqlCommand();
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                if (conexion.State != ConnectionState.Open) conexion.Open();
              
                string valor = "datos";
                string ejecutable = @"C:\Odoo_exe\ecp_main.exe";
                string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, " -m orders_detail -i 2020-04-01 -o " + cant + " -l 10");

                psi.UseShellExecute = false;

                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p = System.Diagnostics.Process.Start(psi);
                p.WaitForExit();
                string er = p.StandardError.ReadToEnd();
                if (er != "")
                {

                }
                else
                {
                    var resultado = p.StandardOutput.ReadToEnd();
                 
                    respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);

                    if (respuestaOdoo.estado == 1)
                    {


                        ordenes_detalle obj = new ordenes_detalle();
                        var printObj = (respuestaOdoo.valor);
                        valor = printObj.ToString();
                        if (valor != "[]")
                        {
                            List<ordenes_detalle> lstobj = JsonConvert.DeserializeObject<List<ordenes_detalle>>(valor);
                            foreach (ordenes_detalle item in lstobj)
                            {
                                cmd.Parameters.Clear();
                                cmd.Connection = conexion;
                                cmd.Parameters.Add("@Nro", SqlDbType.VarChar).Value = item.Nro;
                                cmd.Parameters.Add("@Order_id", SqlDbType.VarChar).Value = item.order_id;
                                cmd.CommandText = "select count(Nro) from Ordenes_Detalle where Nro=@Nro and Order_id=@Order_id and Order_id in (select ID FROM Ordenes)";
                                int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                                if (cantidad == 0)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Connection = conexion;
                                    cmd.Parameters.Add("@Nro", SqlDbType.Int).Value = item.Nro;
                                    cmd.Parameters.Add("@Order_id", SqlDbType.VarChar).Value = item.order_id;
                                    cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = item.codigo;
                                    cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = item.cantidad;
                                    cmd.Parameters.Add("@U_m", SqlDbType.VarChar).Value = item.u_m;
                                    cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = item.precio;
                                    cmd.Parameters.Add("@Sub_Total", SqlDbType.Decimal).Value = item.sub_Total;
                                    cmd.CommandText = "insert into [dbo].[Ordenes_Detalle](Nro,Order_id,Codigo,Cantidad,U_m,Precio,Sub_Total)" +
                                        "values(@Nro,@Order_id,@Codigo,@Cantidad,@U_m,@Precio,@Sub_Total)";
                                    cmd.ExecuteNonQuery();


                                }




                            }
                        }
                        else 
                        {
                            Fin = false;
                        }

                    }


                }

          




            }
            catch (Exception ex)
            {

                cant = cant - 10;
                cmd.Connection = conexion;
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooOrdenesManual";
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = ex.Message;

                cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                    "values(@fecha,@descripcion,@estado,@nombreProceso)";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            finally
            {

                conexion.Close();
            }


        }

        static void ObtenerDatosOdoodetalleManual2(int cant, ref Boolean Fin)
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);

            SqlCommand cmd = new SqlCommand();
            RespuestaOdoo respuestaOdoo = new RespuestaOdoo();

            try
            {
                /////////////////////////////
                const string ex1 = "C:\\";
                const string ex2 = @"C:\Odoo_exe\ecp_main.exe";;

                // Use ProcessStartInfo class
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "ecp_main.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = " -m orders_detail -i 2020-10-07 -o " + cant + " -l 5 ";

                try
                {
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                        var resultado = exeProcess.StandardOutput.ReadToEnd();
                        respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);

                        if (respuestaOdoo.estado == 1)
                        {


                            ordenes_detalle obj = new ordenes_detalle();
                            var printObj = (respuestaOdoo.valor);
                         string   valor = printObj.ToString();
                            if (valor != "[]")
                            {
                                List<ordenes_detalle> lstobj = JsonConvert.DeserializeObject<List<ordenes_detalle>>(valor);
                                foreach (ordenes_detalle item in lstobj)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Connection = conexion;
                                    cmd.Parameters.Add("@Nro", SqlDbType.VarChar).Value = item.Nro;
                                    cmd.Parameters.Add("@Order_id", SqlDbType.VarChar).Value = item.order_id;
                                    cmd.CommandText = "select count(Nro) from Ordenes_Detalle where Nro=@Nro and Order_id=@Order_id and Order_id in (select ID FROM Ordenes)";
                                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                                    if (cantidad == 0)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.Connection = conexion;
                                        cmd.Parameters.Add("@Nro", SqlDbType.Int).Value = item.Nro;
                                        cmd.Parameters.Add("@Order_id", SqlDbType.VarChar).Value = item.order_id;
                                        cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = item.codigo;
                                        cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = item.cantidad;
                                        cmd.Parameters.Add("@U_m", SqlDbType.VarChar).Value = item.u_m;
                                        cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = item.precio;
                                        cmd.Parameters.Add("@Sub_Total", SqlDbType.Decimal).Value = item.sub_Total;
                                        cmd.CommandText = "insert into [dbo].[Ordenes_Detalle](Nro,Order_id,Codigo,Cantidad,U_m,Precio,Sub_Total)" +
                                            "values(@Nro,@Order_id,@Codigo,@Cantidad,@U_m,@Precio,@Sub_Total)";
                                        cmd.ExecuteNonQuery();


                                    }




                                }
                            }
                            else
                            {
                                Fin = false;
                            }

                        }

                    }
                }
                catch
                {
                    // Log error.
                }
                ////////////////////////////////
            }
            catch (Exception ex)
            {

                cant = cant - 10;
                cmd.Connection = conexion;
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = "ObtenerDatosOdooOrdenesManual";
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = "ERROR";
                cmd.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = ex.Message;

                cmd.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                    "values(@fecha,@descripcion,@estado,@nombreProceso)";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            finally
            {

                conexion.Close();
            }


        }

        static void ObtenerDatosOdooproductos2()
        {
            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatosEureka"]);
            SqlCommand cmd = new SqlCommand();
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();

                 
                     string ejecutable = @"C:\Odoo_exe\ecp_main.exe";
                     string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                     System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, "-m clients -o 0 -l 20");

                     psi.UseShellExecute = false;
                     psi.RedirectStandardOutput = true;
                     psi.RedirectStandardError = true;
                     psi.LoadUserProfile=true;
                     System.Diagnostics.Process p = new System.Diagnostics.Process();
                     p = System.Diagnostics.Process.Start(psi);
                     p.WaitForExit();
                string er =p.StandardError.ReadToEnd();
                if (er != "")
                {

                }
                else
                {
                     var resultado = p.StandardOutput.ReadToEnd();
                    respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);
                    respuestaOdoo.estado = 1;


                    if (respuestaOdoo.estado == 1)
                    {

                        if (conexion.State != ConnectionState.Open) conexion.Open();
                        /////////////////
                        cmd.Parameters.Clear();
                        cmd.Connection = conexion;
                        cmd.CommandText = "delete from Productos";
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        ////////////////

                        productos obj = new productos();
                        //var printObj = (respuestaOdoo.valor);
                        //string valor = printObj.ToString();
                        List<productos> lstobj = new List<productos>();// JsonConvert.DeserializeObject<List<clientes>>(valor);
                        obj.id = 16;
                        obj.codigo = "300000";
                        obj.nombre = "MAICENA CAJA 200 g";
                        obj.empresa = "Grupo Tec S.R.L.";
                   
                        lstobj.Add(obj);

                        foreach (productos item in lstobj)
                        {
                            cmd.Connection = conexion;
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = item.id;
                            cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = item.codigo;
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = item.nombre;
                            cmd.Parameters.Add("@Empresa", SqlDbType.VarChar).Value = item.empresa;
                           


                            cmd.CommandText = "insert into [dbo].[Productos](Id,Codigo,Nombre,Empresa)" +
                                "values(@Id,@Codigo,@Nombre,@Empresa)";
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                        }





                    }
                    ///////////////////////////////////

                    //////////////////////////////////7

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                conexion.Close();
            }


        }


        static void ProbandoCargaJson()
        {

            var resultado ="";// p.StandardOutput.ReadToEnd();
            RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
            respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);





        }
        static void ObtenerDatosStock()
        {
            try
            {
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                if (client.BaseAddress == null)
                {

                    client.BaseAddress = new Uri("http://190.104.2.122/wWebApi.Web/");

                }
                HttpResponseMessage resp = client.PostAsJsonAsync("api/Stock", "").Result;

                if (resp.IsSuccessStatusCode)
                {
                    var resultado = resp.Content.ReadAsAsync<RespuestaStock>().Result;
                    string valor = resultado.DetalleStock.ToString();
                    List<stock> lststock = JsonConvert.DeserializeObject<List<stock>>(valor);
                    foreach (stock item in lststock)
                    {
                        string ejecutable = @"C:\INSUMOS\DESCARGAS_01042020\PROYECTO_ODOO\ecp_main.exe";
                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, "-m update_stock -p " + Convert.ToInt32(item.Codigo) + "  -a WH -c " + item.Cantidad);
                        psi.UseShellExecute = false;
                        psi.RedirectStandardOutput = true;
                        psi.RedirectStandardError = true;
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p = System.Diagnostics.Process.Start(psi);
                       p.WaitForExit();
                        string er = p.StandardError.ReadToEnd();
                        if (er != "")
                        {

                        }
                        else
                        {
                            var resultadoOdoo = p.StandardOutput.ReadToEnd();
                            if (resultadoOdoo.Contains("Unexpected error"))
                            {
                                InsertarLogNotificacion(DateTime.Now, resultadoOdoo, Convert.ToString(2), "Error de Actualizacion de Stock producto:" + item.Codigo);

                            }
                            else
                            {
                                respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultadoOdoo);
                                if (respuestaOdoo.estado != 1)
                                {
                                    InsertarLogNotificacion(DateTime.Now, respuestaOdoo.mensaje, Convert.ToString(respuestaOdoo.estado), "Error de Actualizacion de Stock producto:" + item.Codigo);
                                }
                            }
                           
                        }
                    }

                    InsertarLogNotificacion(DateTime.Now, respuestaOdoo.mensaje, Convert.ToString(respuestaOdoo.estado), "Actualizacion Correcta de Stock");

                }
                else
                {
                    InsertarLogNotificacion(DateTime.Now, "", Convert.ToString(resp.StatusCode), "TRAER STOCK DE SERVICIO");
                }
            }
            catch ( Exception ex)
            {

                InsertarLogNotificacion(DateTime.Now, ex.Message, "", "ObtenerDatosStock");
            }
        

        }

        static void ActualizarDatosOdoo()
        {
            try
            {
                DateTime fechaOdoo = DateTime.Now;
                string fechaFin = "";
                //  btnExec.Enabled = false;
                //this.UseWaitCursor = true;
                //tring Token = ObtenerToken();
                RespuestaOdoo respuestaOdoo = new RespuestaOdoo();
                string ejecutable = @"C:\INSUMOS\DESCARGAS_01042020\PROYECTO_ODOO\ecp_main.exe"; // aqui puedes poner la ruta a python.exe si no está 
                                                                                                 // en la variable de entorno PATH

                string fecha = Convert.ToString(DateTime.Now.Year) + "-" + Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day);
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(ejecutable, "-m quotations -i " + fecha + "  -f " + fecha);
                // psi.WindowStyle = ProcessWindowStyle.Hidden
                // psi.WorkingDirectory = IO.Path.GetDirectoryName(txtDir.Text.Trim) ' Cambias el directorio activo a tu path
                // psi.Arguments = txtDir.Text.Trim & "\" & txtFile.Text.Trim
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p = System.Diagnostics.Process.Start(psi);
                p.WaitForExit();
                string er = p.StandardError.ReadToEnd();
                if (er != "")
                {

                }
                else
                {
                    var resultado = p.StandardOutput.ReadToEnd();
                    respuestaOdoo = JsonConvert.DeserializeObject<RespuestaOdoo>(resultado);


                    if (respuestaOdoo.estado == 1)
                    {
                        cabeceraPedido cabeceraPedido = new cabeceraPedido();
                        var printObj = (respuestaOdoo.valor);
                        string valor = printObj.ToString();
                        List<CabeceraPedidoOdoo> lstcabeceraPedidoOdoos = JsonConvert.DeserializeObject<List<CabeceraPedidoOdoo>>(valor);
                        foreach (CabeceraPedidoOdoo item in lstcabeceraPedidoOdoos)
                        {
                            /////////////////////////////////
                            fechaOdoo = Convert.ToDateTime(item.fecha);
                            fechaFin = Convert.ToString(fechaOdoo.Day) + "/" + Convert.ToString(fechaOdoo.Month) + "/" + Convert.ToString(fechaOdoo.Year); ;
                            cabeceraPedido.Id = Convert.ToString(item.codigo);
                            cabeceraPedido.Fecha = fechaFin;
                            cabeceraPedido.FechaEntrega = fechaFin;
                            cabeceraPedido.Id_Cliente = "187936"; //Convert.ToString(item.cod_cliente);
                            cabeceraPedido.id_Distribuidora = "1";

                            //////////////////////////////////////////
                            string stringDetalle = item.detalle.ToString();
                            List<DetallePedidoOdoo> lstdetallePedidoOdoo = JsonConvert.DeserializeObject<List<DetallePedidoOdoo>>(stringDetalle);
                            //////ESTRUCTURANDO LA CLASE A INSERTAR

                            List<DetallePedido> lstdetallePedidos = new List<DetallePedido>();
                            //////////////////////////////////////
                            foreach (DetallePedidoOdoo itemdp in lstdetallePedidoOdoo)
                            {
                                DetallePedido detallePedido = new DetallePedido();
                                detallePedido.Nro = Convert.ToInt32(itemdp.nro);
                                detallePedido.Codigo = itemdp.codigo;
                                detallePedido.Cantidad = Convert.ToInt32(itemdp.cantidad);
                                if (itemdp.u_m == "Display10Unidades") itemdp.u_m = "DISPLAY";
                                if (itemdp.u_m == "Display20Unidades") itemdp.u_m = "DISPLAY";
                                if (itemdp.u_m == "Unidades") itemdp.u_m = "UNIDAD";
                                detallePedido.Und_Medida = itemdp.u_m;
                                detallePedido.ICE = 0;
                                lstdetallePedidos.Add(detallePedido);
                            }
                            cabeceraPedido.DetallePedido = lstdetallePedidos;
                            ////////

                            InsertarPedidos(cabeceraPedido);
                        }


                        // clients = clientarray.ToObject<List<valor>>();
                        //  CabeceraPedidoOdoo cabeceraPedido1 = (CabeceraPedidoOdoo)va;
                        //    var clientarray = respuestaOdoo["items"].Value<JArray>();

                        // return View(clients);

                        int i = 0; ;
                    }
                    ///////////////////////////////////

                    //////////////////////////////////7

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private static void InsertarLogNotificacion(DateTime fecha, string descripcion, string estado,string nombreProceso)
        {

            SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["BaseDatos"]);
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                SqlCommand commandlog = new SqlCommand();

                commandlog.Connection = conexion;
                commandlog.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha;
                commandlog.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = descripcion;
                commandlog.Parameters.Add("@estado", SqlDbType.VarChar, 800).Value = estado;
                commandlog.Parameters.Add("@nombreProceso", SqlDbType.VarChar).Value = nombreProceso;

                commandlog.CommandText = "insert into [dbo].[LogRegistros](fecha,descripcion,estado,nombreProceso)" +
                    "values(@fecha,@descripcion,@estado,@nombreProceso)";
                commandlog.ExecuteNonQuery();
                commandlog.Parameters.Clear();
              
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
        static async Task<string> RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://190.104.2.122/");
            client.DefaultRequestHeaders.Accept.Clear();
       
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            string Token = "";
            try
            {
                // Create a new product
                Parametros parametros = new Parametros
                {
                    UserName = "admin",
                    Password = "123456",
                    Code = "IV001"
                };
                Token = await CreateProductAsync(parametros);
          
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Token;
        }
        static async void InsertarPedido(string token)
        {
            RespuestaPedido respuestaPedido = null;
            try
            {
             
                client1.BaseAddress = new Uri("http://190.104.2.122/");
                client1.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);

                client1.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                cabeceraPedido cabeceraPedido = new cabeceraPedido();
                List<DetallePedido> lstdetallePedido = new List<DetallePedido>();
                DetallePedido detallePedido = new DetallePedido();
                detallePedido.Nro = 1;
                detallePedido.Codigo = "10021";
                detallePedido.Cantidad = 2;
                detallePedido.Und_Medida = "";
             
                detallePedido.ICE = 0;
              
                lstdetallePedido.Add(detallePedido);
                cabeceraPedido.Id = "S00019";
                cabeceraPedido.Fecha = "2020-05-04";
                cabeceraPedido.FechaEntrega = "";
             
                cabeceraPedido.id_Distribuidora = "";
              
                cabeceraPedido.DetallePedido = lstdetallePedido;
              string  Token = await CreatePedidotAsync(cabeceraPedido, token);
            }
            catch ( Exception ex)
            {

                throw  ex ;
            }

        }
        static async Task<string> CreatePedidotAsync(cabeceraPedido cabeceraPedido,string token)
        {
            Respuesta respuesta = null;
            try
            {
             //   client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client1.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client1.PostAsJsonAsync(

                    "wWebApi.Web/api/Pedido", cabeceraPedido);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;

                    respuesta = JsonConvert.DeserializeObject<Respuesta>(resultString);


                }
                // return URI of the created resource.
                // return response.Headers.Location;
            }
            catch (Exception EX)
            {

                throw EX;
            }
            return respuesta.Data;
        }

       
    }
  
      
    
}
