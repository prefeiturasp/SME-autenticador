using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using Autenticador.BLL;

public partial class AreaAdm_IntegracaoExterna_Cadastro : MotherPageLogado
{
    #region Eventos Page Life Cycle

    protected void Page_Load(object sender, EventArgs e)    
    {
        if ( !IsPostBack )
        {
            if (frvCadastro.CurrentMode.Equals(FormViewMode.ReadOnly))
            {
                Button btnEditar = (Button)frvCadastro.FindControl("btnEditar");
                if (btnEditar != null)
                    btnEditar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
               
            }
        }
    }

    #endregion

    #region Eventos

    protected void frvCadastro_DataBound(object sender, EventArgs e)
    {
        FormView fvw = (FormView)sender;
        FormViewMode mode = fvw.CurrentMode;

        byte ine_situacao = Convert.ToByte(DataBinder.Eval(fvw.DataItem, "ine_situacao"));
        int ine_tipo = Convert.ToInt32(DataBinder.Eval(fvw.DataItem, "ine_tipo"));
        int iet_id = Convert.ToInt32(DataBinder.Eval(fvw.DataItem, "iet_id"));

        if (mode.Equals(FormViewMode.ReadOnly))
        {
            Literal ltlSituacao = (Literal)fvw.FindControl("ltlSituacao");

            // Configura situação da integração.
            if(ltlSituacao != null)
            {
                if (ine_situacao == Convert.ToByte(SYS_IntegracaoExternaBO.eSituacao.Inativo))
                {
                    ltlSituacao.Text = "Inativo";
                }
                else
                {
                    ltlSituacao.Text = "Ativo";
                }
            }

            // Configura senha da integração.
            string ine_proxyAutenticacaoSenha = Convert.ToString(DataBinder.Eval(fvw.DataItem, "ine_proxyAutenticacaoSenha"));
            Literal ltlsenha = (Literal)fvw.FindControl("ltlProxyAutenticacaoSenha");
            if (ltlsenha != null)
            {
                if (ine_proxyAutenticacaoSenha.Length > 0)
                {
                    ltlsenha.Text = "*****";
                }
            }

            // Configura tipo de integração.
            Literal ltlTipointegracao = (Literal)fvw.FindControl("ltlTipoIntegracao");
            if(ltlTipointegracao != null)
            {
                if (ine_tipo == Convert.ToInt32(SYS_IntegracaoExternaBO.eTipoIntegracaoExterna.Live))    
                    ltlTipointegracao.Text = "Live";
                else
                    ltlTipointegracao.Text = "";
            }

            // Configura tipo de integração.
            Literal ddlIntegracaoExternaTipo = (Literal)fvw.FindControl("ddlIntegracaoExternaTipo");
            if (ddlIntegracaoExternaTipo != null)
            {
                if (iet_id == Convert.ToInt32(SYS_IntegracaoExternaTipoEnum.IntegracaoExterna))
                    ddlIntegracaoExternaTipo.Text = "Integração Externa";
                else
                    ddlIntegracaoExternaTipo.Text = "";
            }



            // Configura uso do proxy na integração.
            bool ine_proxy = Convert.ToBoolean(DataBinder.Eval(fvw.DataItem, "ine_proxy"));
            Literal ltlProxy = (Literal)fvw.FindControl("ltlProxy");
            if (ltlProxy != null)
            {
                if (ine_proxy)
                    ltlProxy.Text = "Sim";
                else
                    ltlProxy.Text = "Não";
            }


        }
        else if(mode.Equals(FormViewMode.Edit))
        {
            // Configura situação da integração.
            CheckBox ckbInativo = (CheckBox)fvw.FindControl("ckbInativo");
            if (ckbInativo != null)
            {
                if (ine_situacao == Convert.ToByte(SYS_IntegracaoExternaBO.eSituacao.Inativo))
                {
                    ckbInativo.Checked = true;
                }
                else
                {
                    ckbInativo.Checked = false;
                }
            }

            // Configura tipo de integração.
            DropDownList ddltipo = (DropDownList)fvw.FindControl("ddlTipo");
            if( ddltipo != null )
            {
                ddltipo.SelectedValue = ine_tipo.ToString();
            }


            // Configura Integracao Externa Tipo.
            DropDownList ddlIntegracaoExterna = (DropDownList)fvw.FindControl("ddlIntegracaoExterna");
            if (ddlIntegracaoExterna != null)
            {
                ddlIntegracaoExterna.SelectedValue = iet_id.ToString();
            }


            // Configura mensagens informativas
            Label lblInformaUsarProxy = (Label)fvw.FindControl("lblInformaUsarProxy");
            Label lblInformaProxyAutenticacao = (Label)fvw.FindControl("lblInformaProxyAutenticacao");
            if (lblInformaUsarProxy != null)
                lblInformaUsarProxy.Text = UtilBO.GetMessage("Se habilitada a opção (Usar proxy), as configurações de proxy informadas abaixo serão utilizadas no acesso externo.", UtilBO.TipoMensagem.Informacao);
            if (lblInformaProxyAutenticacao != null)
                lblInformaProxyAutenticacao.Text = UtilBO.GetMessage("Se habilitada a opção (Usar autenticação proxy), as configurações de autenticação do proxy informadas abaixo serão utilizadas no acesso externo.", UtilBO.TipoMensagem.Informacao);
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        HtmlInputHidden ine_situacao = (HtmlInputHidden)frvCadastro.FindControl("ine_situacao");
        HtmlInputHidden ine_tipo = (HtmlInputHidden)frvCadastro.FindControl("ine_tipo");
        HtmlInputHidden iet_id = (HtmlInputHidden)frvCadastro.FindControl("iet_id");

        // Configura situação para salvar.
        CheckBox ckbInativo = (CheckBox)frvCadastro.FindControl("ckbInativo");
        if (ckbInativo != null)
        {
            if (ckbInativo.Checked)
            {
                ine_situacao.Value = Convert.ToByte(SYS_IntegracaoExternaBO.eSituacao.Inativo).ToString();
            }
            else
            {
                ine_situacao.Value = Convert.ToByte(SYS_IntegracaoExternaBO.eSituacao.Ativo).ToString();
            }
        }
        
        // Configura tipo de integração para salvar.
        DropDownList ddltipo = (DropDownList)frvCadastro.FindControl("ddlTipo");
        if (ddltipo != null)
        {
            ine_tipo.Value = ddltipo.SelectedValue;
        }

        // Configura Integracao Externa Tipo 
        DropDownList ddlIntegracaoExterna = (DropDownList)frvCadastro.FindControl("ddlIntegracaoExterna");
        if (ddlIntegracaoExterna != null)
        {
            iet_id.Value = ddlIntegracaoExterna.SelectedValue;
        }

    }

    #endregion
}
