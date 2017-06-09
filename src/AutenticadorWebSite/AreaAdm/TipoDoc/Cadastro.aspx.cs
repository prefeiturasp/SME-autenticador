using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class AreaAdm_TipoDoc_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
                _LoadFromEntity(PreviousPage.EditItem);
            else
            {
                CarregarAtributos();
                _ckbBloqueado.Visible = false;
                this._bntSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }

            Page.Form.DefaultFocus = _ddlClassificacao.ClientID;
            Page.Form.DefaultButton = _bntSalvar.UniqueID;
        }
    }

    #region PROPRIEDADES

    private Guid _VS_tdo_id
    {
        get
        {
            if (ViewState["_VS_tdo_id"] != null)
            {
                return new Guid(ViewState["_VS_tdo_id"].ToString());
            }
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_tdo_id"] = value;
        }
    }

    #endregion PROPRIEDADES

    #region METODOS

    private void _LoadFromEntity(Guid tdo_id)
    {
        try
        {
            string atributos = "";
            int posId = 0;

            SYS_TipoDocumentacao TipoDocumentacao = new SYS_TipoDocumentacao { tdo_id = tdo_id };
            SYS_TipoDocumentacaoBO.GetEntity(TipoDocumentacao);
            _VS_tdo_id = TipoDocumentacao.tdo_id;
            _ddlClassificacao.SelectedValue = TipoDocumentacao.tdo_classificacao.ToString();
            _txtDocumento.Text = TipoDocumentacao.tdo_nome;
            _txtSigla.Text = TipoDocumentacao.tdo_sigla;
            _ddlValidacao.SelectedValue = TipoDocumentacao.tdo_validacao.ToString();
            _ckbBloqueado.Checked = !TipoDocumentacao.tdo_situacao.Equals(1);
            _ckbBloqueado.Visible = true;

            if (_ddlClassificacao.SelectedValue.ToString() != "99")
                atributos = TipoDocumentacao.tdo_atributos;
            else
                atributos = SYS_TipoDocumentacaoAtributoBO.SelecionarStringAtributosDefault();

            CarregarAtributos();

            if (!string.IsNullOrEmpty(atributos))
            {
                // Verifica quais foram os atributos selecionados
                foreach (ListItem item in _ckbAtributos.Items)
                {
                    posId = Convert.ToInt16(item.Value);

                    if (posId <= atributos.Length)
                        if (atributos.Substring(posId - 1, 1) == "1")
                            item.Selected = true;
                }
            }

            // Habilita/Desabilita os checkboxes se a classificação estiver default
            OnOffAtributosDefault(_ddlClassificacao.SelectedValue.ToString() == "99" ? false : true);
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o tipo de documentação.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void Salvar()
    {
        try
        {
            string atributos = "";
            Func<string, bool> VerifyStringNull = x => x.Contains('1');

            // Verifica quais foram os atributos selecionados
            foreach (ListItem item in _ckbAtributos.Items)
            {
                atributos += (item.Selected ? "1" : "0");
            }

            //DA ERRO CASO VALIDAÇÃO ESTEJA CHECKADO E NÚMERO NÃO
            if (Convert.ToByte(_ddlValidacao.SelectedValue) != 0 && !_ckbAtributos.Items.FindByText("Número").Selected)
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Para adicionar uma validação é necessário selecionar o atributo número.", UtilBO.TipoMensagem.Alerta);
            }
            else
            {
                if (VerifyStringNull(atributos))
                {
                    SYS_TipoDocumentacao TipoDoc = new SYS_TipoDocumentacao
                    {
                        tdo_id = _VS_tdo_id
                        ,
                        tdo_nome = _txtDocumento.Text
                        ,
                        tdo_sigla = _txtSigla.Text
                        ,
                        tdo_validacao = Convert.ToByte(_ddlValidacao.SelectedValue)
                        ,
                        tdo_situacao = (_ckbBloqueado.Checked ? Convert.ToByte(2) : Convert.ToByte(1))
                        ,
                        tdo_classificacao = Convert.ToByte(_ddlClassificacao.SelectedValue)
                        ,
                        tdo_atributos = atributos
                        ,
                        IsNew = (_VS_tdo_id != Guid.Empty) ? false : true
                    };

                    if (SYS_TipoDocumentacaoBO.Save(TipoDoc))
                    {
                        if (_VS_tdo_id != Guid.Empty)
                        {
                            ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "tdo_id: " + TipoDoc.tdo_id);
                            __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de documentação alterado com sucesso.", UtilBO.TipoMensagem.Sucesso);
                        }
                        else
                        {
                            ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "tdo_id: " + TipoDoc.tdo_id);
                            __SessionWEB.PostMessages = UtilBO.GetErroMessage("Tipo de documentação incluído com sucesso.", UtilBO.TipoMensagem.Sucesso);
                        }

                        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoDoc/Busca.aspx", false);
                    }
                    else
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de documentação.", UtilBO.TipoMensagem.Erro);
                    }
                }
                else
                {
                    _lblMessage.Text = UtilBO.GetErroMessage("Obrigatório pelo menos um atributo.", UtilBO.TipoMensagem.Alerta);
                }
            }
        }
        catch (CoreLibrary.Validation.Exceptions.ValidationException e)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (DuplicateNameException e)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (ArgumentException e)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(e.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception e)
        {
            ApplicationWEB._GravaErro(e);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar o tipo de documentação.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void CarregarAtributos(bool selecionarAtributosDefault = false)
    {
        // Carrega os atributos do tipo de documento, validando se é para checar os atributos que estão marcados como default
        DataTable dt = SYS_TipoDocumentacaoAtributoBO.SelecionarAtributos();
        _ckbAtributos.Items.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            _ckbAtributos.Items.Add(new ListItem(dt.Rows[i]["tda_descricao"].ToString(), dt.Rows[i]["tda_id"].ToString()));

            if (selecionarAtributosDefault)
                if ((bool)dt.Rows[i]["tda_default"])
                    _ckbAtributos.Items.FindByValue(dt.Rows[i]["tda_id"].ToString()).Selected = true;
        }
    }

    protected void OnOffAtributosDefault(bool habilitado)
    {
        foreach (ListItem item in _ckbAtributos.Items)
            item.Enabled = habilitado;

        //((CheckBox)e.Item.FindControl("chkGemeo")).Visible = false;
    }

    #endregion METODOS

    #region EVENTOS

    protected void odsAtributos_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = this;
    }

    protected void _odsAtributos_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int i = 0;

        //i = i + 1;
    }

    protected void _ddlClassificacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (_ddlClassificacao.SelectedValue.ToString() == "99")
                CarregarAtributos(true);

            OnOffAtributosDefault(_ddlClassificacao.SelectedValue.ToString() == "99" ? false : true);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar validar a classificação do tipo de documento.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void odsAtributos_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            ApplicationWEB._GravaErro(e.Exception);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar listar o(s) atributo(s) do(s) tipo(s) de documento(s).", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "TipoDoc/Busca.aspx", false);
    }

    protected void _bntSalvar_Click(object sender, EventArgs e)
    {
        Salvar();
    }

    #endregion EVENTOS
}