using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
   public class TermCondPol:BCEntidad
    {
        public TermCondPol() : base()
        {
        }
        public TermCondPol(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        private string campos(string prefijo = "tcp")
        {
            string strCampos = String.Format(@"{0}.PaisId,{0}.TermCond,{0}.PoliticasSeg"
                    , prefijo);
            return strCampos;
        }
        public List<BE.TermCondPol> CargarBE(DataRow[] dr)
        {
            List<BE.TermCondPol> lst = new List<BE.TermCondPol>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.TermCondPol CargarBE(DataRow dr)
        {

            BE.TermCondPol obj = new BE.TermCondPol();
            obj.PaisId = Convert.ToDecimal(dr["PaisId"].ToString());
            obj.TermCond = Convert.ToString(dr["TermCond"].ToString());
            obj.PoliticasSeg = Convert.ToString(dr["PoliticasSeg"].ToString());
            obj.MensajeSeguro = Convert.ToString(dr["MensajeSeguro"].ToString());


            return obj;
        }
        #region "Busqueda"

        public BE.TermCondPol BuscarTermCondPol(decimal PaisId)
        {
            BE.TermCondPol obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from TermCondPol with(nolock) where PaisId={0}", PaisId);
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
