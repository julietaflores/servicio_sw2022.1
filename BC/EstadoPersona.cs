using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class EstadoPersona : BCEntidad
    {
        private string campos(string prefijo = "c")
        {
            string strCampos = String.Format(@"{0}.EstadoPersonaId,{0}.EstadoPersonaNombreTipo"
                    , prefijo);
            return strCampos;
        }
        public EstadoPersona() : base()
        {
        }

        public EstadoPersona(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        public List<BE.EstadoPersona> CargarBE(DataRow[] dr)
        {
            List<BE.EstadoPersona> lst = new List<BE.EstadoPersona>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.EstadoPersona CargarBE(DataRow dr)
        {
            BE.EstadoPersona obj = new BE.EstadoPersona();
            obj.EstadoPersonaId = Convert.ToDecimal(dr["EstadoPersonaId"].ToString());
            obj.EstadoPersonaNombreTipo = dr["EstadoPersonaNombreTipo"].ToString();




            return obj;
        }

        #region "Busqueda"

        public List<BE.EstadoPersona> ObtenerEstadoPersona(string lang)
        {
            List<BE.EstadoPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "EstadoPersonaSP";


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
