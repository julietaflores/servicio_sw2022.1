using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
    public class LogMovil : BCEntidad
    {
        private string campos(string prefijo = "log")
        {
            string strCampos = String.Format(@" {0}.LogId,{0}.LogFechaRegistro,{0}.LogPersonaId,{0}.LogMetodo,{0}.LogInformacionTelefono,{0}.LogVersion,{0}.LogDescripcion,{0}.LogObservacion1,{0}.LogObservacion2,{0}.LogObservacion3"
                    , prefijo);
            return strCampos;
        }
        public LogMovil() : base()
        {
        }

        public LogMovil(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"
        public Boolean Actualizar(ref BE.LogMovil BEObj, Boolean isTransaccion = false)
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
                        strSql = @"";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into LogMovil (LogId,LogFechaRegistro,LogPersonaId,LogMetodo,LogInformacionTelefono,
LogVersion,LogDescripcion,LogObservacion1,LogObservacion2,LogObservacion3)values
(@LogId,@LogFechaRegistro,@LogPersonaId,@LogMetodo,@LogInformacionTelefono,
@LogVersion,@LogDescripcion,@LogObservacion1,@LogObservacion2,@LogObservacion3)";
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
                        BEObj.LogId = System.Convert.ToInt32(conx.ObtenerValor("select isnull(max(LogId),0) + 1 from dbo.LogMovil with (nolock);"));

                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@LogId", BEObj.LogId);
                    conx.AsignarParametro("@LogFechaRegistro", BEObj.LogFechaRegistro);
                    conx.AsignarParametro("@LogPersonaId", BEObj.LogPersonaId);
                    conx.AsignarParametro("@LogMetodo", BEObj.LogMetodo);
                    conx.AsignarParametro("@LogInformacionTelefono", BEObj.LogInformacionTelefono);
                    conx.AsignarParametro("@LogVersion", BEObj.LogVersion);
                    conx.AsignarParametro("@LogDescripcion", BEObj.LogDescripcion);
                    conx.AsignarParametro("@LogObservacion1", BEObj.LogObservacion1);
                    conx.AsignarParametro("@LogObservacion2", BEObj.LogObservacion2);
                    conx.AsignarParametro("@LogObservacion3", BEObj.LogObservacion3);
                   
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

        public List<BE.LogMovil> CargarBE(DataRow[] dr)
        {
            List<BE.LogMovil> lst = new List<BE.LogMovil>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.LogMovil CargarBE(DataRow dr)
        {
            BE.LogMovil obj = new BE.LogMovil();
            obj.LogId = Convert.ToDecimal(dr["LogId"].ToString());
            obj.LogFechaRegistro = Convert.ToDateTime(dr["LogFechaRegistro"].ToString());
            obj.LogPersonaId= Convert.ToDecimal(dr["LogPersonaId"].ToString());
            obj.LogMetodo = (dr["LogMetodo"].ToString());
            obj.LogInformacionTelefono= (dr["LogInformacionTelefono"].ToString());
            obj.LogVersion = (dr["LogVersion"].ToString());
            obj.LogDescripcion = (dr["LogDescripcion"].ToString());
            obj.LogObservacion1 = (dr["LogObservacion1"].ToString());
            obj.LogObservacion2 = (dr["LogObservacion2"].ToString());
            obj.LogObservacion3 = (dr["LogObservacion3"].ToString());

            return obj;
        }


        #endregion

        #region "Listados"
        public List<BE.LogMovil> ObtenerHijos(IEnumerable<decimal> llaves, string lang, params Enum[] relaciones)
        {
            List<BE.LogMovil> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Log tl with(nolock) where tl.logId in {1}", campos("tl"), this.ConcatenarLlaves(llaves), lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
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
