using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BC
{
   public class Promocion:BCEntidad
    {
        private string campos(string prefijo = "prom")
        {
            string strCampos = String.Format(@"{0}.PromocionId,{0}.PromocionDescripcion,{0}.PromocionDescripcionCliente, {0}.PromocionValor,{0}.PromocionPorc,{0}.PromocionEstado", prefijo);
            return strCampos;
        }


        public Promocion() : base()
        {
        }

        public Promocion(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }


        public List<BE.Promocion> CargarBE(DataRow[] dr)
        {
            List<BE.Promocion> lst = new List<BE.Promocion>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }

        public BE.Promocion CargarBE(DataRow dr)
        {
            BE.Promocion obj = new BE.Promocion();
            obj.PromocionId = Convert.ToDecimal(dr["PromocionId"].ToString());
            obj.PromocionDescripcion = dr["PromocionDescripcion"].ToString();
            obj.PromocionDescripcionCliente = dr["PromocionDescripcionCliente"].ToString();
            obj.PromocionValor = Convert.ToDecimal(dr["PromocionValor"].ToString());
            obj.PromocionPorc = Convert.ToBoolean(dr["PromocionPorc"].ToString());
            obj.PromocionEstado = Convert.ToBoolean(dr["PromocionEstado"].ToString());

            return obj;
        }





        public BE.Promocion ObtenerHijos(IEnumerable<decimal> llaves)
        {
            BE.Promocion obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT {0}
                                            FROM Promocion prom  where prom.PromocionId in {1}", campos("prom"), this.ConcatenarLlaves(llaves));
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





        public List<BE.Promocion> Lista_ObtenerHijos(IEnumerable<decimal> llaves, string lang)
        {
            List<BE.Promocion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {

                string sql = String.Format(@"SELECT {0}
                                            FROM Promocion prom  where prom.PromocionId in {1}", campos("prom"), this.ConcatenarLlaves(llaves), lang);
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

        public int Listado_Usuarios_Con_Promocion(decimal PersonaId)
        {
            int gh = 0;
            List<BE.Promocion> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            PromocionDetallePersona promocionDetallePersona = new PromocionDetallePersona("cadenaCnx");
            Persona persona = new Persona("cadenaCnx");

  
            try
            {

                string sql = String.Format(@"SELECT {0} FROM Promocion prom  where prom.PromocionEstado = 1", campos("prom")) ;
                DataRow[] dr = conx.ObtenerFilas(sql);
                if (dr != null)
                {
                    obj = CargarBE(dr);
                }
                foreach (var promocion in obj)
                {
                    string sql1 = "";
                    DataRow dr1;
                    ClaseConexion conx1 = new ClaseConexion(cadenaConexion);
                    decimal PromocionId = promocion.PromocionId;
                    bool bolOk = false;
                   
                    switch (PromocionId.ToString())
                    {
                        case "1":
                            sql1 = String.Format(@"select {0} from requiereservicio re 
                                             inner join persona per on per.personaid = re.personaid 
                                             inner join servasig sg on sg.requiereservicioid = re.requiereservicioid 
                                             where  re.PersonaId NOT IN(SELECT  ppp.PersonaId  FROM PersonalPreuba ppp) 
                                             and  re.estadoreqservid in (2) and  sg.StatusServAsigId in(3,4)  and re.personaid = {1}
                                             group by {0} 
                                             HAVING count(per.personaid) > 1", persona.campos ("per"), Convert.ToInt32(PersonaId));

                            dr1 = conx1.ObtenerFila(sql1);
                            if (dr1 != null)
                            {


                                List<BE.PromocionDetallePersona> lista = promocionDetallePersona.ExistePersona(PersonaId, PromocionId);

                                if (lista.Count == 0)
                                {

                                     BE.PromocionDetallePersona obj45 = new BE.PromocionDetallePersona();
                                     obj45.TipoEstado = BE.TipoEstado.Insertar;
                                     obj45.PromocionId = PromocionId;
                                     obj45.PersonaId = PersonaId;
                                     obj45.FechaInsercion = DateTime.Now;
                                     obj45.Estado = true;
                                     obj45.Valor = 0;
                                     bolOk = promocionDetallePersona.Actualizar(ref obj45);
                                    gh = gh + 1;
                                }
                                





                            }
                            sql1 = "";

                            break;
                        case "2":


                            sql1 = String.Format(@"select {0} from requiereservicio re
inner join persona per on per.personaid= re.personaid
inner join servasig sg on sg.requiereservicioid != re.requiereservicioid
where  re.PersonaId NOT IN(SELECT  ppp.PersonaId  FROM PersonalPreuba ppp) 
and    re.estadoreqservid in (3,4) and sg.StatusServAsigId in(3,4) 
 and re.personaid = {1}
group by {0} 
EXCEPT
select  {0}  from requiereservicio re
inner join persona per on per.personaid= re.personaid
inner join servasig sg on sg.requiereservicioid = re.requiereservicioid
where  re.PersonaId NOT IN(SELECT  ppp.PersonaId  FROM PersonalPreuba ppp) 
and    re.estadoreqservid in (2,1) and sg.StatusServAsigId in(3,4) 
 and re.personaid = {1}
group by  {0} 
", persona.campos("per"), Convert.ToInt32(PersonaId));


                            dr1 = conx1.ObtenerFila(sql1);
                            if (dr1 != null)
                            {
                                List<BE.PromocionDetallePersona> lista = promocionDetallePersona.ExistePersona(PersonaId, PromocionId);

                                if (lista.Count == 0)
                                {

                                    BE.PromocionDetallePersona obj45 = new BE.PromocionDetallePersona();
                                    obj45.TipoEstado = BE.TipoEstado.Insertar;
                                    obj45.PromocionId = PromocionId;
                                    obj45.PersonaId = PersonaId;
                                    obj45.FechaInsercion = DateTime.Now;
                                    obj45.Estado = true;
                                    obj45.Valor = 0;
                                    bolOk = promocionDetallePersona.Actualizar(ref obj45);
                                    gh = gh + 1;
                                }

                            }

                            sql1 = "";
                            break;

                    }

                }

               

              
            }


            catch (Exception ex)
            {
                gh = 1000;
                throw ex;
            }
            return gh;
        }



    }
}
