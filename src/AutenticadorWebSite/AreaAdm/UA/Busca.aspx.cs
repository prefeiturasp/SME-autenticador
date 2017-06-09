using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Collections.Generic;

public partial class AreaAdm_UA_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
        }

        if (!IsPostBack)
        {
            string message = __SessionWEB.PostMessages;

            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;

            _grvUA.PageSize = ApplicationWEB._Paginacao;

            try
            {

                string ent_padrao = __SessionWEB.__UsuarioWEB.Usuario.ent_id.ToString();
                if (!string.IsNullOrEmpty(ent_padrao))
                    UCComboEntidade1._Combo.SelectedValue = ent_padrao;

                UCComboEntidade1.Inicialize("Entidade");
                UCComboEntidade1._EnableValidator = false;
                UCComboEntidade1._ShowSelectMessage = true;
                if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.Administracao)
                    UCComboEntidade1._Load(Guid.Empty, 0);
                else
                    UCComboEntidade1._LoadBy_UsuarioGrupoUA(Guid.Empty, __SessionWEB.__UsuarioWEB.Grupo.gru_id, __SessionWEB.__UsuarioWEB.Usuario.usu_id, 0);
                
                UCComboTipoUnidadeAdministrativa1.Inicialize("Tipo de unidade administrativa");
                UCComboTipoUnidadeAdministrativa1._EnableValidator = false;
                UCComboTipoUnidadeAdministrativa1._ShowSelectMessage = true;
                UCComboTipoUnidadeAdministrativa1._Load(Guid.Empty, 0);
                UCComboTipoUnidadeAdministrativa1._EnableValidator = false;

                if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.Gestao)
                {
                    foreach (SYS_UsuarioGrupoUA entity in __SessionWEB.__UsuarioWEB.GrupoUA)
                    {
                        _VS_List_UsuarioGrupoUA_uad_id.Add(entity.uad_id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }
            
            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
            Page.Form.DefaultFocus = UCComboEntidade1._Combo.ClientID;

            _divPesquisa.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnPesquisar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;


            if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.UnidadeAdministrativa)
            {
                UCComboEntidade1._Combo.Enabled = false;
                _btnNovo.Visible = false;
            }

            VerificaBusca();
        }
    }

    #region PROPRIEDADES

    public Guid EditItem_ent_id
    {
        get
        {
            return new Guid(_grvUA.DataKeys[_grvUA.EditIndex].Values[0].ToString());
        }
    }

    public Guid EditItem_uad_id
    {
        get
        {
            return new Guid(_grvUA.DataKeys[_grvUA.EditIndex].Values[1].ToString());
        }
    }

    public List<String> _VS_List_UsuarioGrupoUA_uad_id
    {
        get
        {
            if (ViewState["_VS_List_UsuarioGrupoUA_uad_id"] == null)
            {
                List<String> lt = new List<string>();
                ViewState["_VS_List_UsuarioGrupoUA_uad_id"] = lt;
            }
            return (List<String>)ViewState["_VS_List_UsuarioGrupoUA_uad_id"];
        }
        set
        {
            ViewState["_VS_List_UsuarioGrupoUA_uad_id"] = value;
        }
    }

    #endregion

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();

            _grvUA.PageIndex = 0;
            odsUA.SelectParameters.Clear();

            if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.Administracao)
            {
                odsUA.SelectMethod = "GetSelect";
                odsUA.SelectParameters.Add("gru_id", Guid.Empty.ToString());
                odsUA.SelectParameters.Add("usu_id", Guid.Empty.ToString());
            }
            else
            {
                odsUA.SelectMethod = "GetSelectBy_UsuarioGrupoUA";
                odsUA.SelectParameters.Add("gru_id", __SessionWEB.__UsuarioWEB.Grupo.gru_id.ToString());
                odsUA.SelectParameters.Add("usu_id", __SessionWEB.__UsuarioWEB.Usuario.usu_id.ToString());
            }

            odsUA.SelectParameters.Add("tua_id", UCComboTipoUnidadeAdministrativa1._Combo.SelectedValue);
            odsUA.SelectParameters.Add("ent_id", UCComboEntidade1._Combo.SelectedValue);
            odsUA.SelectParameters.Add("uad_id", Guid.Empty.ToString());
            odsUA.SelectParameters.Add("uad_nome", _txtNome.Text);
            odsUA.SelectParameters.Add("uad_codigo", _txtCodigo.Text);
            odsUA.SelectParameters.Add("uad_situacao", "0");
            odsUA.SelectParameters.Add("paginado", "true");
            odsUA.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsUA.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_UA
                ,
                Filtros = filtros
            };

            #endregion

            _grvUA.DataBind();        
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar as unidades administrativas.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_UA)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;
           
            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("tua_id", out valor);

            if (!string.IsNullOrEmpty(valor) && valor != Guid.Empty.ToString())
            {
                UCComboTipoUnidadeAdministrativa1.SetaEventoSource();
                UCComboTipoUnidadeAdministrativa1._Combo.DataBind();
                UCComboTipoUnidadeAdministrativa1._Combo.SelectedValue = valor;
            }

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("ent_id", out valor);

            if (!string.IsNullOrEmpty(valor) && valor != Guid.Empty.ToString())
            {
                UCComboEntidade1.SetaEventoSource();
                UCComboEntidade1._Combo.DataBind();
                UCComboEntidade1._Combo.SelectedValue = valor;
            }

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("uad_nome", out valor);
            _txtNome.Text = valor;

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("uad_codigo", out valor);
            _txtCodigo.Text = valor;

            _Pesquisar();
        }
        else
        {
            fdsResultados.Visible = false;
        }
    }

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "UA/Cadastro.aspx");
    }

    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        fdsResultados.Visible = true;        
        _Pesquisar();
    }

    protected void odsUA_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _grvUA_RowDataBound(object sender, GridViewRowEventArgs e)
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
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
                _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;

                if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.UnidadeAdministrativa
                    || _VS_List_UsuarioGrupoUA_uad_id.Contains(_grvUA.DataKeys[e.Row.RowIndex].Values[1].ToString()))
                    _btnExcluir.Visible = false;
            }
        }
    }

    protected void _grvUA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid ent_id = new Guid(_grvUA.DataKeys[index].Values[0].ToString());
                Guid uad_id = new Guid(_grvUA.DataKeys[index].Values[1].ToString());

                SYS_UnidadeAdministrativa entity = new SYS_UnidadeAdministrativa { ent_id = ent_id, uad_id = uad_id };
                SYS_UnidadeAdministrativaBO.GetEntity(entity);

                if (SYS_UnidadeAdministrativaBO.Delete(entity))
                {
                    _grvUA.PageIndex = 0;
                    _grvUA.DataBind();
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "ent_id: " + ent_id + "; uad_id: " + uad_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Unidade administrativa excluída com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir a unidade administrativa.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (CoreLibrary.Validation.Exceptions.ValidationException ex)
            {
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }
}
