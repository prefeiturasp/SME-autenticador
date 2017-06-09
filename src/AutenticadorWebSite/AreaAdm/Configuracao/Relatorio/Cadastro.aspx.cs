using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Autenticador.BLL;
using Autenticador.DAL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using CoreLibrary.Security.Cryptography;

public partial class AreaAdm_Configuracao_Relatorio_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.ExitPageConfirm));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsConfigRelatorio.js"));
            if (!Convert.ToString(this._btnCancelar.CssClass).Contains("btnMensagemUnload"))
                this._btnCancelar.CssClass += " btnMensagemUnload";
        }
        try
        {
            if (!IsPostBack)
            {
                lblObsoletoMsg.Text = UtilBO.GetErroMessage("Esta funcionalidade está obsoleta.", UtilBO.TipoMensagem.Alerta);

                if (this.__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_consultar)
                {
                    if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                        this._LoadData();
                    else
                        this._chkAlterarSenha.Visible = false;
                    this.ConfigDivSalvarDados();
                    Page.Form.DefaultButton = this._btnSalvar.UniqueID;
                    Page.Form.DefaultFocus = this._ddlSistema.ClientID;
                    if (this._VS_srr_id > 0)
                        this._btnSalvar.Visible = this.__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                    else
                        this._btnSalvar.Visible = this.__SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                }
                else
                {
                    Response.Redirect("~/AreaAdm/Configuracao/Relatorio/Busca.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
        catch (Exception err)
        {
            this._TratarErro(err, "Erro ao tentar carregar a página.");
        }
    }

    #region PROPRIEDADES
    
    /// <summary>
    /// Grava o id do servidor de relatório para editar o registro
    /// </summary>
    public int _VS_srr_id
    {
        get
        {
            if (ViewState["_VS_srr_id"] != null)
                return Convert.ToInt32(ViewState["_VS_srr_id"]);
            return -1;
        }
        set
        {
            ViewState["_VS_srr_id"] = value;
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
    protected void _TratarErro(Exception err, string message)
    {
        ApplicationWEB._GravaErro(err);
        this._lblMessage.Text = UtilBO.GetErroMessage(message, UtilBO.TipoMensagem.Erro);
    }
    /// <summary>
    /// Valida no servidor o campos obrigatório da tela.
    /// </summary>
    /// <returns>True para o preenchimento correto dos campos da página.</returns>
    public bool _IsValid()
    {
        bool valid;
        /* Valida os dados comum para o modo remoto e local */
        this._rfvSistema.Validate();
        this._rfvNome.Validate();
        this._rfvPastaRelatorios.Validate();
        valid = this._rfvSistema.IsValid
            && this._rfvNome.IsValid
            && this._rfvPastaRelatorios.IsValid;
        /* se ods dados comuns forem válidos e o modo for remoto valida o restante dos campos */
        if ((valid) && (this.divRemoteReport.Visible))
        {
            this._rfvUsuario.Validate();
            this._cpvSenha.Validate();
            this._rfvUrlRelatorios.Validate();
            valid = this._rfvUsuario.IsValid
                && this._cpvSenha.IsValid
                && this._rfvUrlRelatorios.IsValid;
        }
        return valid;
    }
    /// <summary>
    /// Carrega os controles da página no caso de edição dos dados.
    /// </summary>
    protected void _LoadData()
    {
        this._VS_srr_id = PreviousPage._Param_srr_id;
        this._ddlSistema.SelectedValue = PreviousPage._Param_sis_id.ToString();
        this._ddlSistema.Enabled = false;
        this.divSenha.Visible = false;

        CFG_ServidorRelatorio srv = new CFG_ServidorRelatorio()
        {
            ent_id = this.__SessionWEB.__UsuarioWEB.Usuario.ent_id, 
            sis_id = PreviousPage._Param_sis_id,
            srr_id = this._VS_srr_id
        };
        CFG_ServidorRelatorioBO.GetEntity(srv);
        this._txtNome.Text = srv.srr_nome;
        this._txtDescricao.Text = srv.srr_descricao;
        if (!srv.srr_remoteServer)
        {
            this._ddlLocalProcessamento.SelectedValue = "false";
            this.divRemoteReport.Visible = false;
        }
        else
        {
            this._txtUsuario.Text = srv.srr_usuario;
            this._txtDominio.Text = srv.srr_dominio;
            this._txtUrlRelatorios.Text = srv.srr_diretorioRelatorios;
        }
        this._txtPastaRelatorios.Text = srv.srr_pastaRelatorios;
        this._ddlSituacao.SelectedValue = srv.srr_situacao.ToString();
        /* Carrega a lista de relatórios selecionados para o servidor */
        this._chkRelatorios.DataBind();
        IList<CFG_RelatorioServidorRelatorio> ltRelatorioAssociados = CFG_ServidorRelatorioBO.ListarRelatorioDoServidor(srv.ent_id, srv.sis_id, srv.srr_id);
        foreach (ListItem item in this._chkRelatorios.Items)
        {
            foreach (CFG_RelatorioServidorRelatorio rel in ltRelatorioAssociados)
            {
                if (rel.rlt_id.Equals(int.Parse(item.Value)))
                {
                    item.Selected = true;
                    break;
                }
            }            
        }
    }

    protected void _Salvar()
    {
        if (this._IsValid())
        {
            CFG_ServidorRelatorioDAO dal = new CFG_ServidorRelatorioDAO();
            dal._Banco.Open(IsolationLevel.RepeatableRead);
            try
            {
                /* Remove os valores dos campos exclusivo para servidores do tipo remoto */
                if (!this.divRemoteReport.Visible)
                {
                    this._txtUsuario.Text = String.Empty;
                    this._txtSenha.Text = String.Empty;
                    this._txtConfirmaSenha.Text = String.Empty;
                    this._txtDominio.Text = String.Empty;
                    this._txtUrlRelatorios.Text = String.Empty;
                }
                /* Preenche o objeto a ser salvo */
                CFG_ServidorRelatorio srv = new CFG_ServidorRelatorio()
                {
                    ent_id = this.__SessionWEB.__UsuarioWEB.Usuario.ent_id,
                    sis_id = Convert.ToInt32(this._ddlSistema.SelectedValue),
                    srr_id = this._VS_srr_id
                };
                /* Caso o usuário queira alterar a senha */
                if ((!this._chkAlterarSenha.Visible) || (this._chkAlterarSenha.Checked))
                {
                    SymmetricAlgorithm encript = new SymmetricAlgorithm(CoreLibrary.Security.Cryptography.SymmetricAlgorithm.Tipo.TripleDES);
                    srv.srr_senha = encript.Encrypt(this._txtSenha.Text);
                }
                else
                    CFG_ServidorRelatorioBO.GetEntity(srv);

                srv.srr_nome = this._txtNome.Text;
                srv.srr_descricao = this._txtDescricao.Text;
                srv.srr_remoteServer = Convert.ToBoolean(this._ddlLocalProcessamento.SelectedValue);
                srv.srr_usuario = this._txtUsuario.Text;
                srv.srr_dominio = this._txtDominio.Text;
                srv.srr_diretorioRelatorios = this._txtUrlRelatorios.Text;
                srv.srr_pastaRelatorios = this._txtPastaRelatorios.Text;
                srv.srr_situacao = Convert.ToByte(this._ddlSituacao.SelectedValue);
                srv.srr_dataCriacao = DateTime.Now;
                srv.srr_dataAlteracao = DateTime.Now;
                srv.IsNew = (this._VS_srr_id.Equals(-1));
                bool ret = CFG_ServidorRelatorioBO.Save(srv, dal._Banco);
                if (ret)
                {
                    /* Insere os relatório pertencentes ao servidor de relatório a uma lista */
                    IList<CFG_RelatorioServidorRelatorio> lt = (from item in this._chkRelatorios.Items.Cast<ListItem>()
                                                                where item.Selected
                                                                select new CFG_RelatorioServidorRelatorio() { sis_id = srv.sis_id, ent_id = srv.ent_id, srr_id = srv.srr_id, rlt_id = int.Parse(item.Value) }).ToList();
                    if (lt.Count > 0)
                        CFG_ServidorRelatorioBO.SalvarRelatoriosDoServidor(lt, dal._Banco);
                    else
                        CFG_ServidorRelatorioBO.ApagarTodosRelatoriosServidor(srv.ent_id, srv.sis_id, srv.srr_id, dal._Banco);
                }
            }
            catch (Exception err)
            {
                dal._Banco.Close(err);
                throw;
            }
            finally
            {
                dal._Banco.Close();
                this.ConfigDivSalvarDados();
            }
        }
    }
    /// <summary>
    /// Configura o painel da mensagem de salvar
    /// </summary>
    protected void ConfigDivSalvarDados()
    {
        // Pega o tema da página que chamou.
        string tema = HttpContext.Current.Handler is Page ?
                    ((Page)HttpContext.Current.Handler).Theme ??
                    "Default" :
                    "Default";
        // Setar caminho da imagem, de acordo com o tipo de mensagem.
        string imagePath = VirtualPathUtility.ToAbsolute(String.Concat(String.Format("~/App_Themes/{0}/images/", tema), "warning.png"));
        this._lblMessageSalvar.Text = String.Empty;
        this.divSalvarDados.Style.Add("background", String.Format("#fff url(\"{0}\") no-repeat 45px 50%;", imagePath));
        this.divSalvarDados.Style.Add("display", "none");
    }

    #endregion

    protected void _ddlSistema_DataBound(object sender, EventArgs e)
    {
        if (_ddlSistema.Items.Count > 0)
            _ddlSistema.Items.Insert(0, new ListItem("-- Selecione um sistema --", "-1"));
        else
            _ddlSistema.Items.Insert(0, new ListItem("-- Não há sistemas disponíveis --", "-1"));
    }
    protected void _ddlLocalProcessamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.divRemoteReport.Visible = Convert.ToBoolean(this._ddlLocalProcessamento.SelectedValue);
    }
    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AreaAdm/Configuracao/Relatorio/Busca.aspx", false);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!(String.IsNullOrEmpty(this._txtSenha.Text.Trim()) && (this.divSenha.Visible)))
            {
                bool message = CFG_ServidorRelatorioBO.VerificarExisteServidor(
                    this.__SessionWEB.__UsuarioWEB.Usuario.ent_id
                    , Convert.ToInt32(this._ddlSistema.SelectedValue)
                    , this._VS_srr_id
                    , this._txtUrlRelatorios.Text
                    , this._txtPastaRelatorios.Text);
                if (!message)
                {
                    this._Salvar();
                    this.__SessionWEB.PostMessages = "Configurações do servidor de relatórios salvo com sucesso.";
                    Response.Redirect("~/AreaAdm/Configuracao/Relatorio/Busca.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    this._lblMessageSalvar.Text = "Já existe um servidor de relatório cadastrados com as configurações atuais.<br />Deseja realmente salvar as configurações atuais?";
                    this.divSalvarDados.Style.Add("display", "block");
                }
            }
            else
            {
                this._lblMessageSalvar.Text = "Campo senha não foi preenchido.<br />Deseja realmente salvar as configurações atuais?";
                this.divSalvarDados.Style.Add("display", "block");
            }

        }
        catch (Exception err)
        {
            this._TratarErro(err, "Erro ao tentar salvar as configurações");
        }
    }
    protected void _chkAlterarSenha_CheckedChanged(object sender, EventArgs e)
    {
        this.divSenha.Visible = this._chkAlterarSenha.Checked;
    }
    protected void _btnSim_Click(object sender, EventArgs e)
    {
        try
        {
            this._Salvar();
            this.__SessionWEB.PostMessages = "Configurações do servidor de relatórios salvo com sucesso.";
            Response.Redirect("~/AreaAdm/Configuracao/Relatorio/Busca.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception err)
        {
            this._TratarErro(err, "Erro ao tentar salvar as configurações");
        }
    }
    protected void odsRelatorios_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
            this._TratarErro(e.Exception, "Erro ao tentar listar os relatórios.");        
    }
    protected void _btnNão_Click(object sender, EventArgs e)
    {
        this.ConfigDivSalvarDados();
    }
}
