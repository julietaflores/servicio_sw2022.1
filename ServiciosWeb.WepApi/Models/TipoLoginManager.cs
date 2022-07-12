
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
    public class TipoLoginManager
    {
        private BC.TipoLogin bcTipoLogin = null;
    
        public TipoLoginManager(string cadConx)
        {
            bcTipoLogin = new BC.TipoLogin(cadConx);
           
        }

        public Respuesta ListadoTipoLogin(string lang)
        {
          
            Respuesta resp = new Respuesta();
            List<BE.TipoLogin> lstTipoLogin = null;
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lstTipoLogin = bcTipoLogin.ObtenerTipoLogin(lang);
                resp.valor = lstTipoLogin;
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