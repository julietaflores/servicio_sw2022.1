using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
    public class set : BCEntidad
    {
        private string campos(string prefijo = "s")
        {
            string strCampos = String.Format(@"{0}.GeneroId,{0}.GeneroNombreTipo"
                    , prefijo);
            return strCampos;
        }
        public set() : base()
        {
        }

        public set(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        public List<BE.set> CargarBE(DataRow[] dr)
        {
            List<BE.set> lst = new List<BE.set>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.set CargarBE(DataRow dr)
        {
            BE.set obj = new BE.set();
            obj.id_s = Convert.ToDecimal(dr["ServicioId"].ToString());
            obj.Nombre_servicio = dr["ServicioNombre"].ToString();




            return obj;
        }
        #region "Busqueda"

        public List<BE.set> Listarservicio(decimal search_tex_form, params Enum[] relaciones)
        {
            List<BE.set> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select Servicioid,ServicioNombre from servicio where categoriaServicioId={0} ", search_tex_form);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //CargarRelaciones(ref obj, lang, relaciones);
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
