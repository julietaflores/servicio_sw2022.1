using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class BilleteraConcepto:BCEntidad
    {
          
        private string campos(string prefijo = "bc")
        {
            string strCampos = String.Format(@"{0}.BilleteraConceptoId,{0}.BilleteraConceptoNombre"
                    , prefijo);
            return strCampos;
        }
        public BilleteraConcepto() : base()
        {
        }

        public BilleteraConcepto(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

     /*   public Boolean Actualizar(ref BE.ServAsig BEObj)
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


        public List<BE.BilleteraConcepto> CargarBE(DataRow[] dr)
        {
            List<BE.BilleteraConcepto> lst = new List<BE.BilleteraConcepto>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.BilleteraConcepto CargarBE(DataRow dr)
        {
            BE.BilleteraConcepto obj = new BE.BilleteraConcepto();          
            ///////////////////////////////
            obj.BilleteraConceptoId = Convert.ToDecimal(dr["BilleteraConceptoId"].ToString());
            obj.BilleteraConceptoNombre = dr["BilleteraConceptoNombre"].ToString();

            return obj;
        }

        public void CargarRelaciones(ref List<BE.BilleteraConcepto> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.BilleteraConcepto> colRequiereServicio = null;
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
        public List<BE.BilleteraConcepto> ObtenerHijos(IEnumerable<decimal> llaves,string lang ,params Enum[] relaciones)
        {
            List<BE.BilleteraConcepto> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM [BilleteraConceptoSP] ('{2}')bc where bc.BilleteraConceptoId in {1}", campos("bc"), this.ConcatenarLlaves(llaves),lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                  //  CargarRelaciones(ref obj, relaciones);
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

