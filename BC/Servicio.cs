using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace BC
{
    public class Servicio : BCEntidad
    {
        private string campos(string prefijo = "servicio")
        {
            string strCampos = String.Format(@" {0}.ServicioId,{0}.ServicioNombre,{0}.ServicioURLFoto,{0}.CategoriaServicioId,{0}.ServicioUsuario,
 {0}.ServicioFechaHoraMod,{0}.ServicioKeyWords,{0}.ServicioPorcentaje,{0}.ServicioTarifaMinima,{0}.ServicioDetalleTipo,{0}.ServicioSabado,{0}.ServicioDomingo,
{0}.ServicioHorarioRegularIni,{0}.ServicioHorarioRegularFin,{0}.ServicioHorarioSabadoIni,{0}.ServicioHorarioSabadoFin,{0}.ServicioHorarioDomingoIni,{0}.ServicioHorarioDomingoFin,
{0}.ServicioPersonaEnTurno,{0}.ServicioDetalleFormulario,{0}.TipoServicioId,{0}.ServicioTarifaPlana,{0}.ServicioTarifaInsumos_Extras,{0}.nroProveedores,{0}.ServicioHoras,{0}.ServicioDescripcion"
                    , prefijo);
            return strCampos;
        }
        public Servicio() : base()
        {
        }
        public Servicio(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.PostContenido BEObj)
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
                        strSql = "update  PostContenido set PostContenidoId = @PostContenidoId,PostContenidoImagen = @PostContenidoImagen,PostContenidoVideo = @PostContenidoVideo,PostContenidoVisible = @PostContenidoVisible";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = "insert into PostContenido values(@PostId,@PostContenidoId,@PostContenidoImagen,@PostContenidoVideo,@PostContenidoVisible)";
                        break;
                }

                if (dbConexion != null)
                    conx = dbConexion;
                else
                    conx.Conectar();
                if (BEObj.TipoEstado == BE.TipoEstado.Insertar)
                    // BEObj.ClienteId = System.Convert.ToInt32(conx.GetValor("select isnull(max(ClienteId),0) + 1 from dbo.Cliente with (nolock)"));
                    conx.CrearComando(strSql);

                conx.AsignarParametro("@PostId", BEObj.PostId);
                conx.AsignarParametro("@PostContenidoId", BEObj.PostContenidoId);
                conx.AsignarParametro("@PostContenidoImagen", BEObj.PostContenidoImagen);
                conx.AsignarParametro("@PostContenidoVideo", BEObj.PostContenidoVideo);
                conx.AsignarParametro("@PostContenidoVisible", BEObj.PostContenidoVisible);
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



        public List<BE.Servicio> CargarBE(DataRow[] dr)
        {
            List<BE.Servicio> lst = new List<BE.Servicio>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Servicio CargarBE(DataRow dr)
        {
            BE.Servicio obj = new BE.Servicio();

            obj.ServicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            obj.ServicioNombre = Convert.ToString(dr["ServicioNombre"].ToString());
            obj.ServicioURLFoto = Convert.ToString(dr["ServicioURLFoto"].ToString());
            obj.CategoriaServicioId = Convert.ToDecimal(dr["CategoriaServicioId"].ToString());
            if (dr["ServicioUsuario"].ToString() != "")
            {
                obj.ServicioUsuario = Convert.ToString(dr["ServicioUsuario"].ToString());

            }
            if (dr["ServicioFechaHoraMod"].ToString() != "")
            {
                obj.ServicioFechaHoraMod = Convert.ToDateTime(dr["ServicioFechaHoraMod"].ToString());

            }
            if (dr["ServicioKeyWords"].ToString() != "")
                obj.ServicioKeyWords = Convert.ToString(dr["ServicioKeyWords"].ToString());
            {
            }
            if (dr["ServicioPorcentaje"].ToString() != "")
            {
                obj.ServicioPorcentaje = Convert.ToDecimal(dr["ServicioPorcentaje"].ToString());

            }
            if (dr["ServicioTarifaMinima"].ToString() != "")
            {
                obj.ServicioTarifaMinima = Convert.ToDecimal(dr["ServicioTarifaMinima"].ToString());

            }
            if (dr["ServicioDetalleTipo"].ToString() != "")
            {
                obj.servicioDetalleTipo = Convert.ToBoolean(dr["ServicioDetalleTipo"].ToString());

            }
            if (dr["ServicioSabado"].ToString() != "")
            {
                obj.servicioSabado = Convert.ToBoolean(dr["ServicioSabado"].ToString());

            }
            if (dr["ServicioSabado"].ToString() != "")
            {
                obj.servicioSabado = Convert.ToBoolean(dr["ServicioSabado"].ToString());

            }
            if (dr["ServicioDomingo"].ToString() != "")
            {
                obj.servicioDomingo = Convert.ToBoolean(dr["ServicioDomingo"].ToString());

            }
            if (dr["ServicioHorarioRegularIni"].ToString() != "")
            {
                obj.servicioHorarioRegularIni = Convert.ToDateTime(dr["ServicioHorarioRegularIni"].ToString());
            }
            if (dr["ServicioHorarioRegularFin"].ToString() != "")
            {
                obj.servicioHorarioRegularFin = Convert.ToDateTime(dr["ServicioHorarioRegularFin"].ToString());
            }
            if (dr["ServicioHorarioSabadoIni"].ToString() != "")
            {
                obj.servicioHorarioSabadoIni = Convert.ToDateTime(dr["ServicioHorarioSabadoIni"].ToString());
            }
            if (dr["servicioHorarioSabadoFin"].ToString() != "")
            {
                obj.servicioHorarioSabadoFin = Convert.ToDateTime(dr["servicioHorarioSabadoFin"].ToString());
            }
            if (dr["ServicioHorarioDomingoIni"].ToString() != "")
            {
                obj.servicioHorarioDomingoIni = Convert.ToDateTime(dr["ServicioHorarioDomingoIni"].ToString());
            }
            if (dr["ServicioHorarioDomingoFin"].ToString() != "")
            {
                obj.servicioHorarioDomingoFin = Convert.ToDateTime(dr["ServicioHorarioDomingoFin"].ToString());
            }
            if (dr["ServicioPersonaEnTurno"].ToString() != "")
            {
                obj.servicioPersonaEnTurno = Convert.ToDecimal(dr["ServicioPersonaEnTurno"].ToString());
            }
            if (dr["ServicioDetalleFormulario"].ToString() != "")
            {
                obj.servicioDetalleFormulario = Convert.ToString(dr["ServicioDetalleFormulario"].ToString());
            }


            if (dr["TipoServicioId"].ToString() != "")
            {
                obj.tipoServicioId = Convert.ToDecimal(dr["TipoServicioId"].ToString());

            }
            if (dr["ServicioTarifaPlana"].ToString() != "")
            {
                obj.servicioTarifaPlana = Convert.ToDecimal(dr["ServicioTarifaPlana"].ToString());

            }
            if (dr["ServicioTarifaInsumos_Extras"].ToString() != "")
            {
                obj.servicioTarifaInsumos_Extras = Convert.ToDecimal(dr["ServicioTarifaInsumos_Extras"].ToString());

            }

            if (dr["nroProveedores"].ToString() != "")
            {
                obj.nroProveedores = Convert.ToInt32(dr["nroProveedores"].ToString());

            }

            if (dr["ServicioHoras"].ToString() != "")
            {
                obj.ServicioHoras = Convert.ToInt32(dr["ServicioHoras"].ToString());

            }
            if (dr["ServicioDescripcion"].ToString() != "")
            {
                obj.ServicioDescripcion = (dr["ServicioDescripcion"].ToString());

            }



            return obj;


        }

        public void CargarRelaciones(ref List<BE.Servicio> colObj, string lang = "", Boolean otros = false, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.CategoriaServicio> colCategoriaServicio = null;
            List<BE.servicioDetalle> colservicioDetalle = null;
            List<BE.ServicioRequerimiento> colServicioRequ = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relServicio.categoriaServicio))
                {
                    BC.CategoriaServicio bcCategoriaServicio = new BC.CategoriaServicio(cadenaConexion);

                    llaves = (from elemento in colObj where elemento.CategoriaServicioId != null select Convert.ToDecimal(elemento.CategoriaServicioId)).Distinct();
                    colCategoriaServicio = bcCategoriaServicio.ObtenerHijos(llaves, lang, relaciones);
                    bcCategoriaServicio = null;
                }
                if (clase.Equals(BE.relServicio.servicioRequerimiento))
                {
                    BC.ServicioRequerimiento bcServReque = new BC.ServicioRequerimiento(cadenaConexion);

                    llaves = (from elemento in colObj where elemento.ServicioId != null select Convert.ToDecimal(elemento.ServicioId)).Distinct();
                    colServicioRequ = bcServReque.ObtenerHijos(llaves, lang, relaciones);
                    bcServReque = null;
                }
                if (clase.Equals(BE.relServicio.servicioDetalle))
                {
                    BC.servicioDetalle bcservicioDetalle = new BC.servicioDetalle(cadenaConexion);

                    llaves = (from elemento in colObj where elemento.ServicioId != null select Convert.ToDecimal(elemento.ServicioId)).Distinct();
                    colservicioDetalle = bcservicioDetalle.ObtenerHijos(llaves, lang, otros, relaciones);
                    bcservicioDetalle = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {
                    if (colCategoriaServicio != null && colCategoriaServicio.Count > 0)
                    {
                        item.categoriaServicio = (from elemento in colCategoriaServicio where elemento.CategoriaServicioId == item.CategoriaServicioId select elemento).ToList().FirstOrDefault();
                    }

                    if (colServicioRequ != null && colServicioRequ.Count > 0)
                    {
                        item.servicioRequerimiento = (from elemento in colServicioRequ where elemento.ServicioId == item.ServicioId select elemento).ToList().FirstOrDefault();
                    }
                    if (colservicioDetalle != null && colservicioDetalle.Count > 0)
                    {
                        item.servicioDetalle = (from elemento in colservicioDetalle where elemento.servicioId == item.ServicioId select elemento).ToList();
                    }

                }
            }
        }

        public void CargarRelaciones(ref BE.Servicio colObj, string lang = "", Boolean otros = false, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            BE.servicioDetalle servicioDetalle = null;
            foreach (Enum clase in relaciones)
            {

                if (clase.Equals(BE.relServicio.servicioDetalle))
                {
                    BC.servicioDetalle bcServicioDetalle = new BC.servicioDetalle(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(colObj.ServicioId) };
                    colObj.servicioDetalle = bcServicioDetalle.ObtenerHijos(llaves, lang, otros, relaciones).ToList();
                    bcServicioDetalle = null;
                }
                if (clase.Equals(BE.relServicio.categoriaServicio))
                {
                    BC.CategoriaServicio bcCategoriaServicio = new BC.CategoriaServicio(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(colObj.CategoriaServicioId) };
                    colObj.categoriaServicio = bcCategoriaServicio.ObtenerHijos(llaves, lang, relaciones).ToList().FirstOrDefault();
                    bcCategoriaServicio = null;
                }

                if (clase.Equals(BE.relServicio.servicioRequerimiento))
                {
                    BC.ServicioRequerimiento bcservicioRequerimiento = new BC.ServicioRequerimiento(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(colObj.ServicioId) };
                    colObj.servicioRequerimiento = bcservicioRequerimiento.ObtenerHijosWeb(llaves, lang, relaciones).ToList().FirstOrDefault();
                    bcservicioRequerimiento = null;
                }
                if (clase.Equals(BE.relServicio.servicioTexto))
                {
                    BC.ServicioTexto bcservicioTexto = new BC.ServicioTexto(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(colObj.ServicioId) };
                    colObj.servicioTexto = bcservicioTexto.ObtenerHijos(llaves, lang, relaciones).ToList().FirstOrDefault();
                    bcservicioTexto = null;
                }
                if (clase.Equals(BE.relServicio.servicioDescripcion))
                {
                    BC.ServicioDescripcion bcservicioDescripcion = new BC.ServicioDescripcion(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(colObj.ServicioId) };
                    colObj.servicioDescripcion = bcservicioDescripcion.ObtenerHijos(llaves, lang, relaciones).ToList().FirstOrDefault();
                    bcservicioDescripcion = null;
                }
            }
        }
        #endregion
        public List<BE.Servicio> ObtenerHijos(IEnumerable<decimal> llaves, string lang, Boolean otros, params Enum[] relaciones)
        {
            List<BE.Servicio> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM  servicioSPT('{2}') servicio where servicio.ServicioId in {1}", campos("servicio"), this.ConcatenarLlaves(llaves), lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, lang, otros, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public BE.Servicio ListadoXServicioId(decimal servicioId, string lang, Boolean otros, params Enum[] relaciones)
        {
            BE.Servicio obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select {0} from ServicioSP('{2}') servicio  where Servicioid='{1}'", campos("servicio"), servicioId, lang);
                DataRow dr = conx.ObtenerFila(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);

                    CargarRelaciones(ref obj, lang, otros, relaciones);
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
