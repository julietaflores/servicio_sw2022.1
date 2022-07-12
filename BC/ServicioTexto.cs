using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
    public class ServicioTexto:BCEntidad
    {


        private string campos(string prefijo = "stxt")
        {
            string strCampos = String.Format(@"{0}.ServicioTextoId,{0}.ServicioId,{0}.ServicioTextodesc", prefijo);
            return strCampos;
        }


        public ServicioTexto() : base()
        {
        }

        public ServicioTexto(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<BE.ServicioTexto> CargarBE(DataRow[] dr)
        {
            List<BE.ServicioTexto> lst = new List<BE.ServicioTexto>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }

        public BE.ServicioTexto CargarBE(DataRow dr)
        {
            BE.ServicioTexto obj = new BE.ServicioTexto();
            obj.ServicioTextoId = Convert.ToDecimal(dr["ServicioTextoId"].ToString());
            obj.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            obj.ServicioTextodesc = Convert.ToString(dr["ServicioTextodesc"].ToString());

            return obj;
        }


        public void CargarRelaciones(ref List<BE.ServicioTexto> colObj, params Enum[] relaciones)
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
                    //BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                    //sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    //colRequiereServicio = bcRequiereServicio.ObtenerHijos(sllaves, relaciones);
                    //bcRequiereServicio = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    //if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    //{
                    //    item.requiereServicio = (from elemento in colRequiereServicio where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList().FirstOrDefault();
                    //}
                }
            }
        }

   
        public List<BE.ServicioTexto> ObtenerHijos(IEnumerable<decimal> llaves, string lang, params Enum[] relaciones)
        {
            List<BE.ServicioTexto> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.[ServicioTextoSP]('{2}') stxt  where stxt.ServicioId in {1}", campos("stxt"), this.ConcatenarLlaves(llaves), lang);
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
