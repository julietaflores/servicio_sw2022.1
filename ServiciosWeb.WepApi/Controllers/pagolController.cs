using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiciosWeb.WepApi.Models;
using ServiciosWeb.WepApi.bo.com.linkser.lnksrvssaup2;
using seguridad.criptografia_publica;
using System.Threading.Tasks;
using ServiciosWeb.Datos.Modelo;
using System.IO;
using System.Data;
using System.Data.SqlClient;

using System.Text;

using System.Web.Http.Description;
using System.Net.Http.Headers;
using ServiciosWeb.WepApi.Models.Entities;
using System.Globalization;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;


namespace ServiciosWeb.WepApi.Controllers
{
    public class pagolController : ApiController
    {

        ServiciosWeb.WepApi.bo.com.linkser.lnksrvssaup2.ServiciosEcommeLNK SE_LNK1 = new ServiciosWeb.WepApi.bo.com.linkser.lnksrvssaup2.ServiciosEcommeLNK();

        public pagolController()
        {


        }


        [HttpGet]
        [Route("api/get_reto")]
        public string get_reto()
        {
            string resultado = SE_LNK1.getReto();
            //    return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return resultado;
        }

        [HttpGet]
        [Route("api/setregistrar")]

        public async Task<Respuesta> setregistrar()
        {
            Respuesta resp = new Respuesta();
            string emisor = "1041";
            string llaveRegistro = "ServicewebEC2020";
            seguridad.criptografia_publica.Claves lnk = new seguridad.criptografia_publica.Claves("", @"c:\clave_pki_publica\publica.rsa");

            byte[] cEmisor = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(emisor, lnk.getClavePublica());
            //string bitString = System.Text.Encoding.UTF8.GetString();

            byte[] cLlaveRegistro = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(llaveRegistro, lnk.getClavePublica());


            seguridad.criptografia_publica.Claves clavesEmisor;

            string rutaPublicaEmisor = @"c:\claves_pki_emisor_publica\publica.rsa";

            string rutaPrivadaEmisor = @"c:\claves_pki_emisor_privada\privada.rsa";

            clavesEmisor = new seguridad.criptografia_publica.Claves(rutaPrivadaEmisor, rutaPublicaEmisor);

            //	serviciov2.bo.com.linkser.lnksrvssaup2.ServiciosEcommeLNK SE_LNK1 = new serviciov2.bo.com.linkser.lnksrvssaup2.ServiciosEcommeLNK();

            //serviciov2.bo.com.linkser.lnksrvssaup2.ServiciosEcommeLNK SE_LNK = new serviciov2.bo.com.linkser.lnksrvssaup2.ServiciosEcommeLNK();

            string resultado = SE_LNK1.setRegistrar(System.Text.Encoding.UTF8.GetString(cEmisor), System.Text.Encoding.UTF8.GetString(clavesEmisor.getClavePublicaBytes()), System.Text.Encoding.UTF8.GetString(cLlaveRegistro));

            resp.estado = 1;
            resp.valor = resultado;
            resp.mensaje = "registrado";
            return resp;
            //  return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        /*
        public JsonResult me_set_autho_ecomm()
        {

         
            seguridad.criptografia_publica.Claves lnk = new seguridad.criptografia_publica.Claves("", @"c:\clave_pki_publica\publica.rsa");

            string emisor;
            byte[] cEmisor;
            string secuencia;
            string comercio;
            string terminal;
            string tarjeta;
            byte[] ctarjeta;
            string nombreCliente;
            string fechaExpiracion;
            byte[] cfecha;
            string cvv2;
            byte[] bcvv2;
            string monto;
            string moneda;
            int fechaws;
            string horaws;
            string reto;
            string firma;
            byte[] firmaDigital;
            string llaveRegistro;
            byte[] cllaveRegistro;
            string rutaPublicaLNK;
            string rutaPublicaEmisor;
            string rutaPrivadaEmisor;
            seguridad.criptografia_publica.Claves clavesEmisor;
            byte[] bdataFirmada;

            secuencia = "123459";
            comercio = "0375127";
            terminal = "02179999";

            nombreCliente = "julieta flores";


            monto = "000000001000";
            moneda = "068";
            fechaws = 20201005;
            horaws = "114205";
            reto = get_reto1();

            rutaPublicaLNK = @"c:\clave_pki_publica\publica.rsa";
            rutaPublicaEmisor = @"c:\claves_pki_emisor_publica\publica.rsa";
            rutaPrivadaEmisor = @"c:\claves_pki_emisor_privada\privada.rsa";

            //System.Net.ServicePointManager.CertificatePolicy = New MyPolicy()
            //'la clave publica de linkser.
            lnk = new seguridad.criptografia_publica.Claves("", rutaPublicaLNK);
            // '   Codigo del Emisor.
            emisor = "1041";
            cEmisor = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(emisor, lnk.getClavePublica());
            // 'tarjeta
            tarjeta = "4560160018692018";
            ctarjeta = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(tarjeta, lnk.getClavePublica());
            //  'fecha expiracion
            fechaExpiracion = "202201";
            cfecha = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(fechaExpiracion, lnk.getClavePublica());
            //  ' cvv2
            cvv2 = "540";
            bcvv2 = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(cvv2, lnk.getClavePublica());
            //'  Llave de registro del Emisor.
            llaveRegistro = "ServicewebEC2020";
            cllaveRegistro = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(llaveRegistro, lnk.getClavePublica());
            //  '  Claves publica y privada del emisor.
            //  '   - La clave publica es la que se le envia a LINKSER, para que este "cifre" informacion que enviara al Emisor.
            //  '   - Con la clave privada es la que se utilizara para "descifrar" informacion enviada por LINKSER.
            clavesEmisor = new seguridad.criptografia_publica.Claves(rutaPrivadaEmisor, rutaPublicaEmisor);
            //  ' creación de la firma digital
            firma = emisor + reto;
            bdataFirmada = System.Text.Encoding.UTF8.GetBytes(firma);
            firmaDigital = seguridad.criptografia_publica.Firma.firmaDigital(bdataFirmada, clavesEmisor.getClavePrivada());
            Object objeto;
            objeto = SE_LNK1.me_set_Autho_Ecomm(System.Text.Encoding.UTF8.GetString(cEmisor),
                                        secuencia, comercio, terminal, System.Text.Encoding.UTF8.GetString(ctarjeta),
                                        nombreCliente,
                                        System.Text.Encoding.UTF8.GetString(cfecha),
                                        System.Text.Encoding.UTF8.GetString(bcvv2), monto,
                                        moneda, fechaws, horaws, reto,
                                        System.Text.Encoding.UTF8.GetString(firmaDigital),
                                        System.Text.Encoding.UTF8.GetString(cllaveRegistro));
            return new JsonResult { Data = objeto, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        */



        public string get_reto1()
        {
            string resultado = SE_LNK1.getReto();
            return resultado;
        }

        [HttpGet]
        [Route("api/me_set_autho_ecomm_v_sw")]
        public async Task<Respuesta> me_set_autho_ecomm_v_sw(string secuencia, string nombreCliente, string tarjeta, string fechaExpiracion, string cvv2, string monto, int fechaws, string horaws)
        {
            Respuesta resp = new Respuesta();
            seguridad.criptografia_publica.Claves lnk = new seguridad.criptografia_publica.Claves("", @"c:\clave_pki_publica\publica.rsa");
            string emisor;
            byte[] cEmisor;
            //   string secuencia;
            string comercio;
            string terminal;
            //   string tarjeta;
            byte[] ctarjeta;
            //    string nombreCliente;
            // string fechaExpiracion;
            byte[] cfecha;
            //     string cvv2;
            byte[] bcvv2;
            //  string monto;
            string moneda;
            //    int fechaws;
            //   string horaws;
            string reto;
            string firma;
            byte[] firmaDigital;
            string llaveRegistro;
            byte[] cllaveRegistro;
            string rutaPublicaLNK;
            string rutaPublicaEmisor;
            string rutaPrivadaEmisor;
            seguridad.criptografia_publica.Claves clavesEmisor;
            byte[] bdataFirmada;

            /*  secuencia = "123456";
              tarjeta = "4560160018692018";
              nombreCliente = "julieta flores";
              fechaExpiracion = "202201";
            cvv2 = "540";
              monto = "000000001000";
              fechaws = 20201005;
              horaws = "114205";



              */
            comercio = "0375127";
            terminal = "02179999";

            moneda = "068";
            reto = get_reto1();

            rutaPublicaLNK = @"c:\clave_pki_publica\publica.rsa";
            rutaPublicaEmisor = @"c:\claves_pki_emisor_publica\publica.rsa";
            rutaPrivadaEmisor = @"c:\claves_pki_emisor_privada\privada.rsa";
            lnk = new seguridad.criptografia_publica.Claves("", rutaPublicaLNK);

            emisor = "1041";
            cEmisor = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(emisor, lnk.getClavePublica());


            ctarjeta = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(tarjeta, lnk.getClavePublica());

            cfecha = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(fechaExpiracion, lnk.getClavePublica());
            bcvv2 = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(cvv2, lnk.getClavePublica());

            llaveRegistro = "ServicewebEC2020";
            cllaveRegistro = seguridad.criptografia_publica.CriptografiaPublica.cipherBloques(llaveRegistro, lnk.getClavePublica());
            clavesEmisor = new seguridad.criptografia_publica.Claves(rutaPrivadaEmisor, rutaPublicaEmisor);

            firma = emisor + reto;
            bdataFirmada = System.Text.Encoding.UTF8.GetBytes(firma);
            firmaDigital = seguridad.criptografia_publica.Firma.firmaDigital(bdataFirmada, clavesEmisor.getClavePrivada());
            Object objeto;
            objeto = SE_LNK1.me_set_Autho_Ecomm(System.Text.Encoding.UTF8.GetString(cEmisor),
                                        secuencia, comercio, terminal, System.Text.Encoding.UTF8.GetString(ctarjeta),
                                        nombreCliente,
                                        System.Text.Encoding.UTF8.GetString(cfecha),
                                        System.Text.Encoding.UTF8.GetString(bcvv2), monto,
                                        moneda, fechaws, horaws, reto,
                                        System.Text.Encoding.UTF8.GetString(firmaDigital),
                                        System.Text.Encoding.UTF8.GetString(cllaveRegistro));

            resp.estado = 1;
            resp.valor = objeto;
            resp.mensaje = "resultado";
            //  return new JsonResult { Data = objeto, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return resp;
        }

    }
}
