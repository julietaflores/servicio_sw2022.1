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
   public class PersonaDireccion:BCEntidad
    {
        private string campos(string prefijo = "perdir")
        {
            string strCampos = String.Format(@"{0}.PersonaId,{0}.PersonaDireccionId,{0}.TipoDireccionId,PersonaDireccionTitulo,
                                       {0}.PersonaDireccionGeo,{0}.PersonaDireccionDescripcion,{0}.CiudadDireccionId,{0}.PersonaDireccionFHMod,
                                       {0}.PersonaDireccionUsuarioMod,{0}.EstadoDireccionId,{0}.PersonaDireccionDireccion"
                    , prefijo);
            return strCampos;
        }
        public PersonaDireccion() : base()
        {
        }
        public PersonaDireccion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }

        #region  "DEFINICION DE METODOS DE ABM"

        public Boolean Actualizar(ref BE.PersonaDireccion BEObj, Boolean isTransaccion = false)
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
                        strSql = @"update 
PersonaDireccion set 

TipoDireccionId=@TipoDireccionId,
PersonaDireccionTitulo=@PersonaDireccionTitulo, 
PersonaDireccionGeo=@PersonaDireccionGeo,
PersonaDireccionDescripcion=@PersonaDireccionDescripcion,
CiudadDireccionId=@CiudadDireccionId,
PersonaDireccionFHMod=@PersonaDireccionFHMod,
PersonaDireccionUsuarioMod=@PersonaDireccionUsuarioMod,
EstadoDireccionId=@EstadoDireccionId,
PersonaDireccionDireccion=@PersonaDireccionDireccion
where (PersonaId=@personaId and PersonaDireccionId=@PersonaDireccionId)


