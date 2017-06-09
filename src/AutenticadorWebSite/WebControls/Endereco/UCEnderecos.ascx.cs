using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.UserControlLibrary.Combos;
using Autenticador.Web.WebProject;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class WebControls_Endereco_UCEnderecos : MotherUserControl
{
    #region Propriedades

    /// <summary>
    /// Retorna os endereços cadastrados.
    /// </summary>
    public DataTable _VS_enderecos
    {
        get
        {
            return RetornaTabelaCadastro();
        }
    }

    [Serializable()]
    public class UnidadeAdmEndereco
    {
        public Guid cpr_id { get; set; }
        public Guid id { get; set; }
        public Guid end_id { get; set; }
        public string end_cep { get; set; }
        public string end_logradouro { get; set; }
        public string end_distrito { get; set; }
        public string end_zona { get; set; }
        public string end_bairro { get; set; }
        public Guid cid_id { get; set; }
        public string cid_nome { get; set; }
        public Guid uae_id { get; set; }
        public string uae_numero { get; set; }
        public string uae_complemento { get; set; }
        public bool uae_Enderecoprincipal { get; set; }
        public double uae_latitude { get; set; }
        public double uae_longitude { get; set; }
        public string banco { get; set; }
        public string excluido { get; set; }
        public string novo { get; set; }
    }

    public DataTable VS_Uae
    {
        get { return (DataTable)(ViewState["VS_Uae"]); }
        set { ViewState["VS_Uae"] = value; }
    }

    /// <summary>
    /// Propriedade em ViewState para setar o ValidationGroup dos validators de cada item
    /// do repeater.
    /// </summary>
    protected string _ValidationGroup
    {
        get
        {
            if (ViewState["_ValidationGroup"] == null)
                return "Endereco";

            return ViewState["_ValidationGroup"].ToString();
        }
        set
        {
            ViewState["_ValidationGroup"] = value;
        }
    }

    /// <summary>
    /// Propriedade guardada em ViewState que indica se é cadastro de um único endereço,
    /// ou de vários.
    /// </summary>
    private bool _VS_CadastroUnico
    {
        get
        {
            if (ViewState["_VS_CadastroUnico"] == null)
                return false;

            return (bool)ViewState["_VS_CadastroUnico"];
        }
        set
        {
            ViewState["_VS_CadastroUnico"] = value;
        }
    }

    /// <summary>
    /// Propriedade que indica se os campos do endereço são obrigatórios (se não informado,
    /// o padrão é true).
    /// </summary>
    protected bool _VS_Obrigatorio
    {
        get
        {
            if (ViewState["_VS_Obrigatorio"] == null)
                return true;

            return (bool)ViewState["_VS_Obrigatorio"];
        }
        set
        {
            ViewState["_VS_Obrigatorio"] = value;
        }
    }

    /// <summary>
    /// Retorna o ClientID do txtLogradouro (se não for cadastro único, retorna da última
    /// linha do repeater).
    /// </summary>
    public string ClientIDTxtLogradouro
    {
        get
        {
            //if (rptEndereco.Items.Count > 0)
            //{
            //    TextBox txtLogradouro = (TextBox)rptEndereco.Items[rptEndereco.Items.Count - 1].FindControl("txtLogradouro");

            //    return txtLogradouro.ClientID;
            //}

            return "";
        }
    }

    /// <summary>
    /// Retorna o ClientID do txtCEP (se não for cadastro único, retorna da última
    /// linha do repeater).
    /// </summary>
    public string ClientIDTxtCEP
    {
        get
        {
            //if (rptEndereco.Items.Count > 0)
            //{
            //    TextBox txt = (TextBox)rptEndereco.Items[rptEndereco.Items.Count - 1].FindControl("txtCEP");

            //    return txt.ClientID;
            //}

            return "";
        }
    }

    /// <summary>
    /// Seta se o número e o complemento ficarão visíveis.
    /// </summary>
    public bool VisibleNumero
    {
        set
        {
            //foreach (RepeaterItem item in rptEndereco.Items)
            //{
            //    item.FindControl("trNumeroCompl").Visible = value;
            //}
        }
    }

    public bool VS_Edit_Command
    {
        get
        {
            if (ViewState["VS_Edit_Command"] == null)
                ViewState["VS_Edit_Command"] = false;
            return (bool)(ViewState["VS_Edit_Command"]);
        }
        set
        {
            ViewState["VS_Edit_Command"] = value;
        }
    }

    private int _VS_Indice
    {
        get
        {
            if (ViewState["_VS_Indice"] == null)
                return 0;

            return (int)ViewState["_VS_Indice"];
        }
        set
        {
            ViewState["_VS_Indice"] = value;
        }
    }

    private bool _VS_NovoEndereco
    {
        get
        {
            if (ViewState["_VS_NovoEndereco"] == null)
                return false;

            return (bool)ViewState["_VS_NovoEndereco"];
        }
        set
        {
            ViewState["_VS_NovoEndereco"] = value;
        }
    }

    #endregion

    #region Métodos

    /// <summary>
    /// Cria um endereço vazio e seta propriedades necessárias, chamar no Page_Load.
    /// </summary>
    public void Inicializar(bool obrigatorio, bool cadastroUnico, string validationGroup)
    {

        _ValidationGroup = validationGroup;
        _VS_CadastroUnico = cadastroUnico;
        _VS_Obrigatorio = obrigatorio;

        NovoEndereco();

        SetaBotoes();
    }

    /// <summary>
    /// Carrega um endereço com os dados passados por parâmetro - Utilizar quando for
    /// cadastro único.
    /// </summary>
    /// <param name="entEndereco"></param>
    /// <param name="numero"></param>
    /// <param name="complemento"></param>
    public void CarregarEndereco(END_Endereco entEndereco, string numero, string complemento, double latitude, double longitude, bool principal)
    {
        try
        {
            DataTable dt = CriaDataTable(true);

            dt.Rows[0]["end_id"] = entEndereco.end_id;

            // Se for endereço novo - não tem END_ID - setar novo ID.
            if (entEndereco.end_id == Guid.Empty)
            {
                dt.Rows[0]["end_id"] = Guid.NewGuid();
            }

            dt.Rows[0]["end_cep"] = entEndereco.end_cep;
            dt.Rows[0]["end_logradouro"] = entEndereco.end_logradouro;
            dt.Rows[0]["end_distrito"] = entEndereco.end_distrito;
            dt.Rows[0]["end_zona"] = entEndereco.end_zona;
            dt.Rows[0]["end_bairro"] = entEndereco.end_bairro;
            dt.Rows[0]["cid_id"] = entEndereco.cid_id;

            // Carregar cidade.
            END_Cidade cid = new END_Cidade()
            {
                cid_id = entEndereco.cid_id
            };
            END_CidadeBO.GetEntity(cid);

            dt.Rows[0]["cid_nome"] = cid.cid_nome;
            dt.Rows[0]["numero"] = numero;
            dt.Rows[0]["complemento"] = complemento;
            dt.Rows[0]["latitude"] = latitude;
            dt.Rows[0]["longitude"] = longitude;
            dt.Rows[0]["Enderecoprincipal"] = principal;
            dt.Rows[0]["novo"] = false;

            CarregarEnderecos(dt);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os endereços.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Carrega um endereço com os dados passados por parâmetro - Utilizar quando for
    /// cadastro único.
    /// </summary>
    /// <param name="end_id"></param>
    /// <param name="numero"></param>
    /// <param name="complemento"></param>
    public void CarregarEndereco(Guid end_id, string numero, string complemento)
    {
        try
        {
            END_Endereco ent = new END_Endereco() { end_id = end_id };
            END_EnderecoBO.GetEntity(ent);

            CarregarEndereco(ent, numero, complemento, 0, 0, true);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os endereços.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Carrega os dados com o DataTable passado, e guarda os ids em ViewState.
    /// </summary>
    /// <param name="dt"></param>
    public void CarregarEnderecosBanco(DataTable dt)
    {
        try
        {
            DataColumnCollection columns = dt.Columns;


            _VS_Indice = 0;
            if (!columns.Contains("banco"))
                dt.Columns.Add("banco", typeof(Boolean));
            if (!columns.Contains("excluido"))
                dt.Columns.Add("excluido", typeof(Boolean));
            if (!columns.Contains("novo"))
                dt.Columns.Add("novo", typeof(Boolean));
            if (!columns.Contains("cid_nome"))
                dt.Columns.Add("cid_nome");
            if (!columns.Contains("id"))
                dt.Columns.Add("id");

            foreach (DataRow dr in dt.Rows)
            {
                dr["banco"] = true;
                dr["excluido"] = false;
                dr["novo"] = false;
                dr["id"] = Guid.NewGuid();

                // Carregar cidade.
                END_Cidade cid = new END_Cidade()
                {
                    cid_id = new Guid(dr["cid_id"].ToString())
                };
                END_CidadeBO.GetEntity(cid);

                dr["cid_nome"] = cid.cid_nome;

                if (string.IsNullOrEmpty(dr["enderecoprincipal"].ToString()))
                    dr["enderecoprincipal"] = "false";
            }

            grvEndereco.DataSource = dt;
            grvEndereco.DataBind();

            VS_Uae = dt;


            SetaBotoes();
        }
        catch (Exception ex)
        {
            lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os endereços.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Carrega os dados com o DataTable passado.
    /// </summary>
    /// <param name="dt"></param>
    private void CarregarEnderecos(DataTable dt)
    {
        try
        {
            grvEndereco.DataSource = dt;
            grvEndereco.DataBind();

            VS_Uae = dt;

            SetaBotoes();
        }
        catch
        {
            lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os endereços.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Carrega a entidade do endereço cadastrado quando for cadastro único, seta o número
    /// e o complemento. 
    /// Retorna true:
    /// - Se o endereço não é obrigatório e está completo 
    ///     (todos os campos obrigatórios estão preenchidos).
    /// - Se o endereço não é obrigatório e não foi preenchido nenhum campo.
    /// Retorna false:
    /// - Se o endereço é obrigatório e não foi preenchido todos os campos.
    /// - Se o endereço não é obrigatório e tem somente alguns campos preenchidos 
    ///     (começou tem que terminar).
    /// </summary>
    /// <param name="ent"></param>
    /// <param name="numero"></param>
    /// <param name="complemento"></param>
    /// <param name="msgErro"></param>
    /// <returns></returns>
    public bool RetornaEnderecoCadastrado(out DataTable dt, out string msgErro)
    {
        END_Endereco ent = new END_Endereco();
        msgErro = "";
        string numero = "";
        string complemento = "";
        double latitude = 0;
        double longitude = 0;

        //DataTable 
        //dt = RetornaEnderecos();
        dt = VS_Uae;
        bool ret = false;
        bool enderecoPrincipal = false;
        // if (dt.Rows.Count > 0)
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            // Carregar dados do endereço.
            DataRow dr = dt.Rows[i];

            if (Convert.ToBoolean(dr["enderecoprincipal"].ToString()))
                enderecoPrincipal = Convert.ToBoolean(dr["enderecoprincipal"].ToString());

            string end_id = dr["end_id"].ToString();

            // Preenche o ID do endereço.
            if ((String.IsNullOrEmpty(end_id)) || (end_id.Equals(Guid.Empty.ToString())))
            {
                ent.end_id = new Guid(dr["id"].ToString());
                ent.IsNew = true;
            }
            else
            {
                ent.end_id = new Guid(dr["end_id"].ToString());
                ent.IsNew = false;

                END_EnderecoBO.GetEntity(ent);
            }

            ent.end_cep = dr["end_cep"].ToString();
            ent.end_logradouro = dr["end_logradouro"].ToString();
            ent.end_distrito = dr["end_distrito"].ToString();
            if (!(string.IsNullOrEmpty(dr["end_zona"].ToString())))
                ent.end_zona = Convert.ToByte(dr["end_zona"]);
            ent.end_bairro = dr["end_bairro"].ToString();
            ent.cid_id = String.IsNullOrEmpty(dr["cid_id"].ToString()) ? Guid.Empty : new Guid(dr["cid_id"].ToString());
            ent.cid_nome = dr["cid_nome"].ToString();

            numero = dr["numero"].ToString();
            complemento = dr["complemento"].ToString();

            //}            

            // Verificar se endereço está válido.
            if (_VS_Obrigatorio)
            {
                ret = ((ent.Validate()) && (ent.cid_id != Guid.Empty));

                if (!ret)
                    msgErro = UtilBO.ErrosValidacao(ent);
                if (!string.IsNullOrEmpty(dr["latitude"].ToString()))
                {
                    if (double.TryParse(dr["latitude"].ToString(), out latitude))
                    {
                        dr["latitude"] = dr["latitude"].ToString().Replace(".", ",");
                        latitude = string.IsNullOrEmpty(dr["latitude"].ToString()) ? 0 : double.Parse(dr["latitude"].ToString());
                    }
                    else
                    {
                        msgErro += "Latitude está incorreto.<br/>";
                        ret = false;
                    }
                }
                if (!string.IsNullOrEmpty(dr["longitude"].ToString()))
                {
                    if (double.TryParse(dr["longitude"].ToString(), out longitude))
                    {
                        dr["longitude"] = dr["longitude"].ToString().Replace(".", ",");
                        longitude = string.IsNullOrEmpty(dr["longitude"].ToString()) ? 0 : double.Parse(dr["longitude"].ToString());
                    }
                    else
                    {
                        msgErro += "Longitude está incorreto.<br/>";
                        ret = false;
                    }
                }

                if (ent.cid_id == Guid.Empty)
                {
                    if (String.IsNullOrEmpty(ent.cid_nome))
                        msgErro += "Endereço " + (i + 1) + " - Cidade é obrigatório.<br/>";
                    else
                        msgErro += "Endereço " + (i + 1) + " - Cidade não encontrada.<br/>";
                }

                if (String.IsNullOrEmpty(numero))
                {
                    ret = false;
                    msgErro += "Endereço " + (i + 1) + " - Número é obrigatório.<br/>";
                }


            }
            else
            {
                if ((!String.IsNullOrEmpty(ent.end_cep)) ||
                    (!String.IsNullOrEmpty(ent.end_logradouro)) ||
                    (!String.IsNullOrEmpty(numero)) ||
                    (!String.IsNullOrEmpty(ent.end_distrito)) ||
                    (ent.end_zona > 0) ||
                    (!String.IsNullOrEmpty(ent.end_bairro)) ||
                    (ent.cid_id != Guid.Empty))
                {
                    // Se preencheu pelo menos 1 campo, tem que preencher todos.
                    ret = ((ent.Validate()) && (ent.cid_id != Guid.Empty));

                    if (!ret)
                        msgErro += UtilBO.ErrosValidacao(ent);

                    if (ent.cid_id == Guid.Empty)
                        msgErro += "Endereço " + (i + 1) + " - Cidade é obrigatório.<br/>";

                    if (String.IsNullOrEmpty(numero))
                    {
                        ret = false;
                        msgErro += "Endereço " + (i + 1) + " - Número é obrigatório.<br/>";
                    }
                    if (!enderecoPrincipal)
                    {
                        ret = false;
                        msgErro += "É necessário selecionar um endereço principal.";
                    }
                    if (!string.IsNullOrEmpty(dr["latitude"].ToString()))
                    {
                        if (double.TryParse(dr["latitude"].ToString(), out latitude))
                        {
                            // dr["latitude"] = dr["latitude"].ToString().Replace(".", ",");
                            latitude = string.IsNullOrEmpty(dr["latitude"].ToString()) ? 0 : double.Parse(dr["latitude"].ToString());
                        }
                        else
                        {
                            msgErro += "Latitude está incorreto.<br/>";
                            ret = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(dr["longitude"].ToString()))
                    {
                        if (double.TryParse(dr["longitude"].ToString(), out longitude))
                        {
                            // dr["longitude"] = dr["longitude"].ToString().Replace(".", ",");
                            longitude = string.IsNullOrEmpty(dr["longitude"].ToString()) ? 0 : double.Parse(dr["longitude"].ToString());
                        }
                        else
                        {
                            msgErro += "Longitude está incorreto.<br/>";
                            ret = false;
                        }
                    }
                }
                else
                {
                    ret = true;
                }
            }
        }
        if (!_VS_Obrigatorio && dt.Rows.Count == 0)
        {
            ret = true;
        }

        if (_VS_Obrigatorio && !enderecoPrincipal)
        {
            ret = false;
            msgErro += "É necessário selecionar um endereço principal.";
        }

        return ret;

    }

    /// <summary>
    /// Cria a tabela de endereços com os campos necessários.
    /// </summary>
    /// <param name="adicionaLinhaVazia"></param>
    /// <returns></returns>
    private DataTable CriaDataTable(bool adicionaLinhaVazia)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("cpr_id");
        dt.Columns.Add("id");
        dt.Columns.Add("end_id");
        dt.Columns.Add("end_cep");
        dt.Columns.Add("end_logradouro");
        dt.Columns.Add("end_distrito");
        dt.Columns.Add("end_zona");
        dt.Columns.Add("end_bairro");
        dt.Columns.Add("cid_id");
        dt.Columns.Add("cid_nome");
        dt.Columns.Add("endRel_id");
        dt.Columns.Add("numero");
        dt.Columns.Add("complemento");
        dt.Columns.Add("enderecoprincipal");
        dt.Columns.Add("latitude");
        dt.Columns.Add("longitude");
        dt.Columns.Add("banco", typeof(Boolean));
        dt.Columns.Add("excluido", typeof(Boolean));
        dt.Columns.Add("novo", typeof(Boolean));

        if (adicionaLinhaVazia)
        {
            dt = AdicionaLinha(dt);
        }

        return dt;
    }

    /// <summary>
    /// Cria uma nova linha na tabela.
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    private DataTable AdicionaLinha(DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["banco"] = false;
        dr["excluido"] = false;
        dr["id"] = Guid.NewGuid();
        dr["novo"] = true;

        if (dt.Rows.Count == 0)
            dr["enderecoprincipal"] = "true";

        dt.Rows.Add(dr);

        return dt;
    }

    /// <summary>
    /// Adiciona novo endereço no repeater
    /// </summary>
    private void NovoEndereco()
    {
        // Atualizar o DataTable do ViewState com os dados que estão no Repeater.
        DataTable dt = RetornaEnderecos();

        //  dt = AdicionaLinha(dt);

        CarregarEnderecos(dt);


    }

    /// <summary>
    /// Retorna um dataTable com os RowStates corretos, de acordo com o que foi alterado 
    /// na tela.
    /// </summary>
    /// <returns></returns>
    private DataTable RetornaTabelaCadastro()
    {
        DataTable dt = RetornaEnderecos();

        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToBoolean(dr["excluido"]))
            {
                // Forçar o RowState para ficar como Deleted.
                dr.AcceptChanges();
                dr.Delete();
            }
            else if (Convert.ToBoolean(dr["banco"]))
            {
                // Forçar o RowState para ficar como Modified.
                dr.AcceptChanges();
                dr["end_id"] = dr["end_id"];
            }
        }

        return dt;
    }

    /// <summary>
    /// Retorna um datatable contendo os dados que estão no repeater.
    /// </summary>
    /// <returns></returns>
    private DataTable RetornaEnderecos()
    {
        DataTable dt = CriaDataTable(false);
        return dt;
    }

    /// <summary>
    /// Preeche os dados do item do repeater com os dados da linha.
    /// </summary>
    /// <param name="dr"></param>
    /// <param name="item"></param>
    /// <param name="preencherNumero"></param>
    private void PreencherDados(DataRow dr, bool preencherNumero)
    {
        txtEnd_id.Value = dr["end_id"].ToString();

        txtCEP.Text = dr["end_cep"].ToString();
        txtLogradouro.Text = dr["end_logradouro"].ToString();
        txtDistrito.Text = dr["end_distrito"].ToString();

        if ((!String.IsNullOrEmpty(dr["end_zona"].ToString())) && (Convert.ToByte(dr["end_zona"].ToString()) > 0))
            UCComboZona1._Combo.SelectedValue = dr["end_zona"].ToString();
        else
            UCComboZona1._Combo.Enabled = true;

        txtBairro.Text = dr["end_bairro"].ToString();
        txtCid_id.Value = dr["cid_id"].ToString();
        txtCidade.Text = dr["cid_nome"].ToString();
        //((RadioButton)item.FindControl("ckbEndPrincipal")).Checked = (string.IsNullOrEmpty(dr["uae_Enderecoprincipal"].ToString()) ? false : Convert.ToBoolean(dr["uae_Enderecoprincipal"].ToString()));

        if (preencherNumero)
        {
            txtNumero.Text = dr["numero"].ToString();
            txtComplemento.Text = dr["complemento"].ToString();
        }
    }

    /// <summary>
    /// Limpa dados da linha informada.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="limpaCEP"></param>
    private void LimpaDadosLinha(bool limpaCEP)
    {
        if (limpaCEP)
        {
            txtCEP.Text = "";
            txtCEP.Focus();
        }

        ckbEndPrincipal.Checked = false;
        txtCEP.Text = "";
        txtLogradouro.Text = "";
        txtNumero.Text = "";
        txtDistrito.Text = "";
        UCComboZona1._Combo.SelectedValue = "-1";
        txtBairro.Text = "";
        txtCid_id.Value = "";
        txtCidade.Text = "";
        txtId.Value = "";
        txtComplemento.Text = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        txtNovo.Value = "";
        lblID.Text = "";
        txtEnd_id.Value = "";

        updModal.Update();
        upValidationSummary.Update();
       // UpdatePanel1.Update();

        txtCEP.Focus();
    }

    /// <summary>
    /// Habilita/Desabilita os campos comuns do endereço (cidade, distrito, 
    /// bairro, zona).
    /// </summary>
    /// <param name="item"></param>
    /// <param name="habilitado"></param>
    private void HabilitaCamposEndereco(bool habilitado)
    {
        if (habilitado)
        {
            txtDistrito.Attributes.Remove("readOnly1");
            txtBairro.Attributes.Remove("readOnly1");
            txtCidade.Attributes.Remove("readOnly1");
        }
        else
        {
            txtDistrito.Attributes.Add("readOnly1", "readOnly");
            txtBairro.Attributes.Add("readOnly1", "readOnly");
            txtCidade.Attributes.Add("readOnly1", "readOnly");
        }

        UCComboZona1._Combo.Enabled = habilitado;
    }

    /// <summary>
    /// Coloca visible=true no botão Novo da última linha do repeater.
    /// </summary>
    private void SetaBotoes()
    {
        // SE GRID VIEW TEM UM ITEM, MOSTRA BOTÃO, SE NÃO NÃO MOSTRA.
        if (grvEndereco.Rows.Count <= 0 && _VS_CadastroUnico)// SE FOR CADASTRO UNICO E NÃO TEM NENHUM ITEM CADASTRADO
        {
            btnNovoEnd.Visible = true;
        }
        else if (!_VS_CadastroUnico) // SE NÃO É CADASTRO UNICO
        {
            btnNovoEnd.Visible = true;
        }
        else
        { // SE JÁ EXISTE UM ENDEREÇO CADASTRADO NÃO PERMITE CADASTRO MULTIPLO
            btnNovoEnd.Visible = false;
        }

    }

    /// <summary>
    /// Exclui o endereço do índice informado da tabela.
    /// </summary>
    /// <param name="i"></param>
    private void ExcluirEndereco(int i)
    {
        DataTable dt = RetornaEnderecos();

        if (dt.Rows.Count > i)
        {
            if (Convert.ToBoolean(dt.Rows[i]["banco"]))
            {
                // Exclui logicamente a linha.
                dt.Rows[i]["excluido"] = true;
            }
            else
            {
                dt.Rows[i].Delete();
            }
        }

        CarregarEnderecos(dt);
    }

    /// <summary>
    /// Verifica se existe algum item visível no repeater
    /// </summary>
    /// <returns>true caso exista pelo menos um item visível 
    /// ou false caso não exista nenhum item visível</returns>
    private bool VerificaItemVisivel()
    {
        foreach (GridViewRow ri in grvEndereco.Rows)
        {
            if (ri.Visible == true)
            {
                return true;
            }
        }

        return false;
    }

    public void AbrirFecharPopup(string div, string action, string resource)
    {
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "NovoItem", "$('#" + div + "').dialog('" + action + "');" +
            (String.IsNullOrEmpty(resource) ? "" : " $('#" + div + "').dialog({title: '" + resource + "'}); "), true);
    }
    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            if (VS_Edit_Command)
            {
                VS_Edit_Command = false;
            }
            VS_Edit_Command = false;
        }
    }
    /// <summary>
    /// Carrega o CEP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtCEP_TextChanged(object sender, EventArgs e)
    {
        TextBox _txtCEP = (TextBox)sender;
        try
        {
            if (!String.IsNullOrEmpty(_txtCEP.Text.Trim()))
            {
                DataTable dt = END_EnderecoBO.GetSelect(Guid.Empty, Guid.Empty, Guid.Empty,
                        Guid.Empty, _txtCEP.Text, "", "", "", "", "", "", 0, false, 0, 1);

                if (dt.Rows.Count == 1)
                {
                    PreencherDados(dt.Rows[0], false);
                    HabilitaCamposEndereco(false);
                    txtNumero.Focus();
                }
                else
                {
                    HabilitaCamposEndereco(true);
                    txtEnd_id.Value = string.Empty;
                    txtLogradouro.Focus();
                }
            }
            else
            {
                LimpaDadosLinha(false);
                HabilitaCamposEndereco(true);
                txtLogradouro.Focus();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            lblMessage.Text =
                UtilBO.GetErroMessage("Erro ao tentar carregar o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void btnLimparEndereco_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnLimparEndereco = (ImageButton)sender;
        RepeaterItem item = ((RepeaterItem)btnLimparEndereco.NamingContainer);

        try
        {
            LimpaDadosLinha(true);
            HabilitaCamposEndereco(true);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            ((Label)item.FindControl("lblMessage")).Text =
                UtilBO.GetErroMessage("Erro ao tentar limpar os campos do endereço.", UtilBO.TipoMensagem.Erro);
        }
    }
    protected void btnNovoEnd_Click(object sender, EventArgs e)
    {
        try
        {
            _VS_NovoEndereco = true;
            LimpaDadosLinha(true);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar incluir novo endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        Button btnExcluir = (Button)sender;
        RepeaterItem item = (RepeaterItem)btnExcluir.NamingContainer;

        try
        {
            ExcluirEndereco(item.ItemIndex);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            ((Label)item.FindControl("lblMessage")).Text =
                UtilBO.GetErroMessage("Erro ao tentar excluir o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }
    /// <summary>
    /// Ações dos botões da GRID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grvEndereco_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            _VS_Indice = int.Parse(e.CommandArgument.ToString());
            _VS_NovoEndereco = false;

            //RADIOBUTTON
            ckbEndPrincipal.Checked = Convert.ToBoolean(VS_Uae.Rows[_VS_Indice]["enderecoprincipal"].ToString());
            
            //TEXT BOX
            txtCEP.Text = VS_Uae.Rows[_VS_Indice]["end_cep"].ToString();
            txtLogradouro.Text = VS_Uae.Rows[_VS_Indice]["end_logradouro"].ToString();
            txtNumero.Text = VS_Uae.Rows[_VS_Indice]["numero"].ToString();
            txtComplemento.Text = VS_Uae.Rows[_VS_Indice]["complemento"].ToString();

            txtBairro.Text = VS_Uae.Rows[_VS_Indice]["end_bairro"].ToString();
            txtCidade.Text = VS_Uae.Rows[_VS_Indice]["cid_nome"].ToString();
            txtDistrito.Text = VS_Uae.Rows[_VS_Indice]["end_distrito"].ToString();

            //CARREGA LATITUDE E LONGITUDE COMO ESTÁ SALVA
            if (VS_Uae.Rows[_VS_Indice]["latitude"].ToString().Equals("0"))
                txtLatitude.Text = "00.000000";
            else
                txtLatitude.Text = VS_Uae.Rows[_VS_Indice]["latitude"].ToString().Replace(",", ".");
            if (VS_Uae.Rows[_VS_Indice]["longitude"].ToString().Equals("0"))
                txtLongitude.Text = "00.000000";
            else
                txtLongitude.Text = VS_Uae.Rows[_VS_Indice]["longitude"].ToString().Replace(",", ".");
            txtNovo.Value = VS_Uae.Rows[_VS_Indice]["novo"].ToString();
            lblID.Text = VS_Uae.Rows[_VS_Indice]["id"].ToString();
            
            //COMBO
            if ((!string.IsNullOrEmpty(VS_Uae.Rows[_VS_Indice]["end_zona"].ToString())) && (Convert.ToInt16(VS_Uae.Rows[_VS_Indice]["end_zona"].ToString()) > 0))
                UCComboZona1._Combo.SelectedValue = VS_Uae.Rows[_VS_Indice]["end_zona"].ToString();
            else
                UCComboZona1._Combo.SelectedIndex = -1;

            //IDS
            txtId.Value = VS_Uae.Rows[_VS_Indice]["endRel_id"].ToString();
            txtCid_id.Value = VS_Uae.Rows[_VS_Indice]["cid_id"].ToString();
            if (!string.IsNullOrEmpty(VS_Uae.Rows[_VS_Indice]["end_id"].ToString()))
                txtEnd_id.Value = VS_Uae.Rows[_VS_Indice]["end_id"].ToString();

            txtCEP.Focus();
            VS_Edit_Command = true;

            updModal.Update();
            upValidationSummary.Update();
            // UpdatePanel1.Update();

            string cep = VS_Uae.Rows[_VS_Indice]["end_cep"].ToString();
            string principal = VS_Uae.Rows[_VS_Indice]["enderecoprincipal"].ToString().ToLower();

            ScriptManager.RegisterStartupScript(upValidationSummary, upValidationSummary.GetType(), "carregarEditar", "carregarEditar(" + cep + ", "+ principal + ");", true); 

            grvEndereco.DataSource = VS_Uae;
            grvEndereco.DataBind();

        }
        else if (e.CommandName == "Deletar")
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());

                if (Convert.ToBoolean(VS_Uae.Rows[index]["enderecoprincipal"]) && VS_Uae.Rows.Count > 1)
                {

                    for (int i = 0; i < VS_Uae.Rows.Count; i++)
                    {

                        if (index != i)
                        {
                            VS_Uae.Rows[i]["enderecoprincipal"] = "true";
                            break;
                        }
                    }
                }

                if ( string.IsNullOrEmpty(VS_Uae.Rows[index]["endRel_id"].ToString()) || VS_Uae.Rows[index]["endRel_id"].ToString().Equals(Guid.Empty))
                {
                    VS_Uae.Rows.Remove(VS_Uae.Rows[index]);
                }
                else
                {
                    VS_Uae.Rows[index]["excluido"] = "true";
                }

                int excluido = 0;

                foreach (DataRow row in VS_Uae.Rows)
                {
                    if (Convert.ToBoolean(row["excluido"]))
                    {
                        excluido++;
                    }
                }

                if ((VS_Uae.Rows.Count == excluido) || VS_Uae.Rows.Count <= 0)
                {
                    grvEndereco.DataSource = null;
                    grvEndereco.DataBind();
                }
                else
                {
                    grvEndereco.DataSource = VS_Uae;
                    grvEndereco.DataBind();
                }
                updCadastroEndereco.Update();

                SetaBotoes();
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
            }
        }
    }
    /// <summary>
    /// Validação da exibição dos botões
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grvEndereco_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton _btnEditar = (ImageButton)e.Row.FindControl("_btnEditar");
            if (_btnEditar != null)
            {
                _btnEditar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_alterar;
                _btnEditar.CommandArgument = e.Row.RowIndex.ToString();
            }

            ImageButton _btnExcluir = (ImageButton)e.Row.FindControl("_btnExcluir");
            if (_btnExcluir != null)
            {
                _btnExcluir.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_excluir;
                _btnExcluir.CommandArgument = e.Row.RowIndex.ToString();
            }

            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "excluido")))
            {
                e.Row.Visible = false;
            }

            RadioButton ckbEndPrincipal = (RadioButton)e.Row.FindControl("ckbEnderecoPrincipal");
            ckbEndPrincipal.Checked = string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "enderecoprincipal").ToString()) ? false : Convert.ToBoolean((DataBinder.Eval(e.Row.DataItem, "enderecoprincipal").ToString()));
        }
    }
    /// <summary>
    ///  Grava ou atualiza um endereço.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        DataRow dr = VS_Uae.NewRow();
        decimal latitude = decimal.Zero;
        decimal longitude = decimal.Zero;
        //DEFINE CULTURA
        CultureInfo cultures = new CultureInfo("fr-FR");
        //CONFIGURA CAMPOS
        if (!string.IsNullOrEmpty(txtLatitude.Text))
        {
            if (!txtLatitude.Text.Contains(',') || !txtLatitude.Text.Contains('.'))
            {

                if (txtLatitude.Text.Contains('-'))
                {
                    if (txtLatitude.Text.Substring(2, 1).Equals(",") || txtLatitude.Text.Substring(2, 1).Equals("."))
                    {
                        txtLatitude.Text = txtLatitude.Text.Substring(0, 2) + ',' + txtLatitude.Text.Substring(3, (txtLatitude.Text.Length - 3));
                    }
                    else
                    {
                        txtLatitude.Text = txtLatitude.Text.Substring(0, 3) + ',' + txtLatitude.Text.Substring(4, (txtLatitude.Text.Length - 4));
                    }
                }
                else
                {
                    if (txtLatitude.Text.Substring(1, 1).Equals(",") || txtLatitude.Text.Substring(1, 1).Equals("."))
                    {
                        txtLatitude.Text = txtLatitude.Text.Substring(0, 1) + ',' + txtLatitude.Text.Substring(2, (txtLatitude.Text.Length - 2));
                    }
                    else
                    {
                        txtLatitude.Text = txtLatitude.Text.Substring(0, 2) + ',' + txtLatitude.Text.Substring(3, (txtLatitude.Text.Length - 3));
                    }
                }
            }
            decimal lat;
            decimal.TryParse(txtLatitude.Text, out lat);
            latitude = lat;
        }
        if (!string.IsNullOrEmpty(txtLongitude.Text))
        {
            if (!txtLongitude.Text.Contains(',') || !txtLongitude.Text.Contains('.'))
            {
                if (txtLongitude.Text.Contains('-'))
                {
                    if (txtLongitude.Text.Substring(2, 1).Equals(",") || txtLongitude.Text.Substring(2, 1).Equals("."))
                    {
                        txtLongitude.Text = txtLongitude.Text.Substring(0, 2) + ',' + txtLongitude.Text.Substring(3, (txtLongitude.Text.Length - 3));
                    }
                    else
                    {
                        txtLongitude.Text = txtLongitude.Text.Substring(0, 3) + ',' + txtLongitude.Text.Substring(4, (txtLongitude.Text.Length - 4));
                    }
                }
                else
                {
                    if (txtLongitude.Text.Substring(1, 1).Equals(",") || txtLongitude.Text.Substring(1, 1).Equals("."))
                    {
                        txtLongitude.Text = txtLongitude.Text.Substring(0, 1) + ',' + txtLongitude.Text.Substring(2, (txtLongitude.Text.Length - 2));
                    }
                    else
                    {
                        txtLongitude.Text = txtLongitude.Text.Substring(0, 2) + ',' + txtLongitude.Text.Substring(3, (txtLongitude.Text.Length - 3));
                    }
                }
            }
            decimal lon;
            decimal.TryParse(txtLongitude.Text, out lon);
            longitude = lon;
        }

        // BUSCA VALOR DO COMBO ZONA
        int zona = UCComboZona1._Combo.SelectedValue == "-1" ? 0 : Convert.ToInt32(UCComboZona1._Combo.SelectedValue);

        //Verifica se é novo ou edição
        if (!_VS_NovoEndereco && string.IsNullOrEmpty(VS_Uae.Rows[_VS_Indice]["endRel_id"].ToString()))
        {
            //SE FOR UMA EDIÇÃO DE ITEM NOVO E NÃO SALVO
            VS_Uae.Rows.Remove(VS_Uae.Rows[_VS_Indice]);
            VS_Uae.AcceptChanges();
            //Obrigatórios
            dr["id"] = Guid.NewGuid();

            if (string.IsNullOrEmpty(txtEnd_id.Value))
                dr["end_id"] = Guid.Empty;
            else
                dr["end_id"] = txtEnd_id.Value;

            dr["end_cep"] = txtCEP.Text;
            dr["end_logradouro"] = txtLogradouro.Text;
            dr["end_distrito"] = txtDistrito.Text;
            dr["end_zona"] = zona;
            dr["end_bairro"] = txtBairro.Text;
            dr["cid_id"] = txtCid_id.Value;
            dr["cid_nome"] = txtCidade.Text;
            dr["numero"] = txtNumero.Text;
            //Não obrigatórios
            dr["complemento"] = string.IsNullOrEmpty(txtComplemento.Text) ? string.Empty : txtComplemento.Text;
            dr["enderecoprincipal"] = ckbEndPrincipal.Checked ? "true" : "false";
            dr["latitude"] = latitude;
            dr["longitude"] = longitude;
            //Setados no back
            dr["banco"] = "false";
            dr["excluido"] = "false";
            dr["novo"] = "true";
            //Verifica se é o primeiro endereço a ser adicionado; se for, seta como principal.
            if (VS_Uae.Rows.Count == 0)
                dr["enderecoprincipal"] = "true";
            else
            { // CASO NÃO VERIFICA SE ESTÁ SETADO COMO PRINCIPAL; CASO SIM, ALTERA O ENDEREÇO QUE ESTAVA SETADO ANTERIORMENTE
                if (ckbEndPrincipal.Checked)
                {
                    dr["enderecoprincipal"] = ckbEndPrincipal.Checked;
                    for (int i = 0; i < VS_Uae.Rows.Count; i++)
                    {
                        if (!dr["id"].ToString().Equals(VS_Uae.Rows[i]["id"].ToString()))
                            VS_Uae.Rows[i]["enderecoprincipal"] = "false";
                    }
                }
            }

            VS_Uae.Rows.Add(dr);
        }
        else if (_VS_NovoEndereco)
        {
            //SE FOR UM ITEM NOVO
            //Obrigatórios
            dr["id"] = Guid.NewGuid();

            if (string.IsNullOrEmpty(txtEnd_id.Value))
                dr["end_id"] = Guid.Empty;
            else
                dr["end_id"] = txtEnd_id.Value;

            dr["end_cep"] = txtCEP.Text;
            dr["end_logradouro"] = txtLogradouro.Text;
            dr["end_distrito"] = txtDistrito.Text;
            dr["end_zona"] = zona;
            dr["end_bairro"] = txtBairro.Text;
            dr["cid_id"] = txtCid_id.Value;
            dr["cid_nome"] = txtCidade.Text;
            dr["numero"] = txtNumero.Text;
            //Não obrigatórios
            dr["complemento"] = string.IsNullOrEmpty(txtComplemento.Text) ? string.Empty : txtComplemento.Text;
            dr["enderecoprincipal"] = ckbEndPrincipal.Checked ? "true" : "false";
            if (latitude != decimal.Zero)
                dr["latitude"] = latitude;
            if (longitude != decimal.Zero)
                dr["longitude"] = longitude;
            //Setados no back
            dr["banco"] = "false";
            dr["excluido"] = "false";
            dr["novo"] = "true";
            //Verifica se é o primeiro endereço a ser adicionado; se for, seta como principal.
            if (VS_Uae.Rows.Count == 0)
                dr["enderecoprincipal"] = "true";
            else
            { // CASO NÃO VERIFICA SE ESTÁ SETADO COMO PRINCIPAL; CASO SIM, ALTERA O ENDEREÇO QUE ESTAVA SETADO ANTERIORMENTE
                if (ckbEndPrincipal.Checked)
                {
                    dr["enderecoprincipal"] = ckbEndPrincipal.Checked;
                    for (int i = 0; i < VS_Uae.Rows.Count; i++)
                    {
                        if (!dr["id"].ToString().Equals(VS_Uae.Rows[i]["id"].ToString()))
                            VS_Uae.Rows[i]["enderecoprincipal"] = "false";
                    }
                }
            }
            VS_Uae.Rows.Add(dr);
        }
        else
        {
            // EDIÇÃO DE ITENS JÁ SALVOS
            int index = 0;
            for (int i = 0; i < VS_Uae.Rows.Count; i++)
            {
                if (txtId.Value == VS_Uae.Rows[i]["endRel_id"].ToString())
                {
                    index = i;
                    break;
                }
            }

            if (string.IsNullOrEmpty(txtEnd_id.Value))
            {
                VS_Uae.Rows[index]["end_id"] = Guid.Empty;
            }
            else
                VS_Uae.Rows[index]["end_id"] = txtEnd_id.Value;

            VS_Uae.Rows[index]["end_cep"] = txtCEP.Text;
            VS_Uae.Rows[index]["end_logradouro"] = txtLogradouro.Text;
            VS_Uae.Rows[index]["end_distrito"] = txtDistrito.Text;
            VS_Uae.Rows[index]["end_zona"] = zona;
            VS_Uae.Rows[index]["end_bairro"] = txtBairro.Text;
            VS_Uae.Rows[index]["cid_id"] = txtCid_id.Value;
            VS_Uae.Rows[index]["cid_nome"] = txtCidade.Text;
            VS_Uae.Rows[index]["endRel_id"] = txtId.Value;
            VS_Uae.Rows[index]["numero"] = txtNumero.Text;
            VS_Uae.Rows[index]["complemento"] = txtComplemento.Text;
            VS_Uae.Rows[index]["Enderecoprincipal"] = ckbEndPrincipal.Checked ? "true" : "false";
            VS_Uae.Rows[index]["latitude"] = latitude;
            VS_Uae.Rows[index]["longitude"] = longitude;
            VS_Uae.Rows[index]["banco"] = "true";
            VS_Uae.Rows[index]["excluido"] = "false";
            VS_Uae.Rows[index]["novo"] = "false";
            if (ckbEndPrincipal.Checked)
            {
                for (int i = 0; i < VS_Uae.Rows.Count; i++)
                {
                    if (lblID.Text != VS_Uae.Rows[i]["id"].ToString())
                        VS_Uae.Rows[i]["enderecoprincipal"] = "false";
                }
            }
        }
        // atualiza o gridview de endereços
        grvEndereco.DataSource = VS_Uae;
        grvEndereco.DataBind();
        // limpa os dados do form
        LimpaDadosLinha(true);
        // atualiza update panel
        updCadastroEndereco.Update();
        // chama metodo JS para limpar a mascara
        ScriptManager.RegisterStartupScript(upValidationSummary, upValidationSummary.GetType(), "limparMascara", "limparMascara();", true);

        SetaBotoes();
    }

    #endregion
}