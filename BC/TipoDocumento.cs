using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class TipoDocumento : BCEntidad
    {
        private string campos(string prefijo = "c")
        {
            string strCampos = String.Format(@" {0}.TipoDocumentoId,{0}.TipoDocumentoNombre,{0}.TipoDocumentoAbreviatura ,{0}.Estado" , prefijo);
            return strCampos;
        }
        public TipoDocumento() : base()
        {
        }

        public TipoDocumento(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<BE.TipoDocumento> CargarBE(DataRow[] dr)
        {
            List<BE.TipoDocumento> lst = new List<BE.TipoDocumento>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.TipoDocumento CargarBE(DataRow dr)
        {
            BE.TipoDocumento obj = new BE.TipoDocumento();
            obj.TipoDocumentoId = Convert.ToDecimal(dr["TipoDocumentoId"].ToString());
            obj.TipoDocumentoNombre = dr["TipoDocumentoNombre"].ToString();
            obj.Estado = Convert.ToInt32(dr["Estado"].ToString());


            return obj;
        }

        public List<BE.TipoDocumento> ObtenerTipoDocumento(string lang)
        {
            List<BE.TipoDocumento> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "TipoDocumentoSP";


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
