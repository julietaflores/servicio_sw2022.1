using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using BE;

namespace BC
{
    public class RequiereServicioProveedores : BCEntidad
    {
        public RequiereServicioProveedores() : base()
        {
        }

        public RequiereServicioProveedores(string cadConx) : base(cadConx)
        {

        }

        private string campos(string prefijo = "rsp")
        {
            string strCampos = String.Format(@"{0}.RequiereServicioId, {0}.RequiereServicioProveedoresId, {0}.RequiereServicioProveedoresAdj, {0}.RequiereServicioProvCotizacion, {0}.RequiereServicioProvFHTrabajo, {0}.RequiereServicioProvDescipcion, {0}.ServicioPersonaId, {0}.RequiereServicioProvFHResp, {0}.StatusRequiereId"
                    , prefijo);
            return strCampos;
        }

        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.RequiereServicioProveedores BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.RequiereServicioProveedores
                                    SET RequiereServicioId = @requiereservicioid,
	                                    RequiereServicioProveedoresId = @requiereservicioproveedoresid,
	                                    RequiereServicioProveedoresAdj = @requiereservicioproveedoresadj,
	                                    RequiereServicioProvCotizacion = @requiereservicioprovcotizacion,
	                                    RequiereServicioProvFHTrabajo = @requiereservicioprovfhtrabajo,
	                                    RequiereServicioProvDescipcion = @requiereservicioprovdescipcion,
	                                    ServicioPersonaId = @serviciopersonaid,
	                                    RequiereServicioProvFHResp = @requiereservicioprovfhresp,
	                                    StatusRequiereId = @statusrequiereid
                                    where  RequiereServicioId = @requiereservicioid and
	                                    RequiereServicioProveedoresId = @requiereservicioproveedoresid;";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO dbo.RequiereServicioProveedores (RequiereServicioId, RequiereServicioProveedoresId, RequiereServicioProveedoresAdj, RequiereServicioProvCotizacion, RequiereServicioProvFHTrabajo, RequiereServicioProvDescipcion, ServicioPersonaId, RequiereServicioProvFHResp, StatusRequiereId)
                                VALUES (@requiereservicioid, @requiereservicioproveedoresid, @requiereservicioproveedoresadj, @requiereservicioprovcotizacion, @requiereservicioprovfhtrabajo, @requiereservicioprovdescipcion, @serviciopersonaid, @requiereservicioprovfhresp, @statusrequiereid);";
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
                        BEObj.RequiereServicioProveedoresId = System.Convert.ToDecimal(conx.ObtenerValor(String.Format("select isnull(max(RequiereServicioProveedoresId),0) + 1 from dbo.RequiereServicioProveedores with (nolock) where RequiereServicioId = '{0}';", BEObj.RequiereServicioId)));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@requiereservicioid", BEObj.RequiereServicioId);
                    conx.AsignarParametro("@requiereservicioproveedoresid", BEObj.RequiereServicioProveedoresId);
                    conx.AsignarParametro("@requiereservicioproveedoresadj", BEObj.RequiereServicioProveedoresAdj);
                    conx.AsignarParametro("@requiereservicioprovcotizacion", BEObj.RequiereServicioProvCotizacion);
                    conx.AsignarParametro("@requiereservicioprovfhtrabajo", BEObj.RequiereServicioProvFHTrabajo);
                    conx.AsignarParametro("@requiereservicioprovdescipcion", BEObj.RequiereServicioProvDescipcion);
                    conx.AsignarParametro("@serviciopersonaid", BEObj.ServicioPersonaId);
                    conx.AsignarParametro("@requiereservicioprovfhresp", BEObj.RequiereServicioProvFHResp);
                    conx.AsignarParametro("@statusrequiereid", BEObj.StatusRequiereId);
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
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }



        public List<BE.RequiereServicioProveedores> CargarBE(DataRow[] dr)
        {
            List<BE.RequiereServicioProveedores> lst = new List<BE.RequiereServicioProveedores>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.RequiereServicioProveedores CargarBE(DataRow dr)
        {
            BE.RequiereServicioProveedores obj = new BE.RequiereServicioProveedores();
            obj.RequiereServicioId = dr["RequiereServicioId"].ToString();
            obj.RequiereServicioProveedoresId = Convert.ToDecimal(dr["RequiereServicioProveedoresId"].ToString());
            obj.RequiereServicioProveedoresAdj = Convert.ToBoolean(dr["RequiereServicioProveedoresAdj"].ToString());
            obj.RequiereServicioProvCotizacion = Convert.ToDecimal(dr["RequiereServicioProvCotizacion"].ToString());
            obj.RequiereServicioProvFHTrabajo = Convert.ToDateTime(dr["RequiereServicioProvFHTrabajo"].ToString());
            obj.RequiereServicioProvDescipcion = dr["RequiereServicioProvDescipcion"].ToString();
            obj.ServicioPersonaId = Convert.ToDecimal(dr["ServicioPersonaId"].ToString());
            obj.RequiereServicioProvFHResp = Convert.ToDateTime(dr["RequiereServicioProvFHResp"].ToString());
            obj.StatusRequiereId = Convert.ToDecimal(dr["StatusRequiereId"].ToString());
            return obj;
        }


        public void CargarRelaciones(ref List<BE.RequiereServicioProveedores> colObj, string lang = "", params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.ServicioPersona> colServicioPersona = null;
            List<BE.StatusRequiere> colStatusRequiere = null;
            List<BE.RequiereServicio> colRequiereServicio = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relReqServProv.servicioPersona))
                {
                    BC.ServicioPersona bcServicioPersona = new BC.ServicioPersona(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.ServicioPersonaId).Distinct();
                    colServicioPersona = bcServicioPersona.ObtenerHijos(llaves, relaciones);
                    bcServicioPersona = null;
                }
                if (clase.Equals(BE.relReqServProv.statusRequiere))
                {
                    BC.StatusRequiere bcStatusRequiere = new BC.StatusRequiere(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.StatusRequiereId.Value).Distinct();
                    colStatusRequiere = bcStatusRequiere.ObtenerHijos(llaves, lang, relaciones);
                    bcStatusRequiere = null;
                }
                if (clase.Equals(BE.relReqServProv.requiereServicio))
                {
                    BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    colRequiereServicio = bcRequiereServicio.ObtenerHijos(sllaves, lang, relaciones);
                    bcRequiereServicio = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colServicioPersona != null && colServicioPersona.Count > 0)
                    {
                        item.servicioPersona = (from elemento in colServicioPersona where elemento.ServicioPersonaId == item.ServicioPersonaId select elemento).ToList().FirstOrDefault();
                    }

