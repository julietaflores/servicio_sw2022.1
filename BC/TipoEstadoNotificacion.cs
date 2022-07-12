using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    class TipoEstadoNotificacion:BCEntidad
    {
        public TipoEstadoNotificacion() : base()
        {
        }
        public TipoEstadoNotificacion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<BE.TipoEstadoNotificacion> CargarBE(DataRow[] dr)
        {
            List<BE.TipoEstadoNotificacion> lst = new List<BE.TipoEstadoNotificacion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.TipoEstadoNotificacion CargarBE(DataRow dr)
        {
            BE.TipoEstadoNotificacion obj = new BE.TipoEstadoNotificacion();

            obj.TipoEstadoId = Convert.ToDecimal(dr["TipoEstadoId"].ToString());
            obj.TipoEstadoNombre = Convert.ToString(dr["TipoEstadoNombre"].ToString());
       

            return obj;


        }

        public List<BE.TipoEstadoNotificacion> ObtenerHijos(IEnumerable<decimal> llaves, string lang = "", params Enum[] relaciones)
        {
            List<BE.TipoEstadoNotificacion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT *
                                            FROM  TipoEstadoNotificacion with (nolock) where TipoEstadoId in {0}", this.ConcatenarLlaves(llaves), lang);
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
    }
}
