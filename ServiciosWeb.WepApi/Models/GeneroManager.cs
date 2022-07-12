
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
    public class GeneroManager
    {
        private BC.Genero bcGenero = null;
    
        public GeneroManager(string cadConx)
        {
            bcGenero = new BC.Genero(cadConx);
           
        }

        public Respuesta ListadoGenero(string lang)
        {
          
            Respuesta resp = new Respuesta();
            List<BE.Genero> lstGenero = null;
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lstGenero = bcGenero.ObtenerGenero(lang);
                resp.valor = lstGenero;
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