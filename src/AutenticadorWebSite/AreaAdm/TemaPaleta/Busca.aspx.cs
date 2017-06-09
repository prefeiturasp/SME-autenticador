namespace AutenticadorWebSite.AreaAdm.TemaPaleta
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Autenticador.BLL;
    using Autenticador.Entities;
    using Autenticador.Web.WebProject;
    using CoreLibrary.Validation.Exceptions;

    public partial class Busca : MotherPageLogado
    {
        #region Propriedades

        /// <summary>
        /// ID do tema padrão selecionado para edição.
        /// </summary>
        public int EditItem_tep_id
        {
            get
            {
                return Convert.ToInt32(grvTema.DataKeys[grvTema.EditIndex].Values["tep_id"].ToString());
            }
        }

        /// <summary>
        /// ID do tema de cores selecionado para edição.
        /// </summary>
        public int EditItem_tpl_id
        {
            get
            {
                return Convert.ToInt32(grvTema.DataKeys[grvTema.EditIndex].Values["tpl_id"].ToString());
            }
        }

        #endregion Propriedades

        #region Page Life Cycle

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
                    lblMensagem.Text = message;

                grvTema.PageIndex = 0;
                grvTema.PageSize = ApplicationWEB._Paginacao;

                btnNovoTema.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
        }

        #endregion Page Life Cycle

        #region Eventos

        protected void btnNovoTema_Click(object sender, EventArgs e)
        {
            Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TemaPaleta/Cadastro.aspx");
        }

        protected void grvTema_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Deletar")
            {
                try
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    int tep_id = Convert.ToInt32(grvTema.DataKeys[index].Values["tep_id"].ToString());
                    int tpl_id = Convert.ToInt32(grvTema.DataKeys[index].Values["tpl_id"].ToString());

                    CFG_TemaPaleta entity = new CFG_TemaPaleta { tep_id = tep_id, tpl_id = tpl_id };

                    if (CFG_TemaPaletaBO.Delete(entity))
                    {
                        grvTema.PageIndex = 0;
                        grvTema.DataBind();
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, String.Format("tep_id: {0} | tpl_id: {1}", tep_id, tpl_id));
                        lblMensagem.Text = UtilBO.GetErroMessage("Tema de cores excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        lblMensagem.Text = UtilBO.GetErroMessage("Erro ao tentar excluir o tema de cores.", UtilBO.TipoMensagem.Erro);
                    }
                }
                catch (ValidationException ex)
                {
                    lblMensagem.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
                }
                catch (Exception ex)
                {
                    ApplicationWEB._GravaErro(ex);
                    lblMensagem.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Erro);
                }
            }
        }

        protected void grvTema_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAlterar = (Label)e.Row.FindControl("lblAlterar");
                if (lblAlterar != null)
                {
                    lblAlterar.Visible = !__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                }

                LinkButton lkbAlterar = (LinkButton)e.Row.FindControl("lkbAlterar");
                if (lkbAlterar != null)
                {
                    lkbAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                }
                ImageButton btnExcluir = (ImageButton)e.Row.FindControl("btnExcluir");
                if (btnExcluir != null)
                {
                    btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                    btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }

        protected void odsTema_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (e.ExecutingSelectCount)
                e.InputParameters.Clear();
        }

        #endregion Eventos
    }
}