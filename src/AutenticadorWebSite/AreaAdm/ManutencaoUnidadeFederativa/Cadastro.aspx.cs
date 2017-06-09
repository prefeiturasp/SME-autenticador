using System;
using System.Web.UI;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Data;
using System.Web.UI.WebControls;

public partial class AreaAdm_ManutencaoUnidadeFederativa_Cadastro : MotherPageLogado
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
                UCComboPais._ValidationGroup = "vlgUnidadeFederativa";
                UCComboPais._Load(0);
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

    private Guid _VS_unf_id
    {
        get
        {
            if (ViewState["_VS_unf_id"] != null)
                return new Guid(ViewState["_VS_unf_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_unf_id"] = value;
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
        UCComboPais._Combo.SelectedValue = Guid.Empty.ToString();        
        _txtUnidadeFederativa.Text = string.Empty;
        _txtSigla.Text = string.Empty;
    }

    private bool _Validar()
    {
        if ((UCComboPais._Combo.SelectedValue == Guid.Empty.ToString()) || (UCComboPais._Combo.SelectedValue == null))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("País é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }
        if (String.IsNullOrEmpty(_txtUnidadeFederativa.Text.Trim()))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Unidade federativa é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }
        if (String.IsNullOrEmpty(_txtSigla.Text.Trim()))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Sigla é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }
        return true;
    }

    private void _Salvar()
    {
        try
        {
            END_UnidadeFederativa entityUnidadeFederativa = new END_UnidadeFederativa
            {
                unf_id = _VS_unf_id
                ,
                pai_id = new Guid(UCComboPais._Combo.SelectedValue)
                ,
                unf_nome = _txtUnidadeFederativa.Text
                ,
                unf_sigla = _txtSigla.Text
                ,
                unf_situacao = Convert.ToByte(1)
                ,
                IsNew = (_VS_unf_id != Guid.Empty) ? false : true
            };

            if (END_UnidadeFederativaBO.Save(entityUnidadeFederativa,_VS_pai_idAntigo, null))
            {
                if (_VS_unf_id == Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "unf_id: " + entityUnidadeFederativa.unf_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Unidade federativa incluída com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "unf_id: " + entityUnidadeFederativa.unf_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage("Unidade federativa alterada com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoUnidadeFederativa/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a unidade federativa.", UtilBO.TipoMensagem.Erro);
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a unidade federativa.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _Carregar(Guid unf_id)
    {
        try
        {
            END_UnidadeFederativa UnidadeFederativa = new END_UnidadeFederativa { unf_id = unf_id };
            END_UnidadeFederativaBO.GetEntity(UnidadeFederativa);

            _VS_unf_id = UnidadeFederativa.unf_id;

            UCComboPais._Combo.SelectedValue = UnidadeFederativa.pai_id.ToString();
            
            _ChangeComboPais();

            _txtUnidadeFederativa.Text = UnidadeFederativa.unf_nome;
            _txtSigla.Text = UnidadeFederativa.unf_sigla;

            _VS_pai_idAntigo = UnidadeFederativa.pai_id;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a unidade federativa.", UtilBO.TipoMensagem.Erro);
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
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoUnidadeFederativa/Busca.aspx", false);
    }

    #endregion
}
