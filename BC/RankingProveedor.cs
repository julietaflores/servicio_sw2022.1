using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class RankingProveedor:BCEntidad
    {
        private string campos(string prefijo = "spd")
        {
            string strCampos = String.Format(@"{0}.ServicioPersonaId, {0}.ServicioPersonaDocId, {0}.ServicioPersonaDocNombre, {0}.ServicioPersonaDocFecha"
                    , prefijo);
            return strCampos;
        }
        public RankingProveedor() : base()
        {
        }

        public RankingProveedor(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"



        public List<BE.ServicioPersona> CargarBE(ref List<BE.ServicioPersona> servicioPersona,DataRow[] dr)
        {
            List<BE.ServicioPersona> lst = new List<BE.ServicioPersona>();
            int i = 0;
            foreach (var item in dr)
            {
                lst.Add(CargarBE( servicioPersona[i], item));
                i = i + 1;
            }
            return lst;
        }
        public BE.ServicioPersona CargarBE(BE.ServicioPersona servicioPersona,DataRow dr)
        {
            BE.RankingProveedor obj = new BE.RankingProveedor();
            obj.NroTrabajos = Convert.ToInt32(dr["NroTrabajos"].ToString());
            obj.Ranking = Convert.ToDecimal(dr["Ranking"].ToString());


            servicioPersona.rankingProveedor = obj;
            return servicioPersona;
        }

        public void CargarRelaciones(ref List<BE.ServicioPersonaDocumento> colObj, params Enum[] relaciones)
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
        #endregion

        #region "Listados"
        public List<BE.RankingProveedor> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.RankingProveedor> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select isnull(sum(pos.PostCalificacion)/count(*),0.0) NroRating,count(*) NroTrabajos ,rsp.ServicioPersonaId
from dbo.Post pos with(nolock) inner join
dbo.ServAsig sra with(nolock) 
on pos.ServAsigId = sra.ServAsigId 
inner join dbo.RequiereServicioProveedores rsp with(nolock)
on sra.RequiereServicioId = rsp.RequiereServicioId 
 and rsp.StatusRequiereId = 4
inner join RequiereServicio rs 
--IGNORANDO LOS REGISTROS DE CALIFICACION DE PRUEBA
on sra.RequiereServicioId=rs.RequiereServicioId
and sra.StatusServAsigId = 3 and sra.ServAsigFHPago is not null
and rs.PersonaId NOT IN (152,201,125,116,225,118,179,113,123)
and rsp.ServicioPersonaId in {0}
group by rsp.ServicioPersonaId",  this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                //    obj = CargarBE(dr);
                  //  CargarRelaciones(ref obj, relaciones);
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

        #endregion
    }
}
