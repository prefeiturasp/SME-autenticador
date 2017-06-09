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

namespace Autenticador.Web.WebProject.SAMLClient
{
    public class Login : MotherPage, IHttpHandler, IRequiresSessionState
    {
        #region Propriedades

        private ResponseType SAMLResponse { get; set; }
        private SAMLAuthnRequest SAMLRequest { get; set; }

        #endregion Propriedades

        #region Métodos

        #region IHttpHandler Members

        public new void ProcessRequest(HttpContext context)
        {
            try
            {
                // ***** RESPONSE *****
                if (!String.IsNullOrEmpty(context.Request[HttpBindingConstants.SAMLResponse]))
                {
                    // Recupera Response
                    string samlresponse = context.Request[HttpBindingConstants.SAMLResponse];
                    XmlDocument doc = new XmlDocument();
                    doc.PreserveWhitespace = true;
                    doc.LoadXml(samlresponse);

                    // Verifica Signature do Response
                    if (XmlSignatureUtils.VerifySignature(doc))
                    {
                        SAMLResponse = SAMLUtility.DeserializeFromXmlString<ResponseType>(doc.InnerXml);
                        if (SAMLResponse.Items.Length > 0)
                        {
                            for (int i = 0; i < SAMLResponse.Items.Length; i++)
                            {
                                if (SAMLResponse.Items[i] is AssertionType)
                                {
                                    NameIDType nameID = null;
                                    AssertionType assertion = (AssertionType)SAMLResponse.Items[i];
                                    for (int j = 0; j < assertion.Subject.Items.Length; j++)
                                    {
                                        if (assertion.Subject.Items[j] is NameIDType)
                                            nameID = (NameIDType)assertion.Subject.Items[j];
                                    }
                                    if (nameID != null)
                                    {
                                        SignHelper.AutenticarUsuarioDaRespostaDoSaml(nameID.Value);
                                    }
                                }

                                // Armazena dados do sistema emissor do Request
                                // em Cookie de sistemas autenticados
                                AddSAMLCookie(context);
                            }
                        }
                        context.Response.Redirect(HttpUtility.UrlDecode(context.Request[HttpBindingConstants.RelayState]), false);
                    }
                    else
                        throw new ValidationException("Não foi possível encontrar assinatura.");
                }
                // ***** REQUEST *****
                else if (!String.IsNullOrEmpty(context.Request[HttpBindingConstants.SAMLRequest]))
                {
                    throw new NotImplementedException();
                }
                else
                {
                    // Carrega as configurações do ServiceProvider
                    ServiceProvider config = ServiceProvider.GetConfig();
                    ServiceProviderEndpoint spend = SAMLUtility.GetServiceProviderEndpoint(config.ServiceEndpoint, SAMLTypeSSO.signon);

                    // Verifica configuração do ServiceProvider para signon
                    if (spend == null)
                        throw new ValidationException("Não foi possível encontrar as configurações do ServiceProvider para signon.");

                    // Verifica se usuário está autenticado, caso não envia um Resquest solicitando autenticação
                    if (!UsuarioWebIsValid())
                    {
                        SAMLRequest = new SAMLAuthnRequest();
                        SAMLRequest.Issuer = config.id;
                        SAMLRequest.AssertionConsumerServiceURL = context.Request.Url.AbsoluteUri;

                        HttpRedirectBinding binding = new HttpRedirectBinding(SAMLRequest, spend.localpath);
                        binding.SendRequest(context, spend.redirectUrl);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(spend.localpath, false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
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