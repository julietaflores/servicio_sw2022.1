using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
    public class BilleteraDetalleAnulacion:BCEntidad
    {
        private string campos(string prefijo = "m")
        {
            string strCampos = String.Format(@" {0}.MaestroDeudaId,{0}.PersonaId,{0}.TipoDeudaId,{0}.MaestroDeudaImporte,{0}.MaestroDeudaVencimiento,{0}.MaestroDeudaPago,{0}.MaestroDeudaObservacion,{0}.MaestroDeudaMotivo,{0}.MaestroDeudaFechaRegistro,{0}.MaestroDeudaUsuarioMod,{0}.MaestroDeudaFechaPago"
                    , prefijo);
            return strCampos;
        }
        public BilleteraDetalleAnulacion() : base()
        {
        }

        public BilleteraDetalleAnulacion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"


        public Boolean Actualizar(ref BE.BilleteraDetalleAnulacion BEObj, Boolean isTransaccion = false)
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
                        strSql = @"update  ServAsig 
set ServAsigFHUbicacion=@ServAsigFHUbicacion,
ServAsigFHEstimadaLlegada=@ServAsigFHEstimadaLlegada,
ServAsigFHInicio=@ServAsigFHInicio,
ServAsigFHFin=@ServAsigFHFin,
ServAsigFHPago=@ServAsigFHPago,
ServAsigCostoTotal=@ServAsigCostoTotal,
StatusServAsigId=@StatusServAsigId,
RequiereServicioId=@RequiereServicioId,
ServAsigPagaCliente=@ServAsigPagaCliente
where ServAsigId=@ServAsigId and ProveedorId=@ProveedorId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into [dbo].[BilleteraDetalleAnulacion]
(BilleteraDetalleAnulacionId,BilleteraDetalleAnulaciondeuda_id, BilleteraDetalleAnulacionReferencia, BilleteraDetalleAnulacionFecha)
values(@BilleteraDetalleAnulacionId,@BilleteraDetalleAnulaciondeuda_id, @BilleteraDetalleAnulacionReferencia,@BilleteraDetalleAnulacionFecha)";
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
                        BEObj.BilleteraDetalleAnulacionId = System.Convert.ToDecimal(conx.ObtenerValor("select isnull(max(BilleteraDetalleAnulacionId),0) + 1 from dbo.BilleteraDetalleAnulacion with (nolock);"));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@BilleteraDetalleAnulacionId", BEObj.BilleteraDetalleAnulacionId);
                    conx.AsignarParametro("@BilleteraDetalleAnulaciondeuda_id", BEObj.BilleteraDetalleAnulaciondeuda_id);
                    conx.AsignarParametro("@BilleteraDetalleAnulacionReferencia", BEObj.BilleteraDetalleAnulacionReferencia);
                    conx.AsignarParametro("@BilleteraDetalleAnulacionFecha", BEObj.BilleteraDetalleAnulacionFecha);
               

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


        public List<BE.BilleteraDetalleAnulacion> CargarBE(DataRow[] dr)
        {
            List<BE.BilleteraDetalleAnulacion> lst = new List<BE.BilleteraDetalleAnulacion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.BilleteraDetalleAnulacion CargarBE(DataRow dr)
        {
            BE.BilleteraDetalleAnulacion obj = new BE.BilleteraDetalleAnulacion();
            obj.BilleteraDetalleAnulaciondeuda_id = (dr["BilleteraDetalleAnulaciondeuda_id"].ToString());
            obj.BilleteraDetalleAnulacionReferencia =(dr["BilleteraDetalleAnulacionReferencia"].ToString());
            obj.BilleteraDetalleAnulacionFecha = Convert.ToDateTime(dr["BilleteraDetalleAnulacionFecha"].ToString());
           

            return obj;
        }

     
        #endregion

        #region "Listados"
    
        #endregion
        #region "Busqueda"
     

        #endregion
    }
}

