using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using BE;


namespace BC
{
     public class EstadoReqServ : BCEntidad
    {
        public EstadoReqServ() : base()
        {
        }
        public EstadoReqServ(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.EstadoReqServ BEObj)
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
                        strSql = "";
                        break;

                }

                if (dbConexion != null)
                    conx = dbConexion;
                else
                    conx.Conectar();
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));
                    conx.CrearComando(strSql);
                ///   conx.AsignarParametro("@StatusServicioId", BEObj.StatusServAsigId);

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
        }


        public List<BE.EstadoReqServ> CargarBE(DataRow[] dr)
        {
            List<BE.EstadoReqServ> lst = new List<BE.EstadoReqServ>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.EstadoReqServ CargarBE(DataRow dr)
        {
            
              BE.EstadoReqServ obj = new BE.EstadoReqServ();
        obj.EstadoReqServId = Convert.ToDecimal(dr["EstadoReqServId"].ToString());
        obj.EstadoReqServNombre = Convert.ToString(dr["EstadoReqServNombre"].ToString());


            return obj;
        }


        public List<BE.EstadoReqServ> ObtenerHijos(IEnumerable<decimal> llaves, string lang, params Enum[] relaciones)
        {
            List<BE.EstadoReqServ> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select EstadoReqServId,EstadoReqServNombre from EstadoReqServSP ('{0}')  where EstadoReqServId in {1} ", lang, this.ConcatenarLlaves(llaves));
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

        #region "Listados"

        #endregion

        #region "Busqueda"

        public BE.EstadoReqServ BuscarEstadoReqServxId(decimal EstadoReqServId)
        {
            BE.EstadoReqServ obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from EstadoReqServ with(nolock) where EstadoReqServId={0}", EstadoReqServId);
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
