using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using System.Data;
using Autenticador.Entities;

public partial class AreaAdm_ManutencaoEndereco_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsUCCadastroEndereco.js"));
            sm.Services.Add(new ServiceReference("~/WSServicos.asmx"));
        }

        if (!IsPostBack)
        {
            string message = __SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;

            _grvEndereco.PageSize = ApplicationWEB._Paginacao;

            VerificaBusca();

            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
            Page.Form.DefaultFocus = _txtCEP.ClientID;

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
            return new Guid(_grvEndereco.DataKeys[_grvEndereco.EditIndex].Value.ToString());
        }
    }

    public DataTable _VS_AssociarEnderecos
    {
        get
        {
            if (ViewState["_VS_AssociarEnderecos"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("end_id");
                dt.Columns.Add("end_cep");
                dt.Columns.Add("end_logradouro");
                dt.Columns.Add("end_distrito");
                dt.Columns.Add("end_zona");
                dt.Columns.Add("end_bairro");                
                dt.Columns.Add("cid_id");
                dt.Columns.Add("cid_nome");                
                dt.Columns.Add("cidadeuf");                
                ViewState["_VS_AssociarEnderecos"] = dt;
            }
            return (DataTable)ViewState["_VS_AssociarEnderecos"];
        }
        set
        {
            ViewState["_VS_AssociarEnderecos"] = value;
        }
    }

    /// <summary>
    /// Retorna e atribui o ViewState com o id da cidade
    /// </summary>
    public Guid _VS_cid_id
    {
        get
        {
            if ((!string.IsNullOrEmpty(_txtCid_id.Value)) && (new Guid(_txtCid_id.Value) != Guid.Empty))
                return new Guid(_txtCid_id.Value);
            return ViewState["_VS_cid_id"] != null ? new Guid(ViewState["_VS_cid_id"].ToString()) : Guid.Empty;
        }
        set
        {
            ViewState["_VS_cid_id"] = value;
        }
    }

    #endregion

    #region METODOS

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();

            _grvEndereco.PageIndex = 0;
            odsEndereco.SelectParameters.Clear();
            odsEndereco.SelectParameters.Add("end_id", Guid.Empty.ToString());
            odsEndereco.SelectParameters.Add("unf_id", Guid.Empty.ToString());
            odsEndereco.SelectParameters.Add("pai_id", Guid.Empty.ToString());
            odsEndereco.SelectParameters.Add("cid_nome", string.Empty);
            odsEndereco.SelectParameters.Add("unf_nome", string.Empty);
            odsEndereco.SelectParameters.Add("unf_sigla", string.Empty);
            odsEndereco.SelectParameters.Add("pai_nome", string.Empty);
            odsEndereco.SelectParameters.Add("cid_id", _VS_cid_id.ToString());
            odsEndereco.SelectParameters.Add("end_cep", _txtCEP.Text);
            odsEndereco.SelectParameters.Add("end_logradouro", _txtLogradouro.Text);
            odsEndereco.SelectParameters.Add("end_bairro", _txtBairro.Text);            
            odsEndereco.SelectParameters.Add("end_situacao", "0");
            odsEndereco.SelectParameters.Add("paginado", "true");
            odsEndereco.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsEndereco.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_ManutencaoEndereco
                ,
                Filtros = filtros
            };

            #endregion

            _grvEndereco.DataBind();
            _updEnderecos.Update();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os enderecos.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_ManutencaoEndereco)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("cid_id", out valor);
            if (!string.IsNullOrEmpty(valor))
            {
                _VS_cid_id = new Guid(valor);

                //Carrega o nome da cidade
                END_Cidade cidade = new END_Cidade {cid_id = _VS_cid_id};
                END_CidadeBO.GetEntity(cidade);
                _txtCidade.Text = cidade.cid_nome;
            }

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("end_cep", out valor);
            _txtCEP.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("end_logradouro", out valor);
            _txtLogradouro.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("end_bairro", out valor);
            _txtBairro.Text = valor;

            _Pesquisar();
        }
        else
        {
            fdsResultado.Visible = false;
        }
    }

    private void _AssociarEndereco(Guid end_id, string end_cep, string end_logradouro, string end_distrito, string end_zona, string end_bairro, Guid cid_id, string cid_nome, string unf_sigla)
    {
        try
        {
            if (_VerificaExistenciaEndereco(end_id))
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Já existe uma associação para este endereço.", UtilBO.TipoMensagem.Erro);
            }
            else
            {
                DataRow dr = _VS_AssociarEnderecos.NewRow();
                dr["end_id"] = end_id;
                dr["end_cep"] = end_cep;
                dr["end_logradouro"] = end_logradouro;
                dr["end_distrito"] = end_distrito;
                dr["end_zona"] = Convert.ToByte(end_zona);
                dr["end_bairro"] = end_bairro;
                dr["cid_nome"] = cid_nome;
                dr["cid_id"] = cid_id;                
                dr["cidadeuf"] = cid_nome + " - " + unf_sigla;               

                //Realiza inserção do novo registro
                _VS_AssociarEnderecos.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    private bool _VerificaExistenciaEndereco(Guid end_id)
    {
        for (int i = 0; i < _VS_AssociarEnderecos.Rows.Count; i++)
        {
            if (_VS_AssociarEnderecos.Rows[i].RowState != DataRowState.Deleted)
            {
                if (_VS_AssociarEnderecos.Rows[i]["end_id"].ToString() == end_id.ToString())                
                    return true;                
            }
        }

        return false;
    }

    private void _RemoverEndereco(Guid end_id)
    {
        try
        {
            for (int i = 0; i < _VS_AssociarEnderecos.Rows.Count; i++)
            {
                if (_VS_AssociarEnderecos.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (_VS_AssociarEnderecos.Rows[i]["end_id"].ToString() == Convert.ToString(end_id))
                    {
                        _VS_AssociarEnderecos.Rows[i].Delete();
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar remover o endereço da associação.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _CarregarGridAssociarEndereco()
    {
        _grvAssociarEnderecos.DataSource = _VS_AssociarEnderecos;
        _grvAssociarEnderecos.DataBind();

        fdsAssociarEnderecos.Visible = _VS_AssociarEnderecos.Rows.Count != 0;
        _btnAssociarEnderecos.Visible = _VS_AssociarEnderecos.Rows.Count > 1;

        _updEnderecos.Update();
    }

    #endregion

    protected void odsEndereco_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        try
        {
            if ((bool)e.ReturnValue)
            {
                _grvEndereco.PageIndex = 0;
                _lblMessage.Text = UtilBO.GetErroMessage("Endereço excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar exibir mensagem de exclusão de endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void odsEndereco_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _btnAssociarEnderecos_Click(object sender, EventArgs e)
    {
        Session["ManutencaoEndereco_dtAssociarEndereco"] = _VS_AssociarEnderecos;
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoEndereco/Associar.aspx", false);
    }

    protected void _grvEndereco_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Associar")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Guid end_id = new Guid(_grvEndereco.DataKeys[index].Values[0].ToString());
            Guid cid_id = new Guid(_grvEndereco.DataKeys[index].Values[1].ToString());
            string end_zona = _grvEndereco.DataKeys[index].Values[2] == DBNull.Value ? "0" : _grvEndereco.DataKeys[index].Values[2].ToString();
            
            string end_cep = Convert.ToString(_grvEndereco.Rows[index].Cells[0].Text);
            string end_logradouro = ((Label)_grvEndereco.Rows[index].FindControl("_lblLogradouro")).Text;
            string end_distrito = ((Label)_grvEndereco.Rows[index].FindControl("_lblDistrito")).Text;           
            string end_bairro = ((Label)_grvEndereco.Rows[index].FindControl("_lblBairro")).Text;            
            string cid_nome = ((Label)_grvEndereco.Rows[index].FindControl("_lblCidade")).Text.Substring(0,((Label)_grvEndereco.Rows[index].FindControl("_lblCidade")).Text.Length - 5);
            string unf_sigla = ((Label)_grvEndereco.Rows[index].FindControl("_lblCidade")).Text.Substring(((Label)_grvEndereco.Rows[index].FindControl("_lblCidade")).Text.Length - 2, 2);            

            _AssociarEndereco(end_id, end_cep, end_logradouro, end_distrito, end_zona, end_bairro, cid_id, cid_nome, unf_sigla);
            _CarregarGridAssociarEndereco();
        }
        else if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid end_id = new Guid(_grvEndereco.DataKeys[index].Values[0].ToString());

                if (!_VerificaExistenciaEndereco(end_id))
                {                    
                    END_Endereco entityEndereco = new END_Endereco { end_id = end_id };
                    END_EnderecoBO.GetEntity(entityEndereco);
                    if (END_EnderecoBO.Delete(entityEndereco))
                    {
                        _grvEndereco.PageIndex = 0;
                        _grvEndereco.DataBind();
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "end_id: " + end_id);
                        _lblMessage.Text = UtilBO.GetErroMessage("Endereço excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o endereço.", UtilBO.TipoMensagem.Erro);
                    }
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Endereço não pode ser excluído, pois está preparado para associação. Remover endereço da associação.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _grvEndereco_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void _grvAssociarEnderecos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remover")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Guid end_id = new Guid(_grvAssociarEnderecos.DataKeys[index].Values[0].ToString());
            
            _RemoverEndereco(end_id);
            _CarregarGridAssociarEndereco();
        }
    }

    protected void _grvAssociarEnderecos_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        _Pesquisar();
        fdsResultado.Visible = true;
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoEndereco/Cadastro.aspx");
    }
}
