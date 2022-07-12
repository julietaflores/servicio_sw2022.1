using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
    public class Moneda:BCEntidad
    {
        private string campos(string prefijo = "mo")
        {
            string strCampos = String.Format(@"{0}.MonedaId,{0}.MonedaNombre,{0}.PaisId"
                    , prefijo);
            return strCampos;
        }
        public Moneda() : base()
        {
        }

        public Moneda(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        /*public Boolean Actualizar(ref BE.Moneda BEObj)
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


        public List<BE.Moneda> CargarBE(DataRow[] dr)
        {
            List<BE.Moneda> lst = new List<BE.Moneda>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Moneda CargarBE(DataRow dr)
        {
            BE.Moneda obj = new BE.Moneda();
        
            obj.MonedaId = Convert.ToDecimal(dr["MonedaId"].ToString());
            obj.MonedaNombre = dr["MonedaNombre"].ToString();
            obj.PaisId = Convert.ToDecimal(dr["PaisId"].ToString());
            return obj;
        }

        public void CargarRelaciones(ref List<BE.Moneda> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.Moneda> colRequiereServicio = null;
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

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    //if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    //{
                    //    item.requiereServicio = (from elemento in colRequiereServicio where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList().FirstOrDefault();
                    //}
                }
            }
        }
        #endregion

        #region "Listados"
        public List<BE.Moneda> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.Moneda> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Moneda mo with(nolock) where mo.paisId in {1}", campos("mo"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                //   CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public BE.Moneda ObtenerHijos2(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            BE.Moneda obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Moneda mo with(nolock) where mo.paisId in {1}", campos("mo"), this.ConcatenarLlaves(llaves));
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
        #region "Busqueda"

        #endregion
    }
}
