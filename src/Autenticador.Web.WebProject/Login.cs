using System;
using System.Web;
using System.Web.SessionState;
using Autenticador.BLL;
using System.Linq;
using CoreLibrary.Validation.Exceptions;
using System.Configuration;

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
                    string provider = IdentitySettingsConfig.IDSSettings.AuthenticationType;
                    Context.GetOwinContext().Authentication.Challenge(provider);
                }
                else
                {
                    if (Context.Request.QueryString.AllKeys.Contains("RedirectUrlSAML")
                        && !String.IsNullOrWhiteSpace(Context.Request.QueryString["RedirectUrlSAML"].ToString()))
                    {
                        Context.Response.Redirect(Context.Request.QueryString["RedirectUrlSAML"].ToString(), false);
                    }
                    else
                    {
                        Context.Response.Redirect("~/Sistema.aspx", false);
                    }
                }
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

        #endregion Métodos
    }
}