using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Security.Cryptography;
using System.Data;
namespace BC
{
    public class UserProfile : BCEntidad
    {
        private string campos(string prefijo = "up")
        {
            string strCampos = String.Format(@"{0}.UserId,{0}.UserName,{0}.Password,{0}.IsActive,{0}.TipoPersona"
                    , prefijo);
            return strCampos;
        }
        public UserProfile() : base()
        {
        }

        public UserProfile(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

    

        public List<BE.UserProfile> CargarBE(DataRow[] dr)
        {
            List<BE.UserProfile> lst = new List<BE.UserProfile>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.UserProfile CargarBE(DataRow dr)
        {
            BE.UserProfile obj = new BE.UserProfile();

            obj.UserId = Convert.ToInt32(dr["UserId"].ToString());
            obj.UserName = dr["UserName"].ToString();
            obj.Password = dr["Password"].ToString();
            obj.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
            obj.TipoPersona= Convert.ToInt32(dr["TipoPersona"].ToString());
            return obj;
        }

        public void CargarRelaciones(ref List<BE.UserProfile> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.UserProfile> colRequiereServicio = null;
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
        #endregion

        #region "Listados"
        public List<BE.UserProfile> ObtenerHijos(IEnumerable<int> llaves, params Enum[] relaciones)
        {
            List<BE.UserProfile> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.UserProfile up with(nolock) where up.UserId in {1}", campos("up"), this.ConcatenarLlaves(llaves));
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
        #endregion
        #region "Busqueda"
        public BE.UserProfile verUserProfile(string UserName,string Password)
        {
            BE.UserProfile obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = md5.ComputeHash(encoding.GetBytes(Password));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                string ff = sb.ToString();
                string sql = String.Format(@"SELECT * from [UserProfile] with(nolock)  where UserName='{0}' and Password='{1}'", UserName, ff);
                DataRow dr = conx.ObtenerFila(sql);
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
