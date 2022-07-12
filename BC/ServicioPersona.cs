using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;

namespace BC
{
    public class ServicioPersona:BCEntidad
    {
        private string campos(string prefijo = "sep")
        {
            string strCampos = String.Format(@"{0}.ServicioPersonaId, {0}.PersonaId, {0}.ServicioId, {0}.ServicioPersonaURLFoto, {0}.EstadoServicioId
                    , {0}.StatusServicioId, {0}.ServicioPersonaUsuario, {0}.ServicioPersonaFechaHora, {0}.ServicioPersonaGaleriaLast
                    , {0}.ServicioPersonaHorarioLast, {0}.ServicioPersonaNombre, {0}.ServicioPersonaDescripcion, {0}.ServicioPersonaReqDelivery
                    , {0}.MonedaId, {0}.PersonaDireccionId, {0}.ServicioPersonaEnDomicilio, {0}.ServicioPersonaEnOficina"
                    , prefijo );
            return strCampos; 
        }
        public ServicioPersona() : base()
        {
        }

        public ServicioPersona(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.ServAsig BEObj)
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

                        strSql = "";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = "insert ServAsig values(@ServAsigId, @ProveedorId, @ServAsigFHUbicacion, @ServAsigFHEstimadaLlegada, @ServAsigFHInicio, @ServAsigFHFin, @ServAsigFHPago,@ServAsigCostoTotal, @StatusServAsigId,@RequiereServicioId,@ServAsigPagaCliente)";
                        break;

                }

                if (dbConexion != null)
                    conx = dbConexion;
                else
                    conx.Conectar();
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));
                    conx.CrearComando(strSql);
                conx.AsignarParametro("@ServAsigId", BEObj.ServAsigId);
                conx.AsignarParametro("@ProveedorId", BEObj.ProveedorId);
                conx.AsignarParametro("@ServAsigFHUbicacion", BEObj.ServAsigFHUbicacion);
                conx.AsignarParametro("@ServAsigFHEstimadaLlegada", BEObj.ServAsigFHEstimadaLlegada);
                conx.AsignarParametro("@ServAsigFHInicio", BEObj.ServAsigFHInicio);
                conx.AsignarParametro("@ServAsigFHFin", BEObj.ServAsigFHFin);
                conx.AsignarParametro("@ServAsigFHPago", BEObj.ServAsigFHPago);
                conx.AsignarParametro("@ServAsigCostoTotal", BEObj.ServAsigCostoTotal);
                conx.AsignarParametro("@StatusServAsigId", BEObj.StatusServAsigId);
                conx.AsignarParametro("@RequiereServicioId", BEObj.RequiereServicioId);
                conx.AsignarParametro("@ServAsigPagaCliente", BEObj.ServAsigPagaCliente);
              

                conx.EjecutarComando();
                if (dbConexion == null)
                    conx.Desconectar();
                bolOk = true;
            }
            catch (Exception ex)
            {
                if (dbConexion == null)
                {
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }

      
        public List<BE.ServicioPersona> CargarBE(DataRow[] dr)
        {
            List<BE.ServicioPersona> lst = new List<BE.ServicioPersona>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.ServicioPersona CargarBE(DataRow dr)
        {
           BE.ServicioPersona obj = new BE.ServicioPersona();
            BE.RankingProveedor objr = new BE.RankingProveedor();
            obj.ServicioPersonaId = Convert.ToDecimal(dr["ServicioPersonaId"].ToString());
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            obj.ServicioPersonaURLFoto = dr["ServicioPersonaURLFoto"].ToString();
            obj.EstadoServicioId = Convert.ToDecimal(dr["EstadoServicioId"].ToString());
            obj.StatusServicioId = Convert.ToDecimal(dr["StatusServicioId"].ToString());
            obj.ServicioPersonaUsuario = dr["ServicioPersonaUsuario"].ToString();
            obj.ServicioPersonaFechaHora = Convert.ToDateTime(dr["ServicioPersonaFechaHora"].ToString());
            obj.ServicioPersonaGaleriaLast = Convert.ToDecimal(dr["ServicioPersonaGaleriaLast"].ToString());
            obj.ServicioPersonaHorarioLast = Convert.ToByte(dr["ServicioPersonaHorarioLast"].ToString());
            obj.ServicioPersonaNombre = dr["ServicioPersonaNombre"].ToString();
            obj.ServicioPersonaDescripcion = dr["ServicioPersonaDescripcion"].ToString();
            obj.ServicioPersonaReqDelivery = Convert.ToBoolean(dr["ServicioPersonaReqDelivery"].ToString());
            obj.MonedaId = Convert.ToDecimal(dr["MonedaId"].ToString());
            if (!DBNull.Value.Equals(dr["PersonaDireccionId"]))
            {
                obj.PersonaDireccionId = Convert.ToDecimal(dr["PersonaDireccionId"].ToString());               
            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaEnDomicilio"]))
            {
                obj.ServicioPersonaEnDomicilio = Convert.ToBoolean(dr["ServicioPersonaEnDomicilio"].ToString());
            }
            if (!DBNull.Value.Equals(dr["ServicioPersonaEnOficina"]))
            {
                obj.ServicioPersonaEnOficina = Convert.ToBoolean(dr["ServicioPersonaEnOficina"].ToString());
            }
            
           /* objr.NroTrabajos= Convert.ToInt32(dr["NroTrabajos"].ToString());
            objr.Ranking = Convert.ToDecimal(dr["Ranking"].ToString());
            obj.rankingProveedor = objr*/
            return obj;
        }

        public void CargarRelaciones(ref List<BE.ServicioPersona> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<decimal> llavesP;
            IEnumerable<string> sllaves;
            List<BE.ServicioPersonaDocumento> colServicioPersonaDocumento = null;
            List<BE.PersonaDireccion> colServicioPersonaDireccion = null;
            List<BE.RankingProveedor> colRankingProveedor = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relServicioPersona.servicioPersonaDocumento))
                {
                    BC.ServicioPersonaDocumento bcServicioPersonaDocumento = new BC.ServicioPersonaDocumento(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.ServicioPersonaId).Distinct();
                    colServicioPersonaDocumento = bcServicioPersonaDocumento.ObtenerHijos(llaves, relaciones);
                    bcServicioPersonaDocumento = null;
                }
                if (clase.Equals(BE.relServicioPersona.personaDireccion))
                {
                    BC.PersonaDireccion bcServiciopersonaDireccion = new BC.PersonaDireccion(cadenaConexion);
                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.PersonaDireccionId)).Distinct();
                    llavesP = (from elemento in colObj select Convert.ToDecimal(elemento.PersonaId)).Distinct();
                    colServicioPersonaDireccion = bcServiciopersonaDireccion.ObtenerHijos(llaves,llavesP, relaciones);
                    bcServiciopersonaDireccion = null;
                }
                if (clase.Equals(BE.relServicioPersona.rankingProveedor))
                {
                    BC.RankingProveedor bcRankingProveedor = new BC.RankingProveedor(cadenaConexion);
                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.ServicioPersonaId)).Distinct();

                    colRankingProveedor = bcRankingProveedor.ObtenerHijos(llaves, relaciones);
                    bcRankingProveedor = null;
                }


            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {                    

                    if (colServicioPersonaDocumento != null && colServicioPersonaDocumento.Count > 0)
                   {
                        item.servicioPersonaDocumento = (from elemento in colServicioPersonaDocumento where elemento.ServicioPersonaId == item.ServicioPersonaId select elemento).ToList();
                   }
                    if (colServicioPersonaDireccion != null && colServicioPersonaDireccion.Count > 0)
                    {
                        item.personaDireccion = (from elemento in colServicioPersonaDireccion where elemento.PersonaDireccionId == item.PersonaDireccionId select elemento).FirstOrDefault();
                    }
                    if (colRankingProveedor != null && colRankingProveedor.Count > 0)
                    {
                        item.rankingProveedor = (from elemento in colRankingProveedor  select elemento).FirstOrDefault();
                    }

                }
            }
        }
        public void CargarRelaciones(ref BE.ServicioPersona obj, params Enum[] relaciones)
        {
            if (relaciones == null || obj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.Persona> colRequiereServicio = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relServAsig.requiereServicio))
                {
                    //BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                    //sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    //colRequiereServicio = bcRequiereServicio.ObtenerHijos(sllaves, relaciones);
                    //bcRequiereServicio = null;
                }

            }

        }       
