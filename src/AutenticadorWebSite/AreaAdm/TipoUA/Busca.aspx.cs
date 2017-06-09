using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_TipoUA_Busca : MotherPageLogado
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
            _dgvTipoUA.PageIndex = 0;
            _dgvTipoUA.PageSize = ApplicationWEB._Paginacao;

            _divConsulta.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_dgvTipoUA.DataKeys[_dgvTipoUA.EditIndex].Value.ToString());
        }
    }
    #endregion

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoUA/Cadastro.aspx",false);
    }

    protected void _odsTipoUA_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _dgvTipoUA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
                _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;         
            }
            LinkButton _lkbAlterar = (LinkButton)e.Row.FindControl("_lkbAlterar");
            if (_lkbAlterar != null)
            {
                _lkbAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }
            Label _lblAlterar = (Label)e.Row.FindControl("_lblAlterar");
            if (_lblAlterar != null)
            {
                _lblAlterar.Visible = !(__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar);
            }
        }
    }

    protected void _dgvTipoUA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid tua_id = new Guid(_dgvTipoUA.DataKeys[index].Value.ToString());

                SYS_TipoUnidadeAdministrativa entity = new SYS_TipoUnidadeAdministrativa { tua_id = tua_id };
                SYS_TipoUnidadeAdministrativaBO.GetEntity(entity);

                if (SYS_TipoUnidadeAdministrativaBO.Delete(entity))
                {
                    _dgvTipoUA.PageIndex = 0;
                    _dgvTipoUA.DataBind();
                    _lblMessage.Text = UtilBO.GetErroMessage("Tipo de unidade administrativa excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);

                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "tua_id: " + tua_id);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o tipo de unidade administrativa.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex); 
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }
}