                    if (colStatusRequiere != null && colStatusRequiere.Count > 0)
                    {
                        item.statusRequiere = (from elemento in colStatusRequiere where elemento.StatusRequiereId == item.StatusRequiereId select elemento).ToList().FirstOrDefault();
                    }

                    if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    {
                        item.requiereServicio = (from elemento in colRequiereServicio where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }

        #endregion

        public List<BE.RequiereServicioProveedores> ListadoRequiereServicioProveedores(string RequiereServicioId, string lang, params Enum[] relaciones)
        {
            List<BE.RequiereServicioProveedores> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.RequiereServicioProveedores rsp with(nolock) where rsp.RequiereServicioId ='{1}'", campos("rsp"), RequiereServicioId);

                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public List<BE.RequiereServicioProveedores> ListadoRequiereServicioProveedores_primero(string RequiereServicioId, string lang)
        {
            List<BE.RequiereServicioProveedores> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT top 1 {0}
                                            FROM dbo.RequiereServicioProveedores rsp with(nolock) where rsp.RequiereServicioId ='{1}'", campos("rsp"), RequiereServicioId);

                DataRow[] dr = conx.ObtenerFilas(sql);
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






        public Boolean ver_Si_se_AdjudicoProveedores(string RequiereServicioId, string lang, params Enum[] relaciones)
        {
            List<BE.RequiereServicioProveedores> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            Boolean bolOk = false;
            int cont = 0;
            try
            {
                string sql = String.Format(@"SELECT count(rsp.RequiereServicioId)
FROM dbo.RequiereServicioProveedores rsp with(nolock)
inner join RequiereServicio rs
on rsp.RequiereServicioId=rs.RequiereServicioId
			  where rsp.RequiereServicioId ='{1}' and rsp.StatusRequiereId=4 ", campos("rsp"), RequiereServicioId);

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


  
        public Boolean ver_Si_Rechazaron_todos_Prov(string RequiereServicioId, string lang, params Enum[] relaciones)
        {
            List<BE.RequiereServicioProveedores> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            Boolean bolOk = false;
            int cont = 0;
            try
            {
                string sql = String.Format(@"(select COUNT(RequiereServicioid) cantidad from requiereservicioproveedores with (nolock) where requiereservicioId='{0}')
UNION
(select COUNT(RequiereServicioid) cantidad from requiereservicioproveedores  with (nolock)
where requiereservicioId='{0}' and StatusRequiereId= 3 )", RequiereServicioId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr.Count() >= 2){
                  
               bolOk = false;
                    

                }           
                else
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



        #region "Listado"
        public List<BE.RequiereServicioProveedores> ObtenerHijos(IEnumerable<string> llaves,IEnumerable<decimal> estados = null, string lang="", long personaId = 0, params Enum[] relaciones)
        {
            List<BE.RequiereServicioProveedores> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string strEstados = "";
                if (estados != null)
                {
                    strEstados = String.Format("and rsp.StatusRequiereId in {0}", this.ConcatenarLlaves(estados));
                }
                string strPersonas = "";
                if (personaId != 0)
                {
                    strPersonas = string.Format("and rsp.ServicioPersonaId in ( select spr1.ServicioPersonaId from dbo.ServicioPersona spr1 with(nolock) where spr1.PersonaId = {0} )",personaId);
                }

                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.RequiereServicioProveedores rsp with(nolock) where rsp.RequiereServicioId in {1} {2} {3}", campos("rsp"), this.ConcatenarLlaves(llaves),strEstados , strPersonas);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public Boolean RegistrarSolicitud(ref BE.RequiereServicioProveedores requiereServicioProveedores)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                bolOk = Actualizar(ref requiereServicioProveedores, true);
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }
                if (requiereServicioProveedores.requiereServicio != null)
                {
                    BC.RequiereServicio bcrequiereServicio = new BC.RequiereServicio(cadenaConexion);                    
                    bcrequiereServicio.dbConexion = conx;                 
                     requiereServicioProveedores.requiereServicio.RequiereServicioId = requiereServicioProveedores.RequiereServicioId;
                        BE.RequiereServicio rs = requiereServicioProveedores.requiereServicio;
                        bolOk = bcrequiereServicio.Actualizar(ref rs, true);
                        if (!bolOk)
                        {
                            throw new Exception("Error al enviar Solicitudes");
                        }
                    requiereServicioProveedores.requiereServicio.RequiereServicioId = rs.RequiereServicioId;
                   
                    bcrequiereServicio = null;
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
        #endregion
        #region "Busqueda"

        #endregion
    }
}
