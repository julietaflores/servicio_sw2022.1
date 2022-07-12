using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class PromocionDetallePersona:BCEntidad
    {

        private string campos(string prefijo = "promdp")
        {
            string strCampos = String.Format(@"{0}.PromocionId,{0}.PersonaId,{0}.FechaInsercion,{0}.Estado,{0}.Valor", prefijo);
            return strCampos;
        }


        public PromocionDetallePersona() : base()
        {
        }

        public PromocionDetallePersona(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }




        public Boolean Actualizar(ref BE.PromocionDetallePersona BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.PromocionDetallePersona
                                    SET PromocionId = @PromocionId,
	                                    PersonaId=@PersonaId,
										FechaInsercion=@FechaInsercion,
										Estado=@Estado,
										Valor=@Valor
                                    where  PromocionId = @PromocionId and PersonaId=@PersonaId";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"INSERT INTO dbo.PromocionDetallePersona (PromocionId, PersonaId, FechaInsercion, Estado, Valor) VALUES  (@PromocionId, @PersonaId, @FechaInsercion, @Estado, @Valor)";
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
                    conx.AsignarParametro("@PromocionId", BEObj.PromocionId);
                    conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
                    conx.AsignarParametro("@FechaInsercion", BEObj.FechaInsercion);
                    conx.AsignarParametro("@Estado", BEObj.Estado);
                    conx.AsignarParametro("@Valor", BEObj.Valor);
                 
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



        public List<BE.PromocionDetallePersona> CargarBE(DataRow[] dr)
        {
            List<BE.PromocionDetallePersona> lst = new List<BE.PromocionDetallePersona>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }

        public BE.PromocionDetallePersona CargarBE(DataRow dr)
        {
            BE.PromocionDetallePersona obj = new BE.PromocionDetallePersona();
            obj.PromocionId = Convert.ToDecimal(dr["PromocionId"].ToString());
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.FechaInsercion = Convert.ToDateTime(dr["FechaInsercion"].ToString());
            obj.Estado = Convert.ToBoolean(dr["Estado"].ToString());
            obj.Valor = Convert.ToDecimal(dr["Valor"].ToString());
            return obj;
        }

        public void CargarRelaciones(ref List<BE.PromocionDetallePersona> colObj, string lang = "", params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            List<BE.Promocion> colPromocion = null;
        
            
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relPromocionDetallePersona.Promocion))
                {
                    BC.Promocion bcPromocion = new BC.Promocion(cadenaConexion);
                    llaves = (from elemento in colObj select Convert.ToDecimal(elemento.PromocionId)).Distinct();
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
                        item.promocion = (from elemento in colPromocion where elemento.PromocionId == item.PromocionId select elemento).ToList().FirstOrDefault();
                    }
                  
                }
            }


        }



     

        public List<BE.PromocionDetallePersona> Lista_ObtenerHijos(decimal PersonaId, string lang, params Enum[] relaciones)
        {
            List<BE.PromocionDetallePersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM PromocionDetallePersona promdp  where promdp.Personaid = {1} and promdp.Estado=1", campos("promdp"), PersonaId, lang);
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj, lang,relaciones);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }



        public List<BE.PromocionDetallePersona> Lista_ObtenerHijos1(IEnumerable<decimal> llaves)
        {
            List<BE.PromocionDetallePersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM PromocionDetallePersona promdp  where promdp.Personaid in {1} and promdp.Estado=1", campos("promdp"), this.ConcatenarLlaves(llaves));
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

        public List<BE.PromocionDetallePersona> Lista_ObtenerHijospromocion(IEnumerable<decimal> llaves, IEnumerable<decimal> llavesp)
        {
            List<BE.PromocionDetallePersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM PromocionDetallePersona promdp  where promdp.PromocionId in {1} and promdp.PersonaId in {2} ", campos("promdp"), this.ConcatenarLlaves(llaves), this.ConcatenarLlaves(llavesp));
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



        public List<BE.PromocionDetallePersona> ExistePersona(decimal PersonaId, decimal PromocionId)
        {
           // IEnumerable<decimal> llaves = new List<decimal>() { PersonaId };
       
            List<BE.PromocionDetallePersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM PromocionDetallePersona promdp  where promdp.Personaid = {1} and promdp.PromocionId = {2} and promdp.Estado=1 ", campos("promdp"), PersonaId,PromocionId);
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

        public List<BE.PromocionDetallePersona> Lista1(decimal PersonaId, decimal PromocionId)
        {
            // IEnumerable<decimal> llaves = new List<decimal>() { PersonaId };

            List<BE.PromocionDetallePersona> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM PromocionDetallePersona promdp  where promdp.Personaid = {1} and promdp.PromocionId = {2}  ", campos("promdp"), PersonaId, PromocionId);
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

    }
}
