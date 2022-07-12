using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class ConceptoNotificacion:BCEntidad
    {

        public ConceptoNotificacion() : base()
        {
        }
        public ConceptoNotificacion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<BE.ConceptoNotificacion> CargarBE(DataRow[] dr)
        {
            List<BE.ConceptoNotificacion> lst = new List<BE.ConceptoNotificacion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.ConceptoNotificacion CargarBE(DataRow dr)
        {
            BE.ConceptoNotificacion obj = new BE.ConceptoNotificacion();

            obj.ConceptoNotificacionId = Convert.ToDecimal(dr["ConceptoNotificacionId"].ToString());
            obj.ConceptoNotificacionNombre = Convert.ToString(dr["ConceptoNotificacionNombre"].ToString());
          


            return obj;


        }

        public List<BE.ConceptoNotificacion> ObtenerHijos(IEnumerable<decimal> llaves, string lang = "", params Enum[] relaciones)
        {
            List<BE.ConceptoNotificacion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT *
                                            FROM  ConceptoNotificacion with (nolock) where ConceptoNotificacionId in {0}", this.ConcatenarLlaves(llaves), lang);
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
