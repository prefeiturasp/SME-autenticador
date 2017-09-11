﻿using System.Configuration;

namespace Autenticador.Web.WebProject
{
    public class IdentitySettingsConfig
    {
        private static IdentitySettings idsSettings = null;

        public static string Msg { get; private set; } = string.Empty;
    
        public static IdentitySettings IDSSettings
        {
            get
            {
                if (idsSettings == null)
                {                    
                    idsSettings = ConfigureIdentitySettingsWebConfig();
                    return idsSettings;
                }
                return idsSettings;
            }
        }

        private static IdentitySettings ConfigureIdentitySettingsWebConfig()
        {
            try
            {
                IdentitySettings settings = new IdentitySettings
                {
                    Cookies_AuthenticationType = ConfigurationManager.AppSettings["Cookies_AuthenticationType"],
                    Cookies_CookieName = ConfigurationManager.AppSettings["Cookies_CookieName"],
                    Cookies_LoginPath = ConfigurationManager.AppSettings["Cookies_LoginPath"],
                    Cookies_CookieDomain = ConfigurationManager.AppSettings["Cookies_CookieDomain"],
                    AuthenticationType = ConfigurationManager.AppSettings["AuthenticationType"],
                    Authority = ConfigurationManager.AppSettings["Authority"],
                    ClientId = ConfigurationManager.AppSettings["ClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
                    EndpointUserInfo = ConfigurationManager.AppSettings["EndpointUserInfo"],
                    RedirectUri = ConfigurationManager.AppSettings["RedirectUri"],
                    ResponseType = ConfigurationManager.AppSettings["ResponseType"],
                    Scope = ConfigurationManager.AppSettings["Scope"],
                    SignInAsAuthenticationType = ConfigurationManager.AppSettings["SignInAsAuthenticationType"],
                    LogoutUrlAVA = ConfigurationManager.AppSettings["LogoutUrlAVA"]
                };

                if (!settings.IsValid)
                {
                    Msg = "Verifique as configurações no Web.condig referente ao autenticador. Ex: ClientId e Authority.";
                }
                else
                {
                    return settings;
                }
            }
            catch (System.Exception)
            {
                Msg = "Ocorreu um erro ao ler as configurações do autenticador no Web.config. Veja o log de erros do sistema.";
            }

            return null;
        }
        
    }


    public class IdentitySettings
    {
        public IdentitySettings() { }

        public string Cookies_AuthenticationType { get; set; }
        public string Cookies_CookieName { get; set; }
        public string Cookies_LoginPath { get; set; }
        public string Cookies_CookieDomain { get; set; }
        public string AuthenticationType { get; set; }
        public string SignInAsAuthenticationType { get; set; }
        public string Authority { get; set; }
        public string RedirectUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string ResponseType { get; set; }
        public string EndpointUserInfo { get; set; }
        public string LogoutUrlAVA { get; set; }

        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.ClientId)
                    && !string.IsNullOrWhiteSpace(this.Authority))
                {
                    return true;
                }
                return false;
            }
        }
    }
}