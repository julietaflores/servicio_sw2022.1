using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebService_Web
{
    public partial class MetodoBusqueda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("https://serviceweb.bo/SearchServices/");



            var request = clienteHttp.GetAsync("api/SearchServicesV1?nombre=" + TextBox1.Text).Result;
            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                //   var listado = JsonConvert.DeserializeObject<List<BE.Persona>>(resultString);
               
                List<BE.SearchServices> searchServices = null;
                searchServices = JsonConvert.DeserializeObject<List<BE.SearchServices>>(resultString);
             //   searchServices = JsonConvert.DeserializeObject<List<BE.SearchServices>>(Convert.ToString(respuesta.valor));
              
              
             
                GridView1.DataSource = searchServices;
                GridView1.DataBind();

                
            }
        }
    }
}