";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into  PersonaDireccion(PersonaId,PersonaDireccionId,TipoDireccionId,PersonaDireccionTitulo,PersonaDireccionGeo,PersonaDireccionDescripcion,
CiudadDireccionId,PersonaDireccionFHMod,PersonaDireccionUsuarioMod,EstadoDireccionId,PersonaDireccionDireccion)
values
(@PersonaId,@PersonaDireccionId,@TipoDireccionId,@PersonaDireccionTitulo,@PersonaDireccionGeo,@PersonaDireccionDescripcion,
@CiudadDireccionId,@PersonaDireccionFHMod,@PersonaDireccionUsuarioMod,@EstadoDireccionId,@PersonaDireccionDireccion)
";
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
                    
                        string sql = String.Format(@"Select isnull(max(PersonaDireccionId),0) + 1 from dbo.PersonaDireccion with (nolock) where PersonaId={0} ",Convert.ToInt32(BEObj.PersonaId) );
                        BEObj.PersonaDireccionId = System.Convert.ToDecimal(conx.ObtenerValor(sql));
                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@PersonaId", BEObj.PersonaId);
                    conx.AsignarParametro("@PersonaDireccionId", BEObj.PersonaDireccionId);
                    conx.AsignarParametro("@TipoDireccionId", BEObj.TipoDireccionId);
                    conx.AsignarParametro("@PersonaDireccionTitulo", BEObj.PersonaDireccionTitulo);
                    conx.AsignarParametro("@PersonaDireccionGeo", BEObj.PersonaDireccionGeo);
                    conx.AsignarParametro("@PersonaDireccionDescripcion", BEObj.PersonaDireccionDescripcion);
                    conx.AsignarParametro("@CiudadDireccionId", BEObj.CiudadDireccionId);
                    conx.AsignarParametro("@PersonaDireccionFHMod", BEObj.PersonaDireccionFHMod);
                    conx.AsignarParametro("@PersonaDireccionUsuarioMod", BEObj.PersonaDireccionUsuarioMod);
                    conx.AsignarParametro("@EstadoDireccionId", BEObj.EstadoDireccionId);
                    conx.AsignarParametro("@PersonaDireccionDireccion", BEObj.PersonaDireccionDireccion);

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



        public List<BE.PersonaDireccion> CargarBE(DataRow[] dr)
        {
            List<BE.PersonaDireccion> lst = new List<BE.PersonaDireccion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.PersonaDireccion CargarBE(DataRow dr)
        {

            BE.PersonaDireccion obj = new BE.PersonaDireccion();
            obj.PersonaId = Convert.ToDecimal(dr["PersonaId"].ToString());
            obj.PersonaDireccionId = Convert.ToDecimal(dr["PersonaDireccionId"].ToString());
            obj.TipoDireccionId = Convert.ToDecimal(dr["TipoDireccionId"].ToString());
            obj.PersonaDireccionTitulo = dr["PersonaDireccionTitulo"].ToString();
            obj.PersonaDireccionGeo = dr["PersonaDireccionGeo"].ToString();
            obj.PersonaDireccionDescripcion = dr["PersonaDireccionDescripcion"].ToString();
            obj.CiudadDireccionId = Convert.ToDecimal(dr["CiudadDireccionId"].ToString());
            obj.PersonaDireccionFHMod = Convert.ToDateTime(dr["PersonaDireccionFHMod"].ToString());
            obj.PersonaDireccionUsuarioMod = dr["PersonaDireccionUsuarioMod"].ToString();
            obj.EstadoDireccionId = Convert.ToDecimal(dr["EstadoDireccionId"].ToString());
            obj.PersonaDireccionDireccion = dr["PersonaDireccionDireccion"].ToString();
            return obj;

            
        }
        public void CargarRelaciones(ref List<BE.PersonaDireccion> colObj, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<string> sllaves;
            List<BE.PersonaDireccion> colRequiereServicio = null;
            foreach (Enum clase in relaciones)
            {
                if (clase.Equals(BE.relServAsig.requiereServicio))
                {
                    //BC.RequiereServicio bcRequiereServicio = new BC.RequiereServicio(cadenaConexion);
                    //sllaves = (from elemento in colObj select elemento.RequiereServicioId).Distinct();
                    //colRequiereServicio = bcRequiereServicio.ObtenerHijos(sllaves, relaciones);
                    //bcRequiereServicio = null;
                }

            }

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {

                    //if (colRequiereServicio != null && colRequiereServicio.Count > 0)
                    //{
                    //    item.requiereServicio = (from elemento in colRequiereServicio where elemento.RequiereServicioId == item.RequiereServicioId select elemento).ToList().FirstOrDefault();
                    //}
                }
            }
        }

        public List<BE.PersonaDireccion> ObtenerHijos(IEnumerable<decimal> llaves, IEnumerable<decimal> llavesP, params Enum[] relaciones)
        {
            List<BE.PersonaDireccion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.PersonaDireccion perdir with(nolock) where perdir.PersonaDireccionId in {1} and perdir.PersonaId in {2} 
union

select 0 as PersonaId,0 PersonaDireccionId,0 TipoDireccionId,'' PersonaDireccionTitulo,
'' PersonaDireccionGeo,'' PersonaDireccionDescripcion, 0 CiudadDireccionId,
'' PersonaDireccionFHMod,
'' PersonaDireccionUsuarioMod,0 EstadoDireccionId,'' PersonaDireccionDireccion
       

", campos("perdir"), this.ConcatenarLlaves(llaves),this.ConcatenarLlaves(llavesP));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                   // CargarRelaciones(ref obj, relaciones);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion

        #region "Listados"
        public List<BE.PersonaDireccion> ListadoxPersona(decimal personaId)
        {
            List<BE.PersonaDireccion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.PersonaDireccion perdir with(nolock) where perdir.PersonaId = {1} and EstadoDireccionId=1" ,campos("perdir"),  personaId);
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


        public List<BE.PersonaDireccion> ListadoPersonaDireccion()
        {
            List<BE.PersonaDireccion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.PersonaDireccion perdir", campos("perdir"));
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

        #region "METODOS DE OPTIMIZACION"

        public BE.PersonaDireccion ListadoxPersonaDireccion(decimal personaId, decimal PersonaDireccionId)
        {
            BE.PersonaDireccion obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"SELECT {0}
                                            FROM dbo.PersonaDireccion perdir with(nolock) where perdir.PersonaId = {1} and PersonaDireccionId={2}", campos("perdir"), personaId, PersonaDireccionId);
                DataRow dr = conx.ObtenerFila(sql);
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
        public void ActualizarPersonaDireccionLasT(decimal IdPersona)
        {
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = "ActualizarPersonaDireccionLasT";
                conx.Conectar();
                conx.CrearComando(sql, CommandType.StoredProcedure);
                conx.AsignarParametro("@IdPersona", IdPersona);

              conx.EjecutarComando();

            }
            catch (Exception ex)
            {

                throw;
            }




        }

        #endregion
    }
}
