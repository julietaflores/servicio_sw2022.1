using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class requiereServicioDetalleWeb : BCEntidad
    {
        public requiereServicioDetalleWeb() : base()
        {
        }

        public requiereServicioDetalleWeb(string cadConx) : base(cadConx)
        {

        }

        private string campos(string prefijo = "rsdw")
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



        public List<BE.requiereServicioDetalleWeb> CargarBE(DataRow[] dr)
        {
            List<BE.requiereServicioDetalleWeb> lst = new List<BE.requiereServicioDetalleWeb>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.requiereServicioDetalleWeb CargarBE(DataRow dr)
        {
            BE.requiereServicioDetalleWeb obj = new BE.requiereServicioDetalleWeb();
            obj.requiereServicioId = dr["RequiereServicioId"].ToString();
            obj.servicioId = Convert.ToInt32(dr["servicioId"].ToString());
            obj.servicioDetalleId = Convert.ToDecimal(dr["ServicioDetalleId"].ToString());
            obj.servicioDetalleDescripcion = (dr["servicioDetalleDescripcion"].ToString());
            obj.servicioDetalleCantidad = Convert.ToInt32(dr["ServicioDetalleCantidad"].ToString());
            obj.servicioDetallePUFecha = Convert.ToInt32(dr["ServicioDetallePUFecha"].ToString());
            obj.servicioDetalleDatos = dr["ServicioDetalleDatos"].ToString();

            return obj;
        }
        public List<BE.requiereServicioDetalleWeb> ObtenerHijos(IEnumerable<string> llaves, params Enum[] relaciones)
        {
            List<BE.requiereServicioDetalleWeb> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select rsd.RequiereServicioId,rsd.ServicioId,rsd.ServicioDetalleId,sd.ServicioDetalleDescripcion,
rsd.ServicioDetalleCantidad,rsd.ServicioDetallePUFecha,rsd.ServicioDetalleDatos from RequiereServicioDetalle rsd
inner join ServicioDetalle sd
on rsd.ServicioId=sd.ServicioId and rsd.ServicioDetalleId=sd.ServicioDetalleId
where requiereServicioId in {0}", this.ConcatenarLlaves(llaves));
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
