using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using BE;

namespace BC
{
   public class ServAsigCosto : BCEntidad
    {
        private string campos(string prefijo = "sac")
        {
            string strCampos = String.Format(@"{0}.ServAsigCostoId,
{0}.ServAsigId,
{0}.ConceptoCostoId,
{0}.ServAsigCostoValor"
                    , prefijo);
            return strCampos;
        }
        public ServAsigCosto() : base()
        {
        }

        public ServAsigCosto(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

       public Boolean Actualizar(ref BE.ServAsigCosto BEObj, Boolean isTransaccion = false)
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
                        strSql = @"update dbo.ServAsigCosto set ServAsigId = @ServAsigId,ConceptoCostoId = @ConceptoCostoId,ServAsigCostoValor = @ServAsigCostoValor  where ServAsigCostoId = @ServAsigCostoId ";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert ServAsigCosto values (@ServAsigCostoId,@ServAsigId,@ConceptoCostoId,@ServAsigCostoValor)";
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
                        BEObj.ServAsigCostoId = this.GenIdAN("ServAsigCosto", conx);
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@ServAsigCostoId", BEObj.ServAsigCostoId);
                    conx.AsignarParametro("@ServAsigId", BEObj.ServAsigId);
                    conx.AsignarParametro("@ConceptoCostoId", BEObj.ConceptoCostoId);
                    conx.AsignarParametro("@ServAsigCostoValor", BEObj.ServAsigCostoValor);

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




        public List<BE.ServAsigCosto> CargarBE(DataRow[] dr)
        {
            List<BE.ServAsigCosto> lst = new List<BE.ServAsigCosto>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.ServAsigCosto CargarBE(DataRow dr)
        {
            BE.ServAsigCosto obj = new BE.ServAsigCosto();
            obj.ServAsigCostoId = Convert.ToString(dr["ServAsigCostoId"].ToString());
            obj.ServAsigId = Convert.ToString(dr["ServAsigId"].ToString());
            obj.ConceptoCostoId = Convert.ToDecimal(dr["ConceptoCostoId"].ToString());
            obj.ServAsigCostoValor = Convert.ToDecimal(dr["ServAsigCostoValor"].ToString());

            return obj;
                                 
        }
        public void CargarRelaciones(ref List<BE.ServAsigCosto> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.ConceptoCosto> colConceptoCosto = null;
          
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relservAsigCosto.conceptoCosto))
                {
                    BC.ConceptoCosto bcConceptoCosto = new BC.ConceptoCosto(cadenaConexion);
                    llaves = (from elemento in colObj select elemento.ConceptoCostoId).Distinct();
                    colConceptoCosto = bcConceptoCosto.ObtenerHijos(llaves, relaciones);
                    bcConceptoCosto = null;
                }

            
            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    if (colConceptoCosto != null && colConceptoCosto.Count > 0)
                    {
                        item.conceptoCosto = (from elemento in colConceptoCosto where elemento.ConceptoCostoId == item.ConceptoCostoId select elemento).ToList().FirstOrDefault();
                    }
                }
            }
        }

        #endregion

        #region "Listados"
        public List<BE.ServAsigCosto> ObtenerHijos(IEnumerable<string> llaves,string lang="", params Enum[] relaciones)
        {
            List<BE.ServAsigCosto> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.ServAsigCosto sac with(nolock) where sac.ServAsigId in {1}", campos("sac"), this.ConcatenarLlaves(llaves));
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

        public List<BE.ServAsigCosto> BuscarServAsigCostoxId(string ServAsigId)
        {
           List<BE.ServAsigCosto> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT  *
                                            FROM dbo.ServAsigCosto with(nolock) where ServAsigId={0}", ServAsigId);
                DataRow[] dr = conx.ObtenerFilas(sql);
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
        #endregion

    }
}
