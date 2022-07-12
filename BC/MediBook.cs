using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
   public class MediBook:BCEntidad
    {

        public MediBook() : base()
        {
        }

        public MediBook(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        public List<BE.MediBook> CargarBE(DataRow[] dr)
        {
            List<BE.MediBook> lst = new List<BE.MediBook>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.MediBook CargarBE(DataRow dr)
        {
            BE.MediBook obj = new BE.MediBook();
            obj.RequiereServicioId = (dr["RequiereServicioId"].ToString());
            obj.Name = (dr["Name"].ToString());
            obj.Lastname = dr["Lastname"].ToString();
            obj.Lastnamemother =(dr["Lastnamemother"].ToString());
            obj.Email = dr["Email"].ToString();
            obj.Cellphone = (dr["Cellphone"].ToString());
            obj.Ci = (dr["Ci"].ToString());
            obj.Expedition = (dr["Expedition"].ToString());
            obj.Birthday = (dr["Birthday"].ToString());
            obj.user = (dr["user"].ToString());
            obj.password = (dr["password"].ToString());




            return obj;
        }
        public List<BE.MediBook> ObtenerDatosMediBook()
        {
            List<BE.MediBook> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = "MediBook";


                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                
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
