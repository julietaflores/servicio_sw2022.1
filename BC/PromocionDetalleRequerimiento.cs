using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BC
{
  public  class PromocionDetalleRequerimiento : BCEntidad
    {

        private string campos(string prefijo = "promdpd")
        {
            string strCampos = String.Format(@"{0}.PPromocionId,{0}.PPersonaId,{0}.PFechaInsercion,{0}.RequiereServicioId,{0}.Estado", prefijo);
            return strCampos;
        }


        public PromocionDetalleRequerimiento() : base()
        {
        }

        public PromocionDetalleRequerimiento(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }




        public Boolean Actualizar(ref BE.PromocionDetalleRequerimiento BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.PromocionDetalleRequerimiento
                                    SET PPromocionId = @PPromocionId,
	                                    PPersonaId=@PPersonaId,
										PFechaInsercion=@PFechaInsercion,
                                        RequiereServicioId = @RequiereServicioId,
										Estado=@Estado
										
                                    where  PPersonaId = @PPersonaId  and RequiereServicioId = @RequiereServicioId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO dbo.PromocionDetalleRequerimiento (PPromocionId, PPersonaId, PFechaInsercion, RequiereServicioId,Estado) VALUES  (@PPromocionId, @PPersonaId, @PFechaInsercion, @RequiereServicioId,@Estado)";
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
                    conx.AsignarParametro("@PPromocionId", BEObj.PPromocionId);
                    conx.AsignarParametro("@PPersonaId", BEObj.PPersonaId);
                    conx.AsignarParametro("@PFechaInsercion", BEObj.PFechaInsercion);
                    conx.AsignarParametro("@RequiereServicioId", BEObj.RequiereServicioId);
                    conx.AsignarParametro("@Estado", BEObj.Estado);
                

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



        public List<BE.PromocionDetalleRequerimiento> CargarBE(DataRow[] dr)
        {
            List<BE.PromocionDetalleRequerimiento> lst = new List<BE.PromocionDetalleRequerimiento>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }

        public BE.PromocionDetalleRequerimiento CargarBE(DataRow dr)
        {
            BE.PromocionDetalleRequerimiento obj = new BE.PromocionDetalleRequerimiento();
            obj.PPromocionId = Convert.ToDecimal(dr["PPromocionId"].ToString());
            obj.PPersonaId = Convert.ToDecimal(dr["PPersonaId"].ToString());
            obj.PFechaInsercion = Convert.ToDateTime(dr["PFechaInsercion"].ToString());
            obj.RequiereServicioId = (dr["RequiereServicioId"].ToString());
            obj.Estado = Convert.ToBoolean(dr["Estado"].ToString());
            
            return obj;
        }

        public void CargarRelaciones(ref List<BE.PromocionDetalleRequerimiento> colObj, string lang ="", params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            List<BE.Promocion> colPromocion = null;


            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relPromocionDetalleRequerimiento.Promocion))
                {
                    BC.Promocion bcPromocion = new BC.Promocion(cadenaConexion);
                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.PPromocionId)).Distinct();
                    colPromocion = bcPromocion.Lista_ObtenerHijos(llaves, lang);
                    bcPromocion = null;
                }
            }


            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {
                    if (colPromocion != null && colPromocion.Count > 0)
                    {
                        item.promocion = (from elemento in colPromocion where elemento.PromocionId == item.PPromocionId select elemento).ToList().FirstOrDefault();
                    }

                }
            }


        }

        public void CargarRelaciones(ref BE.PromocionDetalleRequerimiento colObj, string lang ="", params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
          

            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relPromocionDetalleRequerimiento.Promocion))
                {
                    BC.Promocion bcPromocion = new BC.Promocion(cadenaConexion);
                    llaves = new decimal[] { Convert.ToDecimal(colObj.PPromocionId) };
                    colObj.promocion = bcPromocion.Lista_ObtenerHijos(llaves, lang).ToList().FirstOrDefault();
                  
                    bcPromocion = null;
                }
            }


        


        }




        public List<BE.PromocionDetalleRequerimiento> ListarPromocionDetalleRequerimiento(decimal PPersonaId, decimal PPromocionId, string RequiereServicioId ,string lang, params Enum[] relaciones)
        {
            // IEnumerable<decimal> llaves = new List<decimal>() { PersonaId };

            List<BE.PromocionDetalleRequerimiento> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}  FROM PromocionDetalleRequerimiento promdpd  where promdpd.PPersonaid = {1} and promdpd.PPromocionId = {2} and promdpd.RequiereServicioId='{3}' ", campos("promdpd"), PPersonaId, PPromocionId,RequiereServicioId);
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


        public List<BE.PromocionDetalleRequerimiento> ListarPromocionRequiereServicioId(string RequiereServicioId, string lang, params Enum[] relaciones)
        {
            // IEnumerable<decimal> llaves = new List<decimal>() { PersonaId };

            List<BE.PromocionDetalleRequerimiento> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}  FROM PromocionDetalleRequerimiento promdpd  where  promdpd.RequiereServicioId='{1}' and promdpd.Estado=1 ", campos("promdpd"), RequiereServicioId);
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








    }
}
