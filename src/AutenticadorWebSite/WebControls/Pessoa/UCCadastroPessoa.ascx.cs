using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Web.WebProject;
using System.Web.UI.HtmlControls;

public partial class WebControls_Pessoa_UCCadastroPessoa : MotherUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this.Page);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.CamposData));
        }

        if (!IsPostBack)
        {            
            UCComboPais1._Label.Text = "Nacionalidade";
            UCComboPais1._EnableValidator = false;
            UCComboPais1._ShowSelectMessage = true;
            UCComboPais1._Load(0);

            UCComboTipoEscolaridade1._Label.Text = "Escolaridade";
            UCComboTipoEscolaridade1._EnableValidator = false;
            UCComboTipoEscolaridade1._ShowSelectMessage= true;
            UCComboTipoEscolaridade1._Load(0);

        
            UCComboTipoDeficiencia1._EnableValidator = false;
            UCComboTipoDeficiencia1._ShowSelectMessage = true;
            UCComboTipoDeficiencia1._Load(Guid.Empty,0);

            UCComboEstadoCivil1.Inicialize("Estado civil");
            UCComboEstadoCivil1._EnableValidator = false;

            UCComboSexo1.Inicialize("Sexo");
            UCComboSexo1._EnableValidator = false;
            UCComboSexo1._ValidationGroup = "Pessoa";
        }
    }

    #region DELEGATES

    public delegate void onSeleciona();
    public event onSeleciona _Selecionar;

    public void _SelecionarPessoa()
    {
        if (_Selecionar != null)
            _Selecionar();
    }

    #endregion

    #region PROPRIEDADES

    /// <summary>
    /// Retorna e atribui o ViewState com o id da naturalidade
    /// </summary>
    public Guid _VS_cid_id
    {
        get
        {
            if (!string.IsNullOrEmpty(_txtCid_id.Value))
                return new Guid(_txtCid_id.Value);
            if (ViewState["_VS_cid_id"] != null)
                return new Guid(ViewState["_VS_cid_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_cid_id"] = value;
        }
    }

    /// <summary>
    /// Retorna e atribui o ViewState com o id do pai
    /// </summary>
    public Guid _VS_pes_idFiliacaoPai
    {
        get
        {
            if (!string.IsNullOrEmpty(_txtPes_idFiliacaoPai.Value))
                return new Guid(_txtPes_idFiliacaoPai.Value);
            if (ViewState["_VS_pes_idFiliacaoPai"] != null)
                return new Guid(ViewState["_VS_pes_idFiliacaoPai"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_pes_idFiliacaoPai"] = value;
        }
    }

    /// <summary>
    /// Retorna e atribui o ViewState com o id da mae
    /// </summary>
    public Guid _VS_pes_idFiliacaoMae
    {
        get
        {
            if (!string.IsNullOrEmpty(_txtPes_idFiliacaoMae.Value))
                return new Guid(_txtPes_idFiliacaoMae.Value);
            if (ViewState["_VS_pes_idFiliacaoMae"] != null)
                return new Guid(ViewState["_VS_pes_idFiliacaoMae"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_pes_idFiliacaoMae"] = value;
        }
    }

    /// <summary>
    /// Retorna e atribui o ViewState com o id da pessoa
    /// </summary>
    public Guid _VS_pes_id
    {
        get
        {
            if (ViewState["_VS_pes_id"] != null)
                return new Guid(ViewState["_VS_pes_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_pes_id"] = value;
        }
    }

    /// <summary>
    /// Retorna e atribui o ViewState com o tipo de busca de pessoa
    /// 1 - pai
    /// 2 - mãe
    /// </summary>
    public byte _VS_tipoBuscaPessoa
    {
        get
        {
            if (ViewState["_VS_tipoBuscaPessoa"] != null)
                return Convert.ToByte((ViewState["_VS_tipoBuscaPessoa"].ToString()));
            return 0;
        }
        set
        {
            ViewState["_VS_tipoBuscaPessoa"] = value;
        }
    }

    /// <summary>
    /// Retorno e atribui valores para o textbox Nome
    /// </summary>
    public TextBox _txtNome
    {
        get
        {
            return txtNome;
        }
        set
        {
            txtNome = value;
        }
    }

    /// <summary>
    /// Retorno e atribui valores para o textbox Nome Abreviado
    /// </summary>
    public TextBox _txtNomeAbreviado
    {
        get
        {
            return txtNomeAbreviado;
        }
        set
        {
            txtNomeAbreviado = value;
        }
    }

    /// <summary>
    /// Retorno e atribui valores para o textbox Nome Social
    /// </summary>
    public TextBox _txtNomeSocial
    {
        get
        {
            return txtNomeSocial;
        }
        set
        {
            txtNomeSocial = value;
        }
    }

    /// <summary>
    /// Atribui valores para o combo Nacionalidade
    /// </summary>
    public DropDownList _ComboNacionalidade
    {
        get
        {
            return UCComboPais1._Combo;
        }
        set
        {
            UCComboPais1._Combo = value;
        }
    }

    /// <summary>
    /// Retorno e atribui valores para o checkbox Naturalizado
    /// </summary>
    public CheckBox _chkNaturalizado
    {
        get
        {
            return chkNaturalizado;
        }
        set
        {
            chkNaturalizado = value;
        }
    }

    /// <summary>
    /// Retorno e atribui valores para o textbox Naturalidade
    /// </summary>
    public TextBox _txtNaturalidade
    {
        get
        {
            return txtNaturalidade;
        }
        set
        {
            txtNaturalidade = value;
        }
    }

    /// <summary>
    /// Retorno e atribui valores para o textbox Data Nascimento
    /// </summary>
    public TextBox _txtDataNasc
    {
        get
        {
            return txtDataNasc;
        }
        set
        {
            txtDataNasc = value;
        }
    }

    /// <summary>
    /// Retorno e atribui valores para o textbox Pai
    /// </summary>
    public TextBox _txtPai
    {
        get
        {
            return txtPai;
        }
        set
        {
            txtPai = value;
        }
    }

    /// <summary>
    /// Retorno e atribui valores para o textbox Mae
    /// </summary>
    public TextBox _txtMae
    {
        get
        {
            return txtMae;
        }
        set
        {
            txtMae = value;
        }
    }

    /// <summary>
    /// Atribui valores para o combo Estado Civil
    /// </summary>
    public DropDownList _ComboEstadoCivil
    {
        get
        {
            return UCComboEstadoCivil1._Combo;
        }
        set
        {
            UCComboEstadoCivil1._Combo = value;
        }
    }

    /// <summary>
    /// Atribui valores para o combo Raça Cor
    /// </summary>
    public DropDownList _ComboRacaCor
    {
        get
        {
            return UCComboRacaCor1._Combo;
        }
        set
        {
            UCComboRacaCor1._Combo = value;
        }
    }

    /// <summary>
    /// Atribui valores para o combo Sexo
    /// </summary>
    public DropDownList _ComboSexo
    {
        get
        {
            return UCComboSexo1._Combo;
        }
        set
        {
            UCComboSexo1._Combo = value;
        }
    }

    /// <summary>
    /// Atribui valores para o combo Escolaridade
    /// </summary>
    public DropDownList _ComboEscolaridade
    {
        get
        {
            return UCComboTipoEscolaridade1._Combo;
        }
        set
        {
            UCComboTipoEscolaridade1._Combo = value;
        }
    }

    /// <summary>
    /// Atribui valores para o combo Tipo Deficiencia
    /// </summary>
    public DropDownList _ComboTipoDeficiencia
    {
        get
        {
            return UCComboTipoDeficiencia1._Combo;
        }
        set
        {
            UCComboTipoDeficiencia1._Combo = value;
        }
    }






    /// <summary>
    /// Retorno e atribui valores para o UpdatePanel
    /// </summary>
    public UpdatePanel _updCadastroPessoas
    {
        get
        {
            return _updCadastroPessoa;
        }
        set
        {
            _updCadastroPessoa = value;
        }
    }

    public HtmlInputFile _iptFoto
    {
        get
        {
            return iptFoto;
        }
        set
        {
            iptFoto = value;
        }
    }

    public Image _imgFoto
    {
        get
        {
            return imgFoto;
        }
        set
        {
            imgFoto = value;
        }
    }

    public CheckBox _chbExcluirImagem
    {
        get
        {
            return chbExcluirImagem;
        }
        set
        {
            chbExcluirImagem = value;
        }
    }

    #endregion

    #region EVENTOS

    protected void _btnPai_Click(object sender, ImageClickEventArgs e)
    {
        _VS_tipoBuscaPessoa = 1;
        _SelecionarPessoa();
    }

    protected void _btnMae_Click(object sender, ImageClickEventArgs e)
    {
        _VS_tipoBuscaPessoa = 2;
        _SelecionarPessoa();
    }

    #endregion
}
