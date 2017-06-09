using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using CoreLibrary.SAML20;
using CoreLibrary.SAML20.Bindings;
using CoreLibrary.SAML20.Schemas.Core;
using CoreLibrary.SAML20.Schemas.Protocol;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using CoreLibrary.SAML20.Configuration;
using CoreLibrary.Validation.Exceptions;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.IO;
using System.Web.SessionState;
using System.IO.Compression;
using Autenticador.Web.WebProject.Authentication;

namespace Autenticador.Web.WebProject
{
    public class SignonService : MotherPage, IHttpHandler, IRequiresSessionState
    {
        private RetornoLoginJSON retorno = new RetornoLoginJSON();

        #region Propriedades

        private ResponseType SAMLResponse { get; set; }
        private SAMLAuthnRequest SAMLRequest { get; set; }

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
            #region Trace

            // Write a trace message
            if (Trace.IsEnabled)
            {
                if (context.User != null)
                {
                    Trace.Write("context.User", context.User.ToString());
                    Trace.Write("context.User.Identity", context.User.Identity.ToString());
                    if (context.User.Identity is FormsIdentity)
                    {
                        Trace.Write("context.User.Identity.IsAuthenticated", context.User.Identity.IsAuthenticated.ToString());
                        if (context.User.Identity.IsAuthenticated)
                        {
                            FormsIdentity id = (FormsIdentity)context.User.Identity;
                            Trace.Write("FormsIdentity.Ticket.Name", id.Ticket.Name);
                            Trace.Write("FormsIdentity.Ticket.IssueDate", id.Ticket.IssueDate.ToString());
                        }
                    }
                }
                else
                {
                    Trace.Write("context.User", "NULL");
                }
            }

            #endregion Trace

            try
            {
                //// Verifica autenticação
                if (UserIsAuthenticated())
                {
                    if ((!String.IsNullOrEmpty(context.Request[HttpBindingConstants.SAMLRequest])) &&
                        (!String.IsNullOrEmpty(context.Request[HttpBindingConstants.RelayState])))
                    {
                        // Recupera Request
                        SAMLRequest = new SAMLAuthnRequest();
                        string request = HttpUtility.UrlDecode(context.Request[HttpBindingConstants.SAMLRequest]);
                        SAMLRequest.UnPackRequest(request.Replace(" ", "+"));

                        // Criação e configuração do Response
                        SAMLResponse = new ResponseType();
                        CreateSAMLResponse(context);

                        // Armazena dados do sistema emissor do Request
                        // em Cookie de sistemas autenticados
                        AddSAMLCookie(context);
                    }
                    else
                        throw new ValidationException("Não foi possível atender a solicitação, requisição inválida.");
                }
                else
                    throw new ValidationException("Não foi possível atender a solicitação, o usuário não tem permissão de acesso ao sistema.");
            }
            catch (ValidationException ex)
            {
                retorno.Mensagem = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
                UtilBO.MessageJSON(this.Context.Response.Output, retorno);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                retorno.Mensagem = UtilBO.GetErroMessage("Não foi possível atender a solicitação.", UtilBO.TipoMensagem.Erro);
                UtilBO.MessageJSON(this.Context.Response.Output, retorno);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        #endregion IHttpHandler Members

        private void CreateSAMLResponse(HttpContext context)
        {
            FormsIdentity id = null;

            if (context.User != null)
                if (context.User.Identity.IsAuthenticated)
                    if (context.User.Identity is FormsIdentity)
                        id = (FormsIdentity)context.User.Identity;

            DateTime notBefore = (id != null ? id.Ticket.IssueDate.ToUniversalTime() : DateTime.UtcNow);
            DateTime notOnOrAfter = (id != null ? id.Ticket.Expiration.ToUniversalTime() : DateTime.UtcNow.AddMinutes(20));

            IDProvider config = IDProvider.GetConfig();

            SAMLResponse.Status = new StatusType();
            SAMLResponse.Status.StatusCode = new StatusCodeType();
            SAMLResponse.Status.StatusCode.Value = SAMLUtility.StatusCodes.Success;

            AssertionType assert = new AssertionType();
            assert.ID = SAMLUtility.GenerateID();
            assert.IssueInstant = DateTime.UtcNow.AddMinutes(10);

            assert.Issuer = new NameIDType();
            assert.Issuer.Value = config.id;

            SubjectConfirmationType subjectConfirmation = new SubjectConfirmationType();
            subjectConfirmation.Method = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
            subjectConfirmation.SubjectConfirmationData = new SubjectConfirmationDataType();
            subjectConfirmation.SubjectConfirmationData.Recipient = SAMLRequest.Issuer;
            subjectConfirmation.SubjectConfirmationData.InResponseTo = SAMLRequest.Request.ID;
            subjectConfirmation.SubjectConfirmationData.NotOnOrAfter = notOnOrAfter;

            NameIDType nameID = new NameIDType();
            nameID.Format = SAMLUtility.NameIdentifierFormats.Transient;
            nameID.Value = (id != null ? id.Name : SignHelper.FormatarUserNameDoCookie(this.__SessionWEB.__UsuarioWEB.Usuario));

            assert.Subject = new SubjectType();
            assert.Subject.Items = new object[] { subjectConfirmation, nameID };

            assert.Conditions = new ConditionsType();
            assert.Conditions.NotBefore = notBefore;
            assert.Conditions.NotOnOrAfter = notOnOrAfter;
            assert.Conditions.NotBeforeSpecified = true;
            assert.Conditions.NotOnOrAfterSpecified = true;

            AudienceRestrictionType audienceRestriction = new AudienceRestrictionType();
            audienceRestriction.Audience = new string[] { SAMLRequest.Issuer };
            assert.Conditions.Items = new ConditionAbstractType[] { audienceRestriction };

            AuthnStatementType authnStatement = new AuthnStatementType();
            authnStatement.AuthnInstant = DateTime.UtcNow;
            authnStatement.SessionIndex = SAMLUtility.GenerateID();

            authnStatement.AuthnContext = new AuthnContextType();
            authnStatement.AuthnContext.Items =
                new object[] { "urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport" };

            authnStatement.AuthnContext.ItemsElementName =
                new ItemsChoiceType5[] { ItemsChoiceType5.AuthnContextClassRef };

            StatementAbstractType[] statementAbstract = new StatementAbstractType[] { authnStatement };
            assert.Items = statementAbstract;
            SAMLResponse.Items = new object[] { assert };

            string xmlResponse = SAMLUtility.SerializeToXmlString(SAMLResponse);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlResponse);
            XmlSignatureUtils.SignDocument(doc, assert.ID);
            SAMLResponse = SAMLUtility.DeserializeFromXmlString<ResponseType>(doc.InnerXml);

            WriteResponse(context, HttpUtility.UrlDecode(SAMLRequest.AssertionConsumerServiceURL), SAMLResponse, HttpUtility.UrlDecode(context.Request[HttpBindingConstants.RelayState]));
        }

