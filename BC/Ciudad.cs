using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BC
{
   public  class Ciudad:BCEntidad
    {
        private string campos(string prefijo = "c")
        {
            string strCampos = String.Format(@" {0}.CiudadId,{0}.CiudadNombre,{0}.RegionId,{0}.CiudadGoogleTag,{0}.CiudadGeolocalizacion,{0}.Estado "  , prefijo);
            return strCampos;
        }
        public Ciudad() : base()
        {
        }

        public Ciudad(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
      
 

        public List<BE.Ciudad> CargarBE(DataRow[] dr)
        {
            List<BE.Ciudad> lst = new List<BE.Ciudad>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.Ciudad CargarBE(DataRow dr)
        {
            BE.Ciudad obj = new BE.Ciudad();
            obj.CiudadId = Convert.ToDecimal(dr["CiudadId"].ToString());
            obj.CiudadNombre =dr["CiudadNombre"].ToString();
            obj.RegionId = Convert.ToDecimal(dr["RegionId"].ToString());
            obj.CiudadGoogleTag = dr["CiudadGoogleTag"].ToString();
            obj.CiudadGeolocalizacion = (dr["CiudadGeolocalizacion"].ToString());
            obj.Estado = Convert.ToInt32(dr["Estado"].ToString());
            return obj;
        }

        public void CargarRelaciones(ref List<BE.Ciudad> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.Region> colRequiereServicio = null;
            List<BE.ConfiguracionCiudad> colConfiguracionCiudad = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relciudad.region))
                {
                    BC.Region bcRegion = new BC.Region(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.RegionId).Distinct();
                    colRequiereServicio = bcRegion.ObtenerHijos(llaves, relaciones);
                    bcRegion = null;
                }
                if (clase.Equals(BE.relciudad.configuracionCiudad))
                {
                    BC.ConfiguracionCiudad bcConfiguracionCiudad = new BC.ConfiguracionCiudad(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.CiudadId).Distinct();
                    colConfiguracionCiudad = bcConfiguracionCiudad.ObtenerHijos(llaves, relaciones);
                    bcConfiguracionCiudad = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    {
                        item.Region = (from elemento in colRequiereServicio where elemento.RegionId == item.RegionId select elemento).ToList().FirstOrDefault();
                    }

                    if (colConfiguracionCiudad != null && colConfiguracionCiudad.Count > 0)
                    {
                        item.configuracionCiudad = (from elemento in colConfiguracionCiudad where elemento.CiudadId == item.CiudadId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }


        public List<BE.Ciudad> ObtenerHijos(IEnumerable<decimal> llaves, params Enum[] relaciones)
        {
            List<BE.Ciudad> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.Ciudad c with(nolock) where c.CiudadId in {1}", campos("c"), this.ConcatenarLlaves(llaves));
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


        public List<BE.Ciudad> ObtenerCiudad(string lang)
        {
            List<BE.Ciudad> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = "CiudadSP";


                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdiomaSigla", lang);
                DataTable dt = conx.ObtenerTablaSP(sql);

                ///////////////
                DataRow[] dr = new DataRow[dt.Rows.Count];
                dt.Rows.CopyTo(dr, 0);

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


        public List<BE.Ciudad> ObtenerListaCiudad(string lang)
        {
            List<BE.Ciudad> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = "CiudadesSP";


                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdiomaSigla", lang);
                DataTable dt = conx.ObtenerTablaSP(sql);

                ///////////////
                DataRow[] dr = new DataRow[dt.Rows.Count];
                dt.Rows.CopyTo(dr, 0);

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


    }
}
