using System;
using System.Web.UI;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Data;
using System.Web.UI.WebControls;

public partial class AreaAdm_ManutencaoCidade_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
        }

        if (!IsPostBack)
        {
            try
            {
                UCComboPais.Inicialize("País *");
                UCComboPais._EnableValidator = true;
                UCComboPais._ValidationGroup = "vlgPais";
                UCComboPais._Load(0);

                UCComboUnidadeFederativa.Inicialize("Estado", false);
                UCComboUnidadeFederativa._EnableValidator = false;
                UCComboUnidadeFederativa._ValidationGroup = "vlgPais";
                UCComboUnidadeFederativa._Load(Guid.Empty, 0);
                UCComboUnidadeFederativa._Combo.Enabled = false;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }

            _Limpar();

            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                _Carregar(PreviousPage.EditItem);
            }
            else
            {
                _btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }

            Page.Form.DefaultFocus = UCComboPais._Combo.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }

        UCComboPais.OnSelectedIndexChange = UCComboPais__IndexChanged;
    }

    #region PROPRIEDADES

    private Guid _VS_cid_id
    {
        get
        {
            if (ViewState["_VS_cid_id"] != null)
                return new Guid(ViewState["_VS_cid_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_cid_id"] = value;
        }
    }

    private Guid _VS_pai_idAntigo
    {
        get
        {
            if (ViewState["_VS_pai_idAntigo"] != null)
                return new Guid(ViewState["_VS_pai_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_pai_idAntigo"] = value;
        }
    }

    private Guid _VS_unf_idAntigo
    {
        get
        {
            if (ViewState["_VS_unf_idAntigo"] != null)
                return new Guid(ViewState["_VS_unf_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_unf_idAntigo"] = value;
        }
    }

    #endregion

    #region METODOS

    private void _ChangeComboPais()
    {
        try
        {
            // Carrega o estado de acodo com o país selecionado
            if (!string.IsNullOrEmpty(UCComboPais._Combo.SelectedValue) && UCComboPais._Combo.SelectedValue != new Guid().ToString())
            {
                Guid pai_id = new Guid();

                if (!string.IsNullOrEmpty(UCComboPais._Combo.SelectedValue))
                    pai_id = new Guid(UCComboPais._Combo.SelectedValue);

                UCComboUnidadeFederativa._Combo.Items.Clear();
                UCComboUnidadeFederativa._Load(pai_id, 0);
                UCComboUnidadeFederativa._Combo.Enabled = true;
                UCComboUnidadeFederativa.CancelarBinding = false;
                UCComboUnidadeFederativa._Combo.DataBind();

                if (UCComboUnidadeFederativa._Combo.Items.FindByValue(new Guid().ToString()) == null)
                {
                    UCComboUnidadeFederativa._Combo.Items.Insert(0, new ListItem("-- Selecione uma opção --", new Guid().ToString()));
                }
            }
            else
            {
                UCComboUnidadeFederativa._Combo.Enabled = false;
                UCComboUnidadeFederativa._Combo.SelectedValue = Guid.Empty.ToString();
            }

            // Estado só é obrigatório quando o país selecionado, é o país definido como padrão "Brasil"
            if (UCComboPais._Combo.SelectedValue == SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL))
            {
                UCComboUnidadeFederativa._Label.Text = "Estado *";
                UCComboUnidadeFederativa._EnableValidator = true;                
            }
            else
            {
                UCComboUnidadeFederativa._Label.Text = "Estado";
                UCComboUnidadeFederativa._EnableValidator = false;                
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void UCComboPais__IndexChanged(object sender, EventArgs e)
    {
        _ChangeComboPais();
    }

    private void _Limpar()
    {
        UCComboUnidadeFederativa._Combo.SelectedValue = Guid.Empty.ToString();
        UCComboPais._Combo.SelectedValue = Guid.Empty.ToString();
        UCComboUnidadeFederativa._Combo.Enabled = false;
        _txtCidade.Text = string.Empty;
        _txtDDD.Text = string.Empty;
    }

    private bool _Validar()
    {
        if ((UCComboPais._Combo.SelectedValue == Guid.Empty.ToString()) || (UCComboPais._Combo.SelectedValue == null))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("País é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }
        if (String.IsNullOrEmpty(_txtCidade.Text.Trim()))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Cidade é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }
        if (SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL) == UCComboPais._Combo.SelectedValue)
        {
            if ((UCComboUnidadeFederativa._Combo.SelectedValue == Guid.Empty.ToString()) || (UCComboUnidadeFederativa._Combo.SelectedValue == null))
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Estado é obrigatório para este país.", UtilBO.TipoMensagem.Alerta);
                return false;
            }
        }
        return true;
    }

    private void _Salvar()
    {
        try
        {
            END_Cidade entityCidade = new END_Cidade
            {
                cid_id = _VS_cid_id
                ,
                pai_id = new Guid(UCComboPais._Combo.SelectedValue)
                ,
                unf_id = new Guid(UCComboUnidadeFederativa._Combo.SelectedValue)
                ,
                cid_nome = _txtCidade.Text
                ,
                cid_ddd = string.IsNullOrEmpty(_txtDDD.Text) ? string.Empty : _txtDDD.Text
                ,
                cid_situacao = Convert.ToByte(1)
                ,
                IsNew = (_VS_cid_id != Guid.Empty) ? false : true
            };

            if (END_CidadeBO.Save(entityCidade, _VS_pai_idAntigo, _VS_unf_idAntigo, null))
            {
                if (_VS_cid_id == Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "cid_id: " + entityCidade.cid_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Cidade incluída com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "cid_id: " + entityCidade.cid_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Cidade alterada com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoCidade/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a cidade.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (CoreLibrary.Validation.Exceptions.ValidationException ex)
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a cidade.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _Carregar(Guid cid_id)
    {
        try
        {
            END_Cidade _EntidadeCidade = new END_Cidade { cid_id = cid_id };
            END_CidadeBO.GetEntity(_EntidadeCidade);
            
            _VS_cid_id = _EntidadeCidade.cid_id;
            
            UCComboPais._Combo.SelectedValue = _EntidadeCidade.pai_id.ToString();
            
            _ChangeComboPais();

            UCComboUnidadeFederativa._Combo.SelectedValue = _EntidadeCidade.unf_id != Guid.Empty ? _EntidadeCidade.unf_id.ToString() : Guid.Empty.ToString();

            _txtCidade.Text = _EntidadeCidade.cid_nome;
            _txtDDD.Text = _EntidadeCidade.cid_ddd;

            _VS_pai_idAntigo = _EntidadeCidade.pai_id;
            _VS_unf_idAntigo = _EntidadeCidade.unf_id;                    
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a cidade.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion

    #region EVENTOS

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        if (_Validar())
        {
            _Salvar();
        }
    }

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoCidade/Busca.aspx", false);
    }

    #endregion
}
