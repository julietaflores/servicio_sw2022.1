using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;

namespace BC
{
    public class ServAsig:BCEntidad
    {
        private string campos(string prefijo = "sa")
        {
            string strCampos = String.Format(@"{0}.ServAsigId, {0}.ProveedorId, {0}.ServAsigFHUbicacion, {0}.ServAsigFHEstimadaLlegada, {0}.ServAsigFHInicio
                    , {0}.ServAsigFHFin, {0}.ServAsigFHPago, {0}.ServAsigCostoTotal, {0}.StatusServAsigId, {0}.RequiereServicioId, {0}.ServAsigPagaCliente,{0}.ServAsigCalificado"
                    , prefijo );
            return strCampos; 
        }
        public ServAsig() : base()
        {
        }

        public ServAsig(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

       
        public Boolean Actualizar(ref BE.ServAsig BEObj, Boolean isTransaccion = false)
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
                        strSql = @"update  ServAsig 
set ServAsigFHUbicacion=@ServAsigFHUbicacion,
ServAsigFHEstimadaLlegada=@ServAsigFHEstimadaLlegada,
ServAsigFHInicio=@ServAsigFHInicio,
ServAsigFHFin=@ServAsigFHFin,
ServAsigFHPago=@ServAsigFHPago,
ServAsigCostoTotal=@ServAsigCostoTotal,
StatusServAsigId=@StatusServAsigId,
RequiereServicioId=@RequiereServicioId,
ServAsigPagaCliente=@ServAsigPagaCliente,
ServAsigCalificado=@ServAsigCalificado
where ServAsigId=@ServAsigId and ProveedorId=@ProveedorId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert ServAsig values(@ServAsigId, @ProveedorId, @ServAsigFHUbicacion, @ServAsigFHEstimadaLlegada, @ServAsigFHInicio, @ServAsigFHFin, @ServAsigFHPago, @ServAsigCostoTotal, @StatusServAsigId, @RequiereServicioId, @ServAsigPagaCliente,@ServAsigCalificado)";
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
                        BEObj.ServAsigId = this.GenIdAN("ServAsig", conx);
                    }
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
                    conx.AsignarParametro("@ServAsigCalificado", BEObj.servAsigCalificado);

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

