using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Collections.Generic;

public partial class AreaAdm_Entidade_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));

        if (!IsPostBack)
        {
            string message = __SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;
            _grvEntidades.PageSize = ApplicationWEB._Paginacao;

            try
            {
                UCComboTipoEntidade1.Inicialize("Tipo de entidade");
                UCComboTipoEntidade1._EnableValidator = false;
                UCComboTipoEntidade1._Load(0);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }

            VerificaBusca();
            
            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
            Page.Form.DefaultFocus = UCComboTipoEntidade1._Combo.ClientID;

            _divPesquisa.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnPesquisar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_grvEntidades.DataKeys[_grvEntidades.EditIndex].Value.ToString());            
        }
    }

    #endregion

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();
            
            _grvEntidades.PageIndex = 0;
            odsEntidade.SelectParameters.Clear();
            odsEntidade.SelectParameters.Add("ent_id", Guid.Empty.ToString());
            odsEntidade.SelectParameters.Add("ten_id", UCComboTipoEntidade1._Combo.SelectedValue);            
            odsEntidade.SelectParameters.Add("ent_razaoSocial", _txtRazaoSocial.Text);
            odsEntidade.SelectParameters.Add("ent_nomeFantasia", _txtNomeFantasia.Text);
            odsEntidade.SelectParameters.Add("ent_cnpj", _txtCNPJ.Text);
            odsEntidade.SelectParameters.Add("ent_codigo", _txtCodigo.Text);
            odsEntidade.SelectParameters.Add("ent_situacao", "0");
            odsEntidade.SelectParameters.Add("paginado", "true");
            odsEntidade.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsEntidade.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_Entidade
                ,
                Filtros = filtros
            };

            #endregion

            _grvEntidades.DataBind();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar as entidades.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_Entidade)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;
            
            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("ten_id", out valor);

            if (!string.IsNullOrEmpty(valor) && valor != Guid.Empty.ToString())
            {
                UCComboTipoEntidade1.SetaEventoSource();
                UCComboTipoEntidade1._Combo.DataBind();
                UCComboTipoEntidade1._Combo.SelectedValue = valor;
            }

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("ent_razaoSocial", out valor);
            _txtRazaoSocial.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("ent_nomeFantasia", out valor);
            _txtNomeFantasia.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("ent_cnpj", out valor);
            _txtCNPJ.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("ent_codigo", out valor);
            _txtCodigo.Text = valor;            

            _Pesquisar();
        }
        else
        {
            fdsResultado.Visible = false;
        }
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Entidade/Cadastro.aspx");
    }

    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        fdsResultado.Visible = true;        
        _Pesquisar();        
    }

    protected void odsEntidade_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _grvEntidades_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid ent_id = new Guid(_grvEntidades.DataKeys[index].Value.ToString());
                
                SYS_Entidade entity = new SYS_Entidade { ent_id = ent_id };
                SYS_EntidadeBO.GetEntity(entity);

                if (SYS_EntidadeBO.Delete(entity))
                {
                    _grvEntidades.PageIndex = 0;
                    _grvEntidades.DataBind();
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "ent_id: " + ent_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Entidade excluída com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir a entidade.", UtilBO.TipoMensagem.Erro);  
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _grvEntidades_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label _lblAlterar = (Label)e.Row.FindControl("_lblAlterar");
            if (_lblAlterar != null)
            {
                _lblAlterar.Visible = !__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;                
            }

            LinkButton _btnAlterar = (LinkButton)e.Row.FindControl("_btnAlterar");
            if (_btnAlterar != null)
            {
                _btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
}
