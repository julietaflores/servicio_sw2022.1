using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class cobranzaCBA : BCEntidad
    {
        public cobranzaCBA() : base()
        {
        }

        public cobranzaCBA(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"


        public Boolean Actualizar(ref BE.cobranzaCBA BEObj, Boolean isTransaccion = false)
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
                        strSql = @"insert into CobranzaCBA (CobranzaCBAId,PersonaId,CobranzaCBANombre,CobranzaCBACarnet,CobranzaCBACodigoEstudiante,
CobranzaCBANivel,CobranzaCBAModulo,CobranzaCBAQuiereLibro,CobranzaCBALibro,CobranzaCBABecado,PersonaDireccionId,
CobranzaCBAFechaRegistro,CobranzaCBAFechaCobranza,CobranzaCBAEstado,CobranzaCBAUID)VALUES
 (@CobranzaCBAId,@PersonaId,@CobranzaCBANombre,@CobranzaCBACarnet,@CobranzaCBACodigoEstudiante,
@CobranzaCBANivel,@CobranzaCBAModulo,@CobranzaCBAQuiereLibro,@CobranzaCBALibro,@CobranzaCBABecado,@PersonaDireccionId,
@CobranzaCBAFechaRegistro,@CobranzaCBAFechaCobranza,@CobranzaCBAEstado,@CobranzaCBAUID)";
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
                        BEObj.cobranzaCBAId = System.Convert.ToDecimal(conx.ObtenerValor("select isnull(max(CobranzaCBAId),0) + 1 from dbo.CobranzaCBA with (nolock);"));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@CobranzaCBAId", BEObj.cobranzaCBAId);
                    conx.AsignarParametro("@personaId", BEObj.personaId);
                    conx.AsignarParametro("@CobranzaCBANombre", BEObj.cobranzaCBANombre);
                    conx.AsignarParametro("@CobranzaCBACarnet", BEObj.cobranzaCBACarnet);
                    conx.AsignarParametro("@CobranzaCBACodigoEstudiante", BEObj.cobranzaCBACodigoEstudiante);
                    conx.AsignarParametro("@CobranzaCBANivel", BEObj.cobranzaCBANivel);
                    conx.AsignarParametro("@CobranzaCBAModulo", BEObj.cobranzaCBAModulo);
                    conx.AsignarParametro("@CobranzaCBAQuiereLibro", BEObj.cobranzaCBAQuiereLibro);
                    conx.AsignarParametro("@CobranzaCBALibro", BEObj.cobranzaCBALibro);
                    conx.AsignarParametro("@CobranzaCBABecado", BEObj.cobranzaCBABecado);
                    conx.AsignarParametro("@PersonaDireccionId", BEObj.personaDireccionId);
                    conx.AsignarParametro("@CobranzaCBAFechaRegistro", BEObj.cobranzaCBAFechaRegistro);
                    conx.AsignarParametro("@CobranzaCBAFechaCobranza", BEObj.cobranzaCBAFechaCobranza);
                    conx.AsignarParametro("@CobranzaCBAEstado", BEObj.cobranzaCBAEstado);
                    conx.AsignarParametro("@CobranzaCBAUID", BEObj.cobranzaCBAUID);

                    conx.EjecutarComando();

                    if (!isTransaccion)
                    {
                        conx.ConfirmarTransaccion();
                        conx.Desconectar();
                    }

                    bolOk = true;
                }
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


        public List<BE.cobranzaCBA> CargarBE(DataRow[] dr)
        {
            List<BE.cobranzaCBA> lst = new List<BE.cobranzaCBA>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.cobranzaCBA CargarBE(DataRow dr)
        {
            BE.cobranzaCBA obj = new BE.cobranzaCBA();
            obj.cobranzaCBAId = Convert.ToDecimal(dr["CobranzaCBAId"].ToString());
            obj.personaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.cobranzaCBANombre = dr["CobranzaCBANombre"].ToString();
            obj.cobranzaCBACarnet = dr["CobranzaCBACarnet"].ToString();
            obj.cobranzaCBACodigoEstudiante = (dr["CobranzaCBACodigoEstudiante"].ToString());
            obj.cobranzaCBANivel = dr["CobranzaCBANivel"].ToString();
            obj.cobranzaCBAModulo = dr["cobranzaCBAModulo"].ToString();
            obj.cobranzaCBAQuiereLibro = Convert.ToBoolean(dr["CobranzaCBAQuiereLibro"].ToString());
            obj.cobranzaCBALibro = dr["CobranzaCBALibro"].ToString();
            obj.cobranzaCBABecado = Convert.ToBoolean(dr["cobranzaCBABecado"].ToString());
            obj.personaDireccionId = Convert.ToDecimal(dr["PersonaDireccionId"].ToString());
            obj.cobranzaCBAFechaRegistro = Convert.ToDateTime(dr["CobranzaCBAFechaRegistro"].ToString());
            obj.cobranzaCBAFechaCobranza = Convert.ToDateTime(dr["CobranzaCBAFechaCobranza"].ToString());
            obj.cobranzaCBAEstado = (dr["CobranzaCBAEstado"].ToString());
            obj.cobranzaCBAUID = (dr["CobranzaCBAUID"].ToString());
            return obj;


        }

        public void CargarRelaciones(ref List<BE.Billetera> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.Moneda> colMoneda = null;
            List<BE.BilleteraDetalle> colBilleteraDetalle = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relBilletera.moneda))
                {
                    BC.Moneda bcMoneda = new BC.Moneda(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.MonedaId).Distinct();
                    colMoneda = bcMoneda.ObtenerHijos(llaves, relaciones);
                    bcMoneda = null;
                }
                if (clase.Equals(BE.relBilletera.billeteraDetalle))
                {
                    BC.BilleteraDetalle bcBilleteraDetalle = new BC.BilleteraDetalle(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.MonedaId).Distinct();
                    colBilleteraDetalle = bcBilleteraDetalle.ObtenerHijos(llaves, relaciones);
                    bcBilleteraDetalle = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colMoneda != null && colMoneda.Count > 0)
                    {
                        item.moneda = (from elemento in colMoneda where elemento.MonedaId == item.MonedaId select elemento).ToList().FirstOrDefault();
                    }

                    if (colBilleteraDetalle != null && colBilleteraDetalle.Count > 0)
                    {
                        item.billeteraDetalles = (from elemento in colBilleteraDetalle where elemento.BilleteraId == item.BilleteraId select elemento).ToList();
                    }
                }
            }
        }

        public BE.cobranzaCBA BuscarPorUID(string uid, params Enum[] relaciones)
        {
            BE.cobranzaCBA obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT * from dbo.[CobranzaCBA] c with(nolock) where c.CobranzaCBAUID = '{0}'", uid);
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

