
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
    public class TipoDireccionManager
    {
        private BC.TipoDireccion bcTipoDireccion = null;
    
        public TipoDireccionManager(string cadConx)
        {
            bcTipoDireccion = new BC.TipoDireccion(cadConx);
           
        }

        public Respuesta ListadoTipoDireccion(string lang)
        {
          
            Respuesta resp = new Respuesta();
            List<BE.TipoDireccion> lstTipoDireccion = null;
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lstTipoDireccion = bcTipoDireccion.ObtenerTipoDireccion(lang);
                resp.valor = lstTipoDireccion;
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