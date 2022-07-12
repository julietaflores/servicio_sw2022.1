using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Xml;

namespace ServicioW.ClienteWeb.Negocio
{
    public class Nprueba
    {
        public DataTable cargarGrid()
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
                dt = JsontoDataTable(resultString1);
            }
            return dt;
        }

        public DataTable GetDataTableFromJsonString(string json)
        {
            var jsonLinq = JObject.Parse(json);

            // Find the first array using Linq
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                }
                trgArray.Add(cleanRow);
            }

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }

        public  DataSet jsonToDataSet(string jsonString)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                jsonString = "{ \"rootNode\": {" + jsonString.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xd = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonString);
                DataSet ds = new DataSet();
                ds.ReadXml(new XmlNodeReader(xd));
                return ds;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public DataTable JsontoDataTable(string Json)
        {
            DataTable dt =(DataTable)JsonConvert.DeserializeObject(Json, typeof(DataTable));
            return dt;
        }


    }
}