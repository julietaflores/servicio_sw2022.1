using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class EstadoSiniestro:BCEntidad
    {
        public EstadoSiniestro() : base()
        {
        }
        public EstadoSiniestro(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }



        #region  "DEFINICION DE METODOS DE ABM"

        public List<BE.EstadoSiniestro> CargarBE(DataRow[] dr)
        {
            List<BE.EstadoSiniestro> lst = new List<BE.EstadoSiniestro>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.EstadoSiniestro CargarBE(DataRow dr)
        {

            BE.EstadoSiniestro obj = new BE.EstadoSiniestro();

            ///////////////////////////////
            obj.SiniestroEstadoId = Convert.ToDecimal(dr["SiniestroEstadoId"].ToString());
            obj.SiniestroEstadoNombre = dr["SiniestroEstadoNombre"].ToString();

            return obj;

        }

        public List<BE.EstadoSiniestro> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.EstadoSiniestro> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from EstadoSiniestro with(nolock)
                                            where SiniestroEstadoId in {0}", this.ConcatenarLlaves(llaves));
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


        public List<BE.EstadoSiniestro> VerEstadoSiniestro(string lang="", params Enum[] relaciones)
        {
            List<BE.EstadoSiniestro> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                if (lang == "") lang = "es";
                string sql = String.Format(@"SELECT es.SiniestroEstadoId,e.EquivalenciaValor as SiniestroEstadoNombre
FROM[dbo].[Equivalencia] e with(nolock)
inner join Idioma i with(nolock)
on
e.IdiomaId=i.idiomaId
inner join Objeto o with(nolock)
on 
e.ObjetoId=o.ObjetoId
INNER JOIN EstadoSiniestro  es with(nolock)
ON
e.EquivalenciaObjetoId=es.SiniestroEstadoId
and o.ObjetoNombre='EstadoSiniestro'
and i.IdiomaSigla='{0}'
 ",lang);
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

        #region "Listados"

        #endregion

        #region "Busqueda"

    
        
        #endregion
    }
}
