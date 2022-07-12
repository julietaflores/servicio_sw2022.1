using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class IdiomaS:BCEntidad
    {
       
        public IdiomaS() : base()
        {
        }

        public IdiomaS(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        public List<BE.IdiomaS> CargarBE(DataRow[] dr)
        {
            List<BE.IdiomaS> lst = new List<BE.IdiomaS>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.IdiomaS CargarBE(DataRow dr)
        {
            BE.IdiomaS obj = new BE.IdiomaS();
            obj.IdiomaId = Convert.ToDecimal(dr["IdiomaId"].ToString());
            obj.IdiomaSigla = (dr["IdiomaSigla"].ToString());
            obj.Definicion = dr["EquivalenciaValor"].ToString();
            obj.Estado = Convert.ToInt32(dr["Estado"].ToString());


            return obj;
        }




        public List<BE.IdiomaS> ObtenerIdioma(string lang)
        {
            List<BE.IdiomaS> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "IdiomaSP";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdiomaSigla", lang);
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




        public List<BE.IdiomaS> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.IdiomaS> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select IdiomaSigla,EquivalenciaValor from equivalencia with(nolock)
inner join Idioma with(nolock)
on Equivalencia.IdiomaId=Idioma.IdiomaId
where ObjetoId=15 and EquivalenciaObjetoId={0}", this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
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

        public List<BE.IdiomaS> ObtenerHijosServicio(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.IdiomaS> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql_d = String.Format(@"select categoriaServicioid from servicio where Servicioid={0}", this.ConcatenarLlaves(llaves));
                decimal catId = Convert.ToDecimal(conx.ObtenerValor(sql_d));

                string sql = String.Format(@"

select  c3.IdiomaSigla,c3.serv+'@'+c3.cat EquivalenciaValor  from 
(

select * from 
(select IdiomaSigla IdiomaSigla,EquivalenciaValor serv from equivalencia with(nolock)
inner join Idioma with(nolock)
on Equivalencia.IdiomaId=Idioma.IdiomaId
where ObjetoId=12 and EquivalenciaObjetoId={0})c1
inner join
(select IdiomaSigla IdiomaSigla2,EquivalenciaValor cat from equivalencia with(nolock)
inner join Idioma with(nolock)
on Equivalencia.IdiomaId=Idioma.IdiomaId
where ObjetoId=11 and EquivalenciaObjetoId={1})c2
on c1.IdiomaSigla=c2.IdiomaSigla2

)C3

", this.ConcatenarLlaves(llaves),(catId));
                DataRow[] dr = conx.ObtenerFilas(sql);
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

        public List<BE.IdiomaS> ObtenerHijosStatus(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.IdiomaS> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select IdiomaSigla,EquivalenciaValor from equivalencia with(nolock)
inner join Idioma with(nolock)
on Equivalencia.IdiomaId=Idioma.IdiomaId
where ObjetoId=17 and EquivalenciaObjetoId={0}", this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
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
