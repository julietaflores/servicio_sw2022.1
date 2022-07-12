using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PostLikes:BEEntidad
    {
        public decimal PostId { get; set; }
        public decimal PostLikesId { get; set; }
        public decimal PersonaLikesId { get; set; }
        public System.DateTime PostLikesFechaHora { get; set; }
    }
}
