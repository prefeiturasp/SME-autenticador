using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;

public partial class AreaAdm_Configuracao_Relatorio_Busca : MotherPageLogado
{
    #region CONSTANTES

    /// <summary>
    /// Retorna qual é a coluna de exclusão do gridview _dgvServidor.
    /// Usado para quando a permissão de exclusão do usuário for revogada 
    /// a coluna seja suprimida.
    /// </summary>
    protected const int dgvServidorColumnExcluir = 3;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
        if (!IsPostBack)
        {
            lblObsoletoMsg.Text = UtilBO.GetErroMessage("Esta funcionalidade está obsoleta.", UtilBO.TipoMensagem.Alerta);
            fdsResultados.Visible = false;
            string postMessage = this.__SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(postMessage))
                this._lblMessage.Text = UtilBO.GetMessage(postMessage, UtilBO.TipoMensagem.Sucesso);
            this._btnNovo.Visible = this.__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            this._dgvServidor.Columns[dgvServidorColumnExcluir].Visible = this.__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
        }
    }

    #region PROPRIEDADES

    /// <summary>
    /// Retorna o valor do id do sistema da linha selecionada no gridview para edição.
    /// Usado pelo postbackUrl para informar a tela de cadastro qual o registro que será editado.
    /// </summary>
    public int _Param_sis_id
    {
        get
        {
            return Convert.ToInt32(this._dgvServidor.DataKeys[this._dgvServidor.EditIndex].Values["sis_id"].ToString());
        }
    }
    /// <summary>
    /// Retorna o valor do id do servidor de relatório da linha selecionada no gridview para edição.
    /// Usado pelo postbackUrl para informar a tela de cadastro qual o registro que será editado.
    /// </summary>
    public int _Param_srr_id
    {
        get
        {
            return Convert.ToInt32(this._dgvServidor.DataKeys[this._dgvServidor.EditIndex].Values["srr_id"].ToString());
        }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Grava o log de erro e exibe uma mensagem amigável 
    /// no label _lblMessage para o usuário.
    /// </summary>
    /// <param name="err">Exception</param>
    /// <param name="message">Mensagem amigável há ser exibida ao usuário</param>
    protected void _GravarErro(Exception err, string message)
    {
        ApplicationWEB._GravaErro(err);
        this._lblMessage.Text = UtilBO.GetErroMessage(message, UtilBO.TipoMensagem.Erro);
    }

    #endregion

    protected void odsServidor_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
            this._GravarErro(e.Exception, "Erro ao tentar listar o(s) servidor(es) de relatórios do sistema.");
    }
    protected void odsServidor_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (!IsPostBack)
        {
            e.Cancel = true;
        }
        else
        {
            if (e.ExecutingSelectCount)
                e.InputParameters.Clear();
        }
    }
    protected void _ddlSistema_DataBound(object sender, EventArgs e)
    {
        if (_ddlSistema.Items.Count > 0)
            _ddlSistema.Items.Insert(0, new ListItem("-- Selecione um sistema --", "-1"));
        else
            _ddlSistema.Items.Insert(0, new ListItem("-- Não há sistemas disponíveis --", "-1"));
    }
    protected void _btnPesquisa_Click(object sender, EventArgs e)
    {
        try
        {
            int pageSize;
            if (!Int32.TryParse(SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.QT_ITENS_PAGINACAO), out pageSize))
                pageSize = ApplicationWEB._Paginacao;
            this._dgvServidor.PageIndex = 0;
            this._dgvServidor.PageSize = pageSize;
            this._dgvServidor.DataBind();
            this.fdsResultados.Visible = true;
        }
        catch (Exception err)
        {
            this._GravarErro(err, "Erro ao tentar exibir o(s) dado(s) da pesquisa.");
        }
    }
    protected void _btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AreaAdm/Configuracao/Relatorio/Cadastro.aspx", false);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    protected void _dgvServidor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            LinkButton lkbSelect = (LinkButton)e.Row.FindControl("_lkbSelect");
            Label lblNomeServidor = (Label)e.Row.FindControl("_lblNomeServidor");
            if (lkbSelect != null && lblNomeServidor != null)
            {
                lkbSelect.Visible = this.__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
                lblNomeServidor.Visible = !this.__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar;
            }
            else
                throw new Exception("Não foi possível aplicar as permissões no usuário na tabela da consulta.");

            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void _dgvServidor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Delete"))
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);

                if (index > 0)
                {
                    CFG_ServidorRelatorio entity = new CFG_ServidorRelatorio
                    {
                        sis_id = Convert.ToInt32(_dgvServidor.DataKeys[index]["sis_id"])
                        ,
                        ent_id = new Guid(_dgvServidor.DataKeys[index]["ent_id"].ToString())
                        ,
                        srr_id = Convert.ToInt32(_dgvServidor.DataKeys[index]["sis_id"])
                    };

                    if (CFG_ServidorRelatorioBO.Delete(entity))
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("Configurações do servidor de relatórios excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, String.Format("sis_id: {0}, ent_id: {1}, srr_id: {2}", entity.sis_id, entity.ent_id, entity.srr_id));
                    }
                }
            }
            catch (Exception ex)
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir configurações do servidor de relatórios.", UtilBO.TipoMensagem.Erro);
                ApplicationWEB._GravaErro(ex);
            }
        }
    }
}
