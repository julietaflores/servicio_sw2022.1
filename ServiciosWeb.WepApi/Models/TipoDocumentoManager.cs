
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
    public class TipoDocumentoManager
    {
        private BC.TipoDocumento bcTipoDocumento = null;
    
        public TipoDocumentoManager(string cadConx)
        {
            bcTipoDocumento = new BC.TipoDocumento(cadConx);
           
        }

        public Respuesta ListadoTipoDocumento(string lang)
        {
          
            Respuesta resp = new Respuesta();
            List<BE.TipoDocumento> lstTipoDocumento = null;
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lstTipoDocumento = bcTipoDocumento.ObtenerTipoDocumento(lang);
                resp.valor = lstTipoDocumento;
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