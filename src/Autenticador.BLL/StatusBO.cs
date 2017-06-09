using System;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Security.Principal;
using System.Security.AccessControl;
using CoreLibrary.Web.Mail;

using Autenticador.Entities;

namespace Autenticador.BLL
{
    public class StatusBO
    {
        #region Constants

        public const string Company = "";
        public const string Success = "OK";

        #endregion Constants

        #region Enumerador

        public enum eLevel
        {
            level0 = 0
            ,
            level1
            ,
            level2
            ,
            level3
        }

        #endregion Enumerador

        #region Construtores

        public StatusBO(Status entityStatus)
        {
            objStatus = entityStatus;
        }

        #endregion Construtores

        #region Propriedades

        private XmlDocument xml { get; set; }

        private Status objStatus { get; set; }

        #endregion Propriedades

        #region Métodos

        public virtual XmlDocument GetXmlDocument(eLevel level)
        {
            //objStatus.Validate();

            // Cria documento Xml
            xml = new XmlDocument();

            // Cria elemento Root
            XmlElement xmlElementRoot = xml.CreateElement("Status");
            xmlElementRoot.SetAttribute("company", Company);
            xmlElementRoot.SetAttribute("product", objStatus.SistemaNome);
            xmlElementRoot.SetAttribute("version", objStatus.SistemaVersao);
            xml.AppendChild(xmlElementRoot);

            // Cria declaração do documento
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.InsertBefore(xmlDeclaration, xmlElementRoot);

            switch (level)
            {
                case eLevel.level1:
                case eLevel.level2:
                    xmlElementRoot.AppendChild(GetElementIdentity(false));
                    break;

                case eLevel.level3:
                    {
                        // Verifica autenticação do usuário
                        if (objStatus.UsuarioIsAuthorized)
                        {
                            xmlElementRoot.AppendChild(GetElementFolder());
                            xmlElementRoot.AppendChild(GetElementIdentity(true));
                        }
                        break;
                    }
            }
            if (!String.IsNullOrEmpty(objStatus.EmailTo))
                xmlElementRoot.AppendChild(GetElementEmail(objStatus.EmailTo));

            return xml;
        }

        /// <summary>
        /// Retorna um XmlElement contendo um teste de envio de email
        /// </summary>
        /// <returns></returns>
        protected virtual XmlElement GetElementEmail(string email)
        {
            // Cria elemento Email
            string statusEmail = string.Empty;
            XmlElement xmlElementEmail = xml.CreateElement("Email");
            try
            {
                email = (String.IsNullOrEmpty(email) ? objStatus.EmailSuporte : email);

                // Verifica se configurações para envio de email são válidas
                bool isValid = (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(objStatus.EmailHost));
                if (isValid)
                {
                    Mail mail = new Mail(objStatus.EmailHost, true, System.Net.Mail.MailPriority.Normal)
                    {
                        _From = String.Concat("\"", objStatus.SistemaNome, "\"<", email, ">")
                        ,
                        _Subject = String.Concat(objStatus.SistemaNome, " - Teste de envio de email")
                        ,
                        _To = email
                        ,
                        _Body = "Testando envio de email..."
                    };

                    xmlElementEmail.SetAttribute("mailServer", objStatus.EmailHost);
                    xmlElementEmail.SetAttribute("to", email);

                    mail.SendMail();
                    statusEmail = Success;
                }
                else
                    statusEmail = "Host de email não configurado ou Email para teste não informado.";
            }
            catch (Exception ex)
            {
                statusEmail = ex.Message;
            }
            xmlElementEmail.SetAttribute("status", statusEmail);

            return xmlElementEmail;
        }

        /// <summary>
        /// Retorna um XmlElement contendo informações do Identity
        /// </summary>
        /// <returns></returns>
        protected virtual XmlElement GetElementIdentity(bool full)
        {
            // Cria elemento Identity
            XmlElement xmlElementIdentity = xml.CreateElement("Identity");
            try
            {
                xmlElementIdentity.SetAttribute("type", HttpContext.Current.User.Identity.AuthenticationType);
                xmlElementIdentity.SetAttribute("authenticated", HttpContext.Current.User.Identity.IsAuthenticated.ToString());

                if (full)
                {
                    // Cria elemento FormsIdentity
                    string statusFormsIdentity = string.Empty;
                    XmlElement xmlElementFormsIdentity = xml.CreateElement("FormsIdentity");
                    xmlElementIdentity.AppendChild(xmlElementFormsIdentity);

                    var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                    if (formsIdentity != null)
                    {
                        try
                        {
                            xmlElementFormsIdentity.SetAttribute("name", UtilBO.GetNameFormsAuthentication(formsIdentity.Name, UtilBO.TypeName.Login));
                            statusFormsIdentity = Success;
                        }
                        catch
                        {
                            statusFormsIdentity = "Propriedade name inválida.";
                        }

                        // Cria elemento Ticket
                        string statusTicket = string.Empty;
                        XmlElement xmlElementTicket = xml.CreateElement("Ticket");
                        xmlElementFormsIdentity.AppendChild(xmlElementTicket);
                        try
                        {
                            xmlElementTicket.SetAttribute("version", formsIdentity.Ticket.Version.ToString());
                            xmlElementTicket.SetAttribute("userData", formsIdentity.Ticket.UserData.ToString());
                            xmlElementTicket.SetAttribute("issueDate", formsIdentity.Ticket.IssueDate.ToString());
                            xmlElementTicket.SetAttribute("expiration", formsIdentity.Ticket.Expiration.ToString());
                            xmlElementTicket.SetAttribute("expired", formsIdentity.Ticket.Expired.ToString());
                            statusTicket = Success;
                        }
                        catch
                        {
                            statusTicket = "Ticket de autenticação inválido.";
                        }
                        xmlElementTicket.SetAttribute("status", statusTicket);
                    }
                    else
                    {
                        statusFormsIdentity = "Não foi possível recuperar informações do FormsIdentity.";
                    }
                    xmlElementFormsIdentity.SetAttribute("status", statusFormsIdentity);
                }
            }
            catch (Exception ex)
            {
                xmlElementIdentity.SetAttribute("error", ex.Message);
            }

            return xmlElementIdentity;
        }

