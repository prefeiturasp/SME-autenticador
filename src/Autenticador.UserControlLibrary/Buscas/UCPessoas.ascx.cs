using System;
using System.Web.UI.WebControls;
using Autenticador.BLL;
using Autenticador.Entities;

namespace Autenticador.UserControlLibrary.Buscas
{
    public partial class UCPessoas : Abstract_UCBusca
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
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
                catch (Exception err)
                {
                    if (SetLogErro != null)
                        SetLogErro(err);
                    else
                        throw;
                }
            }
        }

        public void _Limpar()
        {
            _txtNome.Text = string.Empty;
            _txtCPF.Text = string.Empty;
            _txtRG.Text = string.Empty;
            fdsResultados.Visible = false;

            Page.Form.DefaultFocus = _txtNome.ClientID;
            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
        }

        private void _Pesquisar()
        {
            try
            {
                _dgvPessoas.PageIndex = 0;
                odsPessoas.SelectParameters.Clear();
                odsPessoas.SelectParameters.Add("nome", _txtNome.Text);
                odsPessoas.SelectParameters.Add("cpf", _txtCPF.Text);
                odsPessoas.SelectParameters.Add("rg", _txtRG.Text);
                odsPessoas.SelectParameters.Add("data", new DateTime().ToString());
                odsPessoas.DataBind();
                _dgvPessoas.DataBind();
            }
            catch (Exception err)
            {
                if (SetLogErro != null)
                    SetLogErro(err);
                else
                    throw;
            }
        }

        protected void _btnPesquisar_Click(object sender, EventArgs e)
        {
            fdsResultados.Visible = true;
            _Pesquisar();
        }

        protected void odsPessoas_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (e.ExecutingSelectCount)
                e.InputParameters.Clear();
        }

        protected void _dgvPessoas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                SetReturns(_dgvPessoas.DataKeyNames[0], _dgvPessoas.DataKeys[e.NewEditIndex].Values[0]);
                SetReturns(_dgvPessoas.DataKeyNames[1], _dgvPessoas.DataKeys[e.NewEditIndex].Values[1]);
                if (ReturnValues != null)
                    ReturnValues(Returns);
                else
                    throw new NotImplementedException();
                if (!String.IsNullOrEmpty(ContainerName))
                    Close();
            }
            catch (Exception err)
            {
                if (SetLogErro != null)
                    SetLogErro(err);
                else
                    throw;
            }
            finally
            {
                e.Cancel = true;
            }
        }
    }
}