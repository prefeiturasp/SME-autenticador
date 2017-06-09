using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Web;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Security;
using System.Collections;
using System.IO;
using System.Security.AccessControl;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using CoreLibrary.Security.Cryptography;
using CoreLibrary.SAML20.Configuration;
using Autenticador.BLL;
using CoreLibrary.Web.Mail;
using CoreLibrary.SAML20;
using System.Security.Principal;

namespace Autenticador.Web.WebProject.HttpHandlers
{
    /// <summary>
    ///  Classe que implementa IHttpHandler para:
    ///  - Apresentar informações do Banco de dados e testar conexões.
    ///  - Apresentar informações do Identity e validar dados.
    ///  - Apresentar informações do SAML e validar dados.
    ///  - Apresentar informações da pasta do Site.
    ///  - Testar envio de email utilizando o email informado por parâmetro e usando o smtp da 
    ///    configuração do site.
    ///    
    ///  Parâmetros que devem ser passados:
    ///  - Parâmetro level: valor de "1" a "3". Onde quando maior o level, maior o detalhamento
    ///  das informações.
    ///     Ex: level=1 | level=2 | level=3
    ///  - Parâmetro email: passar a conta para a qual será enviado o email de teste. Caso não 
    ///  seja passado, o teste do email não será realizado.
    ///     Ex: email=email@dominio.com
    ///     
    ///  Exemplos de url:
    ///  Status.ashx?level=1
    ///  Status.ashx?level=1&email=email@dominio.com
    /// </summary>
    public class Status : MotherPage, IHttpHandler, IRequiresSessionState
    {
        #region Propriedades

        private XmlDocument xml { get; set; }

        #endregion

        #region IHttpHandler Members

