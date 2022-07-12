using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Web.Services;


namespace ServicioW.ClienteWeb
{
    public partial class Post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        [WebMethod]
        public static string increasediv(string id)
        {
            string shashi;
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=PRUEBAS;User ID=sa;Password=.");
            con.Open();

            SqlCommand cmd = new SqlCommand("select Youtube from mov where ID<" + id, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                sb.Append("<iframe src='" + dr[0].ToString() + "'alt='' height='300px' width='400px'></iframe><br/>");
            }
            shashi = sb.ToString();
            return shashi;
        }
    }
}