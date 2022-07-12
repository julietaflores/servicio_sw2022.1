using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BC
{
    public class Region:BCEntidad
    {
        private string campos(string prefijo = "r")
        {
            string strCampos = String.Format(@"{0}.RegionId,{0}.RegionNombre,{0}.PaisId"
                    , prefijo);
            return strCampos;
        }
        public Region() : base()
        {
        }

        public Region(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        /*  public Boolean Actualizar(ref BE.ServAsig BEObj)
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
            }*/


        public List<BE.Region> CargarBE(DataRow[] dr)
        {
            List<BE.Region> lst = new List<BE.Region>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Region CargarBE(DataRow dr)
        {
            BE.Region obj = new BE.Region();
            obj.RegionId = Convert.ToDecimal(dr["RegionId"].ToString());
            obj.RegionNombre = dr["RegionNombre"].ToString();
            obj.PaisId = Convert.ToDecimal(dr["PaisId"].ToString());
                        return obj;
        }

        public void CargarRelaciones(ref List<BE.Region> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.Pais> colPais = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relRegion.pais))
                {
                    BC.Pais bcPais = new BC.Pais(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.PaisId).Distinct();
                    colPais = bcPais.ObtenerHijos(llaves, relaciones);
                    bcPais = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colPais != null && colPais.Count > 0)
                    {
                        item.pais = (from elemento in colPais where elemento.PaisId == item.PaisId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }
        #endregion

        #region "Listados"
        public List<BE.Region> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.Region> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Region r with(nolock) where r.RegionId in {1}", campos("r"), this.ConcatenarLlaves(llaves));
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
        public List<BE.Region> ObtenerRegion(string lang)
        {
            List<BE.Region> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = "RegionSP";


                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdiomaSigla", lang);
                DataTable dt = conx.ObtenerTablaSP(sql);

                ///////////////
                DataRow[] dr = new DataRow[dt.Rows.Count];
                dt.Rows.CopyTo(dr, 0);

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
    }
}
