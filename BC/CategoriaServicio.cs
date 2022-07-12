using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using BE;


namespace BC
{
   public class CategoriaServicio : BCEntidad
    {
        public CategoriaServicio() : base()
        {
        }
        public CategoriaServicio(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.CategoriaServicio BEObj)
        {
            string strSql = string.Empty;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            bool bolOk = false;

            try
            {
                string TipoEstadoa = BEObj.TipoEstado.ToString();
                switch (BEObj.TipoEstado)
                {

                    case BE.TipoEstado.Modificar:

                        strSql = "";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = "";
                        break;

                }

                if (dbConexion != null)
                    conx = dbConexion;
                else
                    conx.Conectar();
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));
                    conx.CrearComando(strSql);
             ///   conx.AsignarParametro("@StatusServicioId", BEObj.StatusServAsigId);
            
                conx.EjecutarComando();
                if (dbConexion == null)
                    conx.Desconectar();
                bolOk = true;
            }
            catch (Exception ex)
            {
                if (dbConexion == null)
                {
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }

     
        public List<BE.CategoriaServicio> CargarBE(DataRow[] dr)
        {
            List<BE.CategoriaServicio> lst = new List<BE.CategoriaServicio>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.CategoriaServicio CargarBE(DataRow dr)
        {
            BE.CategoriaServicio obj = new BE.CategoriaServicio();
            
            ////////////////////////////
            obj.CategoriaServicioId = Convert.ToDecimal(dr["CategoriaServicioId"].ToString());
            obj.CategoriaServicioNombre = Convert.ToString(dr["CategoriaServicioNombre"].ToString());
            obj.CategoriaServicioURLFoto = Convert.ToString(dr["CategoriaServicioURLFoto"].ToString());
            obj.CiudadId = Convert.ToDecimal(dr["CiudadId"].ToString());
            obj.CategoriaServicioDescripcion = Convert.ToString(dr["CategoriaServicioDescripcion"].ToString());
            if (dr["CategoriaServicioUsuario"].ToString() != "")
            {
                obj.CategoriaServicioUsuario = Convert.ToString(dr["CategoriaServicioUsuario"].ToString());

            }
            if (dr["CategoriaServicioFechaHoraMod"].ToString() != "")
            {
                obj.CategoriaServicioFechaHoraMod = Convert.ToDateTime(dr["CategoriaServicioFechaHoraMod"].ToString());

            }

            if (dr["CategoriaServicioHijoId"].ToString() != "")
            {
                obj.CategoriaServicioHijoId = Convert.ToDecimal(dr["CategoriaServicioHijoId"].ToString());
            }


            if (dr["CategoriaServicioDestLast"].ToString() != "")
            {
                obj.CategoriaServicioDestLast = Convert.ToDecimal(dr["CategoriaServicioDestLast"].ToString());

            }



            return obj;
        }

        public void CargarRelaciones(ref List<BE.CategoriaServicio> colObj, string lang = "", params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.CategoriaServicio> colCategoriaServicio = null;
            List<BE.Ciudad> colCiudad = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relCategoriaServicio.Ciudad))
                {
                    BC.Ciudad bcCiudad = new BC.Ciudad(cadenaConexion);

                    llaves = (from elemento in colObj where elemento.CiudadId != null select Convert.ToDecimal(elemento.CiudadId)).Distinct();
                    colCiudad = bcCiudad.ObtenerHijos(llaves, relaciones);
                    bcCiudad = null;
                }
                


            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {
                    if (colCiudad != null && colCiudad.Count > 0)
                    {
                        item.ciudad = (from elemento in colCiudad where elemento.CiudadId == item.CiudadId select elemento).ToList().FirstOrDefault();
                    }

                  

                }
            }
        }

        public List<BE.CategoriaServicio> ObtenerHijos(IEnumerable<decimal> llaves, string lang, params Enum[] relaciones)
        {
            List<BE.CategoriaServicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select CategoriaServicioId,CategoriaServicioNombre,CategoriaServicioURLFoto,CiudadId,CategoriaServicioDescripcion,CategoriaServicioUsuario,CategoriaServicioFechaHoraMod,CategoriaServicioHijoId,CategoriaServicioDestLast from CategoriaServicioSP ('{0}') where CategoriaServicioId in {1} ", lang, this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, lang, relaciones);
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

        public BE.CategoriaServicio BuscarCategoriaServicioxId(decimal categoriaServicioId)
        {
            BE.CategoriaServicio obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from categoriaServicio with(nolock) where categoriaServicioId={0}", categoriaServicioId);
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
