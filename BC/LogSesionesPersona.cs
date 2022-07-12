using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
     public class LogSesionesPersona:BCEntidad
    {
        public LogSesionesPersona() : base()
        {
        }

        public LogSesionesPersona(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        public Boolean Actualizar(ref BE.LogSesionesPersona BEObj, Boolean isTransaccion = false)
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
                        strSql = @"";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into [dbo].[LogSesionesPersona](LogSesionesPersonaId,PersonaId,LogSesionesPersonaFecha,TipoLoginId,LogSesionesPersonaVersion,LogSesionesPersonaSO,LogSesionesPersonaIdioma)
values(@LogSesionesPersonaId,@PersonaId,@LogSesionesPersonaFecha,@TipoLoginId,@LogSesionesPersonaVersion,@LogSesionesPersonaSO,@LogSesionesPersonaIdioma)";
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

                    if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    {
                        if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                        {
                            BEObj.LogSesionesPersonaId = System.Convert.ToDecimal(conx.ObtenerValor("select isnull(max(LogSesionesPersonaId),0) + 1 from dbo.LogSesionesPersona with (nolock);"));
                        }
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@LogSesionesPersonaId", BEObj.LogSesionesPersonaId);
                    conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
                    conx.AsignarParametro("@LogSesionesPersonaFecha", BEObj.LogSesionesPersonaFecha);
                    conx.AsignarParametro("@TipoLoginId", BEObj.TipoLoginId);
                    conx.AsignarParametro("@LogSesionesPersonaVersion", BEObj.LogSesionesPersonaVersion);
                    conx.AsignarParametro("@LogSesionesPersonaSO", BEObj.LogSesionesPersonaSO);
                    conx.AsignarParametro("@LogSesionesPersonaIdioma", BEObj.LogSesionesPersonaIdioma);
                 

                }
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
                if (!isTransaccion)
                {
                    conx.CancelarTransaccion();
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }

        public Boolean RegistrarLogSesionesPersona(ref BE.LogSesionesPersona logSesionesPersona)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                bolOk = Actualizar(ref logSesionesPersona, true);

                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }
                if (bolOk==true)
                {
                    BC.Persona bcpersona = new BC.Persona(cadenaConexion);
                    bcpersona.dbConexion = conx;
                 BE.Persona persona=   bcpersona.BuscarPersonaxId(logSesionesPersona.PersonaId, logSesionesPersona.LogSesionesPersonaIdioma, null);
                    if (persona!= null)
                    {
                        persona.TipoEstado = BE.TipoEstado.Modificar;
                        persona.PersonaIdioma=logSesionesPersona.LogSesionesPersonaIdioma;
                        bcpersona.Actualizar(ref persona, true);
                    }

                    bcpersona = null;
                }
                conx.ConfirmarTransaccion();
                conx.Desconectar();
                bolOk = true;
            }
            catch (Exception ex)
            {
                conx.CancelarTransaccion();
                conx.Desconectar();
                throw ex;
            }
            return bolOk;
        }

        public string  VersionTelefono(decimal personaId)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            string obj = "";
            try
            {
             
               
                try
                {
                    string sql = String.Format(@"select top 1 LogSesionesPersonaSO from [dbo].[LogSesionesPersona] with(nolock) where PersonaId={0} order by LogSesionesPersonaFecha desc",personaId);
                    obj = Convert.ToString(conx.ObtenerValor(sql));


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return obj;
            }
            catch (Exception ex)
            {
                conx.CancelarTransaccion();
                conx.Desconectar();
                throw ex;
            }
            return obj;
        }

    }
}
