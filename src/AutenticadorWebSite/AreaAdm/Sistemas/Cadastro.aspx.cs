using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Data;
using CoreLibrary.Validation.Exceptions;

public partial class AreaAdm_Sistemas_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string message = __SessionWEB.PostMessages;
            if (!String.IsNullOrEmpty(message))
                _lblMessage.Text = message;
            _uppCadastroSistema.Update(); 

            try
            {
                UCComboEntidade1.Inicialize("Entidade *");
                UCComboEntidade1._EnableValidator = true;
                UCComboEntidade1._Load(Guid.Empty, 0);
                UCComboEntidade1._ValidationGroup = "_vgCadastroSistemaEntidades";
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
                _uppCadastroSistema.Update();
            }

            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                //_txtsisnome.Enabled = false;
                _VS_sis_id = PreviousPage.EditItem_sis_id;
                _Carregar_dados();
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("É necessário selecionar um sistema.", UtilBO.TipoMensagem.Alerta);
                _txtsisnome.Enabled = false;
                _txtsiscaminho.Enabled = false;
                _txturlintegracao.Enabled = false;

                _btnSalvar.Visible = false;
                _btnAddEntidade.Enabled = false;

                _dgvSistemaEntidade.DataSource = new DataTable();
                _dgvSistemaEntidade.DataBind();
                _uppCadastroSistema.Update();

                this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsEntidadesLigadas.js"));
        }
    }

    #region PROPRIEDADES

    private int _VS_sis_id
    {
        /// <summary>
        /// Armazena o id do sistema
        /// </summary>
        get
        {
            if (ViewState["_VS_sis_id"] != null)
                return Convert.ToInt32(ViewState["_VS_sis_id"]);
            return -1;
        }
        set
        {
            ViewState["_VS_sis_id"] = value;
        }
    }

    /// <summary>
    /// ViewState com datatable de SistemaEntidade
    /// Retorno e atribui valores para o DataTable de SistemaEntidade
    /// Propiedade usada como datasource da grid de SistemaEntidade.
    /// </summary>
    private DataTable _VS_SistemaEntidade
    {
        get
        {
            if (ViewState["_VS_SistemaEntidade"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("sis_id");
                dt.Columns.Add("ent_id");
                dt.Columns.Add("sen_chaveK1");
                dt.Columns.Add("sen_urlAcesso");
                dt.Columns.Add("sen_logoCliente");
                dt.Columns.Add("sen_urlCliente");
                dt.Columns.Add("sen_situacao");
                dt.Columns.Add("ent_razaoSocial");
                dt.Columns.Add("IsNew");
                ViewState["_VS_SistemaEntidade"] = dt;
            }
            return (DataTable)ViewState["_VS_SistemaEntidade"];
        }
        set
        {
            ViewState["_VS_SistemaEntidade"] = value;
        }
    }

    private int _VS_linhaAlteracao_VS_SistemaEntidade
    {
        /// <summary>
        /// Armazena o id do sistema
        /// </summary>
        get
        {
            if (ViewState["_VS_linhaAlteracao_VS_SistemaEntidade"] != null)
                return Convert.ToInt32(ViewState["_VS_linhaAlteracao_VS_SistemaEntidade"]);
            return -1;
        }
        set
        {
            ViewState["_VS_linhaAlteracao_VS_SistemaEntidade"] = value;
        }
    }

    /// <summary>
    /// Tamanho máximo da imagem em Bytes
    /// </summary>
    private const int tamanhoMaxBytes = 1024;

    /// <summary>
    /// Altura da imagem
    /// </summary>
    private const int thumbnailHeight = 75;

    private string NomeLogoCabecalho
    {
        get
        {
            return _VS_sis_id + "_logoCabecalho.png";
        }
    }

    private string NomeLogoImagemMenu
    {
        get
        {
            return _VS_sis_id + "_logoImagemMenu.png";
        }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Retorna o nome padrão do logo do cliente
    /// </summary>
    /// <param name="ent_id">ID da entidade</param>
    /// <returns>Nome do logo do cliente</returns>
    private string NomeLogoCliente(Guid ent_id)
    {
        return _VS_sis_id + "_" + ent_id + "_logoEntidade.png";
    }

    private void _LimparSistemaEntidadeDiv()
    {
        UCComboEntidade1._Combo.Enabled = true;
        UCComboEntidade1._Combo.SelectedValue = "00000000-0000-0000-0000-000000000000";
        _txtChaveK1.Text = string.Empty;
        _txtUrlAcesso.Text = string.Empty;
        txtUrlCliente.Text = "";
    }

    private int _LocalizaLinha_VS_SistemaEntidade(int sis_id, string ent_id)
    {
        try
        {
            for (int i = 0; i < _VS_SistemaEntidade.Rows.Count; i++)
            {
                if (_VS_SistemaEntidade.Rows[i].RowState != DataRowState.Deleted)
                {
                    if (_VS_SistemaEntidade.Rows[i]["sis_id"].ToString().Equals(sis_id.ToString()) && _VS_SistemaEntidade.Rows[i]["ent_id"].ToString().Equals(ent_id))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void _CarregarSistemaEntidade(int linha)
    {
        try
        {
            _VS_linhaAlteracao_VS_SistemaEntidade = linha;
            UCComboEntidade1._Combo.SelectedValue = _VS_SistemaEntidade.Rows[linha]["ent_id"].ToString();
            UCComboEntidade1._Combo.Enabled = false;
            _txtChaveK1.Text = _VS_SistemaEntidade.Rows[linha]["sen_chaveK1"].ToString();
            _txtUrlAcesso.Text = _VS_SistemaEntidade.Rows[linha]["sen_urlAcesso"].ToString();
            txtUrlCliente.Text = _VS_SistemaEntidade.Rows[linha]["sen_urlCliente"].ToString();

            if (!string.IsNullOrEmpty(Convert.ToString(_VS_SistemaEntidade.Rows[linha]["sen_logoCliente"])))
            {
                imgLogoCliente.Visible = true;
                imgLogoCliente.ImageUrl = Convert.ToString(_VS_SistemaEntidade.Rows[linha]["sen_logoCliente"]);
            }
            else
            {
                imgLogoCliente.Visible = false;
            }

            _uppCadastroSistemaEntidade.Update();
            ScriptManager.RegisterStartupScript(this, GetType(), "CadastroEntidade", "$(document).ready(function(){$('#divEntidades').dialog('open');});", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void _ExcluirSistemaEntidade(int linha)
    {
        try
        {
            _VS_SistemaEntidade.Rows[linha].Delete();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void _SalvarSistemaEntidade()
    {
        try
        {
            DataRow dr = _VS_SistemaEntidade.NewRow();
            dr["sis_id"] = _VS_sis_id;
            dr["ent_id"] = UCComboEntidade1._Combo.SelectedValue;
            dr["sen_chaveK1"] = _txtChaveK1.Text;
            dr["sen_urlAcesso"] = _txtUrlAcesso.Text;
            dr["sen_urlCliente"] = txtUrlCliente.Text;
            if (!string.IsNullOrEmpty(fupLogoCliente.FileName))
            {
                SalvaImagem(fupLogoCliente, NomeLogoCliente(new Guid(dr["ent_id"].ToString())), tamanhoMaxBytes);
                dr["sen_logoCliente"] = NomeLogoCliente(new Guid(dr["ent_id"].ToString()));
            }
            dr["ent_razaoSocial"] = UCComboEntidade1._Combo.SelectedItem;
            dr["sen_situacao"] = 1;
            //Realiza inserção do novo registro
            _VS_SistemaEntidade.Rows.Add(dr);

            _dgvSistemaEntidade.DataSource = _VS_SistemaEntidade;
            _dgvSistemaEntidade.DataBind();
            _uppGridSistemaEntidade.Update();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void _AtualizaSistemaEntidade()
    {
        try
        {
            _VS_SistemaEntidade.Rows[_VS_linhaAlteracao_VS_SistemaEntidade]["sen_chaveK1"] = _txtChaveK1.Text;
            _VS_SistemaEntidade.Rows[_VS_linhaAlteracao_VS_SistemaEntidade]["sen_urlAcesso"] = _txtUrlAcesso.Text;

            _VS_SistemaEntidade.Rows[_VS_linhaAlteracao_VS_SistemaEntidade]["sen_urlCliente"] = txtUrlCliente.Text;

            if (!string.IsNullOrEmpty(fupLogoCliente.FileName))
            {
                SalvaImagem(fupLogoCliente, NomeLogoCliente(new Guid(UCComboEntidade1._Combo.SelectedValue)), tamanhoMaxBytes);
                _VS_SistemaEntidade.Rows[_VS_linhaAlteracao_VS_SistemaEntidade]["sen_logoCliente"] = NomeLogoCliente(new Guid(UCComboEntidade1._Combo.SelectedValue));
            }

            _dgvSistemaEntidade.DataSource = _VS_SistemaEntidade;
            _dgvSistemaEntidade.DataBind();
            _uppGridSistemaEntidade.Update();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void _Carregar_dados()
    {
        try
        {
            SYS_Sistema sistemas = new SYS_Sistema { sis_id = _VS_sis_id };
            SYS_SistemaBO.GetEntity(sistemas);

            _VS_SistemaEntidade = SYS_SistemaEntidadeBO.SelectEntidade(_VS_sis_id, false, 1, 1);

            _VS_sis_id = sistemas.sis_id;
            _txtsisnome.Text = sistemas.sis_nome;
            _txtsiscaminho.Text = sistemas.sis_caminho;
            _txtsiscaminhoLogout.Text = sistemas.sis_caminhoLogout;
            _txtsisDescricao.Text = sistemas.sis_descricao;
            _txturlintegracao.Text = sistemas.sis_urlIntegracao;
            _ckbOcultaLogo.Checked = sistemas.sis_ocultarLogo;

            if (_VS_SistemaEntidade.Rows.Count <= 0)
                _VS_SistemaEntidade = null;

            string caminhoBDImagem = Server.MapPath(caminhoLogos + sistemas.sis_urlImagem);
            if (File.Exists(caminhoBDImagem))
            {
                imgImagemSistema.Visible = true;
                imgImagemSistema.ImageUrl = caminhoLogos + sistemas.sis_urlImagem;
            }
            else
            {
                imgImagemSistema.Visible = false;
            }

            caminhoBDImagem = Server.MapPath(caminhoLogos + sistemas.sis_urlLogoCabecalho);
            if (File.Exists(caminhoBDImagem))
            {
                imgLogoCabecalho.Visible = true;
                imgLogoCabecalho.ImageUrl = caminhoLogos + sistemas.sis_urlLogoCabecalho;
            }
            else
            {
                imgLogoCabecalho.Visible = false;
            }

            _dgvSistemaEntidade.DataSource = _VS_SistemaEntidade;
            _dgvSistemaEntidade.DataBind();
            _uppGridSistemaEntidade.Update();
            _uppCadastroSistema.Update();

        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            _uppCadastroSistema.Update();
        }

    }

    private void _Salvar()
    {
        try
        {
            SYS_Sistema entsis = new SYS_Sistema { sis_id = _VS_sis_id };
            SYS_SistemaBO.GetEntity(entsis);

            entsis.sis_nome = _txtsisnome.Text;
            entsis.sis_caminho = _txtsiscaminho.Text;
            entsis.sis_caminhoLogout = _txtsiscaminhoLogout.Text;
            entsis.sis_descricao = _txtsisDescricao.Text;
            entsis.sis_urlIntegracao = _txturlintegracao.Text;
            entsis.sis_ocultarLogo = _ckbOcultaLogo.Checked;
            entsis.IsNew = false;

            if (!string.IsNullOrEmpty(fupImagemSistema.FileName))
            {
                entsis.sis_urlImagem = NomeLogoImagemMenu;
                SalvaImagem(fupImagemSistema, NomeLogoImagemMenu, tamanhoMaxBytes);
            }

            List<SYS_SistemaEntidade> _List_Entities_SistemEntidade = SYS_SistemaEntidadeBO.CriaList_Entities_SistemaEntidade(_VS_SistemaEntidade);

            if (!string.IsNullOrEmpty(fupLogoCabecalho.FileName))
            {
                entsis.sis_urlLogoCabecalho = NomeLogoCabecalho;
                SalvaImagem(fupLogoCabecalho, NomeLogoCabecalho, tamanhoMaxBytes);
            }

            if (SYS_SistemaBO.SalvarUrlInte(entsis, _List_Entities_SistemEntidade))
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "sis_id:" + entsis.sis_id);
                __SessionWEB.PostMessages = UtilBO.GetErroMessage("Sistema salvo com sucesso.", UtilBO.TipoMensagem.Sucesso);
                
                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Sistemas/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o sistema.", UtilBO.TipoMensagem.Erro);                
            }

        }
        catch (ValidationException e)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);   
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o sistema.", UtilBO.TipoMensagem.Erro);            
        }
        finally
        {
            _uppCadastroSistema.Update();
        }
    }

    /// <summary>
    /// Salva a imagem no servidor
    /// </summary>
    /// <param name="fupImagem">FileUpload da imagem</param>
    /// <param name="nome">Nome do arquivo</param>
    /// <param name="maxSize">Tamanho máximo do arquivo</param>
    /// <param name="ImagemQuadrada">Ture caso a altura seja igual a largura</param>
    private void SalvaImagem(FileUpload fupImagem, string nome, int maxSize)
    {
        if (!string.IsNullOrEmpty(fupImagem.PostedFile.FileName))
        {
            if (string.IsNullOrEmpty(nome))
            {
                nome = _VS_sis_id + fupImagem.FileName;
            }
            UtilBO.SaveImage(maxSize, CaminhoFisicoLogos
                , nome, fupImagem.PostedFile);       
        }
    }

    #endregion

    #region EVENTOS

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }
    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Sistemas/Busca.aspx", false);
    }
    protected void _btnAddEntidade_Click(object sender, EventArgs e)
    {
        _LimparSistemaEntidadeDiv();
        _VS_linhaAlteracao_VS_SistemaEntidade = -1;
        imgLogoCliente.Visible = false;
        _uppCadastroSistemaEntidade.Update();
        ScriptManager.RegisterStartupScript(this, GetType(), "CadastroEntidade", "$(document).ready(function(){$('#divEntidades').dialog('open');});", true);
    }

    protected void _btnSalvarE_Click(object sender, EventArgs e)
    {
        try
        {
            if (_VS_linhaAlteracao_VS_SistemaEntidade >= 0)
            {
                _AtualizaSistemaEntidade();
                _LimparSistemaEntidadeDiv();
                _uppCadastroSistemaEntidade.Update();                
                ScriptManager.RegisterStartupScript(this, GetType(), "CadastroEntidade", "$(document).ready(function(){$('#divEntidades').dialog('close');});", true);
            }
            else
            {
                if (SYS_SistemaEntidadeBO.VerificaEntidadeExistente(new Guid(UCComboEntidade1._Combo.SelectedValue), _VS_SistemaEntidade))
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Já existe uma entidade cadastrada para este sistema.", UtilBO.TipoMensagem.Alerta);
                    _uppCadastroSistemaEntidade.Update();
                }
                else
                {
                    _SalvarSistemaEntidade();
                    _LimparSistemaEntidadeDiv();
                    _uppCadastroSistemaEntidade.Update();                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "CadastroEntidade", "$(document).ready(function(){$('#divEntidades').dialog('close');});", true);
                }
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar entidade.", UtilBO.TipoMensagem.Erro);
            _uppCadastroSistemaEntidade.Update();
        }
    }
    protected void _btnCancelarE_Click(object sender, EventArgs e)
    {
        _LimparSistemaEntidadeDiv();
        _uppCadastroSistemaEntidade.Update();        
        ScriptManager.RegisterStartupScript(this, GetType(), "CadastroEntidade", "$(document).ready(function(){$('#divEntidades').dialog('close');});", true);
    }

    protected void _dgvSistemaEntidade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        int sis_id = Convert.ToInt32(_dgvSistemaEntidade.DataKeys[index].Values[0]);
        string ent_id = _dgvSistemaEntidade.DataKeys[index].Values[1].ToString();

        if (e.CommandName == "Editar")
        {
            try
            {
                int linha = _LocalizaLinha_VS_SistemaEntidade(sis_id, ent_id);
                if (linha >= 0)
                {
                    _CarregarSistemaEntidade(linha);
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar entidade.", UtilBO.TipoMensagem.Erro);
                _uppCadastroSistema.Update();
            }
        }
        else if (e.CommandName == "Deletar")
        {
            try
            {
                int linha = _LocalizaLinha_VS_SistemaEntidade(sis_id, ent_id);
                if (linha >= 0)
                {
                    _ExcluirSistemaEntidade(linha);
                    _dgvSistemaEntidade.DataSource = _VS_SistemaEntidade;
                    _dgvSistemaEntidade.DataBind();
                    _uppGridSistemaEntidade.Update();
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar excluir entidade.", UtilBO.TipoMensagem.Erro);
                _uppCadastroSistema.Update();
            }
        }
    }
    protected void _dgvSistemaEntidade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (btnDelete != null)
            {
                btnDelete.CommandArgument = e.Row.RowIndex.ToString();
            }
            LinkButton lkbAlterar = (LinkButton)e.Row.FindControl("_lkbAlterar");
            if (lkbAlterar != null)
            {
                lkbAlterar.CommandArgument = e.Row.RowIndex.ToString();
            }
            Image imgLogo = (Image)e.Row.FindControl("_imgLogo");
            if (lkbAlterar != null)
            {
                string sen_logoCliente = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sen_logoCliente"));
                string imagePath = VirtualPathUtility.ToAbsolute("~/App_Themes/" + NomeTemaAtual + "/images/logos/");

                if (!string.IsNullOrEmpty(sen_logoCliente))
                {
                    imgLogo.ImageUrl = imagePath + sen_logoCliente;
                    imgLogo.Visible = true;
                }
                else
                {
                    imgLogo.Visible = false;
                }
            }
        }
    }

    #endregion
}
