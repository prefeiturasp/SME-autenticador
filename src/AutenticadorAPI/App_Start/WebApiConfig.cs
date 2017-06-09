using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Autenticador.BLL;
using Autenticador.Entities;
using CoreLibrary.Security.Cryptography;

namespace AutenticadorAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Autenticação Basic, refer to: http://msdn.microsoft.com/pt-br/library/dn376307.aspx
            config.MessageHandlers.Add(new AuthenticationHandler());
        }
    }

    public class AuthenticationHandler : DelegatingHandler
    {

        private const string SCHEME = "Basic";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (IsValid(request))
            {
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                return Task.Factory.StartNew(() =>
                {
                    return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Credenciais inválidas.");
                });
            }
        }

        private static bool IsValid(HttpRequestMessage request)
        {
            var header = request.Headers;
            if (header.Authorization != null && header.Authorization.Scheme.Equals(SCHEME))
            {
                var credentials = header.Authorization.Parameter;
                if (!string.IsNullOrWhiteSpace(credentials))
                {
                    var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));

                    string[] split = decodedCredentials.Split(':');
                    string username = split[0].Trim();
                    string password = split[1].Trim();

                    CFG_UsuarioAPI userAPI = CFG_UsuarioAPIBO.SelecionaAtivos().FirstOrDefault(p => p.uap_username.Equals(username));
                    if (userAPI != null)
                    {
                        return userAPI.uap_password.Equals(password);
                    }
                }
            }

            return false;
        }
    }
}
