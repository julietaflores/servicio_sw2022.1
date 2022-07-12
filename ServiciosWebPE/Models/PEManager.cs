using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BC;
using ServiciosWebPE.Models.Entities;
using DAL;

namespace ServiciosWebPE.Models
{
    public class PEManager
    {
        private BC.Deuda bcDeuda = null;
        private BC.Pago bcPago = null;
        private BC.ServAsig bcServAsig = null;
        private BC.BilleteraDetalle bcBilleteraDetalle = null;
        private BC.MaestroDeudaPagoExpress bcMaestroDeudaPagoExpress = null;
        public PEManager(string cadConx)
        {
            bcDeuda = new BC.Deuda(cadConx);
            bcServAsig = new BC.ServAsig(cadConx);
            bcPago = new BC.Pago(cadConx);
            bcBilleteraDetalle = new BC.BilleteraDetalle(cadConx);
            bcMaestroDeudaPagoExpress = new BC.MaestroDeudaPagoExpress(cadConx);
        }
        public Respuesta VerDeuda(string tipo_documento,string documento)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.codigo_respuesta = "00";
                resp.mensaje_respuesta = "CONSULTA EXITOSA";
                List<BE.Deuda> lstDeuda = new List<BE.Deuda>();

                lstDeuda = bcDeuda.ListadoDeuda(tipo_documento, documento);
                foreach ( BE.Deuda item in lstDeuda)
                {
                    string valor = bcDeuda._13ObtenerImporteFormatoPagoExpress(Convert.ToString(item.importe));
                    item.importe = valor;

                }

