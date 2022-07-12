using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE;
using BC;
using ServiciosWeb.WepApi.Models.Entities;


namespace ServiciosWeb.WepApi.Models
{
    public class RequiereServicioManager
    {

        private BC.RequiereServicio bcRequiereServicio = null;

        public RequiereServicioManager(string cadConx)
        {
            bcRequiereServicio = new BC.RequiereServicio(cadConx);
        }

        public Respuesta VerRequierServicio(long personaId, string lang)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.RequiereServicio> lstPost = new List<BE.RequiereServicio>();

                lstPost = bcRequiereServicio.ListadoRequiereServicio(personaId, lang, BE.relRequiereServicio.servicio, BE.relServicio .categoriaServicio ,BE.relRequiereServicio.estadoReqServ,BE.relRequiereServicio.requiereServicioProveedores,BE.relRequiereServicio.personaDireccion,BE.relRequiereServicio.persona);

                resp.valor = lstPost;
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