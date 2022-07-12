using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using DAL;
using System.Data;
using BE;
namespace BC
{
    class Persona_Operaciones:BCEntidad
    {

        private string campos(string prefijo = "p_o")
        {
            string strCampos = String.Format(@"{0}.idp_operaciones,{0}.personaid", prefijo);
            return strCampos;
        }

        public Persona_Operaciones() : base()
        {
        }

        public Persona_Operaciones(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<BE.Persona_Operaciones> CargarBE(DataRow[] dr)
        {
            List<BE.Persona_Operaciones> lst = new List<BE.Persona_Operaciones>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }


        public BE.Persona_Operaciones CargarBE(DataRow dr)
        {
            BE.Persona_Operaciones obj = new BE.Persona_Operaciones();

            obj.idp_operaciones = Convert.ToInt32(dr["idp_operaciones"].ToString());
            obj.personaid = Convert.ToInt32(dr["personaid"].ToString());


            return obj;
        }

    }
}
