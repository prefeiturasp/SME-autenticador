using System;
using System.Web.UI.WebControls;
using Autenticador.BLL;

namespace Autenticador.UserControlLibrary.Buscas
{
    public partial class UCCidade : Abstract_UCBusca
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fdsResultados.Visible = false;

                UCComboPais1._EnableValidator = false;
                UCComboPais1._ShowSelectMessage = true;
                UCComboPais1._Load(0);

                UCComboUnidadeFederativa1._EnableValidator = false;
                UCComboUnidadeFederativa1._ShowSelectMessage = true;
                UCComboUnidadeFederativa1._Load(Guid.Empty, 0);
                UCComboUnidadeFederativa1._Combo.Enabled = false;

            }

            UCComboPais1.OnSelectedIndexChange = UCComboPais1_OnSelectedIndexChange1;
        }

        void UCComboPais1_OnSelectedIndexChange1(object sender, EventArgs e)
        {
            try
            {
                string parametro = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL);

                if (UCComboPais1._Combo.SelectedValue == parametro)
                    UCComboUnidadeFederativa1._Combo.Enabled = true;
                else
                {
                    UCComboUnidadeFederativa1._Combo.Enabled = false;
                    UCComboUnidadeFederativa1._Combo.SelectedValue = Guid.Empty.ToString();
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

        public void _Limpar()
        {
            UCComboPais1._Combo.SelectedValue = Guid.Empty.ToString();
            UCComboUnidadeFederativa1._Combo.SelectedValue = Guid.Empty.ToString();
            UCComboUnidadeFederativa1._Combo.Enabled = false;
            _txtCidade.Text = string.Empty;
            fdsResultados.Visible = false;

            Page.Form.DefaultFocus = UCComboPais1._Combo.ClientID;
            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
        }

        private void _Pesquisar()
        {
            try
            {
                _grvCidade.PageIndex = 0;
                odsCidade.SelectParameters.Clear();
                odsCidade.SelectParameters.Add("cid_id", Guid.Empty.ToString());
                odsCidade.SelectParameters.Add("pai_id", UCComboPais1._Combo.SelectedValue);
                odsCidade.SelectParameters.Add("unf_id", UCComboUnidadeFederativa1._Combo.SelectedValue);
                odsCidade.SelectParameters.Add("cid_nome", _txtCidade.Text);
                odsCidade.SelectParameters.Add("unf_sigla", string.Empty);
                odsCidade.SelectParameters.Add("unf_nome", string.Empty);
                odsCidade.SelectParameters.Add("pai_nome", string.Empty);
                odsCidade.SelectParameters.Add("cid_situacao", "0");
                odsCidade.SelectParameters.Add("paginado", "true");
                odsCidade.DataBind();
                _grvCidade.DataBind();
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

        protected void _grvCidade_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                SetReturns(_grvCidade.DataKeyNames[0], _grvCidade.DataKeys[e.NewEditIndex].Values[0]);
                SetReturns(_grvCidade.DataKeyNames[1], _grvCidade.DataKeys[e.NewEditIndex].Values[1]);
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

        protected void odsCidade_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            if (e.ExecutingSelectCount)
                e.InputParameters.Clear();
        }
    }
}