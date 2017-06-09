using System;
using System.Web.Services;
using Autenticador.WebServices.Adapter;
using Autenticador.Entities;
using Autenticador.DAL;
using Autenticador.BLL;
using System.Net;
using Services.RioEduca;
using Autenticador.Web.WebProject;

using System.Web.Services.Protocols;

using CoreLibrary.Validation.Exceptions;

namespace Services
{
    /// <summary>
    /// Summary description for RJService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceUserLive : System.Web.Services.WebService, IServiceUserLive
    {
        #region Enumerador

        private enum eWS
        {
            RIO_EDUCA
        }

        private enum eStatus
        {
            Error
            , Sucess
        }

        #endregion

        #region Propriedades

        private eWS wsChave { get; set; }

        // Configurações Web Service
        private SYS_IntegracaoExterna IntegracaoExterna;

        // Validação SoapHeader
        public ServiceValidation validation;
        private const string wsToken = "oedrtrxgRKhe+h4YrQQToQ==";

        // Web Service Allen
        private LoginWS ServiceRioEduca;

        #endregion

        #region Construtores

        public ServiceUserLive()
        {
            try
            {
                // Carrega configurações Web Service
                SYS_IntegracaoExternaDAO dao = new SYS_IntegracaoExternaDAO();
                IntegracaoExterna = dao.SelectBy_ine_tipo(Convert.ToByte(SYS_IntegracaoExternaBO.eTipoIntegracaoExterna.Live));
            }
            catch
            {
                IntegracaoExterna = new SYS_IntegracaoExterna();
            }
        }

        #endregion

        #region WebMethods

        /// <summary>
        /// Altera senha da conta de email do Usuário.
        /// </summary>
        /// <param name="email">Email do Usuário</param>
        /// <param name="novasenha">Senha nova do Usuário (Hash MD5)</param>
        /// <param name="situacao">Situação do Usuário</param>
        /// <returns></returns>
        [SoapHeader("validation")]
        [WebMethod(EnableSession = true)]
        public ContaLive AlterarContaEmailSenha(string email, string novasenha, string situacao)
        {
            try
            {
                this.ConfigurarService();

                ContaLive conta = new ContaLive();
                switch (wsChave)
                {
                    case eWS.RIO_EDUCA:
                        {
                            // 0(Inativo) ou 1(Ativo)
                            string ativo = (Convert.ToByte(situacao) == Convert.ToByte(SYS_UsuarioBO.eSituacao.Ativo) ? "1" : "0");

                            // Chamada ao WebService
                            Senha senha = ServiceRioEduca.AlterarSenhaConta(email, novasenha, ativo);
                            // Configura retorno do WebService
                            conta = ServiceAllen_AlterarSenhaContaCompleted(senha);
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Chave de acesso para integração externa não encontrada.");
                        }
                }

                // Verifica Status e grava Log 
                if (conta.status == Convert.ToByte(eStatus.Sucess))
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, String.Format("Conta live ( {0} ), alteração de senha.", email));
                return conta;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                // Configura retorno do WebService
                return Service_Error(ex);
            }
        }

