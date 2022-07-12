using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class EstadoNotificacion:BCEntidad
    {
        public EstadoNotificacion() : base()
        {
        }
        public EstadoNotificacion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<BE.EstadoNotificacion> CargarBE(DataRow[] dr)
        {
            List<BE.EstadoNotificacion> lst = new List<BE.EstadoNotificacion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.EstadoNotificacion CargarBE(DataRow dr)
        {
            BE.EstadoNotificacion obj = new BE.EstadoNotificacion();

            obj.EstadoNotificacionId = Convert.ToDecimal(dr["EstadoNotificacionId"].ToString());
            obj.EstadoNotificacionNombre = Convert.ToString(dr["EstadoNotificacionNombre"].ToString());
            obj.EstadoNotificacionImagen = Convert.ToString(dr["EstadoNotificacionImagen"].ToString());
            obj.EstadoNotificacionColor =(dr["EstadoNotificacionColor"].ToString());
        

            return obj;


        }

        public List<BE.EstadoNotificacion> ObtenerHijos(IEnumerable<decimal> llaves, string lang="", params Enum[] relaciones)
        {
            List<BE.EstadoNotificacion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT *
                                            FROM  EstadoNotificacion with (nolock) where EstadoNotificacionId in {0}", this.ConcatenarLlaves(llaves), lang);
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
