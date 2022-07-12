using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class Empresa_Usuario:BCEntidad
    {
        private string campos(string prefijo = "eu")
        {
            string strCampos = String.Format(@"{0}.Nit,{0}.UserId,{0}.Fecha_Asig"
                    , prefijo);
            return strCampos;
        }
        public Empresa_Usuario() : base()
        {
        }

        public Empresa_Usuario(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        public List<BE.Empresa_Usuario> CargarBE(DataRow[] dr)
        {
            List<BE.Empresa_Usuario> lst = new List<BE.Empresa_Usuario>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Empresa_Usuario CargarBE(DataRow dr)
        {
            BE.Empresa_Usuario obj = new BE.Empresa_Usuario();

            obj.Nit = Convert.ToInt32(dr["Nit"].ToString());
            obj.UserId = Convert.ToInt32(dr["UserId"].ToString());
            obj.Fecha_Asig =Convert.ToDateTime(dr["Fecha_Asig"].ToString());
           
            return obj;
        }
        public BE.Empresa_Usuario ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            BE.Empresa_Usuario obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Empresa_Usuario eu with(nolock) where eu.UserId in {1}", campos("mo"), this.ConcatenarLlaves(llaves));
                DataRow dr = conx.ObtenerFila(sql);
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
        public void CargarRelaciones(ref BE.Empresa_Usuario obj, params Enum[] relaciones)
        {
            if (relaciones == null || obj == null)
            {
                return;
            }
            IEnumerable<int> llaves;
            IEnumerable<string> sllaves;
            foreach (Enum clase in relaciones)
            {

                if (clase.Equals(BE.relEmpresa_Usuario.UserProfile))
                {
                    BC.UserProfile bsUserProfile = new BC.UserProfile(cadenaConexion);
                    llaves = new int[] { Convert.ToInt32(obj.UserId) };
                    obj.UserProfile = bsUserProfile.ObtenerHijos(llaves, relaciones).ToList().FirstOrDefault();
                    bsUserProfile = null;
                }
            

            }
        }

        public BE.Empresa_Usuario verEmpresaUsuario(int NIT, params Enum[] relaciones)
        {
            BE.Empresa_Usuario obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
             
                string sql = String.Format(@"SELECT * from [Empresa_Usuario] with(nolock)  where Nit={0} ",NIT);
                DataRow dr = conx.ObtenerFila(sql);
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
