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
    public class PostContenido : BCEntidad
    {
        public PostContenido() : base()
        {
        }

        public PostContenido(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"
        public Boolean Actualizar(ref BE.PostContenido BEObj, Boolean isTransaccion = false)
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
                        strSql = @"update  PostContenido set PostContenidoId = @PostContenidoId,PostContenidoImagen = @PostContenidoImagen,PostContenidoVideo = @PostContenidoVideo,PostContenidoVisible = @PostContenidoVisible";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into PostContenido values(@PostId,@PostContenidoId,@PostContenidoImagen,@PostContenidoVideo,@PostContenidoVisible)";
                        break;

                }

                if (isTransaccion)
                    conx = dbConexion;
                else
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                }
                if (BEObj.TipoEstado != BE.TipoEstado.SinAccion)
                {

                    if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                        // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));

                        conx.CrearComando(strSql);
                    conx.AsignarParametro("@PostId", BEObj.PostId);
                    conx.AsignarParametro("@PostContenidoId", BEObj.PostContenidoId);
                    conx.AsignarParametro("@PostContenidoImagen", BEObj.PostContenidoImagen);
                    conx.AsignarParametro("@PostContenidoVideo", BEObj.PostContenidoVideo);
                    conx.AsignarParametro("@PostContenidoVisible", BEObj.PostContenidoVisible);

                }
                conx.EjecutarComando();

                if (!isTransaccion)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }

                bolOk = true;
            }
            catch (Exception ex)
            {
                if (!isTransaccion)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }

      


        public List<BE.PostContenido> CargarBE(DataRow[] dr)
        {
            List<BE.PostContenido> lst = new List<BE.PostContenido>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.PostContenido CargarBE(DataRow dr)
        {
            BE.PostContenido obj = new BE.PostContenido();
            obj.PostId = Convert.ToDecimal(dr["PostId"].ToString());
            obj.PostContenidoId = Convert.ToDecimal(dr["PostContenidoId"].ToString());
            obj.PostContenidoImagen = Convert.ToString(dr["PostContenidoImagen"].ToString());         
            obj.PostContenidoVideo = Convert.ToString(dr["PostContenidoVideo"].ToString());

            if (dr["PostContenidoVisible"].ToString() != "")
            {
                obj.PostContenidoVisible = Convert.ToBoolean(dr["PostContenidoVisible"].ToString());

            }
          
            return obj;


        }

        #endregion

        public List<BE.PostContenido> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.PostContenido> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {               
                string sql = String.Format(@"SELECT PostId, PostContenidoId, PostContenidoImagen, PostContenidoVideo, PostContenidoVisible
                                            FROM dbo.PostContenido with(nolock) where PostId in {0}", this.ConcatenarLlaves(llaves));
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

        #region "Busqueda"

        public List<BE.PostContenido> BuscarPostContenidoxId(decimal PostId)
        {
            List<BE.PostContenido> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from [PostContenido] with(nolock) where PostId={0}", PostId);
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
        #endregion
    }
}
