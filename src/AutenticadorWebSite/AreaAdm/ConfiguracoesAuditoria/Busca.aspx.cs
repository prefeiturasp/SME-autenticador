using System;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;

public partial class AreaAdm_ConfiguracoesAuditoria_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string message = __SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;

            _dgvSistema.PageIndex = 0;
            _dgvSistema.PageSize = ApplicationWEB._Paginacao;
        }
    }

    #region PROPRIEDADES

    public int EditItem_sis_id
    {
        get
        {
            return Convert.ToInt32(_dgvSistema.DataKeys[_dgvSistema.EditIndex].Value.ToString());
        }

      
    }

    #endregion

    protected void _odsSistema_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }
    protected void _dgvSistema_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }
    }
}
