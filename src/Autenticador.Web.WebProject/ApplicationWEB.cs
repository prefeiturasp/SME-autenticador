using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using CoreLibrary.Log;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Collections.Generic;
using Autenticador.QuartzProvider;
using Autenticador.QuartzProvider.SchedulerProviders;

namespace Autenticador.Web.WebProject
{
    public class ApplicationWEB : CoreLibrary.Web.WebProject.ApplicationWEB, IRequiresSessionState
    {
        #region Constantes

        public const string AppSistemaID = "appSistemaID";

        /// <summary>
        /// Constante com o valor Default em KB, para caso não esteja configurado no sistema.
        /// </summary>
        private const int TamanhoPadraoArquivo = 1024;

        /// <summary>
        /// Constante com o valor da chave que armazena o valor do tamanho máximo permitido upload de arquivo.
        /// </summary>
        public const string AppTamanhoMaximoArquivo = "AppTamanhoMaximoArquivo";

        /// <summary>
        /// Constante com o valor Default(Separados por ",") dos arquivos permitidos.
        /// </summary>
        private const string ArquivosPermitidosPadrao = ".doc,.docx,.pdf,.txt";

        /// <summary>
        /// Constante com o valor da chave que armazena lista dos arquivos permitidos para upload.
        /// </summary>
        public const string AppTiposArquivosPermitidos = "AppTiposArquivosPermitidos";

        /// <summary>
        /// Constante com o valor Default(Separados por ",") das imagens permitidos.
        /// </summary>
        private const string ImagensPermitidasPadrao = ".png,.jpg,.jpeg";

        /// <summary>
        /// Constante com o valor da chave que armazena lista dos tipos de imagens permitidos para upload.
        /// </summary>
        public const string AppTiposImagensPermitidas = "AppTiposImagensPermitidas";

        public const string AppSchedulerHost = "AppSchedulerHost";

        /// <summary>
        /// Constante com o valor da chave que armazena o remetente padrão de e-email enviado pelo sistema.
        /// </summary>
        public const string AppRemetenteEmail = "appRemetenteEmail";

        #endregion Constantes

        #region Propriedades

        protected new SessionWEB __SessionWEB
        {
            get { return ((SessionWEB)Session[SessSessionWEB]); }
            set { Session[SessSessionWEB] = value; }
        }

        /// <summary>
        /// Retorna as configurações do sistema
        /// </summary>
        public static IDictionary<string, CFG_Configuracao> Configuracoes
        {
            get
            {
                IDictionary<string, CFG_Configuracao> configuracoes = (IDictionary<string, CFG_Configuracao>)HttpContext.Current.Application["Config"];
                if ((configuracoes == null) || (configuracoes.Count == 0))
                {
                    configuracoes = new Dictionary<string, CFG_Configuracao>();
                    foreach (CFG_Configuracao p in CFG_ConfiguracaoBO.Consultar())
                        configuracoes.Add(p.cfg_chave, p);

                    try
                    {
                        HttpContext.Current.Application.Lock();
                        HttpContext.Current.Application["Config"] = configuracoes;
                    }
                    finally
                    {
                        HttpContext.Current.Application.UnLock();
                    }
                }
                return configuracoes;
            }
        }

        /// <summary>
        /// Retorna o tamanho máximo permitido do arquivo em KB
        /// </summary>
        public static int TamanhoMaximoArquivo
        {
            get
            {
                int tamanho = -1;
                CFG_Configuracao cfg;
                Configuracoes.TryGetValue(AppTamanhoMaximoArquivo, out cfg);
                if (cfg != null)
                {
                    int.TryParse(cfg.cfg_valor, out tamanho);
                }
                if (tamanho < 1)
                {
                    tamanho = TamanhoPadraoArquivo;
                }

                return tamanho;
            }
        }

