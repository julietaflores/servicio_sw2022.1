//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiciosWeb.Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
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
    }
}
