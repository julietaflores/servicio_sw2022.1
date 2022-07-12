using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ServiciosWeb.Datos.Modelo;

namespace ServiciosWeb.WepApi.Models
{
    public class Conversor
    {
        public static List<ServicioPersona> toServicioPersona(DataRow[] dr)
        {
            List<ServicioPersona> lst = new List<ServicioPersona>();
            foreach (var item in dr)
            {
                lst.Add(toServicioPersona(item));
            }
            return lst;
        }

        public static ServicioPersona toServicioPersona(DataRow dr)
        {
            ServicioPersona obj = new ServicioPersona();
            if (!DBNull.Value.Equals(dr["ServicioPersonaId"]))
            {
                obj.ServicioPersonaId = Convert.ToDecimal(dr["ServicioPersonaId"].ToString());

            }
            if (!DBNull.Value.Equals(dr["PersonaId"]))
            {
                obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            }
            if (!DBNull.Value.Equals(dr["ServicioId"]))
            {
                obj.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());

            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaURLFoto"]))
            {
                obj.ServicioPersonaURLFoto = dr["ServicioPersonaURLFoto"].ToString();

            }
            if (!DBNull.Value.Equals(dr["EstadoServicioId"]))
            {
                obj.EstadoServicioId = Convert.ToDecimal(dr["EstadoServicioId"].ToString());

            }
            if (!DBNull.Value.Equals(dr["StatusServicioId"]))
            {
                obj.StatusServicioId = Convert.ToDecimal(dr["StatusServicioId"].ToString());

            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaUsuario"]))
            {
                obj.ServicioPersonaUsuario = dr["ServicioPersonaUsuario"].ToString();

            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaFechaHora"]))
            {
                obj.ServicioPersonaFechaHora = Convert.ToDateTime(dr["ServicioPersonaFechaHora"].ToString());

            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaGaleriaLast"]))
            {
                obj.ServicioPersonaGaleriaLast = Convert.ToDecimal(dr["ServicioPersonaGaleriaLast"].ToString());
            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaHorarioLast"]))
            {
                obj.ServicioPersonaHorarioLast = Convert.ToByte(dr["ServicioPersonaHorarioLast"].ToString());
            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaNombre"]))
            {
                obj.ServicioPersonaNombre = dr["ServicioPersonaNombre"].ToString();

            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaDescripcion"]))
            {
                obj.ServicioPersonaDescripcion = dr["ServicioPersonaDescripcion"].ToString();

            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaReqDelivery"]))
            {
                obj.ServicioPersonaReqDelivery = Convert.ToBoolean(dr["ServicioPersonaReqDelivery"].ToString());
            }
            if (!DBNull.Value.Equals(dr["MonedaId"]))
            {

                obj.MonedaId = Convert.ToDecimal(dr["MonedaId"].ToString());
            }

            if (!DBNull.Value.Equals(dr["PersonaDireccionId"]))
            {
                obj.PersonaDireccionId = Convert.ToDecimal(dr["PersonaDireccionId"].ToString());
                try
                {
                    obj.personaDireccion = toPersonaDireccion(dr);
                }
                catch (Exception)
                {

                }                
            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaEnDomicilio"]))
            {
                obj.ServicioPersonaEnDomicilio = Convert.ToBoolean(dr["ServicioPersonaEnDomicilio"].ToString());
            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaEnOficina"]))
            {
                obj.ServicioPersonaEnOficina = Convert.ToBoolean(dr["ServicioPersonaEnOficina"].ToString());
            }
            try
            {
                obj.rankingProveedor = new RankingProveedor();
                obj.rankingProveedor.NroTrabajos = Convert.ToInt16(dr["NroTrabajos"].ToString());
                obj.rankingProveedor.Ranking = Convert.ToDecimal(dr["Ranking"].ToString());
               // obj.rankingProveedor.Distancia = Convert.ToDecimal(dr["Distancia"].ToString());
            }
            catch (Exception)
            {
            }
            return obj;
        }

        public static List<PersonaDireccion> toPersonaDireccion(DataRow[] dr)
        {
            List<PersonaDireccion> lst = new List<PersonaDireccion>();
            foreach (var item in dr)
            {
                lst.Add(toPersonaDireccion(item));
            }
            return lst;
        }


        public static List<CategoriaServicio> toCategoriaServicio(DataRow[] dr)
        {
            List<CategoriaServicio> lst = new List<CategoriaServicio>();
            foreach (var item in dr)
            {
                lst.Add(toCategoriaServicio(item));
            }
            return lst;
        }

        public static CategoriaServicio toCategoriaServicio(DataRow dr)
        {
            CategoriaServicio obj = new CategoriaServicio();

            ////////////////////////////
            obj.CategoriaServicioId = Convert.ToDecimal(dr["CategoriaServicioId"].ToString());
            obj.CategoriaServicioNombre = Convert.ToString(dr["CategoriaServicioNombre"].ToString());
            obj.CategoriaServicioURLFoto = Convert.ToString(dr["CategoriaServicioURLFoto"].ToString());
            obj.CiudadId = Convert.ToDecimal(dr["CiudadId"].ToString());
            obj.CategoriaServicioDescripcion = Convert.ToString(dr["CategoriaServicioDescripcion"].ToString());
            obj.CategoriaServicioUsuario = Convert.ToString(dr["CategoriaServicioUsuario"].ToString());
         if (dr["CategoriaServicioFechaHoraMod"].ToString() != "")
            {
                obj.CategoriaServicioFechaHoraMod = Convert.ToDateTime(dr["CategoriaServicioFechaHoraMod"].ToString());

            }

            if (dr["CategoriaServicioHijoId"].ToString() != "")
            {
                obj.CategoriaServicioHijoId = Convert.ToDecimal(dr["CategoriaServicioHijoId"].ToString());
            }
         

            if (dr["CategoriaServicioDestLast"].ToString() != "")
            {
                obj.CategoriaServicioDestLast = Convert.ToDecimal(dr["CategoriaServicioDestLast"].ToString());

            }


        
            return obj;
        }


        public static List<CategoriaServicioDestacada> toCategoriaServicioDestacada(DataRow[] dr)
        {
            List<CategoriaServicioDestacada> lst = new List<CategoriaServicioDestacada>();
            foreach (var item in dr)
            {
                lst.Add(toCategoriaServicioDestacada(item));
            }
            return lst;
        }

        public static CategoriaServicioDestacada toCategoriaServicioDestacada(DataRow dr)
        {
            CategoriaServicioDestacada obj = new CategoriaServicioDestacada();

            ////////////////////////////
            obj.CategoriaServicioId = Convert.ToDecimal(dr["CategoriaServicioId"].ToString());
            obj.CategoriaServicioDestacadaId = Convert.ToDecimal(dr["CategoriaServicioDestacadaId"].ToString());
            obj.CategoriaServicioDestacadaURL = Convert.ToString(dr["CategoriaServicioDestacadaURL"].ToString());
            obj.CategoriaServicioDestacadaFini = Convert.ToDateTime(dr["CategoriaServicioDestacadaFini"].ToString());
            obj.CategoriaServicioDestacadaFFin = Convert.ToDateTime(dr["CategoriaServicioDestacadaFFin"].ToString());
        


            return obj;
        }

        public static PersonaDireccion toPersonaDireccion(DataRow dr)
        {
            PersonaDireccion obj = new PersonaDireccion();
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.PersonaDireccionId = Convert.ToDecimal(dr["PersonaDireccionId"].ToString());
            obj.TipoDireccionId = Convert.ToDecimal(dr["TipoDireccionId"].ToString());
            obj.PersonaDireccionTitulo = dr["PersonaDireccionTitulo"].ToString();
            obj.PersonaDireccionGeo = dr["PersonaDireccionGeo"].ToString();
            obj.PersonaDireccionDescripcion = dr["PersonaDireccionDescripcion"].ToString();
            obj.CiudadDireccionId = Convert.ToDecimal(dr["CiudadDireccionId"].ToString());
            obj.PersonaDireccionFHMod = Convert.ToDateTime(dr["PersonaDireccionFHMod"].ToString());
            obj.PersonaDireccionUsuarioMod = dr["PersonaDireccionUsuarioMod"].ToString();
            obj.EstadoDireccionId = Convert.ToDecimal(dr["EstadoDireccionId"].ToString());
            obj.PersonaDireccionDireccion = dr["PersonaDireccionDireccion"].ToString();
            return obj;
        }

   
        public static List<RequiereServicio> toRequiereServicio(DataRow[] dr)
        {
            List<RequiereServicio> lst = new List<RequiereServicio>();
            foreach (var item in dr)
            {
                lst.Add(toRequiereServicio(item));
            }
            return lst;
        }

        public static   ServiciosWeb.WepApi.Models.RequiereServicio toRequiereServicio(DataRow dr)
        {

            ServiciosWeb.WepApi.Models.RequiereServicio requiereServicio = new  ServiciosWeb.WepApi.Models.RequiereServicio ();
            requiereServicio.RequiereServicioId = dr["RequiereServicioId"].ToString();
            requiereServicio.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            requiereServicio.RequiereServicioFechaHoraReq = Convert.ToDateTime(dr["RequiereServicioFechaHoraReq"].ToString());
            requiereServicio.EstadoReqServId = Convert.ToDecimal(dr["EstadoReqServId"].ToString());
            requiereServicio.RequiereServicioFHDeseada = Convert.ToDateTime(dr["RequiereServicioFHDeseada"].ToString());
            requiereServicio.RequiereServicioFHCaduca = Convert.ToDateTime(dr["RequiereServicioFHCaduca"].ToString());
            requiereServicio.RequiereServicioDescripcion = dr["RequiereServicioDescripcion"].ToString();
            requiereServicio.RequiereServicioURLFoto1 = dr["RequiereServicioURLFoto1"].ToString();
            requiereServicio.RequiereServicioURLFoto2 = dr["RequiereServicioURLFoto2"].ToString();
            requiereServicio.RequiereServicioURLFoto3 = dr["RequiereServicioURLFoto3"].ToString();
            requiereServicio.RequiereServicioURLVideo = dr["RequiereServicioURLVideo"].ToString();
            requiereServicio.RequiereServicioProvLast = Convert.ToDecimal(dr["RequiereServicioProvLast"].ToString());
    
            if (dr["PersonaDireccionId"].ToString() != "")
            {
                requiereServicio.PersonaDireccionId = Convert.ToDecimal(dr["PersonaDireccionId"].ToString());           }         
            requiereServicio.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            if (dr["RequiereServicioFechaMod"].ToString() != "")
            {
                requiereServicio.RequiereServicioFechaMod = Convert.ToDateTime(dr["RequiereServicioFechaMod"].ToString());
            }
            requiereServicio.servicio = null;
            return requiereServicio;
        }


        ///////////////////////////////////////////////////////////////
        public static List<ServiciosWeb.Datos.Modelo.RequiereServicioProveedores> torRequiereServicioProveedores(DataRow[] dr)
        {
            List<ServiciosWeb.Datos.Modelo.RequiereServicioProveedores> lst = new List<ServiciosWeb.Datos.Modelo.RequiereServicioProveedores>();
            foreach (var item in dr)
            {
                lst.Add(torRequiereServicioProveedores(item));
            }
            return lst;
        }

        public static ServiciosWeb.Datos.Modelo.RequiereServicioProveedores torRequiereServicioProveedores(DataRow dr)
        {
            ServiciosWeb.Datos.Modelo.RequiereServicioProveedores obj = new ServiciosWeb.Datos.Modelo.RequiereServicioProveedores();
            ///////////////////////////////
            obj.RequiereServicioId = dr["RequiereServicioId"].ToString();
            obj.RequiereServicioProveedoresId = Convert.ToDecimal(dr["RequiereServicioProveedoresId"].ToString());
            obj.RequiereServicioProveedoresAdj = Convert.ToBoolean(dr["RequiereServicioProveedoresAdj"].ToString());
            obj.RequiereServicioProvCotizacion = Convert.ToDecimal(dr["RequiereServicioProvCotizacion"].ToString());
            obj.RequiereServicioProvFHTrabajo = Convert.ToDateTime(dr["RequiereServicioProvFHTrabajo"].ToString());
            obj.RequiereServicioProvDescipcion = dr["RequiereServicioProvDescipcion"].ToString();
            obj.ServicioPersonaId = Convert.ToDecimal(dr["ServicioPersonaId"].ToString());
            obj.RequiereServicioProvFHResp = Convert.ToDateTime(dr["RequiereServicioProvFHResp"].ToString());
            obj.StatusRequiereId = Convert.ToDecimal(dr["StatusRequiereId"].ToString());
            ////////////////////////////////////////         
           
            //////////////////////////////////////
            return obj;
        }

        public static List<RequiereServicioProveedoresM> toRequiereServicioProveedores(DataRow[] dr)
        {
            List<RequiereServicioProveedoresM> lst = new List<RequiereServicioProveedoresM>();
            foreach (var item in dr)
            {
                lst.Add(toRequiereServicioProveedores(item));
            }
            return lst;
        }

        public static RequiereServicioProveedoresM toRequiereServicioProveedores(DataRow dr)
        {
            RequiereServicioProveedoresM obj = new RequiereServicioProveedoresM();
       
            ///////////////////////////////
            obj.RequiereServicioId = dr["RequiereServicioId"].ToString();
            obj.RequiereServicioProveedoresId = Convert.ToDecimal(dr["RequiereServicioProveedoresId"].ToString());
            obj.RequiereServicioProveedoresAdj = Convert.ToBoolean(dr["RequiereServicioProveedoresAdj"].ToString());
            obj.RequiereServicioProvCotizacion = Convert.ToDecimal(dr["RequiereServicioProvCotizacion"].ToString());
            obj.RequiereServicioProvFHTrabajo = Convert.ToDateTime(dr["RequiereServicioProvFHTrabajo"].ToString());
            obj.RequiereServicioProvDescipcion = dr["RequiereServicioProvDescipcion"].ToString();
            obj.ServicioPersonaId = Convert.ToDecimal(dr["ServicioPersonaId"].ToString());
            obj.RequiereServicioProvFHResp = Convert.ToDateTime(dr["RequiereServicioProvFHResp"].ToString());
            obj.StatusRequiereId = Convert.ToDecimal(dr["StatusRequiereId"].ToString());
            //////////////////////////// 
            
          
            try
            {
                obj.statusRequiere = Conversor.toStatusRequiere(dr);
            }
            catch (Exception)
            {
            }
            try
            {
                obj.servicioPersona = Conversor.toServicioPersona(dr);
            }
            catch (Exception)
            {
            }
            return obj;
        }

        public static List<Persona> toPersona (DataRow[] dr)
        {
            List<Persona> lst = new List<Persona>();
            foreach (var item in dr)
            {
                lst.Add(toPersona(item));
            }
            return lst;
        }
        public static Persona toPersona(DataRow dr)
        {
            Persona obj = new Persona();
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.PersonaTokenId = dr["PersonaTokenId"].ToString();
            obj.PersonaNombres = dr["PersonaNombres"].ToString();
            obj.PersonaApellidos = dr["PersonaApellidos"].ToString();
            obj.PersonaCorreo = dr["PersonaCorreo"].ToString();
            obj.PersonaFechaNacimiento = Convert.ToDateTime(dr["PersonaFechaNacimiento"].ToString());
            obj.PersonaTelefono = dr["PersonaTelefono"].ToString();
            obj.PersonaUID = dr["PersonaUID"].ToString();
            obj.PersonaURLFoto = dr["PersonaURLFoto"].ToString();
            obj.PersonaUsuario = dr["PersonaUsuario"].ToString();
            obj.PersonaFechaHoraMod = Convert.ToDateTime(dr["PersonaFechaHoraMod"].ToString());
            obj.TipoPersonaId = Convert.ToDecimal(dr["TipoPersonaId"].ToString());
            obj.GeneroId = Convert.ToDecimal(dr["GeneroId"].ToString());
            obj.TipoLoginId = Convert.ToDecimal(dr["TipoLoginId"].ToString());
            obj.CiudadId = Convert.ToDecimal(dr["CiudadId"].ToString());
            obj.PersonaFechaRegistro = Convert.ToDateTime(dr["PersonaFechaRegistro"].ToString());
            obj.EstadoPersonaId = Convert.ToDecimal(dr["EstadoPersonaId"].ToString());
            obj.PersonaDireccionLast = Convert.ToDecimal(dr["PersonaDireccionLast"].ToString());
            obj.PersonaDNI = dr["PersonaDNI"].ToString();
            obj.TipoDocumentoId = Convert.ToDecimal(dr["TipoDocumentoId"].ToString());
            obj.PersonaGeoReal = dr["PersonaGeoReal"].ToString();
            obj.PersonaClave = dr["PersonaClave"].ToString();
            obj.PersonaUsuarioMod = dr["PersonaUsuarioMod"].ToString();
            obj.PersonaCodigoTelefono = dr["PersonaCodigoTelefono"].ToString();
            obj.PersonaGeoLocalizacionLast = Convert.ToDecimal(dr["PersonaGeoLocalizacionLast"].ToString());

            return obj;
        }

        public static List<StatusRequiere> toStatusRequiere(DataRow[] dr)
        {
            List<StatusRequiere> lst = new List<StatusRequiere>();
            foreach (var item in dr)
            {
                lst.Add(toStatusRequiere(item));
            }
            return lst;
        }

        public static StatusRequiere toStatusRequiere(DataRow dr)
        {
            StatusRequiere obj = new StatusRequiere();
            ///////////////////////////////
            obj.StatusRequiereId = Convert.ToDecimal(dr["StatusRequiereId"].ToString());
            obj.StatusRequiereNombre  = dr["StatusRequiereNombre"].ToString();            
     
            return obj;
        }
        ////////////////////////////////////////////////////////////////
        ///
        public static List<ServiciosWeb.Datos.Modelo.RequiereServicioProveedores> toRequiereServicioProveedoresDM(List<RequiereServicioProveedoresM> dr)
        {
            List<ServiciosWeb.Datos.Modelo.RequiereServicioProveedores> lst = new List<ServiciosWeb.Datos.Modelo.RequiereServicioProveedores>();
            foreach (var item in dr)
            {
                lst.Add(toRequiereServicioProveedoresDM(item));
            }
            return lst;
        }

        public static ServiciosWeb.Datos.Modelo.RequiereServicioProveedores toRequiereServicioProveedoresDM(RequiereServicioProveedoresM dr)
        {
            ServiciosWeb.Datos.Modelo.RequiereServicioProveedores obj = new ServiciosWeb.Datos.Modelo.RequiereServicioProveedores();
            ///////////////////////////////
            obj.RequiereServicioId = dr.RequiereServicioId;
            obj.RequiereServicioProveedoresId = dr.RequiereServicioProveedoresId;
            obj.RequiereServicioProveedoresAdj = dr.RequiereServicioProveedoresAdj;
            obj.RequiereServicioProvCotizacion = dr.RequiereServicioProvCotizacion;
            obj.RequiereServicioProvFHTrabajo = dr.RequiereServicioProvFHTrabajo;
            obj.RequiereServicioProvDescipcion = dr.RequiereServicioProvDescipcion;
            obj.ServicioPersonaId = dr.ServicioPersonaId;
            obj.RequiereServicioProvFHResp = dr.RequiereServicioProvFHResp;
            obj.StatusRequiereId = dr.StatusRequiereId;
            ////////////////////////////         
            return obj;
        }

      
        public static ServAsig toServAsig(DataRow dr)
        {
            ServAsig obj = new ServAsig();
            obj.ServAsigId = Convert.ToString(dr["ServAsigId"].ToString());
            obj.ProveedorId = Convert.ToDecimal(dr["ProveedorId"].ToString());
            obj.ServAsigFHUbicacion = Convert.ToDateTime(dr["ServAsigFHUbicacion"].ToString());
            obj.ServAsigFHEstimadaLlegada = Convert.ToDateTime(dr["ServAsigFHEstimadaLlegada"].ToString());       
                
         
            if (dr["ServAsigFHInicio"].ToString() != "")
            {
                obj.ServAsigFHInicio = Convert.ToDateTime(dr["ServAsigFHInicio"].ToString());
            }
            if (dr["ServAsigFHFin"].ToString() != "")
            {
                obj.ServAsigFHFin = Convert.ToDateTime(dr["ServAsigFHFin"].ToString());
            }
            if (dr["ServAsigFHPago"].ToString() != "")
            {
                obj.ServAsigFHPago = Convert.ToDateTime(dr["ServAsigFHPago"].ToString());
            }
            if (dr["ServAsigCostoTotal"].ToString() != "")
            {
                obj.ServAsigCostoTotal = Convert.ToDecimal(dr["ServAsigCostoTotal"].ToString());
            }
            if (dr["StatusServAsigId"].ToString() != "")
            {
                obj.StatusServAsigId = Convert.ToDecimal(dr["StatusServAsigId"].ToString());
            }
            if (dr["RequiereServicioId"].ToString() != "")
            {
                obj.RequiereServicioId = Convert.ToString(dr["RequiereServicioId"].ToString());
            }
            if (dr["ServAsigPagaCliente"].ToString() != "")
            {
                obj.ServAsigPagaCliente = Convert.ToBoolean(dr["ServAsigPagaCliente"].ToString());
            }


            return obj;
        }
        public static List<ServAsig> toServAsig(DataRow[] dr)
        {
            List<ServAsig> lst = new List<ServAsig>();
            foreach (var item in dr)
            {
                lst.Add(toServAsig(item));
            }
            return lst;
        }


        public static List<Servicio> toServicio(DataRow[] dr)
        {
            List<Servicio> lst = new List<Servicio>();
            foreach (var item in dr)
            {
                lst.Add(toServicio(item));
            }
            return lst;
        }

        public static ServiciosWeb.WepApi.Models.Servicio toServicio(DataRow dr)
        {
            Servicio obj = new Servicio();
            obj.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            obj.ServicioNombre = Convert.ToString(dr["ServicioNombre"].ToString());
            obj.ServicioURLFoto = Convert.ToString(dr["ServicioURLFoto"].ToString());
            obj.CategoriaServicioId = Convert.ToDecimal(dr["CategoriaServicioId"].ToString());
            obj.ServicioUsuario = Convert.ToString(dr["ServicioUsuario"].ToString());
            if ((dr["ServicioFechaHoraMod"].ToString()) != "")
             {
                obj.ServicioFechaHoraMod = Convert.ToDateTime(dr["ServicioFechaHoraMod"].ToString());
            }
            
            obj.ServicioKeyWords = Convert.ToString(dr["ServicioKeyWords"].ToString());
            return obj;          
        }

        public static ServiciosWeb.WepApi.Models.ServAsigCosto toServAsigCosto(DataRow dr)
        {
            ServAsigCosto obj = new ServAsigCosto();
            obj.ServAsigCostoId = Convert.ToString(dr["ServAsigCostoId"].ToString());
            obj.ServAsigId = Convert.ToString(dr["ServAsigId"].ToString());
            obj.ConceptoCostoId = Convert.ToDecimal(dr["ConceptoCostoId"].ToString());
            obj.ServAsigCostoValor = Convert.ToDecimal(dr["ServAsigCostoValor"].ToString());

            ConceptoCosto cc = new ConceptoCosto();
            cc.ConceptoCostoId = Convert.ToDecimal(dr["ConceptoCostoId"].ToString());
            cc.ConceptoCostoNombre= Convert.ToString(dr["ConceptoCostoNombre"].ToString());

            obj.conceptoCosto = cc;
            return obj;
        }

        public static List<ServAsigCosto> toServAsigCosto(DataRow[] dr)
        {
            List<ServAsigCosto> lst = new List<ServAsigCosto>();
            foreach (var item in dr)
            {
                lst.Add(toServAsigCosto(item));
            }
            return lst;
        }

        public static List<ConceptoCosto> toConceptoCosto(DataRow[] dr)
        {
            List<ConceptoCosto> lst = new List<ConceptoCosto>();
            foreach (var item in dr)
            {
                lst.Add(toConceptoCosto(item));
            }
            return lst;
        }

        public static ConceptoCosto toConceptoCosto(DataRow dr)
        {
            ConceptoCosto obj = new ConceptoCosto();
            ///////////////////////////////
            obj.ConceptoCostoId = Convert.ToDecimal(dr["ConceptoCostoId"].ToString());
            obj.ConceptoCostoNombre = dr["ConceptoCostoNombre"].ToString();

            return obj;
        }

        public static List<StatusServAsig> toStatusServAsig(DataRow[] dr)
        {
            List<StatusServAsig> lst = new List<StatusServAsig>();
            foreach (var item in dr)
            {
                lst.Add(toStatusServAsig(item));
            }
            return lst;
        }

        public static StatusServAsig toStatusServAsig(DataRow dr)
        {
            StatusServAsig obj = new StatusServAsig();
            ///////////////////////////////
            obj.StatusServAsigId = Convert.ToDecimal(dr["StatusServAsigId"].ToString());
            obj.StatusServAsigNombre = dr["StatusServAsigNombre"].ToString();

            return obj;
        }

        public static List<ServicioPersonaGaleria> toServicioPersonaGaleria(DataRow[] dr)
        {
            List<ServicioPersonaGaleria> lst = new List<ServicioPersonaGaleria>();
            foreach (var item in dr)
            {
                lst.Add(toServicioPersonaGaleria(item));
            }
            return lst;
        }

        public static ServicioPersonaGaleria toServicioPersonaGaleria(DataRow dr)
        {
            ServicioPersonaGaleria obj = new ServicioPersonaGaleria();
            ///////////////////////////////
            obj.ServicioPersonaId = Convert.ToDecimal(dr["ServicioPersonaId"].ToString());
            obj.ServicioPersonaGaleriaId  = Convert.ToDecimal(dr["ServicioPersonaGaleriaId"].ToString());
            obj.ServicioPersonaGaleriaURLFoto = dr["ServicioPersonaGaleriaURLFoto"].ToString();

            return obj;
        }

        public static List<ServicioPersonaHorario> toServicioPersonaHorario(DataRow[] dr)
        {
            List<ServicioPersonaHorario> lst = new List<ServicioPersonaHorario>();
            foreach (var item in dr)
            {
                lst.Add(toServicioPersonaHorario(item));
            }
            return lst;
        }

        public static ServicioPersonaHorario toServicioPersonaHorario(DataRow dr)
        {
            ServicioPersonaHorario obj = new ServicioPersonaHorario();
            ///////////////////////////////
            obj.ServicioPersonaId = Convert.ToDecimal(dr["ServicioPersonaId"].ToString());
            obj.ServicioPersonaHorarioId = Convert.ToDecimal(dr["ServicioPersonaHorarioId"].ToString());
            obj.DiaSemanaId = Convert.ToDecimal(dr["DiaSemanaId"].ToString());
            obj.ServicioPersonaHorarioHoraIni1 = Convert.ToDateTime (dr["ServicioPersonaHorarioHoraIni1"].ToString());
            obj.ServicioPersonaHorarioHoraFin1  = Convert.ToDateTime(dr["ServicioPersonaHorarioHoraFin1"].ToString());
            obj.ServicioPersonaHorarioHoraIni2 = Convert.ToDateTime(dr["ServicioPersonaHorarioHoraIni2"].ToString());
            obj.ServicioPersonaHorarioHoraFin2 = Convert.ToDateTime(dr["ServicioPersonaHorarioHoraFin2"].ToString());

            try
            {
                obj.diaSemana = toDiaSemana(dr);
            }
            catch (Exception)
            {
            }
            return obj;
        }

        public static List<DiaSemana> toDiaSemana(DataRow[] dr)
        {
            List<DiaSemana> lst = new List<DiaSemana>();
            foreach (var item in dr)
            {
                lst.Add(toDiaSemana(item));
            }
            return lst;
        }

        public static DiaSemana toDiaSemana(DataRow dr)
        {
            DiaSemana obj = new DiaSemana();
            ///////////////////////////////
            obj.DiaSemanaId = Convert.ToDecimal(dr["DiaSemanaId"].ToString());            
            obj.DiaSemanaNombre = dr["DiaSemanaNombre"].ToString();

            return obj;
        }

        public static List<Comentario> toComentario(DataRow[] dr)
        {
            List<Comentario> lst = new List<Comentario>();
            foreach (var item in dr)
            {
                lst.Add(toComentario(item));
            }
            return lst;
        }

        public static Comentario toComentario(DataRow dr)
        {
            Comentario obj = new Comentario();
            ///////////////////////////////
            obj.PersonaPostId = Convert.ToDecimal(dr["PersonaPostId"].ToString());
            obj.PostId = Convert.ToDecimal(dr["PostId"].ToString());
            obj.ComentarioPersonaNombre = dr["ComentarioPersonaNombre"].ToString();
            obj.ComentarioPersonaFotoUrl = dr["ComentarioPersonaFotoUrl"].ToString();
            obj.ComentarioPersonaDescripcion = dr["ComentarioPersonaDescripcion"].ToString();
            obj.ComentarioFecha = Convert.ToDateTime(dr["ComentarioFecha"].ToString());

            return obj;
        }

      

        public static List<BilleteraConcepto> toBilleteraConcepto(DataRow[] dr)
        {
            List<BilleteraConcepto> lst = new List<BilleteraConcepto>();
            foreach (var item in dr)
            {
                lst.Add(toBilleteraConcepto(item));
            }
            return lst;
        }

        public static BilleteraConcepto toBilleteraConcepto(DataRow dr)
        {
            BilleteraConcepto obj = new BilleteraConcepto();
            ///////////////////////////////
            obj.BilleteraConceptoId  = Convert.ToDecimal(dr["BilleteraConceptoId"].ToString());
            obj.BilleteraConceptoNombre = dr["BilleteraConceptoNombre"].ToString();

            return obj;
        }

        public static List<Moneda> toMoneda(DataRow[] dr)
        {
            List<Moneda> lst = new List<Moneda>();
            foreach (var item in dr)
            {
                lst.Add(toMoneda(item));
            }
            return lst;
        }

        public static Moneda toMoneda(DataRow dr)
        {
            Moneda obj = new Moneda();
            ///////////////////////////////
            obj.MonedaId = Convert.ToDecimal(dr["MonedaId"].ToString());
            obj.MonedaNombre  = dr["MonedaNombre"].ToString();
            obj.PaisId = Convert.ToDecimal(dr["PaisId"].ToString());
            return obj;
        }

        public static List<Billetera> toBilletera(DataRow[] dr)
        {
            List<Billetera> lst = new List<Billetera>();
            foreach (var item in dr)
            {
                lst.Add(toBilletera(item));
            }
            return lst;
        }

        public static Billetera toBilletera(DataRow dr)
        {
            Billetera obj = new Billetera();
            ///////////////////////////////
            obj.BilleteraId = Convert.ToDecimal(dr["BilleteraId"].ToString());
            obj.MonedaId = Convert.ToDecimal(dr["MonedaId"].ToString());
            obj.BilleteraNroCuenta = dr["BilleteraNroCuenta"].ToString();
            obj.BilleteraSaldo = Convert.ToDecimal(dr["BilleteraSaldo"].ToString());
            obj.PersonaBilleteraId = Convert.ToDecimal(dr["PersonaBilleteraId"].ToString());
            obj.BilleteraFechaCreacion = Convert.ToDateTime(dr["BilleteraFechaCreacion"].ToString());

            obj.moneda = toMoneda(dr);
            return obj;
        }

        public static List<BilleteraDetalle> toBilleteraDetalle(DataRow[] dr)
        {
            List<BilleteraDetalle> lst = new List<BilleteraDetalle>();
            foreach (var item in dr)
            {
                lst.Add(toBilleteraDetalle(item));
            }
            return lst;
        }

        public static BilleteraDetalle toBilleteraDetalle(DataRow dr)
        {
            BilleteraDetalle obj = new BilleteraDetalle();
            ///////////////////////////////
            obj.BilleteraDetalleId = Convert.ToInt32(dr["BilleteraDetalleId"].ToString());
            obj.BilleteraId = Convert.ToDecimal(dr["BilleteraId"].ToString());
            if ( dr["CajeroId"] == DBNull.Value )
            {
                obj.CajeroId = null;                
            }
            else
            {
                obj.CajeroId = Convert.ToInt32(dr["CajeroId"].ToString());
            }            
            obj.MedioPagoId = Convert.ToDecimal(dr["MedioPagoId"].ToString());
            obj.BilleteraConceptoId  = Convert.ToDecimal(dr["BilleteraConceptoId"].ToString());
            if (dr["ServAsigId"] == DBNull.Value)
            {
                obj.ServAsigId = null;
            }
            else
            {
                obj.ServAsigId = dr["ServAsigId"].ToString();
                obj.servicio = toServicio(dr);                
            }                
            obj.BilleteraDebeHaber = dr["BilleteraDebeHaber"].ToString();
            obj.BilleteraValor = Convert.ToDecimal(dr["BilleteraValor"].ToString());         
            obj.BilleteraFechaTransaccion = Convert.ToDateTime(dr["BilleteraFechaTransaccion"].ToString());
            obj.BilleteraNroTransaccion = dr["BilleteraNroTransaccion"].ToString();
            obj.BilleteraObservacion = dr["BilleteraObservacion"].ToString();
            obj.billeteraConcepto = toBilleteraConcepto(dr);

            return obj;
        }
    }
}