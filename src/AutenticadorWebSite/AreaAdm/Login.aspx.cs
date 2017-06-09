using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.Entities;
using Autenticador.BLL;
using System.Net;
using CoreLibrary.Validation.Exceptions;
using AutenticadorWebSite;
using Autenticador.Web.WebProject.Authentication;

public partial class AreaAdm_Login : MotherPageLogado
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                // Verifica se usuário está autenticado
                if (UserIsAuthenticated())
                {
                    // Carrega grupos do usuário
                    IList<SYS_Grupo> list = SYS_GrupoBO.GetSelectBySis_idAndUsu_id(
                       __SessionWEB.__UsuarioWEB.Usuario.usu_id
                       , ApplicationWEB.SistemaID);

                    // Verifica se foi carregado os grupos do usuário
                    if (list.Count > 0)
                    {
                        // Verifica se usuário logado possui um único grupo para carregar na Session,
                        // caso possua vários grupos será necessário selecionar apenas um grupo
                        if (list.Count == 1)
                        {
                            __SessionWEB.__UsuarioWEB.Grupo = list[0];

                            LoadSessionSistema();
                            // Realiza autenticação do usuário no Sistema Administrativo

                            //SYS_UsuarioBO.AutenticarUsuario(__SessionWEB.__UsuarioWEB.Usuario, __SessionWEB.__UsuarioWEB.Grupo);

                            SignHelper.AutenticarUsuario(__SessionWEB.__UsuarioWEB.Usuario, __SessionWEB.__UsuarioWEB.Grupo);

                            ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, String.Format("Autenticação do usuário ( {0} ) com grupo ( {1} ) no sistema ( {2} ).", __SessionWEB.__UsuarioWEB.Usuario.usu_login, __SessionWEB.__UsuarioWEB.Grupo.gru_nome, __SessionWEB.TituloSistema));

                            Response.Redirect("~/Index.aspx", false);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            rptGrupos.DataSource = list;
                            rptGrupos.DataBind();
                            divGrupos.Visible = true;
                        }
                    }
                    else
                    {
                        throw new ValidationException("Não foi possível atender a solicitação, nenhum grupo de usuário encontrado.<br />Clique no botão voltar e tente novamente.");
                    }
                }
                else
                {
                    throw new ValidationException("O usuário não tem permissão de acesso ao sistema.<br />Clique no botão voltar e tente novamente.");
                }
            }
            catch (ValidationException ex)
            {
                lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
                btnVoltar.Visible = true;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Não foi possível atender a solicitação.<br />Clique no botão voltar e tente novamente.", UtilBO.TipoMensagem.Erro);
                btnVoltar.Visible = true;
            }
        }
    }

    protected void _rptGrupos_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                // Carrega grupo selecionado na Session
                SYS_Grupo grupo = new SYS_Grupo { gru_id = new Guid(e.CommandArgument.ToString()) };
                __SessionWEB.__UsuarioWEB.Grupo = SYS_GrupoBO.GetEntity(grupo);

                LoadSessionSistema();

                // Realiza autenticação do usuário no Sistema Administrativo
                // SYS_UsuarioBO.AutenticarUsuario(__SessionWEB.__UsuarioWEB.Usuario, __SessionWEB.__UsuarioWEB.Grupo);

                SignHelper.AutenticarUsuario(__SessionWEB.__UsuarioWEB.Usuario, __SessionWEB.__UsuarioWEB.Grupo);

                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Login, String.Format("Autenticação do usuário ( {0} ) com grupo ( {1} ) no sistema ( {2} ).", __SessionWEB.__UsuarioWEB.Usuario.usu_login, __SessionWEB.__UsuarioWEB.Grupo.gru_nome, __SessionWEB.TituloSistema));

                Response.Redirect("~/Index.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            lblMessage.Text = UtilBO.GetErroMessage("Não foi possível atender a solicitação.<br />Clique no botão voltar e tente novamente.", UtilBO.TipoMensagem.Erro);
            btnVoltar.Visible = true;
        }
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB.UrlSistemaAutenticador + "/Sistema.aspx", false);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    #endregion Eventos
}