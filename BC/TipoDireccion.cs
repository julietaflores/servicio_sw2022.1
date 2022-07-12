using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class TipoDireccion : BCEntidad
    {
        private string campos(string prefijo = "c")
        {
            string strCampos = String.Format(@"{0}.PaisId,{0}.PaisNombre"
                    , prefijo);
            return strCampos;
        }
        public TipoDireccion() : base()
        {
        }

        public TipoDireccion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        public List<BE.TipoDireccion> CargarBE(DataRow[] dr)
        {
            List<BE.TipoDireccion> lst = new List<BE.TipoDireccion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.TipoDireccion CargarBE(DataRow dr)
        {
            BE.TipoDireccion obj = new BE.TipoDireccion();
            obj.TipoDireccionId = Convert.ToDecimal(dr["TipoDireccionId"].ToString());
            obj.TipoDireccionNombreTipo = dr["TipoDireccionNombreTipo"].ToString();
            obj.Estado = Convert.ToInt32(dr["Estado"].ToString());



            return obj;
        }
        #region "Busqueda"
        public List<BE.TipoDireccion> ObtenerTipoDireccion(string lang)
        {
            List<BE.TipoDireccion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = "TipoDireccionSP";


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
