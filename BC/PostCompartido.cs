using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL; 

namespace BC
{
   public  class PostCompartido:BCEntidad
    {
        private string campos(string prefijo = "b")
        {
            string strCampos = String.Format(@"{0}.PostId,{0}.PostCompartidoId,{0}.PostCompartidoTitulo,{0}.PostCompartidoFechaHora,{0}.PostCompartidoValeDesc,
{0}.PostCompartidoValeEstado,{0}.PostCompartidoValeFechaHora,{0}.PostCompartidoValeCaducidad,{0}.PersonaCompartidoId
"
                    , prefijo);
            return strCampos;
        }
        public PostCompartido() : base()
        {
        }

        public PostCompartido(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"


        public Boolean Actualizar(ref BE.PostCompartido BEObj, Boolean isTransaccion = false)
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
                        strSql = @"";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into dbo.PostCompartido(PostId,PostCompartidoId,PostCompartidoTitulo,PostCompartidoFechaHora,PostCompartidoValeDesc,PostCompartidoValeEstado,PostCompartidoValeFechaHora,PostCompartidoValeCaducidad, PersonaCompartidoId)
                                    values(@PostId,@PostCompartidoId,@PostCompartidoTitulo,@PostCompartidoFechaHora,@PostCompartidoValeDesc,@PostCompartidoValeEstado,@PostCompartidoValeFechaHora,@PostCompartidoValeCaducidad,@PersonaCompartidoId)";
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
                    {
                        BEObj.PostCompartidoId = System.Convert.ToDecimal(conx.ObtenerValor(String.Format("select isnull(max(PostCompartidoId),0) + 1 from dbo.PostCompartido with (nolock) where PostId = {0} ", BEObj.PostId)));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@PostId", BEObj.PostId);
                    conx.AsignarParametro("@PostCompartidoId", BEObj.PostCompartidoId);
                    conx.AsignarParametro("@PostCompartidoTitulo", BEObj.PostCompartidoTitulo);
                    conx.AsignarParametro("@PostCompartidoFechaHora", BEObj.PostCompartidoValeFechaHora);
                    conx.AsignarParametro("@PostCompartidoValeDesc", BEObj.PostCompartidoValeDesc);
                    conx.AsignarParametro("@PostCompartidoValeEstado", BEObj.PostCompartidoValeEstado);
                    conx.AsignarParametro("@PostCompartidoValeFechaHora", BEObj.PostCompartidoValeFechaHora);
                    conx.AsignarParametro("@PostCompartidoValeCaducidad", BEObj.PostCompartidoValeCaducidad);
                    conx.AsignarParametro("@PersonaCompartidoId", BEObj.PersonaCompartidoId);

                    conx.EjecutarComando();

                    if (!isTransaccion)
                    {
                        conx.ConfirmarTransaccion();
                        conx.Desconectar();
                    }

                    bolOk = true;
                }
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


        public List<BE.PostCompartido> CargarBE(DataRow[] dr)
        {
            List<BE.PostCompartido> lst = new List<BE.PostCompartido>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.PostCompartido CargarBE(DataRow dr)
        {
            BE.PostCompartido obj = new BE.PostCompartido();
            obj.PostId = Convert.ToDecimal(dr["PostId"].ToString());
            obj.PostCompartidoId = Convert.ToDecimal(dr["PostCompartidoId"].ToString());
            obj.PostCompartidoTitulo = dr["PostCompartidoId"].ToString();
            obj.PostCompartidoValeFechaHora = Convert.ToDateTime(dr["PostCompartidoValeFechaHora"].ToString());
            obj.PostCompartidoValeDesc = Convert.ToString(dr["PostCompartidoValeDesc"].ToString());
            obj.PostCompartidoValeEstado = Convert.ToString(dr["PostCompartidoValeEstado"].ToString());
            obj.PostCompartidoValeFechaHora = Convert.ToDateTime(dr["PostCompartidoValeFechaHora"].ToString());
            obj.PostCompartidoValeCaducidad = Convert.ToDateTime(dr["PostCompartidoValeCaducidad"].ToString());
            obj.PersonaCompartidoId = Convert.ToDecimal(dr["PersonaCompartidoId"].ToString());


            return obj;


        }


        #endregion

        public List<BE.PostCompartido> ObtenerHijos(IEnumerable<decimal> llaves, long personaId = 0, params Enum[] relaciones)
        {
            List<BE.PostCompartido> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string strPersona = "";
                if (!personaId.Equals(0))
                {
                    strPersona = String.Format(" and b.PersonaCompartidoId = {0}", personaId);
                }
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.PostCompartido b with(nolock) where b.PostId in {1}", campos ("b") ,this.ConcatenarLlaves(llaves), strPersona);
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

        public List<BE.PostCompartido> ListadoxPostPersona(long postId, long personaId)
        {
            List<BE.PostCompartido> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string strPersona = "";
                if (!personaId.Equals(0))
                {
                    strPersona = String.Format(" and b.PersonaCompartidoId = {0}", personaId);
                }
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.PostCompartido b with(nolock) where b.PostId = {1} {2}", campos("b"), postId, strPersona);
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


        public List<BE.PostCompartido> ListadoxPostPersonaV(long postId, long personaId)
        {
            List<BE.PostCompartido> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string strPersona = "";
                if (!personaId.Equals(0))
                {
                    strPersona = String.Format(" and b.PersonaCompartidoId = {0}", personaId);
                }
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.PostCompartido b with(nolock) where b.PostId = {1} {2}", campos("b"), postId, strPersona);
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
