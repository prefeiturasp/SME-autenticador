using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject.WebArea;
using Autenticador.Web.WebProject.Authentication;

namespace Autenticador.Web.WebProject
{
    public class MotherPage : CoreLibrary.Web.WebProject.MotherPage
    {
        #region Propriedades

        public new SessionWEB __SessionWEB
        {
            get
            {
                return (SessionWEB)Session[CoreLibrary.Web.WebProject.ApplicationWEB.SessSessionWEB];
            }
            set
            {
                Session[CoreLibrary.Web.WebProject.ApplicationWEB.SessSessionWEB] = value;
            }
        }

        /// <summary>
        /// Retorna o nome do tema que está sendo utilizado.
        /// </summary>
        public string NomeTemaAtual
        {
            get
            {
                return Theme;
            }
        }

        /// <summary>
        /// Retorna o caminho virtual da pasta de logos.
        /// </summary>
        public string caminhoLogos
        {
            get
            {
                return "~/App_Themes/" + NomeTemaAtual + "/images/logos/";
            }
        }

        /// <summary>
        /// Retorna o caminho virtual da pasta de imagens.
        /// </summary>
        public string caminhoImagens
        {
            get
            {
                return "~/App_Themes/" + NomeTemaAtual + "/images/";
            }
        }

        /// <summary>
        /// Retorna o caminho físico da pasta de logotipos do sistema.
        /// </summary>
        public string CaminhoFisicoLogos
        {
            get
            {
                return Server.MapPath(caminhoLogos);
            }
        }

        #endregion Propriedades

        #region Eventos Page Life Cycle

        /**********/

        protected virtual void Page_PreInit(object sender, EventArgs e)
        {
            if (this.Page.EnableTheming)
            {
                Page.Theme = __SessionWEB.TemaPadraoLogado.tep_nome;
            }
            else
            {
                this.Page.Theme = String.Empty;
            }
        }

        /*********/

        protected override void OnInit(EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("DirVirtual"))
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "DirVirtual", "<script type='text/javascript'>var diretorioVirtual ='" + Page.ResolveClientUrl("~") + "App_Themes/" + NomeTemaAtual + "/images/" + "';</script>");

