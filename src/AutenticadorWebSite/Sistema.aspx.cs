using System;
using System.Collections.Generic;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using System.Web;

public partial class Sistema : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Verifica se existe messagem na Session
            string message = __SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;
        }

        if (__SessionWEB.TemaCoreUI)
        {
            sctnSistemas.Visible = true;
            fdsSistemas.Visible = false;
        }
        else
        {
            sctnSistemas.Visible = false;
            fdsSistemas.Visible = true;
        }
    }



    #region EVENTOS

    protected void odsSistema_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (!e.ExecutingSelectCount)
            e.InputParameters.Add("usu_id", __SessionWEB.__UsuarioWEB.Usuario.usu_id);
    }

    protected void _dltSistemas_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
            (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            string sis_nome = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "sis_nome"));
            string sis_urlImagem = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "sis_urlImagem"));
            string imagePath = VirtualPathUtility.ToAbsolute("~/App_Themes/" + NomeTemaAtual + "/images/logos/");

            Image img = (Image)e.Item.FindControl("imgSistema");
            img.AlternateText = sis_nome;
            img.ImageUrl = imagePath + sis_urlImagem;
        }
    }

    protected void rptSistemas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
            (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            string sis_nome = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "sis_nome"));
            string sis_urlImagem = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "sis_urlImagem"));
            string imagePath = VirtualPathUtility.ToAbsolute("~/App_Themes/" + NomeTemaAtual + "/images/logos/");

            Image img = (Image)e.Item.FindControl("imgSistema");
            img.AlternateText = sis_nome;
            img.ImageUrl = imagePath + sis_urlImagem;
        }
    }
    #endregion

}
