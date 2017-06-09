using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.Entities;
using Autenticador.BLL;

public partial class AreaAdm_TipoDoc_Busca : MotherPageLogado
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

            _dgvDocumentacao.PageIndex = 0;
            _dgvDocumentacao.PageSize = ApplicationWEB._Paginacao;

            _btnNovaDocumentacao.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_dgvDocumentacao.DataKeys[_dgvDocumentacao.EditIndex].Value.ToString());
        }
    }

    #endregion

    #region EVENTOS

    protected void _btnNovaDocumentacao_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoDoc/Cadastro.aspx");
    }

    protected void _odsTipoDocumentacao_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _dgvDocumentacao_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid tdo_id = new Guid(_dgvDocumentacao.DataKeys[index].Value.ToString());

                SYS_TipoDocumentacao EntityTipoDocumentacao = new SYS_TipoDocumentacao { tdo_id = tdo_id };
                SYS_TipoDocumentacaoBO.GetEntity(EntityTipoDocumentacao);

                if (SYS_TipoDocumentacaoBO.Delete(EntityTipoDocumentacao))
                {
                    _dgvDocumentacao.PageIndex = 0;
                    _dgvDocumentacao.DataBind();
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "tdo_id: " + tdo_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Tipo de documentação excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o tipo de documentação.", UtilBO.TipoMensagem.Erro);
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

    protected void _dgvDocumentacao_RowDataBound(object sender, GridViewRowEventArgs e)
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
    #endregion
    
}