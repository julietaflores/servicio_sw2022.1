using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class Genero : BCEntidad
    {
        private string campos(string prefijo = "c")
        {
            string strCampos = String.Format(@"{0}.GeneroId,{0}.GeneroNombreTipo ,{0}.Estado" , prefijo);
            return strCampos;
        }
        public Genero() : base()
        {
        }

        public Genero(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        public List<BE.Genero> CargarBE(DataRow[] dr)
        {
            List<BE.Genero> lst = new List<BE.Genero>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Genero CargarBE(DataRow dr)
        {
            BE.Genero obj = new BE.Genero();
            obj.GeneroId = Convert.ToDecimal(dr["GeneroId"].ToString());
            obj.GeneroNombreTipo = dr["GeneroNombreTipo"].ToString();
            obj.Estado  = Convert.ToInt32(dr["Estado"].ToString());




            return obj;
        }
   
        public List<BE.Genero> ObtenerGenero(string lang)
        {
            List<BE.Genero> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "GeneroSP";
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