        private void WriteResponse(HttpContext context, string destination, ResponseType responseSAML, string relayState)
        {
            try
            {
                // That UrlEncode should be used to encode only un-trusted values used within URLs such
                // as in query string values. If the URL itself is the source of un-trusted input, then
                // input validation with regular expressions should be used.
                // Fonte: Microsoft Anti-Cross Site Scripting Library V1.5 - User Guide, p-10.
                Regex regex = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+=&amp;%\$#_]*)?$", RegexOptions.IgnoreCase);
                if (!regex.IsMatch(destination))
                    throw new CoreLibrary.Validation.Exceptions.ValidationException("A url contém um valor de entrada possivelmente perigoso, que pode indicar uma tentativa de comprometer a segurança do aplicativo.");

                string samlResponse = SAMLUtility.SerializeToXmlString(responseSAML);
                samlResponse = HttpUtility.UrlEncode(samlResponse);
                string data = string.Format("SAMLResponse={0}&RelayState={1}", samlResponse, relayState);
                retorno.UrlRedirect = destination;
                retorno.UrlRedirectParam = data;
                UtilBO.MessageJSON(context.Response.Output, retorno);
            }
            catch
            {
                throw;
            }
        }

        private void AddSAMLCookie(HttpContext context)
        {
            if (!string.IsNullOrEmpty(SAMLRequest.AssertionConsumerServiceURL))
            {
                HttpCookie cookie = context.Request.Cookies["SistemasLogged"];
                if (cookie == null)
                {
                    cookie = new HttpCookie("SistemasLogged");
                    context.Response.Cookies.Add(cookie);
                }

                // Carrega a Entidade SYS_Sistema apartir do caminho de login
                SYS_Sistema entitySistema = new SYS_Sistema { sis_caminho = SAMLRequest.AssertionConsumerServiceURL };
                if (SYS_SistemaBO.GetSelectBy_sis_caminho(entitySistema, SYS_SistemaBO.TypePath.login))
                {
                    // Armazena sistema no Cookie
                    cookie.Values[entitySistema.sis_id.ToString()] = entitySistema.sis_nome;
                    // Atualiza dados do Cookie
                    context.Response.Cookies.Set(cookie);
                }
                else
                    throw new ValidationException("Não foi possível atender a solicitação, sistema emissor da requisição não está cadastrado corretamente.");
            }
            else
                throw new ValidationException("Não foi possível atender a solicitação, requisição inválida.");
        }

        #endregion Métodos
    }
}