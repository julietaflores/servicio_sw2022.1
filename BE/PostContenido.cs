using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PostContenido : BEEntidad
    {


        public decimal PostId { get; set; }
        public decimal PostContenidoId { get; set; }
        public string PostContenidoImagen { get; set; }
        public string PostContenidoVideo { get; set; }
        public Nullable<bool> PostContenidoVisible { get; set; }

        public PostContenido()
        {

        }
        
    }

}
