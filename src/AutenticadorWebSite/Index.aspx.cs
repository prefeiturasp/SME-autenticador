using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Autenticador.BLL;
using Autenticador.Web.WebProject;

public partial class Index : MotherPageLogado
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string menuXml = SYS_ModuloBO.CarregarSiteMapXML2(
                __SessionWEB.__UsuarioWEB.Grupo.gru_id,
                __SessionWEB.__UsuarioWEB.Grupo.sis_id,
                __SessionWEB.__UsuarioWEB.Grupo.vis_id,
                GetModuloId
                );
            if (String.IsNullOrEmpty(menuXml))
                menuXml = "<menus/>";
            menuXml = menuXml.Replace("url=\"~/", "url=\"");

            XmlTextReader reader = new XmlTextReader(new StringReader(menuXml));
            XPathDocument treeDoc = new XPathDocument(reader);
            XslCompiledTransform siteMap = new XslCompiledTransform();
            siteMap.Load(Server.MapPath("Includes/SiteMap.xslt"));

            StringWriter sw = new StringWriter();
            siteMap.Transform(treeDoc, null, sw);
            string result = sw.ToString();

            Control ctrl = Page.ParseControl(result);
            _lblSiteMap.Controls.Add(ctrl);
        }
        catch (Exception ex) 
        {
            ApplicationWEB._GravaErro(ex); 
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao carregar informações!", UtilBO.TipoMensagem.Erro);       
        }
    }

    /// <summary>
    /// Rertona o Id do modulo da query string
    /// </summary>
    protected int GetModuloId
    {
        get
        {
            int mod_id = 0;
            Int32.TryParse(Convert.ToString(Request.QueryString["mod_id"]), out mod_id);
            //Retorna zero para trazer todos os menus inclusive o nó do sistema
            return mod_id;
        }
    }
}




