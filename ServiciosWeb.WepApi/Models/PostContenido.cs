using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWeb.WepApi.Models
{
    public class PostContenido
    {
        
        public decimal PostId { get; set; }
        public decimal PostContenidoId { get; set; }
        public string PostContenidoImagen { get; set; }
        public string PostContenidoVideo { get; set; }
        public Nullable<bool> PostContenidoVisible { get; set; }


    }

}
