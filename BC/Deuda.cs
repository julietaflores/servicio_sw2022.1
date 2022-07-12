using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BC
{
    public class Deuda:BCEntidad
    {
        private string campos(string prefijo = "spe")
        {
            string strCampos = String.Format(@"{0}.ServicioPersonaId, {0}.PersonaId, {0}.ServicioId, {0}.ServicioPersonaURLFoto, {0}.EstadoServicioId
                    , {0}.StatusServicioId, {0}.ServicioPersonaUsuario, {0}.ServicioPersonaFechaHora, {0}.ServicioPersonaGaleriaLast
                    , {0}.ServicioPersonaHorarioLast, {0}.ServicioPersonaNombre, {0}.ServicioPersonaDescripcion, {0}.ServicioPersonaReqDelivery
                    , {0}.MonedaId, {0}.PersonaDireccionId, {0}.ServicioPersonaEnDomicilio, {0}.ServicioPersonaEnOficina"
                    , prefijo);
            return strCampos;
        }
        public Deuda() : base()
        {
        }

        public Deuda(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.ServAsig BEObj)
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

                        strSql = "";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = "insert ServAsig values(@ServAsigId, @ProveedorId, @ServAsigFHUbicacion, @ServAsigFHEstimadaLlegada, @ServAsigFHInicio, @ServAsigFHFin, @ServAsigFHPago,@ServAsigCostoTotal, @StatusServAsigId,@RequiereServicioId,@ServAsigPagaCliente)";
                        break;

                }

                if (dbConexion != null)
                    conx = dbConexion;
                else
                    conx.Conectar();
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));
                    conx.CrearComando(strSql);
                conx.AsignarParametro("@ServAsigId", BEObj.ServAsigId);
                conx.AsignarParametro("@ProveedorId", BEObj.ProveedorId);
                conx.AsignarParametro("@ServAsigFHUbicacion", BEObj.ServAsigFHUbicacion);
                conx.AsignarParametro("@ServAsigFHEstimadaLlegada", BEObj.ServAsigFHEstimadaLlegada);
                conx.AsignarParametro("@ServAsigFHInicio", BEObj.ServAsigFHInicio);
                conx.AsignarParametro("@ServAsigFHFin", BEObj.ServAsigFHFin);
                conx.AsignarParametro("@ServAsigFHPago", BEObj.ServAsigFHPago);
                conx.AsignarParametro("@ServAsigCostoTotal", BEObj.ServAsigCostoTotal);
                conx.AsignarParametro("@StatusServAsigId", BEObj.StatusServAsigId);
                conx.AsignarParametro("@RequiereServicioId", BEObj.RequiereServicioId);
                conx.AsignarParametro("@ServAsigPagaCliente", BEObj.ServAsigPagaCliente);


                conx.EjecutarComando();
                if (dbConexion == null)
                    conx.Desconectar();
                bolOk = true;
            }
            catch (Exception ex)
            {
                if (dbConexion == null)
                {
                    conx.Desconectar();
                }
                throw ex;
            }
            return bolOk;
        }


        public List<BE.Deuda> CargarBE(DataRow[] dr)
        {
            List<BE.Deuda> lst = new List<BE.Deuda>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Deuda CargarBE(DataRow dr)
        {
            BE.Deuda obj = new BE.Deuda();
            obj.cliente = Convert.ToString(dr["cliente"].ToString());
            obj.descripcion = Convert.ToString(dr["descripcion"].ToString());
            obj.deuda_id = Convert.ToString(dr["deuda_id"].ToString());
            obj.cuota = Convert.ToInt32(dr["cuota"].ToString());
            obj.moneda = Convert.ToString(dr["moneda"].ToString());
            obj.importe = Convert.ToString(dr["importe"].ToString());
                  
            obj.vencimiento =dr["vencimiento"].ToString();


            return obj;
        }

        public void CargarRelaciones(ref List<BE.ServicioPersona> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.RequiereServicio> colRequiereServicio = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relServAsig.requiereServicio))
                {
                    //BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                    //sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    //colRequiereServicio = bcRequiereServicio.ObtenerHijos(sllaves, relaciones);
                    //bcRequiereServicio = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    //if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    //{
                    //    item.requiereServicio = (from elemento in colRequiereServicio where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList().FirstOrDefault();
                    //}
                }
            }
        }
        #endregion

        #region "Listados"
        public List<BE.Deuda> ListadoDeuda(string tipo_documento, string documento, params Enum[] relaciones)
        {
            List<BE.Deuda> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"

select 
 p.PersonaNombres+' '+p.PersonaApellidos as cliente,
 '' as descripcion ,
sa.ServAsigId as deuda_id,
row_number() over( order by RequiereServicioFechaHoraReq desc)cuota,
m.monedanombre as moneda ,
dbo.[VerImportePagoServicio1](sa.ServAsigId)as importe,
convert(varchar, sa.ServAsigFHPago, 103) as vencimiento
from RequiereServicio rs inner join 
ServAsig sa on
rs.RequiereServicioId=sa.RequiereServicioId
inner join Persona p 
on sa.ProveedorId=p.PersonaId
inner join TipoDocumento td on p.TipoDocumentoId=td.TipoDocumentoId
inner join Ciudad c on p.CiudadId=c.CiudadId 
inner join Region r on r.RegionId=c.RegionId
inner join Pais pa on  r.PaisId=pa.PaisId 
inner join Moneda m
on m.PaisId=pa.paisId 
and sa.ServAsigPagaCliente=0 
and sa.ServAsigFHPago is not null
and sa.StatusServAsigId=3
and td.TipoDocumentoAbreviatura='{0}'
and p.PersonaDNI='{1}'

", tipo_documento, documento);
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

        public List<BE.Deuda> ListadoDeudaMaestro(string tipo_documento, string documento, params Enum[] relaciones)
        {
            List<BE.Deuda> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"
select C1.cliente,C1.descripcion,C1.deuda_id,
row_number() over( order by vencimiento desc)cuota,
C1.moneda,C1.importe,

convert(varchar, C1.vencimiento, 103) as vencimiento 
from 
(
select 
 p.PersonaNombres+' '+p.PersonaApellidos as cliente,
 '' as descripcion ,
sa.ServAsigId as deuda_id,
row_number() over( order by RequiereServicioFHDeseada desc)cuota,
m.monedanombre as moneda ,
dbo.[VerImportePagoServicio1](sa.ServAsigId)as importe,
 rs.RequiereServicioFHDeseada as vencimiento
from RequiereServicio rs inner join 
ServAsig sa on
rs.RequiereServicioId=sa.RequiereServicioId
inner join Persona p 
on sa.ProveedorId=p.PersonaId
inner join TipoDocumento td on p.TipoDocumentoId=td.TipoDocumentoId
inner join Ciudad c on p.CiudadId=c.CiudadId 
inner join Region r on r.RegionId=c.RegionId
inner join Pais pa on  r.PaisId=pa.PaisId 
inner join Moneda m
on m.PaisId=pa.paisId 
inner join RequiereServicioProveedores rsp
on
rs.requiereServicioId=rsp.RequiereServicioId
and rsp.StatusRequiereId=4
and rs.EstadoReqServId=2
and sa.ServAsigPagaCliente=0 
and sa.ServAsigFHPago is NOT  null
and sa.StatusServAsigId=3
and td.TipoDocumentoAbreviatura='{0}'
and p.PersonaDNI='{1}'

union all
select p.PersonaNombres+' '+p.PersonaApellidos as cliente,
'' as descripcion,
mde.MaestroDeudaId deuda_id,
row_number() over( order by mde.MaestroDeudaVencimiento desc)cuota,
m.monedanombre as moneda,
mde.MaestroDeudaImporte as importe,
MaestroDeudaVencimiento as vencimiento
from [dbo].[MaestroDeudaPagoExpress] mde
inner join Persona p on mde.PersonaId=p.PersonaId
inner join TipoDocumento td on p.TipoDocumentoId=td.TipoDocumentoId
inner join Ciudad c on p.CiudadId=c.CiudadId 
inner join Region r on r.RegionId=c.RegionId
inner join Pais pa on  r.PaisId=pa.PaisId 
inner join Moneda m on m.PaisId=pa.paisId 
and td.TipoDocumentoAbreviatura='{0}'
and p.PersonaDNI='{1}'
and mde.MaestroDeudaPago=0
)c1




", tipo_documento, documento);
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
        #endregion
        #region "Busqueda"
        public string _13ObtenerImporteFormatoPagoExpress(string ImporteFormatoPagoExpress)
        {

            int resp = 0;
            string Importe = "";
            try
            {
                

                if (ImporteFormatoPagoExpress.Contains('.'))
                {
                    string[] valor;
                    valor = Convert.ToString(ImporteFormatoPagoExpress).Split('.');
                    //  valor = Convert.ToString(ImporteFormatoPagoExpress).Split('.');
                    if (valor[1].Length == 1)
                        valor[1] = valor[1].Insert(1, "0");
                    else
                        valor[1] = valor[1].Substring(0, 2);

                    Importe= valor[0] + valor[1];
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
        #endregion

    }
}
