using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ServicioW.ClienteWeb.Facebook
{
    public class FacebookBackChannelHandler:HttpClientHandler

    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
            {
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?acces_token", "&acces_token"));

            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}