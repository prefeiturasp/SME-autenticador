using System;

using Autenticador.WebServices.Consumer.Services;
using Autenticador.Entities;
using Autenticador.DAL;
using CoreLibrary.Security.Cryptography;
using CoreLibrary.Validation.Exceptions;


namespace Autenticador.WebServices.Consumer
{
    public class ManageUserLive
    {
        #region Enumerador

        public enum eStatus
        {
            Error
            , Sucess
        }

        private enum eTipoIntegracaoExterna
        {
            Live = 1
        }

        #endregion

        #region Propriedades

        private ServiceUserLive WSUserLive;

        private SYS_IntegracaoExterna WSIntegracaoExterna;

        public SYS_IntegracaoExterna IntegracaoExterna
        {
            get { return WSIntegracaoExterna; }
        }

        #endregion

        #region Construtores

        public ManageUserLive()
        {
            try
            {
               SYS_IntegracaoExternaDAO dao = new SYS_IntegracaoExternaDAO();
               WSIntegracaoExterna = dao.SelectBy_ine_tipo(Convert.ToByte(eTipoIntegracaoExterna.Live));
            }
            catch
            {
                WSIntegracaoExterna = new SYS_IntegracaoExterna();
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        ///  Verifica se existe configuração para acesso ao Web Service.
        /// </summary>
        /// <returns></returns>
        public bool ExistsIntegracaoExterna()
        {
            return (WSIntegracaoExterna.ine_id != Guid.Empty);
        }

        /// <summary>
        /// Verifica se email é conta de email do live.
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <returns></returns>
        public bool IsContaEmail(string email)
        {
            if (string.IsNullOrEmpty(WSIntegracaoExterna.ine_dominio))
                return false;

            email = email.Trim();
            string dominio = email.Substring(email.IndexOf('@') + 1);
            return WSIntegracaoExterna.ine_dominio.Equals(dominio);
        }

        /// <summary>
        /// Configura Objeto UserLive apartir do retorno do Web Service.
        /// </summary>
        /// <param name="conta">Estrutura contendo os dados do email do Usuário</param>
        /// <param name="user">Objeto UserLive</param>
        /// <returns></returns>
        private bool ProcessesResult(ContaLive conta, UserLive user)
        {
            bool status = conta.status == Convert.ToByte(eStatus.Sucess);
            if (status)
            {
                // Configura login e email
                if (!string.IsNullOrEmpty(conta.email))
                {
                    user.login = conta.email;
                    user.email = conta.email;
                }

                // Configura senha
                if (!string.IsNullOrEmpty(conta.senha))
                    user.senha = conta.senha;

                // Configura situação
                if (conta.situacao > 0)
                    user.situacao = conta.situacao;
            }
            return status;
        }

        /// <summary>
        ///  Configura acesso ao Web Service.
        /// </summary>
        /// <returns></returns>
        private void ConfigurarService()
        {
            if (this.WSUserLive == null)
            {
                this.WSUserLive = new ServiceUserLive();

                // Verifica se existe a configuração necessária para acesso ao Web Service
                if (!this.ExistsIntegracaoExterna())
                {
                    throw new ValidationException("Não foi possível encontrar as configuração para integração externa.");
                }
                if (string.IsNullOrEmpty(this.WSIntegracaoExterna.ine_urlInterna) || string.IsNullOrEmpty(this.WSIntegracaoExterna.ine_tokenInterno))
                {
                    throw new ArgumentException("Url interna ou Token interno de acesso ao Web Service está nulo ou em branco.");
                }

                // Configura Web Service
                this.WSUserLive.Url = this.WSIntegracaoExterna.ine_urlInterna;

                // Configura Token do Web Service
                SymmetricAlgorithm encript = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES);
                ServiceValidation authentication = new ServiceValidation
                    { WSToken = encript.Encrypt(this.WSIntegracaoExterna.ine_tokenInterno) };
                this.WSUserLive.ServiceValidationValue = authentication;
            }
        }

        #region Methods Web Service

        /// <summary>
        /// Acessa Web Service e cria conta de email do Usuário apartir do tipo de usuário
        /// </summary>
        /// <param name="user">Objeto UserLive (obrigatório tipo de usuário)</param>
        /// <returns></returns>
        public bool CriarContaEmail(UserLive user)
        {
            switch (user.TipoUserLive)
            {
                case eTipoUserLive.Aluno:
                    {
                        return CriarContaEmailAluno(user);
                    }
                case eTipoUserLive.Colaborador:
                    {
                        return CriarContaEmailColaborador(user);
                    }
                case eTipoUserLive.Docente:
                    {
                        return CriarContaEmailDocente(user);
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        /// <summary>
        /// Acessa Web Service e cria conta de email do Usuário Aluno.
        /// </summary>
        /// <param name="user">Objeto UserLive (obrigatório dados do usuário aluno)</param>
        /// <returns></returns>
        private bool CriarContaEmailAluno(UserLive user)
        {
            this.ConfigurarService();

            // Valida os dados do usuário Aluno
            if (!user.Validate())
            {
                throw new ArgumentException("Dados do aluno é obrigatório");
            }

            // Chamada ao WebService
            ContaLive conta = WSUserLive.CriarContaEmailAluno(user.dadosUserAluno.nome, user.dadosUserAluno.matricula, user.dadosUserAluno.escola, user.dadosUserAluno.turma, user.dadosUserAluno.serie);

            // Configura retorno
            if (!ProcessesResult(conta, user))
            {
                throw new Exception(conta.erro);
            }

            return true;
        }

        /// <summary>
        /// Acessa Web Service e cria conta de email do Usuário Docente.
        /// </summary>
        /// <param name="user">Objeto UserLive (obrigatório dados do usuário docente)</param>
        /// <returns></returns>
        private bool CriarContaEmailDocente(UserLive user)
        {
            this.ConfigurarService();

            // Valida os dados do usuário Docente
            if (!user.Validate())
            {
                throw new ArgumentException("Dados do docente é obrigatório");
            }

            // Chamada ao WebService
            ContaLive conta = WSUserLive.CriarContaEmailDocente(user.dadosUserDocente.nome, user.dadosUserDocente.matricula, user.dadosUserDocente.escola, user.dadosUserDocente.turma, user.dadosUserDocente.serie, user.dadosUserDocente.CPF, user.dadosUserDocente.disciplina);

            // Configura retorno
            if (!ProcessesResult(conta, user))
            {
                throw new Exception(conta.erro);
            }

            return true;
        }

        /// <summary>
        /// Acessa Web Service e cria conta de email do Usuário Colaborador.
        /// </summary>
        /// <param name="user">Objeto UserLive (obrigatório dados do usuário colaborador)</param>
        /// <returns></returns>
        private bool CriarContaEmailColaborador(UserLive user)
        {
            this.ConfigurarService();

            // Valida os dados do usuário Colaborador
            if (!user.Validate())
            {
                throw new ArgumentException("Dados do colaborador é obrigatório");
            }

            // Chamada ao WebService
            ContaLive conta = WSUserLive.CriarContaEmailColaborador(user.dadosUserColaborador.nome, user.dadosUserColaborador.CPF, user.dadosUserColaborador.cargo, user.dadosUserColaborador.funcao, user.dadosUserColaborador.setor);

            // Configura retorno
            if (!ProcessesResult(conta, user))
            {
                throw new Exception(conta.erro);
            }

            return true;
        }

        /// <summary>
        /// Acessa Web Service e verifica se existe a conta de email do Usuário.
        /// </summary>
        /// <param name="user">Objeto UserLive (obrigatório email)</param>
        /// <returns></returns>
        public bool VerificarContaEmailExistente(UserLive user)
        {
            this.ConfigurarService();

            // Validação de dados
            if (string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("E-mail é obrigatório");
            }

            // Chamada ao WebService
            ContaLive conta = WSUserLive.VerificarContaEmailExistente(user.email);
            return ProcessesResult(conta, user);
        }

        /// <summary>
        /// Acessa Web Service e altera senha da conta de email do Usuário.
        /// </summary>
        /// <param name="user">Objeto UserLive (obrigatório email, senha (Hash MD5), situacao)</param>
        /// <returns></returns>
        public bool AlterarContaEmailSenha(UserLive user)
        {
            this.ConfigurarService();

            // Validação de dados
            if (string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("E-mail é obrigatório");
            }
            if (string.IsNullOrEmpty(user.senha))
            {
                throw new ArgumentException("Senha é obrigatório");
            }
            if (user.situacao <= 0)
            {
                throw new ArgumentException("Situação é obrigatório");
            }

            // Chamada ao WebService
            ContaLive conta = WSUserLive.AlterarContaEmailSenha(user.email, user.senha, user.situacao.ToString());
            if (!ProcessesResult(conta, user))
            {
                throw new Exception(conta.erro);
            }

            return true;
        }

        #endregion

        #endregion
    }
}
