namespace AutenticadorWebSite.AreaAdm.TemaPaleta
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Web.UI.WebControls;
    using Autenticador.BLL;
    using Autenticador.Entities;
    using Autenticador.Web.WebProject;
    using CoreLibrary.Validation.Exceptions;

    public partial class Cadastro : MotherPageLogado
    {
        #region Propriedades

        /// <summary>
        /// ViewStatque que armazena o ID do tema padrão.
        /// </summary>
        private int VS_tep_id
        {
            get
            {
                return Convert.ToInt32(ViewState["VS_tep_id"] ?? "-1");
            }

            set
            {
                ViewState["VS_tep_id"] = value;
            }
        }

        /// <summary>
        /// ViewStatque que armazena o ID do tema de cores.
        /// </summary>
        private int VS_tpl_id
        {
            get
            {
                return Convert.ToInt32(ViewState["VS_tpl_id"] ?? "-1");
            }

            set
            {
                ViewState["VS_tpl_id"] = value;
            }
        }

        /// <summary>
        /// ViewState que armazena o caminho raiz dos temas.
        /// </summary>
        private string VS_caminhoRaizTemas
        {
            get
            {
                return (ViewState["VS_caminhoRaizTemas"] ?? (ViewState["VS_caminhoRaizTemas"] = CFG_ConfiguracaoBO.SelecionaValorPorChave("AppCaminhoRaizTemasCoreUI"))).ToString();
            }
        }

        #endregion Propriedades

        #region Page Life Cycle

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarTela();

                if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                {
                    VS_tep_id = PreviousPage.EditItem_tep_id;
                    VS_tpl_id = PreviousPage.EditItem_tpl_id;
                    LoadFromEntity();

                    btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                }
                else
                {
                    btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                }

                Page.Form.DefaultFocus = UCComboTemaPadrao.QuantidadeItensCombo > 1 ? UCComboTemaPadrao.Combo_ClientID : txtNome.ClientID;
                Page.Form.DefaultButton = btnSalvar.UniqueID;
            }
        }

        #endregion Page Life Cycle

        #region Métodos

        /// <summary>
        /// Inicializa os componentes da tela de cadastro.
        /// </summary>
        private void InicializarTela()
        {
            try
            {
                UCComboTemaPadrao.Carregar();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMensagem.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tema de cores.", UtilBO.TipoMensagem.Erro);
            }
        }

        /// <summary>
        /// Carrega os dados do tema de cores na tela de cadastro.
        /// </summary>
        private void LoadFromEntity()
        {
            try
            {
                CFG_TemaPaleta entity = new CFG_TemaPaleta
                {
                    tep_id = VS_tep_id
                    ,
                    tpl_id = VS_tpl_id
                };
                CFG_TemaPaletaBO.GetEntity(entity);

                UCComboTemaPadrao.Valor = entity.tep_id;
                UCComboTemaPadrao.PermiteEditar = false;

                txtNome.Text = entity.tpl_nome;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMensagem.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tema de cores.", UtilBO.TipoMensagem.Erro);
            }
        }

        /// <summary>
        /// Salva os dados do tema de cores.
        /// </summary>
        private void Salvar()
        {
            try
            {
                CFG_TemaPaleta entity = new CFG_TemaPaleta
                {
                    tep_id = UCComboTemaPadrao.Valor
                    ,
                    tpl_id = VS_tpl_id
                    ,
                    tpl_nome = txtNome.Text
                    ,
                    tpl_caminhoCSS = Path.Combine(VS_caminhoRaizTemas, txtNome.Text)
                    ,
                    IsNew = VS_tpl_id <= 0
                };

                if (CFG_TemaPaletaBO.Save(entity))
                {
                    ApplicationWEB._GravaLogSistema(VS_tpl_id > 0 ? LOG_SistemaTipo.Update : LOG_SistemaTipo.Insert, String.Format("tep_id: {0} | tpl_id: {1}", entity.tep_id.ToString(), entity.tpl_id.ToString()));
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Tema de cores foi {0} com sucesso.", VS_tep_id > 0 ? "alterado" : "incluído"), UtilBO.TipoMensagem.Sucesso);

                    RedirecionaBusca();
                }
                else
                {
                    lblMensagem.Text = UtilBO.GetErroMessage("Erro ao tentar salva o tema de cores.", UtilBO.TipoMensagem.Erro);
                }
            }
            catch (DuplicateNameException ex)
            {
                lblMensagem.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch (ValidationException ex)
            {
                lblMensagem.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMensagem.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tema de cores.", UtilBO.TipoMensagem.Erro);
            }
        }

        /// <summary>
        /// Redireciona para a tela de busca de temas de cores.
        /// </summary>
        private void RedirecionaBusca()
        {
            Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TemaPaleta/Busca.aspx", false);
        }

        #endregion Métodos

        #region Eventos

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Salvar();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            RedirecionaBusca();
        }

        protected void cvPastaTema_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Válido se o caminho existe.
            args.IsValid = Directory.Exists(Server.MapPath(Path.Combine(VS_caminhoRaizTemas, txtNome.Text)));
        }

        #endregion

    }
}