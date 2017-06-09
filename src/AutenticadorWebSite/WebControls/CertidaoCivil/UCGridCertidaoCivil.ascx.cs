using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using System.Data;
using System.Linq;
using Autenticador.BLL;
using System.Web.UI.HtmlControls;
using Autenticador.Entities;

public partial class WebControls_CertidaoCivil_UCGridCertidaoCivil : MotherUserControl
{
    #region PROPRIEDADES

    /// <summary>
    /// Retorna e atribui valores para o update panel.
    /// </summary>
    public UpdatePanel _updGridCertidaoCivil
    {
        get
        {
            return updGridCertidaoCivil;
        }
        set
        {
            updGridCertidaoCivil = value;
        }
    }

    /// <summary>
    /// ViewState com datatable de certidoes
    /// Retorno e atribui valores para o DataTable de certidoes
    /// </summary>
    public DataTable _VS_certidoes
    {
        get
        {
            if (ViewState["_VS_certidoes"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ctc_id");
                dt.Columns.Add("ctc_tipo");
                dt.Columns.Add("ctc_tipoDescricao");
                dt.Columns.Add("ctc_numeroTermo");
                dt.Columns.Add("ctc_folha");
                dt.Columns.Add("ctc_livro");
                dt.Columns.Add("ctc_dataEmissao");
                dt.Columns.Add("ctc_nomeCartorio");
                dt.Columns.Add("ctc_distritoCartorio");
                dt.Columns.Add("cid_idCartorio");
                dt.Columns.Add("cid_nomeCartorio");
                dt.Columns.Add("unf_idCartorio");
                dt.Columns.Add("unf_nome");
                dt.Columns.Add("unf_idAntigo");
                dt.Columns.Add("ctc_matricula");
                dt.Columns.Add("ctc_gemeo", typeof(bool)).DefaultValue = false;
                dt.Columns.Add("ctc_modeloNovo", typeof(bool)).DefaultValue = false;

                ViewState["_VS_certidoes"] = dt;
            }
            return (DataTable)ViewState["_VS_certidoes"];
        }
        set
        {
            ViewState["_VS_certidoes"] = value;
        }
    }

    /// <summary>
    /// ViewState com ctc_id
    /// </summary>
    public Guid _VS_ctc_id
    {
        get
        {
            return (Guid)ViewState["_VS_ctc_id"];
        }
        set
        {
            ViewState["_VS_ctc_id"] = value;
        }
    }

    private string legenda;

    #endregion

    #region METODOS

    /// <summary>
    /// Método para carregar dados no grid
    /// </summary>
    public void _CarregarCertidaoCivil()
    {
        IniciaRepeater();
        rptCertidaoCivil.DataSource = _VS_certidoes;
        rptCertidaoCivil.DataBind();
        _updGridCertidaoCivil.Update();
    }

    /// <summary>
    /// Retorna o indice na tabela do viewstate pelo id.
    /// </summary>
    /// <param name="ctc_id"></param>
    /// <returns></returns>
    private int RetornaIndice(string ctc_id)
    {
        int indice = -1;

        var x = from DataRow dr in _VS_certidoes.Rows
                where
                    (dr.RowState != DataRowState.Deleted) &&
                    (dr["ctc_id"].ToString().Equals(ctc_id, StringComparison.OrdinalIgnoreCase))

                select _VS_certidoes.Rows.IndexOf(dr);

        if (x.Count() > 0)
        {
            indice = x.First();
        }

        return indice;
    }

    /// <summary>
    /// Inicia o Repeater com dois registros vazios
    /// </summary>
    private void IniciaRepeater()
    {
        if (_VS_certidoes.Rows.Count == 0)
        {
            DataRow dr = _VS_certidoes.NewRow();
            DataRow dr2 = _VS_certidoes.NewRow();

            dr["ctc_id"] = Guid.NewGuid();
            _VS_certidoes.Rows.Add(dr);

            dr2["ctc_id"] = Guid.NewGuid();
            _VS_certidoes.Rows.Add(dr2);

        }
        else
        {
            DataRow dr = _VS_certidoes.NewRow();
            dr["ctc_id"] = Guid.NewGuid();
            dr["ctc_gemeo"] = false;
            dr["ctc_modeloNovo"] = false;

            if (_VS_certidoes.Rows.Count == 1)
            {
                if (_VS_certidoes.Rows[0]["ctc_tipoDescricao"].ToString().Equals("Certidão de nascimento"))
                {
                    _VS_certidoes.Rows.Add(dr);
                }
                else
                    _VS_certidoes.Rows.InsertAt(dr, 0);
            }           
        }
    }

    /// <summary>
    /// Atualiza o Viewstate
    /// </summary>
    /// <param name="mensagemErro"></param>
    /// <returns></returns>
    public bool AtualizaViewState(out string mensagemErro)
    {
        string msgErro = "";

        foreach (RepeaterItem item in rptCertidaoCivil.Items)
        {
            string ctc_id = ((HtmlInputHidden)(item.FindControl("ctc_id"))).Value;
            int index = RetornaIndice(ctc_id);

            if (!VerificaItemVazio(item))
            {
                if (ValidaItem(item, out msgErro))
                {
                    _VS_certidoes.Rows[index]["ctc_tipo"] = ((Panel)item.FindControl("pnlContato")).GroupingText == "Certidão de nascimento" ? 1 : 2;
                    _VS_certidoes.Rows[index]["ctc_tipoDescricao"] = ((Panel)item.FindControl("pnlContato")).GroupingText;
                    _VS_certidoes.Rows[index]["ctc_numeroTermo"] = ((TextBox)(item.FindControl("tbNumTerm"))).Text;
                    _VS_certidoes.Rows[index]["ctc_folha"] = ((TextBox)(item.FindControl("tbFolha"))).Text;
                    _VS_certidoes.Rows[index]["ctc_livro"] = ((TextBox)(item.FindControl("tbLivro"))).Text;
                    _VS_certidoes.Rows[index]["ctc_dataEmissao"] = ((TextBox)(item.FindControl("tbDtEmissao"))).Text;
                    _VS_certidoes.Rows[index]["ctc_nomeCartorio"] = ((TextBox)(item.FindControl("tbNomeCart"))).Text;
                    _VS_certidoes.Rows[index]["ctc_distritoCartorio"] = ((TextBox)(item.FindControl("tbDistritoCart"))).Text;
                    _VS_certidoes.Rows[index]["cid_idCartorio"] = string.IsNullOrEmpty(((HtmlInputHidden)(item.FindControl("tbCid_idCertidao"))).Value) ? 
                        Guid.Empty : new Guid(((HtmlInputHidden)(item.FindControl("tbCid_idCertidao"))).Value);

                    _VS_certidoes.Rows[index]["cid_nomeCartorio"] = ((TextBox)(item.FindControl("tbCidadeCart"))).Text;

                    _VS_certidoes.Rows[index]["unf_idCartorio"] = !(String.IsNullOrEmpty(((DropDownList)item.FindControl("ddlUF")).SelectedValue)) ?
                        ((DropDownList)item.FindControl("ddlUF")).SelectedValue : Convert.ToString(Guid.Empty);

                    _VS_certidoes.Rows[index]["unf_nome"] = !(String.IsNullOrEmpty(((DropDownList)item.FindControl("ddlUF")).SelectedValue)) ?
                       ((DropDownList)item.FindControl("ddlUF")).SelectedItem.Text : string.Empty;

                    _VS_certidoes.Rows[index]["ctc_matricula"] = ((TextBox)(item.FindControl("txtMatricula"))).Text;

                    if (((Panel)item.FindControl("pnlContato")).GroupingText == "Certidão de casamento")
                    {
                        _VS_certidoes.Rows[index]["ctc_gemeo"] = false;

                        _VS_certidoes.Rows[index]["ctc_modeloNovo"] = false;
                    }
                    else
                    {
                        _VS_certidoes.Rows[index]["ctc_gemeo"] = ((CheckBox)(item.FindControl("chkGemeo"))).Checked;

                        _VS_certidoes.Rows[index]["ctc_modeloNovo"] = ((CheckBox)(item.FindControl("chkModeloNovo"))).Checked;
                    }
                }
                else
                {
                    mensagemErro = msgErro;
                    return false;
                }
            }
            else
                if (index != -1)
                    _VS_certidoes.Rows[index].Delete();
        }

        mensagemErro = msgErro;
        return true;
    }

    /// <summary>
    /// Verifica se o item passado por parametro está vazio
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool VerificaItemVazio(RepeaterItem item)
    {
        string s =
        ((TextBox)(item.FindControl("tbNumTerm"))).Text +
        ((TextBox)(item.FindControl("tbFolha"))).Text +
        ((TextBox)(item.FindControl("tbLivro"))).Text +
        ((TextBox)(item.FindControl("tbDtEmissao"))).Text +
        ((TextBox)(item.FindControl("tbNomeCart"))).Text +
        ((TextBox)(item.FindControl("tbDistritoCart"))).Text +
        ((TextBox)(item.FindControl("tbCidadeCart"))).Text +
        ((TextBox)(item.FindControl("txtMatricula"))).Text;

        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Valida os dados do item passado por parametro
    /// </summary>
    /// <param name="item"></param>
    /// <param name="msgErro"></param>
    /// <returns></returns>
    private bool ValidaItem(RepeaterItem item, out string msgErro)
    {
        if (((TextBox)item.FindControl("tbDtEmissao")).Text.Trim().Length > 0)
        {
            DateTime dt;
            if (!DateTime.TryParse(((TextBox)item.FindControl("tbDtEmissao")).Text, out dt))
            {
                msgErro = UtilBO.GetErroMessage("Data de emissão inválida.", UtilBO.TipoMensagem.Alerta);
                return false;
            }
        }

        msgErro = "";
        return true;
    }

    #endregion

    #region EVENTOS

    protected void btnLimparCertidao_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnLimparEndereco = (ImageButton)sender;
        RepeaterItem item = (RepeaterItem)btnLimparEndereco.NamingContainer;

        ((TextBox)(item.FindControl("txtMatricula"))).Text =
        ((TextBox)(item.FindControl("tbNumTerm"))).Text =
        ((TextBox)(item.FindControl("tbFolha"))).Text =
        ((TextBox)(item.FindControl("tbLivro"))).Text =
        ((TextBox)(item.FindControl("tbDtEmissao"))).Text =
        ((TextBox)(item.FindControl("tbNomeCart"))).Text =
        ((TextBox)(item.FindControl("tbDistritoCart"))).Text =
        ((TextBox)(item.FindControl("tbCidadeCart"))).Text =
        ((DropDownList)item.FindControl("ddlUF")).SelectedValue = "";
        ((CheckBox)(item.FindControl("chkGemeo"))).Checked = 
        ((CheckBox)(item.FindControl("chkModeloNovo"))).Checked = false;
    }

    protected void rptCertidaoCivil_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.AlternatingItem) ||
            (e.Item.ItemType == ListItemType.Item))
        {
            if (((Panel)e.Item.FindControl("pnlContato")).GroupingText.Trim().Length > 0)
            {
                legenda = ((Panel)e.Item.FindControl("pnlContato")).GroupingText;

                if (legenda.Equals("Certidão de casamento"))
                {
                    ((CheckBox)e.Item.FindControl("chkGemeo")).Visible = false;
                    ((CheckBox)e.Item.FindControl("chkModeloNovo")).Visible = false;
                }
            }
            else
            {
                ((Panel)e.Item.FindControl("pnlContato")).GroupingText = legenda == "Certidão de nascimento" ? "Certidão de casamento" : "Certidão de nascimento";
                legenda = ((Panel)e.Item.FindControl("pnlContato")).GroupingText;

                if (legenda.Equals("Certidão de casamento"))
                {
                    ((CheckBox)e.Item.FindControl("chkGemeo")).Visible = false;
                    ((CheckBox)e.Item.FindControl("chkModeloNovo")).Visible = false;
                }
            }

            ((DropDownList)e.Item.FindControl("ddlUF")).SelectedValue = Convert.ToString(_VS_certidoes.Rows[e.Item.ItemIndex]["unf_idCartorio"]);
        }
    }

    #endregion
}
