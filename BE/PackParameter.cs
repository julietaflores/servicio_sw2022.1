using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PackParameter
    {


        public List<Pais> paises { get; set; }
        public List<EstadoPersona> estadoPersonas { get; set; }
        public List<Genero> generos { get; set; }
        public List<Region> regiones { get; set; }
        public List<Ciudad> ciudades { get; set; }
        public List<TipoDireccion> tipoDirecciones { get; set; }
        public List<TipoDocumento> tipoDocumentos { get; set; }
        public List<TipoPersona> tipoPersonas { get; set; }
        public List<TipoPost> tipoPosts { get; set; }


    }
}
