using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
using System.IO;

namespace BC
{
    public class Post : BCEntidad
    {
        private string campos(string prefijo = "p")
        {
            string strCampos = String.Format(@"{0}.PostId,{0}.TipoPostId,{0}.PostDescripcion,{0}.PostEnlace,{0}.PostContenidoLast,
{0}.PostCompartidoLast,{0}.PostFechaInsercion,{0}.PostUsuario,
{0}.PostUID,{0}.PostLikesLast,{0}.ServAsigId,{0}.PersonaPostId,{0}.PostComentario,
{0}.PostCalificacion,{0}.PostAutorizaPublicacionImagen,
{0}.PostComentarioAprobacion
"
                    , prefijo);
            return strCampos;
        }
        public Post() : base()
        {
        }

        public Post(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.Post BEObj, Boolean isTransaccion = false)
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
                        strSql = "update  dbo.Post set TipoPostId = @TipoPostId,PostDescripcion = @PostDescripcion,PostEnlace = @PostEnlace,PostContenidoLast = @PostContenidoLast,PostCompartidoLast = @PostCompartidoLast,PostFechaInsercion = @PostFechaInsercion,PostUsuario = @PostUsuario,PostLikesLast = @PostLikesLast,ServAsigId = @ServAsigId,PersonaPostId = @PersonaPostId,PostComentario = @PostComentario,PostCalificacion = @PostCalificacion,PostAutorizaPublicacionImagen = @PostAutorizaPublicacionImagen,PostComentarioAprobacion = @PostComentarioAprobacion where PostId = @PostId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @" SET IDENTITY_INSERT dbo.Post ON;
                                      insert into dbo.Post  (PostId,TipoPostId,PostDescripcion,PostEnlace,PostContenidoLast,PostCompartidoLast,PostFechaInsercion,PostUsuario,PostUID,PostLikesLast,ServAsigId,PersonaPostId,PostComentario,PostCalificacion,PostAutorizaPublicacionImagen,PostComentarioAprobacion)                                 
                                      values(@PostId, @TipoPostId, @PostDescripcion, @PostEnlace, @PostContenidoLast, @PostCompartidoLast, @PostFechaInsercion, @PostUsuario, @PostUID,@PostLikesLast, @ServAsigId, @PersonaPostId, @PostComentario, @PostCalificacion, @PostAutorizaPublicacionImagen, @PostComentarioAprobacion); 
                                    SET IDENTITY_INSERT dbo.Post OFF";
                        break;

                }

