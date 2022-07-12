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
    public class PostLikes : BCEntidad
    {
        public PostLikes() : base()
        {
        }
        public PostLikes(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.PostLikes BEObj, Boolean isTransaccion = false)
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

                        strSql = @"UPDATE dbo.PostLikes
                                    SET                                       
                                        PersonaLikesId = @personalikesid,
                                        PostLikesFechaHora = @postlikesfechahora
                                    where  PostId = @postid and  PostLikesId = @postlikesid";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO dbo.PostLikes (PostId, PostLikesId, PersonaLikesId, PostLikesFechaHora)
                                VALUES(@postid, @postlikesid, @personalikesid, @postlikesfechahora)";
                        break;

                    case BE.TipoEstado.Eliminar:
                        strSql = @"DELETE dbo.PostLikes where PostId = @postid and  PostLikesId = @postlikesid";
                        break;
                }

                if (isTransaccion)
                    conx = dbConexion;
                else
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                }
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                     BEObj.PostLikesId = System.Convert.ToDecimal(conx.ObtenerValor(String.Format("select isnull(max(PostLikesId),0) + 1 from dbo.postlikes with (nolock) where PostId = {0} and PersonaLikesId = {1}", BEObj .PostId,BEObj .PersonaLikesId)));
                
                if(BEObj .TipoEstado != BE.TipoEstado .SinAccion)
                {
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@postid", BEObj.PostId);
                    conx.AsignarParametro("@postlikesid", BEObj.PostLikesId);
                    conx.AsignarParametro("@personalikesid", BEObj.PersonaLikesId);
                    conx.AsignarParametro("@postlikesfechahora", BEObj.PostLikesFechaHora);
                    conx.EjecutarComando();
                }

                if (!isTransaccion)
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
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

        //public Boolean Actualizar(ref BE.Persona BEObj)
        //{
        //    string strSql = string.Empty;
        //    ClaseConexion conx = new ClaseConexion(cadenaConexion);
        //    bool bolOk = false;

        //    try
        //    {
        //        string TipoEstadoa = BEObj.TipoEstado.ToString();
        //        switch (BEObj.TipoEstado)
        //        {

        //            case BE.TipoEstado.Modificar:

        //                strSql = "update dbo.Persona set PersonaTokenId = @PersonaTokenId,PersonaNombres = @PersonaNombres,personaApellidos = @personaApellidos,PersonaCorreo = @PersonaCorreo,PersonaFechaNacimiento = @PersonaFechaNacimiento,PersonaTelefono = @PersonaTelefono,PersonaUID = @PersonaUID,PersonaURLFoto = @PersonaURLFoto,PersonaUsuario = @PersonaUsuario,PersonaFechaHoraMod = @PersonaFechaHoraMod,TipoPersonaId = @TipoPersonaId,GeneroId = @GeneroId,TipoLoginId = @TipoLoginId,CiudadId = @CiudadId,PersonaFechaRegistro = @PersonaFechaRegistro,EstadoPersonaId = @EstadoPersonaId,PersonaDireccionLast = @PersonaDireccionLast,PersonaDNI = @PersonaDNI,TipoDocumentoId = @TipoDocumentoId,PersonaGeoReal = @PersonaGeoReal,PersonaClave = @PersonaClave,PersonaUsuarioMod = @PersonaUsuarioMod,PersonaCodigoTelefono = @PersonaCodigoTelefono,PersonaGeoLocalizacionLast = @PersonaGeoLocalizacionLast WHERE PersonaId = @PersonaId ";
        //                break;
        //            case BE.TipoEstado.Insertar:
        //                strSql = "";
        //                break;

        //        }

        //            conx.Conectar();
        //        if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
        //            // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));
        //            conx.CrearComando(strSql);
        //        conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
        //        conx.AsignarParametro("@PersonaNombres", BEObj.PersonaNombres);
        //        conx.AsignarParametro("@PersonaApellidos", BEObj.PersonaApellidos);
        //        conx.AsignarParametro("@PersonaCorreo", BEObj.PersonaCorreo);
        //        conx.AsignarParametro("@PersonaFechaNacimiento", BEObj.PersonaFechaNacimiento);
        //        conx.AsignarParametro("@PersonaTelefono", BEObj.PersonaTelefono);
        //        conx.AsignarParametro("@PersonaUID", BEObj.PersonaUID);
        //        conx.AsignarParametro("@PersonaURLFoto", BEObj.PersonaURLFoto);
        //        conx.AsignarParametro("@PersonaUsuario", BEObj.PersonaUsuario);
        //        conx.AsignarParametro("@PersonaFechaHoraMod", BEObj.PersonaFechaHoraMod);
        //        conx.AsignarParametro("@TipoPersonaId", BEObj.TipoPersonaId);
        //        conx.AsignarParametro("@CiudadId", BEObj.CiudadId);
        //        conx.AsignarParametro("@PersonaFechaRegistro", BEObj.PersonaFechaRegistro);
        //        conx.AsignarParametro("@EstadoPersonaId", BEObj.EstadoPersonaId);
        //        conx.AsignarParametro("@PersonaDireccionLast", BEObj.PersonaDireccionLast);
        //        conx.AsignarParametro("@PersonaDNI", BEObj.PersonaDNI);
        //        conx.AsignarParametro("@TipoDocumentoId", BEObj.TipoDocumentoId);
        //        conx.AsignarParametro("@PersonaGeoReal", BEObj.PersonaGeoReal);
        //        conx.AsignarParametro("@PersonaClave", BEObj.PersonaClave);
        //        conx.AsignarParametro("@PersonaUsuarioMod", BEObj.PersonaUsuarioMod);
        //        conx.AsignarParametro("@PersonaCodigoTelefono", BEObj.PersonaCodigoTelefono);
        //        conx.AsignarParametro("@PersonaGeoLocalizacionLast", BEObj.PersonaGeoLocalizacionLast);

        //        conx.EjecutarComando();

        //            conx.Desconectar();
        //        bolOk = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        conx.Desconectar();               
        //        throw ex;
        //    }
        //    return bolOk;
        //}

        public List<BE.PostLikes> CargarBE(DataRow[] dr)
        {
            List<BE.PostLikes> lst = new List<BE.PostLikes>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.PostLikes CargarBE(DataRow dr)
        {
            BE.PostLikes obj = new BE.PostLikes();
            obj.PostId = Convert.ToDecimal(dr["PostId"].ToString());
            obj.PostLikesId = Convert.ToDecimal(dr["PostLikesId"].ToString());
            obj.PersonaLikesId = Convert.ToDecimal(dr["PersonaLikesId"].ToString());
            obj.PostLikesFechaHora = Convert.ToDateTime( dr["PostLikesFechaHora"].ToString());

            return obj;         
        }
        #endregion

        #region "Listados"

        public List<BE.PostLikes> ObtenerHijos(IEnumerable<decimal> llaves, long personaId=0, params Enum[] relaciones)
        {
            List<BE.PostLikes> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string strPersona = "";
                if (!personaId.Equals(0))
                {
                    strPersona = String.Format(" and PersonaLikesId = {0}", personaId);
                }
                string sql = String.Format(@"SELECT PostId, PostLikesId, PersonaLikesId, PostLikesFechaHora
                                            FROM dbo.PostLikes with(nolock) where PostId in {0}", this.ConcatenarLlaves(llaves) , strPersona);
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

        public List<BE.PostLikes> ListadoxPostPersona(long postId, long personaId)
        {
           List<BE.PostLikes> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string strPersona = "";
                if (!personaId .Equals(0))
                {
                    strPersona = String.Format(" and PersonaLikesId = {0}", personaId);
                }
                string sql = String.Format(@"SELECT PostId, PostLikesId, PersonaLikesId, PostLikesFechaHora
                                            FROM dbo.PostLikes with(nolock) where PostId = {0} {1}", postId,strPersona );
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

        #region "Busqueda"


        #endregion
    }
}
