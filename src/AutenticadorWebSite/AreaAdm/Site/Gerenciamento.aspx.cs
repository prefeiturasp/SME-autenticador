using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Microsoft.Web.Administration;
using System.Text;
using Autenticador.BLL;

namespace AutenticadorWebSite.AreaAdm.Site
{
    public partial class Gerenciamento : MotherPageLogado
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                IIS75Manager iis = new IIS75Manager();
                SiteCollection list = iis.GetListSites();   
                this.gvSites.DataSource = list;
                this.gvSites.DataBind();   
 
            }
            catch (Exception ex) 
            {                   
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao carregar informações!", UtilBO.TipoMensagem.Erro);       
            }
        }    
  
        protected void gvSites_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType== DataControlRowType.DataRow)
			{
                var site = (Microsoft.Web.Administration.Site)e.Row.DataItem;

                GridView gv = (GridView)e.Row.FindControl("gvApplicationPools");
                gv.DataSource = site.Applications;
                gv.DataBind();
			}			    
        }   
    }

    public class IIS75Manager
    {
        /// <summary>
        /// Retorna todos os sites hospedados no IIS
        /// </summary>
        /// <returns></returns>
        public SiteCollection GetListSites()
        {
            ServerManager mgr = new ServerManager();
            return mgr.Sites;
        }
    }




}
