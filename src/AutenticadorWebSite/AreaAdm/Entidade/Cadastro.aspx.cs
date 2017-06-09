using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using CoreLibrary.Validation.Exceptions;
using System;
using System.Data;
using System.IO;
using System.Web.UI;

public partial class AreaAdm_Entidade_Cadastro : MotherPageLogado
{
    #region Propriedades

    /// <summary>
    /// Indica se é uma alteração ou inclusão de endereço
    /// </summary>
    public bool _VS_IsNew_end_id
    {
        get
        {
            return Convert.ToBoolean(ViewState["_VS_IsNew_end_id"]);
        }
        set
        {
            ViewState["_VS_IsNew_end_id"] = value;
        }
    }

    private Guid _VS_end_idAntigo
    {
        get
        {
            if (ViewState["_VS_end_idAntigo"] != null)
                return new Guid(ViewState["_VS_end_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_end_idAntigo"] = value;
        }
    }

    private Guid _VS_ent_id
    {
        get
        {
            if (ViewState["_VS_ent_id"] != null)
                return new Guid(ViewState["_VS_ent_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_ent_id"] = value;
        }
    }

    private Guid _VS_ent_idSuperiorAntigo
    {
        get
        {
            if (ViewState["_VS_ent_idSuperiorAntigo"] != null)
                return new Guid(ViewState["_VS_ent_idSuperiorAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_ent_idSuperiorAntigo"] = value;
        }
    }

    #endregion Propriedades

    #region Métodos

    /// <summary>
    /// Carrega os dados da entidade nos controles caso seja alteração.
    /// </summary>
    private void _LoadFromEntity()
    {
        try
        {
            SYS_Entidade ent = new SYS_Entidade { ent_id = _VS_ent_id };
            SYS_EntidadeBO.GetEntity(ent);

            _VS_ent_id = ent.ent_id;
            _VS_ent_idSuperiorAntigo = ent.ent_idSuperior;
            UCComboTipoEntidade1._Combo.SelectedValue = ent.ten_id.ToString();
            _txtRazaoSocial.Text = ent.ent_razaoSocial;
            _txtNomeFantasia.Text = (!string.IsNullOrEmpty(ent.ent_nomeFantasia) ? ent.ent_nomeFantasia : string.Empty);
            _txtSigla.Text = (!string.IsNullOrEmpty(ent.ent_sigla) ? ent.ent_sigla : string.Empty);
            _txtCodigo.Text = (!string.IsNullOrEmpty(ent.ent_codigo) ? ent.ent_codigo : string.Empty);
            _txtCNPJ.Text = (!string.IsNullOrEmpty(ent.ent_cnpj) ? ent.ent_cnpj : string.Empty);
            _txtIE.Text = (!string.IsNullOrEmpty(ent.ent_inscricaoEstadual) ? ent.ent_inscricaoEstadual : string.Empty);
            _txtIM.Text = (!string.IsNullOrEmpty(ent.ent_inscricaoMunicipal) ? ent.ent_inscricaoMunicipal : string.Empty);
            UCComboEntidade1._Combo.SelectedValue = (ent.ent_idSuperior != Guid.Empty) ? Convert.ToString(ent.ent_idSuperior) : Guid.Empty.ToString();
            txtUrlAcesso.Text = ent.ent_urlAcesso;
            chkExibeLogoCliente.Checked = ent.ent_exibeLogoCliente;

            if (ent.tep_id > 0)
            {
                UCComboTemaPadrao.Valor = ent.tep_id;
                UCComboTemaPadrao_IndexChanged();

                CFG_TemaPadrao entTema = new CFG_TemaPadrao { tep_id = ent.tep_id };
                CFG_TemaPadraoBO.GetEntity(entTema);

                string caminho = "~/App_Themes/" + entTema.tep_nome + "/images/logos/";

                if (!string.IsNullOrEmpty(ent.ent_logoCliente) && UtilBO.ExisteArquivo(Server.MapPath(caminho + ent.ent_id + "_" + ent.ent_logoCliente)))
                {
                    imgLogoCliente.Visible = true;
                    imgLogoCliente.ImageUrl = caminho + ent.ent_id + "_" + ent.ent_logoCliente;
                }

                UCComboTemaPaleta.Valor = ent.tpl_id > 0 ? new int[] { ent.tep_id, ent.tpl_id } : new int[] { -1, -1 };
            }
            else
            {
                UCComboTemaPadrao.Valor = -1;
            }

            _chkBloqueado.Checked = ent.ent_situacao == 2;

            DataTable dtEndereco = SYS_EntidadeEnderecoBO.CarregarEnderecosBy_Entidade(ent.ent_id);
            UCEnderecos1.CarregarEnderecosBanco(dtEndereco);

            DataTable dt = SYS_EntidadeContatoBO.GetSelect(_VS_ent_id, false, 1, 1);
            if (dt.Rows.Count == 0)
                dt = null;

            UCGridContato1._VS_contatos = dt;

            UCGridContato1._CarregarContato();

            UCComboTipoEntidade1._Combo.Enabled = false;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a entidade.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Insere e altera uma entidade
    /// </summary>
    private void _Salvar()
    {
        try
        {
            string msgErro;
            if (String.IsNullOrEmpty(_lblMessage.Text.Trim()) && !UCGridContato1.SalvaConteudoGrid(out msgErro))
            {
                UCGridContato1._MensagemErro.Visible = false;
                _lblMessage.Text = msgErro;
                txtSelectedTab.Value = "2";
                return;
            }

            string msg;
            DataTable dtEndereco;
            bool cadastraEndereco = UCEnderecos1.RetornaEnderecoCadastrado(out dtEndereco, out msg);

            if (!cadastraEndereco)
            {
                throw new ValidationException(msg);
            }

            SYS_Entidade entityEntidade = new SYS_Entidade
            {
                ent_id = _VS_ent_id
                ,
                ten_id = new Guid(UCComboTipoEntidade1._Combo.SelectedValue)
                ,
                ent_razaoSocial = _txtRazaoSocial.Text
                ,
                ent_nomeFantasia = _txtNomeFantasia.Text
                ,
                ent_sigla = _txtSigla.Text
                ,
                ent_codigo = _txtCodigo.Text
                ,
                ent_cnpj = _txtCNPJ.Text
                ,
                ent_inscricaoEstadual = _txtIE.Text
                ,
                ent_inscricaoMunicipal = _txtIM.Text
                ,
                ent_idSuperior = new Guid(UCComboEntidade1._Combo.SelectedValue)
                ,
                ent_situacao = (_chkBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1))
                ,
                ent_urlAcesso = txtUrlAcesso.Text
                ,
                ent_logoCliente = !string.IsNullOrEmpty(fupLogoCliente.FileName) ?
                                    fupLogoCliente.FileName :
                                    (!string.IsNullOrEmpty(imgLogoCliente.ImageUrl) ? Path.GetFileName(imgLogoCliente.ImageUrl) : string.Empty)
                ,
                ent_exibeLogoCliente = chkExibeLogoCliente.Checked
                ,
                tep_id = UCComboTemaPadrao.Valor
                ,
                tpl_id = UCComboTemaPaleta.tpl_id
                ,
                IsNew = (_VS_ent_id != Guid.Empty) ? false : true
            };

            if (!entityEntidade.IsNew && string.IsNullOrEmpty(fupLogoCliente.FileName) && !string.IsNullOrEmpty(imgLogoCliente.ImageUrl))
            {
                entityEntidade.ent_logoCliente = entityEntidade.ent_logoCliente.Remove(0, entityEntidade.ent_id.ToString().Length + 1);
            }

            string tema = NomeTemaAtual;

            if (UCComboTemaPadrao.Valor > 0)
            {
                CFG_TemaPadrao entTema = new CFG_TemaPadrao { tep_id = UCComboTemaPadrao.Valor };
                CFG_TemaPadraoBO.GetEntity(entTema);

                tema = entTema.tep_nome;
            }

            string caminho = Server.MapPath("~/App_Themes/" + tema + "/images/logos/");

            if (SYS_EntidadeBO.Save(entityEntidade, dtEndereco, UCGridContato1._VS_contatos, _VS_ent_idSuperiorAntigo, _VS_end_idAntigo, caminho, fupLogoCliente.FileName, fupLogoCliente.PostedFile, null))
            {
                if (_VS_ent_id == Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "ent_id: " + entityEntidade.ent_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Entidade incluída com sucesso."), UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "ent_id: " + entityEntidade.ent_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Entidade alterada com sucesso."), UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Entidade/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a entidade.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (ValidationException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (ArgumentException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (DuplicateNameException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a entidade.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion Métodos

    #region Eventos

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "Entidade/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsUCCadastroContato.js"));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsUCCadastroEndereco.js"));
            sm.Services.Add(new ServiceReference("~/WSServicos.asmx"));
        }

        UCComboTemaPadrao.IndexChanged += UCComboTemaPadrao_IndexChanged;

        if (!IsPostBack)
        {
            try
            {
                // Validação para utilização de múltiplos endereços para a entidade
                bool permitirMultiplosEnderecos = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.PERMITIR_MULTIPLOS_ENDERECOS_ENTIDADE);
                UCEnderecos1.Inicializar(false, !permitirMultiplosEnderecos, "Entidade");

                UCComboTipoEntidade1.Inicialize("Tipo de entidade *");
                UCComboTipoEntidade1._EnableValidator = true;
                UCComboTipoEntidade1._ValidationGroup = "Endereco";
                UCComboTipoEntidade1._Load(0);

                UCComboTemaPadrao.Carregar();
                UCComboTemaPadrao_IndexChanged();

                if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                {
                    _VS_ent_id = PreviousPage.EditItem;

                    _LoadFromEntity();

                    _chkBloqueado.Visible = true;
                    Page.Form.DefaultFocus = _txtRazaoSocial.ClientID;
                }
                else
                {
                    UCGridContato1._CarregarContato();

                    _chkBloqueado.Visible = false;
                    _chkBloqueado.Checked = false;

                    _VS_IsNew_end_id = true;

                    Page.Form.DefaultFocus = UCComboTipoEntidade1._Combo.ClientID;
                    this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                }

                UCComboEntidade1.Inicialize("Entidade superior");
                UCComboEntidade1._EnableValidator = false;
                UCComboEntidade1._Load(_VS_ent_id, 0);

                Page.Form.DefaultButton = _btnSalvar.UniqueID;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }
        }
    }

    private void UCComboTemaPadrao_IndexChanged()
    {
        if (UCComboTemaPadrao.Valor > 0)
        {
            UCComboTemaPaleta.PermiteEditar = true;
            UCComboTemaPaleta.CarregarPorTemaPadrao(UCComboTemaPadrao.Valor);
        }
        else
        {
            UCComboTemaPaleta.Valor = new int[] { -1, -1 };
            UCComboTemaPaleta.PermiteEditar = false;
        }
    }

    #endregion Eventos
}