            base.OnInit(e);
            __SessionWEB._AreaAtual = new Area();

            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm != null)
            {
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryCore));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryNotification));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryUI));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryScrollTo));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.StylesheetToggle));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.AutiFill));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.Util));                
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQuerySignalRNotification));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.PluginNotification));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            string dirIncludes = __SessionWEB._AreaAtual._DiretorioIncludes;
            Page.Header.Controls.Add(UtilBO.SetStyleHeader(dirIncludes, "altoContraste.css", true));

            //Inicia tag que checa se o browser é IE6
            LiteralControl ifIE6 = new LiteralControl("<!--[if IE 6]>");
            Page.Header.Controls.Add(ifIE6);

            //Adiciona css para IE6
            Page.Header.Controls.Add(UtilBO.SetStyleHeader(dirIncludes, "cssIE6.css", false));

            //Fecha tag que checa se o browser é IE6
            LiteralControl endifIE6 = new LiteralControl("<![endif]-->");
            Page.Header.Controls.Add(endifIE6);

            if (__SessionWEB.__UsuarioWEB.Grupo != null)
            {
                __SessionWEB.__UsuarioWEB.GrupoPermissao = SYS_GrupoBO.GetGrupoPermissaoBy_url(
                    __SessionWEB.__UsuarioWEB.Grupo.gru_id
                    , HttpContext.Current.Request.CurrentExecutionFilePath
                );
                SYS_Modulo mod = new SYS_Modulo
                {
                    mod_id = __SessionWEB.__UsuarioWEB.GrupoPermissao.mod_id,
                    sis_id = __SessionWEB.__UsuarioWEB.GrupoPermissao.sis_id
                };
                HttpContext.Current.Session[SYS_Modulo.SessionName] = SYS_ModuloBO.GetEntity(mod);
            }

            Title = CoreLibrary.Web.WebProject.ApplicationWEB._TituloDasPaginas;

            base.OnLoad(e);

            // Adiciona Init.js, que carrega na tela todas as funções declaradas dos outros scripts.
            // Carregar sempre por último - depois de todos os outros Js da página.
            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm != null)
                sm.Scripts.Add(new ScriptReference(ArquivoJS.Init));
        }

        #endregion Eventos Page Life Cycle

        #region Métodos

        /// <summary>
        /// Retorna se o usuário está autenticado no sistema, verificando a SessionWEB e o FormsIdentity
        /// </summary>
        /// <returns>True - está autenticado | False - não está autenticado</returns>
        protected bool UserIsAuthenticated()
        {
            if (!User.Identity.IsAuthenticated)
                return false;

            bool ret;
            if (!UsuarioWebIsValid())
            {
                GetFormsIdentityLoadSession();

                // Verifica se a SessionWeb foi carregada
                ret = UsuarioWebIsValid();
            }
            else
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Verifica se a Session do usuário está nula,
        /// se estiver verifica o FormsIdentity e carrega a Session
        /// </summary>
        private void GetFormsIdentityLoadSession()
        {
            try
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var usuarioCore = SignHelper.ObterUsuarioDoClaimsIdentity();
                    LoadSession(usuarioCore);
                    var grupo = SignHelper.ObterGrupoDoUsuarioDoClaimsIdentity();
                    if (grupo != null)
                    {
                        __SessionWEB.__UsuarioWEB.Grupo = grupo;
                    }

                    LoadSessionSistema();
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
            }
        }

        /// <summary>
        /// Verifica se a Session do usuário está carregada
        /// </summary>
        /// <returns>True - está carregada | False - não está carregada</returns>
        protected bool UsuarioWebIsValid()
        {
            if (__SessionWEB.__UsuarioWEB.Usuario != null)
                return (__SessionWEB.__UsuarioWEB.Usuario.usu_id != Guid.Empty);
            return false;
        }

        /// <summary>
        /// Configura a Session com os dados do sistema
        /// </summary>
        protected void LoadSessionSistema()
        {
            // Armazena o nome do sistema atual na Session
            SYS_Sistema entitySistema = new SYS_Sistema { sis_id = ApplicationWEB.SistemaID };
            SYS_SistemaBO.GetEntity(entitySistema);
            __SessionWEB.TituloSistema = entitySistema.sis_nome;

            // Armazena o caminho do logo do sistema atual
            __SessionWEB.UrlLogoSistema = __SessionWEB.LogoImagemCaminho + entitySistema.sis_urlLogoCabecalho;

            // Armazena caminho do logo e url do cliente na entidade logada (se existir)
            SYS_SistemaEntidade entitySistemaEntidade = new SYS_SistemaEntidade { sis_id = ApplicationWEB.SistemaID, ent_id = __SessionWEB.__UsuarioWEB.Usuario.ent_id };
            SYS_SistemaEntidadeBO.GetEntity(entitySistemaEntidade);

            if (!string.IsNullOrEmpty(entitySistemaEntidade.sen_urlCliente))
                __SessionWEB.UrlInstituicao = entitySistemaEntidade.sen_urlCliente;

            if (!string.IsNullOrEmpty(entitySistemaEntidade.sen_logoCliente))
                __SessionWEB.UrlLogoInstituicao = __SessionWEB.LogoImagemCaminho + entitySistemaEntidade.sen_logoCliente;
            else
                __SessionWEB.UrlLogoInstituicao = __SessionWEB.LogoImagemCaminho + "LOGO_CLIENTE.png";
        }

        protected void LoadSession(SYS_Usuario entityUsuario)
        {
            __SessionWEB.__UsuarioWEB.Usuario = entityUsuario;

            System.Web.Configuration.PagesSection pagesSection = System.Configuration.ConfigurationManager.GetSection("system.web/pages") as System.Web.Configuration.PagesSection;
            if ((pagesSection != null))
            {
                __SessionWEB.TemaPadraoLogado = CFG_TemaPadraoBO.CarregarPorNome(pagesSection.Theme);
            }

            // Armazena o cid_id referente a entidade do usuário na Session
            Guid ene_id = SYS_EntidadeEnderecoBO.Select_ene_idBy_ent_id(__SessionWEB.__UsuarioWEB.Usuario.ent_id);
            SYS_EntidadeEndereco entityEntidadeEndereco = new SYS_EntidadeEndereco { ent_id = __SessionWEB.__UsuarioWEB.Usuario.ent_id, ene_id = ene_id };
            SYS_EntidadeEnderecoBO.GetEntity(entityEntidadeEndereco);

            END_Endereco entityEndereco = new END_Endereco { end_id = entityEntidadeEndereco.end_id };
            END_EnderecoBO.GetEntity(entityEndereco);
            __SessionWEB._cid_id = entityEndereco.cid_id;

            // Armazena o nome da pessoa ou o login do usuário na Session
            PES_Pessoa EntityPessoa = new PES_Pessoa { pes_id = __SessionWEB.__UsuarioWEB.Usuario.pes_id };
            PES_PessoaBO.GetEntity(EntityPessoa);
            __SessionWEB.UsuarioLogado = string.IsNullOrEmpty(EntityPessoa.pes_nome) ? __SessionWEB.__UsuarioWEB.Usuario.usu_login : EntityPessoa.pes_nome;
        }

        /// <summary>
        /// Método de validação de campos data para ser usado em Validators.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void ValidarData_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime d;
            args.IsValid = DateTime.TryParse(args.Value, out d);
        }

        #endregion Métodos
    }
}