using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BC
{
   public  class envioCorreo:BCEntidad
    {
        private string campos(string prefijo = "ec")
        {
            string strCampos = String.Format(@"{0}.PersonaCorreo,{0}.Subject,{0}.Body,{0}.Fecha,{0}.TipoCorreo,{0}.Estado,{0}.Descripcion"                    , prefijo);
            return strCampos;
        }
        public envioCorreo() : base()
        {
        }

        public envioCorreo(string cadConx) : base(cadConx)
        {

        }
        public ClaseConexion dbConexion { get; set; }
      

        public Boolean Actualizar(ref BE.envioCorreo BEObj, Boolean isTransaccion = false)
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
                        strSql = @"UPDATE dbo.envioCorreo SET  Estado=@Estado
    where PersonaCorreo=@PersonaCorreo and Subject1=@Subject1 and Body=@Body and Id=@Id and TipoCorreo=@TipoCorreo ";
                        break;
                    case BE.TipoEstado.Insertar:
                        strSql = @"insert into envioCorreo(PersonaCorreo,Subject1,Body,Fecha,TipoCorreo,Estado,Descripcion,Id)
	values(@PersonaCorreo,@Subject1,@Body,@Fecha,@TipoCorreo,@Estado,@Descripcion,@Id)";
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
                        BEObj.Id = System.Convert.ToInt32(conx.ObtenerValor("select isnull(max(Id),0) + 1 from dbo.envioCorreo with (nolock);"));

                    }
                    conx.CrearComando(strSql);
                    conx.AsignarParametro("@PersonaCorreo", BEObj.PersonaCorreo);
                    conx.AsignarParametro("@Subject1", BEObj.Subject1);
                    conx.AsignarParametro("@Body", BEObj.Body);
                    conx.AsignarParametro("@Fecha", BEObj.Fecha);
                    conx.AsignarParametro("@TipoCorreo", BEObj.TipoCorreo);
                    conx.AsignarParametro("@Estado", BEObj.Estado);
                    conx.AsignarParametro("@Id", BEObj.Id);
                    if ((BEObj.Descripcion == "") )
                    {

                        conx.AsignarParametro("@Descripcion", DBNull.Value);
                    }
                    else
                    {
                        conx.AsignarParametro("@Descripcion", BEObj.Descripcion);
                    }

                   

                    conx.EjecutarComando();

                    if (!isTransaccion)
                    {
                        conx.ConfirmarTransaccion();
                        conx.Desconectar();
                    }

                    bolOk = true;
                }
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


        public List<BE.envioCorreo> CargarBE(DataRow[] dr)
        {
            List<BE.envioCorreo> lst = new List<BE.envioCorreo>();
            foreach (var item in dr)
            {
                lst.Add(CargarBE(item));
            }
            return lst;
        }
        public BE.envioCorreo CargarBE(DataRow dr)
        {
            BE.envioCorreo obj = new BE.envioCorreo();
            obj.PersonaCorreo =(dr["PersonaCorreo"].ToString());
            obj.Subject1 =(dr["Subject1"].ToString());
            obj.Body = dr["Body"].ToString();
            obj.Fecha = Convert.ToDateTime(dr["Fecha"].ToString());
            obj.TipoCorreo = Convert.ToString(dr["TipoCorreo"].ToString());
            obj.Estado = Convert.ToString(dr["Estado"].ToString());
            obj.Descripcion = (dr["Descripcion"].ToString());
            obj.Id = Convert.ToInt32((dr["Id"].ToString()));


            return obj;


        }

        public List<BE.envioCorreo> ListadoEnvioCorreo(string TipoCorreo, params Enum[] relaciones)
        {
            List<BE.envioCorreo> obj = null;
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            try
            {
                string sql = String.Format(@"SELECT * FROM dbo.envioCorreo ec with(nolock) 
                                            where ec.TipoCorreo = '{0}' and ec.Estado='Pendiente' ", TipoCorreo);
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
        public bool actualizarEnvioCorreo(BE.envioCorreo envioCorreo)
        {
            bool bolOk = false;
            envioCorreo.TipoEstado = BE.TipoEstado.Modificar;
             bolOk =  Actualizar(ref envioCorreo, false);
            return bolOk;
        }
        public int ExisteId(string RequiereServicioId)
        {
            ClaseConexion conx = new ClaseConexion(cadenaConexion);
            string sql = String.Format(@"SELECT count(Id) FROM dbo.envioCorreo ec with(nolock) 
 where ec.Descripcion = '{0}'  ", RequiereServicioId);
            int existe = Convert.ToInt32(conx.ObtenerValor(sql));
            return existe;
        }
    }
}
