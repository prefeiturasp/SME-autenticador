using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_TipoDeficiencia_Busca : MotherPageLogado
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

            _dgvTipoDeficiencia.PageIndex = 0;
            _dgvTipoDeficiencia.PageSize = ApplicationWEB._Paginacao;

            btnNovoTipoDeficiencia.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_dgvTipoDeficiencia.DataKeys[_dgvTipoDeficiencia.EditIndex].Value.ToString());
        }
    }

    #endregion

    #region EVENTOS

    protected void _odsTipoDeficiencia_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {

        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void btnNovoTipoDeficiencia_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoDeficiencia/Cadastro.aspx");
    }

    protected void _dgvTipoDeficiencia_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void _dgvTipoDeficiencia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid tde_id = new Guid(_dgvTipoDeficiencia.DataKeys[index].Value.ToString());

                PES_TipoDeficiencia EntityTipoDeficiencia = new PES_TipoDeficiencia { tde_id = tde_id };
                PES_TipoDeficienciaBO.GetEntity(EntityTipoDeficiencia);

                if (PES_TipoDeficienciaBO.Delete(EntityTipoDeficiencia))
                {
                    _dgvTipoDeficiencia.PageIndex = 0;
                    _dgvTipoDeficiencia.DataBind();
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "tde_id: " + tde_id);
                    _lblMessage.Text = UtilBO.GetErroMessage("Tipo de deficiência excluída com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o tipo de deficiência.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex); 
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }
    #endregion
}
