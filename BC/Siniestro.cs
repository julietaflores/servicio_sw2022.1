using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class Siniestro:BCEntidad
    {
        public Siniestro() : base()
        {
        }

        public Siniestro(string cadConx) : base(cadConx)
        {

        }

        private string campos(string prefijo = "s")
        {
            string strCampos = String.Format(@"{0}.SiniestroId  , {0}.SiniestroCodigo, {0}.SiniestroFechaHoraReg, {0}.SiniestroDescripcion, {0}.SiniestroFoto1,
                                          {0}.SiniestroFoto2,{0}.SiniestroFoto3,{0}.SiniestroFoto4,{0}.SiniestroVideo,{0}.SiniestroCreadoPor,{0}.ServAsigId,{0}.SiniestroUID,{0}.SiniestroDescripcion2"
                    , prefijo);
            return strCampos;
        }

        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.Siniestro BEObj, Boolean isTransaccion = false)
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
                        strSql = @"update Siniestro
set 

SiniestroCodigo=@SiniestroCodigo,
SiniestroFechaHoraReg=@SiniestroFechaHoraReg,
SiniestroDescripcion=@SiniestroDescripcion,
SiniestroFoto1=@SiniestroFoto1,
SiniestroFoto2=@SiniestroFoto2,
SiniestroFoto3=@SiniestroFoto3,
SiniestroFoto4=@SiniestroFoto4,
SiniestroVideo=@SiniestroVideo,
SiniestroCreadoPor=@SiniestroCreadoPor,
ServAsigId=@ServAsigId,
SiniestroUID=@SiniestroUID,
SiniestroDescripcion2=@SiniestroDescripcion2
	                                 
                                    where  SiniestroId = @SiniestroId   ;";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into Siniestro(SiniestroCodigo,SiniestroFechaHoraReg,SiniestroDescripcion,SiniestroFoto1,SiniestroFoto2,
SiniestroFoto3,SiniestroFoto4,SiniestroVideo,SiniestroCreadoPor,ServAsigId,SiniestroUID,SiniestroDescripcion2)

