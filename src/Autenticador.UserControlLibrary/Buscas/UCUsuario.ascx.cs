using System;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Buscas
{
    public partial class UCUsuario : Abstract_UCBusca
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool exibeComboEntidade = true;
            if (param.ContainsKey("ent_idVisible"))
                exibeComboEntidade = Convert.ToBoolean(param["ent_idVisible"].ToString());
            UCComboEntidade1.Visible = exibeComboEntidade;

            if (!IsPostBack)
            {
                fdsResultados.Visible = false;

                try
                {
                    UCComboEntidade1.Inicialize("Entidade");
                    UCComboEntidade1._Load(Guid.Empty, 1);
                    UCComboEntidade1._EnableValidator = false;
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
            UCComboEntidade1._Combo.SelectedValue = Guid.Empty.ToString();
            _txtLogin.Text = string.Empty;            
            _txtPessoa.Text = string.Empty;
            fdsResultados.Visible = false;

            Page.Form.DefaultFocus = UCComboEntidade1._Combo.ClientID;
            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
        }

        private void _Pesquisar()
        {
            try
            {
                _dgvUsuario.PageIndex = 0;
                odsUsuarios.SelectParameters.Clear();

                bool exibeComboEntidade = true;
                if (param.ContainsKey("ent_idVisible"))
                    exibeComboEntidade = Convert.ToBoolean(param["ent_idVisible"].ToString());
                if (!exibeComboEntidade && !string.IsNullOrEmpty(param["ent_id"].ToString()))
                    odsUsuarios.SelectParameters.Add("ent_id", param["ent_id"].ToString());
                else
                    odsUsuarios.SelectParameters.Add("ent_id", UCComboEntidade1._Combo.SelectedValue);
                                
                odsUsuarios.SelectParameters.Add("login", _txtLogin.Text);                                
                odsUsuarios.SelectParameters.Add("pessoa", _txtPessoa.Text);
                odsUsuarios.DataBind();
                _dgvUsuario.DataBind();
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

        protected void _dgvUsuario_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                SetReturns(_dgvUsuario.DataKeyNames[0], _dgvUsuario.DataKeys[e.NewEditIndex].Values[0]);
                SetReturns(_dgvUsuario.DataKeyNames[1], _dgvUsuario.DataKeys[e.NewEditIndex].Values[1]);
                SetReturns(_dgvUsuario.DataKeyNames[2], _dgvUsuario.DataKeys[e.NewEditIndex].Values[2]);
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

        protected void odsUsuarios_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            if (e.ExecutingSelectCount)
                e.InputParameters.Clear();
        }
    }
}