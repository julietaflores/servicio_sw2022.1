using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
    public class RequiereServicioProveedoresF:BCEntidad
    {
        public RequiereServicioProveedoresF() : base()
        {
        }

        public RequiereServicioProveedoresF(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
        public List<BE.RequiereServicioProveedoresF> CargarBE(DataRow[] dr)
        {
            List<BE.RequiereServicioProveedoresF> lst = new List<BE.RequiereServicioProveedoresF>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.RequiereServicioProveedoresF CargarBE(DataRow dr)
        {
            BE.RequiereServicioProveedoresF obj = new BE.RequiereServicioProveedoresF();
            obj.RequiereServicioId = (dr["requiereServicioid"].ToString());
            obj.ProveedorNombre = dr["PersonaNombres"].ToString();
            obj.StatusRequiereId = Convert.ToDecimal(dr["StatusRequiereId"].ToString());
            obj.PersonaProveedorId = dr["personaId"].ToString();
            obj.EstadoRecepcion = 1;


            return obj;
        }

        public void CargarRelaciones(ref List<BE.RequiereServicioProveedoresF> colObj, IEnumerable<decimal> estados = null, string lang = "", long personaId = 0, params Enum[] relaciones)
        {
            if (relaciones == null || colObj == null)
            {
                return;
            }
            IEnumerable<decimal> llaves;
            IEnumerable<decimal> llavesP;
            IEnumerable<string> sllaves;
            List<BE.IdiomaS> colIdiomaS = null;
            List<BE.Servicio> colServicio = null;
            List<BE.EstadoReqServ> colestadoReqServ = null;
            List<BE.RequiereServicioProveedores> colRequiereServicioProveedores = null;
            List<BE.PersonaDireccion> colPersonaDireccion = null;
            List<BE.Persona> colPersona = null;
            List<BE.ServAsig> colServAsig = null;
            List<BE.requiereServicioDetalle> colrequiereServicioDetalle = null;
            List<BE.requiereServicioDetalleWeb> colrequiereServicioDetalleWeb = null;

            foreach (Enum clase in relaciones)
            {
             //   if (clase.Equals(BE.relReqServProvF.EstadoProvReqId))
               // {
                    BC.IdiomaS bcIdiomaS = new BC.IdiomaS(cadenaConexion);

                    llaves = (from elemento in colObj where elemento.StatusRequiereId != null select Convert.ToDecimal(elemento.StatusRequiereId)).Distinct();
                    colIdiomaS = bcIdiomaS.ObtenerHijosStatus(llaves, relaciones);
                    bcIdiomaS = null;

                
                 
              //  }
             

            if (relaciones.GetLength(0) > 0)
            {
                foreach (var item in colObj)
                {
                        if (colIdiomaS != null && colIdiomaS.Count > 0)
                        {
                            item.EstadoProvReqId = colIdiomaS;
                        }

                    
                
                }
            }
            }
        }


        public List<BE.RequiereServicioProveedoresF> ObtenerHijos(IEnumerable<string> llaves, params Enum[] relaciones)
        {
            List<BE.RequiereServicioProveedoresF> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"select rs.requiereServicioid,p.PersonaNombres,rsp.StatusRequiereId,p.personaId
 from requiereServicio rs with(nolock)
inner join requiereServicioProveedores rsp with(nolock)
on rs.RequiereServicioId=rsp.RequiereServicioId
inner join servicioPersona sp with(nolock)
on rsp.servicioPersonaId=sp.serviciopersonaId
inner join persona p with (nolock)
on sp.personaId=p.personaId
and rs.requiereservicioid={0}", this.ConcatenarLlaves(llaves));
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                    CargarRelaciones(ref obj,null,"", 0, relaciones);
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
