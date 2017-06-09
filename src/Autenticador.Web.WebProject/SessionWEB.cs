using System;
using System.Web;
using System.Web.UI;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject.WebArea;

namespace Autenticador.Web.WebProject
{
    public class SessionWEB : CoreLibrary.Web.WebProject.SessionWEB
    {
        #region ATRIBUTOS

        private string postMessages = string.Empty;

        private string usuarioLogado = string.Empty;
        private string tituloSistema = string.Empty;

        private readonly string tituloGeral = string.Empty;
        private readonly string mensagemCopyright = string.Empty;
        private readonly string urlSistemaAutenticador = string.Empty;

        private string logoImagemCaminho = string.Empty;
        private string urlInstituicao = string.Empty;
        private string urlLogoGeral = string.Empty;
        private string urlLogoSistema = string.Empty;
        private string urlLogoInstituicao = string.Empty;
        private string temaPadrao = string.Empty;

        private readonly string helpDeskContato = string.Empty;

        #endregion

        #region CONSTRUTORES

        public SessionWEB()
        {
            try
            {
                __UsuarioWEB = new UsuarioWEB();

                //Armazena titulo geral do sistema definido nos parâmetros do CoreSSO
                tituloGeral = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TITULO_GERAL);

                //Armazena URL do sistema administrativo definido nos parâmetros do CoreSSO
                urlSistemaAutenticador = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.URL_ADMINISTRATIVO);

                //Armazena URL do cliente definido nos parâmetros do CoreSSO
                urlInstituicao = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.URL_CLIENTE);

