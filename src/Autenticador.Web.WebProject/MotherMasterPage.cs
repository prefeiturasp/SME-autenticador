using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Autenticador.BLL;
using Autenticador.Entities;

namespace Autenticador.Web.WebProject
{
    public class MotherMasterPage : CoreLibrary.Web.WebProject.MotherMasterPage
    {
        public new SessionWEB __SessionWEB
        {
            get
            {
                return (SessionWEB)Session[CoreLibrary.Web.WebProject.ApplicationWEB.SessSessionWEB];
            }
            set
            {
                Session[CoreLibrary.Web.WebProject.ApplicationWEB.SessSessionWEB] = value;
            }
        }

        public string _VS_versao
        {
            get
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    String strRet = String.Empty;

                    xmlDoc.Load(ApplicationWEB._DiretorioFisico + "version.xml");
                    XmlNode xmlNd = xmlDoc.SelectSingleNode("//versionNumber");

                    if (xmlNd != null)
                        strRet = String.Format("Versão: {0}.{1}.{2}.{3}", xmlNd.ChildNodes[0].Attributes["value"].Value,
                            xmlNd.ChildNodes[1].Attributes["value"].Value,
                            xmlNd.ChildNodes[2].Attributes["value"].Value,
                            xmlNd.ChildNodes[3].Attributes["value"].Value
                            );


                    return strRet;
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        /// <summary>
        /// Retorna o caminho virtual da pasta de logos.
        /// </summary>
        public string caminhoLogos
        {
            get
            {
                return "~/App_Themes/" + Page.Theme + "/images/logos/";
            }
        }

        /// <summary>
        /// Retorna o login formatado para mostrar na tela.
        /// </summary>
        public string RetornaLoginFormatado(string login)
        {
            if (!String.IsNullOrEmpty(login))
            {
                // Pega somente o primeiro nome.
                //login = login.Split(' ')[0];

                // Corta o nome se tiver mais que 16 caracteres.
                if (login.Length > 16)
                    login = login.Substring(0, 13) + "...";
            }

            return login;
        }
    }
}