        public List<BE.ServAsig> CargarBE(DataRow[] dr)
        {
            List<BE.ServAsig> lst = new List<BE.ServAsig>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.ServAsig CargarBE(DataRow dr)
        {
           BE.ServAsig obj = new BE.ServAsig();
            obj.ServAsigId = Convert.ToString(dr["ServAsigId"].ToString());
            obj.ProveedorId = Convert.ToDecimal(dr["ProveedorId"].ToString());
            obj.ServAsigFHUbicacion = Convert.ToDateTime(dr["ServAsigFHUbicacion"].ToString());
            obj.ServAsigFHEstimadaLlegada = Convert.ToDateTime(dr["ServAsigFHEstimadaLlegada"].ToString());


            if (dr["ServAsigFHInicio"].ToString() != "")
            {
                obj.ServAsigFHInicio = Convert.ToDateTime(dr["ServAsigFHInicio"].ToString());
            }
            if (dr["ServAsigFHFin"].ToString() != "")
            {
                obj.ServAsigFHFin = Convert.ToDateTime(dr["ServAsigFHFin"].ToString());
            }
            if (dr["ServAsigFHPago"].ToString() != "")
            {
                obj.ServAsigFHPago = Convert.ToDateTime(dr["ServAsigFHPago"].ToString());
            }
            if (dr["ServAsigCostoTotal"].ToString() != "")
            {
                obj.ServAsigCostoTotal = Convert.ToDecimal(dr["ServAsigCostoTotal"].ToString());
            }
            if (dr["StatusServAsigId"].ToString() != "")
            {
                obj.StatusServAsigId = Convert.ToDecimal(dr["StatusServAsigId"].ToString());
            }
            if (dr["RequiereServicioId"].ToString() != "")
            {
                obj.RequiereServicioId = Convert.ToString(dr["RequiereServicioId"].ToString());
            }
            if (dr["ServAsigPagaCliente"].ToString() != "")
            {
                obj.ServAsigPagaCliente = Convert.ToBoolean(dr["ServAsigPagaCliente"].ToString());
            }
            if (dr["ServAsigCalificado"].ToString() != "")
            {
                obj.servAsigCalificado = Convert.ToBoolean(dr["ServAsigCalificado"].ToString());
            }


            return obj;
        }

        public void CargarRelaciones(ref List<BE.ServAsig> colObj, string lang = "", params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.RequiereServicio> colRequiereServicio = null;
            List<BE.ServAsigCosto> colServAsigCosto = null;
            List<BE.StatusServAsig> colStatusServAsig = null;
            List<BE.Post> colPost = null;
            List<BE.Persona> colProveedor = null;


            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relServAsig.requiereServicio))
                {
                    BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    colRequiereServicio = bcRequiereServicio.ObtenerHijos(sllaves, lang, relaciones);
                    bcRequiereServicio = null;
                }
                if (clase.Equals(BE.relServAsig.servAsigCosto))
                {
                    BC.ServAsigCosto bcServAsigCosto = new BC.ServAsigCosto(cadenaConexion);
                    sllaves = (from elemento in colObj select elemento.ServAsigId).Distinct();
                    colServAsigCosto = bcServAsigCosto.ObtenerHijos(sllaves, "", relaciones);
                    bcServAsigCosto = null;
                }
                if (clase.Equals(BE.relServAsig.statusServAsig))
                {
                    BC.StatusServAsig bcStatusServAsig = new BC.StatusServAsig(cadenaConexion);
                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.StatusServAsigId)).Distinct();
                    colStatusServAsig = bcStatusServAsig.ObtenerHijos(llaves, lang, relaciones);
                    bcStatusServAsig = null;
                }
                if (clase.Equals(BE.relServAsig.post))
                {
                    BC.Post bcPost = new BC.Post(cadenaConexion);
                    sllaves = (from elemento in colObj select Convert.ToString(elemento.ServAsigId)).Distinct();
                    colPost = bcPost.ObtenerHijos(sllaves, lang, relaciones);
                    bcPost = null;
                }
                if (clase.Equals(BE.relServAsig.post))
                {
                    BC.Post bcPost = new BC.Post(cadenaConexion);
                    sllaves = (from elemento in colObj select Convert.ToString(elemento.ServAsigId)).Distinct();
                    colPost = bcPost.ObtenerHijos(sllaves, lang, relaciones);
                    bcPost = null;
                }
                if (clase.Equals(BE.relServAsig.proveedor))
                {
                    BC.Persona bcProveedor = new BC.Persona(cadenaConexion);
                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.ProveedorId)).Distinct();

                    colProveedor = bcProveedor.ObtenerHijos(llaves, relaciones);
                    bcProveedor = null;
                }
            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    {
                        item.requiereServicio = (from elemento in colRequiereServicio where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList().FirstOrDefault();
                    }
                    if (colStatusServAsig != null && colStatusServAsig.Count > 0)
                    {
                        item.statusServAsig = (from elemento in colStatusServAsig where elemento.StatusServAsigId == item.StatusServAsigId select elemento).ToList().FirstOrDefault();
                    }
                    if (colPost != null && colPost.Count > 0)
                    {
                        item.post1 = (from elemento in colPost where elemento.ServAsigId == item.ServAsigId select elemento).ToList();
                    }
                    if (colPost != null && colPost.Count > 0)
                    {
                        item.post = (from elemento in colPost where elemento.ServAsigId == item.ServAsigId select elemento).ToList().FirstOrDefault();
                    }
                    if (colServAsigCosto != null && colServAsigCosto.Count > 0)
                    {
                        item.servAsigCosto = (from elemento in colServAsigCosto where elemento.ServAsigId == item.ServAsigId select elemento).ToList();
                    }
                    if (colProveedor != null && colProveedor.Count > 0)
                    {
                        item.proveedor = (from elemento in colProveedor where elemento.PersonaId == item.ProveedorId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }
        public void CargarRelaciones(ref BE.ServAsig obj, string lang = "", params Enum[] relaciones)
        {
            if (relaciones == null || obj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relServAsig.requiereServicio))
                {
                    BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                    sllaves = new string[] { obj.RequiereServicioId };
                    obj.requiereServicio = bcRequiereServicio.ObtenerHijos(sllaves, lang, relaciones).FirstOrDefault();
                    bcRequiereServicio = null;
                }
                if (clase.Equals(BE.relServAsig.servAsigCosto))
                {
                    BC.ServAsigCosto bcServAsigCosto = new BC.ServAsigCosto(cadenaConexion);
                    sllaves = new string[] { obj.ServAsigId };
                    obj.servAsigCosto = bcServAsigCosto.ObtenerHijos(sllaves, lang, relaciones).ToList();
                    bcServAsigCosto = null;
                }
                if (clase.Equals(BE.relServAsig.statusServAsig))
                {
                    BC.StatusServAsig bcStatusServAsig = new BC.StatusServAsig(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(obj.StatusServAsigId) };

                    obj.statusServAsig = bcStatusServAsig.ObtenerHijos(llaves, lang, relaciones).ToList().FirstOrDefault();
                    bcStatusServAsig = null;
                }
                if (clase.Equals(BE.relServAsig.post))
                {
                    BC.Post bcPost = new BC.Post(cadenaConexion);
                    sllaves = new string[] { (obj.ServAsigId) };

                    obj.post = bcPost.ObtenerHijos(sllaves, lang, relaciones).ToList().FirstOrDefault();
                    bcPost = null;
                }
                if (clase.Equals(BE.relServAsig.proveedor))
                {
                    BC.Persona bcProveedor = new BC.Persona(cadenaConexion);
                    llaves = new decimal[] { (obj.ProveedorId) };

                    obj.proveedor =bcProveedor.ObtenerHijos(llaves, relaciones).ToList().FirstOrDefault();
                    bcProveedor = null;
                }


            }
        }
    
        #endregion

        #region "Listados"
        public List<BE.ServAsig> ObtenerHijos(IEnumerable<string> llaves,string lang = "", params Enum[] relaciones)
        {
            List<BE.ServAsig> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ServAsig sa with(nolock) where sa.ServAsigId in {1}", campos("sa"), this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,lang,relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<BE.ServAsig> ObtenerPadres(IEnumerable<string> llaves, string lang = "", params Enum[] relaciones)
        {
            List<BE.ServAsig> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ServAsig sa with(nolock) where sa.RequiereServicioId in {1}", campos("sa"), this.ConcatenarLlaves(llaves));
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
        #endregion
        #region "Busqueda"

        public BE.ServAsig BuscarServAsigxId(string ServAsigId, params Enum[] relaciones)
        {
           BE.ServAsig obj = null;
         
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT * from servasig  with(nolock)  where ServAsigId='{0}'", ServAsigId);
                DataRow dr = conx.ObtenerFila(sql);
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
        public BE.ServAsig BuscarServAsigxRequiereServicioId(string requiereServicioId, params Enum[] relaciones)
        {
            BE.ServAsig obj = null;

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT * from servasig  with(nolock)  where RequiereServicioId='{0}'", requiereServicioId);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, "", relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public Boolean RegistrarSolicitud(ref BE.ServAsig servAsig)
        {
            Boolean bolOk = false;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
                bolOk = Actualizar(ref servAsig, true);
                if (!bolOk)
                {
                    throw new Exception("Error al registrar Solicitud.");
                }
                if (servAsig.servAsigCosto != null)
                {
                    BC.ServAsigCosto bcservAsigCosto = new BC.ServAsigCosto(cadenaConexion);
                    bcservAsigCosto.dbConexion = conx;

                   
                    foreach (BE.ServAsigCosto item in servAsig.servAsigCosto)
                    {
                        if (item.TipoEstado!=BE.TipoEstado.SinAccion)
                        { 
                          item.ServAsigId = servAsig.ServAsigId;
                          BE.ServAsigCosto sc = item;

                          bolOk = bcservAsigCosto.Actualizar(ref sc,true);
                          if (!bolOk)
                          {
                            throw new Exception("Error al enviar Solicitudes");
                           }
                          item.ServAsigId = sc.ServAsigId;
                        }
                    }
                    bcservAsigCosto = null;
                    
                }
                if (servAsig.post!= null)
                {

                    BE.Post bcPost = new BE.Post();
                    BC.PostContenido bcpostContenido = new BC.PostContenido(cadenaConexion);
                    bcPost = servAsig.post;
                    if (bcPost.PostContenido != null)
                    {
                      //  bcservAsigCosto.dbConexion = conx;
                        foreach (BE.PostContenido item in servAsig.post.PostContenido)
                        {
                            item.PostId = servAsig.post.PostId;
                            BE.PostContenido pc = item;
                            bolOk = bcpostContenido.Actualizar(ref pc, true);
                            if (!bolOk)
                            {
                                throw new Exception("Error al enviar Solicitudes");
                            }
                            item.PostId = pc.PostId;
                        }
                     //   bcservAsigCosto = null;

                    }

                 
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
        #region "NOTIFICACIONES"
        public DataSet ObtenerImporte_y_Calificacion(string ServAsigId)
        {
            DataSet obj =new DataSet();

            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT sum(sac.ServAsigCostoValor)ImporteProveedor,p.PostCalificacion FROM ServAsig sa with (nolock)
inner join Post p with (nolock)
on sa.ServAsigId=p.ServAsigId
inner join ServAsigCosto sac with (nolock)
on 
sa.ServAsigId=sac.ServAsigId
and sac.ConceptoCostoId in (3,5)
and sa.ServAsigId='{0}'
group by p.PostCalificacion", ServAsigId);
                obj = conx.ObtenerDataSet(sql);
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion

        public Boolean RegistrarActualizacionServAsig(ref BE.ServAsig sa, string lang)
        {
            Boolean bolOk = false;
            Boolean bolInicio = true;
            string ServicioPersonaURLFoto = "";
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            if (dbConexion == null)
            {
                conx.Conectar();
                conx.ComenzarTransaccion();
                dbConexion = conx;
            }
            else
            {
                conx = dbConexion;
                bolInicio = false;
            }
         
            BC.Post bcpost = new BC.Post(cadenaConexion);
            BC.PostContenido bcpostContenido= new BC.PostContenido(cadenaConexion);

            

            BE.Post post = new BE.Post();
            sa.TipoEstado = BE.TipoEstado.Modificar;
            bolOk = Actualizar(ref sa, true);
            if(bolOk == true)
            {
               /* if ((sa.post != null && sa.post.PostId == 0))
                {
                    foreach (ServAsigCosto i in sa.servAsigCosto)
                    {
                        //PostContenido  posCon= (postCont.PostContenido[0]);
                        ///////////////////

                        IdCosto = ObtenerId(conexion, "servAsigCosto", null);
                        ////////////////////
                        SqlCommand sqlCmd1 = new SqlCommand("[InsertarservAsigCosto]", conexion);
                        sqlCmd1.CommandType = CommandType.StoredProcedure;
                        sqlCmd1.CommandTimeout = 0;
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoId", IdCosto);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigId", sa.ServAsigId);
                        sqlCmd1.Parameters.AddWithValue("@ConceptoCostoId", i.ConceptoCostoId);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoValor", i.ServAsigCostoValor);

                        resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());

                        ////////////////////////////////////////////////////////////////////////////
                    }
                }
                else //actualizamos el Servicio Asignado costo
                {
                    int cont = 0;
                    foreach (ServAsigCosto i in sa.servAsigCosto)
                    {
                        //  PostContenido  posCon= (postCont.PostContenido[0]);
                        ////////////////////
                        SqlCommand sqlCmd1 = new SqlCommand("[ActualizarservAsigCosto]", conexion);
                        sqlCmd1.CommandType = CommandType.StoredProcedure;
                        sqlCmd1.CommandTimeout = 0;
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoId", sa.servAsigCosto[cont].ServAsigCostoId);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigId", sa.ServAsigId);
                        sqlCmd1.Parameters.AddWithValue("@ConceptoCostoId", i.ConceptoCostoId);
                        sqlCmd1.Parameters.AddWithValue("@ServAsigCostoValor", i.ServAsigCostoValor);
                        cont = cont + 1;
                        resPC = resPC + Convert.ToInt32(sqlCmd1.ExecuteNonQuery());

                        ////////////////////////////////////////////////////////////////////////////
                    }
                    if (cont == 0)
                    {
                        resPC = 1;
                    }

                */

            }
            return true;
              
        }

        public List<BE.ServAsig> ListadoRequerimientosNofinalizadosServicios(string lang,DateTime hora,params Enum[] relaciones)
        {
            List<BE.ServAsig> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"select * from RequiereServicio rs
inner join ServAsig sa
on rs.RequiereServicioId=sa.RequiereServicioId
and ServAsigFHPago is null
and ServAsigFHFin is null
and ServAsigFHInicio is not null
and  DATEDIFF(hour,ServAsigFHInicio, '{1}')>=8 

;", campos("sa"),hora);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,lang, relaciones);
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
