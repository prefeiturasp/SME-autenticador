using System;
using System.Web.UI;
using System.Data;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;

public partial class AreaAdm_Cidade_Cadastro : MotherPageLogado
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
                UCComboPais1.Inicialize("País *");
                UCComboPais1._EnableValidator = true;
                UCComboPais1._ValidationGroup = "vlgPais";
                UCComboPais1._Load(0);
                
                UCComboUnidadeFederativa1.Inicialize("Estado");
                UCComboUnidadeFederativa1._EnableValidator = true;
                UCComboUnidadeFederativa1._ValidationGroup = "vlgPais";
                UCComboUnidadeFederativa1._Load(Guid.Empty, 0);
                UCComboUnidadeFederativa1._Combo.Enabled = false;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro  );
            }

            _btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            _Limpar();
            _lblMessage.Visible = false;

            Page.Form.DefaultFocus = UCComboPais1._Combo.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;            
        }

        UCComboPais1.OnSelectedIndexChange = UCComboPais1__IndexChanged;
    }

    #region METODOS

    private void UCComboPais1__IndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (UCComboPais1._Combo.SelectedValue == SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL))
            {
                UCComboUnidadeFederativa1._Combo.Enabled = true;
                UCComboUnidadeFederativa1._EnableValidator = true;
            }
            else
            {
                UCComboUnidadeFederativa1._Combo.Enabled = false;
                UCComboUnidadeFederativa1._EnableValidator = false;
                UCComboUnidadeFederativa1._Combo.SelectedValue = Guid.Empty.ToString();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _Limpar()
    {
        UCComboUnidadeFederativa1._Combo.SelectedValue = Guid.Empty.ToString();
        UCComboPais1._Combo.SelectedValue = Guid.Empty.ToString();
        UCComboUnidadeFederativa1._Combo.Enabled = false;
        _txtCidade.Text = string.Empty;
        _txtDDD.Text = string.Empty;
    }

    private bool _Validar()
    {
        if ((UCComboPais1._Combo.SelectedValue == Guid.Empty.ToString()) || (UCComboPais1._Combo.SelectedValue == null))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("País é obrigatório.", UtilBO.TipoMensagem.Alerta);
            return false;
        }
        if (String.IsNullOrEmpty(_txtCidade.Text.Trim()))
        {
            _lblMessage.Text = UtilBO.GetErroMessage("Cidade é obrigatório.", UtilBO.TipoMensagem.Alerta );
            return false;
        }
        if (SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL) == UCComboPais1._Combo.SelectedValue)
        {
            if ((UCComboUnidadeFederativa1._Combo.SelectedValue == Guid.Empty.ToString()) || (UCComboUnidadeFederativa1._Combo.SelectedValue == null))
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Estado é obrigatório para este país.", UtilBO.TipoMensagem.Alerta );
                return false;
            }
        }
        _lblMessage.Visible = false;
        return true;
    }

    private bool _Salvar()
    {
        try
        {
            END_Cidade entityCidade = new END_Cidade
            {
                pai_id = new Guid(UCComboPais1._Combo.SelectedValue)
                ,
                unf_id = (new Guid(UCComboUnidadeFederativa1._Combo.SelectedValue))
                , cid_nome = _txtCidade.Text
                , cid_ddd = string.IsNullOrEmpty(_txtDDD.Text) ? string.Empty : _txtDDD.Text
                , cid_situacao = Convert.ToByte(1)
            };

            if (END_CidadeBO.Save(entityCidade, Guid.Empty, Guid.Empty, null))
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "cid_id: " + entityCidade.cid_id);
                _lblMessage.Text = UtilBO.GetErroMessage("Cidade incluída com sucesso.", UtilBO.TipoMensagem.Sucesso );
                return true;
            }

            return false;
        }
        catch (CoreLibrary.Validation.Exceptions.ValidationException ex)
        {            
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta );
            return false;
        }
        catch (ArgumentException ex)
        {            
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
            return false;
        }
        catch (DuplicateNameException ex)
        {            
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta );
            return false;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage( "Erro ao tentar incluir a cidade.", UtilBO.TipoMensagem.Erro );
            return false;
        }
    }

    protected void _FecharCadastro()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script type=\"text/javascript\">");
        sb.Append("\r\n");
        sb.Append("if (parent != undefined) {");
        sb.Append("\r\n");
        sb.Append(String.Format("\tparent.fecharBusca('#ifrm{0}');", Request["buscaID"]));
        sb.Append("\r\n");
        sb.Append("}");
        sb.Append("\r\n");
        sb.Append("</script>");
        var scriptManager = ScriptManager.GetCurrent(Page);
        if (scriptManager != null && scriptManager.IsInAsyncPostBack)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), Request["buscaID"], sb.ToString(), false);
        }
        else
        {
            if (!ClientScript.IsClientScriptBlockRegistered(Request["buscaID"]))
                ClientScript.RegisterClientScriptBlock(typeof(Page), Request["buscaID"], sb.ToString());
        }
    }

    #endregion

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        if (_Validar())
        {
            if (_Salvar())
            {
                _Limpar();
            }
        }

        _lblMessage.Visible = true;
    }

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        _FecharCadastro();
    }
}
