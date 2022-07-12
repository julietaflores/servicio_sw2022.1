using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using BE;
using System.Globalization;
using System.Data.SqlClient;

namespace BC
{
    public class BilleteraDetalle:BCEntidad
    {
        public BilleteraDetalle() : base()
        {
        }

        public BilleteraDetalle(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        private string campos(string prefijo = "bd")
        {
            string strCampos = String.Format(@"{0}.BilleteraDetalleId,{0}.BilleteraId,{0}.CajeroId,{0}.MedioPagoId,{0}.BilleteraConceptoId,
                                             {0}.ServAsigId,{0}.BilleteraDebeHaber,{0}.BilleteraValor,{0}.BilleteraFechaTransaccion,{0}.BilleteraNroTransaccion,
                                             {0}.BilleteraObservacion,{0}.BilleteraDetalleUID"
                    , prefijo);
            return strCampos;
        }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.BilleteraDetalle BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.billeteraDetalle
                                    SET                                
                                CajeroId=@CajeroId,
                                MedioPagoId=@MedioPagoId,
                                BilleteraConceptoId=@BilleteraConceptoId,
                                ServAsigId=@ServAsigId,
                                BilleteraDebeHaber=@BilleteraDebeHaber,
                                BilleteraValor=@BilleteraValor,
                                BilleteraFechaTransaccion=@BilleteraFechaTransaccion,
                                BilleteraNroTransaccion=@BilleteraNroTransaccion,
                                BilleteraObservacion=@BilleteraObservacion
                                BilleteraDetalleUID=@BilleteraDetalleUID
                                    where  BilleteraDetalleId = @BilleteraDetalleId and BilleteraId=@BilleteraId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into billeteraDetalle (BilleteraDetalleId,BilleteraId,CajeroId,MedioPagoId,BilleteraConceptoId,ServAsigId,BilleteraDebeHaber,BilleteraValor,BilleteraFechaTransaccion,BilleteraNroTransaccion,BilleteraObservacion,BilleteraDetalleUID)values
                                  (@BilleteraDetalleId,@BilleteraId,@CajeroId,@MedioPagoId,@BilleteraConceptoId,@ServAsigId,@BilleteraDebeHaber,@BilleteraValor,@BilleteraFechaTransaccion,@BilleteraNroTransaccion,@BilleteraObservacion,@BilleteraDetalleUID)";
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
                        BEObj. BilleteraDetalleId = System.Convert.ToInt32(conx.ObtenerValor("select isnull(max(BilleteraDetalleId),0) + 1 from dbo.BilleteraDetalle with (nolock);"));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@BilleteraDetalleId", BEObj.BilleteraDetalleId);
                    conx.AsignarParametro("@BilleteraId", BEObj.BilleteraId);
                    conx.AsignarParametro("@CajeroId", BEObj.CajeroId);
                    conx.AsignarParametro("@MedioPagoId", BEObj.MedioPagoId);
                    conx.AsignarParametro("@BilleteraConceptoId", BEObj.BilleteraConceptoId);
                    conx.AsignarParametro("@ServAsigId", BEObj.ServAsigId);
                    conx.AsignarParametro("@BilleteraDebeHaber", BEObj.BilleteraDebeHaber);
                    conx.AsignarParametro("@BilleteraValor", BEObj.BilleteraValor);
                    conx.AsignarParametro("@BilleteraFechaTransaccion", BEObj.BilleteraFechaTransaccion);
                    conx.AsignarParametro("@BilleteraNroTransaccion", BEObj.BilleteraNroTransaccion);
                    conx.AsignarParametro("@BilleteraObservacion", BEObj.BilleteraObservacion);
                    conx.AsignarParametro("@BilleteraDetalleUID", BEObj.BilleteraDetalleUID);

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



        public List<BE.BilleteraDetalle> CargarBE(DataRow[] dr)
        {
            List<BE.BilleteraDetalle> lst = new List<BE.BilleteraDetalle>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.BilleteraDetalle CargarBE(DataRow dr)
        {
            BE.BilleteraDetalle obj = new BE.BilleteraDetalle();
            ///////////////////////////////
            obj.BilleteraDetalleId = Convert.ToInt32(dr["BilleteraDetalleId"].ToString());
            obj.BilleteraId = Convert.ToDecimal(dr["BilleteraId"].ToString());
            if (dr["CajeroId"] == DBNull.Value)
            {
                obj.CajeroId = null;
            }
            else
            {
                obj.CajeroId = Convert.ToInt32(dr["CajeroId"].ToString());
            }
            obj.MedioPagoId = Convert.ToDecimal(dr["MedioPagoId"].ToString());
            obj.BilleteraConceptoId = Convert.ToDecimal(dr["BilleteraConceptoId"].ToString());
            if (dr["ServAsigId"] == DBNull.Value)
            {
                obj.ServAsigId = null;
            }
            else
            {
                obj.ServAsigId = dr["ServAsigId"].ToString();
                
            }
            obj.BilleteraDebeHaber = dr["BilleteraDebeHaber"].ToString();
            obj.BilleteraValor = Convert.ToDecimal(dr["BilleteraValor"].ToString());
            obj.BilleteraFechaTransaccion = Convert.ToDateTime(dr["BilleteraFechaTransaccion"].ToString());
            obj.BilleteraNroTransaccion = dr["BilleteraNroTransaccion"].ToString();
            obj.BilleteraObservacion = dr["BilleteraObservacion"].ToString();
           

            return obj;

        }


        public void CargarRelaciones(ref List<BE.BilleteraDetalle> colObj, string lang="",params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            IEnumerable<int> illaves;
            List<BE.BilleteraConcepto> colBilleteraConcepto = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relBilleteraDetalle.billeteraConcepto))
                {
                    BC.BilleteraConcepto bcBilleteraConcepto = new BC.BilleteraConcepto(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.BilleteraConceptoId).Distinct();
                    colBilleteraConcepto = bcBilleteraConcepto.ObtenerHijos(llaves,lang, relaciones);
                    bcBilleteraConcepto = null;
                }

                if (clase.Equals(BE.relBilleteraDetalle.servicio))
                {
                  /*  BC.Servicio bcServicio = new BC.Servicio(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.ServcioId).Distinct();
                  
                    colRequiereServicioProveedores = bcRequiereServicioProveedores.ObtenerHijos(sllaves, llaves, relaciones);
                    bcRequiereServicioProveedores = null;*/
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colBilleteraConcepto != null && colBilleteraConcepto.Count > 0)
                    {
                        item.billeteraConcepto = (from elemento in colBilleteraConcepto where elemento.BilleteraConceptoId == item.BilleteraConceptoId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }


        #endregion
        #region "Listados"
        public List<BE.BilleteraDetalle> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.BilleteraDetalle> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.BilleteraDetalle bd with(nolock) where bd.BilleteraId in {1}", campos("bd"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,"", relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
       
        public List<BE.BilleteraDetalle> ListadoBilleteraDetalle(long BilleteraId, int index, int max,string lang, params Enum[] relaciones)
        {
            List<BE.BilleteraDetalle> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
            
                string sql = String.Format(@"SELECT bde.BilleteraDetalleId, bde.BilleteraId, bde.CajeroId, bde.MedioPagoId, bde.BilleteraConceptoId, bde.ServAsigId, bde.BilleteraDebeHaber
,case bde.BilleteraDebeHaber when 'D' then (-1) * bde.BilleteraValor else bde.BilleteraValor end BilleteraValor
, bde.BilleteraFechaTransaccion, bde.BilleteraNroTransaccion ,bde.BilleteraObservacion
, mon.MonedaId , mon.MonedaNombre, mon.PaisId 
, bco.BilleteraConceptoNombre 
, ser.ServicioId, ser.ServicioNombre, ser.ServicioURLFoto, ser.CategoriaServicioId, ser.ServicioUsuario, ser.ServicioFechaHoraMod, ser.ServicioKeyWords
FROM dbo.BilleteraDetalle bde with(nolock) inner join dbo.Billetera bil with(nolock) on bde.BilleteraId = bil.BilleteraId 
inner join dbo.Moneda mon with(nolock) on bil.MonedaId = mon.MonedaId 
inner join dbo.BilleteraConcepto bco with(nolock) on bde.BilleteraConceptoId = bco.BilleteraConceptoId 
left join dbo.ServAsig sra with(nolock) on bde.ServAsigId = sra.ServAsigId 
--left join dbo.RequiereServicio rse with(nolock) on sra.RequiereServicioId = rse.RequiereServicioId 
left join dbo.RequiereServicioProveedores rqs with(nolock) on sra.RequiereServicioId = rqs.RequiereServicioId and rqs.StatusRequiereId = 4
left join dbo.ServicioPersona spe with(nolock) on rqs.ServicioPersonaId = spe.ServicioPersonaId 
left join dbo.ServicioSP ('{3}') ser on spe.ServicioId = ser.servicioId 
where bde.BilleteraId = {0}
order by bde.BilleteraFechaTransaccion desc 
OFFSET {1} ROWS
FETCH NEXT {2} ROWS ONLY", BilleteraId, index - 1, max, lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
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

        public DataTable ObtenerBilleterasId(string referencia)
        {
            DataTable dt = new DataTable();
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select BilleteraId from BilleteraDetalle with (nolock) where BilleteraObservacion='{0}' group by BilleteraId", referencia);
                dt = conx.ObtenerTabla(sql);
             

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        #endregion
        public BE.BilleteraDetalle BuscarPorUID(string uid, params Enum[] relaciones)
        {
            BE.BilleteraDetalle obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0} from dbo.BilleteraDetalle bd with(nolock) where bd.BilleteraDetalleUID = '{1}'", campos("bd"), uid);
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
        public Boolean RegistrarBilleteraDetalle(ref BE.BilleteraDetalle billeteraDetalle,BE.Billetera billetera)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                if (billeteraDetalle.MedioPagoId == 9)
                {
                    billeteraDetalle.BilleteraValor = ObtenerTipoCambioValor(billeteraDetalle.BilleteraValor, "Dolar",conx);
                }
                string NroTran = ObtenerBilleteraNroTransaccion(billetera.PersonaBilleteraId,conx);
                billeteraDetalle.BilleteraNroTransaccion = NroTran;
                billeteraDetalle.BilleteraFechaTransaccion = DateTime.Now;
                       bolOk = Actualizar(ref billeteraDetalle, true);
                if (bolOk == true)
                {
                    ActualizarSaldo(billetera.BilleteraId, 0, 0,0, conx);
                }

                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
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


        public decimal ObtenerTipoCambioValor(decimal valor,string moneda, ClaseConexion conx)
        {
            Boolean bolOk = false;
        
            decimal BilleteraValor = 0;

            try
            {
                conx.CrearComando("dbo.[ObtenerTipoCambioBolivianos]", CommandType.StoredProcedure);
                conx.AsignarParametro("@Valor", valor);
                conx.AsignarParametro("@moneda", moneda);
                conx.AsignarParametro("@BilleteraValor", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                string valorCambio = conx.ObtenerParametro("@BilleteraValor").ToString();
                BilleteraValor = Decimal.Parse(valorCambio,NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return BilleteraValor;
        }

       
        public int ObtenerIdBD(decimal PersonaId, ClaseConexion conx)
        {
            int Id = 0;
           
            try
            {
                conx.CrearComando("dbo.[ObtenerIdBD]", CommandType.StoredProcedure);
               conx.AsignarParametro("@PersonaId", PersonaId);
                conx.AsignarParametro("@BilleteraId", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                Id = Convert.ToInt32(conx.ObtenerParametro("@BilleteraId").ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public string ObtenerImporteaCancelar(string ServAsigId, ClaseConexion conx)
        {
            string Id = "";

            try
            {
               
               
              Id=Convert.ToString( conx.ObtenerValor("select * from VerImportePagoServicioProv('" + ServAsigId + "')" ));
              
                //Convert.ToDecima(valor1, );
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        public string ObtenerImporteaCancelarMaestro(string deuda_id, ClaseConexion conx)
        {
            string Id = "";

            try
            {

                Id = Convert.ToString(conx.ObtenerValor("select MaestroDeudaImporte from [dbo].[MaestroDeudaPagoExpress] with (nolock) where MaestroDeudaId ='" + deuda_id+"'"));
             

                //Convert.ToDecima(valor1, );
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public string ObtenerBilleteraDetalle_Deuda_Id(string referencia, ClaseConexion conx)
        {
            string Id = "";

            try
            {

                Id = Convert.ToString(conx.ObtenerValor("select top 1 ServAsigId from billeteraDetalle with (nolock) where BilleteraObservacion='" + referencia + "'"));
                if (Id == "")
                {
                    Id = Convert.ToString(conx.ObtenerValor("select top 1 BilleteraDetalleUID from billeteraDetalle with (nolock) where BilleteraObservacion='" + referencia + "'"));

                }

                //Convert.ToDecima(valor1, );
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public void InsertarMedioPago_Billetera_Proveedor2(string ServAsigId,string requiereServicio,decimal ProveedorId ,decimal ServAsigCostoTotal,string referencia,ClaseConexion conx)
        {
            decimal Id = 0;

            try
            {
                conx.CrearComando("dbo.[InsertarMedioPago_Billetera_Proveedor2]", CommandType.StoredProcedure);
                conx.AsignarParametro("@ServAsigId", ServAsigId);
                conx.AsignarParametro("@RequiereServicioId", requiereServicio);
                conx.AsignarParametro("@ProveedorId", ProveedorId);
                conx.AsignarParametro("@ServAsigCostoTotal", ServAsigCostoTotal);
                conx.AsignarParametro("@BilleteraObservacion", referencia);

                
                conx.EjecutarComando();
                //Convert.ToDecima(valor1, );
            }
            catch (Exception ex)
            {
                throw ex;
            }

          
        }
        public decimal ActualizarStatusServAsig(string ServAsigId,decimal StatusServAsigId, ClaseConexion conx)
        {
            decimal Id = 0;

            try
            {
                conx.CrearComando("dbo.[ActualizarStatusServAsig]", CommandType.StoredProcedure);
                conx.AsignarParametro("@ServAsigId", ServAsigId);
                conx.AsignarParametro("@StatusServAsigId", StatusServAsigId);

                conx.EjecutarComando();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        public decimal ActualizarSaldo(decimal BilleteraId, decimal BilleteraProveedorId,decimal ServiceWebId ,decimal SeguroId,ClaseConexion conx)
        {
            decimal Id = 0;

            try
                {
                conx.CrearComando("dbo.[ActualizarSaldo]", CommandType.StoredProcedure);
                conx.AsignarParametro("@BilleteraId", BilleteraId);
                conx.AsignarParametro("@BilleteraProveedorId", BilleteraProveedorId);
                conx.AsignarParametro("@ServiceWebId", ServiceWebId);
                conx.AsignarParametro("@IdSeguro", SeguroId);
                conx.EjecutarComando();
                

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public int ObtenerIdBDServiceWeb(decimal PersonaId, ClaseConexion conx)
        {
            int Id = 0;

            try
            {
                conx.CrearComando("dbo.[ObtenerIdBDServiceWebV2]", CommandType.StoredProcedure);
                conx.AsignarParametro("@PersonaId", PersonaId);
                conx.AsignarParametro("@BilleteraId", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                Id = Convert.ToInt32(conx.ObtenerParametro("@BilleteraId").ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        public string ObtenerBilleteraNroTransaccion(decimal PersonaId, ClaseConexion conx)
        {
            string Id = "";
        
            try
            {
                conx.CrearComando("dbo.[ObtenerBilleteraNroTransaccion]", CommandType.StoredProcedure);
                conx.AsignarParametro("@PersonaId", PersonaId);
              
                conx.AsignarParametro("@BilleteraNroTransaccion", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                Id = conx.ObtenerParametro("@BilleteraNroTransaccion").ToString()+"-"+DateTime.Now.Second+"-"+DateTime.Now.Hour;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        public string RevertirPago(string referencia, ClaseConexion conx)
        {
            string Id = "";

            try
            {
                conx.CrearComando("dbo.[RevertirPago]", CommandType.StoredProcedure);
                conx.AsignarParametro("@referencia", Convert.ToString (referencia));
                conx.AsignarParametro("@ServAsigId", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                Id = conx.ObtenerParametro("@ServAsigId").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        public string RevertirPagoMaestro(string referencia, ClaseConexion conx)
        {
            string Id = "";

            try
            {
                conx.CrearComando("dbo.[RevertirPagoV2]", CommandType.StoredProcedure);
                conx.AsignarParametro("@referencia", Convert.ToString(referencia));
                conx.AsignarParametro("@deuda_id", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                Id = conx.ObtenerParametro("@deuda_id").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        public int ExistePago(string referencia,string ServAsigId, ClaseConexion conx)
        {
            int cantidad =0;

            try
            {
               
                conx.CrearComando("dbo.ExistePago", CommandType.StoredProcedure);
                conx.AsignarParametro("@referencia", Convert.ToString(referencia));
                conx.AsignarParametro("@ServAsigId", ServAsigId);

                conx.AsignarParametro("@cantidad", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                cantidad =Convert.ToInt32(conx.ObtenerParametro("@cantidad").ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cantidad;
        }


        public int ExistePagoMaestro(string referencia, string deuda_Id, ClaseConexion conx)
        {
            int cantidad = 0;

            try
            {

                conx.CrearComando("ExistePagoV2", CommandType.StoredProcedure);
                conx.AsignarParametro("@referencia", Convert.ToString(referencia));
                conx.AsignarParametro("@deuda_Id", deuda_Id);

                conx.AsignarParametro("@cantidad", "", ParameterDirection.Output, 200);
                conx.EjecutarComando();
                cantidad = Convert.ToInt32(conx.ObtenerParametro("@cantidad").ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cantidad;
        }

        public string RegistrarPago(ref BE.ServAsig item,BE.Pago obj)
        {
            decimal BilleteraId = 0;
            decimal BC = 0; int BP = 0; decimal BSW = 0;
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            Boolean bolOk=false; string BilleteraNroTransaccion = "";
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            BE.BilleteraDetalle billeteraDetalle = new BE.BilleteraDetalle();
            DataTable dt = new DataTable();
            string estado = "";

            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;


                int cantidad = ExistePago(obj.referencia, item.ServAsigId,conx);
                if (cantidad == 0)
                {
                    string BilleteraValor = _13ObtenerImporteFormatoPagoExpress(ObtenerImporteaCancelar(item.ServAsigId, conx));

                    if (BilleteraValor == obj.importe)
                    {
                        BP = ObtenerIdBD(item.ProveedorId, conx);
                        billeteraDetalle.TipoEstado = BE.TipoEstado.Insertar;
                        billeteraDetalle.BilleteraId = BP;
                        billeteraDetalle.CajeroId = null;
                        billeteraDetalle.MedioPagoId = 5;
                        billeteraDetalle.BilleteraConceptoId = 10;
                        billeteraDetalle.ServAsigId = item.ServAsigId;
                        billeteraDetalle.BilleteraDebeHaber = "H";
                        billeteraDetalle.BilleteraFechaTransaccion = DateTime.Now;
                        billeteraDetalle.BilleteraValor = Convert.ToDecimal(ObtenerImporteaCancelar(item.ServAsigId, conx));
                        billeteraDetalle.BilleteraNroTransaccion = ObtenerBilleteraNroTransaccion(item.ProveedorId, conx);
                        billeteraDetalle.BilleteraObservacion = obj.referencia;
                        billeteraDetalle.BilleteraDetalleUID = "";
                        Actualizar(ref billeteraDetalle, true);



                        InsertarMedioPago_Billetera_Proveedor2(item.ServAsigId, item.requiereServicio.RequiereServicioId, item.ProveedorId, 0, obj.referencia, conx);
                        BC.ServAsig bcServAsig = new BC.ServAsig(cadenaConexion);

                        bcServAsig.dbConexion = conx;
                        item.StatusServAsigId = 4;
                        item.ServAsigFHPago = DateTime.Now;
                        item.TipoEstado = BE.TipoEstado.Modificar;
                        bcServAsig.Actualizar(ref item);




                        conx.ConfirmarTransaccion();
                        conx.Desconectar();
                        estado = "TRANSACCION FINALIZADA";
                    }
                    else
                    {
                        estado = "IMPORTE DIFERENTE";

                    }

                }
                else
                {
                    estado = "PAGO EXISTENTE";
                }


               
            }
            catch (Exception Ex)                      
            {

                throw;
            }
          






            return estado;
        }

        public string RegistrarPagoMaestro(ref BE.ServAsig item, BE.Pago obj, BE.MaestroDeudaPagoExpress itemM = null)
        {
            decimal BilleteraId = 0;
            decimal BC = 0; int BP = 0; decimal BSW = 0;
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            Boolean bolOk = false; string BilleteraNroTransaccion = "";
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            BE.BilleteraDetalle billeteraDetalle = new BE.BilleteraDetalle();
            BE.MaestroDeudaPagoExpress bcMaestroDeudaPagoExpress = new BE.MaestroDeudaPagoExpress();
            DataTable dt = new DataTable();
            string estado = "";

            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                int cantidad = ExistePago(obj.referencia, obj.deuda_id, conx);
                if (cantidad == 0)
                {
                    cantidad = ExistePagoMaestro(obj.referencia, obj.deuda_id, conx);
                }
                if ((cantidad == 0))
                {
                    string BilleteraValor = "";
                    if (item != null)
                    {
                        BilleteraValor = _13ObtenerImporteFormatoPagoExpress(ObtenerImporteaCancelar(obj.deuda_id, conx));
                        if (BilleteraValor == obj.importe)
                        {
                            BP = ObtenerIdBD(item.ProveedorId, conx);
                            billeteraDetalle.TipoEstado = BE.TipoEstado.Insertar;
                            billeteraDetalle.BilleteraId = BP;
                            billeteraDetalle.CajeroId = null;
                            billeteraDetalle.MedioPagoId = 5;
                            billeteraDetalle.BilleteraConceptoId = 10;
                            billeteraDetalle.ServAsigId = item.ServAsigId;
                            billeteraDetalle.BilleteraDebeHaber = "H";
                            billeteraDetalle.BilleteraFechaTransaccion = DateTime.Now;
                            billeteraDetalle.BilleteraValor = Convert.ToDecimal(ObtenerImporteaCancelar(item.ServAsigId, conx));
                            billeteraDetalle.BilleteraNroTransaccion = ObtenerBilleteraNroTransaccion(item.ProveedorId, conx);
                            billeteraDetalle.BilleteraObservacion = obj.referencia;
                            billeteraDetalle.BilleteraDetalleUID = "";
                            Actualizar(ref billeteraDetalle, true);
                            InsertarMedioPago_Billetera_Proveedor2(item.ServAsigId, item.requiereServicio.RequiereServicioId, item.ProveedorId, 0, obj.referencia, conx);
                            BC.ServAsig bcServAsig = new BC.ServAsig(cadenaConexion);
                            bcServAsig.dbConexion = conx;
                            item.StatusServAsigId = 4;
                            item.ServAsigFHPago = DateTime.Now;
                            item.TipoEstado = BE.TipoEstado.Modificar;
                            bcServAsig.Actualizar(ref item);
                            conx.ConfirmarTransaccion();
                            conx.Desconectar();
                            estado = "TRANSACCION FINALIZADA";
                        }
                        else
                        {
                            estado = "IMPORTE DIFERENTE";

                        }
                    }
                    else
                    {
                        BilleteraValor = _13ObtenerImporteFormatoPagoExpressMaestro(ObtenerImporteaCancelarMaestro(obj.deuda_id, conx));
                        if (BilleteraValor == obj.importe)
                        {
                            decimal BilleteraValorImporte = Convert.ToDecimal(ObtenerImporteaCancelarMaestro(obj.deuda_id, conx));
                            string NroTransaccion = ObtenerBilleteraNroTransaccion(itemM.PersonaId, conx);
                            BP = ObtenerIdBD(itemM.PersonaId, conx);
                            billeteraDetalle.TipoEstado = BE.TipoEstado.Insertar;
                            billeteraDetalle.BilleteraId = BP;
                            billeteraDetalle.CajeroId = null;
                            billeteraDetalle.MedioPagoId = 5;
                            billeteraDetalle.BilleteraConceptoId = 10;
                            billeteraDetalle.ServAsigId = null;
                            billeteraDetalle.BilleteraDebeHaber = "H";
                            billeteraDetalle.BilleteraFechaTransaccion = DateTime.Now;
                            billeteraDetalle.BilleteraValor = BilleteraValorImporte;
                            billeteraDetalle.BilleteraNroTransaccion = NroTransaccion;
                            billeteraDetalle.BilleteraObservacion = obj.referencia;
                            billeteraDetalle.BilleteraDetalleUID = itemM.MaestroDeudaId;
                            Actualizar(ref billeteraDetalle, true);
                            //////////////////////////////RETIRANDO DEL PROVEEDOR


                            billeteraDetalle.TipoEstado = BE.TipoEstado.Insertar;
                            billeteraDetalle.BilleteraId = BP;
                            billeteraDetalle.CajeroId = null;
                            billeteraDetalle.MedioPagoId = 5;
                            billeteraDetalle.BilleteraConceptoId = 11;
                            billeteraDetalle.ServAsigId = null;
                            billeteraDetalle.BilleteraDebeHaber = "D";
                            billeteraDetalle.BilleteraFechaTransaccion = DateTime.Now;
                            billeteraDetalle.BilleteraValor = BilleteraValorImporte;
                            billeteraDetalle.BilleteraNroTransaccion = NroTransaccion;
                            billeteraDetalle.BilleteraObservacion = obj.referencia;
                            billeteraDetalle.BilleteraDetalleUID = itemM.MaestroDeudaId;
                            Actualizar(ref billeteraDetalle, true);
                            //////////////////////////////////////////////////////////
                            BSW = ObtenerIdBDServiceWeb(itemM.PersonaId, conx);
                            billeteraDetalle.TipoEstado = BE.TipoEstado.Insertar;
                            billeteraDetalle.BilleteraId = BSW;
                            billeteraDetalle.CajeroId = null;
                            billeteraDetalle.MedioPagoId = 5;
                            billeteraDetalle.BilleteraConceptoId = 12;
                            billeteraDetalle.ServAsigId = null;
                            billeteraDetalle.BilleteraDebeHaber = "H";
                            billeteraDetalle.BilleteraFechaTransaccion = DateTime.Now;
                            billeteraDetalle.BilleteraValor = BilleteraValorImporte;
                            billeteraDetalle.BilleteraNroTransaccion = NroTransaccion;
                            billeteraDetalle.BilleteraObservacion = obj.referencia;
                            billeteraDetalle.BilleteraDetalleUID = itemM.MaestroDeudaId;
                            Actualizar(ref billeteraDetalle, true);
                            ActualizarSaldo(BP, BP, BSW, 0, conx);
                            ///////////////////////////////////////////////////
                            BC.MaestroDeudaPagoExpress bcMaestroDeudaPE = new BC.MaestroDeudaPagoExpress(cadenaConexion);
                            bcMaestroDeudaPE.dbConexion = conx;
                            itemM.MaestroDeudaPago = true;
                            itemM.MaestroDeudaFechaPago = DateTime.Now;
                            itemM.TipoEstado = BE.TipoEstado.Modificar;
                            bcMaestroDeudaPE.Actualizar(ref itemM, true);
                            ////////////////////////////////////////////////////////
                            conx.ConfirmarTransaccion();
                            conx.Desconectar();
                            estado = "TRANSACCION FINALIZADA";
                        }
                        else
                        {
                            estado = "IMPORTE DIFERENTE";
                        }

                    }



                }
                else
                {
                    estado = "PAGO EXISTENTE";
                }



            }
            catch (Exception Ex)
            {

                throw;
            }







            return estado;
        }

        public string _13ObtenerImporteFormatoPagoExpress(string ImporteFormatoPagoExpress)
        {

            int resp = 0;
            string Importe = "";
            try
            {


             // if ((ImporteFormatoPagoExpress.Contains('.')))
               if (ImporteFormatoPagoExpress.Contains(','))

                    {
                        string[] valor;
                         
                 valor = Convert.ToString(ImporteFormatoPagoExpress).Split(',');
               // valor = Convert.ToString(ImporteFormatoPagoExpress).Split('.');
                    if (valor[1].Length == 1)
                        valor[1] = valor[1].Insert(1, "0");
                    else
                        valor[1] = valor[1].Substring(0, 2);

                    Importe = valor[0] + valor[1];
                }
                else
                {
                    Importe = ImporteFormatoPagoExpress + "00";

                }
                ///////////////////////Insertando detalle billetera


            }

            catch (Exception ex)
            {

                throw;
            }



            return Importe;

        }

        public string _13ObtenerImporteFormatoPagoExpressMaestro(string ImporteFormatoPagoExpress)
        {

            int resp = 0;
            string Importe = "";
            try
            {


                if ((ImporteFormatoPagoExpress.Contains('.')))
                {
                    string[] valor;
                 //  valor = Convert.ToString(ImporteFormatoPagoExpress).Split(',');
                      valor = Convert.ToString(ImporteFormatoPagoExpress).Split('.');
                    if (valor[1].Length == 1)
                        valor[1] = valor[1].Insert(1, "0");
                    else
                        valor[1] = valor[1].Substring(0, 2);

                    Importe = valor[0] + valor[1];
                }
                else
                {
                    Importe = ImporteFormatoPagoExpress + "00";

                }
                ///////////////////////Insertando detalle billetera


            }

            catch (Exception ex)
            {

                throw;
            }



            return Importe;

        }
        public Boolean RegistrarPagoCliente(ref BE.ServAsig item, SqlTransaction DataTransactionCom)
        {
            decimal BilleteraId = 0;
            decimal BC = 0; decimal BP = 0; decimal BSW = 0;
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            Boolean bolOk = false; string BilleteraNroTransaccion = "";
            ClaseConexion conx = new ClaseConexion(cadenaConexion);

            try
            {
             //   conx.Conectar();
           //     conx.ComenzarTransaccion();
                dbConexion = conx;
               // decimal BilleteraValor = ObtenerImporteaCancelar(item.ServAsigId, conx);
            //    BilleteraValor = Decimal.Round(BilleteraValor, 2);

                for (int i = 1; i <= 1; i++)
                {
                    BE.BilleteraDetalle itemBilleDet = new BE.BilleteraDetalle();

                    BC.ServAsig bcReqProv = new BC.ServAsig(cadenaConexion);
                    bcReqProv.dbConexion = conx;
                    //   bcReqProv = item;
                    //itemBilleDet.dbConexion = conx;
                    if (i == 1)
                    {
                        decimal personaId = item.ProveedorId;
                        BP = ObtenerIdBD(personaId, conx);
                        itemBilleDet.BilleteraId = BP;
                        itemBilleDet.MedioPagoId = 5;
                        itemBilleDet.BilleteraConceptoId = 2;
                        itemBilleDet.BilleteraDebeHaber = "H";

                    }
                    if (i == 2)
                    {
                        BC = ObtenerIdBD(item.requiereServicio.PersonaId, conx);
                        itemBilleDet.BilleteraId = BP;
                        itemBilleDet.MedioPagoId = 3;
                        itemBilleDet.BilleteraConceptoId = 1;
                        itemBilleDet.BilleteraDebeHaber = "D";
                    }
                    if (i == 3)
                    {
                        BSW = ObtenerIdBDServiceWeb(item.requiereServicio.persona.CiudadId, conx);
                        itemBilleDet.BilleteraId = BSW;
                        itemBilleDet.MedioPagoId = 3;
                        itemBilleDet.BilleteraConceptoId = 1;
                        itemBilleDet.BilleteraDebeHaber = "H";
                    }
                    itemBilleDet.CajeroId = null;

                    itemBilleDet.ServAsigId = item.ServAsigId;
                  //  itemBilleDet.BilleteraValor = BilleteraValor;
                    itemBilleDet.BilleteraFechaTransaccion = DateTime.Now;
                    // BilleteraNroTransaccion = (ObtenerBilleteraNroTransaccion(item.requiereServicio.persona.CiudadId, item.ProveedorId, conx) + "-" + BP);
                    itemBilleDet.BilleteraNroTransaccion = BilleteraNroTransaccion;
                  //  itemBilleDet.BilleteraObservacion = obj.referencia;
                    itemBilleDet.TipoEstado = BE.TipoEstado.Insertar;
                    //   listaBilleteraDet.Add(itemBilleDet);
                    BE.BilleteraDetalle BillDet = itemBilleDet;

                    bolOk = Actualizar(ref BillDet, true);

                }
              //  ActualizarStatusServAsig(item.ServAsigId, 4, conx);
              //  ActualizarSaldo(BC, BP, BSW, conx);

                conx.ConfirmarTransaccion();
                conx.Desconectar();
                bolOk = true;

            }
            catch (Exception Ex)
            {

                throw;
            }







            return true;
        }


        public Boolean RevertirPagoMaestroPE(string referencia)
        {
            decimal BilleteraId = 0;
            decimal BC = 0; decimal BP = 0; decimal BSW = 0;decimal BSseguro = 0;
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            DataTable dt = new DataTable();
            string deudaId = "";
            BC.ServAsig bcServAsig = new BC.ServAsig(cadenaConexion);
            BC.BilleteraDetalleAnulacion bcBilleteraDetalleAnulacion = new BC.BilleteraDetalleAnulacion(cadenaConexion);
            BE.ServAsig item = new BE.ServAsig();
            BE.BilleteraDetalleAnulacion BilleteraDetalleAnulacion = new BE.BilleteraDetalleAnulacion();
            BC.MaestroDeudaPagoExpress bcMaestroDeudaPagoExpress = new BC.MaestroDeudaPagoExpress(cadenaConexion);
            BE.MaestroDeudaPagoExpress itemM = new BE.MaestroDeudaPagoExpress();
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
             
                dt = ObtenerBilleterasId(Convert.ToString(referencia));
                if (dt.Rows.Count == 2)
                {
                    BC = Convert.ToDecimal(dt.Rows[0][0]);
                    BP = Convert.ToDecimal(dt.Rows[1][0]);
                }
                else
                {
                    BC = Convert.ToDecimal(dt.Rows[0][0]);
                    BP = Convert.ToDecimal(dt.Rows[1][0]);
                    BSW = Convert.ToDecimal(dt.Rows[2][0]);
                }

              
              
                deudaId = ObtenerBilleteraDetalle_Deuda_Id(referencia, conx);
                BilleteraDetalleAnulacion.BilleteraDetalleAnulaciondeuda_id = deudaId;
                BilleteraDetalleAnulacion.BilleteraDetalleAnulacionReferencia = referencia;
                BilleteraDetalleAnulacion.BilleteraDetalleAnulacionFecha = DateTime.Now;
                BilleteraDetalleAnulacion.TipoEstado = TipoEstado.Insertar;
                bcBilleteraDetalleAnulacion.Actualizar(ref BilleteraDetalleAnulacion, false);

                item = bcServAsig.BuscarServAsigxId(deudaId, BE.relServAsig.requiereServicio, BE.relRequiereServicio.persona, BE.relServAsig.servAsigCosto, BE.relServAsig.requiereServicio, BE.relRequiereServicio.servicio);
                if (item!=null)
                {
                    item = bcServAsig.BuscarServAsigxId(deudaId, BE.relServAsig.requiereServicio, BE.relRequiereServicio.persona, BE.relServAsig.servAsigCosto, BE.relServAsig.requiereServicio, BE.relRequiereServicio.servicio);
                    item.StatusServAsigId = 3;
                    item.ServAsigFHPago = DateTime.Now;
                    item.TipoEstado = BE.TipoEstado.Modificar;
                    bcServAsig.Actualizar(ref item);
                }
                else
                {
                    itemM = bcMaestroDeudaPagoExpress.BuscarMaestroDeudaPagoExpressxId(deudaId);
                    itemM.MaestroDeudaPago = false;
                    itemM.MaestroDeudaFechaPago = null;
                    itemM.TipoEstado = BE.TipoEstado.Modificar;
                    bcMaestroDeudaPagoExpress.Actualizar(ref itemM);
                }
                //   ActualizarStatusServAsig(ServiAsigId,5,conx);
                ActualizarSaldo(BC,BP,BSW,BSseguro,conx);

                conx.ConfirmarTransaccion();
                conx.Desconectar();
                bolOk = true;

            }
            catch (Exception Ex)
            {

                throw;
            }







            return bolOk;
        }

        public string RevertirPagoMaestro(string referencia)
        {
            decimal BilleteraId = 0;
            decimal BC = 0; decimal BP = 0; decimal BSW = 0; decimal BSseguro = 0;
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            DataTable dt = new DataTable();
            string ServiAsigId = "";
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                dt = ObtenerBilleterasId(Convert.ToString(referencia));
                BC = Convert.ToDecimal(dt.Rows[0][0]);
                BP = Convert.ToDecimal(dt.Rows[1][0]);
                BSW = Convert.ToDecimal(dt.Rows[2][0]);
                BSseguro = Convert.ToDecimal(dt.Rows[3][0]);
                ServiAsigId = RevertirPago(referencia, conx);
                //   ActualizarStatusServAsig(ServiAsigId,5,conx);
                ActualizarSaldo(BC, BP, BSW, BSseguro, conx);

                conx.ConfirmarTransaccion();
                conx.Desconectar();
                bolOk = true;

            }
            catch (Exception Ex)
            {

                throw;
            }







            return ServiAsigId;
        }
    }
}