                //Armazena mensagem de copyright definido nos parâmetros do CoreSSO
                mensagemCopyright = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.MENSAGEM_COPYRIGHT);

                //Armazena contato do help desk definido nos parâmetros do CoreSSO
                helpDeskContato = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.HELP_DESK_CONTATO);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
            }
        }

        #endregion

        #region PROPRIEDADES

        /// <summary>
        /// Guarda em sessão a busca feita pelo usuário nas telas de cadastro.
        /// </summary>
        public Busca BuscaRealizada
        {
            get;
            set;
        }

        /// <summary>
        /// Retorna valor do ID da cidade referente a Entidade do usuário. 
        /// </summary>
        public Guid _cid_id { get; set; }

        public new UsuarioWEB __UsuarioWEB { get; set; }

        /// <summary>
        /// Recebe e atribui mensagens a serem transferidas em um post ou redirect.
        /// Ao ler a mensagens enviada a propriedade é automaticamente limpada.
        /// </summary>
        public string PostMessages
        {
            get
            {
                try
                {
                    return postMessages;
                }
                finally
                {
                    postMessages = string.Empty;
                }
            }
            set
            {
                postMessages = value;
            }
        }

        /// <summary>
        /// Armazena o nome da pessoa ou o login do         
        /// usuário logado no sistema
        /// </summary>
        public string UsuarioLogado
        {
            get
            {
                return usuarioLogado;
            }
            set
            {
                usuarioLogado = value;
            }
        }

        /// <summary>
        /// Armazena o tema do sistema atual
        /// Disponível no web.config do sistema atual        
        /// </summary>
        public string TemaPadrao
        {
            get
            {
                return temaPadrao;
            }
            set
            {
                temaPadrao = value;
            }
        }

        /// <summary>
        /// Retorna se o tema atual setado no web.config pertence à nova UI criada.
        /// Na pasta do tema, o nome termina com _coreUI.
        /// </summary>
        public bool TemaCoreUI
        {
            get
            {
                return TemaPadraoLogado.tep_tipoMenu == (byte)CFG_TemaPadrao.eTipoMenu.CoreUI_Menu;
            }
        }

        private CFG_TemaPadrao temaPadraoLogado = new CFG_TemaPadrao();

        /// <summary>
        /// Entidade do tema padrão do sistema.
        /// </summary>
        public CFG_TemaPadrao TemaPadraoLogado
        {
            get
            {
                if (temaPadraoLogado == null || string.IsNullOrEmpty(temaPadraoLogado.tep_nome))
                {
                    System.Web.Configuration.PagesSection pagesSection = System.Configuration.ConfigurationManager.GetSection("system.web/pages") as System.Web.Configuration.PagesSection;
                    if (pagesSection != null && !string.IsNullOrEmpty(pagesSection.Theme))
                    {
                        temaPadraoLogado = CFG_TemaPadraoBO.CarregarPorNome(pagesSection.Theme);

                        if (temaPadraoLogado.tep_id <= 0)
                        {
                            TemaPadraoSite temaPadraoSite = (TemaPadraoSite)Enum.Parse(typeof(TemaPadraoSite), pagesSection.Theme, true);

                            temaPadraoLogado = new CFG_TemaPadrao
                            {
                                tep_nome = pagesSection.Theme
                                ,
                                tep_tipoMenu = (byte)(pagesSection.Theme.EndsWith("_coreUI") ? CFG_TemaPadrao.eTipoMenu.CoreUI_Menu : CFG_TemaPadrao.eTipoMenu.Menu)
                                ,
                                tep_exibeLinkLogin = (temaPadraoSite == TemaPadraoSite.Novo) || (temaPadraoSite == TemaPadraoSite.SMESP) || (temaPadraoSite == TemaPadraoSite.IntranetSME)
                                ,
                                tep_tipoLogin = (byte)(((temaPadraoSite == TemaPadraoSite.Novo) || (temaPadraoSite == TemaPadraoSite.SMESP)) ?
                                                    CFG_TemaPadrao.eTipoLogin.SobrescreveLabel :
                                                    (temaPadraoSite == TemaPadraoSite.IntranetSME ? CFG_TemaPadrao.eTipoLogin.CorrigeLayout : CFG_TemaPadrao.eTipoLogin.SemTratamento))
                                ,
                                tep_exibeLogoCliente = (temaPadraoSite == TemaPadraoSite.SMESP)
                            };
                        }
                    }
                    else
                    {
                        temaPadraoLogado = new CFG_TemaPadrao();
                    }
                }

                return temaPadraoLogado;
            }

            set
            {
                temaPadraoLogado = value;
            }
        }

        /// <summary>
        /// Armazena o título geral do sistema
        /// Disponível na tabela de parâmetros do CoreSSO.        
        /// </summary>
        public string TituloGeral
        {
            get
            {
                return tituloGeral;
            }
        }

        /// <summary>
        /// Armazena o título do sistema atual
        /// Disponível na tabela SYS_Sitema do CoreSSO
        /// Depois de logado        
        /// </summary>
        public string TituloSistema
        {
            get
            {
                return tituloSistema;
            }
            set
            {
                tituloSistema = value;
            }
        }

        /// <summary>
        /// Armazena a mensagem de copyright
        /// Disponível na tabela de parâmetros do CoreSSO.        
        /// </summary>
        public string MensagemCopyright
        {
            get
            {
                return mensagemCopyright;
            }
        }

        /// <summary>
        /// Armazena a URL do sistema administrativo (Autenticador)
        /// Disponível na tabela de parâmetros do Autenticador.        
        /// </summary>
        public string UrlSistemaAutenticador
        {
            get
            {
                return urlSistemaAutenticador;
            }
        }

        /// <summary>
        /// Armazena a URL da Instituição
        /// Disponível na tabela de parâmetros do Autenticador.        
        /// </summary>
        public string UrlInstituicao
        {
            get
            {
                return urlInstituicao;
            }
            set
            {
                urlInstituicao = value;
            }
        }

        /// <summary>
        /// Armazena o contato do help desk do cliente
        /// Disponível na tabela de parâmetros do Autenticador.        
        /// </summary>
        public string HelpDeskContato
        {
            get
            {
                return HttpUtility.HtmlDecode(helpDeskContato);
            }
        }

        /// <summary>
        /// Armazena o caminho da pasta de logos               
        /// </summary>
        public string LogoImagemCaminho
        {
            get
            {
                if (String.IsNullOrEmpty(logoImagemCaminho))
                {
                    // Pega o tema da página que chamou.
                    temaPadrao = HttpContext.Current.Handler is Page ?
                                ((Page)HttpContext.Current.Handler).Theme ??
                                "Default" :
                                "Default";

                    logoImagemCaminho = "/" + temaPadrao + "/images/logos/";
                }

                return logoImagemCaminho;
            }
        }

        /// <summary>
        /// Armazena a url do logo geral no Sistema Administrativo     
        /// </summary>
        public string UrlLogoGeral
        {
            get
            {
                if (String.IsNullOrEmpty(urlLogoGeral))
                {
                    //Armazena URL do logo geral do sistema definido nos parâmetros do Autenticador
                    urlLogoGeral = LogoImagemCaminho + "LOGO_GERAL_SISTEMA.png";
                }

                return urlLogoGeral;
            }
            set
            {
                urlLogoGeral = value;
            }
        }

        /// <summary>
        /// Armazena a url do logo do sistema no Sistema Administrativo
        /// </summary>
        public string UrlLogoSistema
        {
            get
            {
                return urlLogoSistema;
            }
            set
            {
                urlLogoSistema = value;
            }
        }

        /// <summary>
        /// Armazena a url do logo da instituição no Sistema Administrativo
        /// </summary>
        public string UrlLogoInstituicao
        {
            get
            {
                if (String.IsNullOrEmpty(urlLogoInstituicao))
                {
                    //Armazena URL do logo do cliente definido nos parâmetros do Autenticador
                    urlLogoInstituicao = LogoImagemCaminho + "LOGO_CLIENTE.png";
                }

                return urlLogoInstituicao;
            }
            set
            {
                urlLogoInstituicao = value;
            }
        }

        /// <summary>
        /// Retorna informações da área onde o usuário está logado como 
        /// diretório virtual, de imagens e includes, titulo da página entre outros.
        /// </summary>
        public Area _AreaAtual { get; set; }

        /// <summary>
        /// Retorna o Id do sistema da query string 
        /// (caso o usuário tenha requerido acessar determinado sistema diretamente)
        /// </summary>
        public int SistemaID_QueryString
        {
            get;
            set;
        }

        /// <summary>
        /// Retorna o Id do sistema que solicitou o logout
        /// (caso o usuário tenha requerido acessar determinado sistema diretamente)
        /// </summary>
        public int SistemaIDLogout_QueryString
        {
            get;
            set;
        }

        /// <summary>
        /// Armazena a Url de retorno após completar logout
        /// (A Url é enviada pelo sistema que requisitou o logout através da querystring "SistemaUrlLogout")
        /// </summary>
        public string SistemaUrlLogout_QueryString
        {
            get;
            set;
        }

        /// <summary>
        /// Armazena a quantidade de sistemas que realizaram logout.
        /// Utilizada em conjunto com a SistemaUrlLogout_QueryString para saber
        /// se deve redirecionar para tela de login do Core ou para a Url enviada
        /// pelo sistema
        /// </summary>
        public int SistemasRequestLogout
        {
            get;
            set;
        }

        #endregion
    }
}
