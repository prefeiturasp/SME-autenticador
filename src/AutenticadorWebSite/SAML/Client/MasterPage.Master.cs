using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Web.WebProject;

namespace AutenticadorWebSite.SAML.Client
{
    public partial class MasterPage : MotherMasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    // Exibe o título no navegador
                    Page.Title = __SessionWEB.TituloGeral;

                    //Exibe a mensagem de copyright no rodapé.
                    //lblCopyright.Text = "<span class='tituloGeral'>" + __SessionWEB.TituloGeral + " - " + __SessionWEB.TituloSistema + "</span><span class='sep'> - </span><span class='versao'>" + _VS_versao + "</span><span class='sep'> - </span><span class='mensagem'>" + __SessionWEB.MensagemCopyright + "</span>";
                    lblCopyright.Text = "<span class='tituloGeral'>Desenvolvido no Brasil</span> | <span class='mensagem'>" + __SessionWEB.MensagemCopyright + "</span> | <span class='versao'>" + _VS_versao + "</span>";

                    //Atribui o caminho do logo geral do sistema, caso ele exista no Sistema Administrativo
                    if (string.IsNullOrEmpty(__SessionWEB.UrlLogoGeral))
                        ImgLogoGeral.Visible = false;
                    else
                    {
                        //Carrega logo geral do sistema
                        imgGeral.ImageUrl = UtilBO.UrlImagem(__SessionWEB.UrlLogoGeral);
                        ImgLogoGeral.ToolTip = __SessionWEB.TituloGeral;
                        ImgLogoGeral.NavigateUrl = __SessionWEB.UrlSistemaAutenticador + "/Sistema.aspx";
                    }
                }
                catch (Exception ex)
                {
                    ApplicationWEB._GravaErro(ex);
                }
            }
        }
    }
}