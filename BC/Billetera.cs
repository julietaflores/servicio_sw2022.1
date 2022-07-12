using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
    public class Billetera:BCEntidad
    {
        private string campos(string prefijo = "b")
        {
            string strCampos = String.Format(@"{0}.BilleteraId,{0}.MonedaId,{0}.BilleteraNroCuenta,{0}.BilleteraSaldo,{0}.PersonaBilleteraId,{0}.BilleteraFechaCreacion
"
                    , prefijo);
            return strCampos;
        }
        public Billetera() : base()
        {
        }

        public Billetera(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"


        public Boolean Actualizar(ref BE.Billetera BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.Billetera
                                    SET
                                    MonedaId=@MonedaId,
                                    BilleteraNroCuenta=@BilleteraNroCuenta,
                                    BilleteraSaldo=@BilleteraSaldo,
                                    PersonaBilleteraId=@PersonaBilleteraId,
                                     BilleteraFechaCreacion=@BilleteraFechaCreacion
                                    where  BilleteraId = @BilleteraId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into Billetera(MonedaId,BilleteraNroCuenta,BilleteraSaldo,PersonaBilleteraId,BilleteraFechaCreacion)
                                    values(@MonedaId,@BilleteraNroCuenta,@BilleteraSaldo,@PersonaBilleteraId,@BilleteraFechaCreacion)";
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
                        BEObj.BilleteraId = System.Convert.ToDecimal(conx.ObtenerValor("select isnull(max(BilleteraId),0) + 1 from dbo.Billetera with (nolock);"));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@BilleteraId", BEObj.BilleteraId);
                    conx.AsignarParametro("@MonedaId", BEObj.MonedaId);
                    conx.AsignarParametro("@BilleteraNroCuenta", BEObj.BilleteraNroCuenta);
                    conx.AsignarParametro("@BilleteraSaldo", BEObj.BilleteraSaldo);
                    conx.AsignarParametro("@PersonaBilleteraId", BEObj.PersonaBilleteraId);
                    conx.AsignarParametro("@BilleteraFechaCreacion", BEObj.BilleteraFechaCreacion);
                
                conx.EjecutarComando();

                if (!isTransaccion)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }

                bolOk = true;
                }
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


        public List<BE.Billetera> CargarBE(DataRow[] dr)
        {
            List<BE.Billetera> lst = new List<BE.Billetera>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Billetera CargarBE(DataRow dr)
        {
            BE.Billetera obj = new BE.Billetera();
            obj.BilleteraId = Convert.ToDecimal(dr["BilleteraId"].ToString());
            obj.MonedaId = Convert.ToDecimal(dr["MonedaId"].ToString());
            obj.BilleteraNroCuenta = dr["BilleteraNroCuenta"].ToString();
            obj.BilleteraSaldo = Convert.ToDecimal(dr["BilleteraSaldo"].ToString());
            obj.PersonaBilleteraId = Convert.ToDecimal(dr["PersonaBilleteraId"].ToString());
            obj.BilleteraFechaCreacion = Convert.ToDateTime(dr["BilleteraFechaCreacion"].ToString());

          
            return obj;

         
        }

        public void CargarRelaciones(ref List<BE.Billetera> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.Moneda> colMoneda = null;
            List<BE.BilleteraDetalle> colBilleteraDetalle = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relBilletera.moneda))
                {
                    BC.Moneda bcMoneda = new BC.Moneda(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.MonedaId).Distinct();
                    colMoneda = bcMoneda.ObtenerHijos(llaves, relaciones);
                    bcMoneda = null;
                }
                if (clase.Equals(BE.relBilletera.billeteraDetalle))
                {
                    BC.BilleteraDetalle bcBilleteraDetalle = new BC.BilleteraDetalle(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.MonedaId).Distinct();
                    colBilleteraDetalle = bcBilleteraDetalle.ObtenerHijos(llaves, relaciones);
                    bcBilleteraDetalle = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                   if (colMoneda != null && colMoneda.Count > 0)
                    {
                        item.moneda = (from elemento in colMoneda where elemento.MonedaId == item.MonedaId select elemento).ToList().FirstOrDefault();
                    }

                    if (colBilleteraDetalle != null && colBilleteraDetalle.Count > 0)
                    {
                        item.billeteraDetalles = (from elemento in colBilleteraDetalle where elemento.BilleteraId == item.BilleteraId select elemento).ToList();
                    }
                }
            }
        }
        #endregion

        #region "Listados"
        public List<BE.Billetera> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.Billetera> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Billetera b with(nolock) where b.BilleteraId in {1}", campos("persona"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    // CargarRelaciones(ref obj, relaciones);
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
        public List<BE.Billetera> ListadoBilletera(long personaId, params Enum[] relaciones)
        {
            List<BE.Billetera> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT bil.BilleteraId, bil.MonedaId, bil.BilleteraNroCuenta, bil.BilleteraSaldo, bil.PersonaBilleteraId, bil.BilleteraFechaCreacion
                                          , mon.MonedaNombre , mon.PaisId 
                                            FROM dbo.Billetera bil with(nolock) inner join dbo.Moneda mon with(nolock) on bil.MonedaId = mon.MonedaId 
                                            where bil.PersonaBilleteraId = {0} ", personaId);
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
        public Boolean RegistrarBilletera(ref BE.Billetera billetera)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                bolOk = Actualizar(ref billetera, true);
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
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


        public BE.Billetera BuscarBilleteraxBilleteraId(decimal BilleteraId)
        {
            BE.Billetera obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT * from Billetera with(nolock)  where BilleteraId={0}", BilleteraId);
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

        public BE.RendimientoFinanciero getRendimientoFinanciero(decimal personaId, DateTime fecha)
        {
            BE.RendimientoFinanciero obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "dbo.ObtenerRendimientoFinanciero";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@idPersona", personaId);
                conx.AsignarParametro("@fecha", fecha.ToString("yyyyMMdd"));
                DataTable dr = conx.ObtenerTablaSP(sql);
                obj = toRendimientoFinanciero( dr.Select().FirstOrDefault());
                conx.Desconectar(); 
            }
            catch (Exception ex)
            {
                conx.Desconectar();
                throw ex;
            }
            return obj;
        }

        public BE.RendimientoOperativo getRendimientoOperativo(decimal personaId, DateTime fecha)
        {
            BE.RendimientoOperativo obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "dbo.ObtenerRendimientoOperativo";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@idPersona", personaId);
                conx.AsignarParametro("@fecha", fecha.ToString("yyyyMMdd"));
                DataTable dr = conx.ObtenerTablaSP(sql);
                obj = toRendimientoOperativo(dr.Select().FirstOrDefault());
                conx.Desconectar();
            }
            catch (Exception ex)
            {
                conx.Desconectar();
                throw ex;
            }
            return obj;
        }

        private List<BE.RendimientoFinanciero> toRendimientoFinanciero( DataRow [] dr)
        {
            List<BE.RendimientoFinanciero> lst = new List<BE.RendimientoFinanciero>();
            foreach (var item in dr)
            {
                lst.Add ( toRendimientoFinanciero(item));
            }
            return lst;
        }

        private BE.RendimientoFinanciero toRendimientoFinanciero(DataRow dr)
        {
            BE.RendimientoFinanciero obj = new BE.RendimientoFinanciero();
            obj.IndiceMesActual = Convert.ToInt32 (dr["IndiceMesActual"].ToString());
            obj.IndiceMesAnterior = Convert.ToInt32(dr["IndiceMesAnterior"].ToString());
            obj.IndiceYearActual = Convert.ToInt32(dr["IndiceYearActual"].ToString());

            obj.MesActual = Convert.ToDecimal(dr["MesActual"].ToString());
            obj.MesAnterior = Convert.ToDecimal(dr["MesAnterior"].ToString());
            obj.YearActual = Convert.ToDecimal(dr["YearActual"].ToString());
            return obj;
        }

        private List<BE.RendimientoOperativo> toRendimientoOperativo(DataRow[] dr)
        {
            List<BE.RendimientoOperativo> lst = new List<BE.RendimientoOperativo>();
            foreach (var item in dr)
            {
                lst.Add(toRendimientoOperativo(item));
            }
            return lst;
        }

        private BE.RendimientoOperativo toRendimientoOperativo(DataRow dr)
        {
            BE.RendimientoOperativo obj = new BE.RendimientoOperativo();
            obj.IndiceMesActual = Convert.ToInt32(dr["IndiceMesActual"].ToString());
            obj.IndiceMesAnterior = Convert.ToInt32(dr["IndiceMesAnterior"].ToString());
            obj.IndiceYearActual = Convert.ToInt32(dr["IndiceYearActual"].ToString());

            obj.MesActual = Convert.ToInt64(dr["MesActual"].ToString());
            obj.MesAnterior = Convert.ToInt64(dr["MesAnterior"].ToString());
            obj.YearActual = Convert.ToInt64(dr["YearActual"].ToString());
            return obj;
        }
    }
}
