using System;
using System.Web.UI.HtmlControls;
using Autenticador.BLL;
using Autenticador.Web.WebProject;

public partial class MasterPage : MotherMasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Exibe o título no navegador
        Page.Title = __SessionWEB.TituloGeral + " - " + __SessionWEB.TituloSistema;

        #region Adiciona links de favicon

        string TemaAtual = __SessionWEB.TemaPadraoLogado.tep_nome;

        HtmlLink link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/favicon.ico");
        link.Attributes["rel"] = "shortcut icon";
        link.Attributes["sizes"] = "";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/apple-touch-icon-57x57.png");
        link.Attributes["rel"] = "apple-touch-icon";
        link.Attributes["sizes"] = "57x57";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/apple-touch-icon-114x114.png");
        link.Attributes["rel"] = "apple-touch-icon";
        link.Attributes["sizes"] = "114x114";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/apple-touch-icon-72x72.png");
        link.Attributes["rel"] = "apple-touch-icon";
        link.Attributes["sizes"] = "72x72";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/apple-touch-icon-144x144.png");
        link.Attributes["rel"] = "apple-touch-icon";
        link.Attributes["sizes"] = "144x144";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/apple-touch-icon-60x60.png");
        link.Attributes["rel"] = "apple-touch-icon";
        link.Attributes["sizes"] = "60x60";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/apple-touch-icon-120x120.png");
        link.Attributes["rel"] = "apple-touch-icon";
        link.Attributes["sizes"] = "120x120";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/apple-touch-icon-76x76.png");
        link.Attributes["rel"] = "apple-touch-icon";
        link.Attributes["sizes"] = "76x76";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/apple-touch-icon-152x152.png");
        link.Attributes["rel"] = "apple-touch-icon";
        link.Attributes["sizes"] = "152x152";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/favicon-196x196.png");
        link.Attributes["rel"] = "icon";
        link.Attributes["sizes"] = "196x196";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/favicon-160x160.png");
        link.Attributes["rel"] = "icon";
        link.Attributes["sizes"] = "160x160";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/favicon-96x96.png");
        link.Attributes["rel"] = "icon";
        link.Attributes["sizes"] = "96x96";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/favicon-16x16.png");
        link.Attributes["rel"] = "icon";
        link.Attributes["sizes"] = "16x16";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/favicon-32x32.png");
        link.Attributes["rel"] = "icon";
        link.Attributes["sizes"] = "32x32";
        Page.Header.Controls.Add(link);

        link = new HtmlLink();
        link.Href = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/favicon-32x32.png");
        link.Attributes["rel"] = "icon";
        link.Attributes["sizes"] = "32x32";
        Page.Header.Controls.Add(link);

        HtmlMeta meta = new HtmlMeta();
        meta.Name = "msapplication-TileImage";
        meta.Content = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/mstile-144x144.png");
        Page.Header.Controls.Add(meta);

        meta = new HtmlMeta();
        meta.Name = "msapplication-config";
        meta.Content = ResolveUrl("~/App_Themes/" + TemaAtual + "/images/favicons/browserconfig.xml");
        Page.Header.Controls.Add(meta);

        #endregion

        if (!IsPostBack)
        {
            try
            {
                //Exibe o contato do help desk do cliente
                spnHelpDesk.InnerHtml = __SessionWEB.HelpDeskContato;

                if (__SessionWEB.__UsuarioWEB.Grupo != null)
                {
                    string menuXml = SYS_ModuloBO.CarregarMenuXML(
                        __SessionWEB.__UsuarioWEB.Grupo.gru_id
                        , __SessionWEB.__UsuarioWEB.Grupo.sis_id
                        , __SessionWEB.__UsuarioWEB.Grupo.vis_id
                        );
                    if (String.IsNullOrEmpty(menuXml))
                        menuXml = "<menus/>";
                    XmlDataSource1.Data = menuXml;
                    XmlDataSource1.DataBind();

                    //LoadMenuTipo2();

                    //Carrrega nome do usuario logado no sistema e exibe na pagina na mensagem de Bem-vindo.
                    lblUsuario.Text = RetornaLoginFormatado(__SessionWEB.UsuarioLogado);

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
                        imgGeral.ToolTip = __SessionWEB.TituloGeral;
                        imgGeral.AlternateText = __SessionWEB.TituloGeral;
                        ImgLogoGeral.ToolTip = __SessionWEB.TituloGeral;
                        ImgLogoGeral.NavigateUrl = __SessionWEB.UrlSistemaAutenticador + "/Sistema.aspx";
                    }

                    //Atribui o caminho do logo do sistema atual, caso ele exista no Sistema Administrativo
                    if (string.IsNullOrEmpty(__SessionWEB.UrlLogoSistema))
                        ImgLogoSistemaAtual.Visible = false;
                    else
                    {
                        //Carrega logo do sistema atual
                        imgSistemaAtual.ImageUrl = UtilBO.UrlImagem(__SessionWEB.UrlLogoSistema);
                        imgSistemaAtual.AlternateText = __SessionWEB.TituloSistema;
                        imgSistemaAtual.ToolTip = __SessionWEB.TituloSistema;
                        ImgLogoSistemaAtual.ToolTip = __SessionWEB.TituloSistema;
                        ImgLogoSistemaAtual.NavigateUrl = "~/Index.aspx";
                    }

                    //TODO: Descomentar codigo abaixo.
                    imgImagemInstituicao.Visible = false;
                    ImgLogoInstitiuicao.Visible = false;

                    ////Atribui o caminho do logo cliente, caso ele exista no Sistema Administrativo
                    //if (string.IsNullOrEmpty(__SessionWEB.UrlInstituicao.Trim()))
                    //    ImgLogoInstitiuicao.Visible = false;
                    //else
                    //{
                    //    //Carrega logo do cliente
                    //    ImgLogoInstitiuicao.ImageUrl = UtilBO.UrlImagem(__SessionWEB.UrlLogoInstituicao);
                    //    ImgLogoInstitiuicao.ToolTip = string.Empty;
                    //    ImgLogoInstitiuicao.NavigateUrl = __SessionWEB.UrlInstituicao;
                    //}

                    //imgImageInstituicao.Visible = !ImgLogoInstitiuicao.Visible;
                    //imgImageInstituicao.ImageUrl = UtilBO.UrlImagem(__SessionWEB.UrlLogoInstituicao);

                    string urlHelp = SYS_ModuloSiteMapBO.SelecionaUrlHelpByUrl(__SessionWEB.__UsuarioWEB.Grupo.gru_id, Request.AppRelativeCurrentExecutionFilePath);

                    if (!string.IsNullOrEmpty(urlHelp))
                    {
                        hplHelp.Visible = true;
                        hplHelp.NavigateUrl = urlHelp;
                        hplHelp.ToolTip = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.MENSAGEM_ICONE_HELP);
                    }
                    else
                        hplHelp.Visible = false;

                }
                else
                {
                    Response.Redirect("~/logout.ashx");
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                if ((__SessionWEB.__UsuarioWEB != null) && (__SessionWEB.__UsuarioWEB.Usuario != null))
                    lblUsuario.Text = RetornaLoginFormatado(__SessionWEB.__UsuarioWEB.Usuario.usu_login ?? "");
            }
        }
    }
}
