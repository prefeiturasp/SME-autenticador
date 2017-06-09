using System;
using Autenticador.BLL;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Buscas
{
    public partial class UCUA : Abstract_UCBusca
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool exibeComboEntidade = true;
            if (param.ContainsKey("ent_idVisible"))
                exibeComboEntidade = Convert.ToBoolean(param["ent_idVisible"].ToString());
            UCComboEntidade1.Visible = exibeComboEntidade;

            bool exibeComboTipoUA = true;
            if (param.ContainsKey("tua_idVisible"))
                exibeComboTipoUA = Convert.ToBoolean(param["tua_idVisible"].ToString());
            UCComboTipoUnidadeAdministrativa1.Visible = exibeComboTipoUA;

            if (param.ContainsKey("gru_id") && param.ContainsKey("vis_id") && param.ContainsKey("usu_id"))
            {
                int vis_id = Convert.ToInt32(param["vis_id"].ToString());

                if (vis_id != SysVisaoID.Administracao)
                {
                    odsUA.SelectMethod = "GetSelectBy_PermissaoUsuario";
                }
            }

            if (!IsPostBack)
            {
                try
                {
                    fdsResultados.Visible = false;

                    UCComboEntidade1._EnableValidator = false;
                    UCComboEntidade1._ShowSelectMessage = true;
                    UCComboEntidade1._Load(Guid.Empty, 0);

                    UCComboTipoUnidadeAdministrativa1._EnableValidator = false;
                    UCComboTipoUnidadeAdministrativa1._ShowSelectMessage = true;
                    UCComboTipoUnidadeAdministrativa1._Load(Guid.Empty, 0);
                }
                catch (Exception err)
                {
                    if (SetLogErro != null)
                        SetLogErro(err);
                    else
                        throw;
                }
            }

            if (exibeComboEntidade)
                Page.Form.DefaultFocus = UCComboEntidade1._Combo.ClientID;
            else if (exibeComboTipoUA)
                Page.Form.DefaultFocus = UCComboTipoUnidadeAdministrativa1._Combo.ClientID;
            else
                Page.Form.DefaultFocus = _txtNome.ClientID;

            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
        }

        public void _Limpar()
        {
            UCComboEntidade1._Combo.SelectedValue = Guid.Empty.ToString();
            UCComboTipoUnidadeAdministrativa1._Combo.SelectedValue = Guid.Empty.ToString();
            _txtNome.Text = string.Empty;
            _txtCodigo.Text = string.Empty;
            fdsResultados.Visible = false;
        }

        private void _Pesquisar()
        {
            try
            {
                _dgvUA.PageIndex = 0;
                odsUA.SelectParameters.Clear();

                string gru_id, usu_id;
                int vis_id;

                gru_id = usu_id = Guid.Empty.ToString();

                // Filtra a busca de ua conforme a visão do usuário.
                if (param.ContainsKey("gru_id") && param.ContainsKey("vis_id") && param.ContainsKey("usu_id"))
                {
                    vis_id = Convert.ToInt32(param["vis_id"].ToString());

                    if (vis_id != SysVisaoID.Administracao)
                    {
                        odsUA.SelectMethod = "GetSelectBy_PermissaoUsuario";

                        gru_id = param["gru_id"].ToString();
                        usu_id = param["usu_id"].ToString();
                    }
                }

                odsUA.SelectParameters.Add("gru_id", gru_id);
                odsUA.SelectParameters.Add("usu_id", usu_id);

                bool exibeComboTipoUA = true;
                if (param.ContainsKey("tua_idVisible"))
                    exibeComboTipoUA = Convert.ToBoolean(param["tua_idVisible"].ToString());
                if (!exibeComboTipoUA && !string.IsNullOrEmpty(param["tua_id"].ToString()))
                    odsUA.SelectParameters.Add("tua_id", param["tua_id"].ToString());
                else
                    odsUA.SelectParameters.Add("tua_id", UCComboTipoUnidadeAdministrativa1._Combo.SelectedValue);

                bool exibeComboEntidade = true;
                if (param.ContainsKey("ent_idVisible"))
                    exibeComboEntidade = Convert.ToBoolean(param["ent_idVisible"].ToString());
                if (!exibeComboEntidade && !string.IsNullOrEmpty(param["ent_id"].ToString()))
                    odsUA.SelectParameters.Add("ent_id", param["ent_id"].ToString());
                else
                    odsUA.SelectParameters.Add("ent_id", UCComboEntidade1._Combo.SelectedValue);

                odsUA.SelectParameters.Add("uad_id", Guid.Empty.ToString());
                odsUA.SelectParameters.Add("uad_nome", _txtNome.Text);
                odsUA.SelectParameters.Add("uad_codigo", _txtCodigo.Text);
                odsUA.SelectParameters.Add("uad_situacao", "0");
                odsUA.SelectParameters.Add("paginado", "true");
                odsUA.DataBind();
                _dgvUA.DataBind();
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

        protected void odsUA_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (e.ExecutingSelectCount)
                e.InputParameters.Clear();
        }

        protected void _dgvUA_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                SetReturns(_dgvUA.DataKeyNames[0], _dgvUA.DataKeys[e.NewEditIndex].Values[0]);
                SetReturns(_dgvUA.DataKeyNames[1], _dgvUA.DataKeys[e.NewEditIndex].Values[1]);
                SetReturns(_dgvUA.DataKeyNames[2], _dgvUA.DataKeys[e.NewEditIndex].Values[2]);
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