using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Persona : BEEntidad
    {

        public decimal PersonaId { get; set; }
        public string PersonaTokenId { get; set; }
        public string PersonaNombres { get; set; }
        public string PersonaApellidos { get; set; }
        public string PersonaCorreo { get; set; }
        public System.DateTime PersonaFechaNacimiento { get; set; }
        public string PersonaTelefono { get; set; }
        public string PersonaUID { get; set; }
        public string PersonaURLFoto { get; set; }
        public string PersonaUsuario { get; set; }
        public System.DateTime PersonaFechaHoraMod { get; set; }
        public decimal TipoPersonaId { get; set; }
        public decimal GeneroId { get; set; }
        public decimal TipoLoginId { get; set; }
        public decimal CiudadId { get; set; }
        public System.DateTime PersonaFechaRegistro { get; set; }
        public decimal EstadoPersonaId { get; set; }
        public decimal PersonaDireccionLast { get; set; }
        public string PersonaDNI { get; set; }
        public decimal TipoDocumentoId { get; set; }
        public string PersonaGeoReal { get; set; }
        public string PersonaClave { get; set; }
        public string PersonaUsuarioMod { get; set; }
        public string PersonaCodigoTelefono { get; set; }
        public decimal PersonaGeoLocalizacionLast { get; set; }

        public bool PersonaCorreoValidado { get; set; }
       public string PersonaCodigoVerificacion { get; set; }
        public string PersonaFacebookUid { get; set; }
        public string PersonaGmailUid { get; set; }
        public string PersonaPhoneUid { get; set; }
         public bool PersonaContrasenaActualizada { get; set; }
        public string PersonaIdioma { get; set; }

        public string PersonaTokenIdHuawei { get; set; }
        public string PersonaHuaweUid { get; set; }

        public Ciudad Ciudad { get; set; }

        public PersonaDireccion PersonaDireccion { get; set; }
        public TipoLogin tipoLogin { get; set; }

        public ServicioPersona serviciopersona_Persona { get; set; }
        public int log_Persona { get; set; }





        public string NombreCompleto()
        {
            string nombre = PersonaNombres;
            if (PersonaApellidos != null || PersonaApellidos != "")
            {
                nombre += " " + PersonaApellidos;
            }
            return nombre;
        }

    }
    public enum relPersona
    {
        ciudad, tipoLogin,serviciopersona
    }






}
