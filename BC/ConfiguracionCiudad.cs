using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class ConfiguracionCiudad:BCEntidad
    {
        private string campos(string prefijo = "cfc")
        {
            string strCampos = String.Format(@"{0}.ConfiguracionCiudadId,{0}.CiudadId,{0}.BilleteraIdSeguro,{0}.BilleteraIdServiceWeb,{0}.ConfiguracionCiudadValorSeguro,{0}.ConfiguracionCiudadValorBroker"
                    , prefijo);
            return strCampos;
        }
        public ConfiguracionCiudad() : base()
        {
        }

        public ConfiguracionCiudad(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

       /* public Boolean Actualizar(ref BE.ServAsig BEObj)
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


        public List<BE.ConfiguracionCiudad > CargarBE(DataRow[] dr)
        {
            List<BE.ConfiguracionCiudad> lst = new List<BE.ConfiguracionCiudad>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.ConfiguracionCiudad CargarBE(DataRow dr)
        {
            BE.ConfiguracionCiudad obj = new BE.ConfiguracionCiudad();
            obj.ConfiguracionCiudadId = Convert.ToDecimal(dr["ConfiguracionCiudadId"].ToString());
            if(dr["CiudadId"].ToString()!="")
            obj.CiudadId = Convert.ToDecimal(dr["CiudadId"].ToString());
            if (dr["BilleteraIdSeguro"].ToString() != "")
                obj.BilleteraIdSeguro = Convert.ToDecimal(dr["BilleteraIdSeguro"].ToString());
            if (dr["BilleteraIdServiceWeb"].ToString() != "")
                obj.BilleteraIdServiceWeb = Convert.ToDecimal(dr["BilleteraIdServiceWeb"].ToString());
            if (dr["ConfiguracionCiudadValorSeguro"].ToString() != "")
                obj.ConfiguracionCiudadValorSeguro = Convert.ToDecimal(dr["ConfiguracionCiudadValorSeguro"].ToString());
            if (dr["ConfiguracionCiudadValorBroker"].ToString() != "")
                obj.ConfiguracionCiudadValorBroker = Convert.ToDecimal(dr["ConfiguracionCiudadValorBroker"].ToString());

            return obj;
        }

        public void CargarRelaciones(ref List<BE.ConfiguracionCiudad> colObj,string lang,params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            //IEnumerable<decimal> llaves;
            //IEnumerable<string> sllaves;
            //List<BE.Servicio> colservicio = null;
            //foreach (Enum clase in relaciones)
            //{
            //    if (clase.Equals(BE.relseguro.servicio))
            //    {
            //        BC.Servicio bcservicio= new BC.Servicio(cadenaConexion);
            //        llaves = (from elemento in colObj select elemento.CiudadId).Distinct();
            //        colservicio = bcservicio.ObtenerHijos(llaves,lang, relaciones);
            //        bcservicio = null;
            //    }

            //}

            //if (relaciones.GetLength(0) > 0)
            //{
            //    foreach (var item in colObj)
            //    {

            //        if (colservicio != null && colservicio.Count > 0)
            //        {
            //            item.servicio = (from elemento in colservicio where elemento.ServicioId == item.CiudadId select elemento).ToList().FirstOrDefault();
            //        }
            //    }
            //}
        }
        #endregion

        #region "Listados"
        public List<BE.ConfiguracionCiudad> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.ConfiguracionCiudad> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ConfiguracionCiudad cfc with(nolock) where cfc.CiudadId in {1}", campos("cfc"), this.ConcatenarLlaves(llaves));
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
        public List<BE.ConfiguracionCiudad> ListadoSeguro(decimal ciudadId,string lang, params Enum[] relaciones)
        {
            List<BE.ConfiguracionCiudad> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select {0} 
                                            from dbo.ConfiguracionCiudad cfc with (nolock) where cfc.CiudadId = {1}", campos("cfc"), ciudadId);
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
       
        #endregion
        #region "Busqueda"

        #endregion
    }
}
