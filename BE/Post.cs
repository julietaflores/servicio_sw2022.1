using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Post : BEEntidad
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
        public List<BE.PostLikes> postLikes { get; set; }
        public long PostCountLikes { get; set; }
        public long PostCountShares { get; set; }
        public List<PostCompartido> postCompartido { get; set; }

        public enum relPost
        {
            PostContenido, PostLikes, PostShare, PostLikesPersona, ServAsig, PostCountLikes, PostCountShare
        }
    }
 
}
