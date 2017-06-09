using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using System.Data;
using Autenticador.Entities;

public partial class AreaAdm_ManutencaoCidade_Busca : MotherPageLogado
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
            _grvCidade.PageSize = ApplicationWEB._Paginacao;

            try
            {
                UCComboPais.Inicialize("País");
                UCComboPais._EnableValidator = false;
                UCComboPais._Load(0);

                UCComboUnidadeFederativa.Inicialize("Estado");
                UCComboUnidadeFederativa._EnableValidator = false;

                string pais_padrao = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL);

                if (!string.IsNullOrEmpty(pais_padrao))
                {
                    UCComboPais.SetaEventoSource();
                    UCComboPais._Combo.DataBind();
                    UCComboPais._Combo.SelectedValue = pais_padrao;

                    UCComboUnidadeFederativa._Load(new Guid(pais_padrao), 0);
                    UCComboUnidadeFederativa._Combo.Enabled = true;
                }
                else
                {
                    UCComboUnidadeFederativa._Load(Guid.Empty, 0);
                    UCComboUnidadeFederativa._Combo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }

            VerificaBusca();

            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
            Page.Form.DefaultFocus = UCComboPais._Combo.ClientID;

            _divPesquisa.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnPesquisar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
        }

        UCComboPais.OnSelectedIndexChange = UCComboPais__IndexChanged;
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_grvCidade.DataKeys[_grvCidade.EditIndex].Values[2].ToString());
        }
    }

    public DataTable _VS_AssociacaoCidades
    {
        get
        {
            if (ViewState["_VS_AssociacaoCidades"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("cid_id");
                dt.Columns.Add("pai_id");
                dt.Columns.Add("unf_id");
                dt.Columns.Add("cid_ddd");
                dt.Columns.Add("cid_nome");
                dt.Columns.Add("pai_nome");
                dt.Columns.Add("unf_nome");
                ViewState["_VS_AssociacaoCidades"] = dt;
            }
            return (DataTable)ViewState["_VS_AssociacaoCidades"];
        }
        set
        {
            ViewState["_VS_AssociacaoCidades"] = value;
        }
    }

    #endregion

    #region DELEGATES

    public delegate void onSeleciona(Guid cid_id);
    public event onSeleciona _Selecionar;

    public void _SelecionarCidade(Guid cid_id)
    {
        if (_Selecionar != null)
            _Selecionar(cid_id);
    }

    #endregion

    #region METODOS

    void UCComboPais__IndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (UCComboPais._Combo.SelectedIndex > 0)
            {
                UCComboUnidadeFederativa._Combo.Enabled = true;
               
                UCComboUnidadeFederativa._Combo.Items.Clear();
                UCComboUnidadeFederativa._Load(new Guid(UCComboPais._Combo.SelectedValue), 0);
                UCComboUnidadeFederativa.CancelarBinding = false;
                UCComboUnidadeFederativa._Combo.DataBind();
                if (UCComboUnidadeFederativa._Combo.Items.FindByValue(new Guid().ToString()) == null)
                {
                    UCComboUnidadeFederativa._Combo.Items.Insert(0, new ListItem("-- Selecione uma opção --", new Guid().ToString()));
                }
            }
            else
            {
                UCComboUnidadeFederativa._Combo.Enabled = false;
            }

            UCComboUnidadeFederativa._Combo.SelectedValue = Guid.Empty.ToString();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }
            
    }

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();

            _grvCidade.PageIndex = 0;

            odsCidade.SelectParameters.Clear();
            odsCidade.SelectParameters.Add("cid_id", Guid.Empty.ToString());
            odsCidade.SelectParameters.Add("pai_id", UCComboPais._Combo.SelectedValue);
            odsCidade.SelectParameters.Add("unf_id", UCComboUnidadeFederativa._Combo.SelectedValue);
            odsCidade.SelectParameters.Add("cid_nome", _txtCidade.Text);
            odsCidade.SelectParameters.Add("unf_nome", string.Empty);
            odsCidade.SelectParameters.Add("unf_sigla", string.Empty);
            odsCidade.SelectParameters.Add("pai_nome", string.Empty);
            odsCidade.SelectParameters.Add("cid_situacao", "0");
            odsCidade.SelectParameters.Add("paginado", "true");
            odsCidade.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsCidade.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_ManutencaoCidade
                ,
                Filtros = filtros
            };

            #endregion

            _grvCidade.DataBind();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar as cidades.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_ManutencaoCidade)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("pai_id", out valor);
            if (!string.IsNullOrEmpty(valor) && valor != Guid.Empty.ToString())
            {
                UCComboPais.SetaEventoSource();
                UCComboPais._Combo.DataBind();
                UCComboPais._Combo.SelectedValue = valor;
            }            

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("unf_id", out valor);
            if (!string.IsNullOrEmpty(valor) && valor != Guid.Empty.ToString())
            {
                UCComboUnidadeFederativa._Combo.Enabled = true;
                UCComboUnidadeFederativa.SetaEventoSource();
                UCComboUnidadeFederativa._Combo.DataBind();
                UCComboUnidadeFederativa._Combo.SelectedValue = valor;                
            }            

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("cid_nome", out valor);
            _txtCidade.Text = valor;

            _Pesquisar();
        }
        else
        {
            fdsResultados.Visible = false;
        }
    }

    private void _AssociaCidade_AssociacaoCidades(Guid cid_id, Guid pai_id, Guid unf_id, string cid_ddd, string cid_nome, string pai_nome, string unf_nome)
    {
        try
        {
            if (_VerificaExistenciaCidade_AssociacaoCidades(cid_id))
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Esta cidade já está associada.", UtilBO.TipoMensagem.Erro);
            }
            else
            {
                DataRow dr = _VS_AssociacaoCidades.NewRow();
                dr["cid_id"] = cid_id;
                dr["pai_id"] = pai_id;
                dr["unf_id"] = unf_id;
                dr["cid_ddd"] = cid_ddd;
                dr["cid_nome"] = cid_nome;
                dr["pai_nome"] = pai_nome;
                dr["unf_nome"] = unf_nome;
                //Realiza inserção do novo registro
                _VS_AssociacaoCidades.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar cidade.", UtilBO.TipoMensagem.Erro);
        }
    }

    private bool _VerificaExistenciaCidade_AssociacaoCidades(Guid cid_id)
    {
        for (int i = 0; i < _VS_AssociacaoCidades.Rows.Count; i++)
        {
            if (_VS_AssociacaoCidades.Rows[i].RowState != DataRowState.Deleted)
            {
                if ( new Guid(_VS_AssociacaoCidades.Rows[i]["cid_id"].ToString()) == cid_id)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void _RemoverCidade_AssociacaoCidades(Guid cid_id)
    {
        try
        {
            for (int i = 0; i < _VS_AssociacaoCidades.Rows.Count; i++)
            {
                if (_VS_AssociacaoCidades.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (_VS_AssociacaoCidades.Rows[i]["cid_id"].ToString() == Convert.ToString(cid_id))
                    {
                        _VS_AssociacaoCidades.Rows[i].Delete();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar remover cidade da associação.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _CarregarGridAssociacaoCidades()
    {
        _grvAssociacaoCidades.DataSource = _VS_AssociacaoCidades;
        _grvAssociacaoCidades.DataBind();

        fdsAssociacaoCidades.Visible = _VS_AssociacaoCidades.Rows.Count > 0;                    
        _btnAssociarCidades.Visible = _VS_AssociacaoCidades.Rows.Count > 1;

        _updCidades.Update();
    }

    #endregion

    protected void odsCidade_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        _Pesquisar();
        fdsResultados.Visible = true;
    }

    protected void _grvCidade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AssociarCidade")
        {
            int index = int.Parse(e.CommandArgument.ToString());

            Guid pai_id = new Guid(_grvCidade.DataKeys[index].Values[0].ToString());
            Guid unf_id = _grvCidade.DataKeys[index].Values[1] == DBNull.Value ? Guid.Empty : new Guid(_grvCidade.DataKeys[index].Values[1].ToString());
            Guid cid_id = new Guid(_grvCidade.DataKeys[index].Values[2].ToString());    
                    
            string cid_ddd = ((Label)_grvCidade.Rows[index].FindControl("_lbcid_ddd")).Text;
            string cid_nome = ((Label)_grvCidade.Rows[index].FindControl("_lbcid_nome")).Text;
            string pai_nome = ((Label)_grvCidade.Rows[index].FindControl("_lbpai_nome")).Text;
            string unf_nome = ((Label)_grvCidade.Rows[index].FindControl("_lbunf_nome")).Text;

            _AssociaCidade_AssociacaoCidades(cid_id, pai_id, unf_id, cid_ddd, cid_nome, pai_nome, unf_nome);
            _CarregarGridAssociacaoCidades();
        }
        else if (e.CommandName == "DeletarCidade")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid cid_id = new Guid(_grvCidade.DataKeys[index].Values[2].ToString());

                if (!_VerificaExistenciaCidade_AssociacaoCidades(cid_id))
                {
                    END_Cidade _EntidadeCidade = new END_Cidade { cid_id = cid_id };
                    END_CidadeBO.GetEntity(_EntidadeCidade);
                    if (END_CidadeBO.Delete(_EntidadeCidade))
                    {
                        _grvCidade.PageIndex = 0;
                        _grvCidade.DataBind();
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "cid_id: " + cid_id);
                        _lblMessage.Text = UtilBO.GetErroMessage("Cidade excluída com sucesso.", UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir a cidade.", UtilBO.TipoMensagem.Erro);
                    }
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Cidade não pode ser excluída, pois está preparada para associação. Remover cdade da associação.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _grvCidade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnAlterar = (ImageButton)e.Row.FindControl("_btnAlterar");
            if (_btnAlterar != null)
            {
                _btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                _btnAlterar.CommandArgument = e.Row.RowIndex.ToString();
            }

            ImageButton _btnAssociar = (ImageButton)e.Row.FindControl("_btnAssociar");
            if (_btnAssociar != null)
            {
                _btnAssociar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                _btnAssociar.CommandArgument = e.Row.RowIndex.ToString();
            }

            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void _grvAssociacaoCidades_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoverCidade")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Guid cid_id = new Guid(_grvAssociacaoCidades.DataKeys[index].Value.ToString());
            _RemoverCidade_AssociacaoCidades(cid_id);
            _CarregarGridAssociacaoCidades();
        }
    }

    protected void _grvAssociacaoCidades_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnRemoverCidade = (ImageButton)e.Row.FindControl("_btnRemoverCidade");
            if (_btnRemoverCidade != null)
            {                
                _btnRemoverCidade.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void _btnAssociarCidades_Click(object sender, EventArgs e)
    {
        Session["ManutencaoCidade_dtAssociacaoCidades"] = _VS_AssociacaoCidades;
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoCidade/Associar.aspx", false);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoCidade/Busca.aspx", false);
    }

}
