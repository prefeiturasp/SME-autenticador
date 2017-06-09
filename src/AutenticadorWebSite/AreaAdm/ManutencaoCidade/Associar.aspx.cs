using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using System.Data;
using Autenticador.BLL;
using Autenticador.Entities;
using System.Xml;

public partial class AreaAdm_ManutencaoCidade_Associar : MotherPageLogado
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
            if (Session["ManutencaoCidade_dtAssociacaoCidades"] != null)
            {
                _VS_AssociacaoCidades = (DataTable)(Session["ManutencaoCidade_dtAssociacaoCidades"]);
                Session.Remove("ManutencaoCidade_dtAssociacaoCidades");
            }
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

                UCComboPais._Combo.Enabled = false;
                _txtDDD.Enabled = false;
                _txtCidade.Enabled = false;

                _Limpar();
                _lblMessage.Visible = false;

                _CarregarGridAssociacaoCidades();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o endereço.", UtilBO.TipoMensagem.Erro);
            }

            Page.Form.DefaultFocus = UCComboPais._Combo.ClientID;
            Page.Form.DefaultButton = _btnConfimarAssociacao.UniqueID;
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

    private int _VS_linha_marcada
    {
        get
        {
            if (ViewState["_VS_linha_marcada"] != null)
                return Convert.ToInt32(ViewState["_VS_linha_marcada"]);
            return -1;
        }
        set
        {
            ViewState["_VS_linha_marcada"] = value;
        }
    }

    private DataTable _VS_AssociacaoCidades
    {
        get
        {
            if (ViewState["_VS_AssociacaoCidades"] == null)
            {
                DataTable dt = new DataTable();
                ViewState["_VS_AssociacaoCidades"] = dt;
            }
            return (DataTable)ViewState["_VS_AssociacaoCidades"];
        }
        set
        {
            ViewState["_VS_AssociacaoCidades"] = value;
        }
    }
    
    #endregion

    #region METODOS

    private void _ChangeComboPais()
    {
        try
        {
            if (UCComboPais._Combo.SelectedValue == SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL))
            {
                UCComboUnidadeFederativa._Combo.Enabled = true;
                UCComboUnidadeFederativa._Combo.Enabled = true;
            }
            else
            {
                UCComboUnidadeFederativa._Combo.Enabled = false;
                UCComboUnidadeFederativa._Combo.Enabled = false;
                UCComboUnidadeFederativa._Combo.SelectedValue = Guid.Empty.ToString();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    void UCComboPais__IndexChanged(object sender, EventArgs e)
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

    private void _CarregarGridAssociacaoCidades()
    {
        _grvAssociacaoCidades.DataSource = _VS_AssociacaoCidades;
        _grvAssociacaoCidades.DataBind();
        _updCidades.Update();
    }

    private void _CarregarCidade(int index)
    {
        try
        {
            UCComboPais._Combo.SelectedValue = _grvAssociacaoCidades.DataKeys[index].Values[0].ToString();
            _ChangeComboPais();
            UCComboUnidadeFederativa._Combo.SelectedValue = _grvAssociacaoCidades.DataKeys[index].Values[1].ToString() == "0" ? "-1" : _grvAssociacaoCidades.DataKeys[index].Values[1].ToString();
            _VS_cid_id = new Guid(_grvAssociacaoCidades.DataKeys[index].Values[2].ToString());            
                        
            _txtCidade.Text = ((LinkButton)_grvAssociacaoCidades.Rows[index].FindControl("_btnCidade")).Text;
            _txtDDD.Text = ((Label)_grvAssociacaoCidades.Rows[index].FindControl("_lbcid_ddd")).Text;
            _btnConfimarAssociacao.Visible = true;

            _VS_pai_idAntigo = new Guid(UCComboPais._Combo.SelectedValue);
            _VS_unf_idAntigo = new Guid(UCComboUnidadeFederativa._Combo.SelectedValue);

            UCComboPais._Combo.Enabled = true;
            _txtCidade.Enabled = true;
            _txtDDD.Enabled = true;

            string pais_padrao = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL);

            UCComboUnidadeFederativa._Combo.Enabled = pais_padrao == _VS_pai_idAntigo.ToString();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a cidade.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _MarcaLinhaPadrao(int novaLinha)
    {
        if (_VS_linha_marcada >= 0)
        {
            _grvAssociacaoCidades.Rows[_VS_linha_marcada].Style.Remove("background");
            _grvAssociacaoCidades.Rows[novaLinha].Style.Add("background", "#F8F7CB");
            _VS_linha_marcada = novaLinha;
        }
        else
        {
            _grvAssociacaoCidades.Rows[novaLinha].Style.Add("background", "#F8F7CB");
            _VS_linha_marcada = novaLinha;
        }
    }

    private void _AssociarCidades()
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

            XmlDocument xDoc = new XmlDocument();
            XmlNode xElem = xDoc.CreateNode(XmlNodeType.Element, "Coluna", "");
            XmlNode xNodeCoor = xDoc.CreateNode(XmlNodeType.Element, "ColunaValorAntigo", "");
            XmlNode xNode;

            for (int i = 0; i < _VS_AssociacaoCidades.Rows.Count; i++)
            {
                if (new Guid(_VS_AssociacaoCidades.Rows[i]["cid_id"].ToString()) != _VS_cid_id)
                {
                    xNodeCoor = xDoc.CreateNode(XmlNodeType.Element, "ColunaValorAntigo", "");
                    xNode = xDoc.CreateNode(XmlNodeType.Element, "valor", "");
                    xNode.InnerText = _VS_AssociacaoCidades.Rows[i]["cid_id"].ToString();
                    xNodeCoor.AppendChild(xNode);
                    xElem.AppendChild(xNodeCoor);
                }
            }
            xDoc.AppendChild(xElem);

            if (END_CidadeBO.AssociarCidades(entityCidade, _VS_pai_idAntigo, _VS_unf_idAntigo, xDoc))
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "cid_id: " + entityCidade.cid_id);
                __SessionWEB.PostMessages = UtilBO.GetErroMessage("Cidades associadas com sucesso.", UtilBO.TipoMensagem.Sucesso);
                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoCidade/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar as cidades.", UtilBO.TipoMensagem.Erro);                
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar as cidades.", UtilBO.TipoMensagem.Erro);            
        }
        finally
        {
            _updCidades.Update();
        }
    }
    
    #endregion

    #region EVENTOS

    protected void _btnConfimarAssociacao_Click(object sender, EventArgs e)
    {
        _AssociarCidades();
    }

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoCidade/Busca.aspx", false);
    }

    protected void _grvAssociacaoCidades_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CarregarCidade")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            _MarcaLinhaPadrao(index);
            _CarregarCidade(index);
        }
    }

    protected void _grvAssociacaoCidades_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton _btnCidade = (LinkButton)e.Row.FindControl("_btnCidade");
            if (_btnCidade != null)
            {
                _btnCidade.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
  
    #endregion
}
