using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using Autenticador.BLL;
using Autenticador.Entities;
using CoreLibrary.Security.Cryptography;
using Autenticador.Web.WebProject;
using CoreLibrary.Validation.Exceptions;

namespace Services.GestaoEscolar
{
    /// <summary>
    /// Summary description for CoreSSOService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MSServiceUser : System.Web.Services.WebService
    {
        #region Enum

        private enum eAtivo
        {
            Inativo
            , Ativo
        }

        private enum eStatus
        {
            Error
            , Sucess
        }

        #endregion

        #region Properties

        // Validação SoapHeader
        public ServiceValidation validation;
        private const string wsToken = "cPNe8ezmm+ms+f31rvHsaiHDEDuRNTx/";

        #endregion

        #region Struct

        [Serializable]
        public struct Senha
        {
            [XmlElement]
            public string senha { get; set; }
            [XmlElement]
            public string ativo { get; set; }
            [XmlElement]
            public string status { get; set; }
            [XmlElement]
            public string erro { get; set; }
        }

        #endregion

        #region Constructor

        public MSServiceUser()
        {

        }

        #endregion

        #region WebMethods

        /// <summary>
        /// Altera senha e atualiza situação do Usuário apartir do login.
        /// </summary>
        /// <param name="login">Login do Usuário</param>
        /// <param name="senha">Senha nova do Usuário (Hash MD5)</param>
        /// <param name="situacao">Situação do Usuário (0-Inativo ou 1-Ativo)</param>
        /// <returns>Xml de resposta</returns>
        [SoapHeader("validation")]
        [WebMethod(EnableSession = true)]
        public Senha AlterarSenha(string login, string senha, string ativo)
        {
            Senha structSenha = new Senha();

            try
            {
                // Verifica Autenticação de acesso
                if (!VerificarAuthentication())
                    throw new ValidationException("Sem autorização de acesso ao Web Service.");

                SYS_Usuario entityUsuario = new SYS_Usuario();
                entityUsuario.ent_id = new Guid(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.ENTIDADE_PADRAO));
                entityUsuario.usu_login = login;
                // Carrega usuário apartir do email e entidade padrão
                if (SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(entityUsuario))
                {
                    // Configura senha do usuário
                    entityUsuario.usu_criptografia = Convert.ToByte(eCriptografa.MD5);
                    entityUsuario.usu_senha = senha;

                    // Configura situação do usuário
                    if ((entityUsuario.usu_situacao != Convert.ToByte(SYS_UsuarioBO.eSituacao.Bloqueado)) &&
                        (entityUsuario.usu_situacao != Convert.ToByte(SYS_UsuarioBO.eSituacao.Excluido)) &&
                        (entityUsuario.usu_situacao != Convert.ToByte(SYS_UsuarioBO.eSituacao.Padrao_Sistema)))
                    {
                        if (Convert.ToByte(ativo) == Convert.ToByte(eAtivo.Ativo))
                            entityUsuario.usu_situacao = Convert.ToByte(SYS_UsuarioBO.eSituacao.Ativo);
                        else
                            entityUsuario.usu_situacao = Convert.ToByte(SYS_UsuarioBO.eSituacao.Senha_Expirada);
                    }

                    // Salva alterações do usuário
                    if (!SYS_UsuarioBO.AlterarSenhaAtualizarUsuario(entityUsuario, false, false))
                        throw new Exception("Não foi possível atualizar usuário.");
                }
                else
                    throw new Exception("Não foi possível encontrar usuário.");

                // Configura Resposta
                structSenha.senha = senha;
                structSenha.ativo = ativo;
                structSenha.status = Convert.ToByte(eStatus.Sucess).ToString();
                structSenha.erro = string.Empty;

                // Grava Log 
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, String.Format("usu_id: {0}, alteração de senha via Web Service.", entityUsuario.usu_id));

                return structSenha;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                // Configura Resposta
                structSenha.senha = senha;
                structSenha.ativo = ativo;
                structSenha.status = Convert.ToByte(eStatus.Error).ToString();
                structSenha.erro = ex.Message;

                return structSenha;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Verifica autorização para acessar o Web Service.
        /// </summary>
        /// <returns></returns>
        private bool VerificarAuthentication()
        {
            bool flagAuthentication = ((validation != null) && (!string.IsNullOrEmpty(validation.WSToken)));
            if (flagAuthentication)
            {
                flagAuthentication = (wsToken.Equals(validation.WSToken, StringComparison.OrdinalIgnoreCase));
            }
            return flagAuthentication;
        }

        #endregion
    }

}
