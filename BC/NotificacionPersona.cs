using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
   public class NotificacionPersona:BCEntidad
    {
        public NotificacionPersona() : base()
        {
        }

        public NotificacionPersona(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.NotificacionPersona BEObj, Boolean isTransaccion = false)
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
                        strSql = @"
update NotificacionPersona set
RequiereServicioId=@RequiereServicioId,
PersonaId=@PersonaId,
TipoEstadoId=@TipoEstadoId,
NotificacionPersonaTitulo=@NotificacionPersonaTitulo,
NotificacionPersonaDescripcion=@NotificacionPersonaDescripcion,
NotificacionPersonaFechaRegistro=@NotificacionPersonaFechaRegistro,
NotificacionPersonaFragment=@NotificacionPersonaFragment,
NotificacionPersonaIcono=@NotificacionPersonaIcono,
EstadoNotificacionId=@EstadoNotificacionId,
ConceptoNotificacionId=@ConceptoNotificacionId
where NotificacionPersonaId=@NotificacionPersonaId
";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO NotificacionPersona(NotificacionPersonaId,RequiereServicioId,PersonaId,TipoEstadoId,NotificacionPersonaTitulo,NotificacionPersonaDescripcion,
NotificacionPersonaFechaRegistro,NotificacionPersonaFragment,NotificacionPersonaIcono,EstadoNotificacionId,ConceptoNotificacionId) VALUES(@NotificacionPersonaId,@RequiereServicioId,@PersonaId,@TipoEstadoId,@NotificacionPersonaTitulo,@NotificacionPersonaDescripcion,
@NotificacionPersonaFechaRegistro,@NotificacionPersonaFragment,@NotificacionPersonaIcono,@EstadoNotificacionId,@ConceptoNotificacionId)
;";
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
                        BEObj.NotificacionPersonaId = System.Convert.ToDecimal(conx.ObtenerValor("select isnull(max(NotificacionPersonaId),0) + 1 from dbo.NotificacionPersona with (nolock);"));
                       
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@NotificacionPersonaId", BEObj.NotificacionPersonaId);
                    conx.AsignarParametro("@RequiereServicioId", BEObj.RequiereServicioId);
                    conx.AsignarParametro("@personaid", BEObj.PersonaId);
                    conx.AsignarParametro("@TipoEstadoId", BEObj.TipoEstadoId);
                    conx.AsignarParametro("@NotificacionPersonaTitulo", BEObj.NotificacionPersonaTitulo);
                    conx.AsignarParametro("@NotificacionPersonaDescripcion", BEObj.NotificacionPersonaDescripcion);
                    conx.AsignarParametro("@NotificacionPersonaFechaRegistro", BEObj.NotificacionPersonaFechaRegistro);
                    conx.AsignarParametro("@NotificacionPersonaFragment", BEObj.NotificacionPersonaFragment);
                    conx.AsignarParametro("@NotificacionPersonaIcono", BEObj.NotificacionPersonaIcono);
                    conx.AsignarParametro("@EstadoNotificacionId", BEObj.EstadoNotificacionId);
                    conx.AsignarParametro("@ConceptoNotificacionId", BEObj.ConceptoNotificacionId);
                  
                
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



     


        #endregion
        public List<BE.NotificacionPersona> CargarBE(DataRow[] dr)
        {
            List<BE.NotificacionPersona> lst = new List<BE.NotificacionPersona>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.NotificacionPersona CargarBE(DataRow dr)
        {
            BE.NotificacionPersona notificacionPersona = new BE.NotificacionPersona();

            notificacionPersona.NotificacionPersonaId = Convert.ToDecimal(dr["NotificacionPersonaId"].ToString());
            notificacionPersona.RequiereServicioId = (dr["RequiereServicioId"].ToString());
            notificacionPersona.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            notificacionPersona.TipoEstadoId = Convert.ToDecimal(dr["TipoEstadoId"].ToString());
            notificacionPersona.NotificacionPersonaTitulo = (dr["NotificacionPersonaTitulo"].ToString());
            notificacionPersona.NotificacionPersonaDescripcion = (dr["NotificacionPersonaDescripcion"].ToString());
            notificacionPersona.NotificacionPersonaFechaRegistro = Convert.ToDateTime(dr["NotificacionPersonaFechaRegistro"].ToString());

            notificacionPersona.NotificacionPersonaFragment = dr["NotificacionPersonaFragment"].ToString();
            notificacionPersona.NotificacionPersonaIcono = dr["NotificacionPersonaIcono"].ToString();
            notificacionPersona.EstadoNotificacionId = Convert.ToDecimal(dr["EstadoNotificacionId"].ToString());
            notificacionPersona.ConceptoNotificacionId = Convert.ToDecimal(dr["ConceptoNotificacionId"].ToString());
          

            return notificacionPersona;

        }

        public void CargarRelaciones(ref List<BE.NotificacionPersona> colObj, IEnumerable<decimal> estados = null, string lang = "", long personaId = 0, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<decimal> llavesP;
            IEnumerable<string> sllaves;
            List<BE.EstadoNotificacion> colEstadoNotificacion = null;
            List<BE.ConceptoNotificacion> colConceptoNotificacion = null;
            List<BE.TipoEstadoNotificacion> colTipoEstadoNotificacion = null;

            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relNotificacionPersona.estadoNotificacion))
                {
                    BC.EstadoNotificacion bcEstadoNotificacion = new BC.EstadoNotificacion(cadenaConexion);

                    llaves = (from elemento in colObj where elemento.EstadoNotificacionId != null select Convert.ToDecimal(elemento.EstadoNotificacionId)).Distinct();
                    colEstadoNotificacion = bcEstadoNotificacion.ObtenerHijos(llaves, lang, relaciones);
                    bcEstadoNotificacion = null;
                }
                if (clase.Equals(BE.relNotificacionPersona.conceptoNotificacion))
                {
                    BC.ConceptoNotificacion bcConceptoNotificacion = new BC.ConceptoNotificacion(cadenaConexion);

                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.ConceptoNotificacionId)).Distinct();
                    colConceptoNotificacion = bcConceptoNotificacion.ObtenerHijos(llaves, lang, relaciones);
                    bcConceptoNotificacion = null;
                }
                if (clase.Equals(BE.relNotificacionPersona.tipoEstadoNotificacion))
                {
                    BC.TipoEstadoNotificacion bcTipoEstadoNotificacion = new BC.TipoEstadoNotificacion(cadenaConexion);

                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.TipoEstadoId)).Distinct();
                    colTipoEstadoNotificacion = bcTipoEstadoNotificacion.ObtenerHijos(llaves, lang, relaciones);
                    bcTipoEstadoNotificacion = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {
                    if (colEstadoNotificacion != null && colEstadoNotificacion.Count > 0)
                    {
                        item.estadoNotificacion = (from elemento in colEstadoNotificacion where elemento.EstadoNotificacionId == item.EstadoNotificacionId select elemento).ToList().FirstOrDefault();
                    }
                    if (colConceptoNotificacion != null && colConceptoNotificacion.Count > 0)
                    {
                        item.conceptoNotificacion = (from elemento in colConceptoNotificacion where elemento.ConceptoNotificacionId == item.ConceptoNotificacionId select elemento).ToList().FirstOrDefault();
                    }
                    if (colTipoEstadoNotificacion != null && colTipoEstadoNotificacion.Count > 0)
                    {
                        item.tipoEstadoNotificacion = (from elemento in colTipoEstadoNotificacion where elemento.TipoEstadoId == item.TipoEstadoId select elemento).ToList().FirstOrDefault();
                    }

                }
            }
        }


        public List<BE.NotificacionPersona> ListadoNotificacionPersona(long personaId, string lang, params Enum[] relaciones)
        {
            List<BE.NotificacionPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from NotificacionPersona with (nolock) where PersonaId in ({0}) 
order by NotificacionPersonaFechaRegistro desc", personaId, lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, null, lang, personaId, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public int CantidadLeidoNotificacionPersona(long personaId, string lang, params Enum[] relaciones)
        {
            List<BE.NotificacionPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            conx.Conectar();
            int resultado = 0;
            try
            {
                string sql = String.Format(@"select Count (PersonaId) from [dbo].[NotificacionPersona] with (nolock)
where PersonaId={0} and EstadoNotificacionId in (1)", personaId, lang);
              
             resultado = Convert.ToInt32(conx.ObtenerValor(sql));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }
        public bool saveNotificacionPersona(ref BE.NotificacionPersona notificacionPersona)
        {
            List<BE.NotificacionPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            bool bolOk = false;
            try
            {
                if (notificacionPersona.TipoEstado == BE.TipoEstado.Insertar)
                {
                    bolOk = Actualizar(ref notificacionPersona);
                }
                else
                {
                    bolOk = Actualizar(ref notificacionPersona);



                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolOk;
        }

        public List<BE.NotificacionPersona> LisTadoNotificacionPersonaPaginacion(decimal personaId,string lang, int index, int max, params Enum[] relaciones)
        {
            List<BE.NotificacionPersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from NotificacionPersona with (nolock) where PersonaId in ({0}) 
order by NotificacionPersonaFechaRegistro desc
OFFSET {1} ROWS
FETCH NEXT {2} ROWS ONLY", personaId, index - 1, max);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {

                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,null,"",0, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public int cantidadNotificacionPersona(decimal personaId)
        {
            int obj = 0;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select * from NotificacionPersona with (nolock) where PersonaId in ({0})

                        ", personaId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = dr.Length;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public Boolean ActualizarEstadoAdjudicacion(string RequiereServicioId, Boolean isTransaccion = false)
        {
            string strSql = string.Empty;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            bool bolOk = false;

            try
            {
                       
                        strSql = @"update [dbo].[NotificacionPersona] set NotificacionPersonaFragment='FragmentViewNotification' where RequiereServicioId=@RequiereServicioId  and ConceptoNotificacionId=2";              
                
                if (isTransaccion)
                    conx = dbConexion;
                else
                {
                    conx.Conectar();
                    conx.ComenzarTransaccion();
                }
              

                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@RequiereServicioId", RequiereServicioId);
                   


           
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


    }
}
