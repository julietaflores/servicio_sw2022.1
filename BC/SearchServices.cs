using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BC
{
   public class SearchServices:BCEntidad
    {
        private string campos(string prefijo = "s")
        {
            string strCampos = String.Format(@" {0}.servicioId,{0}.ServicioNombre,{0}.Ciudad"
                    , prefijo);
            return strCampos;
        }
        public SearchServices() : base()
        {
        }

        public SearchServices(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        public List<BE.SearchServices> CargarBE(DataRow[] dr)
        {
            List<BE.SearchServices> lst = new List<BE.SearchServices>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.SearchServices CargarBE(DataRow dr)
        {
            BE.SearchServices obj = new BE.SearchServices();
            obj.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            obj.ServicioNombre = dr["ServicioNombre"].ToString();
            obj.Ciudad = (dr["Ciudad"].ToString());
            return obj;
        }


        #region "Busqueda"
        public List<BE.SearchServices> verSearchServices(string nombre)
        {
            List<BE.SearchServices> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = "BusquedaServicioV2";


                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@Nombre", nombre);
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
