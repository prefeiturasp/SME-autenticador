using System;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Web.WebProject;

public partial class Busca_Cidade : MotherPageLogado
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        UtilBO.SetScriptBusca(Page, _btnNovo, new string[] { }, String.Concat(ApplicationWEB._DiretorioVirtual, "AreaAdm/Cidade/Cadastro.aspx?Pesquisar=", _btnPesquisar.ClientID), "Cadastro de cidade", 350, 250);
        UCComboPais1.OnSelectedIndexChange = UCComboPais1__IndexChanged;

        if (!IsPostBack)
        {
            UCComboPais1._ShowSelectMessage = true;
            UCComboPais1._Load(0);
            UCComboUnidadeFederativa1._ShowSelectMessage = true;
            UCComboUnidadeFederativa1._Load(Guid.Empty, 0);
                       
            fdsResultados.Visible = false;

            string parametro =  SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL);
            if (!string.IsNullOrEmpty(parametro))
            {
                UCComboPais1._Combo.SelectedValue = parametro;
                _ChangeComboPais();
                _VS_pais_padrao = parametro;
                UCComboUnidadeFederativa1._Load(new Guid(parametro), 1);
                UCComboUnidadeFederativa1._Combo.Enabled = true;
            }
            else
            {
                UCComboUnidadeFederativa1._Combo.Enabled = false;
            }

            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
            Page.Form.DefaultFocus = UCComboPais1._Combo.ClientID;            
        }        
    }

    #region PROPRIEDADES

    private String _VS_pais_padrao
    {
        get
        {
            if (ViewState["_VS_pais_padrao"] != null)
                return ViewState["_VS_pais_padrao"].ToString();
            return String.Empty;
        }
        set
        {
            ViewState["_VS_pais_padrao"] = value;
        }
    }

    #endregion

    #region DELEGATES

    public delegate void onSeleciona(int cid_id);
    public event onSeleciona _Selecionar;

    public void _SelecionarCidade(int cid_id)
    {
        if (_Selecionar != null)
            _Selecionar(cid_id);
    }

    #endregion

    #region METODOS

    private void _ChangeComboPais()
    {
        try
        {
            if (UCComboPais1._Combo.SelectedValue == _VS_pais_padrao)
                UCComboUnidadeFederativa1._Combo.Enabled = true;
            else
            {
                UCComboUnidadeFederativa1._Combo.Enabled = false;
                UCComboUnidadeFederativa1._Combo.SelectedValue = Guid.Empty.ToString();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
        }
    }

    void UCComboPais1__IndexChanged(object sender, EventArgs e)
    {
        _ChangeComboPais();
    }

    private void _Pesquisar()
    {
        try
        {
            _grvCidade.PageIndex = 0;

            odsCidade.SelectParameters.Clear();
            odsCidade.SelectParameters.Add("cid_id", string.Empty);
            odsCidade.SelectParameters.Add("pai_id", UCComboPais1._Combo.SelectedValue);
            odsCidade.SelectParameters.Add("unf_id", UCComboUnidadeFederativa1._Combo.SelectedValue);
            odsCidade.SelectParameters.Add("cid_nome", _txtCidade.Text);
            odsCidade.SelectParameters.Add("unf_nome", string.Empty);
            odsCidade.SelectParameters.Add("unf_sigla", string.Empty);
            odsCidade.SelectParameters.Add("pai_nome", string.Empty);
            odsCidade.SelectParameters.Add("cid_situacao", "0");
            odsCidade.SelectParameters.Add("paginado", "true");
            odsCidade.DataBind();
            _grvCidade.DataBind();

            _btnNovo.Visible = _grvCidade.Rows.Count == 0;                            

            _lblMessage.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar as cidades.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion

    protected void odsCidade_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        _Pesquisar();
        fdsResultados.Visible = true;
    }

    protected void _grvCidade_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Retorna o valor selecionado pelo usuário
        UtilBO.SetScriptRetornoBusca(Page, Request["buscaID"], new[] { _grvCidade.DataKeys[e.NewEditIndex].Values[0].ToString(), _grvCidade.DataKeys[e.NewEditIndex].Values[1].ToString() }); 
        e.Cancel = true;
    }
}
