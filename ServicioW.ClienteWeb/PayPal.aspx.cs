using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicioW.ClienteWeb
{
    public partial class PayPal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Params["Id"]!=null)
            {
                TextBoxTotal.Text = Request.Params["Id"];
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
    }
}