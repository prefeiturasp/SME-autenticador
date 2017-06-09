using System;
using System.Collections.Generic;
using System.Web.UI;
using Autenticador.BLL;
using System.Web.UI.WebControls;
using Autenticador.Entities;
using Autenticador.Web.WebProject;

namespace Autenticador.UserControlLibrary.Buscas
{
    public partial class UCUASuperior : Abstract_UCBusca
    {

        #region Propriedade

        /// <summary>
        /// Armazena id da unidade adm superior.
        /// </summary>
        public Guid VsUadId
        {
            get
            {
                if (ViewState["VsUadId"] == null)
                    return Guid.Empty;
                return new Guid(ViewState["VsUadId"].ToString());
            }
            set
            {
                ViewState["VsUadId"] = value;
            }
        }

        /// <summary>
        /// Armazena o nome da unidade adm superior.
        /// </summary>
        public string VsUadNome
        {
            get
            {
                if (ViewState["VsUadNome"] != null)
                    return (string)ViewState["VsUadNome"];
                return string.Empty;
            }
            set
            {
                ViewState["VsUadNome"] = value;
            }
        }

        /// <summary>
        /// Armazena o id da unidade adm.
        /// </summary>
        public Guid VsEntId
        {
            get
            {
                if (ViewState["VsEntId"] == null)
                    return Guid.Empty;
                return new Guid(ViewState["VsEntId"].ToString());
            }
            set
            {
                ViewState["VsEntId"] = value;
            }
        }

        /// <summary>
        /// Armazena id do grupo do usuário.
        /// </summary>
        public Guid VsGruId
        {
            get
            {
                if (ViewState["VsGruId"] == null)
                    return Guid.Empty;
                return new Guid(ViewState["VsGruId"].ToString());
            }
            set
            {
                ViewState["VsGruId"] = value;
            }
        }

        /// <summary>
        /// Armazena id do usuário.
        /// </summary>
        public Guid VsUsuId
        {
            get
            {
                if (ViewState["VsUsuId"] == null)
                    return Guid.Empty;
                return new Guid(ViewState["VsUsuId"].ToString());
            }
            set
            {
                ViewState["VsUsuId"] = value;
            }
        }

        /// <summary>
        /// Armazena id da visão do usuário.
        /// </summary>
        public int VsVisId
        {
            get
            {
                if (ViewState["VsVisId"] != null)
                    return Convert.ToInt32(ViewState["VsVisId"].ToString());
                return -1;
            }
            set
            {
                ViewState["VsVisId"] = value;
            }
        }

        #endregion

        #region Delegates

        public event EventHandler<GridViewCommandEventArgs> RowCommandSelecionar;

        #endregion Delegates

        protected void Page_Load(object sender, EventArgs e)
        {
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
            
            if (exibeComboTipoUA)
                Page.Form.DefaultFocus = UCComboTipoUnidadeAdministrativa1._Combo.ClientID;
            else
                Page.Form.DefaultFocus = _txtNome.ClientID;

            Page.Form.DefaultButton = _btnPesquisar.UniqueID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ScriptManager sm = ScriptManager.GetCurrent(Page);
            if (sm != null)
            {
                sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsPesquisaUA.js"));
            }
        }

        protected void _dgvUA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    int index = int.Parse(e.CommandArgument.ToString());

                    if (e.CommandName.Equals("Selecionar"))
                    {
                        VsUadId = new Guid(_dgvUA.DataKeys[index].Values["uad_id"].ToString());
                        VsUadNome = _dgvUA.DataKeys[index].Values["uad_nome"].ToString();

                        if (RowCommandSelecionar != null)
                            RowCommandSelecionar(sender, e);

                        AbrirFecharPopup("divBuscaUA", "close", string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationWEB._GravaErro(ex);
                throw;
            }
        }

        public void _PesquisarUASuperior(Guid ent_id, Guid uad_idSuperior)
        {
            IList<SYS_UnidadeAdministrativa> lst =
                SYS_UnidadeAdministrativaBO.ConsultarUASuperior(ent_id, uad_idSuperior);

            if (lst.Count > 0)
            {
                VsUadId = lst[0].uad_id;
                VsUadNome = lst[0].uad_nome;
            }
        }

        public void _Limpar()
        {
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

                if (VsVisId != SysVisaoID.Administracao)
                {
                    //ALTERADO SITUAÇÃO 0 PARA 1, POIS ESTAVA TRAZENDO UA's BLOQUEADAS
                    odsUA.SelectParameters.Clear();
                    odsUA.SelectMethod = "GetSelectBy_UsuarioGrupoUA";
                    odsUA.SelectParameters.Add("gru_id", VsGruId.ToString());
                    odsUA.SelectParameters.Add("usu_id", VsUsuId.ToString());
                    odsUA.SelectParameters.Add("tua_id", Guid.Empty.ToString());
                    odsUA.SelectParameters.Add("ent_id", VsEntId.ToString());
                    odsUA.SelectParameters.Add("uad_id", Guid.Empty.ToString());
                    odsUA.SelectParameters.Add("uad_nome", string.Empty);
                    odsUA.SelectParameters.Add("uad_codigo", string.Empty);
                    odsUA.SelectParameters.Add("uad_situacao", "1");
                    odsUA.SelectParameters.Add("paginado", "true");
                }
                else
                {
                    //ALTERADO SITUAÇÃO 0 PARA 1, POIS ESTAVA TRAZENDO UA's BLOQUEADAS
                    odsUA.SelectParameters.Add("uad_id", Guid.Empty.ToString());
                    odsUA.SelectParameters.Add("ent_id", VsEntId.ToString());
                    odsUA.SelectParameters.Add("uad_nome", _txtNome.Text);
                    odsUA.SelectParameters.Add("uad_codigo", _txtCodigo.Text);
                    odsUA.SelectParameters.Add("uad_situacao", "1");
                    odsUA.SelectParameters.Add("paginado", "true");
                }

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

        /// <summary>
        /// Abre o formulário em modo popup na tela
        /// </summary>
        public void ExibirForm()
        {
            _Limpar();
            AbrirFecharPopup("divBuscaUA", "open", string.Format("Consulta de unidade administrativa superior"));
        }

        /// <summary>
        /// Abre e fecha o popup
        /// </summary>
        /// <param name="div">Nome da div</param>
        /// <param name="action">Open: abrir | Close: fechar</param>
        /// <param name="resource">Título do formulário</param>
        public void AbrirFecharPopup(string div, string action, string resource)
        {
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "NovoItem", "$('#" + div + "').dialog('" + action + "');" +
                (String.IsNullOrEmpty(resource) ? "" : " $('#" + div + "').dialog({title: '" + resource + "'}); "), true);
        }

        protected void btnModalCancelar_Click(object sender, EventArgs e)
        {
            _Limpar();
            AbrirFecharPopup("divBuscaUA", "close", string.Empty);
        }
    }
}