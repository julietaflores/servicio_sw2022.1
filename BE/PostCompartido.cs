using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PostCompartido:BEEntidad
    {
        public decimal PostId { get; set; }
       public decimal PostCompartidoId { get; set; }
        public decimal PersonaCompartidoId { get; set; }
        public string   PostCompartidoTitulo { get; set; }
        public DateTime PostCompartidoFechaHora { get; set; }
        public string PostCompartidoValeDesc { get; set; }
        public string PostCompartidoValeEstado { get; set; }
        public DateTime    PostCompartidoValeFechaHora { get; set; }
        public DateTime PostCompartidoValeCaducidad { get; set; }



    }
}
