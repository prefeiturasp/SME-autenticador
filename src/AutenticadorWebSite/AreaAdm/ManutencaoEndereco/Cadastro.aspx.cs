using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;

public partial class AreaAdm_ManutencaoEndereco_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsUCCadastroEndereco.js"));
            sm.Services.Add(new ServiceReference("~/WSServicos.asmx"));
        }

        if (!IsPostBack)
        {
            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                _VS_end_id = PreviousPage.EditItem;

                _LoadFromEntity();

                Page.Form.DefaultFocus = txtLogradouro.ClientID;
            }
            else
            {
                Page.Form.DefaultFocus = txtCEP.ClientID;
                this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }

            LabelNumero.Visible = false;
            txtNumero.Visible = false;
            rfvNumero.Visible = false;
            LabelComplemento.Visible = false;
            txtComplemento.Visible = false;

            txtLogradouro.CssClass = "text60C";

            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }
    }

    #region PROPRIEDADES

    private Guid _VS_cid_idAntigo
    {
        get
        {
            if (ViewState["_VS_cid_idAntigo"] != null)
                return new Guid(ViewState["_VS_cid_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_cid_idAntigo"] = value;
        }
    }

    /// <summary>
    /// Retorna e atribui o ViewState com o id do endereço
    /// </summary>
    public Guid _VS_end_id
    {
        get
        {
            if (!string.IsNullOrEmpty(_txtEnd_id.Value))
                return new Guid(_txtEnd_id.Value);
            return ViewState["_VS_end_id"] != null ? new Guid(ViewState["_VS_end_id"].ToString()) : Guid.Empty;
        }
        set
        {
            ViewState["_VS_end_id"] = value;
        }
    }

    /// <summary>
    /// Retorna e atribui o ViewState com o id da cidade
    /// </summary>
    public Guid _VS_cid_id
    {
        get
        {
            if ((!string.IsNullOrEmpty(_txtCid_id.Value)) && (new Guid(_txtCid_id.Value) != Guid.Empty))
                return new Guid(_txtCid_id.Value);
            return ViewState["_VS_cid_id"] != null ? new Guid(ViewState["_VS_cid_id"].ToString()) : Guid.Empty;
        }
        set
        {
            ViewState["_VS_cid_id"] = value;
        }
    }

    #endregion PROPRIEDADES

    #region METODOS

    protected void UCCidade1BuscaCidade(IDictionary<string, object> parameters)
    {
        _VS_cid_id = new Guid(parameters["cid_id"].ToString());
        txtCidade.Text = parameters["cid_nome"].ToString();
    }

    /// <summary>
    /// Carrega os dados do endereço nos controles caso seja alteração.
    /// </summary>
    private void _LoadFromEntity()
    {
        try
        {
            END_Endereco end = new END_Endereco { end_id = _VS_end_id };
            END_EnderecoBO.GetEntity(end);

            END_Cidade cid = new END_Cidade { cid_id = end.cid_id };
            END_CidadeBO.GetEntity(cid);

            _VS_cid_idAntigo = end.cid_id;

            txtCEP.Text = end.end_cep;
            txtLogradouro.Text = end.end_logradouro;
            txtDistrito.Text = !string.IsNullOrEmpty(end.end_distrito) ? end.end_distrito : string.Empty;
            UCComboZona1._Combo.SelectedValue = end.end_zona > 0 ? end.end_zona.ToString() : "-1";
            txtBairro.Text = end.end_bairro;
            _VS_cid_id = end.cid_id;
            txtCidade.Text = cid.cid_nome;

            txtCEP.Enabled = false;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Insere e altera uma entidade
    /// </summary>
    private void _Salvar()
    {
        try
        {
            //[Gabriel]: Antes o sistema buscava no banco alguma cidade com o cep passado pelo usuario e setava essa cidade sem nenhum aviso, desprezando a cidade passada no formulário
            //List<END_Endereco> end = END_EnderecoBO.GetSelectBy_end_cep_end_logradouro(txtCEP.Text, DBNull.Value.ToString());
            
            END_Endereco entityEndereco = new END_Endereco
            {
                end_id = _VS_end_id
                ,
                end_cep = txtCEP.Text
                ,
                end_logradouro = txtLogradouro.Text
                ,
                end_distrito = txtDistrito.Text
                ,
                end_zona = UCComboZona1._Combo.SelectedValue == "-1" ? Convert.ToByte(0) : Convert.ToByte(UCComboZona1._Combo.SelectedValue)
                ,
                end_bairro = txtBairro.Text
                ,
                //cid_id = (end.Count > 0) ? end[0].cid_id : _VS_cid_id
                cid_id = _VS_cid_id
                ,
                end_situacao = 1
                ,
                IsNew = (_VS_end_id != Guid.Empty) ? false : true
            };

            if (END_EnderecoBO.Save(entityEndereco, _VS_cid_idAntigo, null) != Guid.Empty)
            {
                if (_VS_end_id == Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "end_id: " + entityEndereco.end_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Endereço incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "end_id: " + entityEndereco.end_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Endereço alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoEndereco/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o endereço.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (CoreLibrary.Validation.Exceptions.ValidationException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion METODOS

    #region EVENTOS

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoEndereco/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Salvar();
    }

    protected void btnIncluirEndereco_Click(object sender, ImageClickEventArgs e)
    {
        _VS_end_id = Guid.Empty;
        _txtEnd_id.Value = Guid.Empty.ToString();

        txtLogradouro.Text = string.Empty;
        txtDistrito.Text = string.Empty;
        UCComboZona1._Combo.SelectedValue = "-1";
        txtBairro.Text = string.Empty;

        txtDistrito.Enabled = true;
        UCComboZona1._Combo.Enabled = true;
        txtLogradouro.Enabled = true;
        txtBairro.Enabled = true;
        txtCidade.Enabled = true;

        txtLogradouro.Focus();
    }

    #endregion EVENTOS
}