values(@SiniestroCodigo,@SiniestroFechaHoraReg,@SiniestroDescripcion,@SiniestroFoto1,@SiniestroFoto2,@SiniestroFoto3,
@SiniestroFoto4,@SiniestroVideo,@SiniestroCreadoPor,@ServAsigId,@SiniestroUID,@SiniestroDescripcion2)";
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
                        BEObj.SiniestroId = System.Convert.ToDecimal(conx.ObtenerValor("select isnull(max(SiniestroId),0) + 1 from dbo.Siniestro with (nolock);"));
                    }
                    if (BEObj.TipoEstado == BE.TipoEstado.Modificar)
                    {
                    
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@SiniestroId", BEObj.SiniestroId);
                    conx.AsignarParametro("@SiniestroCodigo", BEObj.SiniestroCodigo);
                    conx.AsignarParametro("@SiniestroFechaHoraReg", BEObj.SiniestroFechaHoraReg);
                    conx.AsignarParametro("@SiniestroDescripcion", BEObj.SiniestroDescripcion);
                    conx.AsignarParametro("@SiniestroFoto1", BEObj.SiniestroFoto1);
                    conx.AsignarParametro("@SiniestroFoto2", BEObj.SiniestroFoto2);
                    conx.AsignarParametro("@SiniestroFoto3", BEObj.SiniestroFoto3);
                    conx.AsignarParametro("@SiniestroFoto4", BEObj.SiniestroFoto4);
                    conx.AsignarParametro("@SiniestroVideo", BEObj.SiniestroVideo);
                    conx.AsignarParametro("@SiniestroCreadoPor", BEObj.SiniestroCreadoPor);
                    conx.AsignarParametro("@ServAsigId", BEObj.ServAsigId);
                    conx.AsignarParametro("@SiniestroUID", BEObj.SiniestroUID);
                    conx.AsignarParametro("@SiniestroDescripcion2", BEObj.SiniestroDescripcion2);

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



        public List<BE.Siniestro> CargarBE(DataRow[] dr)
        {
            List<BE.Siniestro> lst = new List<BE.Siniestro>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Siniestro CargarBE(DataRow dr)
        {
            BE.Siniestro obj = new BE.Siniestro();
            obj.SiniestroId = Convert.ToDecimal(dr["SiniestroId"].ToString());
            obj.SiniestroCodigo = (dr["SiniestroCodigo"].ToString());
            if ((dr["SiniestroFechaHoraReg"].ToString()!=""))
            {
                obj.SiniestroFechaHoraReg = Convert.ToDateTime(dr["SiniestroFechaHoraReg"].ToString());

            }
            obj.SiniestroDescripcion = Convert.ToString(dr["SiniestroDescripcion"].ToString());
            obj.SiniestroFoto1 = (dr["SiniestroFoto1"].ToString());
            obj.SiniestroFoto2 = (dr["SiniestroFoto2"].ToString());
            obj.SiniestroFoto3 = (dr["SiniestroFoto3"].ToString());
            obj.SiniestroFoto4 = (dr["SiniestroFoto4"].ToString());
            obj.SiniestroVideo = (dr["SiniestroVideo"].ToString());
            obj.ServAsigId= (dr["ServAsigId"].ToString());
            obj.SiniestroUID = (dr["SiniestroUID"].ToString());
            obj.SiniestroDescripcion2 = (dr["SiniestroDescripcion2"].ToString());
            return obj;
        }




        #endregion


        public void CargarRelaciones(ref List<BE.Siniestro> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
           List<BE.Siniestro_Estado> colSiniestro_Estado = null;
            List<BE.BilleteraDetalle> colBilleteraDetalle = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relSiniestro.siniestro_estado))
                {
                    BC.Siniestro_Estado bcSiniestro_Estado = new BC.Siniestro_Estado(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.SiniestroId).Distinct();
                    colSiniestro_Estado = bcSiniestro_Estado.ObtenerHijos(llaves, relaciones);
                    bcSiniestro_Estado = null;
                }
          
            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colSiniestro_Estado != null && colSiniestro_Estado.Count > 0)
                    {
                        item.Siniestro_Estado = (from elemento in colSiniestro_Estado where elemento.SiniestroId == item.SiniestroId select elemento).ToList();
                    }

                 
                }
            }
        }

        #region "Listados"
       public List<BE.Siniestro> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.Siniestro> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Siniestro s with(nolock) where s.SiniestroId in {1}", campos("s"), this.ConcatenarLlaves(llaves));
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
        public BE.Siniestro BuscarPorUID(string uid, params Enum[] relaciones)
        {
            BE.Siniestro obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0} from dbo.Siniestro s with(nolock) where s.SiniestroUID = '{1}'", campos("S"), uid);
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
        public Boolean RegistrarSolicitud(ref BE.Siniestro siniestro)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                bolOk = Actualizar(ref siniestro, true);

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
        #endregion

        public List<BE.Siniestro> LisTadoSiniestro(decimal personaId, params Enum[] relaciones)
        {
            List<BE.Siniestro> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select s.SiniestroId,s.SiniestroCodigo,s.SiniestroFechaHoraReg,s.SiniestroDescripcion,
s.SiniestroFoto1,s.SiniestroFoto2,s.SiniestroFoto3,s.SiniestroFoto4,s.SiniestroVideo,s.SiniestroCreadoPor,s.ServAsigId,s.SiniestroUID,s.SiniestroDescripcion2

from Siniestro s with (nolock)
inner join ServAsig sa with (nolock)
on
s.ServAsigId=sa.ServAsigId
inner join RequiereServicio rs with (nolock)
on sa.RequiereServicioId=rs.RequiereServicioId
and
((sa.ProveedorId={1}) or (rs.PersonaId={1}))
order by SiniestroFechaHoraReg desc



                        ", campos("s"), personaId);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                 CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }



        public List<BE.Siniestro> LisTadoSiniestroPaginacion(decimal personaId, int index, int max, params Enum[] relaciones)
        {
            List<BE.Siniestro> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select s.SiniestroId,s.SiniestroCodigo,s.SiniestroFechaHoraReg,s.SiniestroDescripcion,
s.SiniestroFoto1,s.SiniestroFoto2,s.SiniestroFoto3,s.SiniestroFoto4,s.SiniestroVideo,s.SiniestroCreadoPor,s.ServAsigId,s.SiniestroUID,s.SiniestroDescripcion2
from Siniestro s with (nolock)
inner join ServAsig sa with (nolock)
on
s.ServAsigId=sa.ServAsigId
inner join RequiereServicio rs with (nolock)
on sa.RequiereServicioId=rs.RequiereServicioId
and
((sa.ProveedorId={0}) or (rs.PersonaId={0}))
order by s.SiniestroFechaHoraReg desc 
OFFSET {1} ROWS
FETCH NEXT {2} ROWS ONLY", personaId, index - 1, max);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {

                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }


        public int cantidadSiniestro(decimal personaId)
        {
            int obj = 0;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select s.SiniestroId,s.SiniestroCodigo,s.SiniestroFechaHoraReg,s.SiniestroDescripcion,
s.SiniestroFoto1,s.SiniestroFoto2,s.SiniestroFoto3,s.SiniestroFoto4,s.SiniestroVideo,s.SiniestroCreadoPor,s.ServAsigId,s.SiniestroUID,s.SiniestroDescripcion2

from Siniestro s with (nolock)
inner join ServAsig sa with (nolock)
on
s.ServAsigId=sa.ServAsigId
inner join RequiereServicio rs with (nolock)
on sa.RequiereServicioId=rs.RequiereServicioId
and
((sa.ProveedorId={0}) or (rs.PersonaId={0}))

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


        #region "mETODOS AGEGADOS DESPUES DE UNION CON IOS"
        public int ExisteRegistroSiniestroId(decimal SiniestroId)
        {
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            int cantidad = 0;
            try
            {
                string sql = String.Format(@"
select count(siniestroId) from Siniestro_Estado  with (nolock) where SiniestroId={0} ", SiniestroId);
                cantidad = Convert.ToInt32(conx.ObtenerValor(sql));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cantidad;
        }
        #endregion
    }
}
