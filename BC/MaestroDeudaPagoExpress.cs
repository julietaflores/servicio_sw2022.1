using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
   public class MaestroDeudaPagoExpress:BCEntidad
    {
        private string campos(string prefijo = "m")
        {
            string strCampos = String.Format(@" {0}.MaestroDeudaId,{0}.PersonaId,{0}.TipoDeudaId,{0}.MaestroDeudaImporte,{0}.MaestroDeudaVencimiento,{0}.MaestroDeudaPago,{0}.MaestroDeudaObservacion,{0}.MaestroDeudaMotivo,{0}.MaestroDeudaFechaRegistro,{0}.MaestroDeudaUsuarioMod,{0}.MaestroDeudaFechaPago"
                    , prefijo);
            return strCampos;
        }
        public MaestroDeudaPagoExpress() : base()
        {
        }

        public MaestroDeudaPagoExpress(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.MaestroDeudaPagoExpress BEObj, Boolean isTransaccion = false)
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
                        strSql = "update [dbo].[MaestroDeudaPagoExpress] set PersonaId = @PersonaId,TipoDeudaId = @TipoDeudaId,MaestroDeudaImporte = @MaestroDeudaImporte,MaestroDeudaVencimiento = @MaestroDeudaVencimiento,MaestroDeudaPago = @MaestroDeudaPago,MaestroDeudaObservacion = @MaestroDeudaObservacion,MaestroDeudaMotivo = @MaestroDeudaMotivo,MaestroDeudaFechaRegistro = @MaestroDeudaFechaRegistro,MaestroDeudaUsuarioMod = @MaestroDeudaUsuarioMod,MaestroDeudaFechaPago = @MaestroDeudaFechaPago where MaestroDeudaId = @MaestroDeudaId ";

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
                      //  BEObj.BilleteraDetalleId = System.Convert.ToInt32(conx.ObtenerValor("select isnull(max(BilleteraDetalleId),0) + 1 from dbo.BilleteraDetalle with (nolock);"));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@MaestroDeudaId", BEObj.MaestroDeudaId);
                    conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
                    conx.AsignarParametro("@TipoDeudaId", BEObj.TipoDeudaId);
                    conx.AsignarParametro("@MaestroDeudaImporte", BEObj.MaestroDeudaImporte);
                    conx.AsignarParametro("@MaestroDeudaVencimiento", BEObj.MaestroDeudaVencimiento);
                    conx.AsignarParametro("@MaestroDeudaPago", BEObj.MaestroDeudaPago);
                    conx.AsignarParametro("@MaestroDeudaObservacion", BEObj.MaestroDeudaObservacion);
                    conx.AsignarParametro("@MaestroDeudaMotivo", BEObj.MaestroDeudaMotivo);
                    conx.AsignarParametro("@MaestroDeudaFechaRegistro", BEObj.MaestroDeudaFechaRegistro);
                    conx.AsignarParametro("@MaestroDeudaUsuarioMod", BEObj.MaestroDeudaUsuarioMod);
                    conx.AsignarParametro("@MaestroDeudaFechaPago", BEObj.MaestroDeudaFechaPago);

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
   

        public List<BE.MaestroDeudaPagoExpress> CargarBE(DataRow[] dr)
        {
            List<BE.MaestroDeudaPagoExpress> lst = new List<BE.MaestroDeudaPagoExpress>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.MaestroDeudaPagoExpress CargarBE(DataRow dr)
        {
            BE.MaestroDeudaPagoExpress obj = new BE.MaestroDeudaPagoExpress();
            obj.MaestroDeudaId = (dr["MaestroDeudaId"].ToString());
            obj.PersonaId =Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.TipoDeudaId = Convert.ToDecimal(dr["TipoDeudaId"].ToString());
            obj.MaestroDeudaImporte =Convert.ToDecimal( dr["MaestroDeudaImporte"].ToString());
            obj.MaestroDeudaVencimiento =Convert.ToDateTime( (dr["MaestroDeudaVencimiento"].ToString()));
            obj.MaestroDeudaPago = Convert.ToBoolean((dr["MaestroDeudaPago"].ToString()));
            obj.MaestroDeudaObservacion=(dr["MaestroDeudaObservacion"].ToString());
            obj.MaestroDeudaMotivo= (dr["MaestroDeudaMotivo"].ToString());
            obj.MaestroDeudaFechaRegistro= Convert.ToDateTime(dr["MaestroDeudaFechaRegistro"].ToString());
            obj.MaestroDeudaUsuarioMod= (dr["MaestroDeudaUsuarioMod"].ToString());
            if (dr["MaestroDeudaFechaPago"].ToString() != "")
                obj.MaestroDeudaFechaPago = Convert.ToDateTime(dr["MaestroDeudaFechaPago"].ToString());


            return obj;
        }

        public void CargarRelaciones(ref List<BE.Ciudad> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.Region> colRequiereServicio = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relciudad.region))
                {
                    BC.Region bcRegion = new BC.Region(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.RegionId).Distinct();
                    colRequiereServicio = bcRegion.ObtenerHijos(llaves, relaciones);
                    bcRegion = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    {
                        item.Region = (from elemento in colRequiereServicio where elemento.RegionId == item.RegionId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }
        #endregion

        #region "Listados"
        public List<BE.MaestroDeudaPagoExpress> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.MaestroDeudaPagoExpress> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM  [dbo].[MaestroDeudaPagoExpress] m with(nolock) where m.MaestroDeidaId in {1}", campos("c"), this.ConcatenarLlaves(llaves));
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
        public BE.MaestroDeudaPagoExpress BuscarMaestroDeudaPagoExpressxId(string MaestroDeudaId)
        {
            BE.MaestroDeudaPagoExpress obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT * from MaestroDeudaPagoExpress with(nolock)  where MaestroDeudaId='{0}'", MaestroDeudaId);
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
    }
}
