using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class StatusRequiere:BCEntidad
    {
        public StatusRequiere() : base()
        {
        }
        public StatusRequiere(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

  

        #region  "DEFINICION DE METODOS DE ABM"

        public List<BE.StatusRequiere> CargarBE(DataRow[] dr)
        {
            List<BE.StatusRequiere> lst = new List<BE.StatusRequiere>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.StatusRequiere CargarBE(DataRow dr)
        {

            BE.StatusRequiere obj = new BE.StatusRequiere();
           
            ///////////////////////////////
            obj.StatusRequiereId = Convert.ToDecimal(dr["StatusRequiereId"].ToString());
            obj.StatusRequiereNombre = dr["StatusRequiereNombre"].ToString();

            return obj;
          
        }

        public List<BE.StatusRequiere> ObtenerHijos(IEnumerable<decimal> llaves, string lang = "", params Enum[] relaciones)
        {
            List<BE.StatusRequiere> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select sre.statusRequiereId, lng.StatusRequiereNombre
                                            from StatusRequiere sre with (nolock) inner join StatusRequiereSP('{0}') lng on
                                            sre.StatusRequiereId = lng.StatusRequiereId where sre.statusRequiereId in {1}", lang, this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //CargarRelaciones(ref obj, lang, relaciones);
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

        public BE.StatusRequiere BuscarStatusRequierexId(decimal StatusRequiereId)
        {
            BE.StatusRequiere obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from [StatusRequiere] with(nolock) where StatusRequiereId={0} ", StatusRequiereId);
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
