using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class Siniestro_Estado:BCEntidad
    {
        public Siniestro_Estado() : base()
        {
        }

        public Siniestro_Estado(string cadConx) : base(cadConx)
        {

        }

        private string campos(string prefijo = "se")
        {
            string strCampos = String.Format(@"{0}.SiniestroId  , {0}.SiniestroEstadoId, {0}.Siniestro_EstadoFechaHoraMod, {0}.Siniestro_EstadoObservacion"
                    , prefijo);
            return strCampos;
        }

        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.Siniestro_Estado BEObj, Boolean isTransaccion = false)
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
                        strSql = @"update Siniestro_Estado
set 

SiniestroEstadoId=@SiniestroEstadoId,
Siniestro_EstadoFechaHoraMod=@Siniestro_EstadoFechaHoraMod,
Siniestro_EstadoObservacion=@Siniestro_EstadoObservacion


	                                 
                                    where  SiniestroId = @SiniestroId   ;";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into Siniestro_Estado(SiniestroId,SiniestroEstadoId,Siniestro_EstadoFechaHoraMod,Siniestro_EstadoObservacion)

values(@SiniestroId,@SiniestroEstadoId,@Siniestro_EstadoFechaHoraMod,@Siniestro_EstadoObservacion)";
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
                    conx.AsignarParametro("@SiniestroId", BEObj.SiniestroId);
                    conx.AsignarParametro("@SiniestroEstadoId", BEObj.SiniestroEstadoId);
                    conx.AsignarParametro("@Siniestro_EstadoFechaHoraMod", BEObj.Siniestro_EstadoFechaHoraMod);
                    conx.AsignarParametro("@Siniestro_EstadoObservacion", BEObj.Siniestro_EstadoObservacion);
                 

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



        public List<BE.Siniestro_Estado> CargarBE(DataRow[] dr)
        {
            List<BE.Siniestro_Estado> lst = new List<BE.Siniestro_Estado>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Siniestro_Estado CargarBE(DataRow dr)
        {
            BE.Siniestro_Estado obj = new BE.Siniestro_Estado();
            obj.SiniestroId = Convert.ToDecimal(dr["SiniestroId"].ToString());
            obj.SiniestroEstadoId =Convert.ToDecimal (dr["SiniestroEstadoId"].ToString());

            if (dr["Siniestro_EstadoFechaHoraMod"].ToString()!="")
            {
                obj.Siniestro_EstadoFechaHoraMod = Convert.ToDateTime(dr["Siniestro_EstadoFechaHoraMod"].ToString());
            }
         
            obj.Siniestro_EstadoObservacion = (dr["Siniestro_EstadoObservacion"].ToString());
        
            return obj;
        }




        #endregion


        public void CargarRelaciones(ref List<BE.Siniestro> colObj, params Enum[] relaciones)
        {
            /* if (relaciones == null || colObj == null)
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
             }*/
        }

        #region "Listados"
        public List<BE.Siniestro_Estado> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.Siniestro_Estado> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Siniestro_Estado se with(nolock) where se.SiniestroId in {1} order by Siniestro_EstadoFechaHoraMod asc", campos("se"), this.ConcatenarLlaves(llaves));
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
      
        public Boolean RegistrarSolicitud(ref BE.Siniestro_Estado siniestro_Estado)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                bolOk = Actualizar(ref siniestro_Estado, true);

                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }
                /*if (requiereServicio.RequiereServicioProveedores != null)
                {
                    BC.RequiereServicioProveedores bcReqProv = new BC.RequiereServicioProveedores(cadenaConexion);
                    bcReqProv.dbConexion = conx;
                    foreach (BE.RequiereServicioProveedores item in requiereServicio.RequiereServicioProveedores)
                    {
                        item.RequiereServicioId = requiereServicio.RequiereServicioId;
                        BE.RequiereServicioProveedores prov = item;
                        bolOk = bcReqProv.Actualizar(ref prov, true);

                        if (!bolOk)
                        {
                            throw new Exception("Error al enviar Solicitudes");
                        }
                        item.RequiereServicioProveedoresId = prov.RequiereServicioProveedoresId;
                    }

                    bcReqProv = null;
                }*/
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
        #endregion
        public string VerServAsigId(decimal SiniestroId)
        {
            int obj = 0;
            string valor = "";
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select s.ServAsigId from Siniestro_Estado se
inner join Siniestro s
on
se.SiniestroId=s.SiniestroId
and se.SiniestroId={0}


                        ", SiniestroId);
                valor = Convert.ToString(conx.ObtenerValor(sql));


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return valor;
        }
    }
}