#endregion

        #region "Listados"
        public List<BE.ServicioPersona> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.ServicioPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {                
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ServicioPersona spe with(nolock) where spe.ServicioPersonaId in {1}", campos("spe"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<BE.ServicioPersona> ObtenerHijos2(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.ServicioPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ServicioPersona spe with(nolock) where spe.PersonaId in {1}", campos("spe"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public List<BE.ServicioPersona> ListadoServicioPersona(decimal PersonaId, params Enum[] relaciones)
        {
            List<BE.ServicioPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from ServicioPersona with(nolock) where ServicioPersonaId={0}", PersonaId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                   CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        #endregion


        #region "Busqueda"
        public BE.ServicioPersona BuscarxId(decimal serPerId, params Enum[] relaciones)

        {
            BE.ServicioPersona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ServicioPersona spe with(nolock) where spe.ServicioPersonaId={1}", campos("spe"), serPerId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<BE.ServicioPersona> VerServicioPersona(string servicioId,params Enum[] relaciones)

        {
          List<BE.ServicioPersona> obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select {0},dbo.obtenernrotrabajos(ServicioPersonaId)NroTrabajos, dbo.obtenerranking(ServicioPersonaId) Ranking
FROM dbo.ServicioPersona sep with(nolock)
inner join dbo.PersonaDireccion pdr with(nolock) on sep.PersonaDireccionId = pdr.PersonaDireccionId 
and sep.PersonaId = pdr.PersonaId
and StatusServicioId<>2
AND sep.servicioid='{1}'", campos("sep"), servicioId);
                DataRow[]   dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    BC.RankingProveedor bcRankingProveedor = new BC.RankingProveedor(cadenaConexion);
                    bcRankingProveedor.CargarBE(ref obj, dr);
                    CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        #endregion
    }
}
