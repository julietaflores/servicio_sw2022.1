using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;

namespace ServiciosWebPE.Credentials
{
    public class AutorizacionCredencialesToken : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var acceso = ((context.UserName == "admin" && context.Password == "123") || (context.UserName == "user" && context.Password == "userPE!03122019"));
            if (acceso == false)
            {

                context.SetError("Sin acceso", "usuario  pass incorrecto");

            }
            else
            {
                ClaimsIdentity identidad = new ClaimsIdentity(context.Options.AuthenticationType);
                identidad.AddClaim(new Claim(ClaimTypes.Name, context.Password));
                if ((context.UserName == "admin") && (context.Password == "123"))
                {
                    identidad.AddClaim(new Claim(ClaimTypes.Role, "ADMINISTRADOR"));
                    context.Validated(identidad);

                }
                if ((context.UserName == "user") && (context.Password == "userPE!03122019"))
                {
                    identidad.AddClaim(new Claim(ClaimTypes.Role, "USUARIO"));
                    context.Validated(identidad);

                }

            }

        }
    }
}