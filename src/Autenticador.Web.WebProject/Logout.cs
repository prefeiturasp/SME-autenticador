using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using CoreLibrary.SAML20.Bindings;
using CoreLibrary.SAML20.Configuration;
using CoreLibrary.SAML20;
using CoreLibrary.SAML20.Schemas.Protocol;
using CoreLibrary.SAML20.Schemas.Core;
using System.IO;
using System.IO.Compression;
using Autenticador.Entities;
using Autenticador.BLL;
using CoreLibrary.Validation.Exceptions;

namespace Autenticador.Web.WebProject
{
    /// <summary>
    /// You will need to configure this handler in the web.config file of your
    /// web and register it with IIS before being able to use it. For more information
    /// see the following link: http://go.microsoft.com/?linkid=8101007
    /// </summary>
    public class Logout : MotherPage, IHttpHandler, IRequiresSessionState
    {
        #region Propriedades

        private ResponseType SAMLResponse { get; set; }
        private LogoutRequestType SAMLRequest { get; set; }

        #endregion Propriedades

        #region Métodos

        #region IHttpHandler Members

        public new bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public new void ProcessRequest(HttpContext context)
        {
            try
            {
                // ***** REQUEST *****
                if (!String.IsNullOrEmpty(context.Request[HttpBindingConstants.SAMLRequest]))
                {
                    // Recupera LogoutRequest
                    StringBuilder result = new StringBuilder();

                    byte[] encoded = Convert.FromBase64String(HttpUtility.UrlDecode(context.Request[HttpBindingConstants.SAMLRequest]).Replace(" ", "+"));
                    MemoryStream memoryStream = new MemoryStream(encoded);
                    using (DeflateStream stream = new DeflateStream(memoryStream, CompressionMode.Decompress))
                    {
                        StreamReader reader = new StreamReader(new BufferedStream(stream), Encoding.GetEncoding("iso-8859-1"));
                        reader.Peek();
                        result.Append(reader.ReadToEnd());
                        stream.Close();
                    }
                    SAMLRequest = SAMLUtility.DeserializeFromXmlString<LogoutRequestType>(result.ToString());

                    AtribuirSessionSisIDLogout(((NameIDType)SAMLRequest.Item).Value);

                    // Criação e configuração LogoutResponse
                    SAMLResponse = new ResponseType();
                    CreateSAMLResponse();
                }
                // ***** RESPONSE *****
                else if (!String.IsNullOrEmpty(context.Request[HttpBindingConstants.SAMLResponse]))
                {
                    throw new NotImplementedException();
                }
                else
                {
                    AtribuirSessionSisIDLogout(String.Concat(ApplicationWEB._DiretorioVirtual, "Logout.ashx"));

                    __SessionWEB.SistemasRequestLogout++;

                    //Se existir link de retorno na primeira vez, significa que o sistema que iniciou o processo de logout
                    //enviou na queryString o endereço que deve ser redirecionado após terminar todo o processo.
                    if (!String.IsNullOrEmpty(context.Request["SistemaUrlLogout"]) && __SessionWEB.SistemasRequestLogout == 1)
                    {
                        __SessionWEB.SistemaUrlLogout_QueryString = context.Request["SistemaUrlLogout"];
                    }

                    // Verifica se existe sistemas autenticados em Cookie para realizar logout
                    HttpCookie cookie = context.Request.Cookies["SistemasLogged"];

                    if (cookie != null)
                    {
                        if (cookie.Values.AllKeys.Count(p => p != null) > 0)
                        {
                            SYS_Sistema entitySistema = SYS_SistemaBO.GetEntity(new SYS_Sistema { sis_id = Convert.ToInt32(cookie.Values.AllKeys.First()) });
                            HttpContext.Current.Response.Redirect(entitySistema.sis_caminhoLogout, false);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            RedirecionarParaLogin(context);
                        }
                    }
                    else
                    {
                        RedirecionarParaLogin(context);
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

        /// <summary>
        /// Atribui o sis_id que efetuou o logout à session, caso o login seja feito diretamente através da query string
        /// </summary>
        /// <param name="caminhoLogout">Caminho de logout do sistema que efetuou a solicitação</param>
        private void AtribuirSessionSisIDLogout(string caminhoLogout)
        {
            if (__SessionWEB.SistemaID_QueryString > 0 && __SessionWEB.SistemaIDLogout_QueryString <= 0)
            {
                SYS_Sistema sistemaLogout = new SYS_Sistema { sis_caminhoLogout = caminhoLogout };
                SYS_SistemaBO.GetSelectBy_sis_caminho(sistemaLogout, SYS_SistemaBO.TypePath.logout);

                __SessionWEB.SistemaIDLogout_QueryString = sistemaLogout.sis_id;
            }
        }

        /// <summary>
        /// Redireciona para a página de login
        /// </summary>
        /// <param name="context">HttpContext</param>
        private void RedirecionarParaLogin(HttpContext context)
        {
            context.Request.GetOwinContext().Authentication.SignOut();
            if (context.Session != null)
                context.Session.Abandon();
        }

        /// <summary>
        /// Verifica se está logado no SAML provider do Autenticador.
        /// Se sim, redireciona para o logout do provider.
        /// </summary>
        /// <param name="context">Contexto http</param>
        /// <returns>Se foi redirecionado para o SAML Provider</returns>
        private static bool VerificaLogoutCoreProvider(HttpContext context)
        {
            context.Request.GetOwinContext().Authentication.SignOut();
            return true;            
        }

        #endregion IHttpHandler Members

        private void ErrorMessage(string message)
        {
            UtilBO.CreateHtmlFormMessage
                (
                    this.Context.Response.Output
                    , "SAML SSO"
                    , UtilBO.GetErroMessage(message + "<br />Clique no botão voltar e tente novamente.", UtilBO.TipoMensagem.Erro)
                    , string.Concat(__SessionWEB.UrlSistemaAutenticador, "/Sistema.aspx")
                 );
        }

        private void CreateSAMLResponse()
        {
            IDProvider config = IDProvider.GetConfig();

            SAMLResponse.ID = SAMLUtility.GenerateID();
            SAMLResponse.Version = SAMLUtility.VERSION;
            SAMLResponse.IssueInstant = DateTime.UtcNow.AddMinutes(10);
            SAMLResponse.InResponseTo = SAMLRequest.ID;

            SAMLResponse.Issuer = new NameIDType();
            SAMLResponse.Issuer.Value = config.id;

            SAMLResponse.Status = new StatusType();
            SAMLResponse.Status.StatusCode = new StatusCodeType();

            // Atualiza Cookie de sistemas autenticados e configura Status
            HttpCookie cookie = this.Context.Request.Cookies["SistemasLogged"];
            if (cookie != null)
            {
                // Carrega a Entidade SYS_Sistema apartir do caminho de logout
                SYS_Sistema entitySistema = new SYS_Sistema { sis_caminhoLogout = ((NameIDType)SAMLRequest.Item).Value };
                if (SYS_SistemaBO.GetSelectBy_sis_caminho(entitySistema, SYS_SistemaBO.TypePath.logout))
                {
                    // Remove o sistema do Cookie
                    cookie.Values.Remove(entitySistema.sis_id.ToString());
                    // Atualiza dados do Cookie
                    this.Context.Response.Cookies.Set(cookie);

                    if (!cookie.Values.AllKeys.Contains(entitySistema.sis_id.ToString()))
                    {
                        SAMLResponse.Status.StatusCode.Value = SAMLUtility.StatusCodes.Success;
                        SAMLResponse.Status.StatusMessage = "A solicitação foi realizada com sucesso.";
                    }
                    else
                    {
                        SAMLResponse.Status.StatusCode.Value = SAMLUtility.StatusCodes.RequestDenied;
                        SAMLResponse.Status.StatusMessage = "Não foi possível atender a solicitação, o sistema emissor da requisição não está autenticado.";
                    }
                }
                else
                {
                    SAMLResponse.Status.StatusCode.Value = SAMLUtility.StatusCodes.RequestDenied;
                    SAMLResponse.Status.StatusMessage = "Não foi possível atender a solicitação, sistema emissor da requisição não está cadastrado corretamente."; ;
                }
            }
            else
            {
                SAMLResponse.Status.StatusCode.Value = SAMLUtility.StatusCodes.RequestDenied;
                SAMLResponse.Status.StatusMessage = "Não foi possível atender a solicitação.";
            }

            HttpPostBinding binding = new HttpPostBinding(SAMLResponse, HttpUtility.UrlDecode(this.Context.Request[HttpBindingConstants.RelayState]));
            binding.SendResponse(this.Context, HttpUtility.UrlDecode(this.Context.Request[HttpBindingConstants.RelayState]), SAMLTypeSSO.logout);
        }

        /// <summary>
        /// Retorna o Id do sistema da query string
        /// </summary>
        protected int GetSistemaID_QueryString(HttpContext context)
        {
            int sis_idQueryString = 0;
            if (!String.IsNullOrEmpty(context.Request.QueryString["sis"]))
            {
                Int32.TryParse(context.Request.QueryString["sis"], out sis_idQueryString);
            }

            //Retorna zero caso o sistema não tenha sido passado por query string
            return sis_idQueryString;
        }

        #endregion Métodos
    }
}
