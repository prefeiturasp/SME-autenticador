using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;

public partial class WebControls_Sistemas_UCSistemas : MotherUserControl
{
    #region EVENTOS

    protected void odsSistemas_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (!e.ExecutingSelectCount)
            e.InputParameters.Add("usu_id", __SessionWEB.__UsuarioWEB.Usuario.usu_id);
    }

    protected void rptSistemas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            string sis_nome = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "sis_nome"));
            string sis_urlImagem = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "sis_urlImagem"));
            string imagePath = VirtualPathUtility.ToAbsolute("~/App_Themes/" + __SessionWEB.TemaPadrao + "/images/logos/");

            Image img = (Image)e.Item.FindControl("imgSistema");
            img.AlternateText = sis_nome;
            img.ImageUrl = imagePath + sis_urlImagem;
        }
    }

    #endregion
}
