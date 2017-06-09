using System;
using System.Web;
using System.Threading;

namespace Autenticador.Web.WebProject
{
    public class MotherPageLogado : MotherPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Verifica autenticação do usuário pelo Ticket da autenticação SAML
            if (!UserIsAuthenticated())
            {
                try
                {
                    HttpContext.Current.Response.Redirect(ApplicationWEB._DiretorioVirtual + ApplicationWEB._PaginaExpira, true);
                }
                catch (ThreadAbortException)
                {
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }

            }
            __SessionWEB._AreaAtual = new WebArea.AreaAdm();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Caso tenha grupo permissão, verifica as permissões no módulo atual para o usuário.
            if ((__SessionWEB.__UsuarioWEB.GrupoPermissao != null) && (__SessionWEB.__UsuarioWEB.GrupoPermissao.mod_id > 0))
            {
                // Verifica permissão do usuário, caso não tenha nehuma permissão na página redireciona para a Index.
                if ((!__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar) &&
                    (!__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir) &&
                    (!__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar) &&
                    (!__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir))
                {
                     __SessionWEB.PostMessages = BLL.UtilBO.GetErroMessage("Você não possui permissão para acessar a página solicitada.", Autenticador.BLL.UtilBO.TipoMensagem.Alerta);
                    Response.Redirect("~/Index.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }

            // Registra o GATC para a página. Código implementado na MotherPageLogado 
            // para que apenas as páginas da área restrita resgistrem o acompanhamento.
            BLL.UtilBO.RegistraGATC(this.Page);
        }
    }
}
