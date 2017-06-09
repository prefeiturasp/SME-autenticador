using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

namespace Autenticador.Web.WebProject.Authentication
{
    /// <summary>
    /// Classe para armazenar as configurações do cookie de autenticação.
    /// </summary>
    public static class ConfigCustomAutenticadorCookieAuthentication
    {
        private const string Prefix = "CookiAthenticationOptions:";
        private const string KeyLoginPath = Prefix + "LoginPath";
        private const string KeyCookieName = Prefix + "CookieName";
        private const string KeyCookiePath = Prefix + "CookiePath";
        private const string KeyRedirectUrlLoginExternal = Prefix + "RedirectUrlLoginExternal";
        private const string KeyReturnUrlParameter = "ReturnUrl";
        private const string KeyCookieDomain = Prefix + "CookieDomain";
        private const string KeySlidingExpiration = Prefix + "SlidingExpiration";
        private const string KeyExpireTimespanFromHours = Prefix + "ExpireTimeSpanFromHours";
        private const string KeyExpiresUtcFromHours = Prefix + "ExpiresUtcFromHours";
        private const double KeyExpiresUtcFromHoursDefault = 24;
        private const double KeyExpireTimespanFromHoursDefault = 12;

        /// <summary>
        /// Propriedade para acessar as configurações do web.config
        /// </summary>
        private static System.Collections.Specialized.NameValueCollection AppSettings
        {
            get
            {
                return WebConfigurationManager.AppSettings;
            }
        }

        /// <summary>
        /// Página de login.
        /// </summary>
        public static PathString LoginPath
        {
            get { return new PathString(AppSettings[KeyLoginPath]); }
        }

        /// <summary>
        /// Nome do cookie.
        /// </summary>
        public static string CookieName
        {
            get { return AppSettings[KeyCookieName]; }
        }

        /// <summary>
        /// Pasta para ser utiliza no cookie.
        /// </summary>
        public static string CookiePath
        {
            get { return AppSettings[KeyCookiePath]; }
        }

        /// <summary>
        /// Nome da Url de retorno.
        /// </summary>
        public static string ReturnUrlParameter
        {
            get { return KeyReturnUrlParameter; }
        }

        /// <summary>
        /// Endereço absoluto da página externa para o sistema redicionar
        /// </summary>
        public static string RedirectUrlLoginExternal
        {
            get { return AppSettings[KeyRedirectUrlLoginExternal]; }
        }

        /// <summary>
        /// Intervalo de tempo para revalidar o login.
        /// </summary>
        public static TimeSpan ExpireTimeSpan
        {
            get
            {
                var expiresTimeconfig = AppSettings[KeyExpireTimespanFromHours];
                double expiresTime = 0;

                if (!double.TryParse(expiresTimeconfig, out expiresTime))
                {
                    expiresTime = KeyExpireTimespanFromHoursDefault;
                }
                return TimeSpan.FromHours(expiresTime);
            }
        }

        /// <summary>
        /// Duração do cookie.
        /// </summary>
        public static DateTime ExpiresUtc
        {
            get
            {
                var expiresUtcconfig = AppSettings[KeyExpiresUtcFromHours];
                double expiresUtc = 0;
                if (!double.TryParse(expiresUtcconfig, out expiresUtc))
                {
                    expiresUtc = KeyExpiresUtcFromHoursDefault;
                }
                return DateTime.UtcNow.AddHours(expiresUtc);
            }
        }

        /// <summary>
        /// Domínio do cookie, default é nulo.
        /// </summary>
        public static string CookieDomain
        {
            get { return !string.IsNullOrWhiteSpace(AppSettings[KeyCookieDomain]) ? AppSettings[KeyCookieDomain] : null; }
        }

        /// <summary>
        /// Permite que o tempo de expiração seja postergado conforme as requisições futuras.
        /// </summary>
        public static bool SlidingExpiration
        {
            get
            {
                var slidingConfig = AppSettings[KeySlidingExpiration];
                bool sliding = false;
                bool.TryParse(slidingConfig, out sliding);
                return sliding;
            }
        }
    }

    /// <summary>
    /// Configura as opções da autenticação conforme o sistema necessita.
    /// </summary>
    public class AutenticadorCookieAuthenticationOptions : CookieAuthenticationOptions
    {
        public AutenticadorCookieAuthenticationOptions()
        {
            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            LoginPath = ConfigCustomAutenticadorCookieAuthentication.LoginPath;
            CookieHttpOnly = true;
            CookieName = ConfigCustomAutenticadorCookieAuthentication.CookieName;
            CookiePath = ConfigCustomAutenticadorCookieAuthentication.CookiePath;
            SlidingExpiration = ConfigCustomAutenticadorCookieAuthentication.SlidingExpiration;
            ExpireTimeSpan = ConfigCustomAutenticadorCookieAuthentication.ExpireTimeSpan;
            CookieSecure = CookieSecureOption.SameAsRequest;
            ReturnUrlParameter = ConfigCustomAutenticadorCookieAuthentication.ReturnUrlParameter;
            CookieDomain = ConfigCustomAutenticadorCookieAuthentication.CookieDomain;
            Provider = new CookieAuthenticationProvider
            {
                OnApplyRedirect = ApplyRedirect
            };
        }

        /// <summary>
        /// Método executado no momento do redirecionamento do usuário para a página de login ou para a página externa.
        /// </summary>
        /// <param name="context"></param>
        /// <see cref="http://stackoverflow.com/questions/21275399/login-page-on-different-domain"/>
        private void ApplyRedirect(CookieApplyRedirectContext context)
        {
            if (!string.IsNullOrWhiteSpace(ConfigCustomAutenticadorCookieAuthentication.RedirectUrlLoginExternal))
            {
                Uri absoluteUri;
                if (Uri.TryCreate(context.RedirectUri, UriKind.Absolute, out absoluteUri))
                {
                    var path = PathString.FromUriComponent(absoluteUri);
                    if (path == context.OwinContext.Request.PathBase + context.Options.LoginPath)
                        context.RedirectUri = ConfigCustomAutenticadorCookieAuthentication.RedirectUrlLoginExternal +
                            new QueryString(
                                context.Options.ReturnUrlParameter,
                                context.Request.Uri.AbsoluteUri);
                }
            }
            context.Response.Redirect(context.RedirectUri);
        }
    }

    /// <summary>
    /// Classe com os tipos de declaração customizados.
    /// </summary>
    public class AutenticadorCustomClaimTypes
    {
        public const string GrupoId = "GrupoId";
        public const string EntidadeId = "EntidadeId";
    }
}