                resp.datos = lstDeuda;
            }
            catch (Exception ex)
            {
                resp.codigo_respuesta = "22";
                resp.datos = null;
                resp.mensaje_respuesta = ex.Message;
            }
            return resp;

        }

        public Respuesta VerDeudaMaestro(string tipo_documento, string documento)
        {
            Boolean bolOk = false;
            Respuesta resp = new Respuesta();
            try
            {
                resp.codigo_respuesta = "00";
                resp.mensaje_respuesta = "CONSULTA EXITOSA";
                List<BE.Deuda> lstDeuda = new List<BE.Deuda>();

                lstDeuda = bcDeuda.ListadoDeudaMaestro(tipo_documento, documento);
                foreach (BE.Deuda item in lstDeuda)
                {
                    string valor = bcDeuda._13ObtenerImporteFormatoPagoExpress(Convert.ToString(item.importe));
                    item.importe = valor;

                }

                resp.datos = lstDeuda;
            }
            catch (Exception ex)
            {
                resp.codigo_respuesta = "22";
                resp.datos = null;
                resp.mensaje_respuesta = ex.Message;
            }
            return resp;

        }

        public RespuestaPago RealizarPago(BE.Pago obj)
        {
            string estado = "";
            RespuestaPago resp = new RespuestaPago();
            BE.ServAsig item = new BE.ServAsig();
            BE.BilleteraDetalle itemBD = new BE.BilleteraDetalle();
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            decimal BilleteraId = 0;
            IEnumerable<string> llaves;
            try
            {
                if (item != null)
                {

                    item = bcServAsig.BuscarServAsigxId(obj.deuda_id, BE.relServAsig.requiereServicio, BE.relRequiereServicio.persona, BE.relServAsig.servAsigCosto, BE.relServAsig.requiereServicio, BE.relRequiereServicio.servicio);

                    //   bcBilleteraDetalle.
                    estado = bcBilleteraDetalle.RegistrarPago(ref item, obj);

                    if (estado == "TRANSACCION FINALIZADA")
                    {
                        //   item.StatusServAsigId = 4;
                        //  item.ServAsigFHPago =DateTime.Now;
                        // item.TipoEstado = BE.TipoEstado.Modificar;
                        // bcServAsig.Actualizar(ref item);

                        resp.codigo_respuesta = "00";
                        resp.mensaje_respuesta = "PAGO APROBADO";
                        resp.autorizacion = "";

                    }
                    else
                    {
                        resp.codigo_respuesta = "22";
                        resp.mensaje_respuesta = estado;
                        resp.autorizacion = "";

                    }

                }
            }
            catch (Exception ex)
            {


                resp.codigo_respuesta = "22";
                resp.mensaje_respuesta = "Ocurrio una excepcion:" + ex.Message;
                resp.autorizacion = "";
            }

            return resp;

        }


        public RespuestaPago RealizarPagoMaestro(BE.Pago obj)
        {
            string estado = "";
            RespuestaPago resp = new RespuestaPago();
            BE.ServAsig item = new BE.ServAsig();
            BE.MaestroDeudaPagoExpress itemM = new BE.MaestroDeudaPagoExpress();
            BE.BilleteraDetalle itemBD = new BE.BilleteraDetalle();
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            decimal BilleteraId = 0;
            IEnumerable<string> llaves;
            try
            {
                if (item != null)
                {

                    item = bcServAsig.BuscarServAsigxId(obj.deuda_id, BE.relServAsig.requiereServicio, BE.relRequiereServicio.persona, BE.relServAsig.servAsigCosto, BE.relServAsig.requiereServicio, BE.relRequiereServicio.servicio);
                    itemM = bcMaestroDeudaPagoExpress.BuscarMaestroDeudaPagoExpressxId(obj.deuda_id);
                    estado = bcBilleteraDetalle.RegistrarPagoMaestro(ref item, obj, itemM);
                    if (estado == "TRANSACCION FINALIZADA")
                    {
                        //   item.StatusServAsigId = 4;
                        //  item.ServAsigFHPago =DateTime.Now;
                        // item.TipoEstado = BE.TipoEstado.Modificar;
                        // bcServAsig.Actualizar(ref item);

                        resp.codigo_respuesta = "00";
                        resp.mensaje_respuesta = "PAGO APROBADO";
                        resp.autorizacion = "";

                    }
                    else
                    {
                        resp.codigo_respuesta = "22";
                        resp.mensaje_respuesta = estado;
                        resp.autorizacion = "";

                    }

                }
            }
            catch (Exception ex)
            {


                resp.codigo_respuesta = "22";
                resp.mensaje_respuesta = "Ocurrio una excepcion:" + ex.Message;
                resp.autorizacion = "";
            }

            return resp;

        }


        public RespuestaPago RevertirPago(string referencia)
        {
            Boolean bolOk = false;
            RespuestaPago resp = new RespuestaPago();
            BE.ServAsig item = new BE.ServAsig();
            BE.BilleteraDetalle itemBD = new BE.BilleteraDetalle();
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            decimal BilleteraId = 0;
            IEnumerable<string> llaves;
            string ServAsigId = "";
            try
            {

               // ServAsigId = bcBilleteraDetalle.RevertirPago(referencia);
                if (ServAsigId !="")
                {
                    item = bcServAsig.BuscarServAsigxId(ServAsigId, BE.relServAsig.requiereServicio, BE.relRequiereServicio.persona, BE.relServAsig.servAsigCosto, BE.relServAsig.requiereServicio, BE.relRequiereServicio.servicio);
                    item.StatusServAsigId = 3;
                    item.ServAsigFHPago = DateTime.Now;
                    item.TipoEstado = BE.TipoEstado.Modificar;
                    bcServAsig.Actualizar(ref item);
                    resp.codigo_respuesta = "00";
                    resp.mensaje_respuesta = "REVERSA APROBADA";
                    resp.autorizacion = "";
                }

              
                
            }
            catch (Exception ex)
            {


                resp.codigo_respuesta = "22";
                resp.mensaje_respuesta = ex.Message;
                resp.autorizacion = "";
            }

            return resp;

        }

        public RespuestaPago RevertirPagoMaestro(string referencia)
        {
            Boolean bolOk = false;
            RespuestaPago resp = new RespuestaPago();
            BE.ServAsig item = new BE.ServAsig();
            BE.MaestroDeudaPagoExpress itemM = new BE.MaestroDeudaPagoExpress();
            BE.BilleteraDetalle itemBD = new BE.BilleteraDetalle();
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            decimal BilleteraId = 0;
            IEnumerable<string> llaves;
            string ServAsigId = "";
            string deudaId = "";
            try
            {

                bolOk = bcBilleteraDetalle.RevertirPagoMaestroPE(referencia);
                if (bolOk == true)
                {
                   
                    resp.codigo_respuesta = "00";
                    resp.mensaje_respuesta = "REVERSA APROBADA";
                    resp.autorizacion = "";
                }
               



            }
            catch (Exception ex)
            {


                resp.codigo_respuesta = "22";
                resp.mensaje_respuesta = ex.Message;
                resp.autorizacion = "";
            }

            return resp;

        }


    }
}