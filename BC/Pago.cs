using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BC
{
   public  class Pago:BCEntidad
    {
        public Pago() : base()
        {
        }

        public Pago(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        public Boolean RealizarPago(BE.Pago obj)
        {
            Boolean bolOk=false;
            BE.ServAsig objServAsig = null;
            //ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
             

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolOk;
        }
        public Boolean RegistrarPago(ref BE.ServAsig item, BE.Pago obj)
        {
            decimal BilleteraId = 0;
            List<BE.BilleteraDetalle> listaBilleteraDet = new List<BE.BilleteraDetalle>();
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);

            try
            {
                dbConexion = conx;
                for (int i = 1; i <= 2; i++)
                {
                    BE.BilleteraDetalle itemBilleDet = new BE.BilleteraDetalle();

                    BC.ServAsig bcReqProv = new BC.ServAsig(cadenaConexion);
                    bcReqProv.dbConexion = conx;
                    //bcReqProv = item;
                    //itemBilleDet.dbConexion = conx;
                    if (i == 1)
                    {
                        decimal personaId = item.requiereServicio.servAsig.ProveedorId;
                    //    BilleteraId = ObtenerIdBD(personaId);
                        itemBilleDet.MedioPagoId = 5;
                        itemBilleDet.BilleteraConceptoId = 2;
                        itemBilleDet.BilleteraDebeHaber = "H";

                    }
                    if (i == 2)// BilleteraId = ObtenerIdBD(item.requiereServicio.PersonaId);
                    {
                        itemBilleDet.MedioPagoId = 3;
                        itemBilleDet.BilleteraConceptoId = 1;
                        itemBilleDet.BilleteraDebeHaber = "D";
                    }
                    itemBilleDet.CajeroId = null;
                    itemBilleDet.ServAsigId = item.requiereServicio.servAsig.ServAsigId;
                    itemBilleDet.BilleteraValor = Convert.ToDecimal(obj.importe);
                    itemBilleDet.BilleteraFechaTransaccion = DateTime.Now;
                 //   itemBilleDet.BilleteraNroTransaccion = (ObtenerBilleteraNroTransaccion(item.requiereServicio.persona.CiudadId, item.requiereServicio.servAsig.ProveedorId) + "-" + BilleteraId);
                    itemBilleDet.BilleteraObservacion = obj.referencia;
                    listaBilleteraDet.Add(itemBilleDet);
                    foreach (BE.BilleteraDetalle itembd in listaBilleteraDet)
                    {
                        //   item.RequiereServicioId = listaBilleteraDet.B;
                        BE.BilleteraDetalle BillDet = itembd;
                   ///     bolOk = Actualizar(ref BillDet, true);
                        if (!bolOk)
                        {
                            throw new Exception("Error al Registrar El pago");
                        }
                        // item.RequiereServicioProveedoresId = prov.RequiereServicioProveedoresId;
                    }

                }
            }
            catch (Exception Ex)
            {

                throw;
            }







            return true;
        }
    }
}