        public new bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        public new void ProcessRequest(HttpContext context)
        {
            try
            {
                Autenticador.Entities.Status entityStatus = new Autenticador.Entities.Status()
                {
                    SistemaID = ApplicationWEB.SistemaID
                ,
                    SistemaVersao = GetVersion()
                ,
                    SistemaNome = (string.IsNullOrEmpty(__SessionWEB.TituloSistema) ? __SessionWEB.TituloGeral : __SessionWEB.TituloSistema)
                ,
                    EmailSuporte = ApplicationWEB._EmailSuporte
                ,
                    EmailHost = ApplicationWEB._EmailHost
                ,
                    EmailTo = context.Request.QueryString["email"]
                ,
                    UsuarioID = (UserIsAuthenticated() ? __SessionWEB.__UsuarioWEB.Usuario.usu_id : Guid.Empty)
                ,
                    UsuarioIsAuthorized = (__SessionWEB.__UsuarioWEB.Grupo != null ? __SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.Administracao : false)
                };

                StatusBO status = new StatusBO(entityStatus);
                StatusBO.eLevel level = (String.IsNullOrEmpty(context.Request.QueryString["level"]) ? StatusBO.eLevel.level0 : (StatusBO.eLevel)Enum.Parse(typeof(StatusBO.eLevel), context.Request.QueryString["level"], true));
                xml = status.GetXmlDocument(level);

                // Add XmlElement SAML
                if (entityStatus.UsuarioIsAuthorized)
                {
                    XmlElement xmlElementRoot = (XmlElement)xml.GetElementsByTagName("Status")[0];

                    switch (level)
                    {
                        case StatusBO.eLevel.level2:
                            {
                                xmlElementRoot.AppendChild(GetElementSAML(false));
                                break;
                            }
                        case StatusBO.eLevel.level3:
                            {
                                xmlElementRoot.AppendChild(GetElementSAML(true));
                                break;
                            }
                    }
                }
           
                context.Response.ContentType = "text/xml";
                context.Response.Write(xml.OuterXml);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                context.Response.ContentType = "text/plain";
                context.Response.Write("Não foi possível gerar status.");
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Retorna a versão do sistema
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                String str = String.Empty;

                xmlDoc.Load(ApplicationWEB._DiretorioFisico + "version.xml");
                XmlNode xmlNd = xmlDoc.SelectSingleNode("//versionNumber");

                if (xmlNd != null)
                    str = String.Format("Versão: {0}.{1}.{2}.{3}", xmlNd.ChildNodes[0].Attributes["value"].Value,
                        xmlNd.ChildNodes[1].Attributes["value"].Value,
                        xmlNd.ChildNodes[2].Attributes["value"].Value,
                        xmlNd.ChildNodes[3].Attributes["value"].Value
                        );

                return str;
            }
            catch
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Retorna um XmlElement contendo informações do Saml
        /// </summary>
        /// <returns></returns>
        private XmlElement GetElementSAML(bool full)
        {
            // Cria elemento SAML
            XmlElement xmlElementSAML = xml.CreateElement("Saml");
            xmlElementSAML.SetAttribute("type", "Identity Provider");
            xmlElementSAML.SetAttribute("version", SAMLUtility.VERSION);

            // Cria elemento IDProvider
            string statusIDProvider = string.Empty;
            XmlElement xmlElementIDProvider = xml.CreateElement("IDProvider");
            xmlElementSAML.AppendChild(xmlElementIDProvider);
            try
            {
                IDProvider config = IDProvider.GetConfig();
                if (config != null)
                {
                    xmlElementIDProvider.SetAttribute("id", config.id);
                    statusIDProvider = StatusBO.Success;
                }
                else
                {
                    statusIDProvider = "Não foi possível encontrar as configurações.";
                }
            }
            catch (Exception ex)
            {
                statusIDProvider = ex.Message;
            }
            xmlElementIDProvider.SetAttribute("status", statusIDProvider);

            if (full)
            {
                // Cria elemento Logged
                string statusLogged = string.Empty;
                XmlElement xmlElementLogged = xml.CreateElement("Logged");
                xmlElementSAML.AppendChild(xmlElementLogged);
                try
                {
                    HttpCookie cookie = Context.Request.Cookies["SistemasLogged"];
                    xmlElementLogged.SetAttribute("count", (cookie == null ? "0" : cookie.Values.AllKeys.Count(p => p != null).ToString()));
                    if (cookie != null)
                    {
                        foreach (String str in cookie.Values.AllKeys.Where(p => p != null))
                        {
                            XmlElement xmlElement = xml.CreateElement("Application");
                            xmlElementLogged.AppendChild(xmlElement);
                            xmlElement.SetAttribute("name", cookie.Values[str]);
                        }
                    }
                    statusLogged = StatusBO.Success;
                }
                catch (Exception ex)
                {
                    statusLogged = ex.Message;
                }
                xmlElementLogged.SetAttribute("status", statusLogged);

                // Cria elemento Path
                XmlElement xmlElementPath = xml.CreateElement("Path");
                xmlElementSAML.AppendChild(xmlElementPath);
                try
                {
                    IList<Autenticador.Entities.SYS_Sistema> list = SYS_SistemaBO.GetSelectBy_usu_id(__SessionWEB.__UsuarioWEB.Usuario.usu_id);
                    Autenticador.Entities.SYS_Sistema entityCoreSSO = list.Where(p => p.sis_id == ApplicationWEB.SistemaID).First();
                    list.Remove(entityCoreSSO);

                    foreach (Autenticador.Entities.SYS_Sistema entitySistema in list)
                    {
                        string status = string.Empty;
                        XmlElement xmlElement = xml.CreateElement("Application");
                        xmlElementPath.AppendChild(xmlElement);
                        xmlElement.SetAttribute("name", entitySistema.sis_nome);
                        xmlElement.SetAttribute("login", entitySistema.sis_caminho);
                        xmlElement.SetAttribute("logout", entitySistema.sis_caminhoLogout);

                        // Validação url de login
                        if (string.IsNullOrEmpty(entitySistema.sis_caminho))
                            status += "Url de login inválida.";
                        else if (entitySistema.sis_caminho.Contains(entityCoreSSO.sis_caminho))
                            status += "A url de login contém um valor possivelmente inválido, que pode entrar em loop ao redirecionar.";
                        // Validação url de logout
                        if (string.IsNullOrEmpty(entitySistema.sis_caminhoLogout))
                            status += "Url de logout inválida.";
                        else if (entitySistema.sis_caminhoLogout.Contains(entityCoreSSO.sis_caminhoLogout))
                            status += "A url de logout contém um valor possivelmente inválido, que pode entrar em loop ao redirecionar.";

                        if (string.IsNullOrEmpty(status)) status = StatusBO.Success;
                        xmlElement.SetAttribute("status", status);
                    }
                }
                catch (Exception ex)
                {
                    xmlElementPath.SetAttribute("error", ex.Message);
                }
            }

            return xmlElementSAML;
        }

        #endregion
    }
}
