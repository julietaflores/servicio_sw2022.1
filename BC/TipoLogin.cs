using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class TipoLogin : BCEntidad
    {
        private string campos(string prefijo = "tl")
        {
            string strCampos = String.Format(@" {0}.TipoLoginId,{0}.TipoLoginNombreTipo,{0}.Estado" , prefijo);
            return strCampos;
        }
        public TipoLogin() : base()
        {
        }

        public TipoLogin(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
     

        public List<BE.TipoLogin> CargarBE(DataRow[] dr)
        {
            List<BE.TipoLogin> lst = new List<BE.TipoLogin>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.TipoLogin CargarBE(DataRow dr)
        {
            BE.TipoLogin obj = new BE.TipoLogin();
            obj.TipoLoginId = Convert.ToDecimal(dr["TipoLoginId"].ToString());
            obj.TipoLoginNombreTipo = dr["TipoLoginNombreTipo"].ToString();
            obj.Estado = Convert.ToInt32(dr["Estado"].ToString());



            return obj;
        }


        public List<BE.TipoLogin> ObtenerHijos(IEnumerable<decimal> llaves, string lang, params Enum[] relaciones)
        {
            List<BE.TipoLogin> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.TipoLogin tl with(nolock) where tl.TipoLoginId in {1}", campos("tl"), this.ConcatenarLlaves(llaves), lang);
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

        public List<BE.TipoLogin> ObtenerTipoLogin(string lang)
        {
            List<BE.TipoLogin> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "TipoLoginSP";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdiomaSigla", lang);
                DataTable dt = conx.ObtenerTablaSP(sql);
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


    }
}
