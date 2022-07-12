
using BE;
using ServiciosWeb.WepApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Mail;



namespace ServiciosWeb.WepApi.Models
{
    public class PersonaManager
    {
        private BC.Persona bcpersona = null;
        private BC.PersonaDireccion bcpersonaDireccion = null;
        private BC.envioCorreo bcenvioCorreo = null;
        private BC.Promocion bcPromocion = null;
        public PersonaManager(string cadConx)
        {
            bcpersona = new BC.Persona(cadConx);
            bcpersonaDireccion = new BC.PersonaDireccion(cadConx);
            bcenvioCorreo = new BC.envioCorreo(cadConx);
            bcPromocion = new BC.Promocion(cadConx);
        }

        public Respuesta savePersona(ref BE.Persona persona)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                if (persona.TipoEstado == TipoEstado.Insertar)
                {

                    //req = bcReqSer.CargarRelaciones()
                    if (bcpersona.ValidarExistePersona(persona.PersonaCorreo, persona.TipoLoginId, persona.PersonaCodigoTelefono, persona.PersonaTelefono, persona.PersonaTokenId) >= 1)
                    {
                        bolOk = bcpersona.RegistrarSolicitud(ref persona);

                        if (!bolOk)
                        {
                            throw new Exception("Error al registrar Solicitud.");
                        }
                    }
                    else
                    {

                        persona = bcpersona.VerPersona1(persona.PersonaCorreo, persona.TipoLoginId, null);

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
                    bolOk = bcpersona.RegistrarSolicitud_nuevo(ref persona);
                    if (!bolOk)
                    {
                        throw new Exception("Error al registrar Solicitud.");
                    }
                    else
                    {
                        BE.PersonaDireccion personaDireccion = persona.PersonaDireccion;
                        personaDireccion.TipoEstado = BE.TipoEstado.Insertar;
                        personaDireccion.PersonaId = persona.PersonaId;
                        personaDireccion.PersonaDireccionId = 1;
                        bolOk = bcpersonaDireccion.Actualizar(ref personaDireccion);
                        bcpersonaDireccion.ActualizarPersonaDireccionLasT(personaDireccion.PersonaId);

                        if (!bolOk)
                        {
                            throw new Exception("Error al registrar Solicitud.");
                        }
                    }
                }
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





        public int ValidarExistePersonaCorreo(string PersonaCorreo, string PersonaCodigoTelefono, string PersonaTelefono)
        {  
            int cantidad = 0;
            cantidad = bcpersona.ValidarExistePersonaPorCorreo(PersonaCorreo, PersonaCodigoTelefono, PersonaTelefono);
            return cantidad;
        }


        public Respuesta actualizarPersona(ref BE.Persona persona)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                if (persona.TipoEstado == TipoEstado.Modificar)
                {
                    if (bcpersona.ValidarCorreo(persona.PersonaCorreo, null) == null)
                    {

                        bolOk = bcpersona.Actualizar(ref persona);
                    }
                    else
                    {

                        BE.Persona persona1 = bcpersona.ValidarCorreo(persona.PersonaCorreo, null);
                        if (persona.PersonaCorreo == persona1.PersonaCorreo && persona1.PersonaId == persona.PersonaId)
                        {
                            persona.PersonaCorreo = persona1.PersonaCorreo;
                            bolOk = bcpersona.Actualizar(ref persona);

                        }
                        else
                        {
                            resp.estado = 2;
                            resp.valor = null;
                            resp.mensaje = "El correo ingresado existe, favor comunicarse con 62077611";
                        }


                    }

                }


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





























































        //////////////////

        //////////////////
        public Respuesta BuscarPersonaxId(decimal PersonaId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Persona personas = new BE.Persona();

                personas = bcpersona.BuscarPersonaxId(PersonaId, null);

                resp.valor = personas;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        /////////////////
        /////////////////
        public Respuesta ValidarExistePersona(string PersonaCorreo, decimal TipoLoginId, string PersonaCodigoTelefono, string PersonaTelefono, string PersonaUID, string lang)
        {
            Boolean bolok = false;
            Respuesta resp = new Respuesta();
            Respuesta resp1 = new Respuesta();
            try
            {
                int cantidad=0;
                
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.Persona> lstpersona = new List<BE.Persona>();
                lstpersona = null;
                BE.Persona persona = new BE.Persona();
             
                cantidad = bcpersona.ValidarExistePersona(PersonaCorreo, TipoLoginId, PersonaCodigoTelefono, PersonaTelefono, PersonaUID);

                if (cantidad == 0)
                {
                    string tipo = "";
                    switch (TipoLoginId)
                    {
                        case 1:
                            tipo = "NoExistePersonaCorreo";
                            break;
                        case 2:
                            tipo = "NoExistePersonaCorreo";
                            break;
                        case 3:
                            tipo = "NoExistePersonaNumero";
                            break;
                        case 4:
                            tipo = "NoExistePersonaNumero";
                            break;
                    }
                          
                    DataRow data = bcpersona.ListadoDatosNotificacion(tipo, lang);
                    var title = data["title"].ToString();
                    var body = data["body"].ToString();
                    resp.valor = null;
                    resp.mensaje = Convert.ToString(body);
                    resp.estado = 1;
                }
                else
                {
                  
                    persona=bcpersona.BuscarPersonaxId(cantidad,lang,relPersona.tipoLogin);
                    resp1 = registrar_promocionp(persona.PersonaId);
                    DataRow data = bcpersona.ListadoDatosNotificacion("ExistePersona", lang);
                    var title = data["title"].ToString();
                    var body = data["body"].ToString();
                    string TipoLogin = "";
                    string TipoLogin1 = "";
                   
                        TipoLogin1 = persona.tipoLogin.TipoLoginNombreTipo + ";" + " ";
                        TipoLogin = TipoLogin + TipoLogin1;

                    
                  
                    resp.valor = persona;
                    resp.estado = 1;
                    resp.mensaje = Convert.ToString(title) + TipoLogin + Convert.ToString(body);
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

        public Respuesta ExistePersona(decimal PersonaId)
        {
            Boolean bolok = false;
            Respuesta resp = new Respuesta();
            try
            {
                int cantidad = 0;

                resp.estado = 1;
                resp.mensaje = "";
               BE.Persona persona = new BE.Persona();

                persona = bcpersona.ExistePersona(PersonaId);

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

        //////////////////
        public Respuesta VerPersona()
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.Persona> personas = new List<BE.Persona>();

                personas = bcpersona.VerPersona(null);

                resp.valor = personas;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta VerPersonaPersonaTokenId(string PersonaTokenId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.Persona> personas = new List<BE.Persona>();

                personas = bcpersona.VerPersonaPersonaTokenId(PersonaTokenId);

                resp.valor = personas;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }


        public Respuesta Publicidad_Service_Web() {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                bolOk = bcpersona.Registrar_publicidad();
                    if (!bolOk)
                    {
                        throw new Exception("Error al registrar Solicitud.");
                    }
                resp.valor = "Publicidad exitosa";
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }

        public Respuesta ListadoUsuariosTotales()
        {
          
            Respuesta resp = new Respuesta();
            List<BE.Persona> lstPersonas = null;
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lstPersonas = bcpersona.Listado_total_usuarios1();
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



    
        public Respuesta registrar_promocionp(decimal personaid)
        {
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;

                int canr = bcPromocion.Listado_Usuarios_Con_Promocion(personaid);
                resp.valor = null;
                resp.mensaje = canr.ToString();
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;

        }

     

  
        public Respuesta ActualizarGeolocalizacion(decimal IdPersona, string PersonaGeoReal)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";


                //req = bcReqSer.CargarRelaciones()
              
                bcpersona.ActualizarGeolocalizacion(IdPersona, PersonaGeoReal);

             
                //  bcReqSer.CargarRelaciones(ref requiereServicio, lang,relRequiereServicio.servicio);
                resp.valor = null;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        /*     public string EnviarCorreo1(string Personacorreo,string lang)
             {



                 try
                 {

                     DataRow data = bcpersona.ListadoDatosNotificacion("EnviarCorreo", lang);
                     var title = data["title"].ToString();
                     var body = data["body"].ToString();
                     MailMessage mail = new MailMessage();
                     mail = new MailMessage();
                     mail.To.Add(Personacorreo);
                     mail.From = new MailAddress("webmaster@serviceweb.bo","Service Web");
                     mail.Subject = title;
                     string Body;
                    // string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                  //   string pin = GenerateRandom();
                     string cuerpo =body + pin;
                    // Body = text.Replace("MENSAJE", cuerpo);
                     mail.Body = cuerpo;
                     mail.IsBodyHtml = true;
                     var smtp = new SmtpClient
                     {
                         Host = "smtp.office365.com",
                         Port = 587,
                         EnableSsl = true,
                         UseDefaultCredentials = false,
                         Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                         DeliveryMethod = SmtpDeliveryMethod.Network,
                     };


                     smtp.Send(mail);
                     return pin;

                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.Message);
                     //Console.Read();
                     throw ex;
                 }
                 finally
                 {

                 }



             }*/

        /*     public string GenerarCodigo()
             {



                 try
                 {

                    // DataRow data = bcpersona.ListadoDatosNotificacion("EnviarCorreo", lang);
                 /*    var title = data["title"].ToString();
                     var body = data["body"].ToString();
                     MailMessage mail = new MailMessage();
                     mail = new MailMessage();
                   //  mail.To.Add(Personacorreo);
                     mail.From = new MailAddress("webmaster@serviceweb.bo", "Service Web");
                     mail.Subject = title;
                     string Body;
                     // string text = System.IO.File.ReadAllText(@System.Configuration.ConfigurationSettings.AppSettings["CorreoFormatoSW"].ToString());
                    // string pin = GenerateRandom();
                     string cuerpo = body + pin;
                     // Body = text.Replace("MENSAJE", cuerpo);
                     mail.Body = cuerpo;
                     mail.IsBodyHtml = true;
                     var smtp = new SmtpClient
                     {
                         Host = "smtp.office365.com",
                         Port = 587,
                         EnableSsl = true,
                         UseDefaultCredentials = false,
                         Credentials = new System.Net.NetworkCredential("webmaster@serviceweb.bo", "KInsino2000!*"),
                         DeliveryMethod = SmtpDeliveryMethod.Network,
                     };


                     smtp.Send(mail);
                     return "default";

                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.Message);
                     //Console.Read();
                     throw ex;
                 }
                 finally
                 {

                 }



             }*/



    



        public DataRow ListadoDatosNotificacion(string lang)
        {

            DataRow data = bcpersona.ListadoDatosNotificacion("EnviarCorreo", lang);
            // var title = data["title"].ToString();
            // var body = data["body"].ToString();
            return data;
        }
        public string GenerarPIN()
        {
            System.Random randomGenerate = new System.Random();
            System.String sPassword = "";
            sPassword = System.Convert.ToString(randomGenerate.Next(00000001, 99999999));
            return sPassword.Substring(sPassword.Length - 4, 4);

        }
        public Respuesta ConfirmarCorreoPersona(decimal PersonaId,string pin)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Persona persona = new BE.Persona();

                persona = bcpersona.ConfirmarCorreoPersona(PersonaId,pin);
                if (persona != null)
                {
                    persona.PersonaCorreoValidado = true;
                    persona.TipoEstado = BE.TipoEstado.Modificar ;
                    bolOk = bcpersona.Actualizar(ref persona);

                    resp.valor = persona;

                }
              
                if (persona == null)
                {
                    resp.estado = 2;
                    resp.valor = null;
                    resp.mensaje = "Codigo Incorrecto";

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

      

        public Respuesta ValidarExistePersona_Web(string usuario, string password)
        {
            Boolean bolok = false;
            Respuesta resp = new Respuesta();
            int cantidad = 0;
            try
            {


                resp.estado = 1;
                resp.mensaje = "";
                BE.Persona persona = new BE.Persona();
                 persona = null;
             

                persona = bcpersona.ValidarExistePersona_Web(usuario, password);
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



     



        public Respuesta BuscarPorUID(string uid, params Enum[] relaciones)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
               BE.Persona  persona =new  BE.Persona();
                persona = bcpersona.BuscarPorUID(uid,null);
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
        
       public Respuesta ValidarLoginWeb(string PersonaUsuario, string PersonaClave)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Persona persona = new BE.Persona();
                persona = bcpersona.ValidarLoginWeb(PersonaUsuario, PersonaClave,relPersona.serviciopersona);
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


        public Respuesta ValidarCorreo(string Correo)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                BE.Persona persona = new BE.Persona();
                persona = bcpersona.ValidarCorreo(Correo,null);
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

    }
}