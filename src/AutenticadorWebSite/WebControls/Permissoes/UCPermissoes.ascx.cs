using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;

public partial class WebControls_Permissoes_UCPermissoes : MotherUserControl
{
    #region Constantes

    private int ColumnCollapse = 6;

    #endregion

    #region Eventos Page Life Cycle

    protected void Page_PreRender(object sender, EventArgs e)
    {
        grvPermissoes.DataSource = SYS_GrupoBO.GetSelectPermissoes(ModuloPaiId, GrupoId);
        grvPermissoes.DataBind();
    }

    #endregion

    #region Propriedades

    public Guid GrupoId
    {
        get
        {
            if (ViewState["VS_gru_id"] != null)
                return new Guid(ViewState["VS_gru_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["VS_gru_id"] = value;
        }
    }

    public int ModuloPaiId
    {
        get
        {
            if (ViewState["VS_mod_id"] != null)
                return Convert.ToInt32(ViewState["VS_mod_id"]);
            return-1;
        }
        set
        {
            ViewState["VS_mod_id"] = value;
        }
    }


    #endregion

    #region Eventos

    protected void grvPermissoes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView grvPermissoes = (GridView)sender;

            int sis_id = Convert.ToInt32(grvPermissoes.DataKeys[e.Row.RowIndex]["sis_id"]);
            int mod_id = Convert.ToInt32(grvPermissoes.DataKeys[e.Row.RowIndex]["mod_id"]);
            DataTable dtModulo = SYS_ModuloBO.SelectBy_mod_id_Filhos(sis_id, mod_id);

            if (dtModulo.Rows.Count > 0)
            {
                GridView grv = (GridView)e.Row.FindControl("grvPermissoesChild");
                grv.DataSource = SYS_GrupoBO.GetSelectPermissoes(mod_id, new Guid(grvPermissoes.DataKeys[e.Row.RowIndex]["gru_id"].ToString()));
                grv.DataBind();

                LinkButton lkb = (LinkButton)e.Row.FindControl("lkbExpandir");
                if (lkb != null)
                {
                    lkb.OnClientClick = "ExpandCollapse3('" + grv.ClientID + "', '" + lkb.ClientID + "');  return false;";
                    lkb.Visible = true;
                }
            }
            else
            {
                e.Row.Cells[ColumnCollapse].Visible = false;
            }
        }
    }

    #endregion
}


