using System;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml;
using Autenticador.BLL;
using CoreLibrary.SAML20;
using CoreLibrary.SAML20.Bindings;
using CoreLibrary.SAML20.Configuration;
using CoreLibrary.SAML20.Schemas.Core;
using CoreLibrary.SAML20.Schemas.Protocol;
using CoreLibrary.Validation.Exceptions;
using Autenticador.Web.WebProject.Authentication;
using Autenticador.Web.WebProject;

namespace Autenticador.Web.WebProject
{
    public class Login : MotherPage, IHttpHandler, IRequiresSessionState
    {
        #region Métodos

        #region IHttpHandler Members

        public new void ProcessRequest(HttpContext context)
        {
            try
            {

                if (!UserIsAuthenticated())
                {
                    string provider = "cliauth";
                    Context.GetOwinContext().Authentication.Challenge(provider);
                }
                else
                    Context.Response.Redirect("~/Sistema.aspx", false);
            }
            catch (ValidationException ex)
            {
                ErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                ErrorMessage("Não foi possível atender a solicitação.");
            }
        }

        #endregion IHttpHandler Members

        /// <summary>
        /// Cria um html com a mensagem de erro e um botão para voltar.
        /// </summary>
        /// <param name="message">Mensagem de erro.</param>
        private void ErrorMessage(string message)
        {
            // TODO: Verificar url de retorno.
            UtilBO.CreateHtmlFormMessage
                (
                    this.Context.Response.Output
                    , "SAML SSO"
                    , UtilBO.GetErroMessage(message + "<br />Clique no botão voltar e tente novamente.", UtilBO.TipoMensagem.Erro)
                    , string.Concat(__SessionWEB.UrlSistemaAutenticador, "/Sistema.aspx")
                 );
        }

        /// <summary>
        /// Armazena dados do sistema emissor do Request em Cookie de sistemas autenticados
        /// </summary>
        private static void AddSAMLCookie(HttpContext context)
        {
            HttpCookie cookie = context.Request.Cookies["CoreSAMLProvider"];
            if (cookie == null)
            {
                cookie = new HttpCookie("CoreSAMLProvider");
                context.Response.Cookies.Add(cookie);
            }

            // Armazena no Cookie que está logado no Evesp.
            cookie.Values["EvespLoggedIn"] = "true";
            // Atualiza dados do Cookie.
            context.Response.Cookies.Set(cookie);
        }

        #endregion Métodos
    }
}
