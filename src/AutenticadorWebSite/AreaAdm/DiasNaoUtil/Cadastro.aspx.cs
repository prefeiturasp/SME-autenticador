using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Autenticador.Web.WebProject;
using Autenticador.BLL;
using Autenticador.Entities;

public partial class AreaAdm_DiasNaoUtil_Cadastro : MotherPageLogado
{
    #region Propriedades

    private Guid _VS_dnu_id
    {
        get
        {
            if (ViewState["_VS_dnu_id"] != null)
                return new Guid(ViewState["_VS_dnu_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_dnu_id"] = value;
        }
    }

    private bool _AlteraTela_DiaNaoUtilRecorrente
    {
        set
        {
            this._txtData.Visible = !value;
            this._lblFormatoData.Visible = !value;
            this._rfvData.Visible = !value;
            this.cvData.Visible = !value;

            this._txtDataDia.Visible = value;
            this._rfvDataDia.Visible = value;
            this._revDataDia.Visible = value;
            this._lblBarra.Visible = value;
            this._txtDataMes.Visible = value;
            this._lblFormatoData2.Visible = value;
            this._rfvDataMes.Visible = value;
            this._revDataMes.Visible = value;

            this._lblVigenciaIni.Visible = value;
            this._txtVigenciaIni.Visible = value;
            this._rfvVigenciaIni.Visible = value;
            this.cvDataVigIni.Visible = value;
            this._lblVigenciaFim.Visible = value;
            this._txtVigenciaFim.Visible = value;
            this.cvDataVigFim.Visible = value;

            if (value)
            {
                this._txtDataDia.Focus();
                this._txtVigenciaIni.Text = DateTime.Now.Date.AddDays(1).ToString();
            }
            else
            {
                this._txtData.Focus();
                this._txtVigenciaIni.Text = String.Empty;
            }
        }
    }

    private int _AlteraTela_Abrangencia
    {
        set
        {
            try
            {
                if (value == 1 || value == -1)
                {
                    this._UCComboUnidadeFederativa._Combo.SelectedValue = Guid.Empty.ToString();
                    this._UCComboCidade._Combo.SelectedValue = Guid.Empty.ToString();

                    this._UCComboUnidadeFederativa._Combo.Enabled = false;
                    this._UCComboCidade._Combo.Enabled = false;
                    this._UCComboUnidadeFederativa._EnableValidator = false;
                    this._UCComboCidade._EnableValidator = false;
                }
                else if (value == 2)
                {
                    this._UCComboCidade._Combo.SelectedValue = Guid.Empty.ToString();
                    this._UCComboUnidadeFederativa._Combo.SelectedValue = Guid.Empty.ToString();
                    this._UCComboUnidadeFederativa._Combo.Enabled = true;
                    this._UCComboUnidadeFederativa._EnableValidator = true;
                    this._UCComboUnidadeFederativa._Validator.ErrorMessage = "Estado é obrigatório.";
                    this._UCComboUnidadeFederativa._Validator.ValueToCompare = Guid.Empty.ToString();
                    this._UCComboUnidadeFederativa._Validator.Operator = ValidationCompareOperator.NotEqual;
                    this._UCComboUnidadeFederativa._Validator.ControlToValidate = this._UCComboUnidadeFederativa._Combo.ID;

                    this._UCComboCidade._Combo.Enabled = false;
                    this._UCComboCidade._EnableValidator = false;
                }
                else if (value == 3)
                {

                    this._UCComboUnidadeFederativa._Combo.SelectedValue = Guid.Empty.ToString();
                    this._UCComboCidade._Combo.SelectedValue = Guid.Empty.ToString();

                    this._UCComboUnidadeFederativa._Combo.Enabled = true;
                    this._UCComboCidade._Combo.Enabled = false;

                    this._UCComboUnidadeFederativa._EnableValidator = true;
                    this._UCComboUnidadeFederativa._Validator.ErrorMessage = "Estado é obrigatório.";
                    this._UCComboUnidadeFederativa._Validator.ValueToCompare = Guid.Empty.ToString();
                    this._UCComboUnidadeFederativa._Validator.Operator = ValidationCompareOperator.NotEqual;
                    this._UCComboUnidadeFederativa._Validator.ControlToValidate = this._UCComboUnidadeFederativa._Combo.ID;

                    this._UCComboCidade._EnableValidator = true;
                    this._UCComboCidade._Validator.ErrorMessage = "Cidade é obrigatório.";
                    this._UCComboCidade._Validator.ValueToCompare = Guid.Empty.ToString();
                    this._UCComboCidade._Validator.Operator = ValidationCompareOperator.NotEqual;
                    this._UCComboCidade._Validator.ControlToValidate = this._UCComboUnidadeFederativa._Combo.ID;

                }
                this._uppAbrangencia.Update();
                this.UpdatePanel1.Update();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Se verdadeiro deve acionar a validação da VigenciaInicio na BO.
    /// </summary>
    private bool _VerificaVigenciaInicio
    {
        get;
        set;
    }

    #endregion

    #region Métodos

    private void _Salvar()
    {
        try
        {
            if (!this._chkRecorrenciaAnual.Checked)
            {
                this._txtVigenciaIni.Text = this._txtData.Text;
                this._txtVigenciaFim.Text = this._txtData.Text;
            }
            SYS_DiaNaoUtil _DiaNaoUtil = new SYS_DiaNaoUtil()
            {
                dnu_id = this._VS_dnu_id
                ,
                dnu_nome = this._txtNome.Text
                ,
                dnu_abrangencia = Convert.ToByte(this._ddlAbrangencia.SelectedValue)
                ,
                dnu_descricao = this._txtDescricao.Text
                ,
                dnu_data = (this._chkRecorrenciaAnual.Checked) ? Convert.ToDateTime(this._txtDataDia.Text + "/" + this._txtDataMes.Text + "/2000") : Convert.ToDateTime(this._txtData.Text)
                ,
                dnu_recorrencia = (this._chkRecorrenciaAnual.Checked) ? true : false
                ,
                dnu_vigenciaInicio = Convert.ToDateTime(this._txtVigenciaIni.Text.Trim())
                ,
                dnu_vigenciaFim = string.IsNullOrEmpty(this._txtVigenciaFim.Text.Trim()) ? new DateTime() : Convert.ToDateTime(this._txtVigenciaFim.Text.Trim())
                ,
                cid_id = String.IsNullOrEmpty(this._UCComboCidade._Combo.SelectedValue) ? Guid.Empty : new Guid(this._UCComboCidade._Combo.SelectedValue)
                ,
                unf_id = String.IsNullOrEmpty(this._UCComboUnidadeFederativa._Combo.SelectedValue) ? Guid.Empty : new Guid(this._UCComboUnidadeFederativa._Combo.SelectedValue)
                ,
                dnu_situacao = 1
                ,
                IsNew = (this._VS_dnu_id != Guid.Empty) ? false : true
            };
            if (SYS_DiaNaoUtilBO.Save(_DiaNaoUtil,_VerificaVigenciaInicio))
            {
                if (this._VS_dnu_id != Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "dnu_id:" + _DiaNaoUtil.dnu_id);
                    this.__SessionWEB.PostMessages = UtilBO.GetErroMessage("Dia não útil alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "dnu_id:" + _DiaNaoUtil.dnu_id);
                    this.__SessionWEB.PostMessages = UtilBO.GetErroMessage("Dia não útil incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                }
                Response.Redirect(this.__SessionWEB._AreaAtual._Diretorio + "DiasNaoUtil/Busca.aspx", false);
            }
            else
            {
                this._lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o dia não útil.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (ArgumentException e)
        {            
            this._lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            this._lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o dia não útil.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _Carregar(Guid dnu_id)
    {
        try
        {
            SYS_DiaNaoUtil _DiaNaoUtil = new SYS_DiaNaoUtil() { dnu_id = dnu_id };
            SYS_DiaNaoUtilBO.GetEntity(_DiaNaoUtil);
            
            this._VS_dnu_id = _DiaNaoUtil.dnu_id;
            this._txtNome.Text = _DiaNaoUtil.dnu_nome;
            if (_DiaNaoUtil.dnu_recorrencia == true)
            {
                this.Page.Form.DefaultFocus = this._txtVigenciaFim.ClientID;
                this._AlteraTela_DiaNaoUtilRecorrente = true;
                this._chkRecorrenciaAnual.Checked = true;
                if (_DiaNaoUtil.dnu_data.Day > 0 && _DiaNaoUtil.dnu_data.Day < 10)
                    this._txtDataDia.Text = "0" + Convert.ToString(_DiaNaoUtil.dnu_data.Day);
                else
                    this._txtDataDia.Text = Convert.ToString(_DiaNaoUtil.dnu_data.Day);
                if (_DiaNaoUtil.dnu_data.Month > 0 && _DiaNaoUtil.dnu_data.Month < 10)
                    this._txtDataMes.Text = "0" + Convert.ToString(_DiaNaoUtil.dnu_data.Month);
                else
                    this._txtDataMes.Text = Convert.ToString(_DiaNaoUtil.dnu_data.Month);

            }
            else
            {
                this.Page.Form.DefaultFocus = this._txtDescricao.ClientID;
                this._AlteraTela_DiaNaoUtilRecorrente = false;
                this._chkRecorrenciaAnual.Checked = false;
                this._txtData.Text = _DiaNaoUtil.dnu_data.ToString("dd/MM/yyyy");
            }
            this._ddlAbrangencia.SelectedValue = Convert.ToString(_DiaNaoUtil.dnu_abrangencia);
            this._AlteraTela_Abrangencia = _DiaNaoUtil.dnu_abrangencia;

            if (_DiaNaoUtil.unf_id != Guid.Empty)
                this._UCComboUnidadeFederativa._Combo.SelectedValue = Convert.ToString(_DiaNaoUtil.unf_id);
            if (_DiaNaoUtil.cid_id != Guid.Empty)
            {
                this._UCComboCidade._CarregaPorEstado(_DiaNaoUtil.unf_id);
                this._UCComboCidade._Combo.SelectedValue = Convert.ToString(_DiaNaoUtil.cid_id);
            }
            this._txtVigenciaIni.Text = _DiaNaoUtil.dnu_vigenciaInicio.ToString("dd/MM/yyyy");
            this._txtVigenciaFim.Text = (_DiaNaoUtil.dnu_vigenciaFim == new DateTime()) ? null : _DiaNaoUtil.dnu_vigenciaFim.ToString("dd/MM/yyyy");
            this._txtDescricao.Text = _DiaNaoUtil.dnu_descricao;

            this._txtNome.Enabled = false;
            this._chkRecorrenciaAnual.Enabled = false;
            this._txtData.Enabled = false;
            this._txtDataDia.Enabled = false;
            this._txtDataMes.Enabled = false;
            this._ddlAbrangencia.Enabled = false;
            this._UCComboUnidadeFederativa._Combo.Enabled = false;
            this._UCComboCidade._Combo.Enabled = false;
            this._txtVigenciaIni.Enabled = !(_DiaNaoUtil.dnu_vigenciaInicio <= DateTime.Now);
            this._VerificaVigenciaInicio = !(_DiaNaoUtil.dnu_vigenciaInicio <= DateTime.Now);
            
            this.UpdatePanel1.Update();
        }
        catch(Exception e)
        {
            ApplicationWEB._GravaErro(e);
            this._lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a dia não útil.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.CamposData));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
        }

        if (!IsPostBack)
        {
            try
            {
                this._UCComboUnidadeFederativa._ShowSelectMessage = true;
                this._UCComboUnidadeFederativa._Load(Guid.Empty, 0);
                this._UCComboUnidadeFederativa._Combo.Enabled = false;
                this._UCComboUnidadeFederativa._EnableValidator = false;

                this._UCComboCidade.Inicialize("Cidade");
                this._UCComboCidade._Combo.Enabled = false;
                this._UCComboCidade._EnableValidator = false;
                this._UCComboCidade._ShowSelectMessage = true;
                this._UCComboCidade._CarregaPorEstado(Guid.Empty);

                if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                {
                    this._Carregar(PreviousPage.EditItem);
                }
                else
                {
                    this.Page.Form.DefaultFocus = this._txtNome.ClientID;
                    this._btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
                }

                this.Page.Form.DefaultButton = this._btnSalvar.UniqueID;
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                this._lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            }
        }
        this._UCComboUnidadeFederativa.OnSelectedIndexChange = _UCComboUnidadeFederativa__IndexChanged;
    }

    protected void _ddlAbrangencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._AlteraTela_Abrangencia = Convert.ToInt32(_ddlAbrangencia.SelectedValue);
    }
    
    private void _UCComboUnidadeFederativa__IndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (!this._UCComboUnidadeFederativa._Combo.SelectedValue.Equals(Guid.Empty.ToString()) && this._ddlAbrangencia.SelectedValue.Equals("3"))
            {
                this._UCComboCidade._CarregaPorEstado(new Guid(_UCComboUnidadeFederativa._Combo.SelectedValue));
                this._UCComboCidade._Combo.Enabled = true;
                UpdatePanel1.Update();
            }
            else
            {
                this._UCComboCidade._Combo.SelectedValue = Guid.Empty.ToString();
                this._UCComboCidade._Combo.Enabled = false;
                UpdatePanel1.Update();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            this._lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }
    }
    
    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(this.__SessionWEB._AreaAtual._Diretorio + "DiasNaoUtil/Busca.aspx",false);
    }
    
    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
            this._Salvar();
    }
    
    protected void _chkRecorrenciaAnual_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this._AlteraTela_DiaNaoUtilRecorrente = this._chkRecorrenciaAnual.Checked;
            UpdatePanel1.Update();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            this._lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }
    }
    
    #endregion
}