                if (isTransaccion)
                    conx = dbConexion;
                else
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                }
                if (BEObj.TipoEstado != BE.TipoEstado.SinAccion )
                {
                               
                    if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    {
                        BEObj.PostId = System.Convert.ToDecimal(conx.ObtenerValor("select isnull(max(PostId),0) + 1 from dbo.Post with (nolock);"));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@PostId", BEObj.PostId);
                    conx.AsignarParametro("@TipoPostId", BEObj.TipoPostId);
                    conx.AsignarParametro("@PostDescripcion", BEObj.PostDescripcion);
                    conx.AsignarParametro("@PostEnlace", BEObj.PostEnlace);
                    conx.AsignarParametro("@PostContenidoLast", BEObj.PostContenidoLast);
                    conx.AsignarParametro("@PostCompartidoLast", BEObj.PostCompartidoLast);
                    conx.AsignarParametro("@PostFechaInsercion", BEObj.PostFechaInsercion);
                    conx.AsignarParametro("@PostUsuario", BEObj.PostUsuario);
                    conx.AsignarParametro("@PostUID", BEObj.PostUID);
                    conx.AsignarParametro("@PostLikesLast", BEObj.PostLikesLast);
                    if (BEObj.ServAsigId == "") BEObj.ServAsigId = null;
                    conx.AsignarParametro("@ServAsigId", BEObj.ServAsigId);
                    conx.AsignarParametro("@PersonaPostId", BEObj.PersonaPostId);
                    conx.AsignarParametro("@PostComentario", BEObj.PostComentario);
                    conx.AsignarParametro("@PostCalificacion", BEObj.PostCalificacion);
                    conx.AsignarParametro("@PostAutorizaPublicacionImagen", BEObj.PostAutorizaPublicacionImagen);
                    conx.AsignarParametro("@PostComentarioAprobacion", BEObj.PostComentarioAprobacion);
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

      
        public List<BE.Post> CargarBE(DataRow[] dr)
        {
            List<BE.Post> lst = new List<BE.Post>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Post CargarBE(DataRow dr)
        {
            BE.Post obj = new BE.Post();
            obj.PostId = Convert.ToDecimal(dr["PostId"].ToString());
            obj.TipoPostId = Convert.ToDecimal(dr["TipoPostId"].ToString());
            obj.PostDescripcion = dr["PostDescripcion"].ToString();
            obj.PostEnlace = dr["PostEnlace"].ToString();
            obj.PostContenidoLast = Convert.ToDecimal(dr["PostContenidoLast"].ToString());
            obj.PostCompartidoLast = Convert.ToDecimal(dr["PostCompartidoLast"].ToString());
            obj.PostFechaInsercion =Convert.ToDateTime(dr["PostFechaInsercion"].ToString());
            obj.PostUsuario = dr["PostUsuario"].ToString();
            obj.PostUID = dr["PostUID"].ToString();
            obj.PostLikesLast = Convert.ToDecimal(dr["PostLikesLast"].ToString());
            if (dr["ServAsigId"] != DBNull .Value )
                obj.ServAsigId = dr["ServAsigId"].ToString();
            obj.PersonaPostId = Convert.ToDecimal(dr["PersonaPostId"].ToString());
            obj.PostComentario =dr["PostComentario"].ToString();
            obj.PostCalificacion = Convert.ToDecimal(dr["PostCalificacion"].ToString());
            if (dr["PostAutorizaPublicacionImagen"] != DBNull.Value)
                obj.PostAutorizaPublicacionImagen = Convert.ToBoolean(dr["PostAutorizaPublicacionImagen"].ToString());
            if (dr["PostComentarioAprobacion"] != DBNull.Value)
                obj.PostComentarioAprobacion = Convert.ToBoolean(dr["PostComentarioAprobacion"].ToString());
            try
            {
                obj.PostCountLikes = Convert.ToInt64 (dr["PostCountLikes"].ToString());
            }
            catch (Exception ex)
            {               
            }
            try
            {
                obj.PostCountShares = Convert.ToInt64(dr["PostCountShares"].ToString());
            }
            catch (Exception ex)
            {
            }
            return obj;
        }

        public void CargarRelaciones(ref List<BE.Post> colObj,long personaId = 0,params Enum[] relaciones)
        {
               if (relaciones ==null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            List<BE.PostLikes> colPostLikes = null;
            List<BE.PostContenido> colPosContendio = null;
            List<BE.ServAsig> colServAsig = null;
            foreach ( Enum clase in relaciones) {
                if (clase.Equals(BE.Post.relPost.PostContenido))
                {
                    BC.PostContenido bcPosContenido = new BC.PostContenido(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.PostId).Distinct();
                    colPosContendio = bcPosContenido.ObtenerHijos(llaves, relaciones);
                    bcPosContenido = null;
                }
                if (clase.Equals(BE.Post.relPost.PostLikes))
                {
                    BC.PostLikes bcPostLikes = new BC.PostLikes(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.PostId).Distinct();
                    colPostLikes = bcPostLikes.ObtenerHijos(llaves, 0, relaciones);
                    bcPostLikes = null;
                }
                if (clase.Equals (BE.Post.relPost.PostLikesPersona))
                {
                    BC.PostLikes bcPostLikes = new BC.PostLikes(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.PostId).Distinct();
                    colPostLikes = bcPostLikes.ObtenerHijos(llaves, personaId, relaciones);
                    bcPostLikes = null;
                }
                if (clase.Equals(BE.Post.relPost.ServAsig))
                {
                    IEnumerable<string> sllaves;
                    BC.ServAsig  bcServAsig = new BC.ServAsig(cadenaConexion);
                    sllaves = (from elemento in colObj where elemento.ServAsigId != null && elemento.ServAsigId != "" select elemento.ServAsigId).Distinct();
                    colServAsig = bcServAsig.ObtenerHijos(sllaves,"", relaciones);
                    bcServAsig = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {
                    if(colPostLikes != null && colPostLikes .Count > 0)
                    {
                        item.postLikes = (from elemento in colPostLikes where elemento.PostId == item.PostId select elemento).ToList(); 
                    }

                    if (colPosContendio != null && colPosContendio.Count > 0)
                    {
                        item.PostContenido = (from elemento in colPosContendio where elemento.PostId == item.PostId select elemento).ToList();
                    }

                    if (colServAsig != null && colServAsig.Count > 0)
                    {
                        item.servAsig = (from elemento in colServAsig where elemento.ServAsigId == item.ServAsigId select elemento).ToList().FirstOrDefault() ;
                    }
                }
            }
        }

        public void CargarRelaciones(ref BE.Post obj, long  personaId = 0, string lang="",params Enum[] relaciones)
        {
            if (relaciones == null || obj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;          
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.Post.relPost.PostContenido))
                {
                    BC.PostContenido bcPosContenido = new BC.PostContenido(cadenaConexion);
                    llaves = new decimal[] { obj.PostId };
                    obj.PostContenido = bcPosContenido.ObtenerHijos(llaves, relaciones);
                    bcPosContenido = null;
                }
                if (clase.Equals(BE.Post.relPost.PostLikes))
                {
                    BC.PostLikes bcPostLikes = new BC.PostLikes(cadenaConexion);
                    llaves = new decimal[] { obj.PostId };
                    obj.postLikes = bcPostLikes.ObtenerHijos(llaves, 0, relaciones);
                    bcPostLikes = null;
                }
                if (clase.Equals(BE.Post.relPost.PostLikesPersona))
                {
                    BC.PostLikes bcPostLikes = new BC.PostLikes(cadenaConexion);
                    llaves = new decimal[] { obj.PostId };
                    obj.postLikes = bcPostLikes.ObtenerHijos(llaves, personaId , relaciones);
                    bcPostLikes = null;
                }
                if (clase.Equals(BE.Post.relPost.ServAsig))
                {
                    IEnumerable<string> sllaves;
                    if (obj.ServAsigId != null || obj.ServAsigId != "")
                    {
                        BC.ServAsig bcServAsig = new BC.ServAsig(cadenaConexion);
                        sllaves = new string[] { obj.ServAsigId };
                        obj.servAsig  = bcServAsig.ObtenerHijos(sllaves,lang, relaciones).FirstOrDefault() ;
                        bcServAsig = null;

                    }                  
                }

            }          
        }

        public Boolean RegistrarPost(ref BE.Post obj)
        {
            Boolean bolOk = false;
            Boolean bolInicio = true;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                if (dbConexion == null)
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                    dbConexion = conx;
                }
                else
                {
                    conx = dbConexion;
                    bolInicio = false;
                }

                bolOk = Actualizar(ref obj);
                if(bolOk)
                {
                    BC.PostContenido bcPostContenido = new BC.PostContenido(cadenaConexion);
                    bcPostContenido.dbConexion = conx;
                    if ( obj.TipoPostId.Equals((decimal)3))
                    {
                        List<BE.PostContenido> lst = new List<BE.PostContenido >();
                        BE.PostContenido it1 = new BE.PostContenido ();
                        it1.PostId = obj.PostId;
                        it1.PostContenidoId = 1;
                        it1.PostContenidoImagen = "1.png";
                        it1.PostContenidoVideo = null;
                        it1.PostContenidoVisible = true;
                        it1.TipoEstado = TipoEstado.Insertar;

                        BE.PostContenido it2 = new BE.PostContenido();
                        it2.PostId = obj.PostId;
                        it2.PostContenidoId = 2;
                        it2.PostContenidoImagen = "3.png";
                        it2.PostContenidoVideo = null;
                        it2.PostContenidoVisible = true;
                        it2.TipoEstado = TipoEstado.Insertar;

                        BE.PostContenido it3 = new BE.PostContenido();
                        it3.PostId = obj.PostId;
                        it3.PostContenidoId = 3;
                        it3.PostContenidoImagen = "4.png";
                        it3.PostContenidoVideo = null;
                        it3.PostContenidoVisible = true;
                        it3.TipoEstado = TipoEstado.Insertar;

                        BE.PostContenido it4 = new BE.PostContenido();
                        it4.PostId = obj.PostId;
                        it4.PostContenidoId = 4;
                        it4.PostContenidoImagen = "5.png";
                        it4.PostContenidoVideo = null;
                        it4.PostContenidoVisible = true;
                        it4.TipoEstado = TipoEstado.Insertar;

                        lst.Add(it1);
                        lst.Add(it2);
                        lst.Add(it3);
                        lst.Add(it4);

                        obj.PostContenido = lst;
                        obj.PostContenidoLast = 4;

                        obj.TipoEstado = TipoEstado.Modificar;
                        Actualizar(ref obj);
                    }
                    if (obj.PostContenido != null)
                    {
                        foreach (BE.PostContenido item in obj.PostContenido)
                        {
                            BE.PostContenido it = item;
                            it.PostId = obj.PostId;
                            bolOk = bcPostContenido.Actualizar(ref it);
                            item.PostContenidoId = it.PostContenidoId;
                        }
                    }
                   
                }

                if (bolInicio)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }
                bolOk = true;
            }
            catch(Exception ex)
            {
                bolOk = false;
                if (bolInicio)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar(); 
                }
            }
            return bolOk;
        }

        public Boolean RegistrarPostContenido(ref BE.Post obj)
        {
            Boolean bolOk = false;
            Boolean bolInicio = true;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                if (dbConexion == null)
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                    dbConexion = conx;
                }
                else
                {
                    conx = dbConexion;
                    bolInicio = false;
                }

                bolOk = Actualizar(ref obj);
                if (bolOk)
                {
                    BC.PostContenido bcPostContenido = new BC.PostContenido(cadenaConexion);
                    bcPostContenido.dbConexion = conx;
                
                    if (obj.PostContenido != null)
                    {
                        foreach (BE.PostContenido item in obj.PostContenido)
                        {
                            BE.PostContenido it = item;
                            it.PostId = obj.PostId;
                            bolOk = bcPostContenido.Actualizar(ref it);
                            item.PostContenidoId = it.PostContenidoId;
                        }
                    }

                }

                if (bolInicio)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }
                bolOk = true;
            }
            catch (Exception ex)
            {
                bolOk = false;
                if (bolInicio)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                }
            }
            return bolOk;
        }
        #endregion

        #region "Listados"
        public List<BE.Post> ObtenerHijos(IEnumerable<string> llaves, string lang = "", params Enum[] relaciones)
        {
            List<BE.Post> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Post p with(nolock) where p.ServAsigId in {1}", campos("p"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, 0, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public List<BE.Post> ListadoPaginado(long personaId,int index, int max, params Enum[] relaciones)
        {
            List<BE.Post> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string strCountLikes = ", 0 PostCountLikes ";
                string strCountShare = ", 0 PostCountShares";
                if (relaciones .Contains (BE.Post.relPost .PostCountLikes))
                {
                    strCountLikes = ", dbo.ObtenerLikes(PostId) PostCountLikes";
                }

                if (relaciones.Contains(BE.Post.relPost.PostCountShare))
                {
                    strCountShare = ", dbo.ObtenerShares(PostId) PostCountShares";
                }

                string sql = String.Format(@"SELECT PostId, TipoPostId, PostDescripcion, PostEnlace, PostContenidoLast, PostCompartidoLast, PostFechaInsercion
                                                , PostUsuario, PostUID, PostLikesLast, ServAsigId, PersonaPostId, PostComentario, PostCalificacion
                                                , PostAutorizaPublicacionImagen, PostComentarioAprobacion,PostCountLikes,PostCountShares from (
                                    SELECT PostId, TipoPostId, PostDescripcion, PostEnlace, PostContenidoLast, PostCompartidoLast, PostFechaInsercion
                                    , PostUsuario, PostUID, PostLikesLast, ServAsigId, PersonaPostId, PostComentario, PostCalificacion
                                    , PostAutorizaPublicacionImagen, PostComentarioAprobacion {3} {4}
                                    FROM dbo.Post with(nolock)
                                    WHERE PersonaPostId= {0} 
	                                    and isnull(PostAutorizaPublicacionImagen,1) = 
	                                    ( case TipoPostId when 1 then 1 else isnull(PostAutorizaPublicacionImagen,1) end)
                                    union all
                                    SELECT PostId, TipoPostId, PostDescripcion, PostEnlace, PostContenidoLast, PostCompartidoLast, PostFechaInsercion
                                        , PostUsuario, PostUID, PostLikesLast, pos.ServAsigId, PersonaPostId, PostComentario, PostCalificacion
                                        , PostAutorizaPublicacionImagen, PostComentarioAprobacion {3} {4}
                                        FROM dbo.Post pos with(nolock) inner join dbo.ServAsig sra with(nolock) on pos.ServAsigId = sra.ServAsigId
                                        WHERE sra.ProveedorId={0} and sra.StatusServAsigId in(3,4) and sra.ServAsigFHPago is not null
			                                    and isnull(PostAutorizaPublicacionImagen,1) = ( case TipoPostId when 1 then 1 else isnull(PostAutorizaPublicacionImagen,1) end)
                            )tmp
                            ORDER BY PostFechaInsercion DESC
                            OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY;", personaId, index -1, max,strCountLikes ,strCountShare);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,personaId ,relaciones );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public int CantidadPost(long personaId)
        {
             int obj = 0;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"SELECT PostId from (
                                    SELECT PostId
                                    FROM dbo.Post with(nolock)
                                    WHERE PersonaPostId= {0} 
	                                    and isnull(PostAutorizaPublicacionImagen,1) = 
	                                    ( case TipoPostId when 1 then 1 else isnull(PostAutorizaPublicacionImagen,1) end)
                                    union all
                                    SELECT PostId
                                        FROM dbo.Post pos with(nolock) inner join dbo.ServAsig sra with(nolock) on pos.ServAsigId = sra.ServAsigId
                                        WHERE sra.ProveedorId={0} and sra.StatusServAsigId in(3,4) and sra.ServAsigFHPago is not null
			                                    and isnull(PostAutorizaPublicacionImagen,1) = ( case TipoPostId when 1 then 1 else isnull(PostAutorizaPublicacionImagen,1) end)
                            )tmp;", personaId);
                DataRow[] dr = conx.ObtenerFilas(sql);

                if (dr != null)
                {
                    obj = dr.Length;
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

        public BE.Post BuscarPostxId(decimal postId)
        {
            BE.Post obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT *
                                            FROM dbo.Post with(nolock) where PostId={0}", postId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    BC.PostContenido bcPostContenido = new BC.PostContenido(cadenaConexion);
                    bcPostContenido.dbConexion = conx;
                    obj.PostContenido = bcPostContenido.BuscarPostContenidoxId(obj.PostId);
                    obj.servAsig = null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public BE.Post BuscarPostxId(decimal postId, decimal personaId=0, params Enum[] relaciones)
        {
            BE.Post obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string strCountLikes = ", 0 PostCountLikes ";
                string strCountShare = ", 0 PostCountShares";
                if (relaciones.Contains(BE.Post.relPost.PostCountLikes))
                {
                    strCountLikes = ", dbo.ObtenerLikes(PostId) PostCountLikes";
                }

                if (relaciones.Contains(BE.Post.relPost.PostCountShare))
                {
                    strCountShare = ", dbo.ObtenerShares(PostId) PostCountShares";
                }
                string sql = String.Format(@"SELECT PostId, TipoPostId, PostDescripcion, PostEnlace, PostContenidoLast, PostCompartidoLast, PostFechaInsercion
                                    , PostUsuario, PostUID, PostLikesLast, ServAsigId, PersonaPostId, PostComentario, PostCalificacion
                                    , PostAutorizaPublicacionImagen, PostComentarioAprobacion {1} {2}
                                    FROM dbo.Post with(nolock) where PostId={0} ", postId,strCountLikes , strCountShare );
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, (long)personaId, "", relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<BE.Post> postGet_Index(decimal PersonaId, int IndexInicio, int CantidadMax)
        {
            List<BE.Post> Listaobj = null;
            BE.Post obj = null;
            try
            {
              
                DataTable dt = new DataTable();
                ClaseConexion conx = new ClaseConexion(cadenaConexion);
                BC.Post bcPost = new BC.Post(cadenaConexion);
                bcPost.dbConexion = conx;
                BC.PostContenido bcPostContenido = new BC.PostContenido(cadenaConexion);
                bcPostContenido.dbConexion = conx;
                dt = conx.ObtenerTablaSP("PostGet_Index");
                conx.AsignarParametro("@PersonaId", PersonaId);
                conx.AsignarParametro("@IndexInicio", IndexInicio);
                conx.AsignarParametro("@CantidadMax", CantidadMax);
                foreach (DataRow dr in dt.Rows)
                {
                    // On all tables' columns
                    decimal PostId = Convert.ToDecimal(dr["PostId"].ToString());
                   obj = bcPost.BuscarPostxId(Convert.ToDecimal(dr["PostId"].ToString()));

                    Listaobj.Add(obj);
                     
                    
                }


            }
            catch ( Exception ex)
            {

                throw;
            }

            return Listaobj;

        }

        public Boolean ActualizarPost(ref BE.Post obj)
        {
            Boolean bolOk = false;
            Boolean bolInicio = true;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                if (dbConexion == null)
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                    dbConexion = conx;
                }
                else
                {
                    conx = dbConexion;
                    bolInicio = false;
                }

                bolOk = Actualizar(ref obj);
                if (bolOk)
                {
                    BC.PostContenido bcPostContenido = new BC.PostContenido(cadenaConexion);
                    bcPostContenido.dbConexion = conx;
                    if (obj.TipoPostId.Equals((decimal)3))
                    {
                        List<BE.PostContenido> lst = new List<BE.PostContenido>();
                        BE.PostContenido it1 = new BE.PostContenido();
                        it1.PostId = obj.PostId;
                        it1.PostContenidoId = 1;
                        it1.PostContenidoImagen = "1.png";
                        it1.PostContenidoVideo = null;
                        it1.PostContenidoVisible = true;
                        it1.TipoEstado = TipoEstado.Insertar;

                        BE.PostContenido it2 = new BE.PostContenido();
                        it2.PostId = obj.PostId;
                        it2.PostContenidoId = 2;
                        it2.PostContenidoImagen = "3.png";
                        it2.PostContenidoVideo = null;
                        it2.PostContenidoVisible = true;
                        it2.TipoEstado = TipoEstado.Insertar;

                        BE.PostContenido it3 = new BE.PostContenido();
                        it3.PostId = obj.PostId;
                        it3.PostContenidoId = 3;
                        it3.PostContenidoImagen = "4.png";
                        it3.PostContenidoVideo = null;
                        it3.PostContenidoVisible = true;
                        it3.TipoEstado = TipoEstado.Insertar;

                        BE.PostContenido it4 = new BE.PostContenido();
                        it4.PostId = obj.PostId;
                        it4.PostContenidoId = 4;
                        it4.PostContenidoImagen = "5.png";
                        it4.PostContenidoVideo = null;
                        it4.PostContenidoVisible = true;
                        it4.TipoEstado = TipoEstado.Insertar;

                        lst.Add(it1);
                        lst.Add(it2);
                        lst.Add(it3);
                        lst.Add(it4);

                        obj.PostContenido = lst;
                        obj.PostContenidoLast = 4;

                        obj.TipoEstado = TipoEstado.Modificar;
                        Actualizar(ref obj);
                    }
                    if (obj.PostContenido != null)
                    {
                   
                        foreach (BE.PostContenido item in obj.PostContenido)
                        {

                            BE.PostContenido it = item;
                            it.TipoEstado = BE.TipoEstado.Modificar;
                            it.PostId = obj.PostId;
                            bolOk = bcPostContenido.Actualizar(ref it);
                            item.PostContenidoId = it.PostContenidoId;
                        }
                    }

                }

                if (bolInicio)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }
                bolOk = true;
            }
            catch (Exception ex)
            {
                bolOk = false;
                if (bolInicio)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                }
            }
            return bolOk;
        }

        public List<BE.Post>  ToList()
        {
            List<BE.Post> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                
                string sql = String.Format(@"select * from with (nolock)  dbo.Post");
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);                  
                    foreach (BE.Post post in obj)                         {
                            BC.PostContenido bcPostContenido = new BC.PostContenido(cadenaConexion);
                            bcPostContenido.dbConexion = conx;
                        post.PostContenido = bcPostContenido.BuscarPostContenidoxId(post.PostId);
                          
                     
                        if (post.ServAsigId != "")
                        {
                            BC.ServAsig bcservAsig = new BC.ServAsig(cadenaConexion);
                           BC.ServAsigCosto bcservAsigCosto = new BC.ServAsigCosto(cadenaConexion);
                            BC.RequiereServicio bcsrequiereservicio = new BC.RequiereServicio(cadenaConexion);
                            post.servAsig = bcservAsig.BuscarServAsigxId(Convert.ToString(post.ServAsigId));
                            if (post.servAsig.StatusServAsigId != null)
                            {
                                BC.StatusServAsig bcstatusServAsig = new BC.StatusServAsig(cadenaConexion);
                              post.servAsig.statusServAsig=  bcstatusServAsig.BuscarStatusServicioxId(Convert.ToDecimal(post.servAsig.StatusServAsigId));
                            }

                            post.servAsig.servAsigCosto = bcservAsigCosto.BuscarServAsigCostoxId(post.ServAsigId);
                            post.servAsig.post = null;
                            post.servAsig.requiereServicio = bcsrequiereservicio.BuscarRequiereServicioxId(post.servAsig.RequiereServicioId,"");


                        }
                    }
                    

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public Boolean Add(ref BE.Post obj)
        {
            Boolean bolOk = false;
            Boolean bolInicio = true;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                if (dbConexion == null) { conx.Conectar(); conx.ComenzarTransaccion(); dbConexion = conx; }else { conx = dbConexion; bolInicio = false; }
                bolOk = Actualizar(ref obj);
                if (bolInicio)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }
            }
            catch ( Exception ex)
            {

                if (bolInicio)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                }
            }
            return bolOk;
        }

        public string SubirArchivo_MediaIcons_MediaPost(string ServAsigId)
        {
            string nombreImagen="";
            string ServicioPersonaURLFoto;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select ServicioPersonaURLFoto from ServAsig sa with(nolock)
inner join RequiereServicioProveedores rsp with (nolock) 
on sa.RequiereServicioId = rsp.RequiereServicioId 
inner join ServicioPersona sp with(nolock)
on rsp.ServicioPersonaId = sp.ServicioPersonaId 
and StatusRequiereId = 4 where ServAsigId = @ServAsigId",ServAsigId);
             
                ServicioPersonaURLFoto = Convert.ToString(conx.ObtenerValor(sql));  
           
               string archivoOrigen = System.Configuration.ConfigurationSettings.AppSettings["archivoOrigen"];
                string rutaDestino = System.Configuration.ConfigurationSettings.AppSettings["rutaDestino"];
               archivoOrigen = archivoOrigen + ServicioPersonaURLFoto;//(fullPath);
                nombreImagen = String.Concat(Path.GetFileNameWithoutExtension(archivoOrigen), "_", DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.Second + DateTime.Now.Millisecond, Path.GetExtension(archivoOrigen));
                File.Copy(archivoOrigen, Path.Combine(rutaDestino, nombreImagen));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
             
            }

            return nombreImagen;
        }


        #endregion
    }
}
