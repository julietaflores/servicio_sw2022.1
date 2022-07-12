using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class TipoPost : BCEntidad
    {

        private string campos(string prefijo = "c")
        {
            string strCampos = String.Format(@"{0}.TipoPostId,{0}.TipoPostNombre"
                    , prefijo);
            return strCampos;
        }
        public TipoPost() : base()
        {
        }

        public TipoPost(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        public List<BE.TipoPost> CargarBE(DataRow[] dr)
        {
            List<BE.TipoPost> lst = new List<BE.TipoPost>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.TipoPost CargarBE(DataRow dr)
        {
            BE.TipoPost obj = new BE.TipoPost();
            obj.TipoPostId = Convert.ToDecimal(dr["TipoPostId"].ToString());
            obj.TipoPostNombre = dr["TipoPostNombre"].ToString();




            return obj;
        }

        #region "Busqueda"
        public List<BE.TipoPost> ObtenerTipoPost(string lang)
        {
            List<BE.TipoPost> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "TipoPostSP";


                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdiomaSigla", lang);
                DataTable dt = conx.ObtenerTablaSP(sql);

                ///////////////
                DataRow[] dr = new DataRow[dt.Rows.Count];
                dt.Rows.CopyTo(dr, 0);

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
