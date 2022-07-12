using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class Conversacion:BCEntidad
    {
        public Conversacion() : base()
        {
        }

        public Conversacion(string cadConx) : base(cadConx)
        {

        }

        private string campos(string prefijo = "conver")
        {
            string strCampos = String.Format(@"{0}.ConversacionId, {0}.ConversacionPersonaIdEmisor, {0}.ConversacionPersonaIdReceptor, {0}.ServAsigId, {0}.ConversacionContenido,
{0}.ConversacionFechaHora"
                    , prefijo);
            return strCampos;
        }

        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.Conversacion BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.conversacion
                                    SET 
	                                    ConversacionPersonaIdEmisor = @ConversacionPersonaIdEmisor,
	                                    ConversacionPersonaIdReceptor = @ConversacionPersonaIdReceptor,
	                                    ServAsigId = @ServAsigId,
	                                    ConversacionContenido = @ConversacionContenido,
	                                    ConversacionFechaHora = @ConversacionFechaHora,
	                                 
                                    where  ConversacionId = @ConversacionId   ;";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO dbo.conversacion (ConversacionPersonaIdEmisor, ConversacionPersonaIdReceptor, ServAsigId, ConversacionContenido, ConversacionFechaHora)
                                VALUES (@ConversacionPersonaIdEmisor, @ConversacionPersonaIdReceptor, @ServAsigId, @ConversacionContenido, @ConversacionFechaHora);";
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
                    }
                    conx.CrearComando(strSql);

                    conx.AsignarParametro("@ConversacionPersonaIdEmisor", BEObj.ConversacionPersonaIdEmisor);
                    conx.AsignarParametro("@ConversacionPersonaIdReceptor", BEObj.ConversacionPersonaIdReceptor);
                    conx.AsignarParametro("@ServAsigId", BEObj.ServAsigId);
                    conx.AsignarParametro("@ConversacionContenido", BEObj.ConversacionContenido);
                    conx.AsignarParametro("@ConversacionFechaHora", BEObj.ConversacionFechaHora);

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



        public List<BE.Conversacion> CargarBE(DataRow[] dr)
        {
            List<BE.Conversacion> lst = new List<BE.Conversacion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Conversacion CargarBE(DataRow dr)
        {
            BE.Conversacion obj = new BE.Conversacion();
            obj.ConversacionId = Convert.ToDecimal(dr["ConversacionId"].ToString());
            obj.ConversacionPersonaIdEmisor = Convert.ToDecimal(dr["ConversacionPersonaIdEmisor"].ToString());
            obj.ConversacionPersonaIdReceptor = Convert.ToDecimal(dr["ConversacionPersonaIdReceptor"].ToString());
            obj.ConversacionContenido = Convert.ToString(dr["ConversacionContenido"].ToString());
            obj.ConversacionFechaHora = Convert.ToDateTime(dr["ConversacionFechaHora"].ToString());

            return obj;
        }




        #endregion

    }
}
