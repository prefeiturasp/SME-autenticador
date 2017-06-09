using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;

public partial class AreaAdm_ManutencaoEndereco_Associar : MotherPageLogado
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
            if (Session["ManutencaoEndereco_dtAssociarEndereco"] != null)
            {
                _VS_AssociarEnderecos = (DataTable)(Session["ManutencaoEndereco_dtAssociarEndereco"]);
                Session.Remove("ManutencaoEndereco_dtAssociarEndereco");

                _CarregarGridEndereco();
                Page.Form.DefaultFocus = txtLogradouro.ClientID;
            }

            txtCEP.Enabled = false;
            LabelNumero.Visible = false;
            txtNumero.Visible = false;
            rfvNumero.Visible = false;
            LabelComplemento.Visible = false;
            txtComplemento.Visible = false;

            txtLogradouro.Enabled = false;
            txtDistrito.Enabled = false;
            UCComboZona1._Combo.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtLogradouro.CssClass = "text60C";

            _btnSalvar.Visible = false;
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

    public DataTable _VS_AssociarEnderecos
    {
        get
        {
            if (ViewState["_VS_AssociarEnderecos"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("end_id");
                dt.Columns.Add("end_cep");
                dt.Columns.Add("end_logradouro");
                dt.Columns.Add("end_bairro");
                dt.Columns.Add("cid_id");
                dt.Columns.Add("cid_nome");
                dt.Columns.Add("cidadeuf");
                dt.Columns.Add("data");
                ViewState["_VS_AssociarEnderecos"] = dt;
            }
            return (DataTable)ViewState["_VS_AssociarEnderecos"];
        }
        set
        {
            ViewState["_VS_AssociarEnderecos"] = value;
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

    #endregion PROPRIEDADES

    #region METODOS

    public void UCCidade1BuscaCidade(IDictionary<string, object> parameters)
    {
        _VS_cid_id = new Guid(parameters["cid_id"].ToString());
        txtCidade.Text = parameters["cid_nome"].ToString();
        _updEnderecos.Update();
    }

    private void _CarregarGridEndereco()
    {
        _grvAssociarEnderecos.DataSource = _VS_AssociarEnderecos;
        _grvAssociarEnderecos.DataBind();
        _updEnderecos.Update();
    }

    private void _CarregarEndereco(int index)
    {
        try
        {
            _VS_end_id = new Guid(_grvAssociarEnderecos.DataKeys[index].Values[0].ToString());
            _VS_cid_id = new Guid(_grvAssociarEnderecos.DataKeys[index].Values[1].ToString());
            UCComboZona1._Combo.SelectedValue = (_grvAssociarEnderecos.DataKeys[index].Values[2] == DBNull.Value || _grvAssociarEnderecos.DataKeys[index].Values[2].ToString() == "0") ? "-1" : _grvAssociarEnderecos.DataKeys[index].Values[2].ToString();

            txtCEP.Text = ((LinkButton)_grvAssociarEnderecos.Rows[index].FindControl("_btnSelecionar")).Text;
            txtLogradouro.Text = ((Label)_grvAssociarEnderecos.Rows[index].FindControl("_lblLogradouro")).Text;
            txtDistrito.Text = ((Label)_grvAssociarEnderecos.Rows[index].FindControl("_lblDistrito")).Text;
            txtBairro.Text = ((Label)_grvAssociarEnderecos.Rows[index].FindControl("_lblBairro")).Text;
            txtCidade.Text = ((Label)_grvAssociarEnderecos.Rows[index].FindControl("_lblCidade")).Text.Substring(0, ((Label)_grvAssociarEnderecos.Rows[index].FindControl("_lblCidade")).Text.Length - 5);
            _btnSalvar.Visible = true;

            _VS_cid_idAntigo = _VS_cid_id;

            txtLogradouro.Enabled = true;
            txtDistrito.Enabled = true;
            UCComboZona1._Combo.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void _MarcaLinhaPadrao(int novaLinha)
    {
        if (_VS_linha_marcada >= 0)
        {
            _grvAssociarEnderecos.Rows[_VS_linha_marcada].Style.Remove("background");
            _grvAssociarEnderecos.Rows[novaLinha].Style.Add("background", "#F8F7CB");
            _VS_linha_marcada = novaLinha;
        }
        else
        {
            _grvAssociarEnderecos.Rows[novaLinha].Style.Add("background", "#F8F7CB");
            _VS_linha_marcada = novaLinha;
        }
    }

    private void _Associar()
    {
        try
        {
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
                cid_id = _VS_cid_id
                ,
                end_situacao = 1
                ,
                IsNew = (_VS_end_id != Guid.Empty) ? false : true
            };

            XmlDocument xDoc = new XmlDocument();
            XmlNode xElem = xDoc.CreateNode(XmlNodeType.Element, "Coluna", "");
            XmlNode xNodeCoor = xDoc.CreateNode(XmlNodeType.Element, "ColunaValorAntigo", "");
            XmlNode xNode;

            for (int i = 0; i < _VS_AssociarEnderecos.Rows.Count; i++)
            {
                if (_VS_AssociarEnderecos.Rows[i]["end_id"].ToString() != _VS_end_id.ToString())
                {
                    xNodeCoor = xDoc.CreateNode(XmlNodeType.Element, "ColunaValorAntigo", "");
                    xNode = xDoc.CreateNode(XmlNodeType.Element, "valor", "");
                    xNode.InnerText = _VS_AssociarEnderecos.Rows[i]["end_id"].ToString();
                    xNodeCoor.AppendChild(xNode);
                    xElem.AppendChild(xNodeCoor);
                }
            }
            xDoc.AppendChild(xElem);

            if (END_EnderecoBO.AssociarEnderecos(entityEndereco, _VS_cid_idAntigo, xDoc))
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "end_id: " + entityEndereco.end_id);
                __SessionWEB.PostMessages = UtilBO.GetErroMessage("Endereços associados com sucesso.", UtilBO.TipoMensagem.Sucesso);

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoEndereco/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar os endereços.", UtilBO.TipoMensagem.Erro);
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar os endereços.", UtilBO.TipoMensagem.Erro);
        }
        finally
        {
            _updEnderecos.Update();
        }
    }

    #endregion METODOS

    protected void _grvAssociarEnderecos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Selecionar")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            _MarcaLinhaPadrao(index);
            _CarregarEndereco(index);
        }
    }

    protected void _grvAssociarEnderecos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnSelecionar = (LinkButton)e.Row.FindControl("_btnSelecionar");
            if (btnSelecionar != null)
            {
                btnSelecionar.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Associar();
    }

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoEndereco/Busca.aspx", false);
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
}