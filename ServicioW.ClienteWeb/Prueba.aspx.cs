using Newtonsoft.Json;
using ServiciosWeb.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;



namespace ServicioW.ClienteWeb
{
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
               
            ServicioW.ClienteWeb.Negocio.Nprueba Nprueba = new ServicioW.ClienteWeb.Negocio.Nprueba();

           dt= Nprueba.cargarGrid();
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        public void CargarTabla()
        {

           
        }
    }
}