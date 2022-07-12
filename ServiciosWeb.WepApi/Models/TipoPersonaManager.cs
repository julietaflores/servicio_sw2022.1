
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
    public class TipoPersonaManager
    {
        private BC.TipoPersona bctipopersona = null;
    
        public TipoPersonaManager(string cadConx)
        {
            bctipopersona = new BC.TipoPersona(cadConx);
           
        }

        public Respuesta Listadotipopersona(string lang)
        {
          
            Respuesta resp = new Respuesta();
            List<BE.TipoPersona> lsttipoPersona = null;
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lsttipoPersona = bctipopersona.ObtenerTipoPersona(lang);
                resp.valor = lsttipoPersona;
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