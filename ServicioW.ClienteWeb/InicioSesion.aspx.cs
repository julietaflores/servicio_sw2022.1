using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicioW.ClienteWeb
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            ServicioW.ClienteWeb.Negocio.Nprueba Nprueba = new ServicioW.ClienteWeb.Negocio.Nprueba();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}