using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using CoreLibrary.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AreaAdm_Parametros_Cadastro : MotherPageLogado
{
    #region Eventos Life Cycle

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                UCComboSistemaGrupo._ShowSelectMessage = true;
                UCComboSistemaGrupo.Inicialize("Sistema - grupo");
                UCComboSistemaGrupo._Load(0);
                UCComboSistemaGrupo._ValidationGroup = "Padrao";
                UCComboSistemaGrupo._EnableValidator = true;
                UCComboSistemaGrupo._Label.Text += " * ";

                _Carga_VS_Parametros();

                Page.Form.DefaultButton = _btnNovoGrupoPadrao.UniqueID;
                _btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                _btnNovoGrupoPadrao.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }
        }

        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.CamposData));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Tabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MsgConfirmExclusao));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsModuloSeguranca.js"));
        }
    }

    #endregion Eventos Life Cycle

    #region Propriedades

    /// <summary>
    /// Armazena valor do par_id em ViewState
    /// </summary>
    private Guid _VS_par_id
    {
        get
        {
            if (ViewState["_VS_par_id"] != null)
                return new Guid(ViewState["_VS_par_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_par_id"] = value;
        }
    }

    /// <summary>
    /// Armazena valor do pgs_id em ViewState
    /// </summary>
    private Guid _VS_pgs_id
    {
        get
        {
            if (ViewState["_VS_pgs_id"] != null)
                return new Guid(ViewState["_VS_pgs_id"].ToString());
            return Guid.Empty;
        }
    }

    /// <summary>
    /// Armazena valor do par_chave em ViewState
    /// </summary>
    private string _VS_par_chave
    {
        get
        {
            if (ViewState["_VS_par_chave"] != null)
                return Convert.ToString(ViewState["_VS_par_chave"]);
            return null;
        }
        set
        {
            ViewState["_VS_par_chave"] = value;
        }
    }

    /// <summary>
    /// Armazena valor do par_obrigatorio em ViewState
    /// </summary>
    private bool _VS_par_obrigatorio
    {
        get
        {
            if (ViewState["_VS_par_obrigatorio"] != null)
                return Convert.ToBoolean(ViewState["_VS_par_obrigatorio"]);
            return false;
        }
        set
        {
            ViewState["_VS_par_obrigatorio"] = value;
        }
    }

    /// <summary>
    /// Armazena valor do par_descricao em ViewState
    /// </summary>
    private string _VS_par_descricao
    {
        get
        {
            if (ViewState["_VS_par_descricao"] != null)
                return Convert.ToString(ViewState["_VS_par_descricao"]);
            return null;
        }
        set
        {
            ViewState["_VS_par_descricao"] = value;
        }
    }

    /// <summary>
    /// Armazena valor do par_obrigatorio em ViewState
    /// </summary>
    private bool _VS_alteracao
    {
        get
        {
            if (ViewState["_VS_alteracao"] != null)
                return Convert.ToBoolean(ViewState["_VS_alteracao"]);
            return false;
        }
        set
        {
            ViewState["_VS_alteracao"] = value;
        }
    }

    /// <summary>
    /// Habilita as validações e requisições de campo da tela.
    /// </summary>
    private bool _HabilitaCamposObrigatorios
    {
        set
        {
            _rfvVigenciaIni.Visible = value;
            cvDataVigIni.Visible = value;
            cvDataVigFim.Visible = !_VS_par_obrigatorio;
        }
    }

    /// <summary>
    /// Monta o DropDownList usada na DIV conforme o valor da chave.
    /// Retornando a lista com valores não excluidos logicamente.
    /// (ativo e bloqueado)
    /// </summary>
    /// <param name="par_chave"></param>
    private void _MontarDDLParametroValor(string par_chave)
    {
        _ddlParametroValor.DataSourceID = _odsComboParametro.ID;

        if (par_chave == "PERMITIR_LOGIN_COM_PROVIDER_EXTERNO")
        {
            _cvParametroValor.ErrorMessage = "Permitir o login com provider externo.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }

        if (par_chave == "PERMITIR_MULTIPLOS_ENDERECOS_UA")
        {
            _cvParametroValor.ErrorMessage = "Permitir multiplos endereços na Unidade Administrativa é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }

        if (par_chave == "ENDERECO_OBRIGATORIO_CADASTRO_UA")
        {
            _cvParametroValor.ErrorMessage = "Obrigatório o cadastro de endereço na unidades administrativas ?";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }

        if (par_chave == "PERMITIR_MULTIPLOS_ENDERECOS_ENTIDADE")
        {
            _cvParametroValor.ErrorMessage = "Permitir multiplos endereços na Entidade é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }

        if (par_chave == "PERMITIR_MULTIPLOS_ENDERECOS_PESSOA")
        {
            _cvParametroValor.ErrorMessage = "Permitir multiplos endereços em Pessoas é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }

        if (par_chave == "PERMITIR_TIPO_CONTATOS_DUPLICADOS")
        {
            _cvParametroValor.ErrorMessage = "Permitir tipo de contato duplicado é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }

        if (par_chave == "HABILITAR_VALIDACAO_DUPLICIDADE_TIPO_DOCUMENTO_POR_CLASSIFICACAO")
        {
            _cvParametroValor.ErrorMessage = "Habilitar a validação de duplicidades no tipo de documentação para a mesma classificação.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }

        if (par_chave == "PERMITIR_CADASTRAR_DOCUMENTACAO_POR_CLASSIFICACAO")
        {
            _cvParametroValor.ErrorMessage = "Permitir a manutenção da documentação por classificação do tipo de documento.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }

        if (par_chave == "PAIS_PADRAO_BRASIL")
        {
            _ddlParametroValor.Visible = true;
            _cvParametroValor.Visible = true;

            _cvParametroValor.ErrorMessage = "País é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "pai_nome";
            _ddlParametroValor.DataValueField = "pai_id";
            _odsComboParametro.SelectMethod = "GetSelect";
            _odsComboParametro.DataObjectTypeName = "Autenticador.Entities.END_Pais";
            _odsComboParametro.TypeName = "Autenticador.BLL.END_PaisBO";
            _odsComboParametro.SelectParameters.Clear();
            _odsComboParametro.SelectParameters.Add("pai_id", Guid.Empty.ToString());
            _odsComboParametro.SelectParameters.Add("pai_nome", string.Empty);
            _odsComboParametro.SelectParameters.Add("pai_sigla", string.Empty);
            _odsComboParametro.SelectParameters.Add("pai_situacao", "0");
            _odsComboParametro.SelectParameters.Add("paginado", "false");
            _odsComboParametro.DataBind();

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione um país --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "ESTADO_PADRAO_SP")
        {
            _ddlParametroValor.Visible = true;
            _cvParametroValor.Visible = true;

            _cvParametroValor.ErrorMessage = "Estado é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "unf_nome";
            _ddlParametroValor.DataValueField = "unf_id";
            _odsComboParametro.SelectMethod = "GetSelect";
            _odsComboParametro.DataObjectTypeName = "Autenticador.Entities.END_UnidadeFederativa";
            _odsComboParametro.TypeName = "Autenticador.BLL.END_UnidadeFederativaBO";
            _odsComboParametro.SelectParameters.Clear();
            _odsComboParametro.SelectParameters.Add("unf_id", Guid.Empty.ToString());
            _odsComboParametro.SelectParameters.Add("pai_id", Guid.Empty.ToString());
            _odsComboParametro.SelectParameters.Add("unf_nome", string.Empty);
            _odsComboParametro.SelectParameters.Add("unf_sigla", string.Empty);
            _odsComboParametro.SelectParameters.Add("unf_situacao", "0");
            _odsComboParametro.SelectParameters.Add("paginado", "false");
            _odsComboParametro.DataBind();

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione um estado --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "TIPO_DOCUMENTACAO_RG" || par_chave == "TIPO_DOCUMENTACAO_CPF" || par_chave == "TIPO_DOCUMENTACAO_IDENTIFICACAO_FUNCIONAL")
        {
            _ddlParametroValor.Visible = true;
            _cvParametroValor.Visible = true;

            _cvParametroValor.ErrorMessage = "Tipo documentação é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "tdo_nome";
            _ddlParametroValor.DataValueField = "tdo_id";
            _odsComboParametro.SelectMethod = "GetSelect";
            _odsComboParametro.DataObjectTypeName = "Autenticador.Entities.SYS_TipoDocumentacao";
            _odsComboParametro.TypeName = "Autenticador.BLL.SYS_TipoDocumentacaoBO";
            _odsComboParametro.SelectParameters.Clear();
            _odsComboParametro.SelectParameters.Add("tdo_id", Guid.Empty.ToString());
            _odsComboParametro.SelectParameters.Add("tdo_nome", string.Empty);
            _odsComboParametro.SelectParameters.Add("tdo_sigla", string.Empty);
            _odsComboParametro.SelectParameters.Add("tdo_situacao", "0");
            _odsComboParametro.SelectParameters.Add("paginado", "false");
            _odsComboParametro.DataBind();

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione um documento --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "TIPO_MEIOCONTATO_EMAIL")
        {
            _ddlParametroValor.Visible = true;
            _cvParametroValor.Visible = true;

            _cvParametroValor.ErrorMessage = "Tipo de contato é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "tmc_nome";
            _ddlParametroValor.DataValueField = "tmc_id";
            _odsComboParametro.SelectMethod = "GetSelect";
            _odsComboParametro.DataObjectTypeName = "Autenticador.Entities.SYS_TipoMeioContato";
            _odsComboParametro.TypeName = "Autenticador.BLL.SYS_TipoMeioContatoBO";
            _odsComboParametro.SelectParameters.Clear();
            _odsComboParametro.SelectParameters.Add("tmc_id", Guid.Empty.ToString());
            _odsComboParametro.SelectParameters.Add("tmc_nome", string.Empty);
            _odsComboParametro.SelectParameters.Add("tmc_situacao", "0");
            _odsComboParametro.SelectParameters.Add("paginado", "false");
            _odsComboParametro.DataBind();

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione um tipo de contato--", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "TIPO_MEIOCONTATO_TELEFONE")
        {
            _ddlParametroValor.Visible = true;
            _cvParametroValor.Visible = true;

            _cvParametroValor.ErrorMessage = "Tipo de contato é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "tmc_nome";
            _ddlParametroValor.DataValueField = "tmc_id";
            _odsComboParametro.SelectMethod = "GetSelect";
            _odsComboParametro.DataObjectTypeName = "Autenticador.Entities.SYS_TipoMeioContato";
            _odsComboParametro.TypeName = "Autenticador.BLL.SYS_TipoMeioContatoBO";
            _odsComboParametro.SelectParameters.Clear();
            _odsComboParametro.SelectParameters.Add("tmc_id", Guid.Empty.ToString());
            _odsComboParametro.SelectParameters.Add("tmc_nome", string.Empty);
            _odsComboParametro.SelectParameters.Add("tmc_situacao", "0");
            _odsComboParametro.SelectParameters.Add("paginado", "false");
            _odsComboParametro.DataBind();

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione um tipo de contato--", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "TIPO_MEIOCONTATO_TELEFONE_CELULAR")
        {
            _ddlParametroValor.Visible = true;
            _cvParametroValor.Visible = true;

            _cvParametroValor.ErrorMessage = "Tipo de contato é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "tmc_nome";
            _ddlParametroValor.DataValueField = "tmc_id";
            _odsComboParametro.SelectMethod = "GetSelect";
            _odsComboParametro.DataObjectTypeName = "Autenticador.Entities.SYS_TipoMeioContato";
            _odsComboParametro.TypeName = "Autenticador.BLL.SYS_TipoMeioContatoBO";
            _odsComboParametro.SelectParameters.Clear();
            _odsComboParametro.SelectParameters.Add("tmc_id", Guid.Empty.ToString());
            _odsComboParametro.SelectParameters.Add("tmc_nome", string.Empty);
            _odsComboParametro.SelectParameters.Add("tmc_situacao", "0");
            _odsComboParametro.SelectParameters.Add("paginado", "false");
            _odsComboParametro.DataBind();

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione um tipo de contato--", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "TIPO_MEIOCONTATO_SITE")
        {
            _ddlParametroValor.Visible = true;
            _cvParametroValor.Visible = true;

            _cvParametroValor.ErrorMessage = "Tipo de contato é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "tmc_nome";
            _ddlParametroValor.DataValueField = "tmc_id";
            _odsComboParametro.SelectMethod = "GetSelect";
            _odsComboParametro.DataObjectTypeName = "Autenticador.Entities.SYS_TipoMeioContato";
            _odsComboParametro.TypeName = "Autenticador.BLL.SYS_TipoMeioContatoBO";
            _odsComboParametro.SelectParameters.Clear();
            _odsComboParametro.SelectParameters.Add("tmc_id", Guid.Empty.ToString());
            _odsComboParametro.SelectParameters.Add("tmc_nome", string.Empty);
            _odsComboParametro.SelectParameters.Add("tmc_situacao", "0");
            _odsComboParametro.SelectParameters.Add("paginado", "false");
            _odsComboParametro.DataBind();

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione um tipo de contato--", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "EXIBIR_LOGO_CLIENTE")
        {
            _cvParametroValor.ErrorMessage = "Opção por exibição do logo de cliente é obrigatória.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "SALVAR_SEMPRE_MAIUSCULO")
        {
            _cvParametroValor.ErrorMessage = "Opção por salvar dados das pessoas em maiúsculo é obrigatória.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "QT_ITENS_PAGINACAO")
        {
            _ddlParametroValor.Visible = true;
            _cvParametroValor.Visible = true;

            _cvParametroValor.ErrorMessage = "Quantidade de itens na paginação é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Add(new ListItem("10", "10"));
            _ddlParametroValor.Items.Add(new ListItem("20", "20"));
            _ddlParametroValor.Items.Add(new ListItem("50", "50"));
            _ddlParametroValor.Items.Add(new ListItem("100", "100"));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione a quantidade de itens --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "LOG_ERROS_GRAVAR_QUERYSTRING")
        {
            _cvParametroValor.ErrorMessage = "Opção por gravar informações relativas a QueryString no log de erros é obrigatória.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "LOG_ERROS_GRAVAR_SERVERVARIABLES")
        {
            _cvParametroValor.ErrorMessage = "Opção por gravar informações relativas a ServerVariables no log de erros é obrigatória.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "LOG_ERROS_GRAVAR_PARAMS")
        {
            _cvParametroValor.ErrorMessage = "Opção por gravar informações relativas a Params no log de erros é obrigatória.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "REMOVER_OPCAO_ESQUECISENHA")
        {
            _cvParametroValor.ErrorMessage = "Opção para remover esqueci minha senha é obrigatória.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "UTILIZAR_CAPTCHA_FALHA_AUTENTICACAO")
        {
            _cvParametroValor.ErrorMessage = "Utilizar captcha é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "PERMITIR_DTNASCIMENTO_CPF_ESQUECISENHA")
        {
            _cvParametroValor.ErrorMessage = "Permite utilizar data de nascimento e CPF no Esqueci minha senha é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "VALIDAR_UNICIDADE_EMAIL_USUARIO")
        {
            _cvParametroValor.ErrorMessage = "Validar unicidade de email do usuário na entidade é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO")
        {
            _cvParametroValor.ErrorMessage = "Validar obrigatoriedade de email do usuário é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "PERMITIR_ALTERAR_EMAIL_MEUSDADOS")
        {
            _cvParametroValor.ErrorMessage = "Permitir incluir/alterar e-mail na tela de Meus Dados é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "SALVAR_HISTORICO_SENHA_USUARIO")
        {
            _cvParametroValor.ErrorMessage = "Salvar histórico de senhas do usuário é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "PERMITIR_INTEGRACAO_SENHA_EXPIRADA_AD")
        {
            _cvParametroValor.ErrorMessage = "Permitir a integração de senhas expiradas com o Active Directory é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
        else if (par_chave == "GERAR_SENHA_FORMATO_PARAMETRIZADO")
        {
            _cvParametroValor.ErrorMessage = "Gerar senha utilizando o formato definido por parâmetro é obrigatório.";
            _ddlParametroValor.Items.Clear();
            _ddlParametroValor.DataTextField = "";
            _ddlParametroValor.DataValueField = "";

            _ddlParametroValor.DataSourceID = "";

            _ddlParametroValor.Items.Insert(0, new ListItem("Sim", "True", true));
            _ddlParametroValor.Items.Insert(0, new ListItem("Não", "False", true));

            _ddlParametroValor.Items.Insert(0, new ListItem("-- Selecione uma opção --", Guid.Empty.ToString(), true));
            _ddlParametroValor.AppendDataBoundItems = true;
        }
    }

    /// <summary>
    /// Monta o GridView usada na DIV setando propriedades do seu ObjectDataSource.
    /// Apresenta apenas os valores referentes a uma chave.
    /// </summary>
    private bool _MontaGridParametro_Div
    {
        set
        {
            if (value)
            {
                switch (_VS_par_chave)
                {
                    // Parâmetros texto
                    case "URL_ADMINISTRATIVO":
                    case "URL_CLIENTE":
                    case "TITULO_GERAL":
                    case "MENSAGEM_COPYRIGHT":
                    case "TAMANHO_MAX_FOTO_PESSOA":
                    case "FORMATO_SENHA_USUARIO":
                    case "TAMANHO_SENHA_USUARIO":
                    case "LOG_ERROS_CHAVES_NAO_GRAVAR":
                    case "HELP_DESK_CONTATO":
                    case "MENSAGEM_ICONE_HELP":
                    case "SUPORTE_TECNICO_EMAILS":
                    case "ID_GOOGLE_ANALYTICS":
                    case "MENSAGEM_ALERTA_PRELOGIN":
                    case "INTERVALO_MINUTOS_VERIFICAR_FALHA_AUTENTICACAO":
                    case "QUANTIDADE_FALHAS_AUTENTICACAO_EXIBIR_CAPTCHA":
                    case "VERSAO_WEBAPI_CORESSO":
                    case "URL_WEBAPI_CORESSO":
                    case "QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO":
                    case "PRAZO_DIAS_EXPIRA_SENHA":
                        {
                            divFileUpload.Visible = false;
                            divUpn.Visible = true;
                            divParametroCombo.Visible = false;
                            divParametroTextBox.Visible = true;
                            fsParametrosValores.Visible = false;

                            // Carregar registro único com o valor.
                            DataTable dt = SYS_ParametroBO.SelecionaParametroValores(_VS_par_chave, false, 1, 1);

                            Guid id = Guid.Empty;

                            if (dt.Rows.Count > 0)
                            {
                                id = new Guid(dt.Rows[0]["par_id"].ToString());
                            }

                            _CarregarParametro(id);

                            break;
                        }
                    // Parâmetros imagem
                    case "LOGO_CLIENTE":
                    case "LOGO_GERAL_SISTEMA":
                        {
                            divFileUpload.Visible = true;
                            divUpn.Visible = false;
                            divParametroCombo.Visible = false;
                            divParametroTextBox.Visible = false;
                            fsParametrosValores.Visible = false;

                            _lblNome_Par.Text = _VS_par_descricao;

                            break;
                        }
                    // Parâmetros sim ou não (true/false) e a quantidade de itens
                    case "PERMITIR_LOGIN_COM_PROVIDER_EXTERNO":
                    case "PERMITIR_MULTIPLOS_ENDERECOS_UA":
                    case "ENDERECO_OBRIGATORIO_CADASTRO_UA":
                    case "PERMITIR_MULTIPLOS_ENDERECOS_ENTIDADE":
                    case "PERMITIR_MULTIPLOS_ENDERECOS_PESSOA":
                    case "PERMITIR_TIPO_CONTATOS_DUPLICADOS":
                    case "HABILITAR_VALIDACAO_DUPLICIDADE_TIPO_DOCUMENTO_POR_CLASSIFICACAO":
                    case "PERMITIR_CADASTRAR_DOCUMENTACAO_POR_CLASSIFICACAO":
                    case "SALVAR_SEMPRE_MAIUSCULO":
                    case "EXIBIR_LOGO_CLIENTE":
                    case "LOG_ERROS_GRAVAR_QUERYSTRING":
                    case "LOG_ERROS_GRAVAR_SERVERVARIABLES":
                    case "LOG_ERROS_GRAVAR_PARAMS":
                    case "QT_ITENS_PAGINACAO":
                    case "REMOVER_OPCAO_ESQUECISENHA":
                    case "UTILIZAR_CAPTCHA_FALHA_AUTENTICACAO":
                    case "PERMITIR_DTNASCIMENTO_CPF_ESQUECISENHA":
                    case "VALIDAR_UNICIDADE_EMAIL_USUARIO":
                    case "VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO":
                    case "PERMITIR_ALTERAR_EMAIL_MEUSDADOS":
                    case "SALVAR_HISTORICO_SENHA_USUARIO":
                    case "PERMITIR_INTEGRACAO_SENHA_EXPIRADA_AD":
                    case "GERAR_SENHA_FORMATO_PARAMETRIZADO":
                        {
                            divFileUpload.Visible = false;
                            divUpn.Visible = true;
                            divParametroCombo.Visible = true;
                            divParametroTextBox.Visible = false;
                            fsParametrosValores.Visible = false;

                            divVigencia.Visible = false;

                            _lblNome_Par.Text = _VS_par_descricao;

                            // Carregar registro único com o valor.
                            DataTable dt = SYS_ParametroBO.SelecionaParametroValores(_VS_par_chave, false, 1, 1);

                            Guid id = Guid.Empty;

                            if (dt.Rows.Count > 0)
                            {
                                id = new Guid(dt.Rows[0]["par_id"].ToString());
                            }

                            _CarregarParametro(id);

                            break;
                        }
                    // Parâmetros lista
                    default:
                        {
                            divFileUpload.Visible = false;
                            divUpn.Visible = true;
                            divParametroCombo.Visible = true;
                            divParametroTextBox.Visible = false;
                            fsParametrosValores.Visible = true;
                            divVigencia.Visible = true;

                            _lblNome_Par.Text = _VS_par_descricao;
                            _odsParametro2.SelectParameters.Clear();
                            _odsParametro2.SelectMethod = "SelecionaParametroValores";
                            _odsParametro2.SelectParameters.Add("par_chave", _VS_par_chave);
                            _odsParametro2.SelectParameters.Add("paginado", string.Empty);
                            _odsParametro2.DataBind();
                            _lbla.Visible = !_VS_par_obrigatorio || _VS_alteracao;
                            _txtVigenciaFim.Visible = !_VS_par_obrigatorio || _VS_alteracao;

                            break;
                        }
                }
            }
            else
            {
                _odsParametro2.SelectMethod = "SelectVazio";
                _odsParametro2.SelectParameters.Clear();
            }
        }
    }

    /// <summary>
    /// Limpa a tela usada na DIV e seta valores do ViewState
    /// para valores default.
    /// </summary>
    private bool _LimpaCamposParametro_Div
    {
        set
        {
            if (value)
            {
                _VS_par_id = Guid.Empty;
                _VS_par_obrigatorio = false;
                _VS_alteracao = false;
                _txtVigenciaIni.Enabled = true;
                _txtVigenciaIni.Text = string.Empty;
                _txtVigenciaFim.Text = string.Empty;
                _ddlParametroValor.SelectedValue = Guid.Empty.ToString();
            }
        }
    }

    /// <summary>
    /// ViewState com datatable de Parametros
    /// Retorno e atribui valores para o DataTable de Parametros
    /// </summary>
    public DataTable _VS_Parametros
    {
        get
        {
            if (ViewState["_VS_Parametros"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("par_id");
                dt.Columns.Add("par_chave");
                dt.Columns.Add("par_descricao");
                dt.Columns.Add("par_obrigatorio");
                dt.Columns.Add("par_valor_nome");
                dt.Columns.Add("par_vigenciaFim");
                dt.Columns.Add("par_vigenciaInicio");
                dt.Columns.Add("par_vigencia");
                ViewState["_VS_Parametros"] = dt;
            }
            return (DataTable)ViewState["_VS_Parametros"];
        }
        set
        {
            ViewState["_VS_Parametros"] = value;
        }
    }

    #endregion Propriedades

    #region Métodos

    /// <summary>
    /// Método para carga inicial dos parametros Default do sistema
    /// </summary>
    private void _Carga_VS_Parametros()
    {
        try
        {
            _VS_Parametros = null;

            List<String> _List_Parametros = new List<string>();
            _List_Parametros.Add("PAIS_PADRAO_BRASIL|true|País padrão");
            _List_Parametros.Add("ESTADO_PADRAO_SP|true|Estado padrão");
            _List_Parametros.Add("TIPO_DOCUMENTACAO_CPF|true|Tipo de documentação - CPF");
            _List_Parametros.Add("TIPO_DOCUMENTACAO_RG|true|Tipo de documentação - RG");

            //
            _List_Parametros.Add("TIPO_DOCUMENTACAO_IDENTIFICACAO_FUNCIONAL|true|Tipo de documentação - Identificação Funcional");

            _List_Parametros.Add("TIPO_MEIOCONTATO_EMAIL|true|Tipo de contato - E-mail");
            _List_Parametros.Add("TIPO_MEIOCONTATO_TELEFONE|true|Tipo de contato - Telefone");

            //
            _List_Parametros.Add("TIPO_MEIOCONTATO_TELEFONE_CELULAR|true|Tipo de contato - Telefone Celular");

            _List_Parametros.Add("TIPO_MEIOCONTATO_SITE|true|Tipo de contato - Web site");
            _List_Parametros.Add("TAMANHO_MAX_FOTO_PESSOA|false|Tamanho máximo da foto da pessoa (em KB)");

            _List_Parametros.Add("URL_ADMINISTRATIVO|false|Url do sistema administrativo");
            _List_Parametros.Add("TITULO_GERAL|false|Título geral do sistema");
            _List_Parametros.Add("MENSAGEM_COPYRIGHT|false|Mensagem padrão do rodapé");
            _List_Parametros.Add("LOGO_CLIENTE|false|Logo padrão do cliente");
            _List_Parametros.Add("URL_CLIENTE|false|Url do site do cliente");
            _List_Parametros.Add("EXIBIR_LOGO_CLIENTE|false|Exibir logo do cliente");
            _List_Parametros.Add("LOGO_GERAL_SISTEMA|false|Logo geral do sistema");
            _List_Parametros.Add("SALVAR_SEMPRE_MAIUSCULO|false|Salvar dados da pessoa em maiúsculo");
            _List_Parametros.Add("QT_ITENS_PAGINACAO|false|Quantidade de itens na paginação");
            _List_Parametros.Add("FORMATO_SENHA_USUARIO|true|Expressão regular para validar o formato da senha do usuário");
            _List_Parametros.Add("TAMANHO_SENHA_USUARIO|true|Expressão regular para validar o tamanho da senha do usuário");

            _List_Parametros.Add("LOG_ERROS_GRAVAR_QUERYSTRING|false|Gravar informações relativas a QueryString no log de erros");
            _List_Parametros.Add("LOG_ERROS_GRAVAR_SERVERVARIABLES|false|Gravar informações relativas a ServerVariables no log de erros");
            _List_Parametros.Add("LOG_ERROS_GRAVAR_PARAMS|false|Gravar informações relativas a Params no log de erros");
            _List_Parametros.Add("LOG_ERROS_CHAVES_NAO_GRAVAR|false|Chaves de informações relativas a QueryString, ServerVariables e/ou Params que não devem ser gravadas no log de erros (separadas por ';')");

            _List_Parametros.Add("HELP_DESK_CONTATO|false|Help Desk");
            _List_Parametros.Add("MENSAGEM_ICONE_HELP|false|Mensagem que será exibida ao passar o mouse sobre o ícone do help");

            _List_Parametros.Add("ID_GOOGLE_ANALYTICS|false|ID do Google Analytics");

            _List_Parametros.Add("SUPORTE_TECNICO_EMAILS|false|E-mails do suporte técnico (separados por ',')");

            _List_Parametros.Add("REMOVER_OPCAO_ESQUECISENHA|true|Remover opção Esqueci minha senha");

            _List_Parametros.Add("MENSAGEM_ALERTA_PRELOGIN|false|Mensagem de alerta pré login");

            _List_Parametros.Add("UTILIZAR_CAPTCHA_FALHA_AUTENTICACAO|false|Utilizar captcha para falhas de autenticação");
            _List_Parametros.Add("INTERVALO_MINUTOS_VERIFICAR_FALHA_AUTENTICACAO|false|Intervalo em minutos para a contagem de falhas de login (para exibir o captcha)");
            _List_Parametros.Add("QUANTIDADE_FALHAS_AUTENTICACAO_EXIBIR_CAPTCHA|false|Quantidade de falhas necessárias para exibir o captcha");
            _List_Parametros.Add("PERMITIR_DTNASCIMENTO_CPF_ESQUECISENHA|false|Permite utilizar data de nascimento e CPF no Esqueci minha senha");
            _List_Parametros.Add("VERSAO_WEBAPI_CORESSO|true|Versão da WebApi do CoreSSO");
            _List_Parametros.Add("URL_WEBAPI_CORESSO|true|URL da WebApi do CoreSSO");
            _List_Parametros.Add("VALIDAR_UNICIDADE_EMAIL_USUARIO|true|Validar unicidade de email do usuário na entidade");
            _List_Parametros.Add("VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO|true|Validar obrigatoriedade de email do usuário");
            _List_Parametros.Add("PERMITIR_ALTERAR_EMAIL_MEUSDADOS|true|Permitir incluir/alterar e-mail na tela de Meus Dados");
            _List_Parametros.Add("SALVAR_HISTORICO_SENHA_USUARIO|true|Salvar histórico de senhas do usuário");
            _List_Parametros.Add("QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO|true|Quantidade de últimas senhas diferentes utilizadas para validar nova senha.");
            _List_Parametros.Add("PERMITIR_INTEGRACAO_SENHA_EXPIRADA_AD|true|Permitir a integração de senhas expiradas com o Active Directory.");
            _List_Parametros.Add("GERAR_SENHA_FORMATO_PARAMETRIZADO|true|Gerar senha utilizando o formato definido por parâmetro.");

            _List_Parametros.Add("PERMITIR_MULTIPLOS_ENDERECOS_UA|true|Permitir gravar multiplos endereços para unidade administrativa.");
            _List_Parametros.Add("ENDERECO_OBRIGATORIO_CADASTRO_UA|true|Obrigatório o cadastro de endereço na unidades administrativas ?");

            _List_Parametros.Add("PERMITIR_MULTIPLOS_ENDERECOS_ENTIDADE|true|Permitir gravar multiplos endereços para entidade.");

            _List_Parametros.Add("PERMITIR_MULTIPLOS_ENDERECOS_PESSOA|true|Permitir gravar multiplos endereços para pessoa.");

            _List_Parametros.Add("PERMITIR_TIPO_CONTATOS_DUPLICADOS|true|Permitir gravar mais de uma vez o mesmo tipo de contato.");
            _List_Parametros.Add("HABILITAR_VALIDACAO_DUPLICIDADE_TIPO_DOCUMENTO_POR_CLASSIFICACAO|false|Habilitar a validação de duplicidades no tipo de documentação para a mesma classificação.");
            _List_Parametros.Add("PERMITIR_CADASTRAR_DOCUMENTACAO_POR_CLASSIFICACAO|false|Permitir a manutenção da documentação por classificação do tipo de documento.");
            _List_Parametros.Add("PERMITIR_LOGIN_COM_PROVIDER_EXTERNO|true|Permitir o login com provider externo.");
            _List_Parametros.Add("PRAZO_DIAS_EXPIRA_SENHA|true|Prazo em dias para expirar as senhas.");

            if (_VS_Parametros != null)
            {
                foreach (String parametro in _List_Parametros)
                {
                    DataRow dr = _VS_Parametros.NewRow();
                    dr["par_id"] = string.Empty;
                    dr["par_chave"] = parametro.Split('|')[0];
                    dr["par_obrigatorio"] = parametro.Split('|')[1];
                    dr["par_descricao"] = parametro.Split('|')[2];
                    dr["par_valor_nome"] = string.Empty;
                    dr["par_vigenciaFim"] = string.Empty;
                    dr["par_vigenciaInicio"] = string.Empty;
                    dr["par_vigencia"] = string.Empty;
                    //Realiza inserção do novo registro
                    _VS_Parametros.Rows.Add(dr);
                }

                DataTable dt = SYS_ParametroBO.GetSelect(false, 1, 1);

                for (int i = 0; i < _VS_Parametros.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (Convert.ToString(_VS_Parametros.Rows[i]["par_chave"]) == Convert.ToString(dt.Rows[j]["par_chave"]))
                        {
                            _VS_Parametros.Rows[i]["par_id"] = Convert.ToString(dt.Rows[j]["par_id"]);
                            _VS_Parametros.Rows[i]["par_valor_nome"] = Convert.ToString(dt.Rows[j]["par_valor_nome"]);
                            _VS_Parametros.Rows[i]["par_vigenciaFim"] = Convert.ToString(dt.Rows[j]["par_vigenciaFim"]);
                            _VS_Parametros.Rows[i]["par_vigenciaInicio"] = Convert.ToString(dt.Rows[j]["par_vigenciaInicio"]);

                            if ((_VS_Parametros.Rows[i]["par_chave"].Equals("LOG_ERROS_GRAVAR_QUERYSTRING")) ||
                                (_VS_Parametros.Rows[i]["par_chave"].Equals("LOG_ERROS_GRAVAR_SERVERVARIABLES")) ||
                                (_VS_Parametros.Rows[i]["par_chave"].Equals("LOG_ERROS_GRAVAR_PARAMS")))
                                _VS_Parametros.Rows[i]["par_vigencia"] = "*";
                            else
                                _VS_Parametros.Rows[i]["par_vigencia"] = Convert.ToString(dt.Rows[j]["par_vigencia"]);
                        }
                    }
                }

                _dgvParametro1.DataSource = _VS_Parametros;
                _dgvParametro1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o parâmetro.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Carrega os dados do Parametro nos controles caso seja alteração.
    /// </summary>
    /// <param name="par_id"></param>
    private void _CarregarParametro(Guid par_id)
    {
        try
        {
            SYS_Parametro _Parametro = new SYS_Parametro { par_id = par_id };
            SYS_ParametroBO.GetEntity(_Parametro);

            _VS_par_id = _Parametro.par_id;
            _lblNome_Par.Text = _VS_par_descricao;

            if (!_Parametro.IsNew)
            {
                _VS_par_chave = _Parametro.par_chave;
                _VS_par_descricao = _Parametro.par_descricao;
                _VS_par_obrigatorio = _Parametro.par_obrigatorio;
            }

            switch (_VS_par_chave)
            {
                // Parâmetros texto
                case "URL_ADMINISTRATIVO":
                case "URL_CLIENTE":
                case "TITULO_GERAL":
                case "MENSAGEM_COPYRIGHT":
                case "TAMANHO_MAX_FOTO_PESSOA":
                case "FORMATO_SENHA_USUARIO":
                case "TAMANHO_SENHA_USUARIO":
                case "LOG_ERROS_CHAVES_NAO_GRAVAR":
                case "HELP_DESK_CONTATO":
                case "MENSAGEM_ICONE_HELP":
                case "SUPORTE_TECNICO_EMAILS":
                case "ID_GOOGLE_ANALYTICS":
                case "MENSAGEM_ALERTA_PRELOGIN":
                case "INTERVALO_MINUTOS_VERIFICAR_FALHA_AUTENTICACAO":
                case "QUANTIDADE_FALHAS_AUTENTICACAO_EXIBIR_CAPTCHA":
                case "VERSAO_WEBAPI_CORESSO":
                case "URL_WEBAPI_CORESSO":
                case "QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO":
                case "PRAZO_DIAS_EXPIRA_SENHA":
                    {
                        txtValor.Text = _Parametro.par_valor;
                        _txtVigenciaIni.Text = (_Parametro.IsNew ? DateTime.Now : _Parametro.par_vigenciaInicio).ToString("dd/MM/yyyy");
                        _txtVigenciaFim.Text = "";

                        break;
                    }
                // Parâmetros imagens
                case "LOGO_CLIENTE":
                case "LOGO_GERAL_SISTEMA":
                    {
                        break;
                    }
                // Parâmetros sim ou não (true/false) e a quantidade de itens
                case "PERMITIR_LOGIN_COM_PROVIDER_EXTERNO":
                case "PERMITIR_MULTIPLOS_ENDERECOS_UA":
                case "ENDERECO_OBRIGATORIO_CADASTRO_UA":
                case "PERMITIR_MULTIPLOS_ENDERECOS_ENTIDADE":
                case "PERMITIR_MULTIPLOS_ENDERECOS_PESSOA":
                case "PERMITIR_TIPO_CONTATOS_DUPLICADOS":
                case "HABILITAR_VALIDACAO_DUPLICIDADE_TIPO_DOCUMENTO_POR_CLASSIFICACAO":
                case "PERMITIR_CADASTRAR_DOCUMENTACAO_POR_CLASSIFICACAO":
                case "SALVAR_SEMPRE_MAIUSCULO":
                case "EXIBIR_LOGO_CLIENTE":
                case "LOG_ERROS_GRAVAR_QUERYSTRING":
                case "LOG_ERROS_GRAVAR_SERVERVARIABLES":
                case "LOG_ERROS_GRAVAR_PARAMS":
                case "QT_ITENS_PAGINACAO":
                case "REMOVER_OPCAO_ESQUECISENHA":
                case "UTILIZAR_CAPTCHA_FALHA_AUTENTICACAO":
                case "PERMITIR_DTNASCIMENTO_CPF_ESQUECISENHA":
                case "VALIDAR_UNICIDADE_EMAIL_USUARIO":
                case "VALIDAR_OBRIGATORIEDADE_EMAIL_USUARIO":
                case "PERMITIR_ALTERAR_EMAIL_MEUSDADOS":
                case "SALVAR_HISTORICO_SENHA_USUARIO":
                case "PERMITIR_INTEGRACAO_SENHA_EXPIRADA_AD":
                    {
                        _ddlParametroValor.SelectedValue = _Parametro.par_valor;

                        _txtVigenciaIni.Text = (_Parametro.IsNew ? DateTime.Now : _Parametro.par_vigenciaInicio).ToString("dd/MM/yyyy");
                        _txtVigenciaFim.Text = "";

                        break;
                    }
                // Parâmetros lista
                default:
                    {
                        _ddlParametroValor.SelectedValue = _Parametro.par_valor;

                        _txtVigenciaIni.Text = _Parametro.par_vigenciaInicio.ToString("dd/MM/yyyy");
                        _txtVigenciaFim.Text = (_Parametro.par_vigenciaFim != new DateTime()) ? _Parametro.par_vigenciaFim.ToString("dd/MM/yyyy") : string.Empty;

                        _txtVigenciaIni.Enabled = false;
                        _txtVigenciaFim.Enabled = false;

                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o parâmetro.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Insere e altera um Parametro.
    /// </summary>
    private void _Salvar()
    {
        try
        {
            bool limparCampos = false;

            string par_valor;
            _lblNome_Par.Text = _VS_par_descricao;

            if ((_VS_par_chave.Equals("LOGO_CLIENTE")) ||
                (_VS_par_chave.Equals("LOGO_GERAL_SISTEMA")))
            {
                // Não salva no banco, só salva o arquivo físico no servidor.
                if (fupArquivo.PostedFile != null)
                {
                    int w = (_VS_par_chave.Equals("LOGO_CLIENTE") ? 84 : 220);
                    int h = (_VS_par_chave.Equals("LOGO_CLIENTE") ? 60 : 75);

                    string path = CaminhoFisicoLogos;

                    string nomeArquivo = _VS_par_chave + ".png";

                    try
                    {
                        if (UtilBO.SaveThumbnailImage
                            (
                            1000
                            , path
                            , nomeArquivo
                            , fupArquivo.PostedFile
                            , w
                            , h
                            ))
                        {
                            _lblMessageInsert.Text = UtilBO.GetErroMessage("Parâmetro salvo com sucesso.", UtilBO.TipoMensagem.Sucesso);
                        }
                    }
                    catch (ValidationException ex)
                    {
                        _lblMessageInsert.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
                    }
                    catch (Exception ex)
                    {
                        ApplicationWEB._GravaErro(ex);
                        _lblMessageInsert.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o parâmetro.", UtilBO.TipoMensagem.Erro);
                    }
                }
            }
            else
            {
                switch (_VS_par_chave)
                {
                    // Parâmetros texto
                    case "URL_ADMINISTRATIVO":
                    case "URL_CLIENTE":
                    case "TITULO_GERAL":
                    case "MENSAGEM_COPYRIGHT":
                    case "TAMANHO_MAX_FOTO_PESSOA":
                    case "FORMATO_SENHA_USUARIO":
                    case "TAMANHO_SENHA_USUARIO":
                    case "LOG_ERROS_CHAVES_NAO_GRAVAR":
                    case "HELP_DESK_CONTATO":
                    case "MENSAGEM_ICONE_HELP":
                    case "SUPORTE_TECNICO_EMAILS":
                    case "ID_GOOGLE_ANALYTICS":
                    case "MENSAGEM_ALERTA_PRELOGIN":
                    case "INTERVALO_MINUTOS_VERIFICAR_FALHA_AUTENTICACAO":
                    case "QUANTIDADE_FALHAS_AUTENTICACAO_EXIBIR_CAPTCHA":
                    case "VERSAO_WEBAPI_CORESSO":
                    case "URL_WEBAPI_CORESSO":
                    case "QUANTIDADE_ULTIMAS_SENHAS_VALIDACAO":
                    case "PRAZO_DIAS_EXPIRA_SENHA":
                        {
                            par_valor = txtValor.Text;

                            break;
                        }
                    // Parâmetros lista
                    default:
                        {
                            par_valor = _ddlParametroValor.SelectedValue;

                            limparCampos = true;

                            break;
                        }
                }

                SYS_Parametro _Parametro = new SYS_Parametro
                {
                    par_id = _VS_par_id
                    ,
                    par_chave = _VS_par_chave
                    ,
                    par_obrigatorio = _VS_par_obrigatorio
                    ,
                    par_descricao = _VS_par_descricao
                    ,
                    par_valor = par_valor
                    ,
                    par_situacao = 1
                    ,
                    par_vigenciaInicio = Convert.ToDateTime(_txtVigenciaIni.Text)
                    ,
                    par_vigenciaFim = string.IsNullOrEmpty(_txtVigenciaFim.Text) ? new DateTime() : Convert.ToDateTime(_txtVigenciaFim.Text)
                    ,
                    IsNew = (_VS_par_id == Guid.Empty)
                };

                if (SYS_ParametroBO.Save(_Parametro))
                {
                    // Recarrega os parâmetros do sistema.
                    SYS_ParametroBO.RecarregaParametrosVigente();

                    ApplicationWEB._GravaLogSistema(_VS_par_id == Guid.Empty ? LOG_SistemaTipo.Insert : LOG_SistemaTipo.Update, "par_id: " + _Parametro.par_id);
                    _lblMessageInsert.Text = UtilBO.GetErroMessage("Parâmetro " + (_VS_par_id == Guid.Empty ? "incluído" : "alterado") + " com sucesso.", UtilBO.TipoMensagem.Sucesso);

                    if (limparCampos)
                    {
                        _LimpaCamposParametro_Div = true;
                        _VS_par_obrigatorio = true;
                    }
                }
                else
                {
                    _lblMessageInsert.Text = UtilBO.GetErroMessage("Erro ao tentar salvar valor para o parâmetro.", UtilBO.TipoMensagem.Erro);
                }
            }
        }
        catch (DuplicateNameException e)
        {
            _lblMessageInsert.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (ArgumentException e)
        {
            _lblMessageInsert.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessageInsert.Text = UtilBO.GetErroMessage("Erro ao tentar salvar valor para o parâmetro.", UtilBO.TipoMensagem.Erro);
        }
        finally
        {
            _MontaGridParametro_Div = true;
            _uppCadastroParametro.Update();
        }
    }

    /// <summary>
    /// Insere um Grupo Padrao.
    /// </summary>
    private void _SalvarGrupoPadrao()
    {
        try
        {
            SYS_ParametroGrupoPerfil _ParametroGrupoPerfil = new SYS_ParametroGrupoPerfil
            {
                pgs_id = _VS_pgs_id
                        ,
                pgs_chave = txtChave.Text
                        ,
                gru_id = new Guid(UCComboSistemaGrupo._Combo.SelectedValue.Split(';')[1])
                        ,
                pgs_situacao = Convert.ToByte(1)
                        ,
                IsNew = (_VS_pgs_id != Guid.Empty) ? false : true
            };
            if (SYS_ParametroGrupoPerfilBO.Save(_ParametroGrupoPerfil))
            {
                if (_VS_pgs_id != Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "pgs_id: " + _ParametroGrupoPerfil.pgs_id);
                    lblMessage.Text = UtilBO.GetErroMessage("Grupo padrão alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "pgs_id: " + _ParametroGrupoPerfil.pgs_id);
                    lblMessage.Text = UtilBO.GetErroMessage("Grupo padrão incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
            }
            else
            {
                lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar valor para o grupo padrão.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (DuplicateNameException e)
        {
            lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (ArgumentException e)
        {
            lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar valor para o grupo padrão.", UtilBO.TipoMensagem.Erro);
        }
        finally
        {
            _dgvGrupoPadrao.DataBind();
            _uppGridGrupoPadrao.Update();
            _uppCadastroGrupoPerfil.Update();
        }
    }

    /// <summary>
    /// Limpa campos da DIV de cadastro de Grupo Padrão
    /// </summary>
    private void _LimpaCampos()
    {
        txtChave.Text = string.Empty;
        UCComboSistemaGrupo._Combo.SelectedIndex = 0;
    }

    #endregion Métodos

    #region Eventos

    protected void _dgvParametro1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        e.Cancel = true;
        _LimpaCamposParametro_Div = true;

        _VS_par_chave = Convert.ToString((_dgvParametro1.DataKeys[e.NewEditIndex].Values[1]));
        _VS_par_obrigatorio = Convert.ToBoolean((_dgvParametro1.DataKeys[e.NewEditIndex].Values[2]));
        _VS_par_descricao = Convert.ToString((_dgvParametro1.DataKeys[e.NewEditIndex].Values[3]));

        _MontarDDLParametroValor(_VS_par_chave);

        _MontaGridParametro_Div = true;

        _HabilitaCamposObrigatorios = true;
        _uppCadastroParametro.Update();
        ScriptManager.RegisterStartupScript(this, GetType(), "CadastroParametros", "$(document).ready(function(){ $('#divParametro').dialog('open');});", true);
    }

    protected void _dgvParametro1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnAlterar = (ImageButton)e.Row.FindControl("_btnAlterar");
            if (_btnAlterar != null)
            {
                _btnAlterar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
            }

            string chave = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "par_chave"));

            if ((chave.Equals("LOGO_GERAL_SISTEMA")) || (chave.Equals("LOGO_CLIENTE")))
            {
                Image img = (Image)e.Row.FindControl("img");

                if (img != null)
                {
                    if (UtilBO.ExisteArquivo(CaminhoFisicoLogos + chave + ".png"))
                    {
                        img.Visible = true;
                        img.ImageUrl = caminhoLogos + chave + ".png";
                    }
                }
            }
        }
    }

    protected void _odsParametro2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _dgvParametro2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (btnDelete != null)
            {
                if (_VS_par_obrigatorio && (UtilBO.VerificaDataMaior(DateTime.Now, Convert.ToDateTime(btnDelete.CommandArgument)) || UtilBO.VerificaDataIgual(Convert.ToDateTime(btnDelete.CommandArgument), DateTime.Now)))
                {
                    btnDelete.Visible = false;
                }
                btnDelete.CommandArgument = e.Row.RowIndex.ToString();
            }

            ImageButton btnAlterar = (ImageButton)e.Row.FindControl("_btnAlterar");
            if (btnAlterar != null)
            {
                if (!String.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "par_vigenciaFim").ToString()))
                {
                    if (Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "par_vigenciaFim")) < DateTime.Now)
                    {
                        btnAlterar.Visible = false;
                    }
                }
            }
        }
    }

    protected void _dgvParametro2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            int index = int.Parse(e.CommandArgument.ToString());

            Guid par_id = new Guid(_dgvParametro2.DataKeys[index].Value.ToString());
            SYS_Parametro entity = new SYS_Parametro { par_id = par_id };
            SYS_ParametroBO.GetEntity(entity);

            if (SYS_ParametroBO.Delete(entity))
            {
                _dgvParametro2.PageIndex = 0;
                _lblMessageInsert.Text = UtilBO.GetErroMessage("Parâmetro excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "par_id: " + par_id);
            }
            else
            {
                _lblMessageInsert.Text = UtilBO.GetErroMessage("Não é possível excluir este parâmetro.", UtilBO.TipoMensagem.Erro);
            }

            _MontaGridParametro_Div = true;
            _uppCadastroParametro.Update();
        }
    }

    protected void _dgvParametro2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        e.Cancel = true;
        _VS_alteracao = true;
        _CarregarParametro(new Guid(_dgvParametro2.DataKeys[e.NewEditIndex].Value.ToString()));
        _HabilitaCamposObrigatorios = true;
        _MontaGridParametro_Div = true;
        _uppCadastroParametro.Update();
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        if (divFileUpload.Visible || Page.IsValid)
        {
            _Salvar();
            _Carga_VS_Parametros();
        }
    }

    protected void _dgvGrupoPadrao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (btnExcluir != null)
            {
                btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
                btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
            }
        }
    }

    protected void _dgvGrupoPadrao_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletar")
        {
            int index = int.Parse(e.CommandArgument.ToString());

            Guid pgs_id = new Guid(_dgvGrupoPadrao.DataKeys[index].Value.ToString());
            SYS_ParametroGrupoPerfil entity = new SYS_ParametroGrupoPerfil { pgs_id = pgs_id };
            SYS_ParametroGrupoPerfilBO.GetEntity(entity);

            if (SYS_ParametroGrupoPerfilBO.Delete(entity))
            {
                _dgvGrupoPadrao.PageIndex = 0;
                _dgvGrupoPadrao.DataBind();
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Delete, "pgs_id: " + pgs_id);
                lblMessage.Text = UtilBO.GetErroMessage("Grupo padrão excluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
            }
        }
    }

    protected void _dgvGrupoPadrao_RowEditing(object sender, GridViewEditEventArgs e)
    {
        e.Cancel = true;
    }

    protected void btnSalvar_Click1(object sender, EventArgs e)
    {
        _SalvarGrupoPadrao();
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "CalendarioPeriodico", "$('#divParametroGrupoPerfil').dialog('close');", true);
        _LimpaCampos();
    }

    protected void _odsParametroGrupoPerfil_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        _Carga_VS_Parametros();
    }

    protected void _btnNovoGrupoPadrao_Click(object sender, EventArgs e)
    {
        _LimpaCampos();
        _uppCadastroGrupoPerfil.Update();
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "CalendarioPeriodico", "$('#divParametroGrupoPerfil').dialog('open');", true);
    }

    #endregion Eventos
}