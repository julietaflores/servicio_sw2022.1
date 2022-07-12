using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BE;
using DAL;
using System.Data;
using System.Security.Cryptography;




namespace BC
{
    public class Persona : BCEntidad
    {
        public string campos(string prefijo = "per")
        {
            string strCampos = String.Format(@"{0}.PersonaId,{0}.PersonaTokenId,{0}.PersonaNombres,{0}.PersonaApellidos,{0}.PersonaCorreo,{0}.PersonaFechaNacimiento,{0}.PersonaTelefono,{0}.PersonaUID,{0}.PersonaURLFoto,{0}.PersonaUsuario,
{0}.PersonaFechaHoraMod,{0}.TipoPersonaId,{0}.GeneroId,{0}.TipoLoginId,{0}.CiudadId,{0}.PersonaFechaRegistro,{0}.EstadoPersonaId,{0}.PersonaDireccionLast,{0}.PersonaDNI,{0}.TipoDocumentoId,
{0}.PersonaGeoReal,{0}.PersonaClave,{0}.PersonaUsuarioMod,{0}.PersonaCodigoTelefono,{0}.PersonaGeoLocalizacionLast,{0}.PersonaCorreoValidado,{0}.PersonaCodigoVerificacion,{0}.PersonaFacebookUid,{0}.PersonaGmailUid,{0}.PersonaPhoneUid,{0}.PersonaIdioma,{0}.PersonaTokenIdHuawei,{0}.PersonaHuaweUid" //,{0}.PersonaContrasenaActualizada
                    , prefijo);
            return strCampos;
        }
        public Persona() : base()
        {
        }
        public Persona(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

    
        public Boolean Actualizar(ref BE.Persona BEObj, Boolean isTransaccion = false)
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

                        strSql = "update dbo.Persona set PersonaTokenId = @PersonaTokenId,PersonaNombres = @PersonaNombres,personaApellidos = @personaApellidos,PersonaCorreo = @PersonaCorreo,PersonaFechaNacimiento = @PersonaFechaNacimiento,PersonaTelefono = @PersonaTelefono,PersonaUID = @PersonaUID,PersonaURLFoto = @PersonaURLFoto,PersonaUsuario = @PersonaUsuario,PersonaFechaHoraMod = @PersonaFechaHoraMod,TipoPersonaId = @TipoPersonaId,GeneroId = @GeneroId,TipoLoginId = @TipoLoginId,CiudadId = @CiudadId,PersonaFechaRegistro = @PersonaFechaRegistro,EstadoPersonaId = @EstadoPersonaId,PersonaDireccionLast = @PersonaDireccionLast,PersonaDNI = @PersonaDNI,TipoDocumentoId = @TipoDocumentoId,PersonaGeoReal = @PersonaGeoReal,PersonaClave = @PersonaClave,PersonaUsuarioMod = @PersonaUsuarioMod,PersonaCodigoTelefono = @PersonaCodigoTelefono,PersonaGeoLocalizacionLast = @PersonaGeoLocalizacionLast, PersonaCorreoValidado=@PersonaCorreoValidado,PersonaCodigoVerificacion=@PersonaCodigoVerificacion, " +
                            " PersonaFacebookUid=@PersonaFacebookUid,PersonaGmailUid=@PersonaGmailUid,PersonaPhoneUid=@PersonaPhoneUid,PersonaIdioma=@PersonaIdioma ,PersonaTokenIdHuawei=@PersonaTokenIdHuawei,PersonaHuaweUid=@PersonaHuaweUid   WHERE PersonaId = @PersonaId ";

                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into Persona (PersonaTokenId,PersonaNombres,PersonaApellidos,PersonaCorreo,PersonaFechaNacimiento,PersonaTelefono,PersonaUID,
                                                        PersonaURLFoto,PersonaUsuario,PersonaFechaHoraMod,TipoPersonaId,GeneroId,TipoLoginId,CiudadId,PersonaFechaRegistro,
                                                        EstadoPersonaId,PersonaDireccionLast,PersonaDNI,TipoDocumentoId,PersonaGeoReal,PersonaClave,PersonaUsuarioMod,
                                                        PersonaCodigoTelefono,PersonaGeoLocalizacionLast,PersonaCorreoValidado,PersonaCodigoVerificacion,
                                                        PersonaFacebookUid,PersonaGmailUid,PersonaPhoneUid,PersonaIdioma,PersonaTokenIdHuawei,PersonaHuaweUid)
                                                         values
                                                        (@PersonaTokenId,@PersonaNombres,@PersonaApellidos,@PersonaCorreo,@PersonaFechaNacimiento,@PersonaTelefono,@PersonaUID,
                                                         @PersonaURLFoto,@PersonaUsuario,@PersonaFechaHoraMod,@TipoPersonaId,@GeneroId,@TipoLoginId,@CiudadId,@PersonaFechaRegistro,
                                                         @EstadoPersonaId,@PersonaDireccionLast,@PersonaDNI,@TipoDocumentoId,@PersonaGeoReal,@PersonaClave,@PersonaUsuarioMod,
                                                         @PersonaCodigoTelefono,@PersonaGeoLocalizacionLast,@PersonaCorreoValidado,@PersonaCodigoVerificacion,
                                                         @PersonaFacebookUid,@PersonaGmailUid,@PersonaPhoneUid, @PersonaIdioma,@PersonaTokenIdHuawei,@PersonaHuaweUid)";
                        break;

                }
                if (isTransaccion)
                    conx = dbConexion;
                else
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                }
                if (BEObj.TipoEstado != BE.TipoEstado.SinAccion)
                {
                    conx.CrearComando(strSql);
                    if (BEObj.TipoEstado == BE.TipoEstado.Modificar)
                    {
                        conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
                    }
                    conx.AsignarParametro("@PersonaTokenId", BEObj.PersonaTokenId);
                    conx.AsignarParametro("@PersonaNombres", BEObj.PersonaNombres);
                    conx.AsignarParametro("@PersonaApellidos", BEObj.PersonaApellidos);
                    conx.AsignarParametro("@PersonaCorreo", BEObj.PersonaCorreo);
                    conx.AsignarParametro("@PersonaFechaNacimiento", BEObj.PersonaFechaNacimiento);
                    conx.AsignarParametro("@PersonaTelefono", BEObj.PersonaTelefono);
                    conx.AsignarParametro("@PersonaUID", BEObj.PersonaUID);
                    conx.AsignarParametro("@PersonaURLFoto", BEObj.PersonaURLFoto);
                    conx.AsignarParametro("@PersonaUsuario", BEObj.PersonaUsuario);
                    conx.AsignarParametro("@PersonaFechaHoraMod", BEObj.PersonaFechaHoraMod);
                    conx.AsignarParametro("@TipoPersonaId", BEObj.TipoPersonaId);
                    conx.AsignarParametro("@GeneroId", BEObj.GeneroId);
                    conx.AsignarParametro("@TipoLoginId", BEObj.TipoLoginId);
                    conx.AsignarParametro("@CiudadId", BEObj.CiudadId);
                    conx.AsignarParametro("@PersonaFechaRegistro", BEObj.PersonaFechaRegistro);
                    conx.AsignarParametro("@EstadoPersonaId", BEObj.EstadoPersonaId);
                    conx.AsignarParametro("@PersonaDireccionLast", BEObj.PersonaDireccionLast);
                    conx.AsignarParametro("@PersonaDNI", BEObj.PersonaDNI);
                    conx.AsignarParametro("@TipoDocumentoId", BEObj.TipoDocumentoId);
                    conx.AsignarParametro("@PersonaGeoReal", BEObj.PersonaGeoReal);
                    conx.AsignarParametro("@PersonaClave", BEObj.PersonaClave);
                    conx.AsignarParametro("@PersonaUsuarioMod", BEObj.PersonaUsuarioMod);
                    conx.AsignarParametro("@PersonaCodigoTelefono", BEObj.PersonaCodigoTelefono);
                    conx.AsignarParametro("@PersonaGeoLocalizacionLast", BEObj.PersonaGeoLocalizacionLast);
                    conx.AsignarParametro("@PersonaCorreoValidado", BEObj.PersonaCorreoValidado);
                    conx.AsignarParametro("@PersonaCodigoVerificacion", BEObj.PersonaCodigoVerificacion);
                    conx.AsignarParametro("@PersonaFacebookUid", BEObj.PersonaFacebookUid);
                    conx.AsignarParametro("@PersonaGmailUid", BEObj.PersonaGmailUid);
                    conx.AsignarParametro("@PersonaPhoneUid", BEObj.PersonaPhoneUid);
                    conx.AsignarParametro("@PersonaIdioma", BEObj.PersonaIdioma);
                    conx.AsignarParametro("@PersonaTokenIdHuawei", BEObj.PersonaTokenIdHuawei);
                    conx.AsignarParametro("@PersonaHuaweUid", BEObj.PersonaHuaweUid);

                }
                conx.EjecutarComando();
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                {
                    BEObj.PersonaId = System.Convert.ToDecimal(conx.ObtenerValor("select isnull(max(PersonaId),0)  from dbo.Persona with (nolock);"));
                    BEObj.log_Persona = 0;








                }
                if (!isTransaccion)
                {
                    conx.ConfirmarTransaccion();
                    conx.Desconectar();
                }
              bolOk = true;
            }
            catch (Exception ex)
            {
                if (!isTransaccion)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }















        public int ValidarExistePersonaPorCorreo(string PersonaCorreo, string PersonaCodigoTelefono, string PersonaTelefono)
        {
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.CrearComando("dbo.ValidarExistePersonaV2", CommandType.StoredProcedure);
                conx.AsignarParametro("@PersonaCorreo", PersonaCorreo);
                conx.AsignarParametro("@PersonaCodigoTelefono", PersonaCodigoTelefono);
                conx.AsignarParametro("@PersonaTelefono", PersonaTelefono);
                int cantidad = Convert.ToInt32(conx.EjecutarScalar());
                return cantidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }





































































        public List<BE.Persona> Listado_Usuarios_Mas_Frecuentes()
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select {0} from requiereservicio re 
                                             inner join persona per on per.personaid = re.personaid 
                                             inner join servasig sg on sg.requiereservicioid = re.requiereservicioid 
                                             where  re.PersonaId NOT IN(SELECT  ppp.PersonaId  FROM PersonalPreuba ppp) 
                                             and  re.estadoreqservid in (2) 
                                             group by {0} 
                                             HAVING count(per.personaid) > 1",campos("per"));
    
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


  




        public List<BE.Persona> CargarBE(DataRow[] dr)
        {
            List<BE.Persona> lst = new List<BE.Persona>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Persona CargarBE(DataRow dr)
        {
            BE.Persona obj = new BE.Persona();
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.PersonaTokenId = dr["PersonaTokenId"].ToString();
            obj.PersonaNombres = dr["PersonaNombres"].ToString();
            obj.PersonaApellidos = dr["PersonaApellidos"].ToString();
            obj.PersonaCorreo = dr["PersonaCorreo"].ToString();
            obj.PersonaFechaNacimiento = Convert.ToDateTime(dr["PersonaFechaNacimiento"].ToString());
            obj.PersonaTelefono = dr["PersonaTelefono"].ToString();
            obj.PersonaUID = dr["PersonaUID"].ToString();
            obj.PersonaURLFoto = dr["PersonaURLFoto"].ToString();
            obj.PersonaUsuario = dr["PersonaUsuario"].ToString();
            obj.PersonaFechaHoraMod = Convert.ToDateTime(dr["PersonaFechaHoraMod"].ToString());
            obj.TipoPersonaId = Convert.ToDecimal(dr["TipoPersonaId"].ToString());
            obj.GeneroId = Convert.ToDecimal(dr["GeneroId"].ToString());
            obj.TipoLoginId = Convert.ToDecimal(dr["TipoLoginId"].ToString());
            obj.CiudadId = Convert.ToDecimal(dr["CiudadId"].ToString());
            obj.PersonaFechaRegistro = Convert.ToDateTime(dr["PersonaFechaRegistro"].ToString());
            obj.EstadoPersonaId = Convert.ToDecimal(dr["EstadoPersonaId"].ToString());
            obj.PersonaDireccionLast = Convert.ToDecimal(dr["PersonaDireccionLast"].ToString());
            obj.PersonaDNI = dr["PersonaDNI"].ToString();
            obj.TipoDocumentoId = Convert.ToDecimal(dr["TipoDocumentoId"].ToString());
            obj.PersonaGeoReal = dr["PersonaGeoReal"].ToString();
            obj.PersonaClave = dr["PersonaClave"].ToString();
            obj.PersonaUsuarioMod = dr["PersonaUsuarioMod"].ToString();
            obj.PersonaCodigoTelefono = dr["PersonaCodigoTelefono"].ToString();
            obj.PersonaGeoLocalizacionLast = Convert.ToDecimal(dr["PersonaGeoLocalizacionLast"].ToString());
            if (dr["PersonaCorreoValidado"].ToString()!="")
            {
                obj.PersonaCorreoValidado = Convert.ToBoolean(dr["PersonaCorreoValidado"].ToString());
            }
           
            obj.PersonaCodigoVerificacion =(dr["PersonaCodigoVerificacion"].ToString());
            obj.PersonaFacebookUid = (dr["PersonaFacebookUid"].ToString());
            obj.PersonaGmailUid = (dr["PersonaGmailUid"].ToString());
            obj.PersonaPhoneUid= (dr["PersonaPhoneUid"].ToString());
            obj.PersonaIdioma= (dr["PersonaIdioma"].ToString());
         
           
         
             
            obj.PersonaTokenIdHuawei = (dr["PersonaTokenIdHuawei"].ToString());
            obj.PersonaHuaweUid= (dr["PersonaHuaweUid"].ToString());


             

            return obj;
        }

        public void CargarRelaciones(ref List<BE.Persona> colObj, string lang = null, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.Ciudad> colciudad = null;
            List<BE.TipoLogin> colTipoLogin = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relPersona.ciudad))
                {
                    BC.Ciudad bcCiudad = new BC.Ciudad(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.CiudadId).Distinct();
                    colciudad = bcCiudad.ObtenerHijos(llaves, relaciones);
                    bcCiudad = null;
                }
                if (clase.Equals(BE.relPersona.tipoLogin))
                {
                    BC.TipoLogin bcTipoLogin = new BC.TipoLogin(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.TipoLoginId).Distinct();
                    colTipoLogin = bcTipoLogin.ObtenerHijos(llaves, lang, relaciones);
                    bcTipoLogin = null;
                }


            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colciudad != null && colciudad.Count > 0)
                    {
                        item.Ciudad = (from elemento in colciudad where elemento.CiudadId == item.CiudadId select elemento).ToList().FirstOrDefault();
                    }
                    if (colTipoLogin != null && colTipoLogin.Count > 0)
                    {
                        item.tipoLogin = (from elemento in colTipoLogin where elemento.TipoLoginId == item.TipoLoginId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }

        public void CargarRelaciones(ref BE.Persona obj,string lang=null, params Enum[] relaciones)
        {
            if (relaciones == null || obj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relPersona.tipoLogin))
                {
                    BC.TipoLogin bcTipoLogin = new BC.TipoLogin(cadenaConexion);
                    llaves = new decimal[] { obj.TipoLoginId };
                    obj.tipoLogin = bcTipoLogin.ObtenerHijos(llaves, lang, relaciones).FirstOrDefault();
                    bcTipoLogin = null;
                }
                if (clase.Equals(BE.relPersona.ciudad))
                {
                    BC.Ciudad bcCiudad = new BC.Ciudad(cadenaConexion);
                    llaves = new decimal[] { obj.CiudadId };
                    obj.Ciudad = bcCiudad.ObtenerHijos(llaves, relaciones).FirstOrDefault();
                    bcCiudad = null;
                }
                if (clase.Equals(BE.relPersona.serviciopersona))
                {
                    BC.ServicioPersona bcServicioPersona = new BC.ServicioPersona(cadenaConexion);
                    llaves = new decimal[] { obj.PersonaId };
                    obj.serviciopersona_Persona = bcServicioPersona.ObtenerHijos2(llaves, relaciones).FirstOrDefault();
                    bcServicioPersona = null;
                }
           

            }

        }
        #endregion

        #region "Listados"
        public List<BE.Persona> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.persona persona with(nolock) where persona.personaid in {1}", campos("persona"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion
        public List<BE.Persona> ListadoTokenProveedores(string requiereServicioId,decimal  StatusRequiereId , params Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Persona per with(nolock) inner join dbo.ServicioPersona spe with(nolock) on per.PersonaId = spe.PersonaId
                                            inner join dbo.RequiereServicioProveedores rsp with(nolock) on spe.ServicioPersonaId = rsp.ServicioPersonaId
                                            where rsp.RequiereServicioId = '{1}' and rsp.StatusRequiereId={2};", campos("per"), requiereServicioId, StatusRequiereId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }




     

    


        public BE.Persona ListadoSiniestrosCliente(string ServAsigId, params Enum[] relaciones)
        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select  {0} from ServAsig sa with (nolock)
inner join RequiereServicio rs with (nolock)
on sa.RequiereServicioId=rs.RequiereServicioId  
inner join Persona per with (nolock)
on (  rs.PersonaId=per.PersonaId  )
where ServAsigId='{1}'
", campos("per"), ServAsigId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public BE.Persona ListadoSiniestrosProveedor(string ServAsigId, params Enum[] relaciones)
        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select  {0} from ServAsig sa with (nolock)
inner join RequiereServicio rs with (nolock)
on sa.RequiereServicioId=rs.RequiereServicioId  
inner join Persona per with (nolock)
on (   sa.ProveedorId=per.PersonaId )
where ServAsigId='{1}'

", campos("per"), ServAsigId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<BE.Persona> ListadoObtenerIdPersonaProvedorAdj(string requiereServicioId, params Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"	select {0} from RequiereServicio rs with(nolock)
											inner join RequiereServicioProveedores rsp with(nolock)
											on rs.RequiereServicioId=rsp.RequiereServicioId
											inner join ServAsig sa  with(nolock)
											on rs.RequiereServicioId=sa.RequiereServicioId
											inner join Persona per with(nolock)
											on rs.PersonaId=per.PersonaId or sa.ProveedorId=per.PersonaId										
											where rs.RequiereServicioId='{1}'
											and StatusRequiereId=4", campos("per"), requiereServicioId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public BE.Persona BuscarPersonaxId(decimal PersonaId, string lang = "", params Enum[] relaciones)

        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT *
                                            FROM dbo.Persona per with(nolock) where per.PersonaId={1}", campos("per"), Convert.ToInt32(PersonaId));
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public BE.Persona Listado_Usuarios_Mas_Frecuentes_x_persona(decimal PersonaId, string lang = "", params Enum[] relaciones)
        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select {0} from requiereservicio re 
                                             inner join persona per on per.personaid = re.personaid 
                                             inner join servasig sg on sg.requiereservicioid = re.requiereservicioid 
                                             where  re.PersonaId NOT IN(SELECT  ppp.PersonaId  FROM PersonalPreuba ppp) 
                                             and  re.estadoreqservid in (2) and re.personaid = {1}
                                             group by {0} 
                                             HAVING count(per.personaid) > 1", campos("per"), Convert.ToInt32(PersonaId));

                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


   








        public List<BE.Persona> ListadoTokenProveedoresCotizado(string requiereServicioId, Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@" select {0}
from RequiereServicio rs with(nolock)
inner join RequiereServicioProveedores rsp 
on rs.RequiereServicioId=rsp.RequiereServicioId
inner join ServicioPersona sp with(nolock)
on rsp.ServicioPersonaId=sp.ServicioPersonaId
inner join Persona per with(nolock)
on sp.PersonaId=per.PersonaId

where
rsp.StatusRequiereId=2
and rs.RequiereServicioId='{1}';", campos("per"), requiereServicioId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                   // CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public List<BE.Persona> ListadoTokenClientePagado(string ServAsigId, Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select {0}
from ServAsig sa
inner join RequiereServicioProveedores rsp with(nolock)
on sa.RequiereServicioId=rsp.RequiereServicioId
inner join ServicioPersona sp with(nolock)
on rsp.ServicioPersonaId=sp.ServicioPersonaId
inner join Persona per with(nolock)
on sp.PersonaId=per.PersonaId
where
sa.StatusServAsigId=4
and sa.ServAsigId='{1}';", campos("per"), ServAsigId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public List<BE.Persona> ListadoPersonaFinServicio(string ServAsigId, Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select 
{0}
from 
ServAsig sa with (nolock)
inner join 
RequiereServicio rs with (nolock)
on sa.RequiereServicioId=rs.RequiereServicioId
inner join Persona per with (nolock)
on rs.PersonaId=per.PersonaId
and sa.ServAsigFHFin is not null
and sa.ServAsigId='{1}'
;", campos("per"), ServAsigId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public List<BE.Persona> ListadoPersonaProveedores_paraAdjudicar( string RequiereServicioId,Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"
select {0} from RequiereServicioProveedores rsp 
inner join ServicioPersona sp
on rsp.ServicioPersonaId=sp.ServicioPersonaId
inner join Persona per
on per.PersonaId=sp.PersonaId
where rsp.RequiereServicioId='{1}'
and rsp.StatusRequiereId=1

", campos("per"),RequiereServicioId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                   // CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public DataRow ListadoDatosNotificacion(string tipo, string lang)
        {
            DataRow dr = null;
           ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT lang, title, body, Nombre FROM dbo.Notificacion with(nolock) 
                                            where Nombre= '{0}' and lang='{1}';", tipo, lang);
                dr = conx.ObtenerFila(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }
        public DataRow ListadoDatosNotificacionv2(string tipo, string lang)
        {
            DataRow dr = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT lang, title, body,Nombre,Fragment,BotonTexto FROM dbo.Notificacion with(nolock) 
                                            where Nombre= '{0}' and lang='{1}';", tipo, lang);
                dr = conx.ObtenerFila(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }
        #region "Listados"
        public List<BE.Persona> ListadoEstadoSiniestros(decimal SiniestroId, decimal SiniestroEstadoId, params Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format("select {0} from Siniestro_Estado se with (nolock) inner join Siniestro s with (nolock) on " +
                    "se.SiniestroId = s.SiniestroId " +
                    "inner join ServAsig sasig  with (nolock)" +
                    " on s.ServAsigId = sasig.ServAsigId " +
                    "inner join RequiereServicio rs with (nolock)" +
                    " on sasig.RequiereServicioId = rs.RequiereServicioId " +
                    "inner join Persona per with (nolock) " +
                    " on(sasig.ProveedorId = per.PersonaId or rs.PersonaId = per.PersonaId) " +
                    "where s.SiniestroId ={1} and se.SiniestroEstadoId ={2}", campos("per"), SiniestroId, SiniestroEstadoId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }




        public List<BE.Persona> Listado_Personas_Anuncio( Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@" select {0}
from Persona per with(nolock) where  per.PersonaCorreo='yobi31018812@gmail.com'
;", campos("per"));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    // CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion

        #region "Busqueda"

   
        public BE.Persona BuscarPersonaProveedorxId(decimal PersonaId, params Enum[] relaciones)

        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Persona per with(nolock) where per.PersonaId={1}", campos("per"), Convert.ToInt32(PersonaId));
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion

        ///////////////////////////////////////////////////////////////////METODOS



        public DataRow Listado_total_usuarios()
        {
            DataRow dr = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0} FROM dbo.Persona per with(nolock)  INNER JOIN Ciudad c on c.CiudadId= per.CiudadId inner join region rr on rr.regionid = c.regionid  where rr.regionid in (SELECT  pp.Regionid    FROM Region pp where pp.paisId=1 ) and  c.CiudadId in (SELECT  cd.CiudadId   FROM Ciudad cd where cd.Ciudadid=2 ) and  p.PersonaId NOT IN(SELECT  ppp.PersonaId  FROM PersonalPreuba ppp)  ", campos("per"));
                dr = conx.ObtenerFila(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }
   


        public List<BE.Persona> Listado_total_usuarios1()
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0} FROM dbo.Persona per with(nolock)  INNER JOIN Ciudad c on c.CiudadId= per.CiudadId inner join region rr on rr.regionid = c.regionid  where rr.regionid in (SELECT  pp.Regionid    FROM Region pp where pp.paisId=1 ) and  c.CiudadId in (SELECT  cd.CiudadId   FROM Ciudad cd where cd.Ciudadid=2 ) and  per.PersonaId NOT IN(SELECT  ppp.PersonaId  FROM PersonalPreuba ppp)  ", campos("per"));
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

        public Boolean Registrar_publicidad()
        {
          
            BE.Persona persona = new BE.Persona();
            persona.TipoEstado = BE.TipoEstado.Insertar;
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            List<BE.Persona> lstPersonas = Listado_total_usuarios1();
            conx.Conectar();
            conx.ComenzarTransaccion();
            conx.ConfirmarTransaccion();
            dbConexion = conx;
            foreach (var personas in lstPersonas)
            {
                decimal personaidd = personas.PersonaId;
                try
                {
                 
                  
                   
                    for (int i = 0; i < 1; i++)
                    {
                        string val1 = "";
                        string val2 = "";
                        string imagen1 = "";
                        string imagen2 = "";
                        string imagen3 = "";
                        string imagen4 = "";
                        string imagen5 = "";

                        if (i == 0)
                        {
                            val1 = "Publicidad 2";
                            val2 = "administrador 2";
                            imagen1 = "ALFREDO MOSTACEDO.jpg";
                            imagen2 = "LUIS FERNANDO SILVA.jpg";
                            imagen3 = "not1.jpg";
                            imagen4 = "not2.jpg";
                            imagen5 = "not3.jpg";



                        }
                       // else
                       // {
                         //   if (i == 1)
                          //  {
                               

                           // }

                       // }


                        BC.Post bcPost = new BC.Post(cadenaConexion);
                        bcPost.dbConexion = conx;
                        BE.Post post = new BE.Post();
                        post.TipoEstado = persona.TipoEstado;
                        post.TipoPostId = 3;
                        post.PostDescripcion = val1;
                        post.PostEnlace = "www.google.com";
                        post.PostContenidoLast = 1;
                        post.PostCompartidoLast = 0;
                        post.PostFechaInsercion = DateTime.Now;
                        post.PostUsuario = "administrador";
                        post.PostUID = val2;
                        post.PostLikesLast = 0;
                        post.ServAsigId = null;
                        post.PersonaPostId = personaidd;
                        post.PostComentario = "";
                        post.PostCalificacion = 0;
                        post.PostAutorizaPublicacionImagen = true;
                        post.PostComentarioAprobacion = false;
                        bolOk = bcPost.Actualizar(ref post, true);
                        //  
                        dbConexion = conx;
                        if (bolOk == true)
                        {
                            BC.PostContenido bcPostContenido = new BC.PostContenido(cadenaConexion);
                            bcPostContenido.dbConexion = conx;
                            BE.PostContenido postContenido1 = new BE.PostContenido();
                            postContenido1.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido1.PostId = post.PostId;
                            postContenido1.PostContenidoId = 1;
                            postContenido1.PostContenidoImagen = imagen1;
                            postContenido1.PostContenidoVideo = null;
                            postContenido1.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido1, true);
                            BE.PostContenido postContenido2 = new BE.PostContenido();
                            postContenido2.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido2.PostId = post.PostId;
                            postContenido2.PostContenidoId = 2;
                            postContenido2.PostContenidoImagen = imagen2;
                            postContenido2.PostContenidoVideo = null;
                            postContenido2.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido2, true);
                            BE.PostContenido postContenido3 = new BE.PostContenido();
                            postContenido3.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido3.PostId = post.PostId;
                            postContenido3.PostContenidoId = 3;
                            postContenido3.PostContenidoImagen = imagen3;
                            postContenido3.PostContenidoVideo = null;
                            postContenido3.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido3, true);
                            BE.PostContenido postContenido4 = new BE.PostContenido();
                            postContenido4.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido4.PostId = post.PostId;
                            postContenido4.PostContenidoId = 4;
                            postContenido4.PostContenidoImagen = imagen4;
                            postContenido4.PostContenidoVideo = null;
                            postContenido4.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido4, true);
                            BE.PostContenido postContenido5 = new BE.PostContenido();
                            postContenido5.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido5.PostId = post.PostId;
                            postContenido5.PostContenidoId = 5;
                            postContenido5.PostContenidoImagen = imagen5;
                            postContenido5.PostContenidoVideo = null;
                            postContenido5.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido5, true);
                        }
                        i = i + 1;
                    }
                 
                   
                }
                catch (Exception ex)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                    throw ex;
                    bolOk = false;
                }

              
            }
            conx.Desconectar();



            return bolOk;
        }


        public Boolean RegistrarSolicitud_nuevo(ref BE.Persona persona)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                persona.PersonaClave = "9d380ac57a57be831e4e8429d38c9958";
                bolOk = Actualizar(ref persona, true);
                conx.ConfirmarTransaccion();
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }
                conx.Desconectar();
            }
            catch (Exception ex)
            {
                conx.CancelarTransaccion();
                conx.Desconectar();
                throw ex;
            }
            return bolOk;
        }

        public Boolean RegistrarSolicitud(ref BE.Persona persona)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                persona.PersonaClave = "9d380ac57a57be831e4e8429d38c9958";
                bolOk = Actualizar(ref persona, true);
                conx.ConfirmarTransaccion();
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }
                if (persona.TipoEstado == BE.TipoEstado.Insertar)
                {


                    for (int i = 0; i < 3; i++)
                    {
                        string val1 = "";
                        string val2 = "";
                        string imagen1 = "";
                        string imagen2 = "";
                        string imagen3 = "";
                        string imagen4 = "";
                        string imagen5 = "";


                        if (i == 0)
                        {
                            val1 = "Bienvenida 1";
                            val2 = "administrador 1";
                            imagen1 = "1.png";
                            imagen2 = "2.png";
                            imagen3 = "3.png";
                            imagen4 = "4.png";
                            imagen5 = "5.png";



                        }
                        else
                        {
                            if (i == 1)
                            {
                                val1 = "Bienvenida 2";
                                val2 = "administrador 2";
                                imagen1 = "ALFREDO MOSTACEDO.jpg";
                                imagen2 = "LUIS FERNANDO SILVA.jpg";
                                imagen3 = "not1.jpg";
                                imagen4 = "not2.jpg";
                                imagen5 = "not3.jpg";

                            }
                            else
                            {
                                val1 = "Bienvenida 3";
                                val2 = "administrador 3";

                                imagen1 = "yacquelin camacho.jpg";
                                imagen2 = "not3.jpg";
                                imagen3 = "ALFREDO MOSTACEDO.jpg";
                                imagen4 = "not2.jpg";
                                imagen5 = "LUIS FERNANDO SILVA.jpg";
                            }
                        }


                        BC.Post bcPost = new BC.Post(cadenaConexion);
                        bcPost.dbConexion = conx;
                        BE.Post post = new BE.Post();
                        post.TipoEstado = BE.TipoEstado.Insertar;
                        post.TipoPostId = 3;
                        post.PostDescripcion = val1;
                        post.PostEnlace = "www.google.com";
                        post.PostContenidoLast = 1;
                        post.PostCompartidoLast = 0;
                        post.PostFechaInsercion = DateTime.Now;
                        post.PostUsuario = "administrador";
                        post.PostUID = val2;
                        post.PostLikesLast = 0;
                        post.ServAsigId = null;
                        post.PersonaPostId = persona.PersonaId;
                        post.PostComentario = "";
                        post.PostCalificacion = 0;
                        post.PostAutorizaPublicacionImagen = true;
                        post.PostComentarioAprobacion = false;
                        bolOk = bcPost.Actualizar(ref post, true);
                        //  
                        dbConexion = conx;
                        if (bolOk == true)
                        {
                            BC.PostContenido bcPostContenido = new BC.PostContenido(cadenaConexion);
                            bcPostContenido.dbConexion = conx;
                            BE.PostContenido postContenido1 = new BE.PostContenido();
                            postContenido1.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido1.PostId = post.PostId;
                            postContenido1.PostContenidoId = 1;
                            postContenido1.PostContenidoImagen = imagen1;
                            postContenido1.PostContenidoVideo = null;
                            postContenido1.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido1, true);
                            BE.PostContenido postContenido2 = new BE.PostContenido();
                            postContenido2.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido2.PostId = post.PostId;
                            postContenido2.PostContenidoId = 2;
                            postContenido2.PostContenidoImagen = imagen2;
                            postContenido2.PostContenidoVideo = null;
                            postContenido2.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido2, true);
                            BE.PostContenido postContenido3 = new BE.PostContenido();
                            postContenido3.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido3.PostId = post.PostId;
                            postContenido3.PostContenidoId = 3;
                            postContenido3.PostContenidoImagen = imagen3;
                            postContenido3.PostContenidoVideo = null;
                            postContenido3.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido3, true);
                            BE.PostContenido postContenido4 = new BE.PostContenido();
                            postContenido4.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido4.PostId = post.PostId;
                            postContenido4.PostContenidoId = 4;
                            postContenido4.PostContenidoImagen = imagen4;
                            postContenido4.PostContenidoVideo = null;
                            postContenido4.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido4, true);
                            BE.PostContenido postContenido5 = new BE.PostContenido();
                            postContenido5.TipoEstado = BE.TipoEstado.Insertar;
                            postContenido5.PostId = post.PostId;
                            postContenido5.PostContenidoId = 5;
                            postContenido5.PostContenidoImagen = imagen5;
                            postContenido5.PostContenidoVideo = null;
                            postContenido5.PostContenidoVisible = null;
                            bolOk = bcPostContenido.Actualizar(ref postContenido5, true);


                        }
                    }



                    if (bolOk == true)
                    {

                        bolOk = InsertarBilleteraSaldoInicio(persona.CiudadId, persona.PersonaId, conx);
                    }

                }




                conx.Desconectar();


            }
            catch (Exception ex)
            {
                conx.CancelarTransaccion();
                conx.Desconectar();
                throw ex;
                bolOk = false;
            }
            return bolOk;
        }
        public void InsertarPost(ref BE.Persona persona, ClaseConexion conx)
        {

           
            try
            {
                string sql = "InsertarPost";
              
                conx.CrearComando(sql, CommandType.StoredProcedure);


                //////////////////////////
                conx.AsignarParametro("@TipoPostId", 3);
                conx.AsignarParametro("@PostDescripcion", "Bienvenido");
                conx.AsignarParametro("@PostEnlace", "www.google.com");
                conx.AsignarParametro("@PostContenidoLast", 1);
                conx.AsignarParametro("@PostCompartidoLast", 0);
                conx.AsignarParametro("@PostFechaInsercion", DateTime.Now);
                conx.AsignarParametro("@PostUsuario", "administrador");
                conx.AsignarParametro("@PostUID", "administrador");
                conx.AsignarParametro("@PostLikesLast", 0);
                conx.AsignarParametro("@ServAsigId", DBNull.Value);
                conx.AsignarParametro("@PersonaPostId", persona.PersonaId);
                conx.AsignarParametro("@PostComentario", "");
                conx.AsignarParametro("@PostCalificacion", 0.0);
                conx.AsignarParametro("@PostAutorizaPublicacionImagen", true);
                conx.AsignarParametro("@PostComentarioAprobacion", false);
                conx.AsignarParametro("@Identity", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                int valorCambio = Convert.ToInt32(conx.ObtenerParametro("@Identity").ToString());


            
            }
            catch (Exception ex)
            {
                conx.Desconectar();
                throw ex;
            }

        }
        public bool InsertarBilleteraSaldoInicio(decimal ciudaId,decimal PersonaId, ClaseConexion conx)
        {
           bool bolOk = false;

            try
            {
                string sql = "[InsertarBilleteraSaldoInicio]";

                conx.CrearComando(sql, CommandType.StoredProcedure);


                //////////////////////////
                conx.AsignarParametro("@CiudadId", ciudaId);
                conx.AsignarParametro("@PersonaBilleteraId", PersonaId);
                
                conx.EjecutarComando();


                bolOk = true;
            }
            catch (Exception ex)
            {
                conx.Desconectar();
                throw ex;
            }

            return bolOk;
        }
        /////////////////////////////////////////////////////////////////////////METODOS NOTIFICACIONES
        #region "NOTIFICACIONES"  

        public DataSet ObtenerCortizaciones()

        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            DataSet datos = new DataSet();
            try
            {
                string sql = String.Format(@"select Count (rs.PersonaId)cantidad,rs.PersonaId from requiereServicio rs with (nolock)
inner join 
 [dbo].[RequiereServicioProveedores] rsp with (nolock)
 on rs.RequiereServicioId=rsp.RequiereServicioId
 where StatusRequiereId=2
 group by rs.PersonaId");
                datos = conx.ObtenerDataSet(sql);
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        #endregion

        #region "Metodos agegados al unior con IOS"
        public BE.Persona VerPersona1(string PersonaCorreo, decimal TipoLoginId, params Enum[] relaciones)
        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from Persona with(nolock) where PersonaCorreo='{0}' and  TipoLoginId={1}", PersonaCorreo, TipoLoginId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //CargarRelaciones(ref obj, lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public List<BE.Persona> ValidarExistePersona2(string PersonaCorreo, decimal TipoLoginId, string PersonaCodigoTelefono, string PersonaTelefono, string PersonaUID, string lang, params Enum[] relaciones)
        {
            Boolean bolOk = false;

            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);

            try
            {
                string cod = "+" + PersonaCodigoTelefono;
                string sql = String.Format(@"select * from Persona with (nolock) where (PersonaCorreo='{0}' or (PersonaCodigoTelefono='{1}' and PersonaTelefono='{2}') or PersonaUID='{3}' )", PersonaCorreo, cod, PersonaTelefono, PersonaUID);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, lang, relaciones);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public int ValidarExistePersona(string PersonaCorreo, decimal TipoLoginId, string PersonaCodigoTelefono, string PersonaTelefono, string PersonaUID)
        {
            Boolean bolOk = false;


            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.CrearComando("dbo.ValidarExistePersonaV2", CommandType.StoredProcedure);
                /////////////////////////////
                conx.AsignarParametro("@TipoLoginId", TipoLoginId);
                conx.AsignarParametro("@PersonaCorreo", PersonaCorreo);
                conx.AsignarParametro("@PersonaCodigoTelefono", PersonaCodigoTelefono);
                conx.AsignarParametro("@PersonaTelefono", PersonaTelefono);               
             int cantidad = Convert.ToInt32(conx.EjecutarScalar());
                if (cantidad == 0)
                {
                    conx.CrearComando("dbo.ValidarExistePersonaV1", CommandType.StoredProcedure);
                    /////////////////////////////
                    conx.AsignarParametro("@PersonaUID", PersonaUID);
                    
                  cantidad = Convert.ToInt32(conx.EjecutarScalar());

                }


                return cantidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }


        public BE.Persona ExistePersona(decimal PersonaId,params Enum[] relaciones)
        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from persona with(nolock)  where personaId={0} ", PersonaId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //CargarRelaciones(ref obj, lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public List<BE.Persona> VerPersona(params Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from Persona with(nolock) ");
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //CargarRelaciones(ref obj, lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public List<BE.Persona> VerPersonaPersonaTokenId(string PersonaTokenId,params Enum[] relaciones)
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from persona with(nolock)  where PersonaTokenId='{0}' ", PersonaTokenId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //CargarRelaciones(ref obj, lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public void ActualizarGeolocalizacion(decimal IdPersona, string PersonaGeoReal)
        {
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "ActualizarGeolocalizacion";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdPersona", IdPersona);
                conx.AsignarParametro("@PersonaGeoReal", PersonaGeoReal);

              conx.EjecutarComando();

            }
            catch (Exception ex)
            {

                throw;
            }




        }

        public BE.Persona ConfirmarCorreoPersona(decimal PersonaId,string pin, params Enum[] relaciones)
        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from persona with(nolock)  where personaId={0} and PersonaCodigoVerificacion='{1}' ", PersonaId,pin);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //CargarRelaciones(ref obj, lang, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion

        public BE.Persona BuscarPorUID(string uid, params Enum[] relaciones)
        {
            BE.Persona obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0} from dbo.Persona per with(nolock) where per.PersonaUID = '{1}'", campos("per"), uid);
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
        public BE.Persona ValidarExistePersona_Web(string usuario, string password)
        {
            BE.Persona obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0} from dbo.Persona per with(nolock) where per.PersonaUsuario = '{1}' and per.PersonaClave='{2}'", campos("per"), usuario,password);
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
        public BE.Persona ValidarLoginWeb(string PersonaUsuario, string PersonaClave, params Enum[] relaciones)
        {
            BE.Persona obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                  stream = md5.ComputeHash(encoding.GetBytes(PersonaClave));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                string ff = sb.ToString();
                string sql = String.Format(@"SELECT {0} from dbo.Persona per with(nolock) where per.PersonaUsuario 
                = '{1}' and per.PersonaClave = '{2}' ", campos("per"),PersonaUsuario,ff);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public BE.Persona ValidarCorreo(string Correo, params Enum[] relaciones)
        {
            BE.Persona obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
             
               
                string sql = String.Format(@"select {0} from Persona per with(nolock) where PersonaCorreo = '{1}' and EstadoPersonaId=1  ", campos("per"), Correo);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public BE.Persona actualizar_passwordWeb(decimal idp, string password, params Enum[] relaciones)
        {
            BE.Persona obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            conx.Conectar();
            try
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = md5.ComputeHash(encoding.GetBytes(password));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                string ff = sb.ToString();
                string sql = "update dbo.Persona  set PersonaClave = @PersonaClave where personaId=@personaId";
                conx.CrearComando(sql);
                conx.AsignarParametro("@PersonaClave",ff);
                conx.AsignarParametro("@personaId", idp);

                conx.EjecutarComando();

                conx.Desconectar();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }







        public List<BE.Persona> ListadoOperaciones()
        {
            List<BE.Persona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format("SELECT per.* FROM dbo.Persona per with(nolock) inner join dbo.persona_operaciones po with(nolock) on per.PersonaId = po.PersonaId");
                //string sql = String.Format("SELECT per.* FROM dbo.Persona per with(nolock) where per.personaid=1171");
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    // CargarRelaciones(ref obj, null, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }





        public BE.Persona BuscarPersonaxId_enprueba(decimal PersonaId)

        {
            BE.Persona obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select {0} from persona per 
                                   inner join personalpreuba pr with(nolock) on pr.personaid=per.personaid where per.personaid='{1}'", campos("per"), Convert.ToInt32(PersonaId));
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    //  CargarRelaciones(ref obj);
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
