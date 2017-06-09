using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace Autenticador.BLL
{

    public class SYS_UsuarioAutenticacaoExternaBO : BusinessBase<SYS_UsuarioDAO, SYS_Usuario>
    {

        #region ENUM
        /// <summary>
        /// Enumerador para o tipo de autenticação de do usuário
        /// </summary>
        public enum eVerificaTipoAutenticacao
        {
            WebService = 1,
            Autenticador = 2,
            UsuNaoEncontrado = 3
        }

        /// <summary>
        /// Enumerador para as ações de validação de usuario com Integração Externa
        /// </summary>
        public enum eValidacaoWebService
        {
            ValidarNoWebService = 1,
            ValidarComSenhaDoHistorico = 2,
            IntegrcacaoNaoEncontrada = 3
        }
        #endregion

        public SYS_UsuarioAutenticacaoExternaBO()
        {

        }


        public LoginStatus ValidarAutenticacao(SYS_Usuario entityUsuario)
        {

            eVerificaTipoAutenticacao validacaoStatus = eVerificaTipoAutenticacao.UsuNaoEncontrado;
            int indexAD = entityUsuario.usu_login.IndexOf('\\');

            if (indexAD <= 0)
            {
                validacaoStatus = VerificaTipoAutenticacao(entityUsuario);
            }
            else
            {
                validacaoStatus = eVerificaTipoAutenticacao.Autenticador;
            }

            LoginStatus status = LoginStatus.NaoEncontrado;

            //VERIFICA O TIPO DE VALIDAÇÃO
            if (validacaoStatus == eVerificaTipoAutenticacao.Autenticador)
            {
                // Checa as credenciais do usuário
                status = SYS_UsuarioBO.ValidarLogin(entityUsuario);
            }
            else if (validacaoStatus == eVerificaTipoAutenticacao.WebService)
            {

                eValidacaoWebService ValidacaoStatus = VerificaSenhaUsuarioWebService(entityUsuario);

                if (ValidacaoStatus == eValidacaoWebService.ValidarNoWebService)
                {
                    // TEM QUE VERIFICAR QUAL A INTEGRACAO EXTERNA E DIRECIONAR
                    status = ValidaUsuarioWebService(entityUsuario);
                }
                else if (ValidacaoStatus == eValidacaoWebService.ValidarComSenhaDoHistorico
                         && (ValidarUsuarioHistoricoSenha(entityUsuario) == LoginStatus.SenhaInvalida))
                {

                    status = ValidaUsuarioWebService(entityUsuario);

                }
                else if (ValidacaoStatus == eValidacaoWebService.IntegrcacaoNaoEncontrada)
                {
                    status = LoginStatus.Erro;
                }
            }

            return status;
        }



        /// <summary>
        /// Verifica qual o tipo da autenticação que será feita para validar o usuário
        /// </summary>
        /// <param name="usu">Objeto Usuário preenchido com login e senha(descriptografada)</param>
        /// <returns>Retorna tipo da autenticação: 1 [Webservice], 2 [CoreSSO] e 3 [Usuário não encontrado]</returns>
        public eVerificaTipoAutenticacao VerificaTipoAutenticacao(SYS_Usuario usu)
        {
            eVerificaTipoAutenticacao status = eVerificaTipoAutenticacao.Autenticador;
            string senha = usu.usu_senha;
            SYS_UsuarioDAO dao = new SYS_UsuarioDAO();

            if (dao.CarregarBy_ent_id_usu_login(usu))
            {
                usu.usu_senha = senha;
                //VERIFICA CAMPO USU_NTEGRACAOEXTERNA
                if (!String.IsNullOrEmpty(usu.usu_integracaoExterna.ToString()) && Convert.ToInt16(usu.usu_integracaoExterna.ToString()) > 0)
                {
                    status = eVerificaTipoAutenticacao.WebService;
                }
                else
                    status = eVerificaTipoAutenticacao.Autenticador;
            }
            else
                status = eVerificaTipoAutenticacao.UsuNaoEncontrado;

            return status;
        }

        /// <summary>
        /// Verifica se será necessário autenticar no webservice do cliente ou se utilizará senha salva no autenticador
        /// </summary>
        /// <param name="usu"> Objeto Usuário carregado do autenticador</param>
        /// <returns>Enum indicando onde será feita a validação: 1 [Webservice], 2 [Histórico de senhas] e 3 [Não encontrado a integração]</returns>
        public static eValidacaoWebService VerificaSenhaUsuarioWebService(SYS_Usuario usu)
        {
            eValidacaoWebService status = eValidacaoWebService.ValidarNoWebService;
            SYS_IntegracaoExternaTipoBO ietBO = new SYS_IntegracaoExternaTipoBO();
            SYS_IntegracaoExternaTipo ietEntity = new SYS_IntegracaoExternaTipo { iet_id = usu.usu_integracaoExterna };

            if (ietBO.getById(ietEntity))
            {
                Guid usu_id = usu.usu_id;
                DataTable dtSenha = SYS_UsuarioSenhaHistoricoBO.SelecionaUltimaSenha(usu_id, null);

                if (dtSenha.Rows.Count > 0)
                {
                    DateTime ush_data = Convert.ToDateTime(dtSenha.Rows[0]["max_data"].ToString());
                    TimeSpan a = DateTime.Now.Subtract(ush_data);

                    if (a.Days > ietEntity.iet_qtdeDiasAutenticacao)
                    {
                        status = eValidacaoWebService.ValidarNoWebService;
                    }
                    else
                    {
                        status = eValidacaoWebService.ValidarComSenhaDoHistorico;
                    }
                }
            }
            else
                status = eValidacaoWebService.IntegrcacaoNaoEncontrada;

            return status;
        }

        /// <summary>
        /// Validação do usuário no webservice do cliente
        /// </summary>
        /// <param name="usu">Objeto Usuário carregado do autenticador</param>
        /// <returns>Enum LoginStatus: 0 [Sucesso] ou  5[Erro]</returns>
        public static LoginStatus ValidaUsuarioWebService(SYS_Usuario usu)
        {
            LoginStatus status = LoginStatus.Sucesso;
            var result = string.Empty;

            #region

            string envelope = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ser='http://www.w3.org/1999/XMLSchema-instance'>" +
                                "<soapenv:Body>" +
                                "<ser:AuthenticateJAAS>" +
                                        "<user>?</user>" +
                                        "<password>?</password>" +
                                        "<encryption>0</encryption>" +
                                        "<parameters>" +
                                            "<!--Optional:-->" +
                                            "<flowInstanceID>?</flowInstanceID>" +
                                            "<!--Optional:-->" +
                                            "<flowName>?</flowName>" +
                                            "<!--Optional:-->" +
                                            "<pmEncrypted>?</pmEncrypted>" +
                                            "<!--Optional:-->" +
                                            "<pmUserName>" + usu.usu_login.ToString() + "</pmUserName>" +
                                            "<!--Optional:-->" +
                                            "<pmUserPassword>" + usu.usu_senha.ToString() + "</pmUserPassword>" +
                                        "</parameters>" +
                                    "</ser:AuthenticateJAAS>" +
                                "</soapenv:Body>" +
                             "</soapenv:Envelope>";

            #endregion

            try
            {
                string wsReturn = string.Empty;
                //string url = "http://senior:9090/g5-senior-services/cs/SyncMCWFUsers?WSDL";
                // string action = "http://senior:9090/g5-senior-services/cs/SyncMCWFUsers?op=AuthenticateJAAS";

                SYS_IntegracaoExternaDAO dao = new SYS_IntegracaoExternaDAO();

                IList<SYS_IntegracaoExterna> ieEntity = dao.Select();
                if (!String.IsNullOrEmpty(ieEntity[0].ine_urlExterna.ToString()))
                {
                    string action = ieEntity[0].ine_urlExterna.ToString();

                    System.Xml.XmlDocument soapEnvelop = new System.Xml.XmlDocument();

                    soapEnvelop.LoadXml(envelope);
                    System.Xml.XmlDocument soapEnvelopeXml = soapEnvelop;
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(action);

                    //webRequest.Headers.Add("SOAPAction", action);
                    webRequest.ContentType = @"text/xml;charset=""utf-8""";
                    webRequest.Accept = "text/xml";
                    webRequest.Method = "POST";
                    using (System.IO.Stream stream = webRequest.GetRequestStream())
                    {
                        soapEnvelopeXml.Save(stream);
                    }

                    IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
                    asyncResult.AsyncWaitHandle.WaitOne();

                    string soapResult;
                    string retornoWebService = string.Empty;
                    using (System.Net.WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                    {
                        using (System.IO.StreamReader rd = new System.IO.StreamReader(webResponse.GetResponseStream()))
                        {
                            soapResult = rd.ReadToEnd();
                            XmlDocument xml = new XmlDocument();

                            xml.LoadXml(soapResult);

                            //XmlNodeList xnList = xml.GetElementsByTagName("AuthenticateJAASResponse");
                            //XmlNodeList xnList = xml.GetElementsByTagName("AuthenticateJAASResult");
                            XmlNodeList xnList = xml.GetElementsByTagName("pmLogged");

                            foreach (XmlNode xn in xnList)
                            {
                                retornoWebService = xn.InnerText;
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(retornoWebService))
                    {
                        status = SalvarSenhaUsuario(usu, retornoWebService);
                    }
                    else
                    {
                        status = LoginStatus.Erro;
                    }
                }
                else
                {
                    status = LoginStatus.Erro;
                }
            }
            catch (Exception e)
            {
                status = LoginStatus.Erro;
            }

            return status;
        }

        /// <summary>
        /// Valida senha do usuário no histórico
        /// </summary>
        /// <param name="usu">Objeto Usuário carregado do autenticador</param>
        /// <returns>Enum LoginStatus : Sucesso, Expirado, Bloqueado e SenhaInvalida</returns>
        public static LoginStatus ValidarUsuarioHistoricoSenha(SYS_Usuario usu)
        {
            LoginStatus status = LoginStatus.Sucesso;
            DataTable dtSenha = SYS_UsuarioSenhaHistoricoBO.SelecionaUltimaSenha(usu.usu_id, null);

            if (dtSenha.Rows.Count > 0)
            {
                string senhaSalva = dtSenha.Rows[0]["ush_senha"].ToString();
                string senha = usu.usu_senha;

                eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(usu.usu_criptografia), true);

                if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
                {
                    criptografia = eCriptografa.TripleDES;
                }

                usu.usu_senha = UtilBO.CriptografarSenha(usu.usu_senha, criptografia);

                if (!senhaSalva.Equals(usu.usu_senha))
                    status = LoginStatus.SenhaInvalida;
                else if ((usu.usu_situacao == 2) || (usu.usu_situacao == 3))
                    status = LoginStatus.Bloqueado;
                else if (usu.usu_situacao == 5)
                    status = LoginStatus.Expirado;
                else
                    status = LoginStatus.Sucesso;
            }

            return status;
        }

        /// <summary>
        /// Salvar a senha do usuário no hitórico
        /// </summary>
        /// <param name="usu">Objeto Usuário carregado do autenticador</param>
        /// <param name="retornoWebService">Retorno do Webservice do cliente (por enquanto configurado o de Integração Externa : 0 [sucesso] e -1 [não encontrado / usuário ou senha não estão corretos])</param>
        /// <returns>Enum LoginStatus: 0 [Sucesso] ou  5[Erro]</returns>
        public static LoginStatus SalvarSenhaUsuario(SYS_Usuario usu, string retornoWebService)
        {
            SYS_UsuarioSenhaHistoricoDAO dao = new SYS_UsuarioSenhaHistoricoDAO();
            LoginStatus status = LoginStatus.Sucesso;

            try
            {
                if (retornoWebService.Equals("0"))
                {
                    string senhaDescriptografada = usu.usu_senha;
                    dao._Banco.Open(IsolationLevel.ReadCommitted);


                    eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), Convert.ToString(usu.usu_criptografia), true);

                    if (!Enum.IsDefined(typeof(eCriptografa), criptografia))
                    {
                        criptografia = eCriptografa.TripleDES;
                    }

                    usu.usu_senha = UtilBO.CriptografarSenha(usu.usu_senha, criptografia);

                    if (SYS_UsuarioSenhaHistoricoBO.Salvar(usu, dao._Banco))
                    {
                        status = LoginStatus.Sucesso;
                    }
                }
                else if (retornoWebService.Equals("-1"))
                {// CASO NÃO ENCONTRE O USUARIO OU A SENHA NÃO ESTEJA EM CONFORME
                    status = LoginStatus.NaoEncontrado;
                }
                else
                {// OUTRO ERRO
                    status = LoginStatus.Erro;
                }
            }
            catch (Exception e)
            {
                status = LoginStatus.Erro;
                dao._Banco.Close(e);
                // msg("Houve um erro na autenticação (SOAP).")
            }
            finally
            {
                dao._Banco.Close();
            }
            return status;
        }

    }
}
