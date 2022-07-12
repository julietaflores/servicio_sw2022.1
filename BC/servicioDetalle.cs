using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
   public  class servicioDetalle:BCEntidad
    {
        private string campos(string prefijo = "sd")
        {
            string strCampos = String.Format(@" {0}.ServicioId,{0}.ServicioDetalleId,{0}.ServicioDetalleDescripcion,{0}.ServicioDetallePrecioUnitario,{0}.ServicioDetalleCostoInicial,{0}.ServicioDetalleCostoFinal"
                    , prefijo);
            return strCampos;
        }
        public servicioDetalle() : base()
        {
        }

        public servicioDetalle(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        #region  "DEFINICION DE METODOS DE ABM"

      

        public List<BE.servicioDetalle> CargarBE(DataRow[] dr)
        {
            List<BE.servicioDetalle> lst = new List<BE.servicioDetalle>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.servicioDetalle CargarBE(DataRow dr)
        {
            BE.servicioDetalle obj = new BE.servicioDetalle();
            obj.servicioId = Convert.ToDecimal(dr["ServicioId"].ToString());
            obj.servicioDetalleId    = Convert.ToDecimal(dr["ServicioDetalleId"].ToString());
            obj.servicioDetalleDescripcion =(dr["ServicioDetalleDescripcion"].ToString());
            if (dr["servicioDetallePrecioUnitario"].ToString() != "")
            {
                obj.servicioDetallePrecioUnitario = Convert.ToInt32(dr["servicioDetallePrecioUnitario"].ToString());
            }
            if (dr["ServicioDetalleCostoInicial"].ToString() != "")
            {
                obj.servicioDetalleCostoInicial = Convert.ToInt32(dr["ServicioDetalleCostoInicial"].ToString());
            }
            if (dr["servicioDetalleCostoFinal"].ToString() != "")
            {
                obj.servicioDetalleCostoFinal = Convert.ToInt32(dr["servicioDetalleCostoFinal"].ToString());
            }

                             
            return obj;
        }

        public void CargarRelaciones(ref List<BE.servicioDetalle> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.servicioDetalle> colRequiereServicio = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relciudad.region))
                {
                   /* BC.Region bcRegion = new BC.Region(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.RegionId).Distinct();
                    colRequiereServicio = bcRegion.ObtenerHijos(llaves, relaciones);
                    bcRegion = null;*/
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    {
                       // item.Region = (from elemento in colRequiereServicio where elemento.RegionId == item.RegionId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }
        #endregion

        #region "Listados"
        public List<BE.servicioDetalle> ObtenerHijos(IEnumerable<decimal> llaves,string  lang, Boolean otros, params Enum[] relaciones)
        {
            List<BE.servicioDetalle> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            string sql = "";
            otros = true;
            try
            {
              
                    sql = String.Format(@"SELECT {0}
                    FROM ServicioDetalleSP('{2}') sd  where sd.servicioId in {1} order by ServicioDetalleId asc ", campos("sd"), this.ConcatenarLlaves(llaves),lang);

                
             


                   
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
        #endregion
        #region "Busqueda"
     
        #endregion
    }
}
