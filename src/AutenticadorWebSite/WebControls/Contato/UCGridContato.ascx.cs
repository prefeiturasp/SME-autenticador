using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.Web.WebProject;
using System.Data;
using System.Linq;
using Autenticador.Entities;
using Autenticador.BLL;
using System.Text.RegularExpressions;
using CoreLibrary.Validation.Exceptions;

public partial class WebControls_Contato_UCGridContato : MotherUserControl
{
    #region PROPRIEDADES

    /// <summary>
    /// ViewState com datatable de contatos
    /// Retorno e atribui valores para o DataTable de contatos
    /// </summary>
    public DataTable _VS_contatos
    {
        get
        {
            if (ViewState["_VS_contatos"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("tmc_id");
                dt.Columns.Add("tmc_nome");
                dt.Columns.Add("contato");
                ViewState["_VS_contatos"] = dt;
            }
            return (DataTable)ViewState["_VS_contatos"];
        }
        set
        {
            ViewState["_VS_contatos"] = value;
        }
    }

    /// <summary>
    /// Indica se é contato com variavel Guid ou com variavel Int
    /// </summary>
    public bool _VS_GuidSeq
    {
        get
        {
            if (ViewState["_VS_GuidSeq"] != null)
                return Convert.ToBoolean(ViewState["_VS_GuidSeq"]);
            return true;
        }
        set
        {
            ViewState["_VS_GuidSeq"] = value;
        }
    }

    /// <summary>
    /// Indica se permit mais de um contato com o mesmo tipo
    /// </summary>
    public bool _VS_TipoRepetido
    {
        get
        {
            if (ViewState["_VS_TipoRepetido"] != null)
                return Convert.ToBoolean(ViewState["_VS_TipoRepetido"]);
            return false;
        }
        set
        {
            ViewState["_VS_TipoRepetido"] = value;
        }
    }

    /// <summary>
    /// Armazena o sequencial para inclusão no DataTable quando for do tipo int
    /// </summary>
    public int _VS_seq
    {
        get
        {
            if (ViewState["_VS_seq"] != null)
                return Convert.ToInt32(ViewState["_VS_seq"]);
            return -1;
        }
        set
        {
            ViewState["_VS_seq"] = value;
        }
    }

    /// <summary>
    /// Retorna e atribui valores para o grid.
    /// </summary>
    public GridView _grvContatos
    {
        get
        {
            return _grvContato;
        }
        set
        {
            _grvContato = value;
        }
    }

    /// <summary>
    /// Retorna e atribui valores para o update panel.
    /// </summary>
    public UpdatePanel _updGridContatos
    {
        get
        {
            return updGridContatos;
        }
        set
        {
            updGridContatos = value;
        }
    }

    /// <summary>
    /// Retorna e atribui valores para o label de mensagens de erro
    /// </summary>
    public Label _MensagemErro
    {
        get { return this._lblMessage; }
        set { this._lblMessage = value; }
    }

    #endregion

    #region METODOS

    /// <summary>
    /// Atribui par o View State _VS_TipoRepetido a indicação da configuração sobre
    /// cadastro de contato com o mesmo tipo.
    /// </summary>
    /// <param name="permiteTipoRepetido"></param>
    public void InicializarContato(bool permiteTipoRepetido)
    {
        _VS_TipoRepetido = permiteTipoRepetido;
    }

    /// <summary>
    /// Método para carregar dados no grid
    /// </summary>
    public void _CarregarContato()
    {
        _grvContato.DataSource = _VS_contatos;
        _grvContato.DataBind();
        updGridContatos.Update();
    }

    /// <summary>
    /// Método para validar os campos a serem inseridos
    /// </summary>
    /// <param name="row">Linha a ser validada</param>
    /// <returns></returns>
    private bool ValidarLinhasGrid(GridViewRow row, out string msgErro)
    {
        try
        {
            DropDownList ddlTipoMeioContato = (DropDownList)row.FindControl("_ddlTipoMeioContato");
            TextBox contato = (TextBox)row.FindControl("tbContato");

            #region Contato do mesmo tipo
            if (!_VS_TipoRepetido)
            {
                var x = from GridViewRow gvr in _grvContato.Rows
                        where
                            (((DropDownList)gvr.FindControl("_ddlTipoMeioContato")).SelectedValue.Equals(ddlTipoMeioContato.SelectedValue, StringComparison.OrdinalIgnoreCase))
                            && ((TextBox)gvr.FindControl("tbContato")).Text.Trim().Length > 0

                        select gvr;

                if (x.Count() > 1)
                {
                    _lblMessage.Visible = true;
                    msgErro = UtilBO.GetErroMessage("Existem contatos cadastrados com mesmo tipo.", UtilBO.TipoMensagem.Alerta);
                    return false;
                }
            }
            #endregion

            Regex regex;
            Guid tmc_id = new Guid(ddlTipoMeioContato.SelectedValue);

            SYS_TipoMeioContato tmc = new SYS_TipoMeioContato() { tmc_id = tmc_id };
            SYS_TipoMeioContatoBO.GetEntity(tmc);

            if (tmc.tmc_validacao == (byte)Autenticador.Entities.SYS_TipoMeioContatoValidacao.Email)
            {
                regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.None);
                if (!regex.IsMatch(contato.Text))
                {
                    _lblMessage.Visible = true;
                    msgErro = UtilBO.GetErroMessage("Contato está fora do padrão ( seuEmail@seuProvedor ).", UtilBO.TipoMensagem.Alerta);
                    return false;
                }
            }
            else
                if (tmc.tmc_validacao == (byte)Autenticador.Entities.SYS_TipoMeioContatoValidacao.Telefone)
            {
                regex = new Regex(@"^(\(\d{2}\))?[\s]?\d{3,4}-?\d{4}$", RegexOptions.None);
                if (!regex.IsMatch(contato.Text))
                {
                    _lblMessage.Visible = true;
                    msgErro = UtilBO.GetErroMessage("Contato está fora do padrão ( (XX) XXX-XXXX ou (XX) XXXX-XXXX ou (XX) XXXXXXX ou (XX) XXXXXXXX ou XXXX-XXXX ou XXXXXXXX).", UtilBO.TipoMensagem.Alerta);
                    return false;
                }
            }
            else
                    if (tmc.tmc_validacao == (byte)Autenticador.Entities.SYS_TipoMeioContatoValidacao.WebSite)
            {
                regex = new Regex(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$", RegexOptions.None);
                if (!regex.IsMatch(contato.Text))
                {
                    _lblMessage.Visible = true;
                    msgErro = UtilBO.GetErroMessage("Contato está fora do padrão (http(s)://seuSite.dominio ou http(s)://www.seuSite.dominio).", UtilBO.TipoMensagem.Alerta);
                    return false;
                }
            }

            msgErro = "";
            _lblMessage.Visible = false;
            return true;
        }
        catch (Exception ex)
        {
            this._lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
            ApplicationWEB._GravaErro(ex);
            msgErro = "";
            return false;
        }
    }

    /// <summary>
    /// Adiciona uma nova linha ao grid
    /// </summary>
    private void AdicionaLinhaGrid()
    {
        // adiciona nova linha do grid
        DataRow dr = _VS_contatos.NewRow();
        dr["id"] = Guid.NewGuid();
        _VS_contatos.Rows.Add(dr);

        // mostra nova linha
        _CarregarContato();

        // mostra botão
        _grvContato.Rows[_grvContato.Rows.Count - 1].FindControl("btnAdicionar").Visible = true;
    }

    /// <summary>
    /// Verifica se a linha cujo indíce é passado por parâmetro está preenchida
    /// </summary>
    /// <param name="index">indíce da linha a ser verificada</param>
    /// <returns></returns>
    private bool VerificaLinhaPreenchida(int index)
    {
        GridViewRow linha = _grvContato.Rows[index];

        if (((DropDownList)linha.FindControl("_ddlTipoMeioContato")).SelectedValue != (-1).ToString() && !String.IsNullOrEmpty(((TextBox)linha.FindControl("tbContato")).Text.Trim()))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Salva o conteúdo do grid no ViewState
    /// </summary>
    public bool SalvaConteudoGrid(out string msgErro)
    {
        string mensagemErro = "";

        foreach (GridViewRow linha in _grvContato.Rows)
        {
            string id = _grvContato.DataKeys[linha.RowIndex].Value.ToString();
            int index = RetornaIndice(id);

            if (VerificaLinhaPreenchida(linha.RowIndex))
            {
                if (ValidarLinhasGrid(linha, out mensagemErro))
                {
                    _VS_contatos.Rows[index]["tmc_id"] = ((DropDownList)linha.FindControl("_ddlTipoMeioContato")).SelectedValue;
                    _VS_contatos.Rows[index]["tmc_nome"] = ((DropDownList)linha.FindControl("_ddlTipoMeioContato")).SelectedItem;
                    _VS_contatos.Rows[index]["contato"] = ((TextBox)linha.FindControl("tbContato")).Text;
                }
                else
                {
                    msgErro = mensagemErro;
                    return false;
                }
            }
            else
            {
                if (index != -1)
                    _VS_contatos.Rows[index].Delete();
            }
        }

        msgErro = mensagemErro;
        return true;
    }

    /// <summary>
    /// Salva o conteúdo do grid no ViewState, sem validação
    /// </summary>
    private void AtualizaViewState()
    {
        foreach (GridViewRow linha in _grvContato.Rows)
        {
            string id = _grvContato.DataKeys[linha.RowIndex].Value.ToString();
            int index = RetornaIndice(id);

            if (index != -1)
            {
                _VS_contatos.Rows[index]["tmc_id"] = ((DropDownList)linha.FindControl("_ddlTipoMeioContato")).SelectedValue;
                _VS_contatos.Rows[index]["tmc_nome"] = ((DropDownList)linha.FindControl("_ddlTipoMeioContato")).SelectedItem;
                _VS_contatos.Rows[index]["contato"] = ((TextBox)linha.FindControl("tbContato")).Text;
            }
        }
    }

    /// <summary>
    /// Retorna o indice na tabela do viewstate pelo id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private int RetornaIndice(string id)
    {
        int indice = -1;

        var x = from DataRow dr in _VS_contatos.Rows
                where
                    (dr.RowState != DataRowState.Deleted) &&
                    (dr["id"].ToString().Equals(id, StringComparison.OrdinalIgnoreCase))

                select _VS_contatos.Rows.IndexOf(dr);

        if (x.Count() > 0)
        {
            indice = x.First();
        }

        return indice;
    }

    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_grvContato.Rows.Count == 0)
        {
            AdicionaLinhaGrid();
        }

        // mostra botão
        _grvContato.Rows[_grvContato.Rows.Count - 1].FindControl("btnAdicionar").Visible = true;
    }

    protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
    {
        AtualizaViewState();
        AdicionaLinhaGrid();
    }

    protected void _grvContato_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string id = _grvContato.DataKeys[e.Row.RowIndex].Value.ToString();
            int index = RetornaIndice(id);

            // index == -1: não encontrou linha com esse id ou a linha está deletada
            if (index != -1)
            {
                ((DropDownList)e.Row.FindControl("_ddlTipoMeioContato")).SelectedValue = Convert.ToString(_VS_contatos.Rows[e.Row.RowIndex]["tmc_id"]);
            }
        }

    }

    #endregion
}
