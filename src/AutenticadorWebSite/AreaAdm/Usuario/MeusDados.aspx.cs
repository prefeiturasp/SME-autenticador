using System;
using System.Web.UI.WebControls;
using CoreLibrary.Security.Cryptography;
using Autenticador.Entities;
using Autenticador.BLL;
using Autenticador.Web.WebProject;
using Autenticador.WebServices.Consumer;
using System.Web.Services;

public partial class AreaAdm_Usuario_MeusDados : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UCMeusDados1.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            UCMeusDados1.PermissaoAlterar = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar &&
                                            __SessionWEB.__UsuarioWEB.Usuario.usu_integracaoAD != (byte)SYS_Usuario.eIntegracaoAD.IntegradoAD;
        }
    }

    /// <summary>
    /// Valida se a senha atual está correta.
    /// </summary>
    /// <param name="senhaAtual">Senha atual.</param>
    /// <param name="usu_id">ID do usuário.</param>
    /// <returns></returns>
    [WebMethod]
    public static bool ValidarSenhaAtual(string senhaAtual, Guid usu_id)
    {
        return WebControls_MeusDados_UCMeusDados.ValidarSenhaAtual(senhaAtual, usu_id);
    }


    /// <summary>
    /// Valida se já existe usuário com o email.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <param name="usu_id">ID do usuário.</param>
    /// <returns></returns>
    [WebMethod]
    public static bool ValidarEmailExistente(string email, Guid usu_id)
    {
        return WebControls_MeusDados_UCMeusDados.ValidarEmailExistente(email, usu_id);
    }

    /// <summary>
    /// Validação de senha de acordo com suas senhas anteriores.
    /// </summary>
    /// <param name="novaSenha">Nova senha.</param>
    /// <param name="usu_id">ID do usuário.</param>
    /// <returns></returns>
    [WebMethod]
    public static bool ValidarHistoricoSenha(string novaSenha, Guid usu_id)
    {
        return WebControls_MeusDados_UCMeusDados.ValidarHistoricoSenha(novaSenha, usu_id);
    }
}
