using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using BE;
using System.Net.Http.Formatting;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebService_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
               HttpClient clienteHttp = new HttpClient();
               clienteHttp.BaseAddress = new Uri("http://190.104.2.126/ServicioWebPrueba/");

          

            var request = clienteHttp.GetAsync("api/ValidarExistePersona_Web?usuario=" + correo.Text+ "&password="+password.Text).Result;
               if (request.IsSuccessStatusCode)
               {

                   var resultString = request.Content.ReadAsStringAsync().Result;
                //   var listado = JsonConvert.DeserializeObject<List<BE.Persona>>(resultString);
                BE.RespuestaWeb respuesta=null;
                respuesta = JsonConvert.DeserializeObject<BE.RespuestaWeb>(resultString);

                if (respuesta.valor == null)
                {
                  
                    //TextBoxContNueva.Visible = true;
                    //Add the textbox to the DialogWindow's ContentPane Control's collection
                    //  this.WebDialogWindow1.ContentPane.Controls.Add(TextBoxContNueva);
                  
                }
                else
                {
                    BE.Persona persona = null;
                    persona = JsonConvert.DeserializeObject<BE.Persona>(Convert.ToString(respuesta.valor));

                    if (persona.PersonaContrasenaActualizada == true)
                    {
                        Response.Redirect("Inicio.aspx");

                    }
                    else
                    {
                        this.wdwForms.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
                        wdwForms.Visible = true;
                        LabelPersonaId.Text = Convert.ToString(persona.PersonaId);
                    }


                }
  
                }

        }

        protected void btnCerrarventana_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ///////////////////////////////////////////
            if (TextBoxNuevaContrasena.Text == TextBoxRepetecionContrasena.Text)
            {
                //OBTENIENDO LOS DATOS DEL CLIENTE
                HttpClient clienteHttp = new HttpClient();
                clienteHttp.BaseAddress = new Uri("http://190.104.2.126/ServicioWebPrueba/");
                var request = clienteHttp.GetAsync("api/ObtenerPersona?PersonaId=" + LabelPersonaId.Text).Result;
                var resultString = request.Content.ReadAsStringAsync().Result;
                //   var listado = JsonConvert.DeserializeObject<List<BE.Persona>>(resultString);
                //////////////////////////////////////////////////////////////////
                BE.RespuestaWeb respuesta = null;
                respuesta = JsonConvert.DeserializeObject<BE.RespuestaWeb>(resultString);
                BE.Persona persona = new BE.Persona();
                var valor = respuesta.valor;
                persona = JsonConvert.DeserializeObject<BE.Persona>(Convert.ToString(valor));


                persona.PersonaClave = Convert.ToString(TextBoxNuevaContrasena.Text);

                //////////////////////////////////////////////
                ////Actualizando la contraseña

                using (clienteHttp = new HttpClient())
                {

                    string jsonString = JsonConvert.SerializeObject(persona);
                    var requestUrl = new Uri("http://190.104.2.126/ServicioWebPrueba/api/PutPersonaV2");
                    using (HttpContent httpContent = new StringContent(jsonString))
                    {
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        HttpResponseMessage response = clienteHttp.PutAsync(requestUrl, httpContent).Result;
                    }
                }
                ///////////////////////////////////////////



            }





        }

     
    }
    // var response =client.PutAsync(url1, stringContent);
    ////////////////////////////////////////

    
}