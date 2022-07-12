using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class PersonaGeoLocalizacion : BCEntidad
    {

        private string campos(string prefijo = "perGeo")
        {
            string strCampos = String.Format(@"{0}.PersonaId,{0}.PersonaGeoLocalizacionId,{0}.PersonaGeoLocalizacionLangLat,{0}.PersonaGeoLocalizacionFechaHor"
                    , prefijo);
            return strCampos;
        }
        public PersonaGeoLocalizacion() : base()
        {
        }

        public PersonaGeoLocalizacion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"


        public Boolean Actualizar(ref BE.PersonaGeoLocalizacion BEObj, Boolean isTransaccion = false)
        {
            string strSql = string.Empty;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            bool bolOk = false;

            try
            {
                string TipoEstadoa = BEObj.TipoEstado.ToString();
                switch (BEObj.TipoEstado)
                {

                    case BE.TipoEstado.Modificar:

                        strSql = "update dbo.Persona set PersonaTokenId = @PersonaTokenId,PersonaNombres = @PersonaNombres,personaApellidos = @personaApellidos,PersonaCorreo = @PersonaCorreo,PersonaFechaNacimiento = @PersonaFechaNacimiento,PersonaTelefono = @PersonaTelefono,PersonaUID = @PersonaUID,PersonaURLFoto = @PersonaURLFoto,PersonaUsuario = @PersonaUsuario,PersonaFechaHoraMod = @PersonaFechaHoraMod,TipoPersonaId = @TipoPersonaId,GeneroId = @GeneroId,TipoLoginId = @TipoLoginId,CiudadId = @CiudadId,PersonaFechaRegistro = @PersonaFechaRegistro,EstadoPersonaId = @EstadoPersonaId,PersonaDireccionLast = @PersonaDireccionLast,PersonaDNI = @PersonaDNI,TipoDocumentoId = @TipoDocumentoId,PersonaGeoReal = @PersonaGeoReal,PersonaClave = @PersonaClave,PersonaUsuarioMod = @PersonaUsuarioMod,PersonaCodigoTelefono = @PersonaCodigoTelefono,PersonaGeoLocalizacionLast = @PersonaGeoLocalizacionLast WHERE PersonaId = @PersonaId ";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = " insert into PersonaGeoLocalizacion(PersonaId,PersonaGeoLocalizacionId,PersonaGeoLocalizacionLangLat,PersonaGeoLocalizacionFechaHor)  values(@PersonaId, @PersonaGeoLocalizacionId, @PersonaGeoLocalizacionLangLat, @PersonaGeoLocalizacionFechaHor)";
                        break;

                }

                if (isTransaccion)
                    conx = dbConexion;
                else
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                }
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)

                    BEObj.PersonaGeoLocalizacionId = ObtenerPersonaGeolocalizacionId(BEObj.PersonaId);
                // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(PersonaGeoLocalizacionId),0) + 1 from dbo.Cliente with (nolock) where "));
                conx.CrearComando(strSql);
                conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
                conx.AsignarParametro("@PersonaGeoLocalizacionId", BEObj.PersonaGeoLocalizacionId);
                conx.AsignarParametro("@PersonaGeoLocalizacionLangLat", BEObj.PersonaGeoLocalizacionLangLat);
                conx.AsignarParametro("@PersonaGeoLocalizacionFechaHor", BEObj.PersonaGeoLocalizacionFechaHor);


                conx.EjecutarComando();
                if (!isTransaccion)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }

                bolOk = true;
            }
            catch (Exception ex)
            {
                if (dbConexion == null)
                {
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }
        public List<BE.PersonaGeoLocalizacion> CargarBE(DataRow[] dr)
        {
            List<BE.PersonaGeoLocalizacion> lst = new List<BE.PersonaGeoLocalizacion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.PersonaGeoLocalizacion CargarBE(DataRow dr)
        {

            BE.PersonaGeoLocalizacion obj = new BE.PersonaGeoLocalizacion();
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.PersonaGeoLocalizacionId = Convert.ToDecimal(dr["PersonaGeoLocalizacionId"].ToString());
            obj.PersonaGeoLocalizacionLangLat = Convert.ToString(dr["PersonaGeoLocalizacionLangLat"].ToString());
            obj.PersonaGeoLocalizacionFechaHor = Convert.ToDateTime(dr["PersonaGeoLocalizacionFechaHor"].ToString());

            return obj;


        }

        #endregion
        public void ActualizarPersonaGeoLocalizacionLast(decimal IdPersona)
        {
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "PersonaGeoLocalizacionLast";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdPersona", IdPersona);

                conx.EjecutarComando();

            }
            catch (Exception ex)
            {

                throw;
            }




        }
        public decimal ObtenerPersonaGeolocalizacionId(decimal PersonaId)
        {
            decimal GeolocalizacionId = 0;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {


                ///////////////////////////
                string sql = "MaxIdPersonGeo";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@PersonaId", PersonaId);

                GeolocalizacionId = (Convert.ToDecimal(conx.EjecutarScalar())) + 1;
                ////////////////

            }
            catch (Exception ex)
            {
            }

            finally
            {

            }
            return GeolocalizacionId;
        }

        public List<BE.PersonaGeoLocalizacion> ListadoGeolocalizacion()
        {
            List<BE.PersonaGeoLocalizacion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.PersonaGeoLocalizacion perGeo with(nolock) ", campos("perGeo"));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

    }
}
