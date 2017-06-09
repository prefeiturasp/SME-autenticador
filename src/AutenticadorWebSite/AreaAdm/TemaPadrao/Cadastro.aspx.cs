namespace AutenticadorWebSite.AreaAdm.TemaPadrao
{
    using System;
    using System.ComponentModel;
    using System.Data;
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

        #endregion

        #region Page Life Cycle

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarTela();

                if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                {
                    VS_tep_id = PreviousPage.EditItem;
                    LoadFromEntity();
                    btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                }
                else
                {
                    btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                }

                Page.Form.DefaultFocus = txtNome.ClientID;
                Page.Form.DefaultButton = btnSalvar.UniqueID;
            }
        }

        #endregion Page Life Cycle

        #region Métodos

        /// <summary>
        /// Método genérico para carregar combos.
        /// </summary>
        /// <typeparam name="T">Tipo genérico.</typeparam>
        /// <param name="cbo">DropDownLis a ser carregado.</param>
        /// <param name="mensagemSelecione">Mensagem de de selecionar uma opção.</param>
        public static void CarregarComboEnum<T>(DropDownList cbo, string mensagemSelecione = null)
        {
            if (cbo != null)
            {
                Type objType = typeof(T);
                FieldInfo[] propriedades = objType.GetFields();
                foreach (FieldInfo objField in propriedades)
                {
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])objField.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attributes.Length > 0)
                    {
                        cbo.Items.Add(new ListItem(attributes[0].Description, Convert.ToString(objField.GetRawConstantValue())));
                    }
                }

                if (!string.IsNullOrEmpty(mensagemSelecione))
                {
                    cbo.Items.Insert(0, new ListItem(mensagemSelecione, "0"));
                }
            }
        }

        /// <summary>
        /// Carrega os componentes da tela.
        /// </summary>
        private void InicializarTela()
        {
            CarregarComboEnum<CFG_TemaPadrao.eTipoMenu>(ddlTipoMenu, "-- Selecione um tipo de menu --");
            CarregarComboEnum<CFG_TemaPadrao.eTipoLogin>(ddlTipoLogin, "-- Selecione um tipo de login --");
        }

        /// <summary>
        /// Carrega os dados da tela de uma entidade de Tema Padrão.
        /// </summary>
        private void LoadFromEntity()
        {
            try
            {
                CFG_TemaPadrao entity = new CFG_TemaPadrao { tep_id = VS_tep_id };
                CFG_TemaPadraoBO.GetEntity(entity);

                txtNome.Text = entity.tep_nome;
                txtDescricao.Text = entity.tep_descricao;

                chkExibeLinkLogin.Checked = entity.tep_exibeLinkLogin;
                chkExibeLogoCliente.Checked = entity.tep_exibeLogoCliente;

                ddlTipoMenu.SelectedValue = entity.tep_tipoMenu.ToString();
                ddlTipoLogin.SelectedValue = entity.tep_tipoLogin.ToString();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMensagem.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tema padrão.", UtilBO.TipoMensagem.Erro);
            }
        }

        /// <summary>
        /// Salva os dados da tela em uma entidade de Tema Padrão.
        /// </summary>
        private void Salvar()
        {
            try
            {
                CFG_TemaPadrao entity = new CFG_TemaPadrao
                {
                    tep_id = VS_tep_id
                    ,
                    tep_nome = txtNome.Text
                    ,
                    tep_descricao = txtDescricao.Text
                    ,
                    tep_tipoMenu = Convert.ToByte(ddlTipoMenu.SelectedValue)
                    ,
                    tep_tipoLogin = Convert.ToByte(ddlTipoLogin.SelectedValue)
                    ,
                    tep_exibeLinkLogin = chkExibeLinkLogin.Checked
                    ,
                    tep_exibeLogoCliente = chkExibeLogoCliente.Checked
                    ,
                    IsNew = VS_tep_id <= 0
                };

                if (CFG_TemaPadraoBO.Save(entity))
                {
                    ApplicationWEB._GravaLogSistema(VS_tep_id > 0 ? LOG_SistemaTipo.Update : LOG_SistemaTipo.Insert, String.Format("tep_id: {0}", entity.tep_id.ToString()));
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Tema padrão foi {0} com sucesso.", VS_tep_id > 0 ? "alterado" : "incluído"), UtilBO.TipoMensagem.Sucesso);

                    Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TemaPadrao/Busca.aspx", false);
                }
                else
                {
                    lblMensagem.Text = UtilBO.GetErroMessage("Erro ao tentar salva o tema padrão.", UtilBO.TipoMensagem.Erro);
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
                lblMensagem.Text = UtilBO.GetErroMessage("Erro ao tentar salva o tema padrão.", UtilBO.TipoMensagem.Erro);
            }
        }

        #endregion Métodos

        #region Eventos

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TemaPadrao/Busca.aspx", false);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Salvar();
            }
        }

        #endregion Eventos
    }
}