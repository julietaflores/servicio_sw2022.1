using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;

namespace ServicioW.ClienteWeb
{
    public partial class Post1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string increasediv(string id)
        {
            string shashi;
            string nombre ="yohana";
            string res = "";
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=PRUEBAS;User ID=sa;Password=.");
            con.Open();

            SqlCommand cmd = new SqlCommand("select Youtube from mov where ID=" + id, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                sb.Append("<iframe src='" + dr[0].ToString() + "'alt='' height='300px' width='400px'></iframe><br/>");
                sb.Append("<iframe width ='400' height = '500' scrolling ='auto' frameborder ='1'>< p >"+ nombre+"</p></ iframe >");
               res = "<a href='http://190.104.5.77/ServicioWeb/api/ImageDownload?dirfile=MediaPost&name=1.png' rel='lightbox[galeria1]' title='imagen 1'><img width='600' height='600' src='http://190.104.5.77/ServicioWeb/api/ImageDownload?dirfile=MediaPost&name=1.png'/></a>"; 
                 
            }
            shashi = sb.ToString();
            return res;
        }
    }
}