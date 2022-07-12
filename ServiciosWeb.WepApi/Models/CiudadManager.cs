
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
    public class CiudadManager
    {
        private BC.Ciudad bcCiudad = null;
    
        public CiudadManager(string cadConx)
        {
            bcCiudad = new BC.Ciudad(cadConx);
           
        }

        public Respuesta ListadoCiudad(string lang)
        {
          
            Respuesta resp = new Respuesta();
            List<BE.Ciudad> lstCiudad = null;
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                lstCiudad = bcCiudad.ObtenerListaCiudad(lang);
                resp.valor = lstCiudad;
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