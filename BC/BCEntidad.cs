using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public abstract class BCEntidad
    {

        protected string cadenaConexion { get; set; }

        protected BCEntidad()
        {
            cadenaConexion = "Negocio";
        }

        protected BCEntidad (string nombreCadena)
        {
            cadenaConexion = nombreCadena;
        }

        protected string ConcatenarLlaves(IEnumerable<decimal> llaves)
        {
            StringBuilder indices = new StringBuilder();
            if(llaves.Count()>0)
            {
                indices.Append("(");
                foreach (var llave in llaves)
                {
                    indices.Append(llave.ToString ()).Append(",");
                }
                indices.Replace(",", ")", indices.Length - 1, 1);
            }
            else
            {
                indices.Append("(0)");
            }

            return indices.ToString(); 
        }
        protected string ConcatenarLlaves(IEnumerable<int> llaves)
        {
            StringBuilder indices = new StringBuilder();
            if (llaves.Count() > 0)
            {
                indices.Append("(");
                foreach (var llave in llaves)
                {
                    indices.Append(llave.ToString()).Append(",");
                }
                indices.Replace(",", ")", indices.Length - 1, 1);
            }
            else
            {
                indices.Append("(0)");
            }

            return indices.ToString();
        }

        protected string ConcatenarLlaves(IEnumerable<string> llaves)
        {
            StringBuilder indices = new StringBuilder();
            if (llaves.Count() > 0)
            {
                indices.Append("('");
                foreach (var llave in llaves)
                {
                    indices.Append(llave.ToString()).Append("','");
                }
                indices.Replace(",'", ")", indices.Length - 2, 2);
            }
            else
            {
                indices.Append("('')");
            }

            return indices.ToString();
        }

        protected string GenIdAN( string tabla, ClaseConexion conx)
        {
            string newId = "";
            try
            {
                conx.CrearComando("dbo.GenerarId",CommandType.StoredProcedure);
                conx.AsignarParametro("@NombreTabla", tabla);
                conx.AsignarParametro("@id","", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                newId = conx.ObtenerParametro("@id").ToString() ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newId;
        }
    }
}
