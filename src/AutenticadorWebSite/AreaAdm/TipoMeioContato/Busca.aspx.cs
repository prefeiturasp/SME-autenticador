using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_TipoMeioContato_Busca : MotherPageLogado
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

            _dgvTipoMeioContato.PageIndex = 0;
            _dgvTipoMeioContato.PageSize = ApplicationWEB._Paginacao;

            _divConsulta.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            _btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }
    }

    #region PROPRIEDADES

    public Guid EditItem
    {
        get
        {
            return new Guid(_dgvTipoMeioContato.DataKeys[_dgvTipoMeioContato.EditIndex].Value.ToString());
        }
    }

    #endregion

    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoMeioContato/Cadastro.aspx",false);
    }
    protected void _odsTipoMeioContato_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _dgvTipoMeioContato_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Guid tmc_id = new Guid(_dgvTipoMeioContato.DataKeys[index].Value.ToString());

                SYS_TipoMeioContato EntityTipoMeioContato = new SYS_TipoMeioContato { tmc_id = tmc_id };
                SYS_TipoMeioContatoBO.GetEntity(EntityTipoMeioContato);

                if (SYS_TipoMeioContatoBO.Delete(EntityTipoMeioContato))
                {
                    _dgvTipoMeioContato.PageIndex = 0;
                    _dgvTipoMeioContato.DataBind();
                    _lblMessage.Text = UtilBO.GetErroMessage("Tipo de meio de contato excluída com sucesso.", UtilBO.TipoMensagem.Sucesso);

                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "tmc_id: " + tmc_id);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o tipo de meio de contato.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex); 
                _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
            }
        }
    }

    protected void _dgvTipoMeioContato_RowDataBound(object sender, GridViewRowEventArgs e)
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
}
