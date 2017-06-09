using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using CoreLibrary.Validation.Exceptions;
using System.Data;
using Autenticador.Entities;

namespace AutenticadorWebSite.AreaAdm.Configuracao.MensagemSistema
{
    public partial class Cadastro : MotherPageLogado
    {
        #region Eventos Page Life Cycle

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm != null)
            {
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
                sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
            }

            if (!IsPostBack)
            {
                btnNovo.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvMensagem.DataBind();
            }
        }

        #endregion

        #region Eventos

        protected void grvMensagem_DataBinding(object sender, EventArgs e)
        {
            try
            {
                GridView grv = ((GridView)sender);
                if (grv.DataSource == null)
                    grv.DataSource = SYS_MensagemSistemaBO.GetSelect();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar parâmetros de mensagem.", UtilBO.TipoMensagem.Erro);
                updMessage.Update();
            }
        }

        protected void grvMensagem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                if (imgEditar != null)
                    imgEditar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;

                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                if (imgExcluir != null)
                    imgExcluir.Visible = (__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir) &&
                        (Convert.ToByte(DataBinder.Eval(e.Row.DataItem, "mss_situacao")) != (Byte)SYS_MensagemSistemaSituacao.Interno);

            }
        }

        protected void grvMensagem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView grv = ((GridView)sender);
            grv.EditIndex = e.NewEditIndex;
            grv.DataBind();

            int pms_id = Convert.ToInt32(grv.DataKeys[e.NewEditIndex]["mss_id"].ToString());

            TextBox txtChave = (TextBox)grv.Rows[e.NewEditIndex].FindControl("txtChave");
            if (txtChave != null)
                txtChave.Enabled = pms_id <= 0;

            TextBox _txtValor = (TextBox)grv.Rows[e.NewEditIndex].FindControl("txtValor");
            if (_txtValor != null)
                _txtValor.Text = _txtValor.Text;

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

        protected void grvMensagem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView grv = ((GridView)sender);
            try
            {
                SYS_MensagemSistema entity = new SYS_MensagemSistema
                {
                    IsNew = Boolean.Parse(grv.DataKeys[e.RowIndex]["IsNew"].ToString())
                    ,
                    mss_id = Convert.ToInt32(grv.DataKeys[e.RowIndex]["mss_id"])
                    ,
                    mss_situacao = Byte.Parse(grv.DataKeys[e.RowIndex]["mss_situacao"].ToString())
                };

                TextBox txtChave = (TextBox)grvMensagem.Rows[e.RowIndex].FindControl("txtChave");
                if (txtChave != null)
                    entity.mss_chave = txtChave.Text;

                TextBox txtDescricao = (TextBox)grvMensagem.Rows[e.RowIndex].FindControl("txtDescricao");
                if (txtDescricao != null)
                    entity.mss_descricao = txtDescricao.Text;

                TextBox txtValor = (TextBox)grvMensagem.Rows[e.RowIndex].FindControl("txtValor");
                if (txtValor != null)
                {
                    entity.mss_valor = txtValor.Text;
                }

                if (SYS_MensagemSistemaBO.Save(entity))
                {
                    if (Boolean.Parse(grv.DataKeys[e.RowIndex]["IsNew"].ToString()))
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "mss_id: " + entity.mss_id);
                        lblMessage.Text = UtilBO.GetErroMessage("Mensagem do sistema incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "mss_id: " + entity.mss_id);
                        lblMessage.Text = UtilBO.GetErroMessage("Mensagem do sistema alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
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
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar parâmetro.", UtilBO.TipoMensagem.Erro);
            }
            finally
            {
                updMessage.Update();
            }
        }

        protected void grvMensagem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView grv = ((GridView)sender);
            try
            {
                if (!Boolean.Parse(grv.DataKeys[e.RowIndex]["IsNew"].ToString()))
                {
                    SYS_MensagemSistema entity = new SYS_MensagemSistema
                    {
                        mss_id = Convert.ToInt32(grv.DataKeys[e.RowIndex]["mss_id"])
                        ,
                        mss_situacao = Byte.Parse(grv.DataKeys[e.RowIndex]["mss_situacao"].ToString())
                    };

                    if (SYS_MensagemSistemaBO.Delete(entity))
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "mss_id: " + entity.mss_id);
                        lblMessage.Text = UtilBO.GetErroMessage("Mensagem do sistema excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);

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
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir mensagem do sistema.", UtilBO.TipoMensagem.Erro);
            }
            finally
            {
                updMessage.Update();
            }
        }

        protected void grvMensagem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView grv = ((GridView)sender);
            grv.EditIndex = -1;
            grv.DataBind();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                List<SYS_MensagemSistema> parametros = SYS_MensagemSistemaBO.GetSelect().ToList();
                parametros.Add(new SYS_MensagemSistema
                {
                    IsNew = true
                    ,
                    mss_id = -1
                    ,
                    mss_chave = ""
                    ,
                    mss_descricao = ""
                    ,
                    mss_valor = ""
                    ,
                    mss_situacao = (Byte)SYS_MensagemSistemaSituacao.Ativo
                });

                int index = (parametros.Count - 1);
                grvMensagem.EditIndex = index;
                grvMensagem.DataSource = parametros;
                grvMensagem.DataBind();

                ImageButton imgEditar = (ImageButton)grvMensagem.Rows[index].FindControl("imgEditar");
                if (imgEditar != null)
                    imgEditar.Visible = false;
                ImageButton imgSalvar = (ImageButton)grvMensagem.Rows[index].FindControl("imgSalvar");
                if (imgSalvar != null)
                    imgSalvar.Visible = true;
                ImageButton imgCancelar = (ImageButton)grvMensagem.Rows[index].FindControl("imgCancelarParametro");
                if (imgCancelar != null)
                    imgCancelar.Visible = true;

                ImageButton imgExcluir = (ImageButton)grvMensagem.Rows[index].FindControl("imgExcluir");
                if (imgExcluir != null)
                    imgExcluir.Visible = false;

                string script = String.Format("SetConfirmDialogLoader('{0}','{1}');", String.Concat("#", imgExcluir.ClientID), "Confirma a exclusão?");
                Page.ClientScript.RegisterStartupScript(GetType(), imgExcluir.ClientID, script, true);

                grvMensagem.Rows[index].Focus();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar adicionar nova mensagem do sistema.", UtilBO.TipoMensagem.Erro);
                updMessage.Update();
            }
        }
        #endregion
    }
}