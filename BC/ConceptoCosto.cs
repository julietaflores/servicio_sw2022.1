using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BC
{
   public class ConceptoCosto : BCEntidad
    {
        private string campos(string prefijo = "cc")
        {
            string strCampos = String.Format(@"
{0}.ConceptoCostoId,
{0}.ConceptoCostoNombre"
                    , prefijo);
            return strCampos;
        }
        public ConceptoCosto() : base()
        {
        }
        public ConceptoCosto(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        public List<BE.ConceptoCosto> CargarBE(DataRow[] dr)
        {
            List<BE.ConceptoCosto> lst = new List<BE.ConceptoCosto>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.ConceptoCosto CargarBE(DataRow dr)
        {
            BE.ConceptoCosto obj = new BE.ConceptoCosto();
            obj.ConceptoCostoId = Convert.ToDecimal(dr["ConceptoCostoId"].ToString());
            obj.ConceptoCostoNombre = Convert.ToString(dr["ConceptoCostoNombre"].ToString());

            return obj;
                }

        #region "Listados"
        public List<BE.ConceptoCosto> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.ConceptoCosto> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ConceptoCosto cc with(nolock) where cc.ConceptoCostoId in {1}", campos("cc"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                  //  CargarRelaciones(ref obj, relaciones);
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

        public BE.ConceptoCosto BuscarConceptoCostoxId(decimal ConceptoCostoId)
        {
            BE.ConceptoCosto obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from [dbo].[ConceptoCosto] with(nolock) where ConceptoCostoId={0}", ConceptoCostoId);
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
