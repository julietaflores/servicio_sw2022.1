using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
    public class ServicioPersonaDocumento:BCEntidad
    {
        private string campos(string prefijo = "spd")
        {
            string strCampos = String.Format(@"{0}.ServicioPersonaId, {0}.ServicioPersonaDocId, {0}.ServicioPersonaDocNombre, {0}.ServicioPersonaDocFecha"
                    , prefijo);
            return strCampos;
        }
        public ServicioPersonaDocumento() : base()
        {
        }

        public ServicioPersonaDocumento(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

      

        public List<BE.ServicioPersonaDocumento> CargarBE(DataRow[] dr)
        {
            List<BE.ServicioPersonaDocumento> lst = new List<BE.ServicioPersonaDocumento>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.ServicioPersonaDocumento CargarBE(DataRow dr)
        {
            BE.ServicioPersonaDocumento obj = new BE.ServicioPersonaDocumento();
            obj.ServicioPersonaId = Convert.ToDecimal(dr["ServicioPersonaId"].ToString());
            obj.ServicioPersonaDocId = Convert.ToDecimal(dr["ServicioPersonaDocId"].ToString());
            obj.ServicioPersonaDocNombre = Convert.ToString(dr["ServicioPersonaDocNombre"].ToString());
            obj.ServicioPersonaDocFecha =Convert.ToDateTime(dr["ServicioPersonaDocFecha"].ToString());
         

            return obj;
        }

        public void CargarRelaciones(ref List<BE.ServicioPersonaDocumento> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.RequiereServicio> colRequiereServicio = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relServAsig.requiereServicio))
                {
                    //BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                    //sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    //colRequiereServicio = bcRequiereServicio.ObtenerHijos(sllaves, relaciones);
                    //bcRequiereServicio = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    //if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    //{
                    //    item.requiereServicio = (from elemento in colRequiereServicio where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList().FirstOrDefault();
                    //}
                }
            }
        }
        #endregion

        #region "Listados"
        public List<BE.ServicioPersonaDocumento> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.ServicioPersonaDocumento> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ServicioPersonaDocumento spd with(nolock) where spd.ServicioPersonaId in {1}", campos("spd"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public List<BE.ServicioPersonaDocumento> ListadoServicioPersonaDocumento(decimal ServicioPersonaId, string lang, params Enum[] relaciones)
        {
            List<BE.ServicioPersonaDocumento> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from ServicioPersonaDocumento with(nolock) where ServicioPersonaId={0}", ServicioPersonaId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    // CargarRelaciones(ref obj, lang, relaciones);
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
