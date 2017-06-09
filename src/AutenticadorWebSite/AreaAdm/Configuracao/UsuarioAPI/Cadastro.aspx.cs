namespace AutenticadorWebSite.AreaAdm.Configuracao.UsuarioAPI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Autenticador.BLL;
    using Autenticador.Entities;
    using Autenticador.Web.WebProject;
    using CoreLibrary.Security.Cryptography;
    using CoreLibrary.Validation.Exceptions;

    public partial class Cadastro : MotherPageLogado
    {
        #region Propriedades

        /// <summary>
        /// Armazena em viewstate a lista de usuários API.
        /// </summary>
        private List<CFG_UsuarioAPI> VS_ltUsuarioAPI
        {
            get
            {
                return (List<CFG_UsuarioAPI>)(ViewState["VS_ltUsuarioAPI"] ?? new List<CFG_UsuarioAPI>());
            }

            set
            {
                ViewState["VS_ltUsuarioAPI"] = value;
            }
        }

        /// <summary>
        /// Parâmetro com expressão regular para validação do formato da senha.
        /// </summary>
        public string parametroFormatoSenhaUsuario
        {
            get
            {
                return SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.FORMATO_SENHA_USUARIO);
            }
        }

        /// <summary>
        /// Parâmetro com expressão regular para validação do tamanho da senha.
        /// </summary>
        public string parametroTamanhoSenhaUsuario
        {
            get
            {
                return SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TAMANHO_SENHA_USUARIO);
            }
        }

        #endregion

        #region Page Life Cycle

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm != null)
                sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));

            if (!IsPostBack)
                btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
                grvUsuarioAPI.DataBind();
        }

        #endregion

        #region Eventos

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                List<CFG_UsuarioAPI> lt = VS_ltUsuarioAPI;

                lt.Add
                (
                    new CFG_UsuarioAPI
                    {
                        uap_id = -1
                        ,
                        uap_situacao = (byte)CFG_UsuarioAPI.eSituacao.Ativo
                        ,
                        IsNew = true
                        ,
                        uap_username = string.Empty
                        ,
                        uap_password = string.Empty
                    }
                );

                int index = (lt.Count - 1);
                grvUsuarioAPI.EditIndex = index;
                grvUsuarioAPI.DataSource = lt;
                grvUsuarioAPI.DataBind();

                DropDownList ddlSituacao = (DropDownList)grvUsuarioAPI.Rows[index].FindControl("ddlSituacao");
                if (ddlSituacao != null)
                {
                    ddlSituacao.SelectedValue = ((byte)CFG_UsuarioAPI.eSituacao.Ativo).ToString();
                    ddlSituacao.Visible = false;
                }

                Label lblSituacao = (Label)grvUsuarioAPI.Rows[index].FindControl("lblSituacao");
                if (lblSituacao != null)
                {
                    lblSituacao.Visible = true;
                    lblSituacao.Text = "Ativo";
                }

                ImageButton imgEditar = (ImageButton)grvUsuarioAPI.Rows[index].FindControl("imgEditar");
                if (imgEditar != null)
                    imgEditar.Visible = false;
                ImageButton imgSalvar = (ImageButton)grvUsuarioAPI.Rows[index].FindControl("imgSalvar");
                if (imgSalvar != null)
                    imgSalvar.Visible = true;
                ImageButton imgExcluir = (ImageButton)grvUsuarioAPI.Rows[index].FindControl("imgExcluir");
                if (imgExcluir != null)
                    imgExcluir.Visible = false;
                grvUsuarioAPI.Rows[index].Focus();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar adicionar novo usuário API.", UtilBO.TipoMensagem.Erro);
                updMessage.Update();
            }
        }

        protected void grvUsuarioAPI_DataBinding(object sender, EventArgs e)
        {
            try
            {
                GridView grv = ((GridView)sender);
                if (grv.DataSource == null)
                {
                    VS_ltUsuarioAPI = CFG_UsuarioAPIBO.SelecionaAtivos();
                    grv.DataSource = VS_ltUsuarioAPI;
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar usuários API.", UtilBO.TipoMensagem.Erro);
                updMessage.Update();
            }
        }

        protected void grvUsuarioAPI_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridView grv = ((GridView)sender);
                grv.EditIndex = e.NewEditIndex;
                grv.DataBind();

                byte uap_situacao = Convert.ToByte(grv.DataKeys[e.NewEditIndex]["uap_situacao"]);

                DropDownList ddlSituacao = (DropDownList)grv.Rows[e.NewEditIndex].FindControl("ddlSituacao");
                if (ddlSituacao != null)
                    ddlSituacao.SelectedValue = uap_situacao.ToString();
                
                ImageButton imgSalvar = (ImageButton)grv.Rows[e.NewEditIndex].FindControl("imgSalvar");
                if (imgSalvar != null)
                    imgSalvar.Visible = true;
                ImageButton imgEditar = (ImageButton)grv.Rows[e.NewEditIndex].FindControl("imgEditar");
                if (imgEditar != null)
                {
                    imgEditar.Visible = false;
                    ImageButton imgCancelar = (ImageButton)grv.Rows[e.NewEditIndex].FindControl("imgCancelar");
                    if (imgCancelar != null)
                        imgCancelar.Visible = true;
                }

                grv.Rows[e.NewEditIndex].Focus();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar editar os dados.", UtilBO.TipoMensagem.Erro);
                updMessage.Update();
            }
        }

        protected void grvUsuarioAPI_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView grv = ((GridView)sender);
            try
            {
                if (!Boolean.Parse(grv.DataKeys[e.RowIndex]["IsNew"].ToString()))
                {
                    CFG_UsuarioAPI entity = new CFG_UsuarioAPI
                    {
                        uap_id = Convert.ToInt32(grv.DataKeys[e.RowIndex]["uap_id"])
                        ,
                        uap_situacao = Convert.ToByte(grv.DataKeys[e.RowIndex]["uap_situacao"])
                    };

                    if (CFG_UsuarioAPIBO.Delete(entity))
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "uap_id: " + entity.uap_id);
                        lblMessage.Text = UtilBO.GetErroMessage("Usuário API excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                        grv.DataBind();
                    }
                }
            }
            catch (ValidationException ex)
            {
                lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir usuário API.", UtilBO.TipoMensagem.Erro);
            }
            finally
            {
                updMessage.Update();
            }
        }

        protected void grvUsuarioAPI_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView grv = ((GridView)sender);
            try
            {
                CFG_UsuarioAPI entity = new CFG_UsuarioAPI
                {
                    IsNew = Convert.ToBoolean(grv.DataKeys[e.RowIndex]["IsNew"])
                    ,
                    uap_id = Convert.ToInt32(grv.DataKeys[e.RowIndex]["uap_id"])
                    ,
                    uap_situacao = Convert.ToByte(grv.DataKeys[e.RowIndex]["uap_situacao"])
                };

                TextBox txtUsuario = (TextBox)grv.Rows[e.RowIndex].FindControl("txtUsuario");
                if (txtUsuario != null)
                    entity.uap_username = txtUsuario.Text;

                TextBox txtSenha = (TextBox)grv.Rows[e.RowIndex].FindControl("txtSenha");
                if (txtSenha != null)
                    entity.uap_password = txtSenha.Text;

                DropDownList ddlSituacao = (DropDownList)grv.Rows[e.RowIndex].FindControl("ddlSituacao");
                if (ddlSituacao != null)
                    entity.uap_situacao = Convert.ToByte(ddlSituacao.SelectedValue);

                if (Page.IsValid && CFG_UsuarioAPIBO.Save(entity))
                {
                    if (entity.IsNew)
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "uap_id: " + entity.uap_id);
                        lblMessage.Text = UtilBO.GetErroMessage("Usuário API incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "uap_id: " + entity.uap_id);
                        lblMessage.Text = UtilBO.GetErroMessage("Usuário API alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                    }

                    grv.EditIndex = -1;
                    grv.DataBind();
                }
            }
            catch (ValidationException ex)
            {
                lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch (DuplicateNameException ex)
            {
                lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar usuário API.", UtilBO.TipoMensagem.Erro);
            }
            finally
            {
                updMessage.Update();
            }
        }

        protected void grvUsuarioAPI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                byte uap_situacao = Convert.ToByte(DataBinder.Eval(e.Row.DataItem, "uap_situacao"));

                Label lblSituacao = (Label)e.Row.FindControl("lblSituacao");
                if (lblSituacao != null)
                {
                    switch (uap_situacao)
                    {
                        case (byte)CFG_UsuarioAPI.eSituacao.Ativo:
                            lblSituacao.Text = "Ativo";
                            break;
                        case (byte)CFG_UsuarioAPI.eSituacao.Inativo:
                            lblSituacao.Text = "Inativo";
                            break;
                    }
                }

                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                if (imgEditar != null)
                    imgEditar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;

                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                if (imgExcluir != null)
                    imgExcluir.Visible = (__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir);
            }
        }

        protected void grvUsuarioAPI_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView grv = ((GridView)sender);
            grv.EditIndex = -1;
            grv.DataBind();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Retorna a mensagem de erro da validação do tamanho da senha.
        /// </summary>
        /// <returns></returns>
        public string RetornaErrorMessageTamanho()
        {
            return String.Format("A senha deve conter {0}.", UtilBO.GetMessageTamanhoByRegex(parametroTamanhoSenhaUsuario));
        }

        /// <summary>
        /// O método descriptografa a senha salva no banco.
        /// </summary>
        /// <param name="uap_password">Senha criptgrafada.</param>
        /// <returns></returns>
        public string RetornaSenha(string uap_password)
        {
            return string.IsNullOrEmpty(uap_password) ?
                string.Empty :
                new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES).Decrypt(uap_password); 
        }

        #endregion
    }
}