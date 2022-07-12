using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE;
using BC;
using ServiciosWeb.WepApi.Models.Entities;

namespace ServiciosWeb.WepApi.Models
{
    public class PostManager

    {
        private BC.Post bcPost = null;
        private BC.PostLikes bcPostLikes = null;
        private BC.PostCompartido bcPostCompartido = null;
        private BC.PostContenido bcPostContenido = null;

        public PostManager(string cadConx)
        {
            bcPost = new BC.Post(cadConx);
            bcPostLikes = new BC.PostLikes(cadConx);
            bcPostCompartido = new BC.PostCompartido(cadConx);
            bcPostContenido = new BC.PostContenido(cadConx);
        }

        public Respuesta savePost(BE.Post post)
        {
            Respuesta resp = new Respuesta();
            resp.estado = 1;
            try
            {
                post.TipoEstado = BE.TipoEstado.Insertar;
                Boolean bolOk = bcPost.RegistrarPost(ref post);
                resp.valor = post;
                if (!bolOk)
                {
                    resp.estado = 2;
                    resp.mensaje = "Error al Registrar Post.";
                    resp.valor = null;
                }
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta PostPaginacion(long personaId, int index, int max)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                List<BE.Post> lstPost = new List<BE.Post>();

                lstPost = bcPost.ListadoPaginado(personaId, index, max, BE.Post.relPost .PostCountLikes , BE.Post.relPost .PostCountShare , BE.Post.relPost.PostContenido, BE.Post.relPost.PostLikesPersona
                    , BE.Post.relPost .ServAsig, BE.relServAsig .requiereServicio , BE.relRequiereServicio .reqServProvAdj
                    ,BE.relReqServProv .servicioPersona );

                resp.valor = lstPost;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta PostCantidad(long personaId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                int cantPost = 0;

                cantPost = bcPost.CantidadPost(personaId);

                resp.valor = cantPost;
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta savePostLikes(BE.PostLikes postLikes, long personaId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                bolOk = bcPostLikes.Actualizar(ref postLikes, false);
                if (!bolOk)
                {
                    throw new Exception("Error al actualizar Me gusta");
                }
                else
                {
                    BE.Post post = bcPost.BuscarPostxId(postLikes.PostId,personaId,BE.Post .relPost .PostCountLikes , BE.Post.relPost.PostCountShare);
                    if(postLikes .TipoEstado == TipoEstado.Insertar)
                    {
                        post.postLikes = new List<BE.PostLikes>();
                        post.postLikes.Add(postLikes);
                    }
                    resp.valor = post;
                }
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public Respuesta savePostCompartido(ref BE.PostCompartido  postCompartido, decimal personaId)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                bolOk = bcPostCompartido.Actualizar(ref postCompartido, false);
                if (!bolOk)
                {
                    throw new Exception("Error al actualizar Me gusta");
                }
                else
                {
                    BE.Post post = bcPost.BuscarPostxId(postCompartido.PostId, personaId, BE.Post.relPost.PostCountLikes, BE.Post.relPost.PostCountShare);
                    if (postCompartido.TipoEstado == TipoEstado.Insertar)
                    {
                        post.postCompartido = new List<BE.PostCompartido>();
                        post.postCompartido.Add(postCompartido);
                    }
                    resp.valor = post;
                }
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        public Respuesta savePostContenido(BE.PostContenido postContenido)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.estado = 1;
                resp.mensaje = "";
                bolOk = bcPostContenido.Actualizar(ref postContenido, false);
                if (!bolOk)
                {
                    throw new Exception("Error al actualizar Me gusta");
                }
            
            
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public string SubirArchivo_MediaIcons_MediaPost(string ServAsigId)
        {
            string bolOk = "";
            Respuesta resp = new Respuesta();
            try
            {
              
                bolOk = bcPost.SubirArchivo_MediaIcons_MediaPost(ServAsigId);
                if (bolOk=="")
                {
                    throw new Exception("Error al actualizar Me gusta");
                }
               
            }
            catch (Exception ex)
            {
                resp.estado = 2;
                resp.valor = null;
                resp.mensaje = ex.Message;
            }
        
            return bolOk;
        }
    }
}