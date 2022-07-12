using BC;
using BE;
using ServiciosWeb.WepApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace ServiciosWeb.WepApi.Models
{
    public class PackParameterManager
    {

        private BC.Pais bcpais = null;
        private BC.EstadoPersona bcestadoPersona = null;
        private BC.Genero bcGenero = null;
        private BC.Region bcRegion = null;
        private BC.Ciudad bcCiudad = null;
        private BC.TipoDireccion bcTipoDireccion = null;
        private BC.TipoDocumento bcTipoDocumento = null;
        private BC.TipoPersona bcTipoPersona = null;
        private BC.TipoPost bctipoPost = null;
        public PackParameterManager(string cadConx)
        {
            bcpais = new BC.Pais(cadConx);
            bcestadoPersona = new BC.EstadoPersona(cadConx);
            bcGenero = new BC.Genero(cadConx);
            bcRegion = new BC.Region(cadConx);
            bcCiudad = new BC.Ciudad(cadConx);
            bcTipoDireccion = new BC.TipoDireccion(cadConx);
            bcTipoDocumento = new BC.TipoDocumento(cadConx);
            bcTipoPersona = new BC.TipoPersona(cadConx);
            bctipoPost = new BC.TipoPost(cadConx);
        }

        public Respuesta GetPackParameter(string lang)
        {


            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                BE.PackParameter packParameter = new BE.PackParameter();

                packParameter.paises = bcpais.ObtenerPais(lang);
                packParameter.estadoPersonas = bcestadoPersona.ObtenerEstadoPersona(lang);
                packParameter.generos = bcGenero.ObtenerGenero(lang);
                packParameter.regiones = bcRegion.ObtenerRegion(lang);
                packParameter.ciudades = bcCiudad.ObtenerCiudad(lang);
                packParameter.tipoDirecciones = bcTipoDireccion.ObtenerTipoDireccion(lang);
                packParameter.tipoDocumentos = bcTipoDocumento.ObtenerTipoDocumento(lang);
                packParameter.tipoPersonas = bcTipoPersona.ObtenerTipoPersona(lang);

                packParameter.tipoPosts = bctipoPost.ObtenerTipoPost(lang);
                resp.valor = packParameter;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta GetCities(string lang)
        {


            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";

                List<BE.Ciudad> ciudad = new List<BE.Ciudad>();

                ciudad = bcCiudad.ObtenerCiudad(lang);
                resp.valor = ciudad;
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