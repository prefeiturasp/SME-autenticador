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

public partial class WebControls_Endereco_UCEnderecosAntigo : MotherUserControl
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
            if (rptEndereco.Items.Count > 0)
            {
                TextBox txtLogradouro = (TextBox)rptEndereco.Items[rptEndereco.Items.Count - 1].FindControl("txtLogradouro");

                return txtLogradouro.ClientID;
            }

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
            if (rptEndereco.Items.Count > 0)
            {
                TextBox txt = (TextBox)rptEndereco.Items[rptEndereco.Items.Count - 1].FindControl("txtCEP");

                return txt.ClientID;
            }

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
            foreach (RepeaterItem item in rptEndereco.Items)
            {
                item.FindControl("trNumeroCompl").Visible = value;
            }
        }
    }

    private int indice;

    #endregion

    #region Métodos

    /// <summary>
    /// Cria um endereço vazio e seta propriedades necessárias, chamar no Page_Load.
    /// </summary>
    public void Inicializar(bool obrigatorio, bool cadastroUnico, string validationGroup)
    {
        
        _ValidationGroup = validationGroup;
        _VS_CadastroUnico = cadastroUnico;
        _VS_Obrigatorio = _VS_CadastroUnico ? obrigatorio : true;

        NovoEndereco();

        SetaBotoes();
    }

    /// <summary>
    /// Atualiza os campos que estão habilitados ou desabilitados no repeater.
    /// </summary>
    public void AtualizaEnderecos()
    {
        foreach (RepeaterItem item in rptEndereco.Items)
        {
            string end_id = ((HtmlInputHidden)item.FindControl("txtEnd_id")).Value;

            if ((!String.IsNullOrEmpty(end_id)) &&
                (new Guid(end_id) != Guid.Empty))
            {
                // Deixa os campos como ReadyOnly.
                HabilitaCamposEndereco(item, false);
                item.FindControl("btnLimparEndereco").Visible = true;
            }
        }
    }

    /// <summary>
    /// Carrega um endereço com os dados passados por parâmetro - Utilizar quando for
    /// cadastro único.
    /// </summary>
    /// <param name="entEndereco"></param>
    /// <param name="numero"></param>
    /// <param name="complemento"></param>
    public void CarregarEndereco(END_Endereco entEndereco, string numero, string complemento)
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

            CarregarEndereco(ent, numero, complemento);
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
            indice = 0;
            dt.Columns.Add("banco", typeof(Boolean));
            dt.Columns.Add("excluido", typeof(Boolean));

            foreach (DataRow dr in dt.Rows)
            {
                dr["banco"] = true;
                dr["excluido"] = false;
            }

            if (dt.Rows.Count > 0)
            {
                rptEndereco.DataSource = dt;
                rptEndereco.DataBind();
            }

            SetaBotoes();
        }
        catch
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
            indice = 0;
            rptEndereco.DataSource = dt;
            rptEndereco.DataBind();

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
    public bool RetornaEnderecoCadastrado(out END_Endereco ent, out string numero, out string complemento, out string msgErro)
    {
        ent = new END_Endereco();
        msgErro = "";
        numero = "";
        complemento = "";

        DataTable dt = RetornaEnderecos();

        if (dt.Rows.Count > 0)
        {
            // Carregar dados do endereço.
            DataRow dr = dt.Rows[0];

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
            ent.end_zona = Convert.ToByte(dr["end_zona"]);
            ent.end_bairro = dr["end_bairro"].ToString();
            ent.cid_id = String.IsNullOrEmpty(dr["cid_id"].ToString()) ? Guid.Empty : new Guid(dr["cid_id"].ToString());
            ent.cid_nome = dr["cid_nome"].ToString();

            numero = dr["numero"].ToString();
            complemento = dr["complemento"].ToString();
        }

        bool ret;

        // Verificar se endereço está válido.
        if (_VS_Obrigatorio)
        {
            ret = ((ent.Validate()) && (ent.cid_id != Guid.Empty));

            if (!ret)
                msgErro = UtilBO.ErrosValidacao(ent);

            if (ent.cid_id == Guid.Empty)
            {
                if (String.IsNullOrEmpty(ent.cid_nome))
                    msgErro += "Cidade é obrigatório.<br/>";
                else
                    msgErro += "Cidade não encontrada.<br/>";
            }

            if (String.IsNullOrEmpty(numero))
            {
                ret = false;
                msgErro += "Número é obrigatório.";
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
                    msgErro += "Cidade é obrigatório.<br/>";

                if (String.IsNullOrEmpty(numero))
                {
                    ret = false;
                    msgErro += "Número é obrigatório.";
                }
            }
            else
            {
                ret = true;
            }
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
        dt.Columns.Add("numero");
        dt.Columns.Add("complemento");
        dt.Columns.Add("banco", typeof(Boolean));
        dt.Columns.Add("excluido", typeof(Boolean));

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

        dt = AdicionaLinha(dt);

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

        foreach (RepeaterItem item in rptEndereco.Items)
        {

            string end_id = ((HtmlInputHidden)item.FindControl("txtEnd_id")).Value;

            if (!string.IsNullOrEmpty(end_id))
            {
                DataRow dr = dt.NewRow();

                dr["end_id"] = end_id;

                dr["id"] = ((Label)item.FindControl("lblID")).Text;

                dr["end_cep"] = ((TextBox)item.FindControl("txtCEP")).Text;
                dr["end_logradouro"] = ((TextBox)item.FindControl("txtLogradouro")).Text;
                dr["end_distrito"] = ((TextBox)item.FindControl("txtDistrito")).Text;

                UCComboZona UCComboZona1 = (UCComboZona)item.FindControl("UCComboZona1");

                dr["end_zona"] = (UCComboZona1._Combo.SelectedValue == "-1" ? 0 : Convert.ToInt32(UCComboZona1._Combo.SelectedValue));

                dr["end_bairro"] = ((TextBox)item.FindControl("txtBairro")).Text;
                dr["cid_id"] = ((HtmlInputHidden)item.FindControl("txtCid_id")).Value;
                dr["cid_nome"] = ((TextBox)item.FindControl("txtCidade")).Text;
                dr["numero"] = ((TextBox)item.FindControl("txtNumero")).Text;
                dr["complemento"] = ((TextBox)item.FindControl("txtComplemento")).Text;

                dr["banco"] = ((Label)item.FindControl("lblBanco")).Text;
                dr["excluido"] = (String.IsNullOrEmpty(((TextBox)item.FindControl("txtCEP")).Text.Trim())
                        && String.IsNullOrEmpty(((TextBox)item.FindControl("txtLogradouro")).Text.Trim())
                        && String.IsNullOrEmpty(((TextBox)item.FindControl("txtBairro")).Text.Trim())
                        && String.IsNullOrEmpty(((TextBox)item.FindControl("txtCidade")).Text.Trim())
                        && String.IsNullOrEmpty(((TextBox)item.FindControl("txtNumero")).Text.Trim())
                        || !item.Visible);

                dt.Rows.Add(dr);
            }
        }

        return dt;
    }

    /// <summary>
    /// Preeche os dados do item do repeater com os dados da linha.
    /// </summary>
    /// <param name="dr"></param>
    /// <param name="item"></param>
    /// <param name="preencherNumero"></param>
    private void PreencherDados(DataRow dr, RepeaterItem item, bool preencherNumero)
    {
        ((HtmlInputHidden)item.FindControl("txtEnd_id")).Value = dr["end_id"].ToString();

        ((TextBox)item.FindControl("txtCEP")).Text = dr["end_cep"].ToString();
        ((TextBox)item.FindControl("txtLogradouro")).Text = dr["end_logradouro"].ToString();
        ((TextBox)item.FindControl("txtDistrito")).Text = dr["end_distrito"].ToString();

        UCComboZona UCComboZona1 = (UCComboZona)item.FindControl("UCComboZona1");

        if ((!String.IsNullOrEmpty(dr["end_zona"].ToString())) &&
            (Convert.ToByte(dr["end_zona"].ToString()) > 0))
            UCComboZona1._Combo.SelectedValue = dr["end_zona"].ToString();

        ((TextBox)item.FindControl("txtBairro")).Text = dr["end_bairro"].ToString();
        ((HtmlInputHidden)item.FindControl("txtCid_id")).Value = dr["cid_id"].ToString();
        ((TextBox)item.FindControl("txtCidade")).Text = dr["cid_nome"].ToString();

        if (preencherNumero)
        {
            ((TextBox)item.FindControl("txtNumero")).Text = dr["numero"].ToString();
            ((TextBox)item.FindControl("txtComplemento")).Text = dr["complemento"].ToString();
        }
    }

    /// <summary>
    /// Limpa dados da linha informada.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="limpaCEP"></param>
    private void LimpaDadosLinha(RepeaterItem item, bool limpaCEP)
    {
        //((HtmlInputHidden)item.FindControl("txtEnd_id")).Value = "";

        if (limpaCEP)
        {
            ((TextBox)item.FindControl("txtCEP")).Text = "";
            ((TextBox)item.FindControl("txtCEP")).Focus();
        }

        ((UCComboZona)item.FindControl("UCComboZona1"))._Combo.SelectedValue = "-1";
        ((TextBox)item.FindControl("txtLogradouro")).Text = "";
        ((TextBox)item.FindControl("txtDistrito")).Text = "";
        ((TextBox)item.FindControl("txtBairro")).Text = "";
        ((HtmlInputHidden)item.FindControl("txtCid_id")).Value = "";
        ((TextBox)item.FindControl("txtCidade")).Text = "";
        ((TextBox)item.FindControl("txtComplemento")).Text = "";
        ((TextBox)item.FindControl("txtNumero")).Text = "";
    }

    /// <summary>
    /// Habilita/Desabilita os campos comuns do endereço (cidade, distrito, 
    /// bairro, zona).
    /// </summary>
    /// <param name="item"></param>
    /// <param name="habilitado"></param>
    private void HabilitaCamposEndereco(RepeaterItem item, bool habilitado)
    {
        if (habilitado)
        {
            ((TextBox)item.FindControl("txtDistrito")).Attributes.Remove("readOnly1");
            ((TextBox)item.FindControl("txtBairro")).Attributes.Remove("readOnly1");
            ((TextBox)item.FindControl("txtCidade")).Attributes.Remove("readOnly1");
        }
        else
        {
            ((TextBox)item.FindControl("txtDistrito")).Attributes.Add("readOnly1", "readOnly");
            ((TextBox)item.FindControl("txtBairro")).Attributes.Add("readOnly1", "readOnly");
            ((TextBox)item.FindControl("txtCidade")).Attributes.Add("readOnly1", "readOnly");
        }

        ((UCComboZona)item.FindControl("UCComboZona1"))._Combo.Enabled = habilitado;
    }

    /// <summary>
    /// Coloca visible=true no botão Novo da última linha do repeater.
    /// </summary>
    private void SetaBotoes()
    {
        // Só mostra botão de novo e de excluir se não for cadastro único.
        if (!_VS_CadastroUnico)
        {
            btnNovo.Visible = true;
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
        foreach (RepeaterItem ri in rptEndereco.Items)
        {
            if (ri.Visible == true)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Eventos

    protected void txtCEP_TextChanged(object sender, EventArgs e)
    {
        TextBox _txtCEP = (TextBox)sender;
        RepeaterItem item = ((RepeaterItem)_txtCEP.NamingContainer);

        try
        {
            if (!String.IsNullOrEmpty(_txtCEP.Text.Trim()))
            {
                item.FindControl("btnLimparEndereco").Visible = true;

                DataTable dt = END_EnderecoBO.GetSelect(Guid.Empty, Guid.Empty, Guid.Empty,
                        Guid.Empty, _txtCEP.Text, "", "", "", "", "", "", 0, false, 0, 1);

                if (dt.Rows.Count >= 1)
                {
                    // Preenche os dados do endereço.
                    PreencherDados(dt.Rows[0], item, false);

                    HabilitaCamposEndereco(item, false);

                    item.FindControl("txtNumero").Focus();
                }
                else
                {
                    LimpaDadosLinha(item, false);

                    HabilitaCamposEndereco(item, true);

                    item.FindControl("txtLogradouro").Focus();
                }
            }
            else
            {
                item.FindControl("btnLimparEndereco").Visible = false;

                LimpaDadosLinha(item, false);

                HabilitaCamposEndereco(item, true);

                //item.FindControl("txtLogradouro").Focus();
            }
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            ((Label)item.FindControl("lblMessage")).Text =
                UtilBO.GetErroMessage("Erro ao tentar carregar o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void rptEndereco_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) ||
            (e.Item.ItemType == ListItemType.Item))
        {
            if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "excluido")))
            {
                e.Item.Visible = false;
            }
            else
            {
                // Habilita/desabilita máscara dos campos de acordo com o controle.
                TextBox txtCEP = (TextBox)e.Item.FindControl("txtCEP");
                TextBox txtLogradouro = (TextBox)e.Item.FindControl("txtLogradouro");
                TextBox txtNumero = (TextBox)e.Item.FindControl("txtNumero");
                TextBox txtComplemento = (TextBox)e.Item.FindControl("txtComplemento");
                TextBox txtBairro = (TextBox)e.Item.FindControl("txtBairro");
                TextBox txtCidade = (TextBox)e.Item.FindControl("txtCidade");

                if (!String.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "end_id"))))
                {
                    // Deixa os campos como ReadyOnly.
                    HabilitaCamposEndereco(e.Item, false);
                    e.Item.FindControl("btnLimparEndereco").Visible = true;
                }

                Button btnExcluir = (Button)e.Item.FindControl("btnExcluir");
                btnExcluir.Visible = !_VS_CadastroUnico;

                // Inicializando combo de zona.
                UCComboZona UCComboZona1 = (UCComboZona)e.Item.FindControl("UCComboZona1");
                UCComboZona1._EnableValidator = false;

                if ((!String.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "end_zona").ToString())) &&
                    (Convert.ToByte(DataBinder.Eval(e.Item.DataItem, "end_zona").ToString()) > 0))
                {
                    // Selecionar Zona.
                    UCComboZona1._Combo.SelectedValue = DataBinder.Eval(e.Item.DataItem, "end_zona").ToString();
                }

                indice++;
                Panel pnlEndereco = (Panel)e.Item.FindControl("pnlEndereco");
                pnlEndereco.GroupingText = _VS_CadastroUnico ? "" : "Endereço " + (indice);
            }
        }
    }

    protected void btnLimparEndereco_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnLimparEndereco = (ImageButton)sender;
        RepeaterItem item = ((RepeaterItem)btnLimparEndereco.NamingContainer);

        try
        {
            LimpaDadosLinha(item, true);

            HabilitaCamposEndereco(item, true);

            //item.FindControl("txtLogradouro").Focus();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            ((Label)item.FindControl("lblMessage")).Text =
                UtilBO.GetErroMessage("Erro ao tentar limpar os campos do endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
      try
        {
            NovoEndereco();

            if (rptEndereco.Visible == false)
                rptEndereco.Visible = true;

            rptEndereco.Items[rptEndereco.Items.Count - 1].FindControl("txtCEP").Focus();
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

            rptEndereco.Visible = rptEndereco.Items.Count > 0 && VerificaItemVisivel();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            ((Label)item.FindControl("lblMessage")).Text =
                UtilBO.GetErroMessage("Erro ao tentar excluir o endereço.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion
}