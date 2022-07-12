using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
    class ServicioDescripcion:BCEntidad
    {



        private string campos(string prefijo = "sds")
        {
            string strCampos = String.Format(@"{0}.ServicioDescripcionId,{0}.ServicioId,{0}.ServicioDescripciondesc", prefijo);
            return strCampos;
        }


        public ServicioDescripcion() : base()
        {
        }

        public ServicioDescripcion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<BE.ServicioDescripcion> CargarBE(DataRow[] dr)
        {
            List<BE.ServicioDescripcion> lst = new List<BE.ServicioDescripcion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }

        public BE.ServicioDescripcion CargarBE(DataRow dr)
        {
            BE.ServicioDescripcion obj = new BE.ServicioDescripcion();
            obj.ServicioDescripcionId = Convert.ToDecimal(dr["ServicioDescripcionId"].ToString());
            obj.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            obj.ServicioDescripciondesc = Convert.ToString(dr["ServicioDescripciondesc"].ToString());

            return obj;
        }


        public void CargarRelaciones(ref List<BE.ServicioDescripcion> colObj, params Enum[] relaciones)
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
                  
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                 
                }
            }
        }


        public List<BE.ServicioDescripcion> ObtenerHijos(IEnumerable<decimal> llaves, string lang, params Enum[] relaciones)
        {
            List<BE.ServicioDescripcion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.[ServicioDescripcionSP]('{2}') stxt  where stxt.ServicioId in {1}", campos("stxt"), this.ConcatenarLlaves(llaves), lang);
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







    }
}