        /// <summary>
        /// Retorna array com as extensões permitidas para upload de arquivos
        /// </summary>
        public static string[] TiposArquivosPermitidos
        {
            get
            {
                string arquivos = "";
                CFG_Configuracao cfg;
                Configuracoes.TryGetValue(AppTiposArquivosPermitidos, out cfg);
                if (cfg != null)
                {
                    arquivos = cfg.cfg_valor;
                }
                if (String.IsNullOrEmpty(arquivos))
                {
                    arquivos = ArquivosPermitidosPadrao;
                }
                return arquivos.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// Retorna array com as extensões permitidas para upload de imagens
        /// </summary>
        public static string[] TipoImagensPermitidas
        {
            get
            {
                string imagens = "";
                CFG_Configuracao cfg;
                Configuracoes.TryGetValue(AppTiposImagensPermitidas, out cfg);
                if (cfg != null)
                {
                    imagens = cfg.cfg_valor;
                }
                if (String.IsNullOrEmpty(imagens))
                {
                    imagens = ImagensPermitidasPadrao;
                }
                return imagens.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        ///  ID do Sistema
        /// </summary>
        public static int SistemaID
        {
            get { return Convert.ToInt32(Configuracoes[AppSistemaID].cfg_valor); }
        }

        /// <summary>
        /// Título das Páginas
        /// </summary>
        public new static string _TituloDasPaginas
        {
            get { return Configuracoes[AppTitle].cfg_valor; }
        }

        /// <summary>
        /// Host servidor de email a ser utilizado pelo sistema
        /// </summary>
        public new static string _EmailHost
        {
            get { return Configuracoes[AppEmailHost].cfg_valor; }
        }

        /// <summary>
        ///  Endereço de Email para suporte
        /// </summary>
        public new static string _EmailSuporte
        {
            get { return Configuracoes[AppEmailSuporte].cfg_valor; }
        }

        /// <summary>
        /// Paginação Padrão
        /// </summary>
        public new static int _Paginacao
        {
            get { return Convert.ToInt32(Configuracoes[AppPaginacao].cfg_valor); }
        }

        /// <summary>
        /// Página para se redirecionar quando houver erros no sistema
        /// </summary>
        public new static string _PaginaErro
        {
            get { return Configuracoes[AppPaginaErro].cfg_valor; }
        }

        /// <summary>
        /// Página para se redirecionar quando expirar a sessão do usuário
        /// </summary>
        public new static string _PaginaExpira
        {
            get { return Configuracoes[AppPaginaExpira].cfg_valor; }
        }

        /// <summary>
        /// Página de entrada do sistema
        /// </summary>
        public new static string _PaginaInicial
        {
            get { return Configuracoes[AppPaginaInicial].cfg_valor; }
        }

        /// <summary>
        /// Página para se redirecionar quando expirar a sessão do usuário
        /// </summary>
        public new static string _PaginaLogoff
        {
            get { return Configuracoes[AppPaginaLogOff].cfg_valor; }
        }

        /// <summary>
        /// Host para conectar no SchedulerProvider.
        /// </summary>
        public static string SchedulerHost
        {
            get
            {
                string url = "";
                if (Configuracoes.Keys.Contains(AppSchedulerHost))
                    url = Configuracoes[AppSchedulerHost].cfg_valor;
                return url;
            }
        }

        /// <summary>
        /// Interface para acesso aos dados e operações do scheduler do Quartz
        /// </summary>
        public static DefaultSchedulerDataProvider SchedulerDataProvider
        {
            get
            {
                return new DefaultSchedulerDataProvider(SchedulerProvider);
            }
        }

        /// <summary>
        /// Interface para acesso direto ao scheduler do Quartz utilizado na inicializado do serviço.
        /// </summary>
        private static ISchedulerProvider _schedulerProvider;

        public static ISchedulerProvider SchedulerProvider
        {
            get
            {
                if (_schedulerProvider == null)
                {
                    RemoteSchedulerProvider rm = new RemoteSchedulerProvider(SchedulerHost);
                    _schedulerProvider = rm;
                }

                return _schedulerProvider;
            }
        }

        /// <summary>
        /// Remetente padrão de e-mail enviado pelo sistema.
        /// </summary>
        public static string EmailRemetente
        {
            get
            {
                return Configuracoes.Keys.Contains(AppRemetenteEmail) ?
                    Configuracoes[AppRemetenteEmail].cfg_valor :
                    string.Empty;
            }
        }

        #endregion Propriedades

        #region Metodos

        protected override void Session_Start(object sender, EventArgs e)
        {
            __SessionWEB = new SessionWEB();
        }

        /// <summary>
        /// Retorna a coleção em uma string única
        /// </summary>
        /// <param name="Colecao">Coleção de dados</param>
        /// <param name="nomeColecao">Nome da coleção</param>
        /// <param name="listaNaoGravar">Lista com os itens que não devem ser retornados na string</param>
        /// <returns>String única com os itens da coleção</returns>
        private static string retornaListaColecao(System.Collections.Specialized.NameValueCollection Colecao, string nomeColecao, List<string> listaNaoGravar)
        {
            string infoRequest = "\r\n*********** " + nomeColecao + " ***********";
            for (int i = 0; i < Colecao.Count; i++)
            {
                if (!(listaNaoGravar.Exists(p => p == Colecao.AllKeys[i])))
                {
                    infoRequest += "\r\n | ";
                    infoRequest += Colecao.AllKeys[i] + ": ";
                    infoRequest += Colecao[i];
                }
            }

            return infoRequest;
        }

        /// <summary>
        /// Salva log de erro no banco de dados.
        /// Em caso de exceção salva em arquivo teste
        /// na pasta Log da raiz do site.
        /// </summary>
        /// <param name="ex">Exception</param>
        public new static void _GravaErro(Exception ex)
        {
            try
            {
                string path = String.Concat(_DiretorioFisico, "Log");
                LogError logError = new LogError(path);
                //Liga o método no delegate para salvar log no banco de dados.
                logError.SaveLogBD = delegate (string message)
                {
                    LOG_Erros entity = new LOG_Erros();
                    try
                    {
                        //Preenche a entidade com os dados necessário
                        entity.err_descricao = message;
                        entity.err_erroBase = ex.GetBaseException().Message;
                        entity.err_tipoErro = ex.GetBaseException().GetType().FullName;
                        entity.err_dataHora = DateTime.Now;
                        if (HttpContext.Current != null)
                        {
                            string infoRequest = "";
                            try
                            {
                                string naoGravar = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.LOG_ERROS_CHAVES_NAO_GRAVAR);
                                List<string> listaNaoGravar = new List<string>(naoGravar.Split(';'));

                                bool gravarQueryString;
                                Boolean.TryParse(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.LOG_ERROS_GRAVAR_QUERYSTRING), out gravarQueryString);
                                if (gravarQueryString)
                                {
                                    infoRequest += retornaListaColecao(HttpContext.Current.Request.QueryString, "QueryString", listaNaoGravar);
                                }

                                bool gravarServerVariables;
                                Boolean.TryParse(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.LOG_ERROS_GRAVAR_SERVERVARIABLES), out gravarServerVariables);
                                if (gravarServerVariables)
                                {
                                    infoRequest += retornaListaColecao(HttpContext.Current.Request.ServerVariables, "ServerVariables", listaNaoGravar);
                                }

                                bool gravarParams;
                                Boolean.TryParse(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.LOG_ERROS_GRAVAR_PARAMS), out gravarParams);
                                if (gravarParams)
                                {
                                    infoRequest += retornaListaColecao(HttpContext.Current.Request.Params, "Params", listaNaoGravar);
                                }
                            }
                            catch
                            {
                            }

                            entity.err_descricao = entity.err_descricao + infoRequest;

                            entity.err_ip = HttpContext.Current.Request.UserHostAddress;
                            entity.err_machineName = HttpContext.Current.Server.MachineName;
                            entity.err_caminhoArq = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
                            try
                            {
                                entity.err_browser = String.Concat(new[] { HttpContext.Current.Request.Browser.Browser, HttpContext.Current.Request.Browser.MajorVersion.ToString(), HttpContext.Current.Request.Browser.MinorVersionString });
                            }
                            catch
                            {
                                entity.err_browser = string.Empty;
                            }
                            if (HttpContext.Current.Session != null)
                            {
                                SessionWEB session = (SessionWEB)HttpContext.Current.Session[SessSessionWEB];
                                if (session != null)
                                {
                                    if (session.__UsuarioWEB.Usuario != null)
                                    {
                                        entity.usu_id = session.__UsuarioWEB.Usuario.usu_id;
                                        entity.usu_login = session.__UsuarioWEB.Usuario.usu_login;
                                    }
                                    if (session.__UsuarioWEB.Grupo != null)
                                    {
                                        SYS_Sistema sistema = new SYS_Sistema
                                        {
                                            sis_id = session.__UsuarioWEB.Grupo.sis_id
                                        };
                                        SYS_SistemaBO.GetEntity(sistema);
                                        entity.sis_id = sistema.sis_id;
                                        entity.sis_decricao = sistema.sis_nome;
                                    }
                                }
                            }
                        }
                        //Salva o log no banco de dados
                        LOG_ErrosBO.Save(entity);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                };
                logError.Log(ex, true);
            }
            catch { }
        }

        /// <summary>
        /// Grava log de sistema no banco de dados.
        /// </summary>
        /// <param name="acao">Ação executada pelo usuário</param>
        /// <param name="descricao">Descrição do log</param>
        /// <param name="usu_login">Login do usuário que tentou realizar a operação (utilizado para operações de login sem sucesso)</param>
        /// <returns>Informa se o log de sistema foi salvo com sucesso.</returns>
        public static Guid _GravaLogSistema(LOG_SistemaTipo acao, string descricao, string usu_login)
        {
            try
            {
                LOG_Sistema entity = new LOG_Sistema();
                entity.log_acao = Enum.GetName(typeof(LOG_SistemaTipo), acao);
                entity.log_dataHora = DateTime.Now;
                entity.log_descricao = descricao;
                if (HttpContext.Current != null)
                {
                    //Preenche dados do host do site
                    LOG_SistemaBO.GenerateLogID();
                    entity.log_id = new Guid(HttpContext.Current.Session[LOG_Sistema.SessionName].ToString());
                    entity.log_ip = HttpContext.Current.Request.UserHostAddress;
                    entity.log_machineName = HttpContext.Current.Server.MachineName;
                    if (HttpContext.Current.Session != null)
                    {
                        SessionWEB session = (SessionWEB)HttpContext.Current.Session[SessSessionWEB];
                        if (session != null)
                        {
                            //Preenche dados referente ao usuário
                            if (session.__UsuarioWEB != null && session.__UsuarioWEB.Usuario != null)
                            {
                                entity.usu_id = session.__UsuarioWEB.Usuario.usu_id;
                                entity.usu_login = session.__UsuarioWEB.Usuario.usu_login;
                            }
                            //Preenche dados referente ao grupo do usuário
                            if (session.__UsuarioWEB != null && session.__UsuarioWEB.Grupo != null)
                            {
                                //Preenche os dados do grupo
                                entity.gru_id = session.__UsuarioWEB.Grupo.gru_id;
                                entity.gru_nome = session.__UsuarioWEB.Grupo.gru_nome;
                                //Preenche os dados do sistema
                                SYS_Sistema sistema = new SYS_Sistema
                                {
                                    sis_id = session.__UsuarioWEB.Grupo.sis_id
                                };
                                SYS_SistemaBO.GetEntity(sistema);
                                entity.sis_id = sistema.sis_id;
                                entity.sis_nome = sistema.sis_nome;
                                //Preenche os dados do módulo
                                if (HttpContext.Current.Session[SYS_Modulo.SessionName] != null)
                                {
                                    SYS_Modulo modulo = (SYS_Modulo)HttpContext.Current.Session[SYS_Modulo.SessionName];
                                    entity.mod_id = modulo.mod_id;
                                    entity.mod_nome = modulo.mod_nome;
                                }
                                else
                                {
                                    entity.mod_id = 0;
                                    entity.mod_nome = string.Empty;
                                }
                                //Preenche as entidades e unidades administrativa do grupo
                                if (session.__UsuarioWEB.GrupoUA != null)
                                {
                                    //Formata a entidade no padrão JSON
                                    JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                                    entity.log_grupoUA = oSerializer.Serialize(session.__UsuarioWEB.GrupoUA);
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(usu_login) && string.IsNullOrEmpty(entity.usu_login))
                        {
                            // Se o usuário não está logado, e a variável de login foi informada, utiliza ela).
                            entity.usu_login = usu_login;
                        }
                    }
                }
                if (!LOG_SistemaBO.Save(entity))
                    throw new Exception("Não foi possível salvar o log do sistema.");
                if (HttpContext.Current != null)
                    HttpContext.Current.Session[LOG_Sistema.SessionName] = null;
                return entity.log_id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Grava log de sistema no banco de dados.
        /// </summary>
        /// <param name="acao">Ação executada pelo usuário</param>
        /// <param name="descricao">Descrição do log</param>
        /// <param name="usu_login">Login do usuário que tentou realizar a operação (utilizado para operações de login sem sucesso)</param>
        /// <returns>Informa se o log de sistema foi salvo com sucesso.</returns>
        public static Guid _GravaLogSistema(LOG_SistemaTipo acao, string descricao)
        {
            try
            {
                LOG_Sistema entity = new LOG_Sistema();
                entity.log_acao = Enum.GetName(typeof(LOG_SistemaTipo), acao);
                entity.log_dataHora = DateTime.Now;
                entity.log_descricao = descricao;
                if (HttpContext.Current != null)
                {
                    //Preenche dados do host do site
                    LOG_SistemaBO.GenerateLogID();
                    entity.log_id = new Guid(HttpContext.Current.Session[LOG_Sistema.SessionName].ToString());
                    entity.log_ip = HttpContext.Current.Request.UserHostAddress;
                    entity.log_machineName = HttpContext.Current.Server.MachineName;
                    if (HttpContext.Current.Session != null)
                    {
                        SessionWEB session = (SessionWEB)HttpContext.Current.Session[SessSessionWEB];
                        if (session != null)
                        {
                            //Preenche dados referente ao usuário
                            if (session.__UsuarioWEB != null && session.__UsuarioWEB.Usuario != null)
                            {
                                entity.usu_id = session.__UsuarioWEB.Usuario.usu_id;
                                entity.usu_login = session.__UsuarioWEB.Usuario.usu_login;
                            }
                            //Preenche dados referente ao grupo do usuário
                            if (session.__UsuarioWEB != null && session.__UsuarioWEB.Grupo != null)
                            {
                                //Preenche os dados do grupo
                                entity.gru_id = session.__UsuarioWEB.Grupo.gru_id;
                                entity.gru_nome = session.__UsuarioWEB.Grupo.gru_nome;
                                //Preenche os dados do sistema
                                SYS_Sistema sistema = new SYS_Sistema
                                {
                                    sis_id = session.__UsuarioWEB.Grupo.sis_id
                                };
                                SYS_SistemaBO.GetEntity(sistema);
                                entity.sis_id = sistema.sis_id;
                                entity.sis_nome = sistema.sis_nome;
                                //Preenche os dados do módulo
                                if (HttpContext.Current.Session[SYS_Modulo.SessionName] != null)
                                {
                                    SYS_Modulo modulo = (SYS_Modulo)HttpContext.Current.Session[SYS_Modulo.SessionName];
                                    entity.mod_id = modulo.mod_id;
                                    entity.mod_nome = modulo.mod_nome;
                                }
                                else
                                {
                                    entity.mod_id = 0;
                                    entity.mod_nome = string.Empty;
                                }
                                //Preenche as entidades e unidades administrativa do grupo
                                if (session.__UsuarioWEB.GrupoUA != null)
                                {
                                    //Formata a entidade no padrão JSON
                                    JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                                    entity.log_grupoUA = oSerializer.Serialize(session.__UsuarioWEB.GrupoUA);
                                }
                            }
                        }
                    }
                }
                if (!LOG_SistemaBO.Save(entity))
                    throw new Exception("Não foi possível salvar o log do sistema.");
                if (HttpContext.Current != null)
                    HttpContext.Current.Session[LOG_Sistema.SessionName] = null;
                return entity.log_id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Recarrega as configurações do sistema
        /// </summary>
        public static void RecarregarConfiguracoes()
        {
            IDictionary<string, CFG_Configuracao> configuracoes = new Dictionary<string, CFG_Configuracao>();
            foreach (CFG_Configuracao p in CFG_ConfiguracaoBO.Consultar())
                configuracoes.Add(p.cfg_chave, p);

            try
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["Config"] = configuracoes;
            }
            finally
            {
                HttpContext.Current.Application.UnLock();
            }
        }

        #endregion Metodos
    }
}