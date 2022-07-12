using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

using ServiciosWebPE.Credentials;

[assembly: OwinStartup(typeof(ServiciosWebPE.Startup))]

namespace ServiciosWebPE
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            ConfigurarOauth(app);
            app.UseWebApi(config);
        }
        public void ConfigurarOauth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions opcionesautorizacion =
                new OAuthAuthorizationServerOptions()
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/recuperartoken"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                    Provider = new Credentials.AutorizacionCredencialesToken()

                };

            app.UseOAuthAuthorizationServer(opcionesautorizacion);
            OAuthBearerAuthenticationOptions opcionesoauth =
                new OAuthBearerAuthenticationOptions()
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active

                };
            app.UseOAuthBearerAuthentication(opcionesoauth);
        }
    }
}
