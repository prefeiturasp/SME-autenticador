using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using CoreLibrary.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AreaAdm_UA_Cadastro : MotherPageLogado
{
    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "UA/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }

    /// <summary>
    /// Botão para limpar uadSuperior.
    /// </summary>
    protected void btnLimpar_OnClick(object sender, EventArgs e)
    {
        try
        {
            _txtUad_nome.Text = UCUASuperior.VsUadNome = string.Empty;
            UCUASuperior.VsUadId = UCUASuperior.VsEntId = Guid.Empty;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar limpar unidade administrativa superior", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Procurar Unidade administrativa superior.
    /// </summary>
    protected void btnProcurarUASuperior_Click(object sender, EventArgs e)
    {
        try
        {
            if (UCComboEntidade1._Combo.SelectedIndex > 0)
            {
                UCUASuperior.VsEntId = new Guid(UCComboEntidade1._Combo.SelectedValue);
                UCUASuperior.ExibirForm();
            }
            else
            {
                _lblMessage.Text = UtilBO.GetMessage("É necessário selecionar a entidade.", UtilBO.TipoMensagem.Alerta);
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar abrir popUp unidade administrativa superior.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsUCCadastroContato.js"));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsUCCadastroEndereco.js"));
            sm.Services.Add(new ServiceReference("~/WSServicos.asmx"));
        }

        if (!IsPostBack)
        {
            string ent_padrao = __SessionWEB.__UsuarioWEB.Usuario.ent_id.ToString();
            SetFocus(UCComboTipoUnidadeAdministrativa1);
            try
            {
                // Validação para utilização de múltiplos endereços para a unidade administrativa
                bool permitirMultiplosEnderecos = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.PERMITIR_MULTIPLOS_ENDERECOS_UA);
                UCEnderecos1.Inicializar(false, !permitirMultiplosEnderecos, string.Empty);

                if (!string.IsNullOrEmpty(ent_padrao))
                {
                    _VS_ent_id = new Guid(ent_padrao);
                }

                UCComboEntidade1._ShowSelectMessage = true;
                if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.Administracao)
                {
                    UCComboEntidade1._Load(Guid.Empty, 0);
                }
                else
                {
                    UCComboEntidade1._LoadBy_UsuarioGrupoUA(Guid.Empty, __SessionWEB.__UsuarioWEB.Grupo.gru_id, __SessionWEB.__UsuarioWEB.Usuario.usu_id, 0);
                }

                UCComboEntidade1.Inicialize("Entidade *");

                UCComboEntidade1._Combo.SelectedValue = string.IsNullOrEmpty(ent_padrao) ? Guid.Empty.ToString() : ent_padrao;

                UCComboTipoUnidadeAdministrativa1._ShowSelectMessage = true;
                UCComboTipoUnidadeAdministrativa1._Load(Guid.Empty, 0);
                UCComboTipoUnidadeAdministrativa1.Inicialize("Tipo de unidade administrativa *");

                if ((PreviousPage != null) && PreviousPage.IsCrossPagePostBack)
                {
                    _VS_ent_id = PreviousPage.EditItem_ent_id;
                    _VS_uad_id = PreviousPage.EditItem_uad_id;

                    _LoadFromEntity();

                    _chkBloqueado.Visible = true;

                    Page.Form.DefaultFocus = _txtNome.ClientID;
                }
                else
                {
                    UCGridContato1._CarregarContato();

                    _chkBloqueado.Visible = false;
                    _chkBloqueado.Checked = false;

                    _VS_IsNew_end_id = true;

                    Page.Form.DefaultFocus = UCComboEntidade1._Combo.ClientID;

                    this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                }

                UCUASuperior.VsGruId = __SessionWEB.__UsuarioWEB.Grupo.gru_id;
                UCUASuperior.VsUsuId = __SessionWEB.__UsuarioWEB.Usuario.usu_id;
                UCUASuperior.VsVisId = __SessionWEB.__UsuarioWEB.Grupo.vis_id;

                Page.Form.DefaultButton = _btnSalvar.UniqueID;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }
        }

        if (__SessionWEB.__UsuarioWEB.Grupo.vis_id == SysVisaoID.UnidadeAdministrativa)
        {
            UCComboEntidade1._Combo.Enabled = false;
        }

        Delegates();

        UCComboEntidade1.OnSelectedIndexChange += UCComboEntidade1__IndexChanged;
    }

    #region PROPRIEDADES

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
            {
                return new Guid(ViewState["_VS_end_idAntigo"].ToString());
            }

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
            {
                return new Guid(ViewState["_VS_ent_id"].ToString());
            }

            return Guid.Empty;
        }

        set
        {
            ViewState["_VS_ent_id"] = value;
        }
    }

    private Guid _VS_uad_id
    {
        get
        {
            if (ViewState["_VS_uad_id"] != null)
            {
                return new Guid(ViewState["_VS_uad_id"].ToString());
            }

            return Guid.Empty;
        }

        set
        {
            ViewState["_VS_uad_id"] = value;
        }
    }

    private Guid _VS_uad_idSuperiorAntigo
    {
        get
        {
            if (ViewState["_VS_uad_idSuperiorAntigo"] != null)
            {
                return new Guid(ViewState["_VS_uad_idSuperiorAntigo"].ToString());
            }

            return Guid.Empty;
        }

        set
        {
            ViewState["_VS_uad_idSuperiorAntigo"] = value;
        }
    }

    #endregion PROPRIEDADES

    #region DELEGATES

    /// <summary>
    /// Atribui ao evento do delegate o método criado ao selecionar um popup.
    /// </summary>
    protected void Delegates()
    {
        try
        {
            UCUASuperior.RowCommandSelecionar += UCUASuperior__RowCommandSelecionar;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o popUp.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Disparado ao selecionar uma pessoa.
    /// </summary>
    protected void UCUASuperior__RowCommandSelecionar(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (UCUASuperior.VsUadId != new Guid())
                _txtUad_nome.Text = UCUASuperior.VsUadNome;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar selecionar o popUp.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void UCComboEntidade1__IndexChanged(object sender, EventArgs e)
    {
        try
        {
            _txtUad_nome.Text = UCUASuperior.VsUadNome = string.Empty;
            UCUASuperior.VsEntId = UCUASuperior.VsUadId = Guid.Empty;

            UCComboTipoUnidadeAdministrativa1._Combo.Focus();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar limpar campo", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion DELEGATES

    #region METODOS

    public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;
    }

    /// <summary>
    /// Carrega os dados da unidade administrativa nos controles caso seja alteração.
    /// </summary>
    private void _LoadFromEntity()
    {
        try
        {
            SYS_UnidadeAdministrativa uad = new SYS_UnidadeAdministrativa { ent_id = _VS_ent_id, uad_id = _VS_uad_id };
            SYS_UnidadeAdministrativaBO.GetEntity(uad);

            _VS_ent_id = uad.ent_id;
            _VS_uad_id = uad.uad_id;

            UCUASuperior.VsUadId = _VS_uad_idSuperiorAntigo = uad.uad_idSuperior;
            UCUASuperior._Limpar();

            UCComboEntidade1._Combo.SelectedValue = uad.ent_id.ToString();
            UCComboTipoUnidadeAdministrativa1._Combo.SelectedValue = uad.tua_id.ToString();
            _txtNome.Text = uad.uad_nome;
            _txtSigla.Text = !string.IsNullOrEmpty(uad.uad_sigla) ? uad.uad_sigla : string.Empty;
            _txtCodigo.Text = !string.IsNullOrEmpty(uad.uad_codigo) ? uad.uad_codigo : string.Empty;
            _txtCodigoInep.Text = !string.IsNullOrEmpty(uad.uad_codigoInep) ? uad.uad_codigoInep : string.Empty;
            _txtCodigoIntegracao.Text = uad.uad_codigoIntegracao;

            _chkBloqueado.Checked = uad.uad_situacao == 2;

            //List<SYS_UnidadeAdministrativaEnderecoBO.sUnidadeAdministrativaEndereco> ListEnderecoUA =
            //    SYS_UnidadeAdministrativaEnderecoBO.SelecionaEndereco(uad.ent_id, uad.uad_id);

            //[OLD] DataTable dtEnderecoUA = SYS_UnidadeAdministrativaEnderecoBO.SelecionaEndereco(uad.ent_id, uad.uad_id);
            DataTable dtEnderecoUA = SYS_UnidadeAdministrativaEnderecoBO.CarregaEnderecos(uad.ent_id, uad.uad_id);

            //foreach (SYS_UnidadeAdministrativaEnderecoBO.sUnidadeAdministrativaEndereco uaEndereco in ListEnderecoUA)
            //{
            //    if (uaEndereco.endereco.end_id != Guid.Empty)
            //    {
            //UCEnderecos1.CarregarEndereco(uaEndereco.endereco, uaEndereco.unidadeAdministrativaEndereco.uae_numero,
            //    uaEndereco.unidadeAdministrativaEndereco.uae_complemento
            //   , uaEndereco.unidadeAdministrativaEndereco.uae_latitude
            //   , uaEndereco.unidadeAdministrativaEndereco.uae_longitude
            //   , uaEndereco.unidadeAdministrativaEndereco.uae_enderecoPrincipal);

            UCEnderecos1.CarregarEnderecosBanco(dtEnderecoUA);

            _VS_IsNew_end_id = false;
            //_VS_end_idAntigo = uaEndereco.endereco.end_id;
            //    }
            //    else
            //    {
            //        _VS_IsNew_end_id = true;
            //    }
            //}

            DataTable dt = SYS_UnidadeAdministrativaContatoBO.GetSelect(_VS_ent_id, _VS_uad_id, false, 1, 1);
            if (dt.Rows.Count == 0)
            {
                dt = null;
            }

            UCGridContato1._VS_contatos = dt;
            UCGridContato1._CarregarContato();

            UCComboEntidade1._Combo.Enabled = false;
            UCComboTipoUnidadeAdministrativa1._Combo.Enabled = false;

            if (uad.uad_idSuperior != Guid.Empty)
            {
                UCUASuperior._PesquisarUASuperior(uad.ent_id, uad.uad_idSuperior);
                _txtUad_nome.Text = UCUASuperior.VsUadNome;
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a unidade administrativa.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Insere e altera uma entidade
    /// </summary>
    private void _Salvar()
    {
        try
        {
            List<END_Endereco> ltEntityEndereco = new List<END_Endereco>();
            List<SYS_UnidadeAdministrativaEndereco> ltEntityUAEndereco = new List<SYS_UnidadeAdministrativaEndereco>();

            string numero;
            string complemento;
            string msg;
            decimal latitude;
            decimal longitude;

            DataTable dt;

            bool cadastraEndereco = UCEnderecos1.RetornaEnderecoCadastrado(out dt, out msg);

            if (ValidaCampos(dt))
            {
                string msgErro = string.Empty;
                if (String.IsNullOrEmpty(_lblMessage.Text.Trim()) && !UCGridContato1.SalvaConteudoGrid(out msgErro))
                {
                    UCGridContato1._MensagemErro.Visible = false;
                    _lblMessage.Text = msgErro;
                    txtSelectedTab.Value = "2";
                    return;
                }

                //   Cadastra Unidade Administrativa                
                SYS_UnidadeAdministrativa entityUnidadeAdministrativa = new SYS_UnidadeAdministrativa
                {
                    ent_id = _VS_ent_id
                        ,
                    uad_id = _VS_uad_id
                        ,
                    tua_id = new Guid(UCComboTipoUnidadeAdministrativa1._Combo.SelectedValue)
                        ,
                    uad_nome = _txtNome.Text
                        ,
                    uad_sigla = _txtSigla.Text
                        ,
                    uad_codigo = _txtCodigo.Text
                        ,
                    uad_codigoInep = _txtCodigoInep.Text
                        ,
                    uad_idSuperior = UCUASuperior.VsUadId
                        ,
                    uad_codigoIntegracao = _txtCodigoIntegracao.Text
                        ,
                    uad_situacao = _chkBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1)
                        ,
                    IsNew = (_VS_uad_id != Guid.Empty) ? false : true
                };


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (!cadastraEndereco)
                    {
                        throw new ValidationException(msg);
                    }

                    DataRow dr = dt.Rows[i];
                    string end_id = dr["end_id"].ToString();
                    Guid uae_id = string.IsNullOrEmpty(dr["endRel_id"].ToString()) ? Guid.NewGuid() : new Guid(dr["endRel_id"].ToString());

                    int zona = 0;
                    if (!string.IsNullOrEmpty(dr["end_zona"].ToString()))
                        zona = Convert.ToInt16(dr["end_zona"].ToString());

                    bool excluido = Convert.ToBoolean(dr["excluido"]);

                    if ((String.IsNullOrEmpty(end_id)) || (end_id.Equals(Guid.Empty.ToString())))
                    {
                        ltEntityEndereco.Add(new END_Endereco
                        {
                            end_id = new Guid(dr["id"].ToString()),
                            IsNew = true,
                            cid_nome = dr["cid_nome"].ToString(),
                            end_logradouro = dr["end_logradouro"].ToString(),
                            end_distrito = dr["end_distrito"].ToString(),
                            end_zona = (zona != 0) ? Convert.ToByte(dr["end_zona"].ToString()) : Convert.ToByte(0),
                            end_bairro = dr["end_bairro"].ToString(),
                            cid_id = new Guid(dr["cid_id"].ToString()),
                            end_cep = dr["end_cep"].ToString(),
                            end_situacao =  Convert.ToByte(1)
                        });
                    }
                    else
                    {
                        END_Endereco entityEndereco = new END_Endereco();
                        entityEndereco.end_id = new Guid(dr["end_id"].ToString());
                        entityEndereco.IsNew = false;

                        END_EnderecoBO.GetEntity(entityEndereco);

                        entityEndereco.cid_id = new Guid(dr["cid_id"].ToString());
                        entityEndereco.cid_nome = dr["cid_nome"].ToString();
                        entityEndereco.end_logradouro = dr["end_logradouro"].ToString();
                        entityEndereco.end_distrito = dr["end_distrito"].ToString();
                        entityEndereco.end_zona = (zona != 0) ? Convert.ToByte(dr["end_zona"].ToString()) : Convert.ToByte(0);
                        entityEndereco.end_bairro = dr["end_bairro"].ToString();
                        entityEndereco.end_cep = dr["end_cep"].ToString();
                        entityEndereco.end_situacao = (excluido) ? Convert.ToByte(3) : Convert.ToByte(1);
                        ltEntityEndereco.Add(entityEndereco);
                    }
                    numero = dr["numero"].ToString();
                    complemento = dr["complemento"].ToString();
                    latitude = string.IsNullOrEmpty(dr["latitude"].ToString()) ? 0 : decimal.Parse(dr["latitude"].ToString());
                    longitude = string.IsNullOrEmpty(dr["longitude"].ToString()) ? 0 : decimal.Parse(dr["longitude"].ToString());

                    bool excluirEndereco = String.IsNullOrEmpty(dr["end_cep"].ToString()) && !_VS_IsNew_end_id;


                    if (!String.IsNullOrEmpty(ltEntityEndereco[i].end_cep))
                    {
                        ltEntityUAEndereco.Add(new SYS_UnidadeAdministrativaEndereco()
                        {
                            ent_id = _VS_ent_id,
                            uad_id = _VS_uad_id,
                            end_id = ltEntityEndereco[i].end_id,
                            uae_numero = numero,
                            uae_complemento = complemento,
                            uae_situacao = (excluido) ? Convert.ToByte(3) : Convert.ToByte(1),
                            IsNew = (string.IsNullOrEmpty(dr["novo"].ToString()) ? false : Convert.ToBoolean(dr["novo"].ToString())),
                            uae_latitude = latitude,
                            uae_longitude = longitude,
                            uae_id = uae_id,
                            uae_enderecoPrincipal = (string.IsNullOrEmpty(dr["enderecoprincipal"].ToString()) ? false : Convert.ToBoolean(dr["enderecoprincipal"].ToString())),
                        });
                    }
                }

                if (SYS_UnidadeAdministrativaBO.Save(entityUnidadeAdministrativa, ltEntityEndereco, ltEntityUAEndereco, UCGridContato1._VS_contatos, _VS_uad_idSuperiorAntigo, _VS_end_idAntigo, null))
                {
                    if (_VS_uad_id == Guid.Empty)
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "ent_id: " + entityUnidadeAdministrativa.ent_id + "; uad_id: " + entityUnidadeAdministrativa.uad_id);
                        __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Unidade administrativa incluída com sucesso."), UtilBO.TipoMensagem.Sucesso);
                    }
                    else
                    {
                        ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "ent_id: " + entityUnidadeAdministrativa.ent_id + "; uad_id: " + entityUnidadeAdministrativa.uad_id);
                        __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Unidade administrativa alterada com sucesso."), UtilBO.TipoMensagem.Sucesso);
                    }

                    Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "UA/Busca.aspx", false);
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a unidade administrativa.", UtilBO.TipoMensagem.Erro);
                }
            }
        }
        catch (ValidationException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            SetFocus(ValidationSummary1);
        }
        catch (ArgumentException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            SetFocus(ValidationSummary1);
        }
        catch (DuplicateNameException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            SetFocus(ValidationSummary1);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a unidade administrativa.", UtilBO.TipoMensagem.Erro);
            SetFocus(ValidationSummary1);
        }
    }

    private bool ValidaCampos(DataTable dt)
    {
        bool retorno = true;
        _VS_ent_id = new Guid(UCComboEntidade1._Combo.SelectedValue);
        if (UCComboTipoUnidadeAdministrativa1._Combo.SelectedValue.Equals(Guid.Empty.ToString()))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Tipo Unidade Administrativa é obrigatório!", UtilBO.TipoMensagem.Alerta);
            SetFocus(ValidationSummary1);
            retorno = false;
        }
        else if (string.IsNullOrEmpty(_txtNome.Text))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("O campo Nome é obrigatório!", UtilBO.TipoMensagem.Alerta);
            SetFocus(ValidationSummary1);
            retorno = false;
        }

        int excluidoCount = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            if (Convert.ToBoolean(dr["excluido"]))
                excluidoCount++;
        }


        if (dt.Rows.Count == 0 || excluidoCount == dt.Rows.Count)
        {
            // Validação se é obrigatório endereço para a unidade administrativa
            bool obrigatorioEndereco = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.ENDERECO_OBRIGATORIO_CADASTRO_UA);
            if (obrigatorioEndereco)
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Cadastro de endereço é obrigatório!", UtilBO.TipoMensagem.Alerta);
                SetFocus(ValidationSummary1);
                retorno = false;
            }
        }

        return retorno;
    }

    #endregion METODOS
}