        /// <summary>
        /// Cria conta de email do Usuário Aluno.
        /// </summary>
        /// <param name="nome">Nome do Usuário Aluno</param>
        /// <param name="matricula">Matricula do Usuário Aluno</param>
        /// <param name="escola">Escola do Usuário Aluno</param>
        /// <param name="turma">Turma do Usuário Aluno</param>
        /// <param name="serie">Série do Usuário Aluno</param>
        /// <returns></returns>
        [SoapHeader("validation")]
        [WebMethod(EnableSession = true)]
        public ContaLive CriarContaEmailAluno(string nome, string matricula, string escola, string turma, string serie)
        {
            try
            {
                this.ConfigurarService();

                ContaLive conta = new ContaLive();
                switch (wsChave)
                {
                    case eWS.RIO_EDUCA:
                        {
                            // Chamada ao WebService
                            Aluno aluno = ServiceRioEduca.CriarAluno(nome, matricula, escola, turma, serie);
                            // Configura retorno do WebService
                            conta = ServiceAllen_CriarAlunoCompleted(aluno);
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Chave de acesso para integração externa não encontrada.");
                        }
                }

                // Verifica Status e grava Log 
                if (conta.status == Convert.ToByte(eStatus.Sucess))
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, String.Format("Conta live ( {0} ), criada para o aluno {1}.", conta.email, nome));
                return conta;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                // Configura retorno do WebService
                return Service_Error(ex);
            }
        }

        /// <summary>
        /// Cria conta de email do Usuário Docente.
        /// </summary>
        /// <param name="nome">Nome do Usuário Docente</param>
        /// <param name="matricula">Matricula do Usuário Docente</param>
        /// <param name="escola">Escola do Usuário Docente</param>
        /// <param name="turma">Turma do Usuário Docente</param>
        /// <param name="serie">Série do Usuário Docente</param>
        /// <param name="CPF">CPF do Usuário Docente</param>
        /// <param name="disciplina">Disciplina lecionada pelo Usuário Docente</param>
        /// <returns></returns>
        [SoapHeader("validation")]
        [WebMethod(EnableSession = true)]
        public ContaLive CriarContaEmailDocente(string nome, string matricula, string escola, string turma, string serie, string CPF, string disciplina)
        {
            try
            {
                this.ConfigurarService();

                ContaLive conta = new ContaLive();
                switch (wsChave)
                {
                    case eWS.RIO_EDUCA:
                        {
                            // Chamada ao WebService
                            Professor professor = ServiceRioEduca.CriarProfessor(nome, matricula, escola, turma, serie, CPF, disciplina);
                            // Configura retorno do WebService
                            conta = ServiceAllen_CriarProfessorCompleted(professor);
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Chave de acesso para integração externa não encontrada.");
                        }
                }

                // Verifica Status e grava Log 
                if (conta.status == Convert.ToByte(eStatus.Sucess))
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, String.Format("Conta live ( {0} ), criada para o docente {1}.", conta.email, nome));
                return conta;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                // Configura retorno do WebService
                return Service_Error(ex);
            }
        }

        /// <summary>
        ///  Cria conta de email do Usuário Colaborador.
        /// </summary>
        /// <param name="nome">Nome do Usuário Colaborador</param>
        /// <param name="CPF">CPF do Usuário Colaborador</param>
        /// <param name="cargo">Cargo do Usuário Colaborador</param>
        /// <param name="funcao">Função do Usuário Colaborador</param>
        /// <param name="setor">Setor do Usuário Colaborador</param>
        /// <returns></returns>
        [SoapHeader("validation")]
        [WebMethod(EnableSession = true)]
        public ContaLive CriarContaEmailColaborador(string nome, string CPF, string cargo, string funcao, string setor)
        {
            try
            {
                this.ConfigurarService();

                ContaLive conta = new ContaLive();
                switch (wsChave)
                {
                    case eWS.RIO_EDUCA:
                        {
                            // Chamada ao WebService
                            Funcionario funcionario = ServiceRioEduca.CriarFuncionario(nome, CPF, cargo, funcao, setor);
                            // Configura retorno do WebService
                            conta = ServiceAllen_CriarFuncionarioCompleted(funcionario);
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Chave de acesso para integração externa não encontrada.");
                        }
                }

                // Verifica Status e grava Log 
                if (conta.status == Convert.ToByte(eStatus.Sucess))
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, String.Format("Conta live ( {0} ), criada para o colaborador {1}.", conta.email, nome));
                return conta;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);

                // Configura retorno do WebService
                return Service_Error(ex);
            }
        }

        /// <summary>
        /// Verifica se existe a conta de email do Usuário.
        /// </summary>
        /// <param name="email">Email do Usuário</param>
        [SoapHeader("validation")]
        [WebMethod(EnableSession = true)]
        public ContaLive VerificarContaEmailExistente(string email)
        {
            try
            {
                this.ConfigurarService();

                switch (wsChave)
                {
                    case eWS.RIO_EDUCA:
                        {
                            // Chamada ao WebService
                            Conta conta = ServiceRioEduca.ExisteContaEmail(email);
                            // Configura retorno do WebService
                            return ServiceAllen_ExisteContaEmailCompleted(conta);
                        }
                    default:
                        {
                            throw new ArgumentException("Chave de acesso para integração externa não encontrada.");
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Methods

        #region ServiceRioEduca

        private ContaLive ServiceAllen_ExisteContaEmailCompleted(Conta conta)
        {
            ContaLive contaLive = new ContaLive();
            contaLive.email = conta.email;
            contaLive.senha = conta.senha;
            contaLive.status = Convert.ToByte(conta.status);
            contaLive.erro = conta.msgErro;

            return contaLive;
        }

        private ContaLive ServiceAllen_CriarAlunoCompleted(Aluno aluno)
        {
            ContaLive contaLive = new ContaLive();
            contaLive.email = aluno.email;
            contaLive.senha = aluno.senha;
            contaLive.situacao = Convert.ToByte(SYS_UsuarioBO.eSituacao.Senha_Expirada);
            contaLive.status = Convert.ToByte(aluno.status);
            contaLive.erro = aluno.msgErro;

            return contaLive;
        }

        private ContaLive ServiceAllen_CriarProfessorCompleted(Professor professor)
        {
            ContaLive contaLive = new ContaLive();
            contaLive.email = professor.email;
            contaLive.senha = professor.senha;
            contaLive.situacao = Convert.ToByte(SYS_UsuarioBO.eSituacao.Senha_Expirada);
            contaLive.status = Convert.ToByte(professor.status);
            contaLive.erro = professor.msgErro;

            return contaLive;
        }

        private ContaLive ServiceAllen_CriarFuncionarioCompleted(Funcionario funcionario)
        {
            ContaLive contaLive = new ContaLive();
            contaLive.email = funcionario.email;
            contaLive.senha = funcionario.senha;
            contaLive.situacao = Convert.ToByte(SYS_UsuarioBO.eSituacao.Senha_Expirada);
            contaLive.status = Convert.ToByte(funcionario.status);
            contaLive.erro = funcionario.msgErro;

            return contaLive;
        }

        private ContaLive ServiceAllen_AlterarSenhaContaCompleted(Senha senha)
        {
            ContaLive contaLive = new ContaLive();
            contaLive.senha = senha.senha;
            contaLive.situacao = (senha.ativo.Equals("1") ? Convert.ToByte(SYS_UsuarioBO.eSituacao.Ativo) : Convert.ToByte(SYS_UsuarioBO.eSituacao.Senha_Expirada));
            contaLive.status = Convert.ToByte(senha.status);
            contaLive.erro = senha.msgErro;

            return contaLive;
        }

        #endregion

        private ContaLive Service_Error(Exception ex)
        {
            ContaLive contaLive = new ContaLive();
            contaLive.status = Convert.ToByte(eStatus.Error);
            contaLive.erro = ex.Message;

            return contaLive;
        }

        /// <summary>
        /// Configura Web Service.
        /// </summary>
        /// <returns></returns>
        private void ConfigurarService()
        {
            // Verifica Autenticação de acesso
            if (!VerificarAuthentication())
                throw new ValidationException("Sem autorização de acesso ao Web Service.");

            if (ServiceRioEduca == null)
            {
                ServiceRioEduca = new LoginWS();

                // Configura Web Service
                wsChave = (eWS)Enum.Parse(typeof(eWS), IntegracaoExterna.ine_chave, true);
                switch (wsChave)
                {
                    case eWS.RIO_EDUCA:
                        {
                            ServiceRioEduca.Url = IntegracaoExterna.ine_urlExterna;
                            break;
                        }
                    default:
                        {
                            throw new ArgumentException("Não foi possível encontrar as configuração para integração externa.");
                        }
                }
                // Configura Token do Web Service
                if (!string.IsNullOrEmpty(IntegracaoExterna.ine_tokenExterno))
                {
                    Validation validation = new Validation();
                    validation.AuthToken = IntegracaoExterna.ine_tokenExterno;
                    ServiceRioEduca.ValidationValue = validation;
                }
                // Configura Proxy do Web Service
                if (IntegracaoExterna.ine_proxy)
                {
                    WebProxy proxy;
                    SYS_IntegracaoExternaBO.GerarProxy(IntegracaoExterna, out proxy);
                    switch (wsChave)
                    {
                        case eWS.RIO_EDUCA:
                            {
                                ServiceRioEduca.Proxy = proxy;
                                break;
                            }
                        default:
                            {
                                throw new ArgumentException("Não foi possível encontrar as configuração para integração externa.");
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Verifica autorização para acessar o Web Service.
        /// </summary>
        /// <returns></returns>
        private bool VerificarAuthentication()
        {
            bool flagAuthentication = ((validation != null) && (!string.IsNullOrEmpty(validation.WSToken)));
            if (flagAuthentication)
            {
                flagAuthentication = (wsToken.Equals(validation.WSToken));
            }
            return flagAuthentication;
        }

        #endregion
    }
}
