
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
    public class IdiomaSManager
    {
        private BC.IdiomaS bcIdiomaS = null;
    
        public IdiomaSManager(string cadConx)
        {
            bcIdiomaS = new BC.IdiomaS(cadConx);
           
        }

        public Respuesta ListadoIdiomaS(string lang)
        {
          
            Respuesta resp = new Respuesta();
            List<BE.IdiomaS> lstIdiomaS = null;
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lstIdiomaS = bcIdiomaS.ObtenerIdioma(lang);
                resp.valor = lstIdiomaS;
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