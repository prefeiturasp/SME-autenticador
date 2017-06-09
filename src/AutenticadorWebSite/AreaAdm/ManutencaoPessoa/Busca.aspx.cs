using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.Entities;
using Autenticador.BLL;
using System.Data;

public partial class AreaAdm_ManutencaoPessoa_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.CamposData));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
        }

        if (!IsPostBack)
        {
            try
            {
                string message = __SessionWEB.PostMessages; 
                if (!String.IsNullOrEmpty(message))
                    _lblMessage.Text = message;

                _grvPessoa.PageSize = ApplicationWEB._Paginacao;

                Guid tdo_id;
                SYS_TipoDocumentacao tdo = new SYS_TipoDocumentacao();

                string tipoDocCPF = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_CPF);
                if (!string.IsNullOrEmpty(tipoDocCPF))
                {
                    tdo_id = new Guid(tipoDocCPF);
                    tdo.tdo_id = tdo_id;
                    SYS_TipoDocumentacaoBO.GetEntity(tdo);
                    _lblCPF.Text = tdo.tdo_sigla;
                    _grvPessoa.Columns[2].HeaderText = tdo.tdo_sigla;
                    _grvAssociarPessoas.Columns[2].HeaderText = tdo.tdo_sigla;
                }
                else
                {
                    _lblCPF.Text = string.Empty;
                    _lblCPF.Visible = false;
                    _txtCPF.Visible = false;
                    _grvPessoa.Columns[2].HeaderText = string.Empty;
                    _grvPessoa.Columns[2].HeaderStyle.CssClass = "hide";
                    _grvPessoa.Columns[2].ItemStyle.CssClass = "hide";

                    _grvAssociarPessoas.Columns[2].HeaderText = string.Empty;
                    _grvAssociarPessoas.Columns[2].HeaderStyle.CssClass = "hide";
                    _grvAssociarPessoas.Columns[2].ItemStyle.CssClass = "hide";
                }

                string tipoDocRG = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_RG);
                if (!string.IsNullOrEmpty(tipoDocRG))
                {
                    tdo_id = new Guid(tipoDocRG);
                    tdo.tdo_id = tdo_id;
                    SYS_TipoDocumentacaoBO.GetEntity(tdo);
                    _lblRG.Text = tdo.tdo_sigla;
                    _grvPessoa.Columns[3].HeaderText = tdo.tdo_sigla;
                    _grvAssociarPessoas.Columns[3].HeaderText = tdo.tdo_sigla;
                }
                else
                {
                    _lblRG.Text = string.Empty;
                    _lblRG.Visible = false;
                    _txtRG.Visible = false;
                    _grvPessoa.Columns[3].HeaderText = string.Empty;
                    _grvPessoa.Columns[3].HeaderStyle.CssClass = "hide";
                    _grvPessoa.Columns[3].ItemStyle.CssClass = "hide";

                    _grvAssociarPessoas.Columns[3].HeaderText = string.Empty;
                    _grvAssociarPessoas.Columns[3].HeaderStyle.CssClass = "hide";
                    _grvAssociarPessoas.Columns[3].ItemStyle.CssClass = "hide";
                }

                VerificaBusca();

                Page.Form.DefaultButton = _btnPesquisar.UniqueID;
                Page.Form.DefaultFocus = _txtNome.ClientID;

                _divPesquisa.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
                _btnPesquisar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
                _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_grvPessoa.DataKeys[_grvPessoa.EditIndex].Value.ToString());
        }
    }

    public DataTable _VS_AssociarPessoas
    {
        get
        {
            if (ViewState["_VS_AssociarPessoas"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("pes_id");
                dt.Columns.Add("pes_nome");
                dt.Columns.Add("pes_dataNascimento");
                dt.Columns.Add("tipo_documentacao_cpf");
                dt.Columns.Add("tipo_documentacao_rg");                
                ViewState["_VS_AssociarPessoas"] = dt;
            }
            return (DataTable)ViewState["_VS_AssociarPessoas"];
        }
        set
        {
            ViewState["_VS_AssociarPessoas"] = value;
        }
    }

    #endregion

    #region METODOS

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();

            DateTime data;
            string dataString = string.Empty;

            if (!string.IsNullOrEmpty(_txtDataNasc.Text))
            {
                data = Convert.ToDateTime(_txtDataNasc.Text);
                dataString = data.ToString("yyyy-MM-dd");
            }        

            _grvPessoa.PageIndex = 0;
            odsPessoas.SelectParameters.Clear();
            odsPessoas.SelectParameters.Add("nome", _txtNome.Text);
            odsPessoas.SelectParameters.Add("data", dataString);
            odsPessoas.SelectParameters.Add("cpf", _txtCPF.Text);
            odsPessoas.SelectParameters.Add("rg", _txtRG.Text);
            odsPessoas.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsPessoas.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_ManutencaoPessoa
                ,
                Filtros = filtros
            };

            #endregion

            _grvPessoa.DataBind();
            _updPessoas.Update();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar as pessoas.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_ManutencaoPessoa)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("nome", out valor);
            _txtNome.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("data", out valor);            
            if (!string.IsNullOrEmpty(valor))
                _txtDataNasc.Text = Convert.ToDateTime(valor).ToString("dd/MM/yyyy");
            

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("cpf", out valor);
            _txtCPF.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("rg", out valor);
            _txtRG.Text = valor;

            _Pesquisar();
        }
        else
        {
            fdsResultado.Visible = false;
        }
    }

    private void _AssociarPessoa(Guid pes_id, string pes_nome, string pes_dataNascimento, string cpf, string rg)
    {
        try
        {
            if (_VerificaExistenciaPessoa(pes_id))
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Já existe uma associação para esta pessoa.", UtilBO.TipoMensagem.Erro);
            }
            else
            {
                DataRow dr = _VS_AssociarPessoas.NewRow();
                dr["pes_id"] = pes_id;
                dr["pes_nome"] = pes_nome;
                dr["pes_dataNascimento"] = !string.IsNullOrEmpty(pes_dataNascimento) ? pes_dataNascimento : string.Empty;
                dr["tipo_documentacao_cpf"] = cpf;
                dr["tipo_documentacao_rg"] = rg;                

                //Realiza inserção do novo registro
                _VS_AssociarPessoas.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar a pessoa.", UtilBO.TipoMensagem.Erro);
        }
    }

    private bool _VerificaExistenciaPessoa(Guid pes_id)
    {
        for (int i = 0; i < _VS_AssociarPessoas.Rows.Count; i++)
        {
            if (_VS_AssociarPessoas.Rows[i].RowState != DataRowState.Deleted)
            {
                if (_VS_AssociarPessoas.Rows[i]["pes_id"].ToString() == pes_id.ToString())
                    return true;
            }
        }

        return false;
    }

    private void _RemoverEndereco(Guid pes_id)
    {
        try
        {
            for (int i = 0; i < _VS_AssociarPessoas.Rows.Count; i++)
            {
                if (_VS_AssociarPessoas.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (_VS_AssociarPessoas.Rows[i]["pes_id"].ToString() == Convert.ToString(pes_id))
                    {
                        _VS_AssociarPessoas.Rows[i].Delete();
                        break;
                    }
                }
            }
        }
        catch( Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar remover a pessoa da associação.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _CarregarGridAssociarPessoa()
    {
        _grvAssociarPessoas.DataSource = _VS_AssociarPessoas;
        _grvAssociarPessoas.DataBind();

        fdsAssociarPessoas.Visible = _VS_AssociarPessoas.Rows.Count > 0;
        _btnAssociar.Visible = _VS_AssociarPessoas.Rows.Count > 1;

        _updPessoas.Update();
    }

    #endregion

    protected void _grvPessoa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Associar")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Guid pes_id = new Guid(_grvPessoa.DataKeys[index].Values[0].ToString());
            string pes_nome = ((Label)_grvPessoa.Rows[index].FindControl("_lblNome")).Text;
            string data = ((Label)_grvPessoa.Rows[index].FindControl("_lblData")).Text;
            string cpf = ((Label)_grvPessoa.Rows[index].FindControl("_lblCPF")).Text;
            string rg = ((Label)_grvPessoa.Rows[index].FindControl("_lblRG")).Text;            

            _AssociarPessoa(pes_id, pes_nome, data, cpf, rg);
            _CarregarGridAssociarPessoa();
        }
        else if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid pes_id = new Guid(_grvPessoa.DataKeys[index].Values[0].ToString());

                if (!_VerificaExistenciaPessoa(pes_id))
                {                    
                    PES_Pessoa entityPessoa = new PES_Pessoa { pes_id = pes_id };
                    PES_PessoaBO.GetEntity(entityPessoa);

                    if (PES_PessoaBO.Delete(entityPessoa, null))
                    {
                        _grvPessoa.PageIndex = 0;
                        _grvPessoa.DataBind();
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "pes_id: " + pes_id);
                        _lblMessage.Text = UtilBO.GetErroMessage("Pessoa excluída com sucesso.", UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir a pessoa.", UtilBO.TipoMensagem.Erro);
                    }
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Pessoa não pode ser excluída, pois está preparada para associação. Remover pessoa da associação.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _grvPessoa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnAlterar = (ImageButton)e.Row.FindControl("_btnAlterar");
            if (_btnAlterar != null)
            {
                _btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                _btnAlterar.CommandArgument = e.Row.RowIndex.ToString();
            }

            ImageButton btnAssociar = (ImageButton)e.Row.FindControl("_btnAssociar");
            if (btnAssociar != null)
            {
                btnAssociar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                btnAssociar.CommandArgument = e.Row.RowIndex.ToString();
            }

            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void _btnAssociar_Click(object sender, EventArgs e)
    {
        Session["ManutencaoPessoa_dtAssociarPessoa"] = _VS_AssociarPessoas;
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoPessoa/Associar.aspx", false);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoPessoa/Busca.aspx", false);
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoPessoa/Cadastro.aspx");
    }

    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            fdsResultado.Visible = true;
            _Pesquisar();
        }
        else
            fdsResultado.Visible = false;
    }

    protected void _grvAssociarPessoas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remover")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Guid pes_id = new Guid(_grvAssociarPessoas.DataKeys[index].Values[0].ToString());

            _RemoverEndereco(pes_id);
            _CarregarGridAssociarPessoa();
        }
    }

    protected void _grvAssociarPessoas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnRemover = (ImageButton)e.Row.FindControl("_btnRemover");
            if (_btnRemover != null)
            {
                _btnRemover.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void odsPessoas_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }
}
