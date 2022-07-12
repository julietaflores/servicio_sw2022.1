using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class TipoPersona : BCEntidad
    {
        private string campos(string prefijo = "c")
        {
            string strCampos = String.Format(@"{0}.TipoPersonaId,{0}.TipoPersonaNombreTipo,{0}.Estado"
                    , prefijo);
            return strCampos;
        }
        public TipoPersona() : base()
        {
        }

        public TipoPersona(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        public List<BE.TipoPersona> CargarBE(DataRow[] dr)
        {
            List<BE.TipoPersona> lst = new List<BE.TipoPersona>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.TipoPersona CargarBE(DataRow dr)
        {
            BE.TipoPersona obj = new BE.TipoPersona();
            obj.TipoPersonaId = Convert.ToDecimal(dr["TipoPersonaId"].ToString());
            obj.TipoPersonaNombreTipo = dr["TipoPersonaNombreTipo"].ToString();
            obj.Estado = Convert.ToInt32(dr["Estado"].ToString());

            return obj;
        }

     
        public List<BE.TipoPersona> ObtenerTipoPersona(string lang)
        {
            List<BE.TipoPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "TipoPersonaSP";
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
