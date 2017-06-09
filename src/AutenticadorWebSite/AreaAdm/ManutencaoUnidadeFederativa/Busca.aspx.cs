using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using System.Data;
using Autenticador.Entities;

public partial class AreaAdm_ManutencaoUnidadeFederativa_Busca : MotherPageLogado
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
            _grvUnidadeFederativa.PageSize = ApplicationWEB._Paginacao;

            try
            {
                UCComboPais.Inicialize("País");
                UCComboPais._EnableValidator = false;
                UCComboPais._Load(0);

                string pais_padrao = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL);

                if (!string.IsNullOrEmpty(pais_padrao))
                {
                    UCComboPais.SetaEventoSource();
                    UCComboPais._Combo.DataBind();
                    UCComboPais._Combo.SelectedValue = pais_padrao;
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
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_grvUnidadeFederativa.DataKeys[_grvUnidadeFederativa.EditIndex].Values[1].ToString());
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

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();

            _grvUnidadeFederativa.PageIndex = 0;

            odsUnidadeFederativa.SelectParameters.Clear();
            odsUnidadeFederativa.SelectParameters.Add("unf_id", Guid.Empty.ToString());
            odsUnidadeFederativa.SelectParameters.Add("pai_id", UCComboPais._Combo.SelectedValue);
            odsUnidadeFederativa.SelectParameters.Add("unf_nome", txtUnidadeFederativa.Text);
            odsUnidadeFederativa.SelectParameters.Add("unf_sigla", string.Empty);
            odsUnidadeFederativa.SelectParameters.Add("unf_situacao", "0");
            odsUnidadeFederativa.SelectParameters.Add("paginado", "true");
            odsUnidadeFederativa.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsUnidadeFederativa.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_ManutencaoUnidadeFederativa
                ,
                Filtros = filtros
            };

            #endregion

            _grvUnidadeFederativa.DataBind();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar as unidades federativas.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_ManutencaoUnidadeFederativa)
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

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("unf_nome", out valor);
            txtUnidadeFederativa.Text = valor;

            _Pesquisar();
        }
        else
        {
            fdsResultados.Visible = false;
        }
    }

    #endregion

    protected void odsUnidadeFederativa_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        _Pesquisar();
        fdsResultados.Visible = true;
    }

    protected void _grvUnidadeFederativa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeletarUnidadeFederativa")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid unf_id = new Guid(_grvUnidadeFederativa.DataKeys[index].Values[1].ToString());

                END_UnidadeFederativa _EntidadeUnidadeFederativa = new END_UnidadeFederativa { unf_id = unf_id };
                END_UnidadeFederativaBO.GetEntity(_EntidadeUnidadeFederativa);
                if (_EntidadeUnidadeFederativa.unf_integridade > 0)
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Não é possível excluir a unidade federativa, pois ela já está sendo utilizada no sistema.", UtilBO.TipoMensagem.Alerta);
                }
                else
                {
                    if (END_UnidadeFederativaBO.Delete(_EntidadeUnidadeFederativa))
                    {
                        _grvUnidadeFederativa.PageIndex = 0;
                        _grvUnidadeFederativa.DataBind();
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "unf_id: " + unf_id);
                        _lblMessage.Text = UtilBO.GetErroMessage("Unidade federativa excluída com sucesso.", UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir a unidade federativa.", UtilBO.TipoMensagem.Erro);
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _grvUnidadeFederativa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnAlterar = (ImageButton)e.Row.FindControl("_btnAlterar");
            if (_btnAlterar != null)
            {
                _btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                _btnAlterar.CommandArgument = e.Row.RowIndex.ToString();
            }

            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoUnidadeFederativa/Busca.aspx", false);
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoUnidadeFederativa/Cadastro.aspx", false);
    }

}
