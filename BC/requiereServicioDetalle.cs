using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class requiereServicioDetalle:BCEntidad        
    {
        public requiereServicioDetalle() : base()
        {
        }

        public requiereServicioDetalle(string cadConx) : base(cadConx)
        {

        }

        private string campos(string prefijo = "rsd")
        {
            string strCampos = String.Format(@"{0}.RequiereServicioId, {0}.ServicioId, {0}.ServicioDetalleId, 
{0}.ServicioDetalleCantidad, {0}.ServicioDetallePUFecha, {0}.ServicioDetalleDatos"
                    , prefijo);
            return strCampos;
        }

        public ClaseConexion dbConexion { get; set; }

      

        public Boolean Actualizar(ref BE.requiereServicioDetalle BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.RequiereServicioDetalle
                                    SET RequiereServicioId = @requiereservicioid,
	                                    ServicioId=@ServicioId,
										
										ServicioDetalleCantidad=@ServicioDetalleCantidad,
										ServicioDetallePUFecha=@ServicioDetallePUFecha,
										ServicioDetalleDatos=@ServicioDetalleDatos
                                    where  RequiereServicioId = @requiereservicioid and
									ServicioDetalleId=@ServicioDetalleId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO dbo.RequiereServicioDetalle (RequiereServicioId, ServicioId, ServicioDetalleId, ServicioDetalleCantidad, ServicioDetallePUFecha, ServicioDetalleDatos) VALUES  (@RequiereServicioId, @ServicioId, @ServicioDetalleId,@ServicioDetalleCantidad, @ServicioDetallePUFecha, @ServicioDetalleDatos)
";
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
                      /*  BEObj.requiereServicioDetalleId = System.Convert.ToInt32(conx.ObtenerValor
                            (String.Format
                            ("select isnull(max(RequiereServicioDetalleId),0) + 1 from dbo.RequiereServicioDetalle with (nolock) where RequiereServicioId = '{0}'; ", BEObj.requiereServicioId)));*/
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@requiereservicioid", BEObj.requiereServicioId);
                    conx.AsignarParametro("@ServicioId", BEObj.servicioId);
                    conx.AsignarParametro("@ServicioDetalleId", BEObj.servicioDetalleId);
                    conx.AsignarParametro("@ServicioDetalleCantidad", BEObj.servicioDetalleCantidad);
                    conx.AsignarParametro("@ServicioDetallePUFecha", BEObj.servicioDetallePUFecha);
                    conx.AsignarParametro("@ServicioDetalleDatos", BEObj.servicioDetalleDatos);
                   
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



        public List<BE.requiereServicioDetalle> CargarBE(DataRow[] dr)
        {
            List<BE.requiereServicioDetalle> lst = new List<BE.requiereServicioDetalle>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.requiereServicioDetalle CargarBE(DataRow dr)
        {
            BE.requiereServicioDetalle obj = new BE.requiereServicioDetalle();
            obj.requiereServicioId = dr["RequiereServicioId"].ToString();
            obj.servicioId = Convert.ToInt32(dr["servicioId"].ToString());
            obj.servicioDetalleId = Convert.ToDecimal(dr["ServicioDetalleId"].ToString());
            obj.servicioDetalleCantidad = Convert.ToInt32(dr["ServicioDetalleCantidad"].ToString());
            obj.servicioDetallePUFecha = Convert.ToInt32(dr["ServicioDetallePUFecha"].ToString());
            obj.servicioDetalleDatos = dr["ServicioDetalleDatos"].ToString();
         
            return obj;
        }
        public List<BE.requiereServicioDetalle> ObtenerHijos(IEnumerable<string> llaves, params Enum[] relaciones)
        {
            List<BE.requiereServicioDetalle> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.RequiereServicioDetalle rsd with(nolock) where rsd.RequiereServicioId in {1}", campos("rsd"), this.ConcatenarLlaves(llaves));
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

        public List<BE.requiereServicioDetalle> ListadoXrequiereServicioDetalle(string RequiereServicioId, params Enum[] relaciones)
        {
            List<BE.requiereServicioDetalle> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.RequiereServicioDetalle rsd with(nolock) where rsd.RequiereServicioId='{1}'", campos("rsd"),RequiereServicioId);
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
    }
}
