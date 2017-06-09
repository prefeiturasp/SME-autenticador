using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Web.WebProject;
using Autenticador.Entities;

public partial class Busca_Pessoas : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this._dgvPessoas.PageSize = ApplicationWEB._Paginacao;
            fdsResultados.Visible = false;

            Guid tdo_id;
            SYS_TipoDocumentacao tdo = new SYS_TipoDocumentacao();

            string tipoDocCPF = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_CPF);
            if (!string.IsNullOrEmpty(tipoDocCPF))
            {
                tdo_id = new Guid(tipoDocCPF);
                tdo.tdo_id = tdo_id;
                SYS_TipoDocumentacaoBO.GetEntity(tdo);
                _lblCPF.Text = tdo.tdo_sigla;
                _dgvPessoas.Columns[3].HeaderText = tdo.tdo_sigla;
            }
            else
            {
                _lblCPF.Text = string.Empty;
                _lblCPF.Visible = false;
                _txtCPF.Visible = false;
                _dgvPessoas.Columns[3].HeaderText = string.Empty;
                _dgvPessoas.Columns[3].HeaderStyle.CssClass = "hide";
                _dgvPessoas.Columns[3].ItemStyle.CssClass = "hide";
            }

            string tipoDocRG = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_RG);
            if (!string.IsNullOrEmpty(tipoDocRG))
            {
                tdo_id = new Guid(tipoDocRG);
                tdo.tdo_id = tdo_id;
                SYS_TipoDocumentacaoBO.GetEntity(tdo);
                _lblRG.Text = tdo.tdo_sigla;
                _dgvPessoas.Columns[4].HeaderText = tdo.tdo_sigla;
            }
            else
            {
                _lblRG.Text = string.Empty;
                _lblRG.Visible = false;
                _txtRG.Visible = false;
                _dgvPessoas.Columns[4].HeaderText = string.Empty;
                _dgvPessoas.Columns[4].HeaderStyle.CssClass = "hide";
                _dgvPessoas.Columns[4].ItemStyle.CssClass = "hide";
            }
        }
    }

    protected void _btnPesquisar_Click(object sender, EventArgs e)
    {
        this._dgvPessoas.PageIndex = 0;
        odsPessoas.DataBind();
        fdsResultados.Visible = true;
    }
    protected void odsPessoas_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }
    protected void _dgvPessoas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //Retorna o valor selecionado pelo usuário
        UtilBO.SetScriptRetornoBusca(Page, Request["buscaID"], new string[] { this._dgvPessoas.DataKeys[e.NewEditIndex].Values[0].ToString(), this._dgvPessoas.DataKeys[e.NewEditIndex].Values[1].ToString() });
        e.Cancel = true;
    }
}
