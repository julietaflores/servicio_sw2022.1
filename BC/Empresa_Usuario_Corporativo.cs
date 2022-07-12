using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class Empresa_Usuario_Corporativo : BCEntidad
    {
        private string campos(string prefijo = "euc")
        {
            string strCampos = String.Format(@"{0}.Nit,{0}.UserId,{0}.Fecha_Asig"
                    , prefijo);
            return strCampos;
        }
        public Empresa_Usuario_Corporativo() : base()
        {
        }

        public Empresa_Usuario_Corporativo(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        public List<BE.Empresa_Usuario_Corporativo> CargarBE(DataRow[] dr)
        {
            List<BE.Empresa_Usuario_Corporativo> lst = new List<BE.Empresa_Usuario_Corporativo>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Empresa_Usuario_Corporativo CargarBE(DataRow dr)
        {
            BE.Empresa_Usuario_Corporativo obj = new BE.Empresa_Usuario_Corporativo();

            obj.Nit = Convert.ToInt32(dr["Nit"].ToString());
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.Fecha_Asig = Convert.ToDateTime(dr["Fecha_Asig"].ToString());
            obj.UserId= Convert.ToInt32(dr["UserId"].ToString());
            obj.Asignacion_Nombre_UserId = dr["Asignacion_Nombre_UserId"].ToString();

            return obj;
        }
        public BE.Empresa_Usuario_Corporativo ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            BE.Empresa_Usuario_Corporativo obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Empresa_Usuario_Corporativo euc with(nolock) where euc.PersonaId in {1}", campos("euc"), this.ConcatenarLlaves(llaves));
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
        public void CargarRelaciones(ref BE.Empresa_Usuario_Corporativo obj, params Enum[] relaciones)
        {
            if (relaciones == null || obj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            foreach (Enum clase in relaciones)
            {

                if (clase.Equals(BE.relEmpresa_Usuario_Corporativo.Persona))
                {
                    BC.Persona bcPersona = new BC.Persona(cadenaConexion);
                    llaves = new decimal[] { Convert.ToInt32(obj.PersonaId) };
                    obj.Persona = bcPersona.ObtenerHijos(llaves, relaciones).ToList().FirstOrDefault();
                    bcPersona = null;
                }


            }
        }

        public BE.Empresa_Usuario_Corporativo verEmpresaUsuarioCorporativo(decimal PersonaId, params Enum[] relaciones)
        {
            BE.Empresa_Usuario_Corporativo obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"SELECT * from [Empresa_Usuario_Corporativo] with(nolock)  where PersonaId={0} ", PersonaId);
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
