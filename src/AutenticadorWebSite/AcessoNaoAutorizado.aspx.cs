using System;
using System.Web;
using Autenticador.Web.WebProject;

namespace AutenticadorWebSite
{
    public partial class AcessoNaoAutorizado : MotherPageLogado
    {
        #region Eventos

        protected void btnSim_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Concat(ApplicationWEB._DiretorioVirtual, "Sistema.aspx"), false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnNao_Click(object sender, EventArgs e)
        {
            __SessionWEB.SistemaIDLogout_QueryString = __SessionWEB.SistemaID_QueryString;

            Response.Redirect("~/logout.ashx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        #endregion
    }
}