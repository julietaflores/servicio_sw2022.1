using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using BE;

namespace BC
{
    public class StatusServAsig : BCEntidad
    {
        private string campos(string prefijo = "ss")
        {
            string strCampos = String.Format(@"	{0}.StatusServAsigId,
	{0}.StatusServAsigNombre"
                    , prefijo);
            return strCampos;
        }
        public StatusServAsig() : base()
        {
        }
        public StatusServAsig(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.StatusServAsig BEObj)
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

                        strSql = "";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = "";
                        break;

                }

                if (dbConexion != null)
                    conx = dbConexion;
                else
                    conx.Conectar();
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));
                    conx.CrearComando(strSql);
                conx.AsignarParametro("@StatusServicioId", BEObj.StatusServAsigId);
                conx.AsignarParametro("@StatusServicioNombre", BEObj.StatusServAsigNombre);
                conx.EjecutarComando();
                if (dbConexion == null)
                    conx.Desconectar();
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

        //public Boolean Actualizar(ref BE.Persona BEObj)
        //{
        //    string strSql = string.Empty;
        //    ClaseConexion conx = new ClaseConexion(cadenaConexion);
        //    bool bolOk = false;

        //    try
        //    {
        //        string TipoEstadoa = BEObj.TipoEstado.ToString();
        //        switch (BEObj.TipoEstado)
        //        {

        //            case BE.TipoEstado.Modificar:

        //                strSql = "update dbo.Persona set PersonaTokenId = @PersonaTokenId,PersonaNombres = @PersonaNombres,personaApellidos = @personaApellidos,PersonaCorreo = @PersonaCorreo,PersonaFechaNacimiento = @PersonaFechaNacimiento,PersonaTelefono = @PersonaTelefono,PersonaUID = @PersonaUID,PersonaURLFoto = @PersonaURLFoto,PersonaUsuario = @PersonaUsuario,PersonaFechaHoraMod = @PersonaFechaHoraMod,TipoPersonaId = @TipoPersonaId,GeneroId = @GeneroId,TipoLoginId = @TipoLoginId,CiudadId = @CiudadId,PersonaFechaRegistro = @PersonaFechaRegistro,EstadoPersonaId = @EstadoPersonaId,PersonaDireccionLast = @PersonaDireccionLast,PersonaDNI = @PersonaDNI,TipoDocumentoId = @TipoDocumentoId,PersonaGeoReal = @PersonaGeoReal,PersonaClave = @PersonaClave,PersonaUsuarioMod = @PersonaUsuarioMod,PersonaCodigoTelefono = @PersonaCodigoTelefono,PersonaGeoLocalizacionLast = @PersonaGeoLocalizacionLast WHERE PersonaId = @PersonaId ";
        //                break;
        //            case BE.TipoEstado.Insertar:
        //                strSql = "";
        //                break;

        //        }

        //            conx.Conectar();
        //        if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
        //            // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));
        //            conx.CrearComando(strSql);
        //        conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
        //        conx.AsignarParametro("@PersonaNombres", BEObj.PersonaNombres);
        //        conx.AsignarParametro("@PersonaApellidos", BEObj.PersonaApellidos);
        //        conx.AsignarParametro("@PersonaCorreo", BEObj.PersonaCorreo);
        //        conx.AsignarParametro("@PersonaFechaNacimiento", BEObj.PersonaFechaNacimiento);
        //        conx.AsignarParametro("@PersonaTelefono", BEObj.PersonaTelefono);
        //        conx.AsignarParametro("@PersonaUID", BEObj.PersonaUID);
        //        conx.AsignarParametro("@PersonaURLFoto", BEObj.PersonaURLFoto);
        //        conx.AsignarParametro("@PersonaUsuario", BEObj.PersonaUsuario);
        //        conx.AsignarParametro("@PersonaFechaHoraMod", BEObj.PersonaFechaHoraMod);
        //        conx.AsignarParametro("@TipoPersonaId", BEObj.TipoPersonaId);
        //        conx.AsignarParametro("@CiudadId", BEObj.CiudadId);
        //        conx.AsignarParametro("@PersonaFechaRegistro", BEObj.PersonaFechaRegistro);
        //        conx.AsignarParametro("@EstadoPersonaId", BEObj.EstadoPersonaId);
        //        conx.AsignarParametro("@PersonaDireccionLast", BEObj.PersonaDireccionLast);
        //        conx.AsignarParametro("@PersonaDNI", BEObj.PersonaDNI);
        //        conx.AsignarParametro("@TipoDocumentoId", BEObj.TipoDocumentoId);
        //        conx.AsignarParametro("@PersonaGeoReal", BEObj.PersonaGeoReal);
        //        conx.AsignarParametro("@PersonaClave", BEObj.PersonaClave);
        //        conx.AsignarParametro("@PersonaUsuarioMod", BEObj.PersonaUsuarioMod);
        //        conx.AsignarParametro("@PersonaCodigoTelefono", BEObj.PersonaCodigoTelefono);
        //        conx.AsignarParametro("@PersonaGeoLocalizacionLast", BEObj.PersonaGeoLocalizacionLast);

        //        conx.EjecutarComando();

        //            conx.Desconectar();
        //        bolOk = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        conx.Desconectar();               
        //        throw ex;
        //    }
        //    return bolOk;
        //}

        public List<BE.StatusServAsig> CargarBE(DataRow[] dr)
        {
            List<BE.StatusServAsig> lst = new List<BE.StatusServAsig>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.StatusServAsig CargarBE(DataRow dr)
        {
            BE.StatusServAsig obj = new BE.StatusServAsig();
            obj.StatusServAsigId = Convert.ToDecimal(dr["StatusServAsigId"].ToString());
            obj.StatusServAsigNombre = dr["StatusServAsigNombre"].ToString();

            return obj;         
        }

        public List<BE.StatusServAsig> ObtenerHijos(IEnumerable<decimal> llaves,string lang, params Enum[] relaciones)
        {
            List<BE.StatusServAsig> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.StatusServAsigSP ('{1}')  ss  where ss.StatusServAsigId in {2}", campos("ss"), lang,this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion

        #region "Listados"

        #endregion

        #region "Busqueda"

        public BE.StatusServAsig BuscarStatusServicioxId(decimal StatusServAsigId)
        {
            BE.StatusServAsig obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from [StatusServAsig] with(nolock) where StatusServAsigId={0}", StatusServAsigId);
                DataRow dr = conx.ObtenerFila(sql);
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
        #endregion
    }
}