        /// <summary>
        /// Retorna um XmlElement contendo informações do diretório especificado
        /// </summary>
        /// <returns></returns>
        protected virtual XmlElement GetElementFolder()
        {
            // Cria elemento Folder
            XmlElement xmlElementFolder = xml.CreateElement("Folder");
            try
            {
                DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                xmlElementFolder.SetAttribute("name", directory.FullName);
                xmlElementFolder.SetAttribute("size", (GetDirectorySize(directory, true) / 1024).ToString() + "(KB)");

                // Cria elemento Authorization
                XmlElement xmlElement = xml.CreateElement("Authorization");
                xmlElementFolder.AppendChild(xmlElement);
                try
                {
                    WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                    WindowsPrincipal currentPrincipal = new WindowsPrincipal(currentUser);
                    xmlElement.SetAttribute("read", GetAccessDirectory(currentUser, currentPrincipal, directory, FileSystemRights.Read).ToString());
                    xmlElement.SetAttribute("write", GetAccessDirectory(currentUser, currentPrincipal, directory, FileSystemRights.Write).ToString());
                    xmlElement.SetAttribute("modify", GetAccessDirectory(currentUser, currentPrincipal, directory, FileSystemRights.Modify).ToString());
                    xmlElement.SetAttribute("readAndExecute", GetAccessDirectory(currentUser, currentPrincipal, directory, FileSystemRights.ReadAndExecute).ToString());
                    xmlElement.SetAttribute("fullControl", GetAccessDirectory(currentUser, currentPrincipal, directory, FileSystemRights.FullControl).ToString());
                    xmlElement.SetAttribute("listDirectory", GetAccessDirectory(currentUser, currentPrincipal, directory, FileSystemRights.ListDirectory).ToString());
                }
                catch (Exception ex)
                {
                    xmlElement.SetAttribute("error", ex.Message);
                }
            }
            catch (Exception ex)
            {
                xmlElementFolder.SetAttribute("error", ex.Message);
            }
            return xmlElementFolder;
        }

        /// <summary>
        /// Retorna se tem permissão apartir do tipo de direito passado por parâmetro
        /// </summary>
        /// <param name="user"></param>
        /// <param name="principal"></param>
        /// <param name="directory"></param>
        /// <param name="eFileSystemRights"></param>
        /// <returns></returns>
        public static bool GetAccessDirectory(WindowsIdentity user, WindowsPrincipal principal, DirectoryInfo directory, FileSystemRights eFileSystemRights)
        {
            bool access = false;

            // Get the collection of authorization rules that apply to the current directory
            AuthorizationRuleCollection collectionRule = directory.GetAccessControl().GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));

            foreach (FileSystemAccessRule currentRule in collectionRule)
            {
                // If the current rule applies to the current user
                if (user.User.Equals(currentRule.IdentityReference) || principal.IsInRole((SecurityIdentifier)currentRule.IdentityReference))
                    if ((currentRule.FileSystemRights & eFileSystemRights) == eFileSystemRights)
                        access |= (currentRule.AccessControlType.Equals(AccessControlType.Allow));
            }
            return access;
        }

        /// <summary>
        /// Retorna o tamanho do diretório passado por parâmetro
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="includeSubdirectories"></param>
        /// <returns></returns>
        public static long GetDirectorySize(DirectoryInfo directory, bool includeSubdirectories)
        {
            long totalSize = 0;

            foreach (FileInfo file in directory.GetFiles())
                totalSize += file.Length;

            if (includeSubdirectories)
                foreach (DirectoryInfo dir in directory.GetDirectories())
                    totalSize += GetDirectorySize(dir, true);

            return totalSize;
        }

        #endregion Métodos
    }
}