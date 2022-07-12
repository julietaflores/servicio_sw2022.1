using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using DotNetOpenAuth.OpenId;
using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.WepApi.Models;
using ServiciosWeb.WepApi.Models.Entities;
using System.Web.Http.Cors;

namespace ServiciosWeb.WepApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostController : ApiController
    {
        private PostConnection db = new PostConnection();
        ServiciosWeb.Datos.Modelo.clasePost PersonaMensaje = new ServiciosWeb.Datos.Modelo.clasePost();
        ServiciosWeb.Datos.Modelo.Respuesta<List<Post>> Respuesta = new ServiciosWeb.Datos.Modelo.Respuesta<List<Post>>();
        ServiciosWeb.Datos.Modelo.Respuesta<PostCont> RespuestaCont = new ServiciosWeb.Datos.Modelo.Respuesta<PostCont>();
        ServiciosWeb.Datos.Modelo.Respuesta<List<PostCont>> ListaRespuestaCont = new ServiciosWeb.Datos.Modelo.Respuesta<List<PostCont>>();
        SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["con"]);

        private PostManager postManager = null;
        public PostController()
        {
            postManager = new PostManager("cadenaCnx");
            
        }
        
        // GET: api/Post
      
        public IHttpActionResult GetPost()
        {
            var Listado = db.Post.ToList();
            if (Listado == null)
            {
                Respuesta.estado = 0;
                Respuesta.mensaje = "Listado null";
            }
            else
            {
                Respuesta.estado = 1;
                Respuesta.mensaje = "";
            }
            Respuesta.valor = Listado;



            return Ok(Respuesta);
        }

        [ResponseType(typeof(Persona))]
        [ResponseType(typeof(Post))]////MetodO DUPLICADO Y PROBADO
        [HttpGet]
        [Route("api/Post")]
        public IHttpActionResult GetPost_PerId(decimal PersonaId)
        {
            // Persona persona = db.Persona.Find(PersonaTokenId);
            var Listado = db.Post.Where(x => x.PersonaPostId == PersonaId).ToList();

            if (Listado == null)
            {
                Respuesta.estado = 2;
                Respuesta.mensaje = "Listado null";
            }
            else
            {
                Respuesta.estado = 1;
                Respuesta.mensaje = "";
            }
            Respuesta.valor = Listado;



            return Ok(Respuesta);
        }
        // POST: api/Post
        /// <summary>t
        /// Insertar a la tabla post
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Post))]
        [HttpPost]
        [Route("api/Post")]
        public IHttpActionResult PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                var error1 = BadRequest(ModelState).ToString();
            }

            db.Post.Add(post);
            if (db.SaveChanges() > 0)
            {
                PersonaMensaje.estado = 1;
                PersonaMensaje.valor = post;
                PersonaMensaje.mensaje = "";
            }
            else
            {
                PersonaMensaje.estado = 2;
                PersonaMensaje.mensaje = "Informacion no Guardada";
            }

            //  return CreatedAtRoute("DefaultApi", new { id = persona.PersonaId }, persona);

            ////
            return Ok(PersonaMensaje);
        }
       
        [HttpPost]
        [Route("api/savePost")]
        public IHttpActionResult savePost(BE.Post post)
        {
            if (!ModelState.IsValid)
            {
                var error1 = BadRequest(ModelState).ToString();
            }
            Respuesta resp = new Respuesta();
            resp = postManager.savePost(post);
            return Ok(resp);
        }

        [ResponseType(typeof(Post))]
        [HttpGet]
        [Route("api/postpaginacion")]
        public IHttpActionResult PostPaginacion(long personaId,int index, int max,String v=null,String d=null)
        {
            Respuesta resp = new Respuesta();
            String message = "";
            if(validarVersion(ref message, v, d))
            {
                resp = postManager.PostPaginacion(personaId, index, max);
                if (index == 1)
                {
                    resp.mensaje = postManager.PostCantidad(personaId).valor.ToString();
                }
                else
                {
                    resp.mensaje = "1";
                }                
            }
            else
            {
                resp.estado = 2;
                resp.mensaje = message;
            }
            
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/savePostLikes")]
        public IHttpActionResult savePostLikes(BE.PostLikes postLikes)
        {
            if (!ModelState.IsValid)
            {
                var error1 = BadRequest(ModelState).ToString();
            }
            Respuesta resp = new Respuesta();
            resp = postManager.savePostLikes(postLikes, (long)postLikes .PersonaLikesId);
            return Ok(resp);
        }

        /////////////////////////
        [HttpPost]
        [Route("api/Post")]
        public IHttpActionResult PostContenido(PostContenido postContenido)
        {
            int resp, resPC = 0;
            string valorServAsigId = "";
            decimal valorPersonaPostId = 0;
            decimal ValorPostId = 0;
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                ///////////////////////////////////////////Pais
                ValorPostId = postContenido.PostId;
                //  PostContenido  posCon= (postCont.PostContenido[0]);

                    SqlCommand sqlCmd1 = new SqlCommand("InsertarPostContenido", conexion);
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.Parameters.AddWithValue("@PostId", ValorPostId);
                    sqlCmd1.Parameters.AddWithValue("@PostContenidoId",postContenido.PostContenidoId);
                    sqlCmd1.Parameters.AddWithValue("@PostContenidoImagen",postContenido.PostContenidoImagen);
                    if (postContenido.PostContenidoVideo == null)
                    {
                        sqlCmd1.Parameters.Add("@PostContenidoVideo", SqlDbType.VarChar).Value = System.Data.SqlTypes.SqlString.Null;
                    }
                    else
                    {
                        sqlCmd1.Parameters.AddWithValue("@PostContenidoVideo", postContenido.PostContenidoVideo);
                    }
                    if (postContenido.PostContenidoVisible == null)
                    {
                        sqlCmd1.Parameters.Add("@PostContenidoVisible", SqlDbType.Bit).Value = System.Data.SqlTypes.SqlString.Null;
                    }
                    else
                    {
                        sqlCmd1.Parameters.AddWithValue("@PostContenidoVisible", postContenido.PostContenidoVisible);
                    }
                    resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());

                

                if ((resPC != 0))
                {
                    RespuestaCont.mensaje = "OK";
                    RespuestaCont.estado = 1;

                    RespuestaCont.valor = null;


                }
            }
            catch (Exception ex)
            {
                RespuestaCont.mensaje = ex.Message;
                RespuestaCont.estado = 2;
                RespuestaCont.valor = null;
            }
            finally
            {
                conexion.Close();
            }
            return Ok(RespuestaCont);
        }


        //////////////////////////

        // POST: api/Post
        /// <summary>
        /// Insertar a la tabla post
        /// </summary>

        /// <returns></returns>
        [ResponseType(typeof(PostCont))]
        [HttpGet]
        [Route("api/PostPostContenido")]
        public ServiciosWeb.Datos.Modelo.Respuesta<PostCont> GetPostContenido(decimal PostId)
        {
            if (conexion.State != ConnectionState.Open) conexion.Open();
            ///////////////////////////////////////////Post
            try
            {
                SqlCommand sqlCmd = new SqlCommand("VerPost", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@PostId", PostId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Post Post = dt.AsEnumerable().Select
              (row => new Post
              {
                  PostId = row.Field<decimal?>(0).GetValueOrDefault(),
                  TipoPostId = row.Field<decimal>(1),
                  PostDescripcion = row.Field<string>(2),
                  PostEnlace = row.Field<string>(3),
                  PostContenidoLast = row.Field<decimal>(4),
                  PostCompartidoLast = row.Field<decimal>(5),
                  PostFechaInsercion = row.Field<DateTime>(6),
                  PostUsuario = row.Field<string>(7),
                  PostUID = row.Field<string>(8),
                  PostLikesLast = row.Field<decimal>(9),
                  ServAsigId = row.Field<string>(10),
                  PersonaPostId = row.Field<decimal?>(11),
                  PostComentario = row.Field<string>(12),
                  PostCalificacion = row.Field<decimal?>(13),
                  PostAutorizaPublicacionImagen = row.Field<bool?>(14),
                  PostComentarioAprobacion = row.Field<bool?>(15)

              }).FirstOrDefault();
                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();

                ////////////////////////////////////////////////PostContenido
                SqlCommand sqlCmd1 = new SqlCommand("VerPost_Contenido", conexion);
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.Parameters.AddWithValue("@PostId", PostId);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                List<PostContenido> ListPostContenido = dt1.AsEnumerable().Select
              (row => new PostContenido
              {
                  PostId = row.Field<decimal?>(0).GetValueOrDefault(),
                  PostContenidoId = row.Field<decimal>(1),
                  PostContenidoImagen = row.Field<string>(2),
                  PostContenidoVideo = row.Field<string>(3),
                  PostContenidoVisible = row.Field<bool?>(4)

              }).ToList();
                sqlCmd1.Parameters.Clear();
                da1.Dispose();
                dt1.Dispose();

                /////////////////////////////////////////////////////////////PostCont
                PostCont postCont = new PostCont();
                postCont.PostId = Post.PostId;
                postCont.TipoPostId = Post.TipoPostId;
                postCont.PostDescripcion = Post.PostDescripcion;
                postCont.PostEnlace = Post.PostEnlace;
                postCont.PostContenidoLast = Post.PostContenidoLast;
                postCont.PostCompartidoLast = Post.PostCompartidoLast;
                postCont.PostFechaInsercion = Post.PostFechaInsercion;
                postCont.PostUsuario = Post.PostUsuario;
                postCont.PostUID = Post.PostUID;
                postCont.PostLikesLast = Post.PostLikesLast;
                postCont.ServAsigId = Post.ServAsigId;
                postCont.PersonaPostId = Post.PersonaPostId;
                postCont.PostComentario = Post.PostComentario;
                postCont.PostCalificacion = Post.PostCalificacion;
                postCont.PostAutorizaPublicacionImagen = Post.PostAutorizaPublicacionImagen;
                postCont.PostComentarioAprobacion = Post.PostComentarioAprobacion;
                postCont.PostContenido = ListPostContenido;

                RespuestaCont.estado = 1;
                RespuestaCont.valor = postCont;
                RespuestaCont.mensaje = "OK";
                return RespuestaCont;
            }
            catch (Exception ex)
            {

                RespuestaCont.estado = 2;
                RespuestaCont.valor = null;
                RespuestaCont.mensaje = ex.Message;
                return RespuestaCont;
            }
            finally
            {
                conexion.Close();
            }
            
        }

        // POST: api/Post
        /// <summary>
        /// Insertar a la tabla post
        /// </summary>

        /// <returns></returns>
        [ResponseType(typeof(Post))]
        [HttpPost]
        [Route("api/PostPostContenido")]
        public IHttpActionResult PostPostContenido(PostCont postCont)
        {
            int resp,resPC = 0;
            string valorServAsigId = "";
            decimal valorPersonaPostId = 0;
            int ValorPostId = 0;
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                ///////////////////////////////////////////Pais

                SqlCommand sqlCmd = new SqlCommand("InsertarPost", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //sqlCmd.Parameters.AddWithValue("@PostId", postCont.PostId);
                sqlCmd.Parameters.AddWithValue("@TipoPostId", postCont.TipoPostId);                  
                sqlCmd.Parameters.AddWithValue("@PostDescripcion", postCont.PostDescripcion);
                sqlCmd.Parameters.AddWithValue("@PostEnlace", postCont.PostEnlace);
                sqlCmd.Parameters.AddWithValue("@PostContenidoLast", postCont.PostContenidoLast);
                sqlCmd.Parameters.AddWithValue("@PostCompartidoLast", postCont.PostCompartidoLast);
                sqlCmd.Parameters.AddWithValue("@PostFechaInsercion", postCont.PostFechaInsercion);
                sqlCmd.Parameters.AddWithValue("@PostUsuario", "administrador");
                
                if (postCont.PostUID == null)
                {
                    sqlCmd.Parameters.Add("@PostUID", SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostUID", postCont.PostUID);

                }
                sqlCmd.Parameters.AddWithValue("@PostLikesLast", postCont.PostLikesLast);
              

                valorServAsigId = postCont.ServAsigId;
                if (valorServAsigId==null)
                {
                    sqlCmd.Parameters.Add("@ServAsigId", SqlDbType.VarChar).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@ServAsigId", postCont.ServAsigId);

                }
               
                if (postCont.PersonaPostId == null)
                {
                    sqlCmd.Parameters.Add("@PersonaPostId", SqlDbType.Decimal).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PersonaPostId", postCont.PersonaPostId);

                }

                if (postCont.PostComentario == null)
                {
                    sqlCmd.Parameters.Add("@PostComentario", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.Add("@PostComentario", SqlDbType.NVarChar).Value = postCont.PostComentario;

                }

                if (postCont.PostCalificacion == null)
                {
                    sqlCmd.Parameters.Add("@PostCalificacion", SqlDbType.Money).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostCalificacion", postCont.PostCalificacion);

                }
                if (postCont.PostAutorizaPublicacionImagen == null)
                {
                    sqlCmd.Parameters.Add("@PostAutorizaPublicacionImagen", SqlDbType.Bit).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostAutorizaPublicacionImagen", postCont.PostAutorizaPublicacionImagen);


                }

                if (postCont.PostComentarioAprobacion == null)
                {
                    sqlCmd.Parameters.Add("@PostComentarioAprobacion", SqlDbType.Bit).Value = DBNull.Value;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostComentarioAprobacion", postCont.PostComentarioAprobacion);
                }

                sqlCmd.Parameters.Add("@Identity", SqlDbType.Int).Direction = ParameterDirection.Output;
                resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());
              
                ValorPostId = Convert.ToInt32(sqlCmd.Parameters["@Identity"].Value);

                sqlCmd.Parameters.Clear();




                foreach (  PostContenido i in postCont.PostContenido)
                {
                  //  PostContenido  posCon= (postCont.PostContenido[0]);
                 
                    SqlCommand sqlCmd1 = new SqlCommand("InsertarPostContenido", conexion);
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.Parameters.AddWithValue("@PostId", ValorPostId);
                    sqlCmd1.Parameters.AddWithValue("@PostContenidoId", i.PostContenidoId);
                    sqlCmd1.Parameters.AddWithValue("@PostContenidoImagen", i.PostContenidoImagen);
                    if (i.PostContenidoVideo == null)
                    {
                        sqlCmd1.Parameters.Add("@PostContenidoVideo", SqlDbType.VarChar).Value = System.Data.SqlTypes.SqlString.Null;
                    }
                    else
                    {
                        sqlCmd1.Parameters.AddWithValue("@PostContenidoVideo", i.PostContenidoVideo);
                    }
                    if (i.PostContenidoVisible == null)
                    {
                        sqlCmd1.Parameters.Add("@PostContenidoVisible", SqlDbType.Bit).Value = System.Data.SqlTypes.SqlString.Null;
                    }
                    else
                    {
                        sqlCmd1.Parameters.AddWithValue("@PostContenidoVisible", i.PostContenidoVisible);
                    }
                    resPC = resPC+ Convert.ToInt32(sqlCmd1.ExecuteNonQuery());

                }
          
               if(resp!=0 && (resPC!= 0))
                {
                    RespuestaCont.mensaje = "OK";
                    RespuestaCont.estado = 1;

                    RespuestaCont.valor = null;

                  
                }
            }
            catch (Exception ex)
            {
                RespuestaCont.mensaje = ex.Message;
                RespuestaCont.estado =2;
                RespuestaCont.valor = null;
            }
            finally
            {
                conexion.Close();
            }
            return Ok(RespuestaCont);
        }

        [ResponseType(typeof(Post))]
        [HttpPut]
        [Route("api/PostPostContenido")]
        public IHttpActionResult ActualizarPostContenido(PostCont postCont)
        {
            int resp, resPC = 0;
            string valorServAsigId = "";
            decimal valorPersonaPostId = 0;
            int ValorPostId = 0;
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                ///////////////////////////////////////////ACTUALIZAR POST
                SqlCommand sqlCmd = new SqlCommand("ActualizarPost", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@PostId", postCont.PostId);
                sqlCmd.Parameters.AddWithValue("@TipoPostId",postCont.TipoPostId);
                sqlCmd.Parameters.AddWithValue("@PostDescripcion", postCont.PostDescripcion);
                sqlCmd.Parameters.AddWithValue("@PostEnlace", postCont.PostEnlace);
                sqlCmd.Parameters.AddWithValue("@PostContenidoLast", postCont.PostContenidoLast);
                sqlCmd.Parameters.AddWithValue("@PostCompartidoLast", postCont.PostCompartidoLast);
                sqlCmd.Parameters.AddWithValue("@PostFechaInsercion", postCont.PostFechaInsercion);
             


                if (postCont.PostUsuario == null)
                {
                    sqlCmd.Parameters.Add("@PostUsuario", SqlDbType.VarChar).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostUsuario", postCont.PostUsuario);
                }
                if (postCont.PostUID == null)
                {
                    sqlCmd.Parameters.Add("@PostUID", SqlDbType.VarChar  ).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostUID", postCont.PostUID);
                }
                if (postCont.PostLikesLast == null)
                {
                    sqlCmd.Parameters.Add("@PostLikesLast", SqlDbType.Decimal).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostLikesLast", postCont.PostLikesLast);
                }
                if (postCont.ServAsigId == null)
                {
                    sqlCmd.Parameters.Add("@ServAsigId", SqlDbType.VarChar).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@ServAsigId", postCont.ServAsigId);
                }
                if (postCont.PersonaPostId == null)
                {
                    sqlCmd.Parameters.Add("@PersonaPostId", SqlDbType.Decimal).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PersonaPostId", postCont.PersonaPostId);
                }
                if (postCont.PostComentario == null)
                {
                    sqlCmd.Parameters.Add("@PostComentario", SqlDbType.NVarChar).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.Add("@PostComentario", SqlDbType.NVarChar).Value = postCont.PostComentario;
                }
                if (postCont.PostCalificacion == null)
                {
                    sqlCmd.Parameters.Add("@PostCalificacion", SqlDbType.Money).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostCalificacion", postCont.PostCalificacion);
                }
                if (postCont.PostAutorizaPublicacionImagen == null)
                {
                    sqlCmd.Parameters.Add("@PostAutorizaPublicacionImagen", SqlDbType.Bit).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostAutorizaPublicacionImagen", postCont.PostAutorizaPublicacionImagen);
                }

                if (postCont.PostComentarioAprobacion == null)
                {
                    sqlCmd.Parameters.Add("@PostComentarioAprobacion", SqlDbType.Bit).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PostComentarioAprobacion", postCont.PostComentarioAprobacion);
                }



              

                resp = Convert.ToInt32(sqlCmd.ExecuteNonQuery());

                if (resp > 0)
                {
                    foreach (PostContenido i in postCont.PostContenido)
                    {
                        //  PostContenido  posCon= (postCont.PostContenido[0]);

                        SqlCommand sqlCmd1 = new SqlCommand("ActualizarPostContenido", conexion);
                        sqlCmd1.CommandType = CommandType.StoredProcedure;
                        sqlCmd1.Parameters.AddWithValue("@PostId", i.PostId);
                        sqlCmd1.Parameters.AddWithValue("@PostContenidoId", i.PostContenidoId);
                        sqlCmd1.Parameters.AddWithValue("@PostContenidoImagen", i.PostContenidoImagen);
                        if (i.PostContenidoVideo == null)
                        {
                            sqlCmd1.Parameters.Add("@PostContenidoVideo", SqlDbType.VarChar).Value = System.Data.SqlTypes.SqlString.Null;
                        }
                        else
                        {
                            sqlCmd1.Parameters.AddWithValue("@PostContenidoVideo", i.PostContenidoVideo);
                        }
                        if (i.PostContenidoVisible == null)
                        {
                            sqlCmd1.Parameters.Add("@PostContenidoVisible", SqlDbType.Bit).Value = System.Data.SqlTypes.SqlString.Null;
                        }
                        else
                        {
                            sqlCmd1.Parameters.AddWithValue("@PostContenidoVisible", i.PostContenidoVisible);
                        }
                        resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());

                    }
                }
                else
                {
                    PersonaMensaje.estado = 2;
                }

                ////////////////////////////////////////////
              

                if  (resPC != 0)
                {
                    RespuestaCont.mensaje = "OK";
                    RespuestaCont.estado = 1;

                    RespuestaCont.valor = null;


                }
            }
            catch (Exception ex)
            {
                RespuestaCont.mensaje = ex.Message;
                RespuestaCont.estado = 2;
                RespuestaCont.valor = null;
            }
            finally
            {
                conexion.Close();
            }
            return Ok(RespuestaCont);
        }

        // DELETE: api/Post/5
        /* [ResponseType(typeof(Post))]
         public IHttpActionResult DeletePost(decimal id)
         {
             Post post = db.Post.Find(id);
             if (post == null)
             {
                 return NotFound();
             }

             db.Post.Remove(post);
             db.SaveChanges();

             return Ok(post);
         }

         protected override void Dispose(bool disposing)
         {
             if (disposing)
             {
                 db.Dispose();
             }
             base.Dispose(disposing);
         }

         private bool PostExists(decimal id)
         {
             return db.Post.Count(e => e.PostId == id) > 0;
         }*/


        /// <summary>
        /// Obtener un Subgrupo de post en base a los parametros enviados
        /// </summary>
        /// <param name="PersonaId"></param>
        /// <param name="IndexInicio"></param>
        /// <param name="CantidadMax"></param>
        /// <returns></returns>

        [HttpGet]
        //  [ActionName("ValidarExistePersona")]
        [Route("api/Get_Index")]
        public IHttpActionResult postGet_Index(decimal PersonaId, int IndexInicio,int CantidadMax)
        {
            try
            {
                if (conexion.State != ConnectionState.Open) conexion.Open();
                ///////////////////////////////////////////Pais
                SqlCommand sqlCmd = new SqlCommand("PostGet_Index", conexion);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@PersonaId", PersonaId);
                sqlCmd.Parameters.AddWithValue("@IndexInicio", IndexInicio);
                sqlCmd.Parameters.AddWithValue("@CantidadMax", CantidadMax);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                /////////////////////////////////////////////////////
                   List<PostCont>ListaPostCont = new List<PostCont>();
                foreach (DataRow dtRow in dt.Rows)
                {
                    // On all tables' columns
                    foreach (DataColumn dc in dt.Columns)
                    {
                        int PostId= Convert.ToInt32(dtRow[dc]);
                        PostCont DPostCont = GetPostContenido1(PostId);

                        ListaPostCont.Add(new PostCont() {
                            PostId = DPostCont.PostId,
                            TipoPostId=DPostCont.TipoPostId,
                            PostDescripcion=DPostCont.PostDescripcion,
                            PostEnlace=DPostCont.PostEnlace,
                            PostContenidoLast=DPostCont.PostContenidoLast,
                            PostCompartidoLast=DPostCont.PostCompartidoLast,
                            PostFechaInsercion=DPostCont.PostFechaInsercion,
                            PostUsuario=DPostCont.PostUsuario,
                            PostUID=DPostCont.PostUID,
                            PostLikesLast=DPostCont.PostLikesLast,
                            ServAsigId=DPostCont.ServAsigId,
                            PersonaPostId=DPostCont.PersonaPostId,
                            PostComentario=DPostCont.PostComentario,
                            PostCalificacion=DPostCont.PostCalificacion,
                            PostAutorizaPublicacionImagen=DPostCont.PostAutorizaPublicacionImagen,
                            PostComentarioAprobacion=DPostCont.PostComentarioAprobacion,
                            PostContenido=DPostCont.PostContenido,
                            servAsig = DPostCont.servAsig
                        });
                    }
                }
                ////////////////////////////////////////////////////
             
                sqlCmd.Parameters.Clear();
                da.Dispose();
                dt.Dispose();
                ListaRespuestaCont.valor = ListaPostCont;
                ListaRespuestaCont.estado = 1;
                ListaRespuestaCont.mensaje = "OK";



            }
            catch (Exception ex)
            {
                ListaRespuestaCont.estado = 2;
                ListaRespuestaCont.mensaje = ex.Message;
                ListaRespuestaCont.valor = null;
            }
            finally
            {
                conexion.Close();
            }
            return Ok(ListaRespuestaCont);

        }




        public PostCont GetPostContenido1(int PostId)
        {
          
            ///////////////////////////////////////////Post

            SqlCommand sqlCmd = new SqlCommand("VerPost", conexion);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PostId", PostId);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Post Post = dt.AsEnumerable().Select
          (row => new Post
          {
              PostId = row.Field<decimal?>(0).GetValueOrDefault(),
              TipoPostId = row.Field<decimal>(1),
              PostDescripcion = row.Field<string>(2),
              PostEnlace = row.Field<string>(3),
              PostContenidoLast = row.Field<decimal>(4),
              PostCompartidoLast = row.Field<decimal>(5),
              PostFechaInsercion = row.Field<DateTime>(6),
              PostUsuario = row.Field<string>(7),
              PostUID = row.Field<string>(8),
              PostLikesLast = row.Field<decimal>(9),
              ServAsigId = row.Field<string>(10),
              PersonaPostId = row.Field<decimal?>(11),
              PostComentario = row.Field<string>(12),
              PostCalificacion= row.Field<decimal?>(13),
              PostAutorizaPublicacionImagen = row.Field<bool?>(14),
              PostComentarioAprobacion= row.Field<bool?>(15)

          }).FirstOrDefault();
            ServAsig servAsig = new ServAsig();           
            if (Post.ServAsigId != null)
            {
                servAsig = Conversor.toServAsig(dt.Select().FirstOrDefault());
                servAsig.requiereServicio = Conversor.toRequiereServicio(dt.Select().FirstOrDefault());
                servAsig.requiereServicio.RequiereServicioProveedores = Conversor.toRequiereServicioProveedores(dt.Select());
                servAsig.requiereServicio.RequiereServicioProveedores[0].servicioPersona = Conversor.toServicioPersona(dt.Select().FirstOrDefault());
            }

            sqlCmd.Parameters.Clear();
            da.Dispose();
            dt.Dispose();

            ////////////////////////////////////////////////PostContenido
            SqlCommand sqlCmd1 = new SqlCommand("VerPost_Contenido", conexion);
            sqlCmd1.CommandType = CommandType.StoredProcedure;
            sqlCmd1.Parameters.AddWithValue("@PostId", PostId);
            SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            List<PostContenido> ListPostContenido = dt1.AsEnumerable().Select
          (row => new PostContenido
          {
              PostId = row.Field<decimal?>(0).GetValueOrDefault(),
              PostContenidoId = row.Field<decimal>(1),
              PostContenidoImagen = row.Field<string>(2),
              PostContenidoVideo = row.Field<string>(3),
               PostContenidoVisible = row.Field<bool?>(4)
          }).ToList();
            sqlCmd1.Parameters.Clear();
            da1.Dispose();
            dt1.Dispose();

            /////////////////////////////////////////////////////////////PostCont
            PostCont postCont = new PostCont();
            postCont.PostId = Post.PostId;
            postCont.TipoPostId = Post.TipoPostId;
            postCont.PostDescripcion = Post.PostDescripcion;
            postCont.PostEnlace = Post.PostEnlace;
            postCont.PostContenidoLast = Post.PostContenidoLast;
            postCont.PostCompartidoLast = Post.PostCompartidoLast;
            postCont.PostFechaInsercion = Post.PostFechaInsercion;
            postCont.PostUsuario = Post.PostUsuario;
            postCont.PostUID = Post.PostUID;
            postCont.PostLikesLast = Post.PostLikesLast;
            postCont.ServAsigId = Post.ServAsigId;
            postCont.PersonaPostId = Post.PersonaPostId;
            postCont.PostAutorizaPublicacionImagen = Post.PostAutorizaPublicacionImagen;
            postCont.PostComentario = Post.PostComentario;
            postCont.PostCalificacion = Post.PostCalificacion;
            postCont.PostComentarioAprobacion = Post.PostComentarioAprobacion;
            postCont.PostContenido = ListPostContenido;
            if (Post.ServAsigId != null)
            {
                postCont.servAsig = servAsig;
            }
            RespuestaCont.estado = 1;
            RespuestaCont.valor = postCont;
            RespuestaCont.mensaje = "OK";
            return postCont;
        }                          



        [HttpPost]
        [Route("api/upload2")]     
        public async Task<ServiciosWeb.Datos.Modelo.Respuesta<List<Post>>> upload2()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;
                var fileDir = httpRequest.Form.Get("name");

                if (httpRequest.Files == null || httpRequest.Files.Count == 0)
                {
                    var res = string.Format("Porfavor Cargue la imagen");
                    dict.Add("error", res);
                    Respuesta.estado = 2;
                    Respuesta.valor = null;
                    Respuesta.mensaje = res;
                    return Respuesta;
                }
                foreach (string file in httpRequest.Files)
                {

                    var postedFile = httpRequest.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("por favor carque la imagen de tipo .jpg,.gif,.png.");

                            dict.Add("error", message);
                            Respuesta.estado = 2;
                            Respuesta.valor = null;
                            Respuesta.mensaje = message;

                            return Respuesta;//Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            Respuesta.estado = 2;
                            Respuesta.valor = null;
                            Respuesta.mensaje = message;
                            return Respuesta;//Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {

                            string archivoOrigen = System.Configuration.ConfigurationManager.AppSettings["Resources"];

                           // var filePath = HttpContext.Current.Server.MapPath(archivoOrigen + fileDir + "/" + postedFile.FileName);
                            var filePath = Path.Combine(archivoOrigen + fileDir + "/" + postedFile.FileName);

                          
                            postedFile.SaveAs(filePath);

                        }
                    }


                }
                var message1 = string.Format("Imagenes Insertadas con exito");
                Respuesta.estado = 1;
                Respuesta.valor = null;
                Respuesta.mensaje = message1;
                return Respuesta;// Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;

                // return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("Error");
                dict.Add("error", res);
                Respuesta.estado = 2;
                Respuesta.valor = null;
                Respuesta.mensaje = res + "" + ex.Message;
                return Respuesta;
                // return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            finally
            {
                conexion.Close();
            }
        }

        [HttpPost]
        [Route("api/upload")]
        public async Task<ServiciosWeb.Datos.Modelo.Respuesta<List<Post>>> upload()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string direccion = "";
            try
            {

                var httpRequest = HttpContext.Current.Request;
                var fileDir = httpRequest.Form.Get("name");

                if (httpRequest.Files == null || httpRequest.Files.Count == 0)
                {
                    var res = string.Format("Porfavor Cargue la imagen");
                    dict.Add("error", res);
                    direccion = crearAbrir_fichero_Fotos_Log();
                    EscribeEnArchivo(res, direccion, true);
                    Respuesta.estado = 5;
                    Respuesta.valor = null;
                    Respuesta.mensaje = res;
                    return Respuesta;
                }
                foreach (string file in httpRequest.Files)
                {

                    var postedFile = httpRequest.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = (1024*3) * (1024*3) * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("por favor carque la imagen de tipo .jpg,.gif,.png.");

                            dict.Add("error", message);
                            direccion = crearAbrir_fichero_Fotos_Log();
                            EscribeEnArchivo("por favor carque la imagen de tipo .jpg,.gif,.png.", direccion, true);
                            Respuesta.estado = 6;
                            Respuesta.valor = null;
                            Respuesta.mensaje = message;

                            return Respuesta;//Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            direccion = crearAbrir_fichero_Fotos_Log();
                            EscribeEnArchivo(message, direccion, true);
                            Respuesta.estado = 7;
                            Respuesta.valor = null;
                            Respuesta.mensaje = message;
                            return Respuesta;//Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {

                            string archivoOrigen = System.Configuration.ConfigurationManager.AppSettings["Resources"];

                            // var filePath = HttpContext.Current.Server.MapPath(archivoOrigen + fileDir + "/" + postedFile.FileName);
                            var filePath = Path.Combine(archivoOrigen + fileDir + "/" + postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            var message1 = string.Format("Imagenes Insertadas con exito");
                            direccion = crearAbrir_fichero_Fotos_Log();
                            EscribeEnArchivo("Imagenes Insertadas con exito", direccion, true);
                            Respuesta.estado = 8;
                            Respuesta.valor = null;
                            Respuesta.mensaje = message1;
                           // Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                        }
                    }

                    direccion = crearAbrir_fichero_Fotos_Log();
                    EscribeEnArchivo("ArchivoInsertado: " + Convert.ToString(postedFile.FileName) + " Hora:" + Convert.ToString(DateTime.Now), direccion, true);
                }

               

                direccion = crearAbrir_fichero_Fotos_Log();
                EscribeEnArchivo("FINALIZACION DEL CICLO INSERCION"+" Hora:"+Convert.ToString(DateTime.Now), direccion, true);

                return Respuesta;

                // return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("Error");
                string LogFotos = crearAbrir_fichero_Fotos_Log();
                EscribeEnArchivo(ex.Message, LogFotos, true);
                dict.Add("error", res);
                Respuesta.estado = 2;
                Respuesta.valor = null;
                Respuesta.mensaje = res + "" + ex.Message;
                return Respuesta;
                // return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
           
        }
        public string LogInsercionFotos()
        {

            DateTime fecha = DateTime.Now;
            System.IO.StreamWriter sw;
            string fichero = "";
            fichero = @"C:\LogFotos\LogFotos.txt";
            /////////////////////////////////////////////////////////////
            //const string fic = @"E:\tmp\Prueba.txt";

            if (System.IO.File.Exists(fichero) == false)
            {
                sw = new System.IO.StreamWriter(fichero);
                sw.Close();
            }


            return fichero;

        }
        public void Insertando_Log_Fotos(string nuevo_valor)
        {
            string fichero = crearAbrir_fichero_Fotos_Log();
            EscribeEnArchivo("LogFotos" + nuevo_valor + "\n", fichero, false);

            string path = @"c:\LogFotos\LogFotos.txt";
            string text = File.ReadAllText(path);
            text = text.Replace(text, nuevo_valor);
            File.WriteAllText(path, text);


        }
        public string crearAbrir_fichero_Fotos_Log()
        {

            DateTime fecha = DateTime.Now;
            System.IO.StreamWriter sw;
            string fichero = "";
            fichero = @"C:\LogFotos\" + fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + "_log" + ".txt";
            /////////////////////////////////////////////////////////////
            //const string fic = @"E:\tmp\Prueba.txt";

            if (System.IO.File.Exists(fichero) == false)
            {
                sw = new System.IO.StreamWriter(fichero);
                sw.Close();
            }


            return fichero;

        }
        public string crearAbrir_fichero_Fotos_LogDownload()
        {

            DateTime fecha = DateTime.Now;
            System.IO.StreamWriter sw;
            string fichero = "";
            fichero = @"C:\LogFotos\" + fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + "_Download" + ".txt";
            /////////////////////////////////////////////////////////////
            //const string fic = @"E:\tmp\Prueba.txt";

            if (System.IO.File.Exists(fichero) == false)
            {
                sw = new System.IO.StreamWriter(fichero);
                sw.Close();
            }


            return fichero;

        }

        public static void EscribeEnArchivo(string contenido, string rutaArchivo, bool sobrescribir)
        {
            try
            {
                StreamWriter sw = File.AppendText(rutaArchivo);

                sw.WriteLine(contenido);
                sw.Close();
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
        }
        public async Task<IHttpActionResult> Upload1()
        {


            var httpRequest = HttpContext.Current.Request;

            foreach (string file in httpRequest.Files)
            {

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                var postedFile = httpRequest.Files[file];

            }

            string[] paths = { @"E:\WebSite\" };
            string fullPath = Path.Combine(paths);
            var folder = fullPath;//HostingEnvironment.MapPath("~/Temp");
            var provider = new MultipartFormDataStreamProvider(folder);
            var data = await Request.Content.ReadAsMultipartAsync(provider);

            return Ok();
        }
        [HttpPost]
        [Route("api/CargarAchivo")]
        public IHttpActionResult SubirArchivo(HttpPostedFileBase file)
        {
            SubirArchivoModelo modelo = new SubirArchivoModelo();
            if (file != null)
            {
               // string[] paths = { @"F:\Fotos\" };
                //string fullPath = Path.Combine(paths);
                //string[] paths = {@"d:\archives", "2001", "media", "images"};
               // HostingEnvironment.MapPath("~/Temp");
                string ruta1 = HostingEnvironment.MapPath("~/Resources/MediaPost");//(fullPath);
                ruta1 += file.FileName;
                //  ruta += file.FileName;
                modelo.SubirArchivo(ruta1, file);
               
            }
            return Ok("HOLA");
        }

        [HttpPost]
        [Route("api/SubirArchivo_MediaIcons_MediaPost")]
        public string SubirArchivo_MediaIcons_MediaPost(string ServAsigId, SqlTransaction DataTransactionCom, SqlConnection conexion)
        {
            string nombreImagen;
            string ServicioPersonaURLFoto;
            try
            {

                if (conexion.State != ConnectionState.Open) conexion.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = "select ServicioPersonaURLFoto from ServAsig sa inner join RequiereServicioProveedores rsp on sa.RequiereServicioId = rsp.RequiereServicioId inner join ServicioPersona sp on rsp.ServicioPersonaId = sp.ServicioPersonaId and StatusRequiereId = 4 where ServAsigId = @ServAsigId";
                cmd.Parameters.Add("@ServAsigId", SqlDbType.VarChar).Value = ServAsigId;
                if (DataTransactionCom != null)
                {
                    cmd.Transaction = DataTransactionCom;
                }

                ServicioPersonaURLFoto = Convert.ToString(cmd.ExecuteScalar());
                string archivoOrigen = System.Configuration.ConfigurationManager.AppSettings["archivoOrigen"];
                string rutaDestino = System.Configuration.ConfigurationManager.AppSettings["rutaDestino"];
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
                if (DataTransactionCom == null)
                {
                    conexion.Close();
                }
            }

            return nombreImagen;
        }



        [HttpGet]        
        [Route("api/ImageDownload")]
        public HttpResponseMessage Download(string dirfile, string name)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            string Download = System.Configuration.ConfigurationManager.AppSettings["Resources"];
            var path = Download + dirfile +"/"+ name;
            // path = System.Web.Hosting.HostingEnvironment.MapPath(path);
            path = Path.Combine(path); 
            var ext = System.IO.Path.GetExtension(path);
            var contents = System.IO.File.ReadAllBytes(path);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(contents);

            response.Content = new StreamContent(ms);            
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + ext);
            string LogFotos = crearAbrir_fichero_Fotos_LogDownload();
            EscribeEnArchivo(path+"HORA:"+Convert.ToString(DateTime.Now), LogFotos, true);
            return response;
        }


        [HttpPost]
        [Route("api/savePostCompartido")]
        public IHttpActionResult savePostCompartido(BE.PostCompartido postCompartido)
        {
            Respuesta resp = new Respuesta();

            resp = postManager.savePostCompartido (ref postCompartido, postCompartido .PersonaCompartidoId );

            return Ok(resp);
        }

        public Boolean validarVersion(ref String message, String v = null, String d = null)
        {
            Boolean bolOK = false;
            String versionA = System.Configuration.ConfigurationManager.AppSettings["VersionAndroid"];
            String versionI = System.Configuration.ConfigurationManager.AppSettings["VersionIOS"];
            Boolean validarV = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["ValidarVersion"]);
            message = "Esta versión ha quedado obsoleta. Por favor actualicese a la nueva versión mayor o igual a: ";

            if (!validarV)
            {
                bolOK = true;

            }
            else if (v == null)
            {
                message += versionA +" desde Google Play."; 
            }
            else if (d == null)
            {
                message = "Marca de Dispositivo no soportado.";
            }
            else
            {
                decimal decV = 0;
                decimal decVersionD = 0;
                decV = Convert.ToDecimal(v);
                if (d == "a")
                {
                    decVersionD = Convert.ToDecimal(versionA);
                    if (decV >= decVersionD)
                    {
                        bolOK = true;
                    }
                    else
                    {
                        message += versionA
                            + "<br/><a href=\"https://play.google.com/store/apps/details?id=com.serviceweb.bo\">Descargar desde Google Play</a>"; 
                    }
                }
                else if (d == "i")
                {
                    decVersionD = Convert.ToDecimal(versionI);
                    if (decV >= decVersionD)
                    {
                        bolOK = true;
                    }
                }
                else
                {
                    message = "Versión de dispositivo no soportada.";
                }
            }

            return bolOK;
        }
    }
}