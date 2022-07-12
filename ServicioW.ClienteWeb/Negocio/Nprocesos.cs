using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ServicioW.ClienteWeb.Negocio
{
    public class Nprocesos
    {
        public DataTable ValidarUsuario()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            HttpClient clienteHttp = new HttpClient();
            clienteHttp.BaseAddress = new Uri("http://localhost/ServicioWebApi/");

            var request = clienteHttp.GetAsync("api/Libro").Result;
            if (request.IsSuccessStatusCode)
            {
                var resultString = request.Content.ReadAsStringAsync().Result;
                string ValoresJson = request.Content.ReadAsStringAsync().Result;
                string resultString1 = request.Content.ReadAsStringAsync().Result;
                var listado = JsonConvert.DeserializeObject<List<Prueba>>(resultString);
                ////////////////////////
                // DataSet data = JsonConvert.DeserializeObject<DataSet>(resultString);

                //     dt = ToDataTable<Prueba>();
                //      ds = jsonToDataSet(resultString1);
             //   dt = JsontoDataTable(resultString1);
            }
            return dt;
        }

    }
}