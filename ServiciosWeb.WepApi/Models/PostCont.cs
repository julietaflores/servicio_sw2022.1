using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class PostCont
    {

        public decimal PostId { get; set; }
        public decimal TipoPostId { get; set; }
        public string PostDescripcion { get; set; }
        public string PostEnlace { get; set; }
        public decimal PostContenidoLast { get; set; }
        public decimal PostCompartidoLast { get; set; }
        public System.DateTime PostFechaInsercion { get; set; }
        public string PostUsuario { get; set; }
        public string PostUID { get; set; }
        public decimal PostLikesLast { get; set; }
        public string ServAsigId { get; set; }
        public Nullable<decimal> PersonaPostId { get; set; }
        public string PostComentario { get; set; }
        public Nullable<decimal> PostCalificacion { get; set; }
        public Nullable<bool> PostAutorizaPublicacionImagen { get; set; }
        public Nullable<bool> PostComentarioAprobacion { get; set; }
        public List<PostContenido> PostContenido { get; set; }
        public ServAsig servAsig { get; set; }
    }
}