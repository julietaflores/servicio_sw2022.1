using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BC
{
   public class BilleteraPagoTarjeta : BCEntidad
    {

        private string campos(string prefijo = "BPT")
        {
            string strCampos = String.Format(@"{0}.PagoTarjetaId,{0}.Secuencia,{0}.PersonaId, {0}.Importe, {0}.RegistroFechaHora, {0}.Estado , {0}.MonedaId, {0}.Codigo", prefijo);
            return strCampos;
        }


        public BilleteraPagoTarjeta() : base()
        {
        }

        public BilleteraPagoTarjeta(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }



        public Boolean Actualizar(ref BE.BilleteraPagoTarjeta BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.BilleteraPagoTarjeta
                                    SET PagoTarjetaId = @PagoTarjetaId,
	                                    Secuencia=@Secuencia,
										PersonaId=@PersonaId,
										Importe=@Importe,
										RegistroFechaHora=@RegistroFechaHora,
                                        Estado = @Estado,
                                        MonedaId = @MonedaId,
                                        Codigo = @Codigo
                                    where  PagoTarjetaId = @PagoTarjetaId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO dbo.BilleteraPagoTarjeta ( Secuencia, PersonaId, Importe, RegistroFechaHora,Estado,MonedaId,Codigo) VALUES  (@Secuencia, @PersonaId, @Importe, @RegistroFechaHora,@Estado,@MonedaId,@Codigo)";
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
                 
                    conx.AsignarParametro("@Secuencia", BEObj.Secuencia);
                    conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
                    conx.AsignarParametro("@Importe", BEObj.Importe);
                    conx.AsignarParametro("@RegistroFechaHora", BEObj.RegistroFechaHora);
                    conx.AsignarParametro("@Estado", BEObj.Estado);
                    conx.AsignarParametro("@MonedaId", BEObj.MonedaId);
                    conx.AsignarParametro("@Codigo", BEObj.Codigo);


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


        public List<BE.BilleteraPagoTarjeta> CargarBE(DataRow[] dr)
        {
            List<BE.BilleteraPagoTarjeta> lst = new List<BE.BilleteraPagoTarjeta>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }

        public BE.BilleteraPagoTarjeta CargarBE(DataRow dr)
        {
            BE.BilleteraPagoTarjeta obj = new BE.BilleteraPagoTarjeta();
            obj.PagoTarjetaId = Convert.ToDecimal(dr["PagoTarjetaId"].ToString());
            obj.Secuencia = Convert.ToDecimal(dr["Secuencia"].ToString());
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.Importe = Convert.ToDecimal(dr["Importe"].ToString());
            obj.RegistroFechaHora = Convert.ToDateTime(dr["RegistroFechaHora"].ToString());
            obj.Estado = Convert.ToBoolean(dr["Estado"].ToString());
            obj.MonedaId = Convert.ToDecimal(dr["MonedaId"].ToString());
            obj.Codigo = (dr["Codigo"].ToString());

            return obj;
        }





        public BE.BilleteraPagoTarjeta ObtenerHijos(IEnumerable<string> llaves)
        {
            BE.BilleteraPagoTarjeta obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM BilleteraPagoTarjeta BPT  where BPT.Codigo in {1}", campos("BPT"), this.ConcatenarLlaves(llaves));
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

        public List<BE.BilleteraPagoTarjeta> Lista_ObtenerHijos1(IEnumerable<decimal> llaves)
        {
            List<BE.BilleteraPagoTarjeta> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM BilleteraPagoTarjeta BPT   where BPT.PersonaId in {1}", campos("BPT"), this.ConcatenarLlaves(llaves));
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


        public BE.BilleteraPagoTarjeta BuscarPorSecuencia(decimal secuencia)
        {
            BE.BilleteraPagoTarjeta obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0} from dbo.BilleteraPagoTarjeta BPT with(nolock) where BPT.secuencia = '{1}'", campos("BPT"), secuencia);
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





    }
}
