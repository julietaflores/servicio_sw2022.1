using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using BE;
using System.Data.SqlTypes;

namespace BC
{
    public class RequiereServicio:BCEntidad
    {
        public RequiereServicio() : base()
        {
        }
        public RequiereServicio(string cadConx) : base(cadConx)
        {

        }

        private string campos(string prefijo = "rs")
        {
            string strCampos = String.Format(@"{0}.RequiereServicioId, {0}.PersonaId, {0}.RequiereServicioFechaHoraReq, {0}.RequiereServicioFHCaduca, {0}.EstadoReqServId, {0}.RequiereServicioFHDeseada, {0}.RequiereServicioDescripcion, {0}.RequiereServicioURLFoto1, {0}.RequiereServicioURLFoto2, {0}.RequiereServicioURLFoto3, {0}.RequiereServicioURLVideo, {0}.RequiereServicioProvLast, {0}.PersonaDireccionId, {0}.ServicioId, {0}.RequiereServicioFechaMod, {0}.RequiereServicioUID,{0}.RequiereServicioInmediato,{0}.RequiereServicioGeoInmediato,{0}.RequiereServicioOtros"
                    , prefijo);
            return strCampos;
        }

        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.RequiereServicio  BEObj, Boolean isTransaccion = false)
        {
            string strSql = string.Empty;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            bool bolOk = false;

            try
            {
                string TipoEstadoa = BEObj.TipoEstado.ToString();
                switch (BEObj.TipoEstado)
                {
                    case BE.TipoEstado.Modificar:
                        strSql = @"UPDATE dbo.RequiereServicio
                                    SET
                                        PersonaId = @personaid,
                                        RequiereServicioFechaHoraReq = @requiereserviciofechahorareq,
                                        RequiereServicioFHCaduca = @requiereserviciofhcaduca,
                                        EstadoReqServId = @estadoreqservid,
                                        RequiereServicioFHDeseada = @requiereserviciofhdeseada,
                                        RequiereServicioDescripcion = @requiereserviciodescripcion,
                                        RequiereServicioURLFoto1 = @requiereserviciourlfoto1,
                                        RequiereServicioURLFoto2 = @requiereserviciourlfoto2,
                                        RequiereServicioURLFoto3 = @requiereserviciourlfoto3,
                                        RequiereServicioURLVideo = @requiereserviciourlvideo,
                                        RequiereServicioProvLast = @requiereservicioprovlast,
                                        PersonaDireccionId = @personadireccionid,
                                        ServicioId = @servicioid,
                                        RequiereServicioFechaMod = @requiereserviciofechamod,
                                        RequiereServicioUID = @requiereserviciouid
                                        
                                    where  RequiereServicioId = @requiereservicioid";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO dbo.RequiereServicio (RequiereServicioId, PersonaId, RequiereServicioFechaHoraReq, RequiereServicioFHCaduca, EstadoReqServId, RequiereServicioFHDeseada, RequiereServicioDescripcion, RequiereServicioURLFoto1, RequiereServicioURLFoto2, RequiereServicioURLFoto3, RequiereServicioURLVideo, RequiereServicioProvLast, PersonaDireccionId, ServicioId, RequiereServicioFechaMod, RequiereServicioUID,RequiereServicioInmediato,RequiereServicioGeoInmediato,RequiereServicioOtros)
                                VALUES (@requiereservicioid, @personaid, @requiereserviciofechahorareq, @requiereserviciofhcaduca, @estadoreqservid, @requiereserviciofhdeseada, @requiereserviciodescripcion, @requiereserviciourlfoto1, @requiereserviciourlfoto2, @requiereserviciourlfoto3, @requiereserviciourlvideo, @requiereservicioprovlast, @personadireccionid, @servicioid, @requiereserviciofechamod, @requiereserviciouid,@RequiereServicioInmediato,@RequiereServicioGeoInmediato,@RequiereServicioOtros);";
                        break;

                }

                if (isTransaccion)
                    conx = dbConexion;
                else
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                }
                if (BEObj.TipoEstado != BE.TipoEstado.SinAccion)
                {

                    if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    {
                        BEObj.RequiereServicioId = this.GenIdAN("RequiereServicio", conx);
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@requiereservicioid", BEObj.RequiereServicioId );
                    conx.AsignarParametro("@personaid", BEObj.PersonaId);
                    conx.AsignarParametro("@requiereserviciofechahorareq", BEObj.RequiereServicioFechaHoraReq);
                    conx.AsignarParametro("@requiereserviciofhcaduca", BEObj.RequiereServicioFHCaduca);
                    conx.AsignarParametro("@estadoreqservid", BEObj.EstadoReqServId);
                    conx.AsignarParametro("@requiereserviciofhdeseada", BEObj.RequiereServicioFHDeseada);
                    conx.AsignarParametro("@requiereserviciodescripcion", BEObj.RequiereServicioDescripcion);
                    conx.AsignarParametro("@requiereserviciourlfoto1", BEObj.RequiereServicioURLFoto1);
                    conx.AsignarParametro("@requiereserviciourlfoto2", BEObj.RequiereServicioURLFoto2);
                    conx.AsignarParametro("@requiereserviciourlfoto3", BEObj.RequiereServicioURLFoto3);                   
                    conx.AsignarParametro("@requiereserviciourlvideo", BEObj.RequiereServicioURLVideo);
                    conx.AsignarParametro("@requiereservicioprovlast", BEObj.RequiereServicioProvLast);
                    if ((BEObj.PersonaDireccionId == null) || (BEObj.PersonaDireccionId == 0))
                    {

                        conx.AsignarParametro("@personadireccionid", DBNull.Value);
                    }
                    else
                    {
                        conx.AsignarParametro("@personadireccionid", BEObj.PersonaDireccionId);
                    }
                    
                    conx.AsignarParametro("@servicioid", BEObj.ServicioId );
                    conx.AsignarParametro("@requiereserviciofechamod", BEObj.RequiereServicioFechaMod);
                    conx.AsignarParametro("@requiereserviciouid", BEObj.RequiereServicioUID);
                    conx.AsignarParametro("@RequiereServicioInmediato", BEObj.RequiereServicioInmediato);
                    conx.AsignarParametro("@RequiereServicioGeoInmediato", BEObj.RequiereServicioGeoInmediato);
                    conx.AsignarParametro("@RequiereServicioOtros", BEObj.requiereServicioOtros);
                }
                conx.EjecutarComando();

                if (!isTransaccion)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }
                    
                bolOk = true;
            }
            catch (Exception ex)
            {
                if (!isTransaccion)
                {
                    conx.CancelarTransaccion ();
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }
        public List<BE.RequiereServicio> CargarBE(DataRow[] dr)
        {
            List<BE.RequiereServicio> lst = new List<BE.RequiereServicio>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.RequiereServicio CargarBE(DataRow dr)
        {
            BE.RequiereServicio requiereServicio = new BE.RequiereServicio();
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
                requiereServicio.PersonaDireccionId = Convert.ToDecimal(dr["PersonaDireccionId"].ToString());
            }
            requiereServicio.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            if (dr["RequiereServicioFechaMod"].ToString() != "")
            {
                requiereServicio.RequiereServicioFechaMod = Convert.ToDateTime(dr["RequiereServicioFechaMod"].ToString());
            }
            if (dr["RequiereServicioUID"].ToString() != "")
            {
                requiereServicio.RequiereServicioUID = dr["RequiereServicioUID"].ToString();
            }
            if (dr["RequiereServicioInmediato"].ToString() != "")
            requiereServicio.RequiereServicioGeoInmediato = (dr["RequiereServicioGeoInmediato"].ToString());
            if (dr["RequiereServicioOtros"].ToString() != "")
                requiereServicio.requiereServicioOtros =Convert.ToBoolean (dr["RequiereServicioOtros"].ToString());



            requiereServicio.servicio = null;
            return requiereServicio;

        }


        //public void CargarRelaciones(ref List<BE.RequiereServicio> colObj,params Enum[] relaciones)
        //{
        //    if (relaciones == null || colObj == null)
        //    {
        //        return;
        //    }
        //    IEnumerable<decimal> llaves;
        //    IEnumerable<string> sllaves;
        //    List<BE.RequiereServicioProveedores> colRequiereServicioProveedores = null;
        //    foreach (Enum clase in relaciones)
        //    {
        //        if (clase.Equals(BE.relRequiereServicio.requiereServicioProveedores))
        //        {
        //            BC.RequiereServicioProveedores bcRequiereServicioProveedores = new BC.RequiereServicioProveedores(cadenaConexion);
        //            sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
        //            colRequiereServicioProveedores = bcRequiereServicioProveedores.ObtenerHijos(sllaves,null, relaciones);
        //            bcRequiereServicioProveedores = null;
        //        }

        //        if (clase.Equals(BE.relRequiereServicio.reqServProvAdj))
        //        {
        //            BC.RequiereServicioProveedores bcRequiereServicioProveedores = new BC.RequiereServicioProveedores(cadenaConexion);
        //            sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
        //            llaves = new decimal[] { 4};
        //            colRequiereServicioProveedores = bcRequiereServicioProveedores.ObtenerHijos(sllaves, llaves, relaciones);
        //            bcRequiereServicioProveedores = null;
        //        }

        //    }

        //    if (relaciones.GetLength(0) > 0)
        //    {
        //        foreach (var item in colObj)
        //        {

        //            if (colRequiereServicioProveedores != null && colRequiereServicioProveedores.Count > 0)
        //            {
        //                item.RequiereServicioProveedores = (from elemento in colRequiereServicioProveedores where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList();
        //            }
        //        }
        //    }
        //}

        public void CargarRelaciones(ref List<BE.RequiereServicio> colObj, IEnumerable<decimal> estados = null, string lang = "",long personaId = 0, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<decimal> llavesP;
            IEnumerable<string> sllaves;
            List<BE.Servicio> colServicio = null;
            List<BE.EstadoReqServ> colestadoReqServ = null;
            List<BE.RequiereServicioProveedores> colRequiereServicioProveedores = null;
            List<BE.PersonaDireccion> colPersonaDireccion = null;
            List<BE.Persona> colPersona = null;
            List<BE.ServAsig> colServAsig = null;
            List<BE.requiereServicioDetalle> colrequiereServicioDetalle = null;
            List<BE.requiereServicioDetalleWeb> colrequiereServicioDetalleWeb = null;

            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relRequiereServicio.servicio))
                {
                    BC.Servicio bcServicio = new BC.Servicio(cadenaConexion);

                    llaves = (from elemento in colObj where elemento.ServicioId != null select Convert.ToDecimal(elemento.ServicioId)).Distinct();
                    colServicio = bcServicio.ObtenerHijos(llaves, lang, false,relaciones);
                    bcServicio = null;
                }
                if (clase.Equals(BE.relRequiereServicio.estadoReqServ))
                {
                   BC.EstadoReqServ bcEstadoReqServ = new BC.EstadoReqServ(cadenaConexion);

                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.EstadoReqServId)).Distinct();
                    colestadoReqServ = bcEstadoReqServ.ObtenerHijos(llaves, lang, relaciones);
                    bcEstadoReqServ = null;
                }
                if (clase.Equals(BE.relRequiereServicio.requiereServicioProveedores))
                {
                    BC.RequiereServicioProveedores bcRequiereServicioProveedores = new BC.RequiereServicioProveedores(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    colRequiereServicioProveedores = bcRequiereServicioProveedores.ObtenerHijos(sllaves, null,lang,0, relaciones);
                    bcRequiereServicioProveedores = null;
                }
                if (clase.Equals(BE.relRequiereServicio.reqServProvPersona))
                {
                    BC.RequiereServicioProveedores bcRequiereServicioProveedores = new BC.RequiereServicioProveedores(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    colRequiereServicioProveedores = bcRequiereServicioProveedores.ObtenerHijos(sllaves, null, lang,personaId , relaciones);
                    bcRequiereServicioProveedores = null;
                }
                if (clase.Equals(BE.relRequiereServicio.reqServProvAdj))
                {
                    BC.RequiereServicioProveedores bcRequiereServicioProveedores = new BC.RequiereServicioProveedores(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    llaves = new decimal[] { 4 };
                    colRequiereServicioProveedores = bcRequiereServicioProveedores.ObtenerHijos(sllaves, llaves,lang,0, relaciones);
                    bcRequiereServicioProveedores = null;
                }
                if (clase.Equals(BE.relRequiereServicio.personaDireccion))
                {
                    BC.PersonaDireccion bcPersonaDireccion = new BC.PersonaDireccion(cadenaConexion);
                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.PersonaDireccionId)                      
                              ).Distinct();

                    llavesP = (from elemento in colObj select Convert.ToDecimal(elemento.PersonaId)
                               ).Distinct();

                    colPersonaDireccion = bcPersonaDireccion.ObtenerHijos(llaves,llavesP, relaciones);
                    bcPersonaDireccion = null;
                }
                if (clase.Equals(BE.relRequiereServicio.persona))
                {
                    BC.Persona bcPersona = new BC.Persona(cadenaConexion);
                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.PersonaId)).Distinct();

                    colPersona = bcPersona.ObtenerHijos(llaves, relaciones);
                    bcPersona = null;
                }
                if (clase.Equals(BE.relRequiereServicio.servAsig))
                {
                    BC.ServAsig  bcServAsig = new BC.ServAsig(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento .RequiereServicioId).Distinct();

                    colServAsig  = bcServAsig.ObtenerPadres(sllaves,lang,relaciones);
                    bcServAsig = null;
                }
                if (clase.Equals(BE.relRequiereServicio.requiereServicioDetalle))
                {
                    BC.requiereServicioDetalle bcrequiereServicioDetalle = new BC.requiereServicioDetalle(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    colrequiereServicioDetalle = bcrequiereServicioDetalle.ObtenerHijos(sllaves, relaciones);
                    bcrequiereServicioDetalle = null;
                }
                if (clase.Equals(BE.relRequiereServicio.requiereServicioDetalleWeb))
                {
                    BC.requiereServicioDetalleWeb bcrequiereServicioDetalleWeb = new BC.requiereServicioDetalleWeb(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    colrequiereServicioDetalleWeb = bcrequiereServicioDetalleWeb.ObtenerHijos(sllaves, relaciones);
                    bcrequiereServicioDetalleWeb = null;
                }
            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {
                    if (colServicio != null && colServicio.Count > 0)
                    {
                        item.servicio = (from elemento in colServicio where elemento.ServicioId == item.ServicioId select elemento).ToList().FirstOrDefault();
                    }
                    if (colestadoReqServ != null && colestadoReqServ.Count > 0)
                    {
                        item.estadoReqServ = (from elemento in colestadoReqServ where elemento.EstadoReqServId == item.EstadoReqServId select elemento).ToList().FirstOrDefault();
                    }
                    if (colRequiereServicioProveedores != null && colRequiereServicioProveedores.Count > 0)
                    {
                        item.RequiereServicioProveedores = (from elemento in colRequiereServicioProveedores where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList();
                    }
                    if (colPersonaDireccion != null && colPersonaDireccion.Count > 0)
                    {
                        item.personaDireccion = (from elemento in colPersonaDireccion
                                                 where (elemento.PersonaDireccionId == item.PersonaDireccionId && elemento.PersonaId==item.PersonaId)
                                                 select elemento).ToList().FirstOrDefault();
                    }
                    if (colPersona != null && colPersona.Count > 0)
                    {
                        item.persona = (from elemento in colPersona where elemento.PersonaId == item.PersonaId select elemento).ToList().FirstOrDefault();
                    }
                    if (colServAsig != null && colServAsig.Count > 0)
                    {
                        item.servAsig = (from elemento in colServAsig where elemento.RequiereServicioId  == item.RequiereServicioId  select elemento).ToList().FirstOrDefault();
                    }
                    if (colrequiereServicioDetalle != null && colrequiereServicioDetalle.Count > 0)
                    {
                        item.requiereServicioDetalle = (from elemento in colrequiereServicioDetalle where elemento.requiereServicioId == item.RequiereServicioId select elemento).ToList();
                    }
                    if (colrequiereServicioDetalleWeb != null && colrequiereServicioDetalleWeb.Count > 0)
                    {
                        item.requiereServicioDetalleWeb = (from elemento in colrequiereServicioDetalleWeb where elemento.requiereServicioId == item.RequiereServicioId select elemento).ToList();
                    }
                }
            }
        }

        public void CargarRelaciones(ref BE.RequiereServicio obj,  IEnumerable<decimal>estados=null, string lang = "", long personaId = 0, params Enum[] relaciones)
        {
            if (relaciones == null || obj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<decimal> llavesc;
            IEnumerable<string> sllaves;
            foreach (Enum clase in relaciones)
            {
              
                if (clase.Equals(BE.relRequiereServicio.servicio))
                {
                    BC.Servicio bsServicio = new BC.Servicio(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(obj.ServicioId) };
                    obj.servicio = bsServicio.ObtenerHijos(llaves, lang,false,relaciones).ToList().FirstOrDefault();
                    bsServicio = null;
                }
                if (clase.Equals(BE.relRequiereServicio.requiereServicioProveedores))
                {
                    BC.RequiereServicioProveedores bcReqServP = new BC.RequiereServicioProveedores(cadenaConexion);
                    sllaves = new string[] { obj.RequiereServicioId };
                    obj.RequiereServicioProveedores = bcReqServP.ObtenerHijos(sllaves,estados,"",0,relaciones);
                    bcReqServP = null;
                }
                if (clase.Equals(BE.relRequiereServicio.personaDireccion))
                {
                    if (obj.PersonaDireccionId .HasValue)
                    {
                        BC.PersonaDireccion bcPersonaDireccion = new BC.PersonaDireccion(cadenaConexion);
                        llaves = new decimal[] { Convert.ToDecimal(obj.PersonaDireccionId) };
                        IEnumerable<decimal> llavesP;
                        llavesP = new decimal[] { Convert.ToDecimal(obj.PersonaId) };
                        obj.personaDireccion = bcPersonaDireccion.ObtenerHijos(llaves, llavesP, relaciones).ToList ().FirstOrDefault ();                        
                        bcPersonaDireccion = null;
                    }                    
                }
                if (clase.Equals(BE.relRequiereServicio.persona))
                {
                    BC.Persona bcPersona = new BC.Persona(cadenaConexion);                    
                    obj.persona = bcPersona.BuscarPersonaxId(obj.PersonaId, "", relaciones);
                    bcPersona = null;
                }
                if (clase.Equals(BE.relRequiereServicio.servAsig))
                {
                    BC.ServAsig bcServAsig = new BC.ServAsig(cadenaConexion);                    
                    obj.servAsig = bcServAsig.BuscarServAsigxRequiereServicioId(obj.RequiereServicioId, relaciones);
                    bcServAsig = null;
                }
                if (clase.Equals(BE.relRequiereServicio.estadoReqServ))
                {
                    BC.EstadoReqServ bcEstadoReqServ = new BC.EstadoReqServ(cadenaConexion);                  
                    obj.estadoReqServ  = bcEstadoReqServ.BuscarEstadoReqServxId(obj.EstadoReqServId);
                    bcEstadoReqServ = null;
                }
                if (clase.Equals(BE.relRequiereServicio.requiereServicioDetalle))
                {
                    BC.requiereServicioDetalle bcrequiereServicioDetalle = new BC.requiereServicioDetalle(cadenaConexion);
                    sllaves = new string[] { obj.RequiereServicioId };
                    obj.requiereServicioDetalle = bcrequiereServicioDetalle.ObtenerHijos(sllaves, relaciones);
                    bcrequiereServicioDetalle = null;
                }
                if (clase.Equals(BE.relRequiereServicio.requiereServicioDetalle))
                {
                    BC.requiereServicioDetalle bcrequiereServicioDetalle = new BC.requiereServicioDetalle(cadenaConexion);
                    sllaves = new string[] { obj.RequiereServicioId };
                    obj.requiereServicioDetalle = bcrequiereServicioDetalle.ObtenerHijos(sllaves, relaciones);
                    bcrequiereServicioDetalle = null;
                }
                if (clase.Equals(BE.relRequiereServicio.requiereServicioDetalleWeb))
                {
                    BC.requiereServicioDetalleWeb bcrequiereServicioDetalleWeb = new BC.requiereServicioDetalleWeb(cadenaConexion);
                    sllaves = new string[] { obj.RequiereServicioId };
                    obj.requiereServicioDetalleWeb = bcrequiereServicioDetalleWeb.ObtenerHijos(sllaves, relaciones);
                    bcrequiereServicioDetalleWeb = null;
                }
                if (clase.Equals(BE.relRequiereServicio.IdiomaS))
                {
                    BC.IdiomaS bcIdiomaS = new BC.IdiomaS(cadenaConexion);
                    llaves = new decimal[] { obj.EstadoReqServId };
                    obj.IdiomaS = bcIdiomaS.ObtenerHijos(llaves, relaciones);
                    bcIdiomaS = null;
                }
                if (clase.Equals(BE.relRequiereServicio.IdiomaServ))
                {
                    BC.IdiomaS bcIdiomaS = new BC.IdiomaS(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(obj.ServicioId) };
                   // llavesc = new decimal[] { Convert.ToDecimal(obj.servicio.CategoriaServicioId) };
                    obj.IdiomaServ = bcIdiomaS.ObtenerHijosServicio(llaves, relaciones);
                    bcIdiomaS = null;
                   
                }
                if (clase.Equals(BE.relRequiereServicio.RequiereServicioProveedoresF))
                {
                    BC.RequiereServicioProveedoresF bcReqServP = new BC.RequiereServicioProveedoresF(cadenaConexion);
                    sllaves = new string[] { obj.RequiereServicioId };
                    obj.RequiereServicioProveedoresF = bcReqServP.ObtenerHijos(sllaves, relaciones);
                    bcReqServP = null;
                }
            }
        }
        #endregion

        public List<BE.RequiereServicio> ObtenerHijos(IEnumerable<string> llaves,string lang="", params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.RequiereServicio rse with(nolock) where rse.RequiereServicioId in {1}", campos("rse"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,null,lang,0,relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        #region "Busqueda"

        public BE.RequiereServicio BuscarRequiereServicioxId(string RequiereServicioId,string lang, params Enum[] relaciones)
        {
            BE.RequiereServicio obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT * from RequiereServicio with(nolock)  where RequiereServicioId='{0}'", RequiereServicioId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null,lang, 0, relaciones);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public BE.RequiereServicio BuscarRequiereServicioxIdFirebase(string RequiereServicioId, string lang, params Enum[] relaciones)
        {
            BE.RequiereServicio obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT * from RequiereServicio with(nolock)  where RequiereServicioId='{0}'", RequiereServicioId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, lang, 0, relaciones);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public BE.RequiereServicio BuscargetReqSerxSerAsigId(string serAsigId, string lang, params Enum[] relaciones)
        {
            BE.RequiereServicio obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select {0} from ServAsig sa with(nolock)
inner join RequiereServicio rs with(nolock)
on sa.RequiereServicioId=rs.RequiereServicioId
where sa.ServAsigId='{1}'", campos("rs"), serAsigId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, lang, 0, relaciones);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public BE.RequiereServicio BuscarPorUID(string uid, params Enum[] relaciones)
        {
            BE.RequiereServicio obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0} from dbo.RequiereServicio rse with(nolock) where rse.RequiereServicioUID = '{1}'",campos ("rse"), uid);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion
        #region "Listado"
        public List<BE.RequiereServicio> ListadoRequiereServicio(long personaId, string lang, params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from(
                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
                            inner join [ServicioSP]  ('{1}') s
                            on rs.ServicioId=s.servicioId
                            inner join  [CategoriaServicioSP]('{1}') cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join [dbo].[EstadoReqServSP] ('{1}') ers
                            on rs.EstadoReqServId=ers.EstadoReqServId
 inner join dbo.PersonaDireccion pdir 
 on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsigSP  ('{1}') ssa on sra.StatusServAsigId = ssa.StatusServAsigId

                            where sp.PersonaId={0}
                            and rs.EstadoReqServId = 2
                            and rsp.StatusRequiereId in (4)
                            and rs.RequiereServicioId in(
                            select RequiereServicioId from ServAsig sra2 with(nolock) where sra2.StatusServAsigId <> 4)
                            and rs.RequiereServicioInmediato = 0
                            union all

                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 inner join dbo.PersonaDireccion pdir 
  on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 

                            inner join [ServicioSP] ('{1}') s
                            on rs.ServicioId=s.servicioId
                            inner join  [CategoriaServicioSP] ('{1}') cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join [dbo].[EstadoReqServSP]('{1}') ers
                            on rs.EstadoReqServId=ers.EstadoReqServId

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsigSP ('{1}') ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where sp.PersonaId={0}
                            and rsp.StatusRequiereId in (1,2,3)
                            and rs.EstadoReqServId in (1)
                            and rs.RequiereServicioInmediato = 0

                            union all

                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 inner join dbo.PersonaDireccion pdir 
on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 
  
                            inner join [ServicioSP]   ('{1}') s
                            on rs.ServicioId=s.servicioId
                            inner join  [CategoriaServicioSP] ('{1}') cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join [dbo].[EstadoReqServSP]('{1}') ers
                            on rs.EstadoReqServId=ers.EstadoReqServId

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsigSP ('{1}') ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where rs.PersonaId={0}
                            and rs.EstadoReqServId = 2 -- Adjudicado para el Cliente
                            and rsp.StatusRequiereId = 4 -- Adjudicado para el proveedor
                            and rs.RequiereServicioId not in(
                            select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId = 4)
	and rs.RequiereServicioId not in( select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId =3 AND ServAsigFHPago is not null and ServAsigPagaCliente=0)

                             and rs.RequiereServicioInmediato = 0
UNION ALL
-----------------------------------------------------------------------------------------LEFT JIN

               select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
                            inner join [ServicioSP] ('{1}') s
                            on rs.ServicioId=s.servicioId
                            inner join  [CategoriaServicioSP] ('{1}') cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join [dbo].[EstadoReqServSP]('{1}') ers
                            on rs.EstadoReqServId=ers.EstadoReqServId
 LEFT join dbo.PersonaDireccion pdir 
 on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsigSP  ('{1}') ssa on sra.StatusServAsigId = ssa.StatusServAsigId

                            where sp.PersonaId={0}
                            and rs.EstadoReqServId = 2
                            and rsp.StatusRequiereId in (4)
                            and rs.RequiereServicioId in(
                            select RequiereServicioId from ServAsig sra2 with(nolock) where sra2.StatusServAsigId <> 4)
                            and rs.RequiereServicioInmediato = 1
                            union all

                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 LEFT join dbo.PersonaDireccion pdir 
  on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 

                            inner join [ServicioSP]  ('{1}') s
                            on rs.ServicioId=s.servicioId
                            inner join  [CategoriaServicioSP] ('{1}') cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join [dbo].[EstadoReqServSP]('{1}') ers
                            on rs.EstadoReqServId=ers.EstadoReqServId

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsigSP('{1}') ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where sp.PersonaId={0}
                            and rsp.StatusRequiereId in (1,2,3)
                            and rs.EstadoReqServId in (1)
                            and rs.RequiereServicioInmediato = 1

                            union all

                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 LEFT join dbo.PersonaDireccion pdir 
on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 
  
                            inner join [ServicioSP]   ('{1}') s
                            on rs.ServicioId=s.servicioId
                            inner join  [CategoriaServicioSP] ('{1}') cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join [dbo].[EstadoReqServSP] ('{1}') ers
                            on rs.EstadoReqServId=ers.EstadoReqServId

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsigSP  ('{1}') ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where rs.PersonaId={0}
                            and rs.EstadoReqServId = 2 -- Adjudicado para el Cliente
                            and rsp.StatusRequiereId = 4 -- Adjudicado para el proveedor
                            and rs.RequiereServicioId not in(
                            select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId = 4)
	and rs.RequiereServicioId not in( select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId =3 AND ServAsigFHPago is not null and ServAsigPagaCliente=0)

                             and rs.RequiereServicioInmediato = 1





                            ) tmp
                            order by tmp.RequiereServicioFechaMod desc

                        ", personaId, lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,null, lang, personaId, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<BE.RequiereServicio> ListadoRequiereServicio_A_adjudicar(decimal personaId, string lang, params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"
SELECT {0} FROM  requiereservicioProveedores rsp with(nolock)
INNER JOIN serviciopersona sp with(nolock)
on rsp.ServicioPersonaId =sp.ServicioPersonaId
INNER JOIN persona p with(nolock)
on sp.personaId=p.personaId
inner join RequiereServicio rs with(nolock)
on rsp.RequiereServicioId=rs.RequiereServicioId
where rsp.requiereServicioId in (
select requiereServicioId from requiereServicio  rs where rs.requiereServicioId not in(
select RequiereServicioId from servAsig) and rs.EstadoReqServId=1)
and p.personaId={1} and rsp.StatusRequiereId=1
                        ", campos("rs"), personaId, lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, lang, Convert.ToInt32(personaId),
                        relRequiereServicio.servicio,relServicio.categoriaServicio,relCategoriaServicio.Ciudad,
                        
                        relciudad.configuracionCiudad,relciudad.region,relRegion.pais,relpais.moneda,relRequiereServicio.personaDireccion);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public BE.RequiereServicio ListadoRequiereServicioXRequiereServicioId(string RequiereServicioId, string StatusRequiereId, IEnumerable<decimal> estados, string lang = "",params Enum[] relaciones)
        {
            BE.RequiereServicio obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select {0} from RequiereServicio rse with(nolock) where RequiereServicioId='{1}'
                        ", campos("rse"),RequiereServicioId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);

                    CargarRelaciones(ref obj, estados, "", 0, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<BE.RequiereServicio> ListadoRequiereServicioPaginacion(long personaId,int index, int max,string lang, params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from(
                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId
 inner join dbo.PersonaDireccion pdir 
 on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 
                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where sp.PersonaId={0}
                            and rs.EstadoReqServId = 2
                            and rsp.StatusRequiereId in (4)
                            and rs.RequiereServicioId in(
                            select RequiereServicioId from ServAsig sra2 with(nolock) where sra2.StatusServAsigId <> 4)
                            and rs.RequiereServicioInmediato = 0
                            union all
                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
                            inner join dbo.PersonaDireccion pdir 
                            on rs.PersonaDireccionId = pdir.PersonaDireccionId 
                            and rs.PersonaId = pdir.PersonaId 
                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId
                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where sp.PersonaId={0}
                            and rsp.StatusRequiereId in (1,2,3)
                            and rs.EstadoReqServId in (1)
                            and rs.RequiereServicioInmediato = 0
                            union all
                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 inner join dbo.PersonaDireccion pdir 
on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 
                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId
                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where rs.PersonaId={0}
                            and rs.EstadoReqServId = 2 -- Adjudicado para el Cliente
                            and rsp.StatusRequiereId = 4 -- Adjudicado para el proveedor
                            and rs.RequiereServicioId not in(
                            select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId = 4)
	--and rs.RequiereServicioId not in( select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId =3 AND ServAsigFHPago is not null and ServAsigPagaCliente=0)
 and rs.RequiereServicioInmediato = 0
UNION ALL
-----------------------------------------------------------------------------------------LEFT JIN
               select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join  CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId
 LEFT join dbo.PersonaDireccion pdir 
 on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 
                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsigSP  ('{1}') ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where sp.PersonaId={0}
                            and rs.EstadoReqServId = 2
                            and rsp.StatusRequiereId in (4)
                            and rs.RequiereServicioId in(
                            select RequiereServicioId from ServAsig sra2 with(nolock) where sra2.StatusServAsigId <> 4)
                            and rs.RequiereServicioInmediato = 1
                           union all
                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 LEFT join dbo.PersonaDireccion pdir 
  on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 
                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId
                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where sp.PersonaId={0}
                            and rsp.StatusRequiereId in (1,2,3)
                            and rs.EstadoReqServId in (1)
                            and rs.RequiereServicioInmediato = 1
                            union all
                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 LEFT join dbo.PersonaDireccion pdir 
on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 
                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId
                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where rs.PersonaId={0}
                            and rs.EstadoReqServId = 2 -- Adjudicado para el Cliente
                            and rsp.StatusRequiereId = 4 -- Adjudicado para el proveedor
                            and rs.RequiereServicioId not in(
                            select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId = 4)
                        	and rs.RequiereServicioId not in( select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId =3 AND ServAsigFHPago is not null and ServAsigPagaCliente=0)
                             and rs.RequiereServicioInmediato = 1
                            ) tmp  
                            order by tmp.RequiereServicioFechaMod desc
                    OFFSET {2} ROWS
FETCH NEXT {3} ROWS ONLY
                        ", personaId, lang,index-1,max);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,null, lang, personaId, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }









        public int cantidadRequiereServicio(long personaId, string lang)
        {
           int  obj = 0;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from(
                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId

                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId

 inner join dbo.PersonaDireccion pdir 
 on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId

                            where sp.PersonaId={0}
                            and rs.EstadoReqServId = 2
                            and rsp.StatusRequiereId in (4)
                            and rs.RequiereServicioId in(
                            select RequiereServicioId from ServAsig sra2 with(nolock) where sra2.StatusServAsigId <> 4)
                            and rs.RequiereServicioInmediato = 0
                            union all

                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 inner join dbo.PersonaDireccion pdir 
  on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 


                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId


                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where sp.PersonaId={0}
                            and rsp.StatusRequiereId in (1,2,3)
                            and rs.EstadoReqServId in (1)
                            and rs.RequiereServicioInmediato = 0

                            union all

                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 inner join dbo.PersonaDireccion pdir 
on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 

                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where rs.PersonaId={0}
                            and rs.EstadoReqServId = 2 -- Adjudicado para el Cliente
                            and rsp.StatusRequiereId = 4 -- Adjudicado para el proveedor
                            and rs.RequiereServicioId not in(
                            select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId = 4)
	--and rs.RequiereServicioId not in( select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId =3 AND ServAsigFHPago is not null and ServAsigPagaCliente=0)

                             and rs.RequiereServicioInmediato = 0
UNION ALL
-----------------------------------------------------------------------------------------LEFT JIN

               select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId

                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join  CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId

 LEFT join dbo.PersonaDireccion pdir 
 on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 

                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsigSP  ('{1}') ssa on sra.StatusServAsigId = ssa.StatusServAsigId

                            where sp.PersonaId={0}
                            and rs.EstadoReqServId = 2
                            and rsp.StatusRequiereId in (4)
                            and rs.RequiereServicioId in(
                            select RequiereServicioId from ServAsig sra2 with(nolock) where sra2.StatusServAsigId <> 4)
                            and rs.RequiereServicioInmediato = 1
                            union all

                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 LEFT join dbo.PersonaDireccion pdir 
  on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 


                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId


                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where sp.PersonaId={0}
                            and rsp.StatusRequiereId in (1,2,3)
                            and rs.EstadoReqServId in (1)
                            and rs.RequiereServicioInmediato = 1

                            union all

                            select rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                            ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                            ,rs.RequiereServicioProvLast,ISNULL(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                            from ServicioPersona sp with(nolock)
                            inner join RequiereServicioProveedores rsp with(nolock)
                            on sp.ServicioPersonaId=rsp.ServicioPersonaId
                            inner join RequiereServicio rs with(nolock) 
                            on rsp.RequiereServicioId=rs.RequiereServicioId
 LEFT join dbo.PersonaDireccion pdir 
on rs.PersonaDireccionId = pdir.PersonaDireccionId 
 and rs.PersonaId = pdir.PersonaId 
  

                            inner join Servicio s
                            on rs.ServicioId=s.servicioId
                            inner join CategoriaServicio cs 
                            on s.CategoriaServicioId=cs.CategoriaServicioId
                            inner join EstadoReqServ ers
                            on rs.EstadoReqServId=ers.EstadoReqServId


                            inner join dbo.Persona per with(nolock) on rs.PersonaId = per.PersonaId 
                            left join dbo.ServAsig sra with(nolock) on rs.RequiereServicioId = sra.RequiereServicioId 
                            left join dbo.StatusServAsig ssa on sra.StatusServAsigId = ssa.StatusServAsigId
                            where rs.PersonaId={0}
                            and rs.EstadoReqServId = 2 -- Adjudicado para el Cliente
                            and rsp.StatusRequiereId = 4 -- Adjudicado para el proveedor
                            and rs.RequiereServicioId not in(
                            select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId = 4)
                             	and rs.RequiereServicioId not in( select RequiereServicioId from ServAsig with(nolock) where StatusServAsigId =3 AND ServAsigFHPago is not null and ServAsigPagaCliente=0)
                             and rs.RequiereServicioInmediato = 1
                            ) tmp  

                        ", personaId, lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = dr.Length;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public int ImporteRequiereServicio(string RequiereServicioId,bool ServicioDetalleTipo)
        {
            int obj = 0;
        
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "ImporteRequiereServicio";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@RequiereServicioId", RequiereServicioId);
                conx.AsignarParametro("@ServicioDetalleTipo", ServicioDetalleTipo);
                obj=Convert.ToInt32( conx.EjecutarScalar());
            
            }
            catch (Exception ex)
            {

                throw;
            }

          
            return obj;
        }


      


        public List<BE.RequiereServicio> ListadoRequiereServicioXEstado(long personaId, string lang,string EstadoReqServId, params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from (
                        select  rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                        ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                        ,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                        from [dbo].[RequiereServicio] rs
                       
                        inner join dbo.PersonaDireccion pdir with(nolock) 
                       on rs.PersonaDireccionId = pdir.PersonaDireccionId 
                        and rs.PersonaId = pdir.PersonaId 
                        where rs.PersonaId={0} and rs.RequiereServicioInmediato = 0
                         and ',{2},' LIKE '%,'+CAST(rs.EstadoReqServId AS varchar)+',%'
                        union all
                        select  rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
                        ,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
                        ,rs.RequiereServicioProvLast,isnull(rs.PersonaDireccionId,0)PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod,rs.RequiereServicioUID,rs.RequiereServicioInmediato,rs.RequiereServicioGeoInmediato,rs.RequiereServicioOtros
                        from [dbo].[RequiereServicio] rs
                      
                    LEFT join dbo.PersonaDireccion pdir with(nolock) 
                       on rs.PersonaDireccionId = pdir.PersonaDireccionId 
                        and rs.PersonaId = pdir.PersonaId 

                        where rs.PersonaId={0} and rs.RequiereServicioInmediato = 1
                         and ',{2},' LIKE '%,'+CAST(rs.EstadoReqServId AS varchar)+',%'

                         )C1
                        order by C1.RequiereServicioFechaMod desc
                        ", personaId, lang, EstadoReqServId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,null, lang,personaId, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public int CantidadRequiereServicioXEstado(long personaId,String lang, string EstadoReqServId)
        {
            int obj = 0;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select RequiereServicioId from (
                        select  rs.RequiereServicioId
                        from [dbo].[RequiereServicio] rs
                      
                        inner join dbo.PersonaDireccion pdir with(nolock) 
                       on rs.PersonaDireccionId = pdir.PersonaDireccionId 
                        and rs.PersonaId = pdir.PersonaId 
                        where rs.PersonaId={0} and rs.RequiereServicioInmediato = 0
                         and ',{2},' LIKE '%,'+CAST(rs.EstadoReqServId AS varchar)+',%'
                        union all
                        select  rs.RequiereServicioId
                        from [dbo].[RequiereServicio] rs
                    
                    LEFT join dbo.PersonaDireccion pdir with(nolock) 
                       on rs.PersonaDireccionId = pdir.PersonaDireccionId 
                        and rs.PersonaId = pdir.PersonaId 

                        where rs.PersonaId={0} and rs.RequiereServicioInmediato = 1
                         and ',{2},' LIKE '%,'+CAST(rs.EstadoReqServId AS varchar)+',%'

                         )C1                       
                        ", personaId, lang, EstadoReqServId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = dr.Length;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        /*public List<BE.RequiereServicio> ListadoRequiereServicioCliente(long personaId, string lang, params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select  rs.RequiereServicioId,rs.PersonaId,rs.RequiereServicioFechaHoraReq,rs.RequiereServicioFHCaduca,rs.EstadoReqServId,rs.RequiereServicioFHDeseada
,rs.RequiereServicioDescripcion,rs.RequiereServicioURLFoto1,rs.RequiereServicioURLFoto2,rs.RequiereServicioURLFoto3,rs.RequiereServicioURLVideo
,rs.RequiereServicioProvLast,rs.PersonaDireccionId,rs.ServicioId,rs.RequiereServicioFechaMod from [dbo].[RequiereServicio] rs
inner join [ServicioSP]  ('{1}') s
on rs.ServicioId=s.servicioId
inner join [CategoriaServicioSP]('{1}') cs
on s.CategoriaServicioId=cs.CategoriaServicioId
inner join [dbo].[EstadoReqServSP] ('{1}') ers
on rs.EstadoReqServId=ers.EstadoReqServId
inner join dbo.PersonaDireccion pdir on rs.PersonaDireccionId = pdir.PersonaDireccionId
and rs.PersonaId = pdir.PersonaId 
where
 rs.PersonaId={0}
", personaId, lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones1(ref obj, lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }*/

        #endregion
        public Boolean RegistrarSolicitud(ref BE.RequiereServicio requiereServicio)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                bolOk = Actualizar(ref requiereServicio, true);
                int seguro = 0;
                int importe = 0;
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }
                if (requiereServicio.TipoEstado == BE.TipoEstado.Insertar)
                {
                    if (requiereServicio.requiereServicioDetalle != null)
                    {
                        BC.requiereServicioDetalle bcReqServicioDetalle = new BC.requiereServicioDetalle(cadenaConexion);
                        bcReqServicioDetalle.dbConexion = conx;
                        foreach (BE.requiereServicioDetalle item in requiereServicio.requiereServicioDetalle)
                        {
                            item.requiereServicioId = requiereServicio.RequiereServicioId;
                            item.servicioId = Convert.ToDecimal(requiereServicio.ServicioId);
                            BE.requiereServicioDetalle requiereServicioDetalle = item;
                            bolOk = bcReqServicioDetalle.Actualizar(ref requiereServicioDetalle, true);

                            if (!bolOk)
                            {
                                throw new Exception("Error al enviar Solicitudes");
                            }
                           
                        }

                        bcReqServicioDetalle = null;
                    }

                  

                }
                if (requiereServicio.TipoEstado == BE.TipoEstado.Modificar)
                {


                    if (requiereServicio.requiereServicioDetalle != null)
                    {
                        BC.requiereServicioDetalle bcReqServicioDetalle = new BC.requiereServicioDetalle(cadenaConexion);
                        bcReqServicioDetalle.dbConexion = conx;
                        foreach (BE.requiereServicioDetalle item in requiereServicio.requiereServicioDetalle)
                        {
                            item.requiereServicioId = requiereServicio.RequiereServicioId;
                            item.servicioId = Convert.ToDecimal(requiereServicio.ServicioId);
                            BE.requiereServicioDetalle requiereServicioDetalle = item;
                            bolOk = bcReqServicioDetalle.Actualizar(ref requiereServicioDetalle, true);

                            if (!bolOk)
                            {
                                throw new Exception("Error al enviar Solicitudes");
                            }
                           
                        }

                        bcReqServicioDetalle = null;
                    }



                }
                conx.ConfirmarTransaccion();
            
                if (bolOk == true)
                {
                    if (requiereServicio.TipoEstado == BE.TipoEstado.Insertar)
                    {
                        if (requiereServicio.RequiereServicioProveedores == null)
                        {
                            BC.ServicioPersona bcServPersona = new BC.ServicioPersona(cadenaConexion);
                            BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                            BC.RequiereServicioProveedores bcReqProv = new BC.RequiereServicioProveedores(cadenaConexion);
                            bcReqProv.dbConexion = conx;
                            bcServPersona.dbConexion = conx;
                            bcRequiereServicio.dbConexion = conx;
                            BE.RequiereServicio rs = new BE.RequiereServicio();
                            List<BE.ServicioPersona> servicioPersonas = bcServPersona.VerServicioPersona(Convert.ToString(Convert.ToInt32(requiereServicio.ServicioId)), null);
                            rs = bcRequiereServicio.BuscarRequiereServicioxId(requiereServicio.RequiereServicioId, "es", relRequiereServicio.servicio);
                            foreach (BE.ServicioPersona item in servicioPersonas)
                            {
                                if (rs.requiereServicioOtros == true)
                                {
                                    rs.servicio.servicioDetalleTipo = true;
                                }
                                importe = (bcRequiereServicio.ImporteRequiereServicio(rs.RequiereServicioId, rs.servicio.servicioDetalleTipo));


                                BE.RequiereServicioProveedores requiereServicioProveedores = new BE.RequiereServicioProveedores();
                                requiereServicioProveedores.TipoEstado = TipoEstado.Insertar;
                                requiereServicioProveedores.RequiereServicioId = rs.RequiereServicioId;
                                requiereServicioProveedores.RequiereServicioProveedoresId = 0;
                                requiereServicioProveedores.RequiereServicioProveedoresAdj = false;
                                requiereServicioProveedores.RequiereServicioProvCotizacion = importe;
                                requiereServicioProveedores.RequiereServicioProvFHTrabajo = rs.RequiereServicioFechaHoraReq;
                                requiereServicioProveedores.RequiereServicioProvDescipcion = "";
                                requiereServicioProveedores.ServicioPersonaId = item.ServicioPersonaId;
                                requiereServicioProveedores.RequiereServicioProvFHResp = rs.RequiereServicioFechaHoraReq;
                                requiereServicioProveedores.StatusRequiereId = 1;

                                BE.RequiereServicioProveedores prov = requiereServicioProveedores;
                                bolOk = bcReqProv.Actualizar(ref prov, true);

                                if (!bolOk)
                                {
                                    throw new Exception("Error al enviar Solicitudes");
                                }

                            }

                            bcReqProv = null;
                        }
                    }
                }
                conx.Desconectar();
            }
            catch (Exception ex)
            {
                conx.CancelarTransaccion();
                conx.Desconectar();
                throw ex;
            }
                return bolOk;
        }


        public Boolean RegistrarAdjudicacion(ref BE.RequiereServicio requiereServicio,decimal ProveedorId, string StatusRequiereId)
        {
            Boolean bolOk = false;
            Boolean rechazado = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
             

           
                if (requiereServicio.TipoEstado == BE.TipoEstado.Modificar)
                {                             
                  
                    if (StatusRequiereId == "0")
                    {
                        BC.RequiereServicioProveedores bcReqProv = new BC.RequiereServicioProveedores(cadenaConexion);
                        bcReqProv.dbConexion = conx;
                        rechazado = bcReqProv.ver_Si_Rechazaron_todos_Prov(requiereServicio.RequiereServicioId, null);
                        if (rechazado == true)
                        {
                            requiereServicio.EstadoReqServId = 4;
                        }

                        ///////////////////////////////////////////////////////////////////////
                        BE.RequiereServicioProveedores requiereServicioProveedores = new BE.RequiereServicioProveedores();
                        foreach (BE.RequiereServicioProveedores item in requiereServicio.RequiereServicioProveedores)
                        {
                            BC.RequiereServicioProveedores bcReqProv1 = new BC.RequiereServicioProveedores(cadenaConexion);
                            bcReqProv1.dbConexion = conx;
                            item.TipoEstado = BE.TipoEstado.Modificar;

                            if (item.servicioPersona.PersonaId == ProveedorId)
                            {
                          
                                if (StatusRequiereId == "0")
                                {
                                    item.StatusRequiereId = 3;
                                    requiereServicioProveedores = item;
                                    bcReqProv1.Actualizar(ref requiereServicioProveedores, true);
                                }
                            }


                        }
                        /////////////////////////////////////////////////////////////////////////77

                    }                                      

                    if (StatusRequiereId == "1")
                    {
                        requiereServicio.EstadoReqServId = 2;
                        BC.ServAsig bcservAsig = new BC.ServAsig(cadenaConexion);
                        bcservAsig.dbConexion = conx;
                        BE.ServAsig servAsig = new BE.ServAsig();
                        servAsig = bcservAsig.BuscarServAsigxRequiereServicioId(requiereServicio.RequiereServicioId, null);
                        if (servAsig == null)
                        {
                            BE.ServAsig servAsig1 = new BE.ServAsig();
                            servAsig1.ServAsigId = "0";
                            servAsig1.ProveedorId = ProveedorId;
                            servAsig1.ServAsigFHUbicacion = DateTime.Now;
                            servAsig1.ServAsigFHEstimadaLlegada = requiereServicio.RequiereServicioFechaHoraReq;
                            servAsig1.ServAsigCostoTotal = 0;
                            servAsig1.StatusServAsigId = 1;
                            servAsig1.RequiereServicioId = requiereServicio.RequiereServicioId;
                            servAsig1.TipoEstado = BE.TipoEstado.Insertar;
                            bcservAsig.Actualizar(ref servAsig1, true);
                            bolOk = Actualizar(ref requiereServicio, true);

                           
                       
                            BE.RequiereServicioProveedores requiereServicioProveedores = new BE.RequiereServicioProveedores();
                            foreach (BE.RequiereServicioProveedores item in requiereServicio.RequiereServicioProveedores)
                            {
                                BC.RequiereServicioProveedores bcReqProv = new BC.RequiereServicioProveedores(cadenaConexion);
                                bcReqProv.dbConexion = conx;
                                item.TipoEstado = BE.TipoEstado.Modificar;

                                if (item.servicioPersona.PersonaId == ProveedorId)
                                {
                                    if (StatusRequiereId == "1")
                                    {
                                        item.StatusRequiereId = 4;
                                        requiereServicioProveedores = item;
                                        bcReqProv.Actualizar(ref requiereServicioProveedores, true);
                                       // break;
                                    }
                                    if (StatusRequiereId == "0")
                                    {
                                        item.StatusRequiereId = 3;
                                        bcReqProv.Actualizar(ref requiereServicioProveedores, true);
                                    }
                                }

                              
                            }

                            BC.NotificacionPersona bcNotificacionPersona = new BC.NotificacionPersona(cadenaConexion);
                            bcNotificacionPersona.dbConexion = conx;
                            bolOk = bcNotificacionPersona.ActualizarEstadoAdjudicacion(requiereServicio.RequiereServicioId, true);
                        }

                    }
                }
                conx.ConfirmarTransaccion();
                conx.Desconectar();
                bolOk = true;
            }
            catch (Exception ex)
            {
                conx.CancelarTransaccion();
                conx.Desconectar();
                throw ex;
            }
            return bolOk;
        }

        public Boolean ValidarFotosEncarpeta(BE.RequiereServicio requiereServicio)
        {
          /*  foreach (  item in requiereServicio)
            {

            }*/
            return true;
        }

        public List<BE.RequiereServicio> ListadoRequerimientosaInciarServicio(DateTime hora,params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@" select {0}
                 from requiereServicio rs with(nolock)
                 inner
                 join servAsig sa with(nolock)
                on rs.requiereServicioId =  sa.RequiereServicioId 
                inner join persona per with(nolock) 
                on sa.ProveedorId = per.personaId 
                inner join servicio s with(nolock)
                on rs.servicioId = s.servicioId AND year(rs.RequiereServicioFHDeseada) =
                 year(getdate())  AND month(rs.RequiereServicioFHDeseada)= month(getdate())
    AND day(rs.RequiereServicioFHDeseada)= day(getdate())  and DATEPART(HOUR,
    rs.RequiereServicioFHDeseada)= DATEPART(Hour,'{1}')  and sa.ServAsigFHInicio is null
;", campos("rs"),hora);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null,"",0, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

   
        public List<BE.RequiereServicio> ListadoRequerimientos_Para_Adjudicar(params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

             
                ////////
                string sql = String.Format(@" select {0} from RequiereServicio rs with(nolock)
where rs.RequiereServicioId not in (
select rs.RequiereServicioId from RequiereServicio rs with(nolock)
inner join RequiereServicioProveedores rsp with(nolock)
on rs.RequiereServicioId=rsp.RequiereServicioId
where rsp.StatusRequiereId IN (4))and rs.EstadoReqServId=1
;", campos("rs"));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, "", 0, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public List<BE.RequiereServicio> ListadoRequerimientos_NuevoRequerimientos(params Enum[] relaciones)
        {
            List<BE.RequiereServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {


                ////////
                string sql = String.Format(@" select {0} from RequiereServicio rs with(nolock)
where rs.RequiereServicioId not in (
select rs.RequiereServicioId from RequiereServicio rs with(nolock)
inner join RequiereServicioProveedores rsp with(nolock)
on rs.RequiereServicioId=rsp.RequiereServicioId
where rsp.StatusRequiereId=4)and rs.EstadoReqServId=1 and rs.RequiereServicioId not in (select title  from Log_Notificacion with(nolock) )
;", campos("rs"));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, "", 0, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public Boolean ver_Si_se_EstaCancelado(string RequiereServicioId, string lang, params Enum[] relaciones)
        {
            List<BE.RequiereServicioProveedores> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            Boolean bolOk = false;
            int cont = 0;
            try
            {
                string sql = String.Format(@"select COUNT(RequiereServicioId) from RequiereServicio rs where RequiereServicioId='{1}'
			  and rs.EstadoReqServId=3", campos("rsp"), RequiereServicioId);

                cont = Convert.ToInt32(conx.ObtenerValor(sql));
                if (cont >= 1)
                {
                    bolOk = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolOk;
        }

        public Boolean ver_Si_se_Envio_Notificacion(string RequiereServicioId, params Enum[] relaciones)
        {
            List<BE.RequiereServicioProveedores> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            Boolean bolOk = false;
            int cont = 0;
            try
            {
                string sql = String.Format(@"select COUNT(deviceTokens) from [Log_Notificacion]  where Title='{0}'", RequiereServicioId);

                cont = Convert.ToInt32(conx.ObtenerValor(sql));
                if (cont >= 1)
                {
                    bolOk = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolOk;
        }


        public Boolean validarSiEstaAdjudicado(string RequiereServicioId, params Enum[] relaciones)
        {
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            Boolean bolOk = false;
            int cont = 0;
            try
            {
                string sql = String.Format(@"select COUNT(ServAsigId) from ServAsig where RequiereServicioId='{0}'", RequiereServicioId);

                cont = Convert.ToInt32(conx.ObtenerValor(sql));
                if (cont >= 1)
                {
                    bolOk = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolOk;
        }

        public Boolean validarSiEstaAdjudicadoyEsMayora6horas(string RequiereServicioId, params Enum[] relaciones)
        {
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            Boolean bolOk = false;
            int cont = 0;
            try
            {
                DateTime HoraBoliviana = ObtenerHoraBoliviana();
                string hORA = HoraBoliviana.ToString("yyyy-MM-dd HH:MM:ss");
                string sql = String.Format(@"select count(rs.requiereServicioid) from requiereServicio rs 
inner join ServAsig sa
on rs.RequiereServicioId=sa.RequiereServicioId
where
DATEDIFF(HOUR, '{1}',rs.requiereServicioFHDeseada)>6
and rs.RequiereServicioId='{0}'

", RequiereServicioId,hORA);
                
                cont = Convert.ToInt32(conx.ObtenerValor(sql));
                if (cont >= 1)
                {
                    bolOk = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolOk;
        }
        public DateTime ObtenerHoraBoliviana()
        {
            DateTime localDateTime = DateTime.Now;
            localDateTime = localDateTime.ToLocalTime();
            DateTime utcDateTime = localDateTime.ToUniversalTime();
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SA Western Standard Time");
            DateTime worldDateTime = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(utcDateTime.ToString()), timeZoneInfo);

            return worldDateTime;
        }


    }


}
